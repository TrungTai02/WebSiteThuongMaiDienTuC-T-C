using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Areas.Admin.Controllers
{
    public class BaichuaduyetController : Controller
    {
        // GET: Admin/Baichuaduyet
        private TMDTDbConText db = new TMDTDbConText();
        public ActionResult Index()
        {
            int idKiemDuyet = 1;
            var chuaDuyetBaiDang = db.BaiDangs.Where(b => b.IDKiemDuyet == idKiemDuyet).ToList();

            return View(chuaDuyetBaiDang);
        }
        [HttpPost]
        public ActionResult DuyetBai(int id)
        {
            // Tìm bài đăng trong cơ sở dữ liệu dựa trên ID
            var baiDang = db.BaiDangs.Find(id);

            if (baiDang != null)
            {
                // Cập nhật trạng thái của bài đăng thành 2
                baiDang.IDKiemDuyet = 2; 

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }

            // Chuyển hướng người dùng đến trang danh sách bài đăng sau khi duyệt
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Baivietkhonghople(int id)
        {
            // Tìm bài đăng trong cơ sở dữ liệu dựa trên ID
            var baiDang = db.BaiDangs.Find(id);

            if (baiDang != null)
            {
                // Lấy người đăng bài
                var userId = baiDang.UserID;
                var dbUser = db.Users.Find(userId);

                // Kiểm tra xem người dùng và bài đăng tồn tại
                if (dbUser != null)
                {
                    // Cộng thêm 10 xu cho người dùng
                    dbUser.ViTiens.FirstOrDefault().SoluongXu += 10;
                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    // Cập nhật giá trị Xu trong Session
                    UserLogin user = Session[Code_Func.User_Session] as UserLogin;
                    user.SoLuongXu = dbUser.ViTiens.FirstOrDefault().SoluongXu;
                    Session[Code_Func.User_Session] = user;
                }

                // Cập nhật trạng thái của bài đăng thành 3
                baiDang.IDKiemDuyet = 3;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }

            // Chuyển hướng người dùng đến trang danh sách bài đăng sau khi xử lý
            return RedirectToAction("Index");
        }

    }
}
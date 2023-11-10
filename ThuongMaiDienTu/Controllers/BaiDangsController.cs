using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Controllers
{
    public class BaiDangsController : Controller
    {
        private TMDTDbConText db = new TMDTDbConText();
        #region phần này là hiển thị
        // GET: BaiDangs
        public ActionResult Index()
        {
            var user = Session[Code_Func.User_Session] as UserLogin;

            if (user != null)
            {
                // Truy vấn chỉ các bài đăng của người dùng hiện tại
                var baiDangs = db.BaiDangs
                    .Include(b => b.DanhMuc)
                    .Include(b => b.User)
                    .Include(b => b.TrangThai1)
                    .Where(b => b.UserID == user.UserID)
                    .ToList();

                return View(baiDangs);
            }

            // Xử lý trường hợp người dùng không được xác thực
            return RedirectToAction("Index", "Login"); // Chuyển hướng đến trang đăng nhập
        }
        #endregion


        #region Phần này là thêm mới tin tức
        // GET: BaiDangs/Create
        public ActionResult Create()
        {
            // Kiểm tra quyền truy cập ở đây (đảm bảo người dùng đã đăng nhập và có quyền tạo bài đăng)

            ViewBag.IDDanhMuc = new SelectList(db.DanhMucs, "IDDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: BaiDangs/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiDang baiDang, HttpPostedFileBase[] images)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu người dùng có đủ số xu (ít nhất 10 xu) để đăng tin
                UserLogin user = Session[Code_Func.User_Session] as UserLogin;
                var userId = user.UserID;
                var dbUser = db.Users.Find(userId);

                if (dbUser != null && dbUser.ViTiens.FirstOrDefault().SoluongXu >= 10)
                {
                    // Trừ 10 xu
                    dbUser.ViTiens.FirstOrDefault().SoluongXu -= 10;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    // Cập nhật lại giá trị Xu trong Session
                    user.SoLuongXu = dbUser.ViTiens.FirstOrDefault().SoluongXu;
                    Session[Code_Func.User_Session] = user;

                    // Thêm người đăng tin vào bài đăng
                    baiDang.UserID = userId;

                    if (images != null && images.Length > 0)
                    {
                        foreach (var image in images)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                var imageMoRong = Path.GetExtension(image.FileName);
                                var imageName = Path.GetFileName(image.FileName);
                                var tenDayDu = imageName + DateTime.Now.ToString("yyyyMMddHHmmss") + imageMoRong;
                                var imagePath = Path.Combine(Server.MapPath("~/Content/Images/"), tenDayDu);
                                image.SaveAs(imagePath);

                                // Thêm đường dẫn vào danh sách
                                baiDang.HinhAnhBaiDangs.Add(new HinhAnhBaiDang { DuongDanHinhAnh = tenDayDu });
                            }
                        }
                    }

                    db.BaiDangs.Add(baiDang);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Người dùng không có đủ xu, hiển thị thông báo hoặc thực hiện xử lý tùy ý
                    TempData["Message"] = "Không đủ xu để đăng tin.";
                }
            }

            // Nếu ModelState không hợp lệ, trả về lại form Create
            return View(baiDang);
        }

        #endregion


        #region phần này là detele
        public ActionResult Delete(int id)
        {
            var baiDang = db.BaiDangs.Find(id); // Tìm bài đăng cần xóa
            if (baiDang == null)
            {
                return HttpNotFound(); // Xử lý trường hợp không tìm thấy bài đăng
            }

            return View(baiDang); // Trả về view xác nhận xóa bài đăng
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var baiDang = db.BaiDangs.Find(id);
            // Lấy danh sách hình ảnh liên quan đến bài đăng
            var hinhAnh = db.HinhAnhBaiDangs.Where(h => h.IDBaiDang == baiDang.IDBaiDang).ToList();
            if (baiDang == null)
            {
                return HttpNotFound();
            }

            // Xóa bài đăng
            db.BaiDangs.Remove(baiDang);

            // Xóa tất cả các hình ảnh liên quan
            foreach (var image in hinhAnh)
            {
                db.HinhAnhBaiDangs.Remove(image);
            }

            db.SaveChanges();

            return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách bài đăng sau khi xóa
        }
        #endregion
    }
}

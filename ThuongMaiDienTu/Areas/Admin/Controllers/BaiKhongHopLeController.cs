using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Areas.Admin.Controllers
{
    public class BaiKhongHopLeController : Controller
    {
        // GET: Admin/BaiKhongHopLe
        private TMDTDbConText db = new TMDTDbConText();
        public ActionResult Index()
        {
            int idKiemDuyet = 3;
            var khongHopLeBaiDang = db.BaiDangs.Where(b => b.IDKiemDuyet == idKiemDuyet).ToList();
        
            return View(khongHopLeBaiDang);
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
        public ActionResult XoaBaiDang(int id)
        {
            // Tìm bài đăng trong cơ sở dữ liệu dựa trên ID
            var baiDang = db.BaiDangs.Find(id);

            if (baiDang != null)
            {
                // Xóa bài đăng khỏi cơ sở dữ liệu
                db.BaiDangs.Remove(baiDang);

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }

            // Chuyển hướng người dùng đến trang danh sách bài đăng sau khi xóa
            return RedirectToAction("Index");
        }
    }
}
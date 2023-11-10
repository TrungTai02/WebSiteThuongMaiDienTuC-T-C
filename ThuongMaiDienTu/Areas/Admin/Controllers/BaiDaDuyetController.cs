using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Areas.Admin.Controllers
{
    public class BaiDaDuyetController : Controller
    {
        // GET: Admin/BaiDaDuyet
        private TMDTDbConText db = new TMDTDbConText();
        public ActionResult Index()
        {
            int idKiemDuyet = 2;
            var duyetBaiDang = db.BaiDangs.Where(b => b.IDKiemDuyet == idKiemDuyet).ToList();

            return View(duyetBaiDang);
        }
        
        [HttpPost]
        public ActionResult DungHienThi(int id)
        {
            // Tìm bài đăng trong cơ sở dữ liệu dựa trên ID
            var baiDang = db.BaiDangs.Find(id);

            if (baiDang != null)
            {
                // Cập nhật trạng thái của bài đăng thành 1 (hoặc giá trị tương ứng)
                baiDang.IDKiemDuyet = 4; 

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
            }

            // Chuyển hướng người dùng đến trang danh sách bài đăng sau khi duyệt
            return RedirectToAction("Index");
        }
    }
}
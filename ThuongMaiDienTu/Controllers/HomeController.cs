using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Controllers
{
    public class HomeController : Controller
    {
        TMDTDbConText ojbTMDTDbConText = new TMDTDbConText();
        public ActionResult Index()
        {
            var listBaiDang = ojbTMDTDbConText.BaiDangs.ToList();  
            return View(listBaiDang);
        }
        #region Phần này hiển thị theo danh mục
        //Danh mục
        public ActionResult DanhMuc()
        {
            var danhMucList = ojbTMDTDbConText.DanhMucs.ToList();
            return PartialView("Partial_DanhMucMenu", danhMucList);
        }
        public ActionResult HienThiBaiDangTheoDanhMuc(int idDanhMuc)
        {
            var danhMuc = ojbTMDTDbConText.DanhMucs.FirstOrDefault(d => d.IDDanhMuc == idDanhMuc);
            if (danhMuc != null)
            {
                var baiDangList = ojbTMDTDbConText.BaiDangs
                    .Where(b => b.IDDanhMuc == idDanhMuc)
                    .ToList();
                ViewBag.TenDanhMuc = danhMuc.TenDanhMuc;
                return View("Index",baiDangList);
            }
            return HttpNotFound();
        }
        #endregion

        #region phần này hiển thị chi tiết
        //chi tiết sản phẩm
        public ActionResult ChiTiet(int id)
        {
            var baiDang = ojbTMDTDbConText.BaiDangs.FirstOrDefault(b => b.IDBaiDang == id);

            if (baiDang == null)
            {
                // Xử lý khi không tìm thấy bài đăng, ví dụ: Hiển thị trang 404
                return HttpNotFound();
            }

            return View(baiDang);
        }
        #endregion

        #region Tìm kiếm
        // tìm kiếm sản phẩm
        public ActionResult TimKiemTheoChuDe(string tukhoa)
        {
            
            var danhSachBaiDang = ojbTMDTDbConText.BaiDangs
                .Where(b => b.ChuDe != null && b.ChuDe.ToUpper().Contains(tukhoa))
                .ToList();

            return View("Index", danhSachBaiDang);
        }
        #endregion

        [HttpGet]
        public ActionResult CheckLoginStatus()
        {
            UserLogin user = Session[Code_Func.User_Session] as UserLogin;
            bool isLoggedIn = (user != null);
            return Json(new { isLoggedIn }, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Areas.Admin.Controllers
{
    public class BaiQuaHanController : Controller
    {
        // GET: Admin/BaiQuaHan
        private TMDTDbConText db = new TMDTDbConText();
        public ActionResult Index()
        {
            int idKiemDuyet = 4;
            var giaHanBaiDang = db.BaiDangs.Where(b => b.IDKiemDuyet == idKiemDuyet).ToList();
            return View(giaHanBaiDang);
        }
       
    }
}
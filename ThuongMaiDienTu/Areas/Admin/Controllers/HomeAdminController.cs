using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using ThuongMaiDienTu.DungChung;

namespace ThuongMaiDienTu.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            var User = Session[Code_Func.User_Session] as UserLogin;

            if(User != null)
            {
                int chucVu = User.ChucVu;
                if (chucVu == 1)
                {
                    return View();
                }
                return Redirect("~/Login/Index");

            }
            else
                return Redirect("~/Login/Index");


        }
      
    }
}
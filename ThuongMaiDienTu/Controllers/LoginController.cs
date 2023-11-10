using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
                return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModels model)
        {
            if (ModelState.IsValid)
            {
                var xb = new User_TMDT();
                var result = xb.Login(model.UserName, model.Password);
                

                //Đăng nhập với quyền admin= 2, user =1
                if (result == 1 || result == 2)
                {
                    var session = new UserLogin();
                    var user = xb.getItem(model.UserName);
                    var users = xb.getUserInfo(model.UserName);
                    session.Username = user.UserName;
                    session.UserID = user.UserID;
                    session.HovaTen = user.HoVaTen;
                    session.ChucVu = user.ChucVu;
                    var soLuongXu = user.ViTiens.Sum(vt => vt.SoluongXu);
                    session.SoLuongXu = soLuongXu;

                    session.EmailConfimed = user.EmailConfimed;

                    //Them vao code_func
                    Session.Add(Code_Func.User_Session, session);
                    if (session.EmailConfimed == false)
                    {
                        Session[Code_Func.User_Session] = null;
                      
                        return RedirectToAction("EmailConfirmFasle", "Register");
                    }
                    else
                    {
                       
                        return RedirectToAction("Index", "Home");
                    }

                }
                else if (result == 0 )
                {
                    TempData["ErrorMessage"] = "Sai mật khẩu";
                }
                else
                {
                    TempData["ErrorMessage"] = "Tài khoản không tồn tại";
                }    
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            // Xử lý đăng xuất ở đây (ví dụ: xóa phiên đăng nhập, xóa cookie đăng nhập, vv.)

            // Sau khi xử lý đăng xuất, đặt userInfo về null
            Session[Code_Func.User_Session] = null;

            // Chuyển hướng người dùng đến trang chính
            return RedirectToAction("Index", "Home"); 
        }

    }

}
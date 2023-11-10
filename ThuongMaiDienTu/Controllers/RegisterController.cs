using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;
using System.Net;
using System.Net.Mail;
using ThuongMaiDienTu.Helper;
using System.Threading.Tasks;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ThuongMaiDienTu.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register

        private TMDTDbConText db = new TMDTDbConText(); // Thay YourDbContext bằng context của bạn
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            // Kiểm tra xem UserName đã tồn tại trong cơ sở dữ liệu hay chưa
            bool isUserNameAvailable = !db.Users.Any(u => u.UserName == user.UserName);

            if (!isUserNameAvailable)
            {
                // Nếu UserName đã tồn tại, hiển thị thông báo lỗi và không thực hiện đăng ký
                ModelState.AddModelError("UserName", "Vui Lòng nhập UserName");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.EmailConfimed = false; // Đặt trạng thái xác thực email là false

                // Thực hiện đăng ký nếu dữ liệu hợp lệ
                db.Users.Add(user);
                db.SaveChanges(); // Lưu user mới vào cơ sở dữ liệu để có UserID

                // Tạo một ví tiền mới và gán UserID cho nó
                ViTien newViTien = new ViTien
                {
                    UserID = user.UserID, // Gán UserID của user cho ViTien
                    SoluongXu = 0 // Đặt số lượng xu
                };

                db.ViTiens.Add(newViTien); // Thêm ví tiền vào cơ sở dữ liệu
                db.SaveChanges(); // Lưu ví tiền mới vào cơ sở dữ liệu

                // Tạo mã xác thực và gửi email xác thực (sử dụng mã xác thực trong email)
                var code = Guid.NewGuid().ToString(); // Tạo một mã xác thực 
                var callbackUrl = Url.Action("ConfirmEmail", "Register", new { userId = user.UserID, code }, protocol: Request.Url.Scheme);


                // Gửi email xác thực
                SendEmail.SendEmails(user.Email, "Xác thực tài khoản", "Nhấn vào liên kết sau để xác thực tài khoản: <a href=\"" + callbackUrl + "\">Xác thực</a>", "");

                return View("EmailConfirmFasle");
            }

            return View(user);
        }
        [HttpGet]
        public ActionResult EmailConfirmFasle()
        {
            return View();
        }
        public ActionResult ConfirmEmail(int userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("EmailConfirmFasle");
            }

            var user = db.Users.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                return View("EmailConfirmFasle");
            }

            if (user.EmailConfimed)
            {
                // Người dùng đã xác thực email rồi, bạn có thể xử lý tương ứng ở đây
                return RedirectToAction("Index", "Home");
            }

            // Xác thực email bằng cách cập nhật trạng thái EmailConfirmed
            user.EmailConfimed = true;
            db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index", "Home");
        }

      

       //Kiểm tra userName đã tồn tại trong db
       [HttpPost]
        public JsonResult CheckUserNameInDB(string UserName)
        {
                bool isAvailable = !db.Users.Any(u => u.UserName == UserName);
                return Json(isAvailable);
        }
    }
}
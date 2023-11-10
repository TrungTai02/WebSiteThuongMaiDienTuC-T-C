using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Controllers
{
    public class GiaoDichController : Controller
    {
        // GET: GiaoDich
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LichSuGiaoDich()
        {
            return View();
        }
        public ActionResult NapXu()
        {
            return View();
        }
        public ActionResult CapNhatXu(int soluongxu)
        {
            UserLogin users = Session[Code_Func.User_Session] as UserLogin;
            int currentUserId = users.UserID;

            using (var context = new TMDTDbConText()) 
            {
                var user = context.Users.Find(currentUserId);
                if (user != null)
                {
                    // Cập nhật số lượng xu của người dùng
                    user.ViTiens.FirstOrDefault().SoluongXu += soluongxu;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    users.SoLuongXu += soluongxu;

                    Session[Code_Func.User_Session] = users;
                    // Trả về một phản hồi thành công
                    return Json(new { success = true });
                }
            }

            // Nếu có lỗi hoặc không tìm thấy người dùng, trả về một phản hồi lỗi
            return Json(new { success = false, message = "Cập nhật thất bại." });
        }

        // lấy xu hiển thị
        public ActionResult GetXuFromSession()
        {
            UserLogin user = Session[Code_Func.User_Session] as UserLogin;
            int userId = user.UserID;

            using (var context = new TMDTDbConText())
            {
                var dbUser = context.Users.Find(userId);
                if (dbUser != null)
                {
                    int xuCount = dbUser.ViTiens.FirstOrDefault().SoluongXu;
                    return Json(new { success = true, xuCount = xuCount }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { success = false, message = "Lỗi khi lấy số Xu." }, JsonRequestBehavior.AllowGet);
        }



    }
}
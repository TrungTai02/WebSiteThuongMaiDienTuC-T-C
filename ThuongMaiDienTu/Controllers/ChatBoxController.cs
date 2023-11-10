using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.DungChung;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.Controllers
{
    public class ChatBoxController : Controller
    {
        // GET: ChatBox
        private TMDTDbConText db = new TMDTDbConText();
        public ActionResult ChatBox()
        {
            
            var User = Session[Code_Func.User_Session] as UserLogin;

            if (User != null)
            {
                ViewBag.UserID = User.UserID;
                ViewBag.HoVaTen = User.HovaTen;
                return View();
            }
            else
                return Redirect("~/Login/Index");

        }

  
        #region Lấy danh sách chat
        public JsonResult GetUserList()
        {
            var User = Session[Code_Func.User_Session] as UserLogin;
            int userID = User.UserID;
            

            var userList = db.Hoithoai
            .Where(tc => tc.User1ID == userID || tc.User2ID == userID)
            .Select(tc => tc.User1ID == userID ? tc.USer2 : tc.USer1)
            .Distinct()
            .Select(user => new
            {
                HoVaTen = user.HoVaTen,
                userid = user.UserID

            })
            .ToList();


            return Json(userList, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region lịch sử cuộc trò chuyện
        [HttpGet]
        public JsonResult GetChatHistory(int userID)
        {
            var currentUser = Session[Code_Func.User_Session] as UserLogin;
            if (currentUser == null)
            {
                return Json(new { error = "Not logged in" });
            }

            var chatHistory = db.LichSuTroChuyens
                .Where(c => (c.UserID == currentUser.UserID && c.ReceiverUserID == userID) || (c.UserID == userID && c.ReceiverUserID == currentUser.UserID))
                .OrderBy(c => c.Timestamp)
                .Select(c => new
                {
                    UserID = c.UserID,
                    Message = c.Message
                })
                .ToList();

            return Json(chatHistory, JsonRequestBehavior.AllowGet);
        }

        #endregion



        #region Gửi tin nhắn
        [HttpPost]
        public JsonResult SendMessage(int userID, string message)
        {
            var currentUser = Session[Code_Func.User_Session] as UserLogin;

            if (currentUser == null)
            {
                return Json(new { error = "Not logged in" });
            }

            // Kiểm tra xem userID có hợp lệ không
            if (userID <= 0)
            {
                return Json(new { error = "Invalid user ID" });
            }

            // Kiểm tra xem message có rỗng không
            if (string.IsNullOrWhiteSpace(message))
            {
                return Json(new { error = "Message is empty" });
            }

            // Tạo một đối tượng LichSuTroChuyen để lưu tin nhắn vào cơ sở dữ liệu
            var chatMessage = new LichSuTroChuyen
            {
                UserID = currentUser.UserID,
                ReceiverUserID = userID,
                Message = message,
                Timestamp = DateTime.Now
            };

            // Kiểm tra và tạo cuộc trò chuyện nếu cần
            var existingChat = db.Hoithoai.FirstOrDefault(c => (c.User1ID == currentUser.UserID && c.User2ID == userID) || (c.User1ID == userID && c.User2ID == currentUser.UserID));

            if (existingChat == null)
            {
                // Cuộc trò chuyện chưa tồn tại, tạo mới
                var newChat = new TroChuyen
                {
                    User1ID = currentUser.UserID,
                    User2ID = userID
                };

                db.Hoithoai.Add(newChat);
                db.SaveChanges();

                // Sau khi tạo cuộc trò chuyện, bạn có thể lấy ID của nó từ newChat.IDHoiThoai
                // Và sau đó, thêm tin nhắn mới vào danh sách tin nhắn
                chatMessage.TroChuyenID = newChat.IDHoiThoai;
            }
            else
            {
                // Cuộc trò chuyện đã tồn tại, sử dụng ID của nó để thêm tin nhắn
                chatMessage.TroChuyenID = existingChat.IDHoiThoai;
            }

            try
            {
                // Lưu tin nhắn vào cơ sở dữ liệu
                db.LichSuTroChuyens.Add(chatMessage);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { error = "Failed to send message", details = ex.Message });
            }
        }
        #endregion


        [HttpPost]
        public ActionResult CreateChat()
        {
            var User = Session[Code_Func.User_Session] as UserLogin;

            // Đọc user2Id từ dữ liệu gửi lên
            int user2Id;
            if (!int.TryParse(Request.Form["nguoiDangBaiID"], out user2Id))
            {
                return Json(new { error = "Invalid user ID" });
            }

            // Kiểm tra xem cuộc trò chuyện đã tồn tại chưa bằng cách kiểm tra trong cơ sở dữ liệu
            var existingChat = db.Hoithoai.FirstOrDefault(c =>
                (c.User1ID == User.UserID && c.User2ID == user2Id) ||
                (c.User1ID == user2Id && c.User2ID == User.UserID));

            if (existingChat != null)
            {
                // Cuộc trò chuyện đã tồn tại, chuyển hướng đến trang chat hoặc thực hiện hành động tương ứng
               
                return Json(new { success = true, chatId = existingChat.IDHoiThoai });
            }

            // Nếu cuộc trò chuyện chưa tồn tại, tạo mới và lưu vào cơ sở dữ liệu
            var chat = new TroChuyen
            {
                User1ID = User.UserID,
                User2ID = user2Id
            };

            db.Hoithoai.Add(chat);
            db.SaveChanges();

            // Sau khi tạo cuộc trò chuyện, bạn có thể lấy ID của nó từ chat.IDHoiThoai
            return Json(new { success = true, chatId = user2Id });
        }

    }
}

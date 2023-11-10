using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ThuongMaiDienTu.Models;
using ThuongMaiDienTu.Models.EF;

namespace ThuongMaiDienTu.DungChung
{

    //đây là class để dùng viết các hàm xử lý

    public class User_TMDT
    {

        TMDTDbConText db = new TMDTDbConText();

        public User getUserInfo(string username)
        {
            using (var context = new TMDTDbConText())
            {
                return context.Users
                    .Where(u => u.UserName == username)
                    .Include(u => u.ViTiens) // Đảm bảo sử dụng Include để tải ViTiens
                    .FirstOrDefault();
            }
        }
        public User Add(User nv)
        {
            db.Users.Add(nv);
            db.SaveChanges();
            return nv;
        }
        public BaiDang AddBaiDang(BaiDang bd)
        {
            db.BaiDangs.Add(bd);
            db.SaveChanges();
            return bd;
        }
        public User getItem(String UserName)
        {
            return db.Users.FirstOrDefault(u => u.UserName == UserName);
        }
        public List<User> getList()
        {
            return db.Users.ToList();
        }

        public int Login(String UserName, String password)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == UserName);
            if (user == null)
            {
                return -1; //Tai khoản không tồn tại
            }
            else if (user.Password == password)
            {
                if (user.ChucVu == 1)
                    return 2;// đăng nhập với quyền admin
                else
                    return 1;// đăng nhập với quyền user
            }
            else
                return 0;// sai mật khẩu
        }

    }
}
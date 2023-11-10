using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.DungChung
{
    // Đây là class lưu trữ tất cả thông tin khi đã đăng nhập
    [Serializable]
    public class UserLogin
    {
        public int UserID {set; get;}
        public string Username { set; get; }
        public string HovaTen { set; get; }
        public int ChucVu { set; get; }
        public int SoLuongXu { set; get; }
        public bool EmailConfimed { get; set; }
        //....
    }
}
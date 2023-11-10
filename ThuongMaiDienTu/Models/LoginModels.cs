using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public String UserName { set; get; }

         [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        public String Password { set; get; }

    }
}
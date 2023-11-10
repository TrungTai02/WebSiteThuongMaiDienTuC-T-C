using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ThuongMaiDienTu.StartupOwin))]

namespace ThuongMaiDienTu
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}

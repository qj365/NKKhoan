using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NKKhoan.Startup))]
namespace NKKhoan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

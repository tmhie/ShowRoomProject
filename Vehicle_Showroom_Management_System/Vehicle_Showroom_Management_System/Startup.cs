using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vehicle_Showroom_Management_System.Startup))]
namespace Vehicle_Showroom_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

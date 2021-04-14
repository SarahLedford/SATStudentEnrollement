using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SATStudentEnrollment.UI.MVC.Startup))]
namespace SATStudentEnrollment.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

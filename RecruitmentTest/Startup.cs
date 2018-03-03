using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecruitmentTest.Startup))]
namespace RecruitmentTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

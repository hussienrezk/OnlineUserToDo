using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineUserToDo.Startup))]
namespace OnlineUserToDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

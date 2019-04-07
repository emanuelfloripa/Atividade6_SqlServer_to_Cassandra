using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Atividade6_Cassandra.Startup))]
namespace Atividade6_Cassandra
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

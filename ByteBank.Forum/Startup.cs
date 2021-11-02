using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using ByteBank.Forum.Identity;
using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(ByteBank.Forum.Startup))]

namespace ByteBank.Forum
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(
                () => new IdentityDbContext<UsuarioAplicacao>("DefaultConnection"));

            builder.CreatePerOwinContext<IUserStore<UsuarioAplicacao>>((
                opcoes,
                contextoOwin) =>
            {
                var dbContext = contextoOwin.Get<DbContext>();
                return new UserStore<UsuarioAplicacao>(dbContext);
            });

            builder.CreatePerOwinContext<UserManager<UsuarioAplicacao>>((
                opcoes,
                contextoOwin) =>
            {
                var userStore = contextoOwin.Get<IUserStore<UsuarioAplicacao>>();
                var userManager = new UserManager<UsuarioAplicacao>(userStore);

                var userValidator = new UserValidator<UsuarioAplicacao>(userManager)
                {
                    RequireUniqueEmail = true
                };

                userManager.UserValidator = userValidator;
                userManager.PasswordValidator = new SenhaValidador()
                {
                    TamanhoRequerido = 6,
                    ObrigCaractExpeciais = true,
                    ObrigDigitos = true,
                    ObrigLowerCase = true,
                    ObrigUpperCase = true
                };

                userManager.EmailService = new EmailService();

                var dataProtectionProvider = opcoes.DataProtectionProvider;
                var dataProtectionProviderCreated = dataProtectionProvider.Create("ByteBank.Forum");

                userManager.UserTokenProvider =
                    new DataProtectorTokenProvider<UsuarioAplicacao>(dataProtectionProviderCreated);

                return userManager;
            });
        }
    }
}
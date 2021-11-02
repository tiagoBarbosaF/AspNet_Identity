using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        private UserManager<UsuarioAplicacao> _userManager;

        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if (_userManager != null) return _userManager;

                var contextOwin = HttpContext.GetOwinContext();
                _userManager = contextOwin.GetUserManager<UserManager<UsuarioAplicacao>>();

                return _userManager;
            }
            set => _userManager = value;
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var novoUsuario = new UsuarioAplicacao
            {
                Email = model.Email,
                UserName = model.UserName,
                NomeCompleto = model.NomeCompleto
            };

            var usuario = await UserManager.FindByEmailAsync(model.Email);
            var usuarioExistente = usuario != null;

            if (usuarioExistente)
                return View("AguardandoConfirmacao");

            var resultado = await UserManager.CreateAsync(novoUsuario, model.Senha);

            if (resultado.Succeeded)
            {
                await EnviarEmailDeConfirmacaoAsync(novoUsuario);
                return View("AguardandoConfirmacao");
            }
            else
                AdicionarErros(resultado);

            return View(model);
        }

        private async Task EnviarEmailDeConfirmacaoAsync(UsuarioAplicacao usuarioAplicacao)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(usuarioAplicacao.Id);

            var linkDeCallback =
                Url.Action(
                    "ConfirmacaoEmail",
                    "Conta",
                    new
                    {
                        usuarioId = usuarioAplicacao.Id,
                        token = token,
                        Request.Url.Scheme
                    });

            await UserManager.SendEmailAsync(
                usuarioAplicacao.Id,
                "Fórum ByteBank - Confirmação de Email",
                $"Bem vindo ao fórum ByteBank, clique aqui {linkDeCallback} para confirmar seu email!");
        }

        public async Task<ActionResult> ConfirmacaoEmail(string usuarioId, string token)
        {
            if (usuarioId == null || token == null)
                return View("Error");

            var resultado = await UserManager.ConfirmEmailAsync(usuarioId, token);

            if (resultado.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        private void AdicionarErros(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
                ModelState.AddModelError("", error);
        }
    }
}
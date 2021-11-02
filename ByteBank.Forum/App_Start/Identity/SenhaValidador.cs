using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ByteBank.Forum.Identity
{
    public class SenhaValidador : IIdentityValidator<string>
    {
        public int TamanhoRequerido { get; set; }
        public bool ObrigCaractExpeciais { get; set; }
        public bool ObrigLowerCase { get; set; }
        public bool ObrigUpperCase { get; set; }
        public bool ObrigDigitos { get; set; }

        public async Task<IdentityResult> ValidateAsync(string item)
        {
            var erros = new List<string>();
            
            // if(ObrigCaractExpeciais && !VerificaCaracteresEspeciais(item))
            //     erros.Add("A senha deve conter caracteres especiais!");
            
            if(!VerificarTamanhoRequerido(item))
                erros.Add($"A senha deve conter no mínimo {TamanhoRequerido} caracteres.");
            
            if(ObrigLowerCase && !VerificarLowerCase(item))
                erros.Add("A senha deve conter no mínimo uma letra minúscula.");
            
            if(ObrigUpperCase && !VerificarUpperCase(item))
                erros.Add("A senha deve conter no mínimo uma letra maiúscula.");
            
            if(ObrigDigitos && !VerificarDigit(item))
                erros.Add("A senha deve conter no mínimo um dígito.");

            return erros.Any() ? IdentityResult.Failed(erros.ToArray()) : IdentityResult.Success;
        }

        private bool VerificarTamanhoRequerido(string senha) => senha?.Length >= TamanhoRequerido;
        private bool VerificaCaracteresEspeciais(string senha) => Regex.IsMatch(senha, @"!@#&%=-]\/}{:<>,");
        private bool VerificarLowerCase(string senha) => senha.Any(char.IsLower);
        private bool VerificarUpperCase(string senha) => senha.Any(char.IsUpper);
        private bool VerificarDigit(string senha) => senha.Any(char.IsDigit);
    }
}
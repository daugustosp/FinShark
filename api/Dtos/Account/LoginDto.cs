using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class LoginDto
    {
       
        [Required(ErrorMessage = "O Usuario � obrigat�rio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A senha � obrigat�ria.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres.")]
    
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$",
        ErrorMessage = "A senha deve conter letra mai�scula, min�scula, n�mero e caractere especial.")]

        public string Password { get; set; }

    }
}
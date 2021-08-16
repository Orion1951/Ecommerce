using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Areas.Usuarios.Models
{
   public class InputModelRegister
   {
      [Required(ErrorMessage = "El campo {0} es obligatorio.")]
      public string Nombre { get; set; }
      [Required(ErrorMessage = "El campo {0} es obligatorio.")]
      public string Apellido { get; set; }
      [Required(ErrorMessage = "El campo {0} es obligatorio.")]
      [DataType(DataType.PhoneNumber)]     
      [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage ="El formato de telefono ingresado no es correcto.")]
      public string NumeroTelefonico { get; set; }
      [Required(ErrorMessage ="El campo {0} es obligatorio.")]
      [EmailAddress(ErrorMessage = "La informacion no corresponde a una direccion de correo valida.")]
      public string Email { get; set; }
      [Display(Name = "Contraseña")]
      [Required(ErrorMessage ="El campo contraseña es obligatorio.")]
      [StringLength(50, ErrorMessage ="El numero de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
      public string Password { get; set; }
      [Required]
      public string Role { get; set; }
   }
}

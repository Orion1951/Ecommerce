using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Areas.Usuarios.Controllers
{
   [Area("Usuarios")]
   public class UsuariosController : Controller
   {
      public IActionResult Usuarios()
      {
         return View();
      }
   }
}

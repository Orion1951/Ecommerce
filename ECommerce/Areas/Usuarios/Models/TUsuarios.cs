﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Areas.Usuarios.Models
{
   public class TUsuarios
   {
      public int Id { get; set; }
      public string Nombre { get; set; }
      public string Apellido { get; set; }
      public string Email { get; set; }
      public string IdUser { get; set; }
      public byte[] Image { get; set; }
   }
}

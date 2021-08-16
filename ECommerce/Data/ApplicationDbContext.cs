using ECommerce.Areas.Usuarios.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
   public class ApplicationDbContext : IdentityDbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }
      public DbSet<TUsuarios> TUsuarios { get; set; }

   }
}

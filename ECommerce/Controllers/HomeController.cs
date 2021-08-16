using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
   public class HomeController : Controller
   {

      //IServiceProvider _serviceProvider;

      public HomeController(IServiceProvider serviceProvider)
      {
         //_serviceProvider= serviceProvider;
      }

      public async Task<IActionResult> Index()
      {
         //await CrearRolesAsync(_serviceProvider);
         return View();
      }

      public IActionResult Privacy()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
      private async Task CrearRolesAsync(IServiceProvider serviceProvider)
      {
         var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
         String[] rolesNames = { "Admin", "Usuario" };
         foreach (var item in rolesNames)
         {
            var roleExist = await roleManager.RoleExistsAsync(item);
            if (!roleExist)
            {
               await roleManager.CreateAsync(new IdentityRole(item));
            }
         }
      }
   }
}

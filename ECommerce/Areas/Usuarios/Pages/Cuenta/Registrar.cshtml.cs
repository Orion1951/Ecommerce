using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Areas.Usuarios.Models;
using ECommerce.Data;
using ECommerce.Library;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Areas.Usuarios.Pages.Cuenta
{
    public class RegistrarModel : PageModel
    {
      private readonly SignInManager<IdentityUser> _signInManager;
      private readonly UserManager<IdentityUser> _userManager;
      private readonly RoleManager<IdentityRole> _roleManager;
      private readonly ApplicationDbContext _dbContext;
      private readonly LUserRoles _userRoles;
      private static InputModel _dataInput;
      private IWebHostEnvironment _environment;
      private readonly UploadImage _uploadImage;

      public RegistrarModel(
         SignInManager<IdentityUser> signInManager,
         UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager,
         ApplicationDbContext dbContext, 
         IWebHostEnvironment environment
         )
      {
         _signInManager = signInManager;
         _userManager = userManager;
         _roleManager = roleManager;
         _dbContext = dbContext;
         _userRoles = new LUserRoles();
         _environment = environment;
         _uploadImage = new UploadImage();
      }
        public void OnGet()
        {
         if (_dataInput != null)
         {
            Input = _dataInput;
            Input.rolesLista = _userRoles.getRoles(_roleManager);
            Input.AvatarImage = null;
         }
         else
         {
            Input = new InputModel
            {
               rolesLista = _userRoles.getRoles(_roleManager)
            };
         }
        }
      [BindProperty]
      public InputModel Input { get; set; }
      public class InputModel : InputModelRegister
      {
         public IFormFile AvatarImage { get; set; }
         [TempData]
         public string ErrorMessage { get; set; }
         public List<SelectListItem> rolesLista { get; set; }
      }
      public async Task<IActionResult> OnPost()
      {
         if (await SaveAsync())
         {
            return Redirect("/Usuarios/Usuarios?area=Usuarios");
         }
         else
         {
            return Redirect("/Usuarios/Registrar");
         }
      }
      private async Task<bool> SaveAsync()
      {
         _dataInput = Input;
         var valor = false;
         if (!Input.Role.Equals("Seleccione un rol"))
         {
            var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
            if (userList.Count.Equals(0))
            {
               var strategy = _dbContext.Database.CreateExecutionStrategy();
               await strategy.ExecuteAsync(async () =>
               {
                  using (var transaction = _dbContext.Database.BeginTransaction())
                  {
                     try
                     {
                        var user = new IdentityUser
                        {
                           UserName = Input.Email,
                           Email = Input.Email,
                           PhoneNumber = Input.NumeroTelefonico
                        };
                        var result = await _userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded)
                        {
                           await _userManager.AddToRoleAsync(user, Input.Role);
                           var dataUser = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList().Last();
                           var imageByte = await _uploadImage.ByteAvatarImageAsync(Input.AvatarImage, _environment, "images/images/default.png");
                               var tUser = new TUsuarios
                               {
                                   Nombre = Input.Nombre,
                                   Apellido = Input.Apellido,
                                   Email = Input.Email,
                                   IdUser = dataUser.Id,
                                   Image = imageByte
                               };
                               await _dbContext.AddAsync(tUser);
                               _dbContext.SaveChanges();
                               transaction.Commit();
                               _dataInput = null;
                               valor = true;
                        }
                        else
                        {
                           foreach (var item in result.Errors)
                           {
                              _dataInput.ErrorMessage = item.Description;
                           }
                           valor = false;
                           transaction.Rollback();
                        }
                     }
                     catch (Exception ex)
                     {
                        _dataInput.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        valor = false;
                     }
                  }
               });
            }
            else
            {
               _dataInput.ErrorMessage = $"El {Input.Email} ya esta registrado";
               valor = false;
            }
         }
         else
         {
            _dataInput.ErrorMessage = "Seleccione un rol.";
            valor = false;
         }
         return valor;
      }
    }
}

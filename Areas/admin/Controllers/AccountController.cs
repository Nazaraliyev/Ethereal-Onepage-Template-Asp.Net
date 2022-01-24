using Ethereal_Onepage_Template_Asp.Net.Data;
using Ethereal_Onepage_Template_Asp.Net.Models;
using Ethereal_Onepage_Template_Asp.Net.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ethereal_Onepage_Template_Asp.Net.Areas.admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Index()
        {
            VmAccountIndex model = new VmAccountIndex()
            {
                customUsers = await _userManager.Users.ToListAsync(),
                roles = await _roleManager.Roles.ToListAsync(),
                UserRole = await _context.UserRoles.ToListAsync(),
            };
            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            VmUserRegister model = new VmUserRegister()
            {
                role = await _roleManager.Roles.ToListAsync()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VmUserRegister model)
        {
            model.role = await _roleManager.Roles.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ProfileFile != null)
            {
                if (model.ProfileFile.ContentType == "image/jpeg" || model.ProfileFile.ContentType == "image/png")
                {
                    if (model.ProfileFile.Length > 3145728)
                    {
                        ModelState.AddModelError("", "You can only upload image untline 3mb");
                        return View(model);
                    }

                    string fileName = Guid.NewGuid() + "-" + model.ProfileFile.FileName;
                    string filePath = Path.Combine("wwwroot", "area/admin/img/profile", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfileFile.CopyTo(stream);
                    }

                    model.Profile = fileName;

                }
                else
                {
                    ModelState.AddModelError("", "You can only upload Image file");
                    return View(model);
                }
            }

            if(model.RoleId == "0")
            {
                ModelState.AddModelError("", "Role is required");
                return View(model);
            }

            CustomUser user = new CustomUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Phone,
                Profile = model.Profile
            };

            var result  = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return View(model);
            }

            IdentityUserRole<string> userRole = new IdentityUserRole<string>()
            {
                RoleId = model.RoleId,
                UserId = user.Id
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(string Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            if( !await _userManager.Users.AnyAsync(u => u.Id == Id))
            {
                return NotFound();
            }

            CustomUser oldUser =await _userManager.FindByIdAsync(Id);
            IdentityUserRole<string> userROle = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == Id);

            VmUserRegister willUpdateUser = new VmUserRegister()
            {
                Id = oldUser.Id,
                Name = oldUser.Name,
                Surname = oldUser.Surname,
                Email = oldUser.Email,
                Phone = oldUser.PhoneNumber,
                RoleId = userROle.RoleId,
                Secure = oldUser.SecurityStamp,
                Concurency = oldUser.ConcurrencyStamp
            };

            willUpdateUser.role =await _roleManager.Roles.ToListAsync();
            return View(willUpdateUser);
        }



        public IActionResult Login()
        {
            return View();
        }
    }
}

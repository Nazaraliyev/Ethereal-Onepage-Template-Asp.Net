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
        private readonly SignInManager<CustomUser> _signInManager;

        public AccountController(AppDbContext context, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<CustomUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

            if (model.RoleId == "0")
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

            var result = await _userManager.CreateAsync(user, model.Password);
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
            if (Id == null)
            {
                return NotFound();
            }

            if (!await _userManager.Users.AnyAsync(u => u.Id == Id))
            {
                return NotFound();
            }

            CustomUser oldUser = await _userManager.FindByIdAsync(Id);
            IdentityUserRole<string> userROle = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == Id);

            VmuserUpdate willUpdateUser = new VmuserUpdate()
            {
                Id = oldUser.Id,
                Name = oldUser.Name,
                Surname = oldUser.Surname,
                Email = oldUser.Email,
                Phone = oldUser.PhoneNumber,
                RoleId = userROle.RoleId,
                Secure = oldUser.SecurityStamp,
                Concurency = oldUser.ConcurrencyStamp,
                Profile = oldUser.Profile,
                PasswordHash = oldUser.PasswordHash,
            };

            willUpdateUser.role = await _roleManager.Roles.ToListAsync();
            return View(willUpdateUser);
        }


        [HttpPost]
        public async Task<IActionResult> Update(VmuserUpdate model)
        {
            model.role = await _roleManager.Roles.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ProfileFile != null)
            {
                if (model.Profile != null)
                {
                    string oldProfile = Path.Combine("wwwroot", "area/admin/img/profile", model.Profile);

                    if (System.IO.File.Exists(oldProfile))
                    {
                        System.IO.File.Delete(oldProfile);
                    }
                }

                string fileName = Guid.NewGuid() + "-" + model.ProfileFile.FileName;
                string filePath = Path.Combine("wwwroot", "area/admin/img/profile", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileFile.CopyTo(stream);
                }

                model.Profile = fileName;
            }

            if (model.RoleId == "0")
            {
                ModelState.AddModelError("", "Role is required");
                return View(model);
            }


            //CustomUser user = new CustomUser()
            //{
            //    Id = model.Id,
            //    Name = model.Name,
            //    Surname = model.Surname,
            //    Email = model.Email,
            //    PhoneNumber = model.Phone,
            //    UserName = model.Email,
            //    SecurityStamp = model.Secure,
            //    ConcurrencyStamp = model.Concurency,
            //    Profile = model.Profile,
            //    PasswordHash = model.PasswordHash,
            //    NormalizedEmail = model.Email.ToUpper(),
            //    NormalizedUserName = model.Email.ToUpper(),
            //};

            //var result = await _userManager.UpdateAsync(user);
            //if (!result.Succeeded)
            //{
            //    return View(model);
            //}



            CustomUser user = await _userManager.FindByIdAsync(model.Id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email.ToUpper();
            user.PhoneNumber = model.Phone;
            user.Profile = model.Profile;

            //_context.UserRoles.Remove(await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == model.Id));
            await _context.SaveChangesAsync();

            IdentityUserRole<string> userRole = new IdentityUserRole<string>()
            {
                UserId = model.Id,
                RoleId = model.RoleId,
            };

            //_context.UserRoles.Update(userRole);


            _context.UserRoles.Remove(await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == model.Id));
            await _context.UserRoles.AddAsync(userRole);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async  Task<IActionResult> Reset (string Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            if(!await _userManager.Users.AnyAsync(u => u.Id.Equals(Id))){
                return NotFound();
            }


            VmUserReset model = new VmUserReset()
            {
                Id = Id,
            };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Reset(VmUserReset model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            CustomUser user = await _userManager.FindByIdAsync(model.Id);
            string newPassword = _userManager.PasswordHasher.HashPassword(user,model.Password);
            user.PasswordHash = newPassword;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }




        public async Task<IActionResult> DeleteAsync(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            if (!await _userManager.Users.AnyAsync(u => u.Id == Id))
            {
                return NotFound();
            }

            CustomUser willDeleteUser = await _userManager.FindByIdAsync(Id);

            var result = await _userManager.DeleteAsync(willDeleteUser);

            if (result.Succeeded)
            {
                if (willDeleteUser != null)
                {
                    string Profile = Path.Combine("wwwroot", "area/admin/img/profile", willDeleteUser.Profile);
                    if (System.IO.File.Exists(Profile))
                    {
                        System.IO.File.Delete(Profile);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return NotFound();

        }


        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(VmLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}

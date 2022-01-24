using Ethereal_Onepage_Template_Asp.Net.Data;
using Ethereal_Onepage_Template_Asp.Net.Models;
using Ethereal_Onepage_Template_Asp.Net.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //public IActionResult Create(VmUserRegister model)
        //{
           
        //}
        public IActionResult Login()
        {
            return View();
        }
    }
}

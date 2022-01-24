using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ethereal_Onepage_Template_Asp.Net.Areas.admin.Controllers
{
    [Area("admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (model.Name == null)
            {
                ModelState.AddModelError("", "Role name is required");
                return View(model);
            }


            if (await _roleManager.Roles.AnyAsync(r => r.Name == model.Name))
            {
                ModelState.AddModelError("", "This role exist in database");
                return View(model);
            }

            await _roleManager.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (await _roleManager.FindByIdAsync(Id) == null)
            {
                return NotFound();
            }
            return View(await _roleManager.FindByIdAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole model)
        {
            if (model.Name == null)
            {
                ModelState.AddModelError("", "Role name is empty");
                return View(model);
            }


            if (await _roleManager.Roles.AnyAsync(r => r.Name == model.Name))
            {
                ModelState.AddModelError("", "This role exist in database");
                return View(model);
            }

            await _roleManager.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}

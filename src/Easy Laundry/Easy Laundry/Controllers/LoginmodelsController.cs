using Easy_Laundry.Data;
using Easy_Laundry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Easy_Laundry.Controllers
{
    public class LoginmodelsController : Controller
    {
        private readonly Easy_LaundryContext _context;

        public LoginmodelsController(Easy_LaundryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(Loginmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Loginmodel.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    return RedirectToAction("Index1", "OrderModels");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please try again.");
                    return View("AdminLogin", model);
                }
            }

            return View("AdminLogin");
        }

        // GET: Loginmodels
        public async Task<IActionResult> Index()
        {
            return _context.Loginmodel != null ?
                        View(await _context.Loginmodel.ToListAsync()) :
                        Problem("Entity set 'Easy_LaundryContext.Loginmodel'  is null.");
        }

        // GET: Loginmodels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loginmodel == null)
            {
                return NotFound();
            }

            var loginmodel = await _context.Loginmodel
                .FirstOrDefaultAsync(m => m.UId == id);
            if (loginmodel == null)
            {
                return NotFound();
            }

            return View(loginmodel);
        }

        // GET: Loginmodels/Create
        public IActionResult create()
        {
            return View();
        }

        // POST: Loginmodels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind("UId,Username,Password")] Loginmodel loginmodel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginmodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminLogin));
            }
            return View(loginmodel);
        }

        // GET: Loginmodels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loginmodel == null)
            {
                return NotFound();
            }

            var loginmodel = await _context.Loginmodel.FindAsync(id);
            if (loginmodel == null)
            {
                return NotFound();
            }
            return View(loginmodel);
        }

        // POST: Loginmodels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,Username,Password")] Loginmodel loginmodel)
        {
            if (id != loginmodel.UId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginmodel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginmodelExists(loginmodel.UId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loginmodel);
        }

        // GET: Loginmodels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loginmodel == null)
            {
                return NotFound();
            }

            var loginmodel = await _context.Loginmodel
                .FirstOrDefaultAsync(m => m.UId == id);
            if (loginmodel == null)
            {
                return NotFound();
            }

            return View(loginmodel);
        }

        // POST: Loginmodels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loginmodel == null)
            {
                return Problem("Entity set 'Easy_LaundryContext.Loginmodel'  is null.");
            }
            var loginmodel = await _context.Loginmodel.FindAsync(id);
            if (loginmodel != null)
            {
                _context.Loginmodel.Remove(loginmodel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginmodelExists(int id)
        {
            return (_context.Loginmodel?.Any(e => e.UId == id)).GetValueOrDefault();
        }
    }
}

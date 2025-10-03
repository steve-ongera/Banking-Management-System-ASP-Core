using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Data;
using BankingManagementSystem.Models;

namespace BankingManagementSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly BankingDbContext _context;

        public AccountsController(BankingDbContext context)
        {
            _context = context;
        }

        // GET: Accounts/Index
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.Accounts
                .OrderByDescending(a => a.DateOpened)
                .ToListAsync();
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewBag.AccountTypes = GetAccountTypes();
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,AccountHolderName,Email,PhoneNumber,AccountType,Balance,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
                // Check if account number already exists
                if (await _context.Accounts.AnyAsync(a => a.AccountNumber == account.AccountNumber))
                {
                    ModelState.AddModelError("AccountNumber", "Account number already exists");
                    ViewBag.AccountTypes = GetAccountTypes();
                    return View(account);
                }

                account.DateOpened = DateTime.Now;
                _context.Add(account);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Account created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AccountTypes = GetAccountTypes();
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            ViewBag.AccountTypes = GetAccountTypes();
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,AccountNumber,AccountHolderName,Email,PhoneNumber,AccountType,Balance,DateOpened,IsActive")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if account number already exists for another account
                    if (await _context.Accounts.AnyAsync(a => a.AccountNumber == account.AccountNumber && a.AccountId != account.AccountId))
                    {
                        ModelState.AddModelError("AccountNumber", "Account number already exists");
                        ViewBag.AccountTypes = GetAccountTypes();
                        return View(account);
                    }

                    _context.Update(account);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Account updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.AccountTypes = GetAccountTypes();
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Account deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        private List<string> GetAccountTypes()
        {
            return new List<string>
            {
                "Savings Account",
                "Current Account",
                "Fixed Deposit",
                "Recurring Deposit"
            };
        }
    }
}
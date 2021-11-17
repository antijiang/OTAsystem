using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OTASystem.Data;

namespace OTASystem.Controllers
{
    public class OTAUsersController : Controller
    {
        private readonly OTADb _context;

        public OTAUsersController(OTADb context)
        {
            _context = context;
        }

        // GET: OTAUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.OTAUsers.ToListAsync());
        }

        // GET: OTAUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTAUser = await _context.OTAUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oTAUser == null)
            {
                return NotFound();
            }

            return View(oTAUser);
        }

        // GET: OTAUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OTAUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Expired,MaxOTACount,OTAUsed")] OTAUser oTAUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oTAUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oTAUser);
        }

        // GET: OTAUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTAUser = await _context.OTAUsers.FindAsync(id);
            if (oTAUser == null)
            {
                return NotFound();
            }
            return View(oTAUser);
        }

        // POST: OTAUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Expired,MaxOTACount,OTAUsed")] OTAUser oTAUser)
        {
            if (id != oTAUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oTAUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OTAUserExists(oTAUser.Id))
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
            return View(oTAUser);
        }

        // GET: OTAUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTAUser = await _context.OTAUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oTAUser == null)
            {
                return NotFound();
            }

            return View(oTAUser);
        }

        // POST: OTAUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oTAUser = await _context.OTAUsers.FindAsync(id);
            _context.OTAUsers.Remove(oTAUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OTAUserExists(int id)
        {
            return _context.OTAUsers.Any(e => e.Id == id);
        }
    }
}

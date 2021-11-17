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
    public class OTARecordsController : Controller
    {
        private readonly OTADb _context;

        public OTARecordsController(OTADb context)
        {
            _context = context;
        }

        // GET: OTARecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.OTARecords.ToListAsync());
        }

        // GET: OTARecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTARecord = await _context.OTARecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oTARecord == null)
            {
                return NotFound();
            }

            return View(oTARecord);
        }

        // GET: OTARecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OTARecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Mac,Secret,QueryTime,IsComputed")] OTARecord oTARecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oTARecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oTARecord);
        }

        // GET: OTARecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTARecord = await _context.OTARecords.FindAsync(id);
            if (oTARecord == null)
            {
                return NotFound();
            }
            return View(oTARecord);
        }

        // POST: OTARecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Mac,Secret,QueryTime,IsComputed")] OTARecord oTARecord)
        {
            if (id != oTARecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oTARecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OTARecordExists(oTARecord.Id))
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
            return View(oTARecord);
        }

        // GET: OTARecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oTARecord = await _context.OTARecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oTARecord == null)
            {
                return NotFound();
            }

            return View(oTARecord);
        }

        // POST: OTARecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oTARecord = await _context.OTARecords.FindAsync(id);
            _context.OTARecords.Remove(oTARecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OTARecordExists(int id)
        {
            return _context.OTARecords.Any(e => e.Id == id);
        }
    }
}

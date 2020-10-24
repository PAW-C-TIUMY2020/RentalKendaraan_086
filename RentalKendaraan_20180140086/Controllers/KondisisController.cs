using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan_20180140086.Models;

namespace RentalKendaraan_20180140086.Controllers
{
    public class KondisisController : Controller
    {
        private readonly Rental_KendaraanContext _context;

        public KondisisController(Rental_KendaraanContext context)
        {
            _context = context;
        }

        // GET: Kondisis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kondisi.ToListAsync());
        }

        // GET: Kondisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisi = await _context.Kondisi
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisi == null)
            {
                return NotFound();
            }

            return View(kondisi);
        }

        // GET: Kondisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kondisis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKondisi,NamaKondisi")] Kondisi kondisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kondisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kondisi);
        }

        // GET: Kondisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisi = await _context.Kondisi.FindAsync(id);
            if (kondisi == null)
            {
                return NotFound();
            }
            return View(kondisi);
        }

        // POST: Kondisis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKondisi,NamaKondisi")] Kondisi kondisi)
        {
            if (id != kondisi.IdKondisi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kondisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KondisiExists(kondisi.IdKondisi))
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
            return View(kondisi);
        }

        // GET: Kondisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kondisi = await _context.Kondisi
                .FirstOrDefaultAsync(m => m.IdKondisi == id);
            if (kondisi == null)
            {
                return NotFound();
            }

            return View(kondisi);
        }

        // POST: Kondisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kondisi = await _context.Kondisi.FindAsync(id);
            _context.Kondisi.Remove(kondisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KondisiExists(int id)
        {
            return _context.Kondisi.Any(e => e.IdKondisi == id);
        }
    }
}

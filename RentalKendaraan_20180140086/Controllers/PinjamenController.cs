﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalKendaraan_20180140086.Models;

namespace RentalKendaraan_20180140086.Controllers
{
    public class PinjamenController : Controller
    {
        private readonly Rental_KendaraanContext _context;

        public PinjamenController(Rental_KendaraanContext context)
        {
            _context = context;
        }

        // GET: Pinjamen
        public async Task<IActionResult> Index(string ktsd, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            //buat list menyimpan ketersediaan
            var ktsdList = new List<string>();
            //Query mengambil data
            var ktsdQuery = from d in _context.Pinjaman orderby d.IdJaminanNavigation.NamaJaminan.ToString() select d.IdJaminanNavigation.NamaJaminan.ToString();

            ktsdList.AddRange(ktsdQuery.Distinct());

            //untuk menampilkan di view
            ViewBag.ktsd = new SelectList(ktsdList);

            //panggil db context
            var menu = from m in _context.Pinjaman.Include(k => k.IdJaminanNavigation) select m;

            //untuk memilih dropdownlist ketersediaan
            if (!string.IsNullOrEmpty(ktsd))
            {
                menu = menu.Where(x => x.IdJaminanNavigation.NamaJaminan.ToString() == ktsd);
            }

            //membuat pagedList
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            //untuk sorting
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.IdCustomerNavigation.NamaCustomer);
                    break;
                case "Date":
                    menu = menu.OrderBy(s => s.TglPeminjaman);
                    break;
                case "date_desc":
                    menu = menu.OrderByDescending(s => s.TglPeminjaman);
                    break;
                default:
                    menu = menu.OrderBy(s => s.IdCustomerNavigation.NamaCustomer);
                    break;
            }


            //definisi jumlah data pada halaman
            int pageSize = 5;

            return View(await PaginatedList<Pinjaman>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Pinjamen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pinjaman = await _context.Pinjaman
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (pinjaman == null)
            {
                return NotFound();
            }

            return View(pinjaman);
        }

        // GET: Pinjamen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pinjamen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Pinjaman pinjaman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pinjaman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pinjaman);
        }

        // GET: Pinjamen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pinjaman = await _context.Pinjaman.FindAsync(id);
            if (pinjaman == null)
            {
                return NotFound();
            }
            return View(pinjaman);
        }

        // POST: Pinjamen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPeminjaman,TglPeminjaman,IdKendaraan,IdCustomer,IdJaminan,Biaya")] Pinjaman pinjaman)
        {
            if (id != pinjaman.IdPeminjaman)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pinjaman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PinjamanExists(pinjaman.IdPeminjaman))
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
            return View(pinjaman);
        }

        // GET: Pinjamen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pinjaman = await _context.Pinjaman
                .FirstOrDefaultAsync(m => m.IdPeminjaman == id);
            if (pinjaman == null)
            {
                return NotFound();
            }

            return View(pinjaman);
        }

        // POST: Pinjamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pinjaman = await _context.Pinjaman.FindAsync(id);
            _context.Pinjaman.Remove(pinjaman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PinjamanExists(int id)
        {
            return _context.Pinjaman.Any(e => e.IdPeminjaman == id);
        }
    }
}

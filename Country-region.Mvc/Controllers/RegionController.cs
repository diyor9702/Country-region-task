using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Yangi.DATA.Context;
using Yangi.DATA.Models;

namespace Country_region.Mvc.Controllers
{
    public class RegionController : Controller
    {
        private readonly AppDbContext _context;
        public RegionController(AppDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public async Task<IActionResult> Index()
        {
            var region=await _context.Regionses.Include("Country").ToListAsync();
            return View(region);
        }
        public async Task<IActionResult> Create()
        {
            var countries = await _context.Countrieses.ToListAsync();
            ViewBag.CountryId = countries.Select(p => new CountryIdName
            {
                Id = p.Id,
                Name = p.Name
            });
            return View();
        }
        public async Task<IActionResult> Add(Region region)
        {
            await _context.Regionses.AddAsync(region);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var region = await _context.Regionses.FirstOrDefaultAsync(p => p.Id == Id);
            var countries = await _context.Countrieses.ToListAsync();
            ViewBag.CountryId = countries.Select(p => new CountryIdName
            {
                Id = p.Id,
                Name = p.Name
            });
            if (region == null)
                return Redirect("Error");
            return View(region);
        }
        public async Task<IActionResult> Update(Region region)
        {
            _context.Regionses.Attach(region);
            _context.Entry(region).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(long Id)
        {
            var region = await _context.Regionses.Include(p=>p.Country).FirstOrDefaultAsync(p => p.Id == Id);
            if (region == null)
                return Redirect("Error");
            return View(region);
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var region = await _context.Regionses.Include("Country").FirstOrDefaultAsync(p => p.Id == Id);
            if (region == null)
                return Redirect("Error");
            return View(region);
        }

        public async Task<IActionResult> Remove(Region region)
        {
            _context.Regionses.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }

    public class CountryIdName
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}

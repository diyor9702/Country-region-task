using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paket.Core.Common;
using System;
using X.PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yangi.DATA.Context;
using Yangi.DATA.Models;
using Newtonsoft.Json;
using Country_region.Mvc.Models;

namespace Country_region.Mvc.Controllers
{
    public class CountryController : Controller 
    {
        private readonly AppDbContext _context;
        public CountryController(AppDbContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }
        public async Task<IActionResult> Index(int? min, int? max, int? number, string sortOrder, int? indexsort, int numberpage)
        {
            #region nimadir
            //if (max >= min && number > 0)
            //{
            //    var query = _context.Countrieses.AsQueryable().Where(p => p.ShortName.Length == number && p.Population >= min && p.Population <= max);
            //    return View(query);
            //}
            //else
            //{
            //    if (min >= max)
            //    {
            //        if (number == 0)
            //        {
            //            var countries = await _context.Countrieses.ToListAsync();
            //            return View(countries);
            //        }
            //        else
            //        {
            //            var quer = _context.Countrieses.AsQueryable().Where(p => p.ShortName.Length == number);                        
            //             return View(quer);
            //        }
            //    }
            //    else
            //    {
            //        var querys = _context.Countrieses.AsQueryable().Where(p => p.Population >= min && p.Population <= max || p.Population <= min && p.Population >= max);
            //        return View(querys);

            //    }
            //}
            #endregion

            ViewBag.Index = indexsort;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortParm = sortOrder == "Name" ? "Reversename" : "Name";

            var countries = await _context.Countrieses.ToListAsync();

            if (number != null || (min != null && max != null))
            {
                numberpage = 1;
            }


            ViewBag.CurrentFilter = number;
            ViewBag.CurrentFiltermax = max;
            ViewBag.CurrentFiltermin = min;
            int? mx = max < min ? min : max;
            int? mn = max == mx ? min : max;
            int count = _context.Countrieses.Count();
            int pages = count;
            ViewBag.Pages = (count - 1) / 10 + 1;
            switch (indexsort)
            {
                case 1:
                    countries = (sortOrder == "Name" ?
                        ((await _context.Countrieses.ToListAsync())
                        .Where(p => (number == null || p.ShortName.Length == number) && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                       .OrderBy(p => p.Name) :
                       ((await _context.Countrieses.ToListAsync())
                       .Where(p => (number == null || p.ShortName.Length == number) && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                       .OrderByDescending(p => p.Name)).Skip((numberpage - 1) * 10).Take(10).ToList();
                    break;
                case 2:
                    countries = (sortOrder == "Name" ?
                        ((await _context.Countrieses.ToListAsync())
                        .Where(p => (number == null || p.ShortName.Length == number) && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                       .OrderBy(p => p.Population) :
                       ((await _context.Countrieses.ToListAsync())
                       .Where(p => (number == null || p.ShortName.Length == number) && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                       .OrderByDescending(p => p.Population)).Skip((numberpage - 1) * 10).Take(10).ToList();
                    break;
                default:
                        countries = countries.Where(p => (number == null || p.ShortName.Length == number)
                        && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Skip((numberpage - 1) * 10).Take(10).ToList();
                        break;
            }
            return View(countries);

        }
        public IEnumerable<Country> GetallS(int? min, int? max, int? number, string sortOrder, int? indexsort, int numberpage)
        {
            var countries = _context.Countrieses.ToList();
            int? mx = max < min ? min : max;
            int? mn = max == mx ? min : max;
                    switch (indexsort)
                    {
                        case 1:
                                countries = (sortOrder == "Name" ?
                                    (( _context.Countrieses.ToList())
                                    .Where(p => (number == null || p.ShortName.Length == number) &&
                                    (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                                   .OrderBy(p => p.Name) :
                                   (( _context.Countrieses.ToList())
                                   .Where(p => (number == null || p.ShortName.Length == number) &&
                                   (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                                   .OrderByDescending(p => p.Name)).Skip((numberpage - 1) * 10).Take(10).ToList();
                         break;
                        case 2:
                                countries = (sortOrder == "Name" ?
                                    (( _context.Countrieses.ToList())
                                    .Where(p => (number == null || p.ShortName.Length == number) &&
                                    (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                                   .OrderBy(p => p.Population) :
                                   (( _context.Countrieses.ToList())
                                   .Where(p => (number == null || p.ShortName.Length == number) &&
                                   (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).ToList())
                                   .OrderByDescending(p => p.Population)).Skip((numberpage - 1) * 10).Take(10).ToList();
                         break;
                        default:
                                    countries = countries.Where(p => (number == null || p.ShortName.Length == number)
                                    && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Skip((numberpage - 1) * 10).Take(10).ToList();
                         break;
                    }

            return countries;

        }
        public int Getacount(int? min, int? max, int? number, string sortOrder, int? indexsort)
        {
            int num = 10;
            int? mx = max < min ? min : max;
            int? mn = max == mx ? min : max;
            switch (indexsort)
            {
                case 1:
                    num = (sortOrder == "Name" ?
                        ((_context.Countrieses.ToList())
                        .Where(p => (number == null || p.ShortName.Length == number) &&
                        (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Count()) :
                       ((_context.Countrieses.ToList())
                       .Where(p => (number == null || p.ShortName.Length == number) &&
                       (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Count()));
                       
                    break;
                case 2:
                    num = (sortOrder == "Name" ?
                        ((_context.Countrieses.ToList())
                        .Where(p => (number == null || p.ShortName.Length == number) &&
                        (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Count()):
                       ((_context.Countrieses.ToList())
                       .Where(p => (number == null || p.ShortName.Length == number) &&
                       (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Count()));
                       
                    break;
                default:
                    num = ((_context.Countrieses.ToList()).Where(p => (number == null || p.ShortName.Length == number)
                    && (mn == null || mx == null || (p.Population >= mn && p.Population <= mx))).Count());
                    break;
            }

            return num;

        }
        public IActionResult HomeVue( int? min,  int? max,  int? number, string sortOrder, int? indexsort, int numberpage)
        {
                int count = _context.Countrieses.Count();
                int pages = (count - 1) / 10 + 1;

                IList<int> allpages = new List<int>();
                for (int i = 1; i <= pages; i++)
                    allpages.Add(i);
                ViewBag.Pages = allpages;

                return View(GetallS( min, max, number,  sortOrder, indexsort, numberpage));
        }

        [HttpGet]
        public IActionResult getcountryvue(int? min, int? max, int? number, string sortOrder, int? indexsort, int numberpage)
        {
                var result = new CountryVueModel()
                {
                    countries = GetallS(min, max, number, sortOrder, indexsort, numberpage)
                };
                int count = Getacount(min, max, number, sortOrder, indexsort);
                int pages = (count - 1) / 10 + 1;
                List<int> allpages = new List<int>();
                for (int i = 1; i <= pages; i++)
                    allpages.Add(i);
                result.Pagesnumber = allpages;                
                return Json(result);
        }
        public IActionResult Create()
        {
            return View();
        }       
        public async Task<IActionResult> Add(Country country)
        {

            await _context.Countrieses.AddAsync(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(long Id)
        {
            var country = await _context.Countrieses.FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Update(Country country)
        {
            _context.Countrieses.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(long Id)
        {
            var country = await _context.Countrieses.Include(p => p.Regions).FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Delete(long Id)
        {
            var country = await _context.Countrieses.Include(p => p.Regions).FirstOrDefaultAsync(p => p.Id == Id);
            if (country == null)
                return Redirect("Error");
            return View(country);
        }
        public async Task<IActionResult> Remove(Country country)
        {
            _context.Countrieses.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

      

    }
}






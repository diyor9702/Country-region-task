using System.Collections.Generic;
using Yangi.DATA.Models;

namespace Country_region.Mvc.Models
{
    public class CountryVueModel
    {
        public IEnumerable<Country>  countries { get; set; }
        public List<int> Pagesnumber { get; set; }
    }
}

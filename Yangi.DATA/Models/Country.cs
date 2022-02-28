using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangi.DATA.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public int Population { get; set; }
        public virtual List<Region> Regions { get; set; }
    }
}

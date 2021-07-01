using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TorebaCrawlerCore;
using TorebaCrawlerWeb.Data;

namespace TorebaCrawlerWeb.Pages.Takoyaki
{
    public class IndexModel : PageModel
    {
        private readonly TorebaCrawlerWeb.Data.TorebaCrawlerWebContext _context;

        public IndexModel(TorebaCrawlerWeb.Data.TorebaCrawlerWebContext context)
        {
            _context = context;
        }

        public IList<Machine> Machine { get;set; }

        public async Task OnGetAsync()
        {

            var takoyakis = from m in _context.Machine
                          select m;
            
            takoyakis = takoyakis.Where(s => s.MachineType.Equals(MachineType.Takoyaki));
            takoyakis = takoyakis.Where(s => s.CostType.Equals(CostType.All));

            Machine = await takoyakis.ToListAsync();
        }
    }
}

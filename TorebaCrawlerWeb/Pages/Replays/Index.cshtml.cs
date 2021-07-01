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

namespace TorebaCrawlerWeb.Pages.Replays
{
    public class IndexModel : PageModel
    {
        private readonly TorebaCrawlerWeb.Data.TorebaCrawlerWebContext _context;

        public IndexModel(TorebaCrawlerWeb.Data.TorebaCrawlerWebContext context)
        {
            _context = context;
        }

        public IList<Replay> Replay { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {

            var replays = from m in _context.Replay
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                replays = replays.Where(s => s.MachineID.Equals(Int32.Parse(SearchString)));
            }

            //Replay = await _context.Replay.ToListAsync();
            Replay = await replays.ToListAsync();
        }
    }
}


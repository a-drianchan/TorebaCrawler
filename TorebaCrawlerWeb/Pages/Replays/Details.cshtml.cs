using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorebaCrawlerCore;
using TorebaCrawlerWeb.Data;

namespace TorebaCrawlerWeb.Pages.Replays
{
    public class DetailsModel : PageModel
    {
        private readonly TorebaCrawlerWeb.Data.TorebaCrawlerWebContext _context;

        public DetailsModel(TorebaCrawlerWeb.Data.TorebaCrawlerWebContext context)
        {
            _context = context;
        }

        public Replay Replay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Replay = await _context.Replay.FirstOrDefaultAsync(m => m.ReplayID == id);

            if (Replay == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

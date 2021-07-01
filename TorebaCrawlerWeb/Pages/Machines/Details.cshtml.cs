﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TorebaCrawlerCore;
using TorebaCrawlerWeb.Data;

namespace TorebaCrawlerWeb.Pages.Machines
{
    public class DetailsModel : PageModel
    {
        private readonly TorebaCrawlerWeb.Data.TorebaCrawlerWebContext _context;

        public DetailsModel(TorebaCrawlerWeb.Data.TorebaCrawlerWebContext context)
        {
            _context = context;
        }

        public Machine Machine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Machine = await _context.Machine.FirstOrDefaultAsync(m => m.MachineID == id);

            if (Machine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

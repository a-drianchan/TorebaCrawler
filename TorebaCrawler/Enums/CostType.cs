using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TorebaCrawlerCore
{
    public enum CostType
    {
        [Display(Name = "All")]
        All,
        [Display(Name = "TP Only")]
        TpOnly,
        [Display(Name = "No Tutorial Ticket Allowed")]
        NoTutorial
    }
}

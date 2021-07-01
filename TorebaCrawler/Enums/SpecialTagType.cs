using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TorebaCrawlerCore.Enums
{
    public enum SpecialTagType
    {
        [Display(Name = "Sale")]
        Sale,
        [Display(Name = "Super Chance")]
        SuperChance,
        [Display(Name = "Special Event")]
        SpecialEvent,
        [Display(Name = "Free Ticket")]
        PrizeGrab,
        [Display(Name = "None")]
        None,
        [Display(Name = "Limited")]
        Limited,
        [Display(Name = "New")]
        New,
        [Display(Name = "Recommended")]
        Recommended
    }
}

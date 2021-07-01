using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TorebaCrawlerCore.Enums
{
    public enum StatusType
    {
        [Display(Name = "Open")]
        Open,
        [Display(Name = "Maintenance")]
        Maintenance,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Restocking")]
        Restocking,
        [Display(Name = "In Use")]
        InUse,
        [Display(Name = "Sold Out")]
        SoldOut,
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TorebaCrawlerCore.Enums;

namespace TorebaCrawlerCore
{
    public class Machine
    {
        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Prize ID")]
        public int PrizeID { get; set; }
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }
        public int Cost { get; set; }

        [Display(Name = "Play Style")]
        public MachineType MachineType { get; set; }
        [Display(Name = "Accepted Payment")]
        public CostType CostType { get; set; }
        [Display(Name = "Status")]
        public StatusType StatusType { get; set; }
        [Display(Name = "Special Tag")]
        public SpecialTagType SpecialTagType { get; set; }

    }
}

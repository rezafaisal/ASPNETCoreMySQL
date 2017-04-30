using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class ContohModel{
        [Display(Name ="Contoh Text")]
        public String ContohText{set; get;}

        [Display(Name ="Contoh Date Time")]
        public DateTime ContohDateTime{set; get;}

        [Display(Name ="Contoh Number")]
        public Double ContohNumber{set; get;}

        [Display(Name ="Contoh Boolean")]
        [Required]
        public Boolean ContohBoolean{set; get;}
    }
}
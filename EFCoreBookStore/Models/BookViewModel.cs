using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class BookViewModel{
        [Display(Name ="ISBN")]
        public int ISBN {set; get;}
        
        [Display(Name ="Category")]
        public String CategoryName {set; get;}
        
        [Display(Name ="Title")]
        public String Title {set; get;}
        
        [Display(Name ="Photo")]
        public String Photo {set; get;}
        
        [Display(Name ="Publish Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime PublishDate {set; get;}
        
        [Display(Name ="Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Price {set; get;}
        
        [Display(Name ="Quantity")]
        public int Quantity {set; get;}
        
        [Display(Name ="List Author Names")]
        public string AuthorNames {set; get;}
    }
}
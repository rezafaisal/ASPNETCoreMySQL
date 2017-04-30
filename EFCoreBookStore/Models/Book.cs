using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class Book{
        [Display(Name ="ISBN")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} tidak boleh lebih {1} dan tidak boleh kurang {2} karakter.")]
        public int ISBN {set; get;}
        
        [Display(Name ="Category ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public int CategoryID {set; get;}
        
        [Display(Name ="Title")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String Title {set; get;}
        
        [Display(Name ="Photo")]
        public String Photo {set; get;}
        
        [Display(Name ="Publish Date")]
        public DateTime PublishDate {set; get;}
        
        [Display(Name ="Price")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public double Price {set; get;}
        
        [Display(Name ="Quantity")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} harus angka")]
        public int Quantity {set; get;}
    }
}
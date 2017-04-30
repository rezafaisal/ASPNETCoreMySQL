using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class Author{
        [Display(Name ="ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int AuthorID {set; get;}
        
        [Display(Name ="Author's Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String Name {set; get;}
        
        [Display(Name ="Email")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "{0} tidak valid.")]
        public String Email {set; get;}
    }
}
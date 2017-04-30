using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class User{
        [Display(Name ="Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String UserName {set; get;}

        [Display(Name ="Role")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int RoleID {set; get;}
        
        [Display(Name ="Email")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String Email {set; get;}

        [Display(Name ="Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String Password {set; get;}

        [Display(Name ="Fullname")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String Fullname {set; get;}
    }
}
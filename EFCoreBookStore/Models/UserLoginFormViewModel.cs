using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class UserLoginFormViewModel{
        [Display(Name ="Username")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public String UserName {set; get;}

        [Display(Name ="Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.Password)]
        public String Password {set; get;}
    }
}
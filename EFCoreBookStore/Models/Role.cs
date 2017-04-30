using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreBookStore.Models
{
    public partial class Role{
        [Display(Name ="Role ID")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public int RoleID {set; get;}
        
        [Display(Name ="Role Name")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(256, ErrorMessage = "{0} tidak boleh lebih {1} karakter.")]
        public String RoleName {set; get;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrganicFoodMVC.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nhập tên nhà bán hàng")]
        [Display(Name="Tên nhà bán hàng")]
        public string Name { get; set; }

        [Display(Name = "Số nhà, đường, ấp")]
        public string StreetAddress { get; set; }

        [Display(Name = "Tên xã")]
        public string Village { get; set; }

        [Display(Name = "Tên huyện")]
        public string District { get; set; }

        [Display(Name = "Tên thành phố")]
        public string City { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Display(Name = "Là nhà bán hàng")]
        public bool IsAuthorizedCompany { get; set; }
       
    }
}

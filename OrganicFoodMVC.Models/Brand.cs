using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrganicFoodMVC.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Tên Thương Hiệu")]
        [Required(ErrorMessage ="Điền tên thương hiệu")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name="Mô tả")]
        [MaxLength(5000)]
        public string Discription { get; set; }

        [MaxLength(255)]
        public string Logo { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrganicFoodMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Tên rau quả")]
        [Required(ErrorMessage = "Không được để tên trống")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name="Thông tin chi tiết")]
        [MaxLength(5000)]
        public string Discription { get; set; }

        [Display(Name = "Số Lượng")]
        [Required(ErrorMessage = "Nhập số Kg")]
        [Range(1, 10000, ErrorMessage ="Nhập số kg trong khoảng 1 đến 10000")]
        public int Quantity { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Nhập giá sản phẩm")]
        [Range(1, 100000000, ErrorMessage = "Nhập giá trong khoảng 1 đến 100 triệu")]
        public long Price { get; set; }

        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Chọn loại sản phẩm")]
        [Display(Name = "Loại sản phẩm")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Chọn thương hiệu")]
        [Display(Name = "Thương hiệu")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [Required(ErrorMessage = "Chọn loại đơn vị tính")]
        [Display(Name = "Đơn vị tính")]
        public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
    }
}

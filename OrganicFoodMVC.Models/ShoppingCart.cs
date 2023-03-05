using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrganicFoodMVC.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required(ErrorMessage ="Hãy chọn số lượng")]
        [Range(1, 1000, ErrorMessage ="Chọn số lượng lớn hơn 0")]
        public int Count { get; set; }
        [NotMapped]
        public long Price { get; set; }
    }
}

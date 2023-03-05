using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrganicFoodMVC.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime ShippingDate { get; set; }
        [Required]
        public long OrderTotal { get; set; }
        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string TransactionId { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Nhập họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nhập số nhà, tên đường")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Nhập tên xã")]
        public string Village { get; set; }
        [Required(ErrorMessage = "Nhập tên quận (huyện)")]
        public string District { get; set; }
        [Required(ErrorMessage = "Nhập tên tỉnh (thành phố)")]
        public string City { get; set; }
    }
}

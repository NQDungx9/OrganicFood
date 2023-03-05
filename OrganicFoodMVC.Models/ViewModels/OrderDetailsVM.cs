using System;
using System.Collections.Generic;
using System.Text;

namespace OrganicFoodMVC.Models.ViewModels
{
    public class OrderDetailsVM
    {
        public OrderHeader orderHeader { get; set; }
        public IEnumerable<OrderDetails> orderDetails { get; set; }
    }
}

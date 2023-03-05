using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;
using OrganicFoodMVC.Models.ViewModels;
using OrganicFoodMVC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrganicFoodMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // anh xa du lieu
        [BindProperty]
        public OrderDetailsVM orderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = SD.Role_User_Indi + "," + SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Index(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        
            IEnumerable<OrderDetails> orderDetails = _unitOfWork.OrderDetails.GetAll(
                                                        u => u.OrderHeader.ApplicationUserId == claim.Value,
                                                        includeProperties: "OrderHeader,Product");
         

            // status order
            switch (status)
            {
                case "pending":
                    orderDetails = orderDetails.Where(o => o.OrderHeader.PaymentStatus == SD.StatusPending);
                    break;
                case "inprocess":
                    orderDetails = orderDetails.Where(o => o.OrderHeader.OrderStatus == SD.StatusApproved ||
                                                            o.OrderHeader.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    orderDetails = orderDetails.Where(o => o.OrderHeader.OrderStatus == SD.StatusShipped);
                    break;
                case "rejected":
                    orderDetails = orderDetails.Where(o => o.OrderHeader.OrderStatus == SD.StatusCancelled ||
                                                            o.OrderHeader.OrderStatus == SD.StatusRefunded ||
                                                            o.OrderHeader.OrderStatus == SD.PaymentStatusRejected);
                    break;
                default:
                    break;
            }


            return View(orderDetails);
        }
    }
}

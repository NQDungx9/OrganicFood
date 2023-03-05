using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OrganicFoodMVC.Utility
{
    public static class SD
    {

        // role
        public const string Role_User_Indi = "Individual Customer";
        public const string Role_User_Comp = "Company Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        //session
        public const string ssShoppingCart = "Shopping Cart Session";

        //
        public const string StatusPending = "Chờ xử lý";
        public const string StatusApproved = "Đã tiếp nhận";
        public const string StatusInProcess = "Đang giao";
        public const string StatusShipped = "Giao thành công";
        public const string StatusCancelled = "Hủy đơn";
        public const string StatusRefunded = "Hoàn lại";

        public const string PaymentStatusPending = "Chờ xử lý";
        public const string PaymentStatusApproved = "Đã thanh toán";
        public const string PaymentStatusDelayedPayment = "Thanh toán khi nhận hàng";
        public const string PaymentStatusRejected = "Hoàn tiền";

        // remove hint
        public static string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;

namespace OrganicFoodMVC.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Họ tên")]
            public string Name { get; set; }

            [Display(Name = "Số nhà, tên đường")]
            public string StreetAddress { get; set; }

            [Display(Name = "Xã (Thôn)")]
            public string Village { get; set; }

            [Display(Name = "Quận (Huyện)")]
            public string District { get; set; }

            [Display(Name = "Tỉnh (Thành phố)")]
            public string City { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var name = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == userName).Name;
            var streetAddress = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == userName).StreetAddress;
            var village = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == userName).Village;
            var district = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == userName).District;
            var city = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == userName).City;



            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Email = email,
                Name = name,
                StreetAddress = streetAddress,
                Village = village,
                District = district,
                City = city
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user1 = await _userManager.GetUserAsync(User);
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(i => i.UserName == user1.UserName);
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            // change user
            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set email number.";
                    return RedirectToPage();
                }
            }

            // change user
            user.Name = Input.Name;
            user.StreetAddress = Input.StreetAddress;
            user.Village = Input.Village;
            user.District = Input.District;
            user.City = Input.City;
            
            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Save();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Cập nhập thông tin thành công";
            return RedirectToPage();
        }
    }
}

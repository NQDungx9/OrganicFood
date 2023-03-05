using Microsoft.AspNetCore.Mvc;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicFoodMVC.Components
{
    public class Search : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public Search(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
    }
}

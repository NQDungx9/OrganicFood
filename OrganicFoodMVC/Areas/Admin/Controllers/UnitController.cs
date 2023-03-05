using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;
using OrganicFoodMVC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganicFoodMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        // show insert and update
        public IActionResult Upsert(int? id)
        {
            Unit unit = new Unit();
            if (id == null)
            {
                // this is for create
                return View(unit);
            }

            //this is for edit
            unit = _unitOfWork.Unit.Get(id.GetValueOrDefault());
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // insert and update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Unit unit)
        {
            if (ModelState.IsValid)
            {
                if (unit.Id == 0)
                {
                    _unitOfWork.Unit.Add(unit); // insert
                }
                else
                {
                    _unitOfWork.Unit.Update(unit); // update
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        #region API CALLS

        // api get unit
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Unit.GetAll();
            return Json(new { data = allObj });
        }

        //api delete unit
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Unit.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Xóa thất bại!" });
            }
            _unitOfWork.Unit.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        #endregion
    }
}

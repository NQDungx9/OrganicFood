using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;
using OrganicFoodMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using OrganicFoodMVC.Utility;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using HtmlAgilityPack;

namespace OrganicFoodMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // info path to save image
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }

        // insert and update product
        public IActionResult Upsert(int? id)
        {
            IEnumerable<Category> CatList = _unitOfWork.Category.GetAll();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = CatList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandList = _unitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                UnitList = _unitOfWork.Unit.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null)
            {
                //this is for create
                return View(productVM);
            }
            //this is for edit
            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);

        }

        // info path to save image
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (productVM.Product.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image

                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    productVM.Product.ImageUrl = @"\images\products\" + fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (productVM.Product.Id != 0)
                    {
                        Product objFromDb = _unitOfWork.Product.Get(productVM.Product.Id);
                        productVM.Product.ImageUrl = objFromDb.ImageUrl;
                    }
                }

                // insert
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);

                }
                else // update
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else // assign cateList and BrandList if modelState.IsVaild false
            {
                IEnumerable<Category> CatList = _unitOfWork.Category.GetAll();
                productVM.CategoryList = CatList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                productVM.BrandList = _unitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                productVM.UnitList = _unitOfWork.Unit.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                if (productVM.Product.Id != 0)
                {
                    productVM.Product = _unitOfWork.Product.Get(productVM.Product.Id);
                }
            }
            return View(productVM);
        }

        public ActionResult ExportToExcel()
        {
            var doctors = _unitOfWork.Product.GetAll(includeProperties: "Category,Brand,Unit");

            byte[] fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("DoctorsInfo");
            Sheet.Cells["B1"].Value = "Mã Sản Phẩm";
            Sheet.Cells["C1"].Value = "Tên Sản Phẩm";
            //Sheet.Cells["C1"].Value = "Mô tả ";
            Sheet.Cells["D1"].Value = "Số lượng";
            Sheet.Cells["E1"].Value = "Giá";
            Sheet.Cells["F1"].Value = "Link ảnh";
            Sheet.Cells["G1"].Value = "Loại sản phẩm";
            Sheet.Cells["H1"].Value = "Thương hiệu";
            Sheet.Cells["I1"].Value = "Đơn vị";


            int row = 2;
            foreach (var item in doctors)
            {
                //var doc = new HtmlDocument();
                //doc.LoadHtml(item.Discription);
                //var innertext = doc.DocumentNode.InnerText; 
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Id;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Name;
                //Sheet.Cells[string.Format("C{0}", row)].Value = innertext;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Quantity;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Price;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.ImageUrl;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Category.Name;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Brand.Name;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Unit.Name;

                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            fileContents = Ep.GetAsByteArray();

            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }

            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "SanPhamcuaOrganicFood.xlsx"
            );
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }


        #region API CALLS

        // API get product
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll(includeProperties: "Category,Brand,Unit");
            return Json(new { data = allObj });
        }

        // API delete product
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Xóa thất bại!" });
            }

            // delete image
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Xóa thành công!" });
        }

        #endregion
    }
}

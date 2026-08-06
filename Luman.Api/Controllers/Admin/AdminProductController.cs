using Luman.Busines.DTOs.ProductDTO;
using Luman.Busines.Services.ProductService;
using Luman.Busines.Utility;
using Luman.DataLayer.EntityModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Luman.Api.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/Admin")]
    [ApiController]
    [ApiVersion("2.0")]
    [Authorize]
    [PermissionChecker(1,3)]
    public class AdminProductController : ControllerBase
    {
        private static readonly string[] ExtensionFile = { ".jpg", ".png", ".jpeg",".gif",".webp" };
        private readonly IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Category

        [HttpPost("AddCategories")]
        public IActionResult AddCategories([FromBody] CreateCategoriesDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            Category cat = new()
            {
                Name = model.Name,
            };

            _productService.AddCategory(cat);
            return Ok();
        }

        [HttpGet("GetAllCategory")]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAllCategories());
        }


        #endregion


        #region ProductAdmin


        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryid = _productService.GetGroupIdByName(model.Categoryname);

            //ذخیره عکس 
           
            if (model.Imagename == null || model.Imagename.Length == 0)
            {
                return BadRequest("فایلی وارد نشده است");
            }
            if (!ExtensionFile.Contains(Path.GetExtension(model.Imagename.FileName).ToLower()))
            {
                return BadRequest("فایل وارد شده اشتباه است.");
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" , "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Imagename.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
               await model.Imagename.CopyToAsync(stream);
            }


            // ایجاد مدل محصول
            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                imagename = fileName,

            };

            if (categoryid == null)
            {
                return NotFound();
            }
            if (_productService.CreateProduct(product))
            {
                _productService.addgroup(product, categoryid);
            }

            return Ok(new { Message = "محصول با موفقیت افزوده شد", product.ProductId, product.Name });
        }


        [HttpGet("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            return Ok(_productService.GetAllProductForAdmin());
        }



        [HttpPatch("EditeProduct/{proid:int}")]
        public async Task<IActionResult> EditeProduct([FromForm] EditeProduct model, int proid)
        {
            if (!ModelState.IsValid) return BadRequest(model);




            var product = _productService.GetproductById(proid);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");



            if (model.Imagename != null)
            {
                if (!ExtensionFile.Contains(Path.GetExtension(model.Imagename.FileName).ToLower()))
                {
                    return BadRequest("فایل وارد شده اشتباه است.");
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Imagename.FileName);

                if (!string.IsNullOrEmpty(product.imagename))
                {
                    // مسیر فیزیکی فایل در سرور
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.imagename);

                    // حذف فایل در صورت وجود
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   await model.Imagename.CopyToAsync(stream);
                }
                product.imagename = fileName;
               

            }

            product.Name = model.Name;
            product.Price = model.Price;

            _productService.EditeProduct(product);
            return Ok();

        }


        [HttpDelete("DeleteProduct/{productId:int}")]
        public IActionResult DeleteProduct(int productId)
        {
            _productService.DeleteProduct(productId);
            return Ok(new { Message = "حذف با موفقیت انجام شد", productId });
        }


        #endregion



    }
}

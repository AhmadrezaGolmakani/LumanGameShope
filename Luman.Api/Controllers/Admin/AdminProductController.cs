using Luman.Busines.DTOs.ProductDTO;
using Luman.Busines.Services.ProductService;
using Luman.Busines.Utility;
using Luman.DataLayer.EntityModel.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luman.Api.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/Admin")]
    [ApiController]
    [ApiVersion("2.0")]
    
    public class AdminProductController : ControllerBase
    {
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
        public IActionResult AddProduct([FromForm] CreateProductDTO model)
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
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:Users/ASUS/Desktop/Images" , "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Imagename.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.Imagename.CopyToAsync(stream);
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
        public IActionResult EditeProduct([FromForm] EditeProduct model, int proid)
        {
            if (!ModelState.IsValid) return BadRequest(model);




            var product = _productService.GetproductById(proid);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Imagename.FileName);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "\"C:Users/ASUS/Desktop/Images\"", "uploads");



            if (model.Imagename != null)
            {

                if (!string.IsNullOrEmpty(product.imagename))
                {
                    // مسیر فیزیکی فایل در سرور
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "C:Users/ASUS/Desktop/Images/uploads", product.imagename);

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
                    model.Imagename.CopyToAsync(stream);
                }

            }

            product.imagename = fileName;
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

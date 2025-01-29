using Luman.Busines.DTOs.OrderDTO;
using Luman.Busines.Services.OrderService;
using Luman.Busines.Services.ProductService;
using Luman.Busines.Utility;
using Luman.DataLayer.EntityModel.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Luman.Api.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/Admin")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IProductService _productService;

        public DiscountController(IOrderServices orderServices, IProductService productService)
        {
            _orderServices = orderServices;
            _productService = productService;
        }

        #region Discount

        /// <summary>
        /// افزودن  کد تخفیف 
        /// </summary>
        /// <param name="model">مدل ورودی (تاریخ ها به شمسی وارد شود)</param>
        /// <param name="Productname">نام محصول اگر کد تخفیف برای محصولی خاص است</param>
        /// <returns></returns>
        [HttpPost("AddDiscount")]
        public IActionResult AddDiscount([FromBody] CreateDiscountDTO model, string? Productname)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            int? productid = null;
            if (!string.IsNullOrWhiteSpace(Productname))
            {
                productid = _productService.GetProductidByname(Productname);

                if (productid == null)
                {
                    return BadRequest("محصولی با این نام یافت نشد.");
                }
            }

            try
            {
                var persianCalendar = new PersianCalendar();

                DateTime? startDate = null;
                DateTime? endDate = null;

                if (!string.IsNullOrWhiteSpace(model.StartDate))
                {
                    string[] startParts = model.StartDate.Split('/');
                    startDate = new DateTime(int.Parse(startParts[0]), int.Parse(startParts[1]), int.Parse(startParts[2]), persianCalendar);
                }

                if (!string.IsNullOrWhiteSpace(model.EndDate))
                {
                    string[] endParts = model.EndDate.Split('/');
                    endDate = new DateTime(int.Parse(endParts[0]), int.Parse(endParts[1]), int.Parse(endParts[2]), persianCalendar);
                }

                Discount discount = new()
                {
                    DiscountCode = model.DiscountCode,
                    DiscountPercent = model.DiscountPercent,
                    UsableCount = model.UsableCount,
                    StartDate = startDate,
                    EndDate = endDate,
                    ProductId = productid
                };

                if (_orderServices.IsExistDiscount(model.DiscountCode))
                {
                    return StatusCode(302, new { Message = "کد وارد شده موجود می‌باشد", Code = model.DiscountCode, Id = model.DiscountId });
                }

                _orderServices.AddDiscount(discount);

                return Ok(new { Message = "کد تخفیف با موفقیت افزوده شد", Code = model.DiscountCode, ID = discount.DiscountId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "خطا در افزودن کد تخفیف", Error = ex.Message });
            }
        }

        /// <summary>
        /// دریافت همه کد تخفیف ها
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllDiscount")]
        public IActionResult GetAllDiscount()
        {
            var discounts = _orderServices.GetAllDiscounts().Select(discount => new
            {
                discount.DiscountId,
                discount.DiscountCode,
                discount.DiscountPercent,
                discount.UsableCount,
                StartDate = discount.StartDate.ToPersainDate(),
                EndDate = discount.EndDate.ToPersainDate(),
                discount.ProductId
            });

            return Ok(discounts);
        }



        /// <summary>
        /// ویرایش کد تخفیف
        /// </summary>
        /// <param name="discountid">ایدی کد تخفیف</param>
        /// <param name="stDate">تاریخ شروع(به شمسی وارد شود) </param>
        /// <param name="edDate">تاریخ پایان(به شمسی وارد شود)</param>
        /// <returns></returns>
        [HttpPatch("UpdateDiscount/{discountid:int}")]
        public IActionResult UpdateDiscount(int discountid, string stDate = "", string edDate = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var discount = _orderServices.GetDiscountById(discountid);

            if (!string.IsNullOrWhiteSpace(stDate))
            {
                string[] std = stDate.Split('/');
                discount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }

            if (!string.IsNullOrWhiteSpace(edDate))
            {
                string[] edd = edDate.Split('/');
                discount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
            }

            _orderServices.UpdateDiscount(discount);

            return Ok(new { Message = "با موفقیت ویرایش شد", Id = discount.DiscountId, Code = discount.DiscountCode });
        }
    } 

    #endregion
}


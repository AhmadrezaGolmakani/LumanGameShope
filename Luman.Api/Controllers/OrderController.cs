using Luman.Busines.DTOs.OrderDTO;
using Luman.Busines.Services.OrderService;
using Luman.Busines.Services.ProductService;
using Luman.Busines.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luman.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Order")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderservice;
        private readonly IProductService _productService;
        private readonly IUserServices _userService;


        public OrderController(IOrderServices orderservice, IProductService productService, IUserServices userService)
        {
            _orderservice = orderservice;
            _productService = productService;
            _userService = userService;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(int proid)
        {
            var productid = _productService.GetproductById(proid);


            _orderservice.AddOrder(User.Identity.Name, proid);

            return Ok(new { Message = "محصول با موفقیت به سبد خرید افزوده شد", UserName = User.Identity.Name, ProductId = productid });


        }

        [HttpGet("GetOrderForUserPanel/{id:int}")]
        public IActionResult GetOrderForUserPanel(int id)
        {
            var order = _orderservice.ShowOrderForUserPanel(User.Identity.Name, id);

            if (order == null)
            {
                return NotFound(new { Message = $"فاکتوری برای کاربر{User.Identity.Name}پیدا نشد" });
            }

            return Ok(order);
        }

        [HttpGet("AllOrder")]
        public IActionResult GetAllOrder()
        {
            return Ok(_orderservice.GetAllOrders(User.Identity.Name));
        }


        [HttpPost("usediscount")]
        public IActionResult UseDiscount([FromBody]UseDiscountDTO model)
        {
            return Ok(_orderservice.UseDiscount(model.Code , model.orderId , model.proid));
        }

       
    }
}

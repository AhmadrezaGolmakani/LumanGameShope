using Luman.Busines.DTOs.OrderDTO;
using Luman.Busines.Services.UserService;
using Luman.DataLayer.Context;
using Luman.DataLayer.EntityModel.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.OrderService
{
    public class OrderServices : IOrderServices
    {

        private readonly LumanContext _context;
        private readonly IUserServices _usersevice;

        public OrderServices(LumanContext context, IUserServices usersevice)
        {
            _context = context;
            _usersevice = usersevice;
        }

        public bool AddDiscount(Discount discount)
        {
            _context.discounts.Add(discount);
            return Save();
        }

        public int AddOrder(string username, int productid)
        {
            int userid = _usersevice.GetUserIdByUserName(username);

            Order order = _context.orders
                .FirstOrDefault(o => o.UserId == userid && !o.IsFinaly);

            var product = _context.products.Find(productid);

            if (order == null)
            {
                order = new()
                {
                    UserId = userid,
                    IsFinaly = false,
                    CreateTime = DateTime.Now,
                    OrderSum = product.Price,
                    orderDetails = new List<OrderDetails>()
                    {
                        new OrderDetails()
                        {
                            ProductId = productid,
                            Count = 1,
                            Price = product.Price
                        }
                    }

                };
                _context.orders.Add(order);
                Save();
            }
            else
            {
                OrderDetails detail = _context.orderDetails
                   .FirstOrDefault(d => d.OrderId == order.OrderId && d.ProductId == productid);
                if (detail != null)
                {
                    detail.Count += 1;
                    _context.orderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetails()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        ProductId = productid,
                        Price = product.Price,
                    };
                    _context.orderDetails.Add(detail);
                }

                Save();
                UpdatePriceOrder(order.OrderId);
            }
            return order.OrderId;
        }

        public bool FinalyOrder(string userName, int orderId)
        {
            throw new NotImplementedException();

        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.discounts.ToList();
        }

        public List<Order> GetAllOrders(string userName)
        {
            var userId = _usersevice.GetUserIdByUserName(userName);
            return _context.orders.Where(o => o.UserId == userId).ToList();
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.discounts.Find(discountId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.orders.Find(orderId);
        }

        public bool IsExistDiscount(string code)
        {
            return _context.discounts.Any(d => d.DiscountCode == code);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public Order ShowOrderForUserPanel(string userName, int orderId)
        {
            var userId = _usersevice.GetUserIdByUserName(userName);
            return _context.orders.Include(o => o.orderDetails)
                .ThenInclude(o => o.product)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public bool UpdateDiscount(Discount discount)
        {
            _context.discounts.Update(discount);
            return Save();
        }

        public void UpdateOrder(Order order)
        {
            _context.orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.orders.Find(orderId);
            order.OrderSum = _context.orderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price * d.Count);
            _context.orders.Update(order);
            _context.SaveChanges();
        }

        public DiscountUseType UseDiscount(string code, int orderId , int proid)
        {
            var discount = _context.discounts.FirstOrDefault(d => d.DiscountCode == code);
            var orderdetails = _context.orders
    .Include(o => o.orderDetails)
    .FirstOrDefault(o => o.OrderId == orderId);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
                return DiscountUseType.NotStarted;

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
                return DiscountUseType.Expired;

            // چک کردن محدودیت تعداد استفاده فقط اگر مقدار مشخص باشد و معتبر باشد
            if (discount.UsableCount.HasValue && discount.UsableCount.Value <= 0)
                return DiscountUseType.Finished;

            var order = GetOrderById(orderId);
            if (order == null)
                return DiscountUseType.OrderNotFound;

            if (order.OrderSum <= 0)
                return DiscountUseType.InvalidOrderAmount;

            if (discount.DiscountPercent < 0 || discount.DiscountPercent > 100)
                return DiscountUseType.InvalidPercent;

            // چک کردن محدودیت محصول برای نوع اول تخفیف
            if (discount.IsForSpecificProduct)
            {
                if (orderdetails == null || !order.orderDetails.Any())
                {
                    return DiscountUseType.InvalidProduct;
                }

                if (proid == null || !order.orderDetails.Any(oi => oi.ProductId == proid))
                {
                    return DiscountUseType.InvalidProduct;
                }
            }

            // محاسبه و اعمال تخفیف
            long discountAmount = (order.OrderSum * discount.DiscountPercent) / 100;
            order.OrderSum -= discountAmount;

            UpdateOrder(order);

            // کاهش تعداد دفعات استفاده در صورت محدودیت
            if (discount.UsableCount.HasValue)
            {
                discount.UsableCount -= 1;
            }

            _context.SaveChanges();

            return DiscountUseType.Success;
        }

    }
}

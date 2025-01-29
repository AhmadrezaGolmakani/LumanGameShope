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
    }
}

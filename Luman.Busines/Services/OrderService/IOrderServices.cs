using Luman.Busines.DTOs.OrderDTO;
using Luman.DataLayer.EntityModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.OrderService
{
    public interface IOrderServices
    {
        #region discount
        DiscountUseType UseDiscount(string code, int orderId , int proid);

        bool AddDiscount(Discount discount);
        List<Discount> GetAllDiscounts();
        Discount GetDiscountById(int discountId);
        bool UpdateDiscount(Discount discount);
        bool IsExistDiscount(string code);

        bool Save();
        #endregion


        #region order

        int AddOrder(string username, int productid);
        void UpdatePriceOrder(int orderId);
        Order ShowOrderForUserPanel(string userName, int orderId);
        bool FinalyOrder(string userName, int orderId);
        List<Order> GetAllOrders(string userName);
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);


        #endregion
    }
}

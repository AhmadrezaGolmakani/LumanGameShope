using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.DTOs.OrderDTO
{
    public enum DiscountUseType
    {
        NotFound,
        NotStarted,
        Expired,
        Finished,
        OrderNotFound,
        InvalidOrderAmount,
        InvalidPercent,
        Success,
        InvalidProduct
    }
}

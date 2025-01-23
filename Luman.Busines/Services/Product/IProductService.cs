using Luman.Busines.DTOs.ProductDTO;
using Luman.DataLayer.EntityModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.Product
{
    public interface IProductService
    {

        #region CategoryAdmin

        bool AddCategory(Category model);
        List<Category> GetAllCategories();

        int GetGroupIdByName(string name);

        #endregion

        #region AdminProduct
        bool CreateProduct(DataLayer.EntityModel.Product.Product  model);

        int GetProductid(string name);

        List<Luman.DataLayer.EntityModel.Product.Product> GetAllProduct();

        List<DataLayer.EntityModel.Product.Product> GetAllProductForAdmin();

        bool EditeProduct();

        bool Save();
        #endregion

        #region Product

        int GetProductId(int proId);

        public void addgroup(DataLayer.EntityModel.Product.Product product , int groupid);

        #endregion



    }
}

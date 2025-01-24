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

        DataLayer.EntityModel.Product.Product GetproductById(int proid);

        bool EditeProduct(DataLayer.EntityModel.Product.Product product);

        public void addgroup(DataLayer.EntityModel.Product.Product product, int groupid);

        bool CreateProduct(DataLayer.EntityModel.Product.Product model);

        List<DataLayer.EntityModel.Product.Product> GetAllProduct();

        List<DataLayer.EntityModel.Product.Product> GetAllProductForAdmin();

        bool DeleteProduct(int proId);

        bool Save();

        #endregion




    }
}

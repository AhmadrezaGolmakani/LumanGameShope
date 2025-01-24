using Luman.Busines.DTOs.ProductDTO;
using Luman.DataLayer.Context;
using Luman.DataLayer.EntityModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.Product
{
    public class ProductService : IProductService
    {

        private readonly LumanContext _context;

        public ProductService(LumanContext context)
        {
            _context = context;
        }

        public bool AddCategory(Category model)
        {
            _context.categories.Add(model);
            return Save();
        }

        public void addgroup(DataLayer.EntityModel.Product.Product product, int groupid)
        {
            
            CategoryProduct categoryProduct = new(){
                ProductId = product.ProductId,
                CategoryId = groupid
            };
            _context.Add(categoryProduct);
            Save();
        }

        public bool CreateProduct(DataLayer.EntityModel.Product.Product model)
        {
            _context.products.Add(model);
            return Save();
        }

        

        public bool DeleteProduct(int proId)
        {
            var product = GetproductById(proId);
            _context.products.Remove(product);
            return Save();
        }

        public bool EditeProduct(DataLayer.EntityModel.Product.Product product)
        {
            _context.Update(product);
            return Save();
            
        }

        public List<Category> GetAllCategories()
        {
            return _context.categories.ToList();
        }

        public List<DataLayer.EntityModel.Product.Product> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public List<DataLayer.EntityModel.Product.Product> GetAllProductForAdmin()
        {
            return _context.products.ToList();
        }

        public int GetGroupIdByName(string name)
        {
            return _context.categories.SingleOrDefault(c => c.Name == name).CategoryId;
        }

        public DataLayer.EntityModel.Product.Product GetproductById(int proid)
        {
            return _context.products.Find(proid);
        }

        public int GetProductid(int productId)
        {
            return _context.products.Find(productId).ProductId;
        }


        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}

using Luman.Busines.DTOs.ProductDTO;
using Luman.DataLayer.Context;
using Luman.DataLayer.EntityModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luman.Busines.Services.ProductService
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

        public void addgroup(Product product, int groupid)
        {

            CategoryProduct categoryProduct = new()
            {
                ProductId = product.ProductId,
                CategoryId = groupid
            };
            _context.Add(categoryProduct);
            Save();
        }

        public bool CreateProduct(Product model)
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

        public bool EditeProduct(Product product)
        {
            _context.Update(product);
            return Save();

        }

        public List<Category> GetAllCategories()
        {
            return _context.categories.ToList();
        }

        public List<Product> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProductForAdmin()
        {
            return _context.products.ToList();
        }

        public int GetGroupIdByName(string name)
        {
            return _context.categories.SingleOrDefault(c => c.Name == name).CategoryId;
        }

        public Product GetproductById(int proid)
        {
            return _context.products.Find(proid);
        }

        public int GetProductidByname(string name)
        {

            return _context.products.SingleOrDefault(c => c.Name == name).ProductId;

        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }


    }
}

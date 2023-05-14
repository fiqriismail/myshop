using Microsoft.EntityFrameworkCore;
using MyShopCore.Web.Api.Models.Products;

namespace MyShopCore.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Product> Products { get; set; }    

        public async ValueTask<Product> InsertProductAsync(Product product)
        {
            this.Entry(product).State = EntityState.Added;
            await this.SaveChangesAsync();

            return product;
        }

        public IQueryable<Product> SelectAllProducts()
        {
            return this.Products.AsQueryable();
        }
        public async ValueTask<Product> SelectProductByIdAsync(Guid productId)
        { 
            return await this.Products.FindAsync(productId);
        }

        public async ValueTask<Product> UpdateProductAsync(Product product)
        {
            this.Entry(product).State = EntityState.Modified;
            await this.SaveChangesAsync();

            return product;
        }

        public async ValueTask<Product> DeleteProductAsync(Product product)
        { 
            this.Entry(product).State = EntityState.Deleted;
            await this.SaveChangesAsync();

            return product;

        }
    }
}

using MyShopCore.Web.Api.Brokers.DateTimes;
using MyShopCore.Web.Api.Brokers.Loggings;
using MyShopCore.Web.Api.Brokers.Storages;
using MyShopCore.Web.Api.Models.Products;

namespace MyShopCore.Web.Api.Services.Foundations.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public ProductService(
            ILoggingBroker loggingBroker, 
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async ValueTask<Product> AddProductAsync(Product product)
        {
            this.loggingBroker.LogInformation($"{product.Title} added");

            product.Id = Guid.NewGuid();
            product.Created = this.dateTimeBroker.GetCurrentDateTime();
            product.CreatedBy = Guid.NewGuid();

            return await this.storageBroker.InsertProductAsync(product);
        }

        public async ValueTask<Product> ModifyProductAsync(Product product)
        {
            this.loggingBroker.LogInformation($"{product.Title} modified");

            product.Updated = this.dateTimeBroker.GetCurrentDateTime();
            product.UpdatedBy = Guid.NewGuid();

            return await this.storageBroker.UpdateProductAsync(product);
        }

        public async ValueTask<Product> RemoveProductAsync(Product product)
        {
            this.loggingBroker.LogInformation($"{product.Title} removed");
            return await this.storageBroker.DeleteProductAsync(product);
        }

        public IQueryable<Product> RetrieveAllProducts()
        {
            return this.storageBroker.SelectAllProducts();
        }

        public async ValueTask<Product> RetrieveProductByIdAsync(Guid productId)
        {
            return await this.storageBroker.SelectProductByIdAsync(productId);
        }
    }
}

namespace MyShopCore.Web.Api.Models.Products.Exceptions
{
    public class InvalidProductIdException : Exception
    {
        public InvalidProductIdException() : base("Product id cannot be null") { }
    }
}

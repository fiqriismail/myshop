namespace MyShopCore.Web.Api.Models.Products.Exceptions
{
    public class NullProductException : Exception
    {
        public NullProductException() 
            : base(message: "Product is null")
        {      
        }

       
    }
}

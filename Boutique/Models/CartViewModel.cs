using Boutique.Entities.Concrate;

namespace Boutique.Models
{
    public class CartViewModel
    {
        public List<CartLine> CartLines { get; set; }
        public CartViewModel()
        {
            CartLines = new List<CartLine>();
        }

        public void AddProduct(Product product, int quantity, string isIncrease)
        {

            var selectedProduct = CartLines.FirstOrDefault(x => x.Products.Id == product.Id);

            if (selectedProduct == null)
            {
                CartLines.Add(new CartLine { Products = product, Quantity = quantity });
            }
            else
            {
                if (isIncrease == "increase")
                {
                    selectedProduct.Quantity += quantity;
                }
                else
                {
                    selectedProduct.Quantity -= quantity;
                }

            }
        }
        public void RemoveProduct(Product product)
        {
            CartLines.RemoveAll(i => i.Products.Id == product.Id);
        }

        public double TotalPrice()
        {
            return CartLines.Sum(i => i.Products.Price * i.Quantity);
        }

        public void ClearAll()
        {
            CartLines.Clear();
        }
    }
}

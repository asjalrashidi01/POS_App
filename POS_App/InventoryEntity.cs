using System;

namespace POS_App
{
    public class InventoryEntity
    {
        // Properties

        public List<ProductEntity> Products { get; set; }

        // Constructor

        public InventoryEntity(List<ProductEntity> products)
        {
            Products = products;
        }

        // Methods

        public void AddProduct(ProductEntity product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(ProductEntity product)
        {
            Products.Remove(product);
        }
    }
}
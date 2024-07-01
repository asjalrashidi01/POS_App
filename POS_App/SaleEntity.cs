using System;
using System.Numerics;

namespace POS_App
{
    public class SaleEntity
    {
        // Properties

        public List<ProductEntity> Products { get; set; }
        public float Total { get; set; }

        // Constructor

        public SaleEntity(List<ProductEntity> products, float total)
        {
            Products = products;
            Total = total;
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

        public float GetTotal()
        {
            return Total;
        }

        public void AddToTotal(float new_total)
        {
            Total += new_total;
        }
    }
}
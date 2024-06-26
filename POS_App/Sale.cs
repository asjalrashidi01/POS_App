using System;
using System.Numerics;

public class Sale
{
    // Properties

    public List<Product> Products { get; set; }

    public float Total { get; set; }

    // Constructor

    public Sale(List<Product> products, float total)
    {
        Products = products;
        Total = total;
    }

    // Methods

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(Product product)
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
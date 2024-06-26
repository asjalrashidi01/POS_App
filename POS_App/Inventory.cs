using System;

public class Inventory
{
    // Properties

    public List<Product> Products { get; set; }

    // Constructor

    public Inventory(List<Product> products) {
        Products = products;
    }

    // Methods

    public void AddProduct (Product product) {
        Products.Add(product);
    }

    public void RemoveProduct (Product product) {
        Products.Remove(product);
    }
}
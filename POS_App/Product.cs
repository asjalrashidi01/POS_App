using System;

public class Product
{
    // Properties

    public string Name { get; set; }
    public float Price { get; set; }
    public float Quantity { get; set; }
    public int Type { get; set; }
    public int Category { get; set; }

    // Constructor

    public Product(string name, float price, float quantity, int type, int category)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        Type = type;
        Category = category;
    }
}
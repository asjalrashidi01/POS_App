#region Imports

using System;
using System.Xml.Linq;

#endregion

#region Initializing DB (static class)

DataContext data = new DataContext(new List<Admin>(), new List<Cashier>(), new Inventory(new List<Product>()), new List<Sale>(), new Sale(new List<Product>(), 0));

#endregion

#region Initializing DB (static lists)

// Dictionary for product types


data.ProductTypes.Add(1, "Electronics");
data.ProductTypes.Add(2, "Clothing");
data.ProductTypes.Add(3, "Groceries");

// Dictionary for product categories


data.ProductCategories.Add(1, "Mobiles");
data.ProductCategories.Add(2, "Laptops");
data.ProductCategories.Add(3, "T-Shirts");
data.ProductCategories.Add(4, "Jeans");
data.ProductCategories.Add(5, "Fruits");
data.ProductCategories.Add(6, "Vegetables");

#endregion

#region Main

// Variables

string choice = "0";

// Adding dummy users

data.AddAdminUser(new Admin("Alice", "alice@gmail.com", "alice123"));
data.AddAdminUser(new Admin("Bob", "bob@gmail.com", "bob123"));
data.AddAdminUser(new Admin("Claire", "claire@gmail.com", "claire123"));

data.AddCashierUser(new Cashier("Alex", "alex@gmail.com", "alex123"));
data.AddCashierUser(new Cashier("Beth", "beth@gmail.com", "beth123"));
data.AddCashierUser(new Cashier("Charlie", "charlie@gmail.com", "charlie123"));

// Adding dummy products

data.AddInventoryProduct(new Product("iPhone", 700f, 5, 1, 1));
data.AddInventoryProduct(new Product("HP Probook", 900f, 10, 1, 2));
data.AddInventoryProduct(new Product("Red Shirt", 20f, 12, 2, 3));
data.AddInventoryProduct(new Product("Blue Jeans", 30f, 6, 2, 4));
data.AddInventoryProduct(new Product("Apple", 2f, 100, 3, 5));
data.AddInventoryProduct(new Product("Potato", 1f, 250, 3, 6));

// Main App

Console.WriteLine("Welcome to the POS App!");

Console.WriteLine("Enter numbers to select the respective choice");

Console.WriteLine("1. Admin");
Console.WriteLine("2. Cashier");

Console.WriteLine("Enter your choice: ");

choice = Console.ReadLine();

if (choice == "1")
{
    Console.WriteLine("\n\n1. SignUp");
    Console.WriteLine("2. LogIn");

    Console.WriteLine("Enter your choice: ");

    choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("\n\nSignUp (Admin)");

        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter your email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        Admin admin = new Admin(name, email, password);
        data.AddAdminUser(admin);

        Console.WriteLine("\n\nLogIn (Admin)");

        Console.WriteLine("Enter your email: ");
        email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        password = Console.ReadLine();

        string loginStatus = data.LoginAdminUser(email, password);

        if (loginStatus == "Login successful!")
        {
            Console.WriteLine("\n\nLogin successful!");

            while (true)
            {
                data.PrintInventory();

                Console.WriteLine("\n\n1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Details");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Enter your choice: ");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Enter product name: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter product price: ");

                    float price = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product quantity: ");

                    float quantity = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product type: ");

                    foreach (KeyValuePair<int, string> type in data.ProductTypes)
                    {
                        Console.WriteLine(type.Key + ". " + type.Value);
                    }

                    int type_value = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product category: ");

                    foreach (KeyValuePair<int, string> category in data.ProductCategories)
                    {
                        Console.WriteLine(category.Key + ". " + category.Value);
                    }

                    int category_value = int.Parse(Console.ReadLine());

                    data.AddInventoryProduct(new Product(product_name, price, quantity, type_value, category_value));
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Enter product name to be deleted: ");

                    string product_name = Console.ReadLine();

                    string removeStatus = data.RemoveInventoryProduct(product_name);

                    if (removeStatus == "Success!")
                    {
                        Console.WriteLine("\n\nProduct removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine(removeStatus);
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Enter product name to be updated: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter new product name: ");

                    string new_name = Console.ReadLine();

                    Console.WriteLine("Enter new product price: ");

                    float new_price = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product quantity: ");

                    float new_quantity = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product type: ");

                    foreach (KeyValuePair<int, string> type in data.ProductTypes)
                    {
                        Console.WriteLine(type.Key + ". " + type.Value);
                    }

                    int new_type_value = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product category: ");

                    foreach (KeyValuePair<int, string> category in data.ProductCategories)
                    {
                        Console.WriteLine(category.Key + ". " + category.Value);
                    }

                    int new_category_value = int.Parse(Console.ReadLine());

                    string updateStatus = data.UpdateInventoryProduct(product_name, new_name, new_price, new_quantity, new_type_value, new_category_value);

                    if (updateStatus == "Success!")
                    {
                        Console.WriteLine("\n\nProduct updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine(updateStatus);
                    }
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid choice. Please try again.");
                }
            }
        }
        else
        {
            Console.WriteLine(loginStatus);
        }
    }
    else if (choice == "2")
    {
        Console.WriteLine("\n\nLogIn (Admin)");

        Console.WriteLine("Enter your email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        string loginStatus = data.LoginAdminUser(email, password);

        if (loginStatus == "Login successful!")
        {
            Console.WriteLine("\n\nLogin successful!");

            while (true)
            {
                data.PrintInventory();

                Console.WriteLine("\n\n1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Details");
                Console.WriteLine("4. Exit");

                Console.WriteLine("Enter your choice: ");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Enter product name: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter product price: ");

                    float price = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product quantity: ");

                    float quantity = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product type: ");

                    foreach (KeyValuePair<int, string> type in data.ProductTypes)
                    {
                        Console.WriteLine(type.Key + ". " + type.Value);
                    }

                    int type_value = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter product category: ");

                    foreach (KeyValuePair<int, string> category in data.ProductCategories)
                    {
                        Console.WriteLine(category.Key + ". " + category.Value);
                    }

                    int category_value = int.Parse(Console.ReadLine());

                    data.AddInventoryProduct(new Product(product_name, price, quantity, type_value, category_value));
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Enter product name to be deleted: ");

                    string product_name = Console.ReadLine();

                    string removeStatus = data.RemoveInventoryProduct(product_name);

                    if (removeStatus == "Success!")
                    {
                        Console.WriteLine("\n\nProduct removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine(removeStatus);
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Enter product name to be updated: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter new product name: ");

                    string new_name = Console.ReadLine();

                    Console.WriteLine("Enter new product price: ");

                    float new_price = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product quantity: ");

                    float new_quantity = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product type: ");

                    foreach (KeyValuePair<int, string> type in data.ProductTypes)
                    {
                        Console.WriteLine(type.Key + ". " + type.Value);
                    }

                    int new_type_value = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter new product category: ");

                    foreach (KeyValuePair<int, string> category in data.ProductCategories)
                    {
                        Console.WriteLine(category.Key + ". " + category.Value);
                    }

                    int new_category_value = int.Parse(Console.ReadLine());

                    string updateStatus = data.UpdateInventoryProduct(product_name, new_name, new_price, new_quantity, new_type_value, new_category_value);

                    if (updateStatus == "Success!")
                    {
                        Console.WriteLine("\n\nProduct updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine(updateStatus);
                    }
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid choice. Please try again.");
                }
            }
        }
        else
        {
            Console.WriteLine(loginStatus);
        }
    }
    else {
        Console.WriteLine("\n\nInvalid choice. Please try again.");
    }
}
else if (choice == "2") {
    Console.WriteLine("\n\n1. SignUp");
    Console.WriteLine("2. LogIn");

    Console.WriteLine("Enter your choice: ");

    choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("\n\nSignUp (Cashier)");

        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter your email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        Cashier cashier = new Cashier(name, email, password);
        data.AddCashierUser(cashier);

        Console.WriteLine("\n\nLogIn (Cashier)");

        Console.WriteLine("Enter your email: ");
        email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        password = Console.ReadLine();

        string loginStatus = data.LoginCashierUser(email, password);

        if (loginStatus == "Login successful!")
        {
            Console.WriteLine("\n\nLogin successful!");

            data.StartSale();

            while (true)
            {
                data.PrintInventory();

                data.PrintCurrentSale();

                data.PrintCurrentTotal();

                Console.WriteLine("\n\n1. Add Product");
                Console.WriteLine("2. Confirm Sale");
                Console.WriteLine("3. Exit");

                Console.WriteLine("Enter your choice: ");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n\nEnter product name: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter product quantity: ");

                    float product_quantity = float.Parse(Console.ReadLine());

                    data.UpdateSale(product_name, product_quantity);
                }
                else if (choice == "2")
                {
                    data.ConfirmSale();
                    
                    Console.WriteLine("\n\nSale confirmed!");

                    Console.WriteLine("Your total is: " + data.GetTotal());
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid choice. Please try again.");
                }
            }
        }
        else
        {
            Console.WriteLine(loginStatus);
        }
    }
    else if (choice == "2")
    {
        Console.WriteLine("\n\nLogIn (Cashier)");

        Console.WriteLine("Enter your email: ");
        string email = Console.ReadLine();

        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();

        string loginStatus = data.LoginCashierUser(email, password);

        if (loginStatus == "Login successful!")
        {
            Console.WriteLine("\n\nLogin successful!");

            data.StartSale();

            while (true)
            {
                data.PrintInventory();

                data.PrintCurrentSale();

                data.PrintCurrentTotal();

                Console.WriteLine("\n\n1. Add Product");
                Console.WriteLine("2. Confirm Sale");
                Console.WriteLine("3. Exit");

                Console.WriteLine("Enter your choice: ");

                choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n\nEnter product name: ");

                    string product_name = Console.ReadLine();

                    Console.WriteLine("Enter product quantity: ");

                    float product_quantity = float.Parse(Console.ReadLine());

                    data.UpdateSale(product_name, product_quantity);
                }
                else if (choice == "2")
                {
                    data.ConfirmSale();

                    Console.WriteLine("\n\nSale confirmed!");

                    Console.WriteLine("Your total is: " + data.GetTotal());
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid choice. Please try again.");
                }
            }
        }
        else
        {
            Console.WriteLine(loginStatus);
        }
    }
    else
    {
        Console.WriteLine("\n\nInvalid choice. Please try again.");
    }
}
else {
    Console.WriteLine("\n\nInvalid choice. Please try again.");
}

Console.WriteLine("Thank you for using the POS App!");

data.PrintAllData();

#endregion
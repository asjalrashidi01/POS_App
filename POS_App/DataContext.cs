using System;

public class DataContext
{
    // Properties

    private List<Admin> AdminUsers { get; set; }
    private List<Cashier> CashierUsers { get; set; }
    private Inventory InventoryList { get; set; }
    private List<Sale> SalesList { get; set; }
    private Sale CurrentSale { get; set; }

    // Dictionary for product types
    public Dictionary<int, string> ProductTypes = new Dictionary<int, string>();

    // Dictionary for product categories
    public Dictionary<int, string> ProductCategories = new Dictionary<int, string>();

    // Constructor

    public DataContext(List<Admin> adminUsers, List<Cashier> cashierUsers, Inventory inventoryList, List<Sale> salesList, Sale currentSale)
    {
        AdminUsers = adminUsers;
        CashierUsers = cashierUsers;
        InventoryList = inventoryList;
        SalesList = salesList;
        CurrentSale = currentSale;
    }

    // Methods

    public void AddAdminUser(Admin admin)
    {
        AdminUsers.Add(admin);
    }

    public void RemoveAdminUser(Admin admin)
    {
        AdminUsers.Remove(admin);
    }

    public string LoginAdminUser(string email, string password)
    {
        Admin existingAdmin = AdminUsers.Find(a => a.Email == email);

        if (existingAdmin != null && existingAdmin.Login(email, password))
        {
            return "Login successful!";
        }
        else if (existingAdmin != null)
        {
            return "Incorrect password!";
        }
        else
        {
           return "Login failed!";
        }
    }

    public void AddCashierUser(Cashier cashier)
    {
        CashierUsers.Add(cashier);
    }

    public void RemoveCashierUser(Cashier cashier)
    {
        CashierUsers.Remove(cashier);
    }

    public string LoginCashierUser(string email, string password)
    {
        Cashier existingCashier = CashierUsers.Find(a => a.Email == email);

        if (existingCashier != null && existingCashier.Login(email, password))
        {
            return "Login successful!";
        }
        else if (existingCashier != null)
        {
            return "Incorrect password!";
        }
        else
        {
            return "Login failed!";
        }
    }

    public void AddInventoryProduct(Product product)
    {
        InventoryList.AddProduct(product);
    }

    public string RemoveInventoryProduct(string product_name)
    {
        Product product = InventoryList.Products.Find(p => p.Name == product_name);

        if (product == null)
        {
            return "\n\nProduct not found. Please try again.";
        }
        else
        {
            InventoryList.RemoveProduct(product);
            return "Success!";
        }
    }

    public string UpdateInventoryProduct(string product_name, string new_product_name, float new_product_price, float new_product_quantity, int new_product_type, int new_product_category)
    {
        Product product_to_update = InventoryList.Products.Find(p => p.Name == product_name);

        if (product_to_update == null || new_product_name == null || new_product_price == null || new_product_quantity == null || new_product_type == null || new_product_category == null)
        {
            return "\n\nProduct not found. Please try again.";
        }
        else
        {
            product_to_update.Name = new_product_name;
            product_to_update.Price = new_product_price;
            product_to_update.Quantity = new_product_quantity;
            product_to_update.Type = new_product_type;
            product_to_update.Category = new_product_category;

            return "Success!";
        }
    }

    public void PrintInventory()
    {
        Console.WriteLine("\n\nCurrent Inventory:");

        foreach (Product product in InventoryList.Products)
        {
            Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
        }
    }

    public void AddSale(Sale sale)
    {
        SalesList.Add(sale);
    }

    public void PrintAllData()
    {
        Console.WriteLine("\n\nDATA:");

        Console.WriteLine("\n\nAdmin Users:");

        foreach (Admin admin in AdminUsers)
        {
            Console.WriteLine("Name: " + admin.Name + ", Email: " + admin.Email);
        }

        Console.WriteLine("\n\nCashier Users:");

        foreach (Cashier cashier in CashierUsers)
        {
            Console.WriteLine("Name: " + cashier.Name + ", Email: " + cashier.Email);
        }

        Console.WriteLine("\n\nInventory:");

        foreach (Product product in InventoryList.Products)
        {
            Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
        }

        Console.WriteLine("\n\nSales:");

        foreach (Sale sale in SalesList)
        {
            Console.WriteLine("\nProducts:\n");

            foreach (Product product in sale.Products)
            {
                Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
            }

            Console.WriteLine("\nTotal: " + sale.GetTotal());
        }
    }

    public void StartSale()
    {
        CurrentSale = new Sale(new List<Product>(), 0);
    }

    public void UpdateSale(string product_name, float product_quantity)
    {
        Product product = InventoryList.Products.Find(p => p.Name == product_name);

        if (product != null && product.Quantity >= product_quantity)
        {
            CurrentSale.AddProduct(new Product(product.Name, product.Price, product_quantity, product.Type, product.Category));
            product.Quantity -= product_quantity;
            CurrentSale.AddToTotal(product.Price * product_quantity);

            if (product.Quantity == 0)
            {
                InventoryList.RemoveProduct(product);
            }
        }
        else
        {
            Console.WriteLine("\n\nProduct not found or quantity not available. Please try again.");
        }
    }

    public void ConfirmSale()
    {
        AddSale(CurrentSale);
        CurrentSale = new Sale(new List<Product>(), 0);
    }

    public void PrintCurrentSale()
    {
        Console.WriteLine("\n\nCurrent Sale");

        foreach (Product product in CurrentSale.Products)
        {
            Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
        }
    }

    public void PrintCurrentTotal()
    {
        Console.WriteLine("\n\nCurrent Total: " + CurrentSale.GetTotal());
    }

    public float GetTotal()
    {
        return CurrentSale.GetTotal();
    }
}
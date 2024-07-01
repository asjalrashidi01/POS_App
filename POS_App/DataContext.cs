using System;

namespace POS_App
{
    public class DataContext
    {
        // Properties

        private List<AdminEntity> AdminUsers { get; set; }
        private List<CashierEntity> CashierUsers { get; set; }
        private InventoryEntity InventoryList { get; set; }
        private List<SaleEntity> SalesList { get; set; }
        private SaleEntity CurrentSale { get; set; }

        // Dictionary for product types
        public Dictionary<int, string> ProductTypes = new Dictionary<int, string>();

        // Dictionary for product categories
        public Dictionary<int, string> ProductCategories = new Dictionary<int, string>();

        // Constructor
        public DataContext()
        {
            AdminUsers = new List<AdminEntity>();
            CashierUsers = new List<CashierEntity>();
            InventoryList = new InventoryEntity(new List<ProductEntity>());
            SalesList = new List<SaleEntity>();
            CurrentSale = new SaleEntity(new List<ProductEntity>(), 0);
        }
        public DataContext(List<AdminEntity> adminUsers, List<CashierEntity> cashierUsers, InventoryEntity inventoryList, List<SaleEntity> salesList, SaleEntity currentSale)
        {
            AdminUsers = adminUsers ?? new List<AdminEntity>();
            CashierUsers = cashierUsers ?? new List<CashierEntity>();
            InventoryList = inventoryList ?? new InventoryEntity(new List<ProductEntity>());
            SalesList = salesList ?? new List<SaleEntity>();
            CurrentSale = currentSale ?? new SaleEntity(new List<ProductEntity>(), 0);
        }

        // Methods

        public void AddAdminUser(AdminEntity admin)
        {
            AdminUsers.Add(admin);
        }

        public void RemoveAdminUser(AdminEntity admin)
        {
            AdminUsers.Remove(admin);
        }

        public async Task<string> LoginAdminUser(string email, string password)
        {
            AdminEntity existingAdmin = AdminUsers.Find(a => a.Email == email);

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

        public void AddCashierUser(CashierEntity cashier)
        {
            CashierUsers.Add(cashier);
        }

        public void RemoveCashierUser(CashierEntity cashier)
        {
            CashierUsers.Remove(cashier);
        }

        public async Task<string> LoginCashierUser(string email, string password)
        {
            CashierEntity existingCashier = CashierUsers.Find(a => a.Email == email);

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

        public async void AddInventoryProduct(ProductEntity product)
        {
            InventoryList.AddProduct(product);
        }

        public async Task<string> RemoveInventoryProduct(string product_name)
        {
            ProductEntity product = InventoryList.Products.Find(p => p.Name == product_name);

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

        public async Task<string> UpdateInventoryProduct(string product_name, string new_product_name, float new_product_price, float new_product_quantity, int new_product_type, int new_product_category)
        {
            ProductEntity product_to_update = InventoryList.Products.Find(p => p.Name == product_name);

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

        public async Task<List<ProductEntity>> GetProductList() {
            return InventoryList.Products;
        }

        public void PrintInventory()
        {
            Console.WriteLine("\n\nCurrent Inventory:");

            foreach (ProductEntity product in InventoryList.Products)
            {
                Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
            }
        }

        public async Task<List<SaleEntity>> GetSaleList()
        {
            return SalesList;
        }

        public void AddSale(SaleEntity sale)
        {
            SalesList.Add(sale);
        }

        public void PrintAllData()
        {
            Console.WriteLine("\n\nDATA:");

            Console.WriteLine("\n\nAdmin Users:");

            foreach (AdminEntity admin in AdminUsers)
            {
                Console.WriteLine("Name: " + admin.Name + ", Email: " + admin.Email);
            }

            Console.WriteLine("\n\nCashier Users:");

            foreach (CashierEntity cashier in CashierUsers)
            {
                Console.WriteLine("Name: " + cashier.Name + ", Email: " + cashier.Email);
            }

            Console.WriteLine("\n\nInventory:");

            foreach (ProductEntity product in InventoryList.Products)
            {
                Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
            }

            Console.WriteLine("\n\nSales:");

            foreach (SaleEntity sale in SalesList)
            {
                Console.WriteLine("\nProducts:\n");

                foreach (ProductEntity product in sale.Products)
                {
                    Console.WriteLine("Name: " + product.Name + ", Price: " + product.Price + ", Quantity: " + product.Quantity + ", Type: " + ProductTypes[product.Type] + ", Category: " + ProductCategories[product.Category]);
                }

                Console.WriteLine("\nTotal: " + sale.GetTotal());
            }
        }

        public void StartSale()
        {
            CurrentSale = new SaleEntity(new List<ProductEntity>(), 0);
        }

        public void UpdateSale(string product_name, float product_quantity)
        {
            ProductEntity product = InventoryList.Products.Find(p => p.Name == product_name);

            if (product != null && product.Quantity >= product_quantity)
            {
                CurrentSale.AddProduct(new ProductEntity(product.Name, product.Price, product_quantity, product.Type, product.Category));
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
            CurrentSale = new SaleEntity(new List<ProductEntity>(), 0);
        }

        public void PrintCurrentSale()
        {
            Console.WriteLine("\n\nCurrent Sale");

            foreach (ProductEntity product in CurrentSale.Products)
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
        public List<AdminEntity> GetAdminUsers()
        {
            return this.AdminUsers;
        }

        public List<CashierEntity> GetCashierUsers()
        {
            return this.CashierUsers;
        }
    }
}
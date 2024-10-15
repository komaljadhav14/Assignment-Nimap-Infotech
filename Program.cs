using System;
using System.Data;

namespace Nimap_Infotech_Assignment_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create CategoryDAL and ProductDAL instances
            CategoryDAL categoryDAL = new CategoryDAL();
            ProductDAL productDAL = new ProductDAL();

            // Add a Category (ensure add a category first)
            categoryDAL.AddCategory("Electronics");

            // Add a Product (ensure category ID is correct; check DB for actual IDs)
            productDAL.AddProduct("Smartphone", 1);

            // Display Paginated Product List (Page 1, Size 10)
            DataTable products = productDAL.GetPaginatedProducts(1, 10);
            foreach (DataRow row in products.Rows)
            {
                Console.WriteLine($"ProductId: {row["ProductId"]}, ProductName: {row["ProductName"]}, CategoryId: {row["CategoryId"]}, CategoryName: {row["CategoryName"]}");
            }
        }
    }
}

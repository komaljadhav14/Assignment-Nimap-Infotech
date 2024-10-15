using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Nimap_Infotech_Assignment_1
{
    public class ProductDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ProductDB"].ConnectionString;

        // Add Product
        public void AddProduct(string productName, int categoryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Products (ProductName, CategoryId) VALUES (@ProductName, @CategoryId)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product added successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // Get All Products
        public DataTable GetPaginatedProducts(int pageNumber, int pageSize)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Calculate the starting row number
                    int startRow = (pageNumber - 1) * pageSize;

                    // Use OFFSET and FETCH for pagination in SQL Server
                    string query = @"
                        SELECT P.ProductId, P.ProductName, C.CategoryId, C.CategoryName
                        FROM Products P
                        JOIN Categories C ON P.CategoryId = C.CategoryId
                        ORDER BY P.ProductId
                        OFFSET @StartRow ROWS
                        FETCH NEXT @PageSize ROWS ONLY";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartRow", startRow);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return dt;
        }

        // Update Product
        public void UpdateProduct(int productId, string productName, int categoryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Products SET ProductName = @ProductName, CategoryId = @CategoryId WHERE ProductId = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product updated successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // Delete Product
        public void DeleteProduct(int productId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Products WHERE ProductId = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product deleted successfully.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}

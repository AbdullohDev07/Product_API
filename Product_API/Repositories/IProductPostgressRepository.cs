using Npgsql;
using Product_API.Models;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        public string ConnectionString = "Server=localhost;Port=16172;Database=your_database;username=postgres;Password=axihub;";
        public Product Add(Product product)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {

                connection.Open();
                try
                {
                    using NpgsqlCommand cmd = new NpgsqlCommand(@$"insert into products(Name,Description,PhotoPath) values ('{product.Name}','{product.Description}','{product.PhotoPath}') ", connection);
                    cmd.ExecuteNonQuery();


                }
                catch
                {
                }
            }
            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {

                connection.Open();
                try
                {
                    Product product = new Product();
                    using NpgsqlCommand cmd = new NpgsqlCommand(@$"select * from product ", connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        product.Name = reader.GetString(0);
                        product.Description = reader.GetString(1);
                        product.PhotoPath = reader.GetString(2);
                        products.Add(product);
                    }

                }
                catch
                {
                }
                return products;
            }
        }


    }
}

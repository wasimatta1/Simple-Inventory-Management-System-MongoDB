using MongoDB.Driver;

namespace Simple_Inventory_Management_System
{
    internal class Inventory : IInventory
    {
        IMongoCollection<Product> productCollection;

        public Inventory()
        {
            this.productCollection = MongoDBConnection();
        }

        private IMongoCollection<Product> MongoDBConnection()
        {
            string connectionString = "mongodb://127.0.0.1:27017";
            string DBName = "Inventory-Management";
            string collectionName = "Product";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(DBName);
            return db.GetCollection<Product>(collectionName);

        }


        public void AddProduct(Product input)
        {
            //var filter = Builders<Product>.Filter.Eq(p => p.Name, input.Name);
            var existingProduct = productCollection.Find(p => p.Name == input.Name).FirstOrDefault();

            if (existingProduct != null)
            {
                existingProduct.Quantity += input.Quantity;
                editProduct(existingProduct);
            }
            else
            {
                productCollection.InsertOne(input);
            }
        }

        public void deleteProduct(string input)
        {

            var result = productCollection.DeleteOne(p => p.Name == input);

            if (result.DeletedCount == 0)
            {
                Console.WriteLine("Product not found");
            }

        }

        public void editProduct(Product input)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, input.Name);
            var update = Builders<Product>.Update.Set(p => p.Price, input.Price)
                                                 .Set(p => p.Quantity, input.Quantity);

            var result = productCollection.UpdateOne(filter, update);

            if (result.MatchedCount == 0)
            {
                Console.WriteLine("Product not found");
            }


        }

        public IEnumerable<Product> ListProducts() => productCollection.Find(_ => true).ToList();



        public Product? searchProduct(string input) => productCollection.Find(p => p.Name == input).FirstOrDefault();

    }
}

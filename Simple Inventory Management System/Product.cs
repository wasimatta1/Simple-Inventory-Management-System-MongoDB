using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Simple_Inventory_Management_System
{
    public record Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string _id;
        private string _name;
        private double _price;
        private int _quantity;


        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set { _price = value < 0 ? 0 : value; } }
        public int Quantity { get => _quantity; set { _quantity = value < 0 ? 0 : value; } }

    }
}

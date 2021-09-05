using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;




namespace DrinkMachine.Models
{
    public class Product 
    {
        private int _quantity, _price;
        private string _name;

        private static Dictionary<string, int> PRODUCTS_PRICE = new Dictionary<string, int>() {
            {Constants.COKE_KEY, 25 },
            { Constants.PEPSI, 36 },
            { Constants.SODA, 45}
        };
            
        [Range(0,Int64.MaxValue, ErrorMessage ="The number of drinks selected can't be less than 0")]
        public int Quantity { get =>  _quantity; set => _quantity = value; }
        public int Price { get => _price = getProductPrice(); }
        public string Name { get => _name; set => _name = value; }

        public Product() 
        { }

        public Dictionary<string, int> getPriceProducts()
        {
            return PRODUCTS_PRICE;
        }

        public int getProductPrice(string id)
        {
            return PRODUCTS_PRICE[id];
        }

        private  int getProductPrice()
        {
            return getProductPrice(this.Name);
        }
    }
}

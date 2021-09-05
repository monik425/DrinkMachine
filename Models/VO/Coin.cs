using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMachine.Models
{
    
    public class Coin 
    {
        private static Dictionary<string, int> COINS_VALUES = new Dictionary<string, int>() { 
            {Constants.PENNY_VALUE, 1 }, 
            { Constants.NICKEL_VALUE, 5 }, 
            { Constants.DIME_VALUE, 10}, 
            { Constants.QUARTER_VALUE, 25 } };

        private int _quantity, _value;
        private string _id;

        [Range(0, Int64.MaxValue, ErrorMessage = "The amount of coins can't be less than 0")]
        public int Quantity { get => _quantity; set => _quantity = value; }
        public int Value { get => _value = GetValue(); }
        public string Id { get => _id; set => _id = value; }

        public Dictionary<string, int> GetAcceptedMoney()
        {
            return COINS_VALUES;
        }

        public int GetValueById(string _Id)
        {
            int retVal = -1;
            if (COINS_VALUES.ContainsKey(_Id))
            {
                retVal = COINS_VALUES[_Id];
            }

            return retVal;
        }

        private int GetValue()
        {
            return GetValueById(this.Id);

        }
    }
}

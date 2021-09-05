using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMachine.Models
{
    public class Constants
    {
        public static string DIME_VALUE = "Dime";
        public static string PENNY_VALUE = "Penny";
        public static string NICKEL_VALUE = "Nickel";
        public static string QUARTER_VALUE = "Quarter";

        public static string COKE_KEY = "Coke";
        public static string PEPSI = "Pepsy";
        public static string SODA = "Soda";

        public static int NOT_ENOUGH_CHANGE_ERROR_KEY = 1;
        public static int NOT_ENOUGH_PRODUCT_ERROR_KEY = 2;
        public static int NOT_ENOUGH_MONEY_ERROR_KEY = 3;
        public static string NOT_ENOUGH_CHANGE_ERROR_VALUE = "Not sufficient change in the inventory";
        public static string NOT_ENOUGH_PRODUCT_ERROR_VALUE =  "Drink is sold out, your purchase cannot be processed";
        public static string NOT_ENOUGH_MONEY_ERROR_VALUE = "Not sufficient money inserted";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMachine.Models.Utils
{
    public static class VendingMachineUtils
    { 
       public static Dictionary<string, int> CalculateChange(int Change, List<Coin> MachineCoins)
        {
            Dictionary<string, int> ReturningChange = new Dictionary<string, int>();
            int RemainingChange = Change;

            if (RemainingChange > 0)
            {
                List<Coin> SortedAvailableMoney = SortMoneyListByCurrencyValue(MachineCoins);
                foreach (Coin money in SortedAvailableMoney)
                {
                    if (RemainingChange >= money.Value)
                    {
                        int QuantityToSubstract = RemainingChange / money.Value;

                        if (QuantityToSubstract <= money.Quantity)
                        {
                            RemainingChange %= money.Value;
                        }

                        else
                        {
                            RemainingChange -= (money.Value * money.Quantity);
                            QuantityToSubstract = money.Quantity;
                        }

                        ReturningChange.Add(money.Id, QuantityToSubstract);
                        UpdateAmountOfCoinsList(money.Id, QuantityToSubstract * -1, MachineCoins);

                    }
                }
            }
            return ReturningChange;
        }


        public static void ProcessPurchase (VendingMachine VendingMachine)
        {
            ProcessReceivedMoney(VendingMachine);
            ProcessSelectedProducts(VendingMachine);
        }

        


        public static void InitializeClientObjects(VendingMachine VendingMachine, Dictionary<string, int> ValidProducts, Dictionary<string, int> ValidCoins)
        {
            VendingMachine.ClientCoins = InitializeClientCoins(ValidCoins);
            VendingMachine.ClientProducts = InitializeClientProducts(ValidProducts);
        }

        public static void InitializeMachine(VendingMachine VendingMachine)
        {
            VendingMachine.InventoryProducts = SetMachineProducts();
            VendingMachine.MachineCoins = InitializeMachineStartingMoney();
        }


        public static Dictionary<string, int> ProductListToDictionary(List<Product> List)
        {
            return List.Select(p => new { id = p.Name, quantity = p.Quantity }).ToDictionary(x => x.id, x => x.quantity);
        }

        private static List<Coin> SortMoneyListByCurrencyValue(List<Coin> List)
        {
            var qry = from e in List
                      orderby e.Value descending
                      select e;

            return qry.ToList<Coin>();

        }

        /**
         * PRIVATE METHODS
         * **/

        private static void ProcessSelectedProducts(VendingMachine VendingMachine)
        {
            foreach (Product product in VendingMachine.ClientProducts)
            {
                if (product.Quantity > 0)
                {
                    UpdateProductInventory(product.Name, product.Quantity * -1, VendingMachine.InventoryProducts); // doing the quantity a negative number because this method could be updated to restock the Inventory in the future
                }
            }
        }

        private static void ProcessReceivedMoney(VendingMachine Machine)
        {
            foreach (Coin coin in Machine.ClientCoins)
            {
                if (coin.Quantity > 0) // To avoid updating the machine coins if the client didn't add that particular coin
                {
                    UpdateAmountOfCoinsList(coin.Id, coin.Quantity, Machine.MachineCoins);
                }
            }
        }

        private static void UpdateAmountOfCoinsList(String CoinId, int quantityToAdd, List<Coin> List)
        {
            var coin = List.Find(item => item.Id == CoinId);
            List.Remove(coin);

            coin.Quantity += quantityToAdd;
            List.Add(coin);

        }


        private static void UpdateProductInventory(string ProductId, int Quantity, List<Product> InventoryProducts)
        {
            var myItem = InventoryProducts.Find(item => item.Name == ProductId);
            InventoryProducts.Remove(myItem);
            myItem.Quantity += Quantity;
            InventoryProducts.Add(myItem);
        }



        private static List<Coin> InitializeMachineStartingMoney()
        {
            return new List<Coin> {
                new Coin { Id = Constants.PENNY_VALUE, Quantity = 100 },
                new Coin { Id = Constants.NICKEL_VALUE, Quantity = 10 },
                new Coin { Id = Constants.DIME_VALUE, Quantity = 5 },
                new Coin { Id = Constants.QUARTER_VALUE, Quantity = 25 }

            };

        }


        
        private static List<Coin> InitializeClientCoins(Dictionary<string, int> ValidCoins)
        {
            List<Coin> ClientCoins = new List<Coin>();
            foreach (KeyValuePair<string, int> entry in ValidCoins)
            {
                ClientCoins.Add(new Coin() { Id = entry.Key, Quantity = 0 });

            }
            return ClientCoins;

        }

        private static List<Product> InitializeClientProducts(Dictionary<string, int> ValidProducts)
        {
            List<Product> ClientProducts = new List<Product>();

            foreach (KeyValuePair<string, int> entry in ValidProducts)
            {
                ClientProducts.Add(new Product() { Name = entry.Key, Quantity = 0 });
            }
            return ClientProducts;
        }



        private static List<Product> SetMachineProducts()
        {
            return new List<Product> {
                new Product() {Name = Constants.COKE_KEY, Quantity=5},
                new Product() {Name = Constants.PEPSI, Quantity=15},
                new Product() {Name = Constants.SODA, Quantity=3},
            };
        }


    }
}

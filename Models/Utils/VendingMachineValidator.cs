using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkMachine.Models.Utils
{
    public class VendingMachineValidator
    {
        private readonly VendingMachine VendingMachine;
        private Dictionary<int, string> ValidationError { get; set; }

        public bool ValidateEnoughMoneyToGiveChange (int MachineMoney, int ChangeToGive)
        {
            return MachineMoney >= ChangeToGive;
        }

        public bool ValidateEnoughMoneyToPerformPurchase(int AmountPurchase, int AmountInserted)
        {
            return AmountInserted >= AmountPurchase;

        }



        private bool ValidateEnoughProducts (Dictionary<string, int> ProductsToBuy, Dictionary<string, int> AvailableProducts)
        {
            bool EnoughProducts = true;
            for (int index = 0; index < ProductsToBuy.Count && EnoughProducts; index++)
            {
                var itemToBuy = ProductsToBuy.ElementAt(index);
                var itemKey = itemToBuy.Key;
                var itemValue = itemToBuy.Value;
                EnoughProducts = AvailableProducts.ContainsKey(itemKey) && AvailableProducts[itemKey] >= itemValue;
            }
            return EnoughProducts; 
        }

        public VendingMachineValidator(VendingMachine VendingMachine)
        {
            this.VendingMachine = VendingMachine;
            ValidationError = new Dictionary<int, string>();
        }


        private void PerformValidation()
        {
            if (!ValidateEnoughMoneyToGiveChange(VendingMachine.MachineTotalMoney, VendingMachine.Change))
            {
                ValidationError.Add(Constants.NOT_ENOUGH_CHANGE_ERROR_KEY, Constants.NOT_ENOUGH_CHANGE_ERROR_VALUE);
            }

            if (!ValidateEnoughProducts(VendingMachineUtils.ProductListToDictionary(VendingMachine.ClientProducts), VendingMachineUtils.ProductListToDictionary(VendingMachine.InventoryProducts)))
            {
                ValidationError.Add(Constants.NOT_ENOUGH_PRODUCT_ERROR_KEY, Constants.NOT_ENOUGH_PRODUCT_ERROR_VALUE);
            }

            if (!ValidateEnoughMoneyToPerformPurchase(VendingMachine.CostOfPurchase,VendingMachine.AmountPaidByClient ))
            {
                ValidationError.Add(Constants.NOT_ENOUGH_MONEY_ERROR_KEY, Constants.NOT_ENOUGH_MONEY_ERROR_VALUE);
            }
        }


        public Dictionary<int, string> GetValidationDic()
        {
            PerformValidation();
            return ValidationError;
        }




    }
}

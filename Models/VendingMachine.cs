using DrinkMachine.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkMachine.Models
{
    public class VendingMachine
    {
        public List<Product> InventoryProducts { get; set; }
        public List<Coin> MachineCoins { get; set; }

        private Dictionary<string, int> AcceptedMoneyAndValue;
        private Dictionary<string, int> PriceProducts;

        public List<Coin> ClientCoins { get; set; }
        public List<Product> ClientProducts { get; set; }

        public int CostOfPurchase { 
            get =>ClientProducts.Sum(item => item.Quantity * item.Price);
            set => CostOfPurchase = value;
        }
        public int AmountPaidByClient {
            get => ClientCoins.Sum(item => item.Value * item.Quantity);
            set => AmountPaidByClient = value;
        }
        public int Change {
            get => AmountPaidByClient - CostOfPurchase;
            set => Change = value;
        }

        public int MachineTotalMoney
        {
            get => MachineCoins.Sum(item => item.Quantity * item.Value);
            
        }

        public  Dictionary<int, string> ValidationMessages { get; set; }

        public string ResultMessage { get; set; }

        public VendingMachine()
        {
            VendingMachineUtils.InitializeMachine(this);
            VendingMachineUtils.InitializeClientObjects(this, getExistingProducts(), getValidMoney() );            
        }

       

        private Dictionary<string, int> getExistingProducts()
        {
            if (PriceProducts == null || AcceptedMoneyAndValue.Count == 0)
            {
                PriceProducts = new Product().getPriceProducts();
            }
            return PriceProducts;
        }

        private Dictionary<string, int> getValidMoney()
        {
            if (AcceptedMoneyAndValue == null || AcceptedMoneyAndValue.Count == 0)
            {
                AcceptedMoneyAndValue = new Coin().GetAcceptedMoney();
            }

            return AcceptedMoneyAndValue;


        }

      
        public bool PerformValidations()
        {
            VendingMachineValidator Validator = new VendingMachineValidator(this);
            ValidationMessages = Validator.GetValidationDic();


            return ValidationMessages.Count == 0;
        }

        public void GenerateErrorMessages()
        {
            StringBuilder Message = new StringBuilder();

            foreach (KeyValuePair<int, string> entry in ValidationMessages)
            {
                Message.Append(entry.Value);

            }
            
            ResultMessage =  Message.ToString();
        }


        public void PerformPurchase()
        {
            VendingMachineUtils.ProcessPurchase(this);
            

        }

      public void ReturnProductsAndChange()
        {
            Dictionary <string, int> ReturningChange = VendingMachineUtils.CalculateChange(Change, MachineCoins);
            StringBuilder Message = new StringBuilder();
            Message.Append("You bought ");

            foreach (Product p in ClientProducts)
            {
                if (p.Quantity > 0)
                {
                    Message.Append(p.Quantity + " " + p.Name + "(s) ");
                }
            }

            if (Change > 0)
            {
                Message.Append("Your change is: ");
                foreach (KeyValuePair<string, int> entry in ReturningChange)
                {
                    Message.Append(entry.Value +" " + entry.Key + "(s) ");

                }
            }

            ResultMessage = Message.ToString();


        } 
    }

}

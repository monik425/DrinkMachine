using DrinkMachine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DrinkMachine.Controllers
{
    public class VendingMachineController : Controller
    {
       
        public ActionResult Index()
        {
           
            
            return View(new VendingMachine());
        }


       

        [HttpPost]
        public ActionResult Index(VendingMachine Model)
        {
            try
            {

                
                bool isValid = Model.PerformValidations();
                if (isValid)
                {
                    Model.PerformPurchase();
                    Model.ReturnProductsAndChange();
                }
                else
                {
                    Model.GenerateErrorMessages();
                }

                ModelState.Clear();
                return View(Model);


            }
            catch
            {
                return View(new VendingMachine());

            }
        }
    }
}

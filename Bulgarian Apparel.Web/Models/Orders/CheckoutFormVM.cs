using Bulgarian_Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bulgarian_Apparel.Web.Models.Orders
{
    public class CheckoutFormVM
    {
        public double TotalPrice { get; set; }
        [Display(Name = "Ship my products to: ")]
        public string ShippingAddress { get; set; }
        public IList<PaymentType> PaymentType { get; set; }
        public string PaymentTypeId { get; set; }
        [Display(Name = "Prefered method of payment")]
        public string PreferedPayment { get; set; }
        [Display(Name = "Credit card number")]
        public string CreditCardNumber { get; set; }
        public string CCV { get; set; }
    }
}
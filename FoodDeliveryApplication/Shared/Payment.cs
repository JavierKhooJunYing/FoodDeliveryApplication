﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApplication.Shared
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}

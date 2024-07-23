using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.Models.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public string ShippingDateString => OrderHeader.ShippingDate.ToShortDateString();
        public string PaymentDueDateString => OrderHeader.PaymentDueDate.ToShortDateString();
        public string PaymentDateString => OrderHeader.PaymentDate.ToShortDateString();
    }
}

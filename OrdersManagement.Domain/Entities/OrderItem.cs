﻿using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Money TotalPrice { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}

using OrdersManagement.Domain.ValueObjects;

namespace OrdersManagement.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Address DeliveryAddress { get; set; }
        public OrderStatus OrderStatus { get; set; } // new property
        public Money TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
        }
        public void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            OrderItems.AddRange(orderItems);
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            OrderItems.Remove(orderItem);
        }

        public void UpdateDeliveryAddress(Address deliveryAddress)
        {
            DeliveryAddress = deliveryAddress;
        }

        public void Cancel()
        {
            if (OrderStatus != OrderStatus.Placed)
            {
                throw new InvalidOperationException("Only orders that are placed can be cancelled.");
            }

            OrderStatus = OrderStatus.Cancelled;
        }
    }

    public enum OrderStatus
    {
        Placed,
        Cancelled
    }
}

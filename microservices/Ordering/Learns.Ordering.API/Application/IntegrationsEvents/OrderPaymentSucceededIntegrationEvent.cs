namespace Learns.Ordering.API.Application.IntegrationsEvents
{
    public class OrderPaymentSucceededIntegrationEvent
    {
        public OrderPaymentSucceededIntegrationEvent(long orderId) => OrderId = orderId;
        public long OrderId { get; }
    }
}

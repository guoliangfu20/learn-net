namespace Learns.Ordering.API.Application.IntegrationsEvents
{
    /// <summary>
    /// 集成事件
    /// </summary>
    public class OrderCreatedIntegrationEvent
    {
        public OrderCreatedIntegrationEvent(long orderId) => OrderId = orderId;
        public long OrderId { get; }
    }
}

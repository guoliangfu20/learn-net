namespace Learns.API.Application.IntegrationsEvents
{
    public interface ISubscriberService
    {
        void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event);

        void OrderCreated(OrderCreatedIntegrationEvent @event);
    }
}

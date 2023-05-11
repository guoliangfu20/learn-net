using DotNetCore.CAP;
using MediatR;

namespace Learns.API.Application.IntegrationsEvents
{
    /// <summary>
    /// 订阅服务
    /// </summary>
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        IMediator _mediator;
        public SubscriberService(IMediator mediator)
        {
            _mediator = mediator;
        }


        [CapSubscribe("OrderPaymentSucceeded")]
        public void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event)
        {
            //Do SomeThing
        }

        [CapSubscribe("OrderCreated")]  // 通过标识属性，完成订阅
        public void OrderCreated(OrderCreatedIntegrationEvent @event)
        {


            //Do SomeThing
        }
    }
}

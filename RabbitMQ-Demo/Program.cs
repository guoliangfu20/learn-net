// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



ConnectionFactory connectionFactory = new ConnectionFactory()
{
    HostName = "127.0.0.1",
    UserName = "admin",
    Password = "123456",
    VirtualHost = "my_host",
};

//var connection = connectionFactory.CreateConnection();


// 定义一个消费者
/*
using (var connection = connectionFactory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "my_MQ_queue",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);


    // 消费者
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body;

        var message = Encoding.UTF8.GetString(body.ToArray());
        Console.WriteLine($"收到消息：{message}");
    };

    channel.BasicConsume(
        queue: "my_MQ_queue"
        , autoAck: true,
        consumer: consumer);

    Console.WriteLine("按回车推出！");
    Console.ReadLine();
}

*/

// 定义一个生产者

using (var connection = connectionFactory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "my_MQ_queue",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    Console.WriteLine("输入需要传输的消息，输入 q 退出");

    var message = Console.ReadLine();

    message = message ?? "";

    while (message != "q")
    {
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: "my_MQ_queue",
             basicProperties: null,
             body: body);
        Console.WriteLine($" 发送消息: {message}");
        message = Console.ReadLine();
    }
}
Console.WriteLine("按回车退出! ");
Console.ReadKey();
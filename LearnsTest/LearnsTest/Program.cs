// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System.Collections;

/*
Queue queue = new Queue();

// 入列
queue.Enqueue("order_1");
queue.Enqueue("order_2");
queue.Enqueue("order_3");


// 出列

try
{
    Console.WriteLine(queue.Dequeue());
    Console.WriteLine(queue.Dequeue());

    queue.Clear();

    Console.WriteLine(queue.Dequeue());
    Console.WriteLine(queue.Dequeue());
}
catch (Exception ex)
{
    Console.WriteLine($"exception ,msg:{ex.Message}, {ex.ToString()}");
}
*/


/*
Queue<Order> queues = new Queue<Order>();
queues.Enqueue(new Order() { orderId = "111" });
queues.Enqueue(new Order() { orderId = "111" });
queues.Enqueue(new Order() { orderId = "111" });
queues.Enqueue(new Order() { orderId = "2222" });
queues.Enqueue(new Order() { orderId = "33333" });

try
{

    var newq = queues.Distinct().ToList();
    Console.WriteLine(newq[0]);
    Console.WriteLine(newq[1]);
    Console.WriteLine(newq[2]);
    Console.WriteLine(newq[3]);

    Console.WriteLine(  "--------------");

    Console.WriteLine(queues.Dequeue().ToString());



    Console.WriteLine(queues.Dequeue().ToString());
    Console.WriteLine(queues.Dequeue().ToString());

}
catch (Exception ex)
{
    Console.WriteLine($" 出现错误了,error: {ex.Message}");
}
*/

Employee emp1 = new Employee();
emp1.name = "张三";
emp1.depart = "T1";
emp1.empAddress = new Address() { address = "北京市石景山" };


Employee emp2 = emp1.GetClone();
emp2.name = "李四";
emp2.empAddress.address = "美国芝加哥";

emp1.empAddress.address = "河南驻马店";

emp1.empAddress = new Address() { address = "云南大理" };


Console.ReadKey();



class Employee
{
    public string name { get; set; }
    public string depart { get; set; }
    public Address empAddress { get; set; }

    public Employee GetClone()
    {
        return (Employee)this.MemberwiseClone();
    }
}

class Address
{
    public string address { get; set; }
}




class Order
{
    public string orderId { get; set; }

    public override string ToString()
    {
        Console.WriteLine($"orderid:{orderId}");
        return base.ToString();
    }
}

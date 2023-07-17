// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System;
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

/*
Employee emp1 = new Employee();
emp1.name = "张三";
emp1.depart = "T1";
emp1.empAddress = new Address() { address = "北京市石景山" };


Employee emp2 = emp1.GetClone();
emp2.name = "李四";
emp2.empAddress.address = "美国芝加哥";

emp1.empAddress.address = "河南驻马店";

emp1.empAddress = new Address() { address = "云南大理" };
*/

/*
Employee e = null;
Employee f = new Employee() { name = "zhanglang" };

Employee f2 = e ?? f;

var d = f?.name;
var g = e?.name;
Console.WriteLine(d + "--" + g);
*/

//int[] arr = { 4, 6, -2, 3, 1, 5 };
//Bubbling(arr);

//Console.WriteLine(Fibonacci(5));

Console.WriteLine("enter the nums: ");
string next = Console.ReadLine();
while (next != null)
{
    if (next.Equals("q")) break;

    int n = Convert.ToInt32(next);
    Console.WriteLine($"the Fibonacci is:{Fibonacci(n)}");
    next = Console.ReadLine();
}


void Bubbling(int[] arrs)
{
    if (arrs == null) return;

    for (int i = 0; i < arrs.Length - 1; i++)
    {
        for (int j = 0; j < arrs.Length - 1 - i; j++)
        {
            if (arrs[j] > arrs[j + 1])
            {
                int temp = arrs[j];
                arrs[j] = arrs[j + 1];
                arrs[j + 1] = temp;
            }
        }
    }

    for (int k = 0; k < arrs.Length; k++)
    {
        Console.WriteLine(arrs[k]);
    }
}

int Fibonacci(int num)
{
    // 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89…
    if (num <= 0) return 0;
    if (num == 1 || num == 2) return 1;

    return Fibonacci(num - 1) + Fibonacci(num - 2);
}

Console.ReadKey();

static bool IsInt(object obj)
{
    if (obj == null) return false;
    bool res = Int32.TryParse(obj.ToString(), out int nnn);
    return res;
}

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

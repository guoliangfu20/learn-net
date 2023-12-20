// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



List<Order> orders = new List<Order>
{
    new Order { Id = 1001, Name="aaaa"},
    new Order { Id = 1003, Name="bbbb"},
    new Order { Id = 1006, Name="cc"},
    new Order { Id = 1110, Name="f"},
};

List<string> fields = typeof(Order).GetProperties().Select(s => s.Name).ToList();

Console.WriteLine(string.Join(",",fields));

public class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
}




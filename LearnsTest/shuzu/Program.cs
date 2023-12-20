// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");



List<Person> persons = new List<Person>()
{
    new Person(){  id=1001,name="aaaa"},
    new Person(){  id=1002,name="bbb"},
    new Person(){  id=1003,name="ccc"},
    new Person(){  id=1004,name="ddd"},
};

List<int> ints = new List<int>() { 1001, 1003, 1005 };

string dels = "";
foreach (var item in ints)
{
    if (!persons.Exists(p => p.id == item))
    {
        dels += item;
    }

}


public class Person
{
    public int id { get; set; }
    public string name { get; set; }
    public int isOK { get; set; }
}



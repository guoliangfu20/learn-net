using System;

namespace DependencyInjectionAutofacDemo.Services
{
    public interface IMyServices
    {
        void ShowCode();
    }

    public class MyService1 : IMyServices
    {
        public void ShowCode()
        {
            Console.WriteLine($"MyService1#ShowCode:{GetHashCode()}");
        }
    }

    public class MyService2 : IMyServices
    {
        public MyNameService NameService { get; set; }

        public void ShowCode()
        {
            Console.WriteLine($"MyService2.ShowCode:{GetHashCode()},NameService是否为空：{NameService == null}");
        }
    }

    public class MyNameService
    {

    }

}

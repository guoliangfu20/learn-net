using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int total = 0;

            int[] nums = { 12, 25, 30 };

            int num = 12;

            //for (int i = 0; i < nums.Length; i++)
            //{
            //    // +10
            //    Task t1 = Task.Run(() => { nums[i] += 10; Task.Delay(1000).Wait(); });

            //    // -10
            //    Task t2 = Task.Run(() => { nums[i] -= 10; Task.Delay(2000).Wait(); });

            //    // *10
            //    Task t3 = Task.Run(() => { nums[i] *= 10; Task.Delay(3000).Wait(); });

            //    Task.WaitAll(t1, t2, t3);
            //    total += nums[i];
            //}

            // +10
            //Task t1 = Task.Run(() => { num += 10; Task.Delay(1000).Wait(); });

            //// -10
            //Task t2 = Task.Run(() => { num -= 10; Task.Delay(2000).Wait(); });

            //// *10
            //Task t3 = Task.Run(() => { num *= 10; Task.Delay(3000).Wait(); });

            //Task.WaitAll(t1, t2, t3);
            //total += num;


            Thread t1 = new Thread();






            Console.WriteLine($"最后结果：{total}");

            Console.WriteLine("Thread Finished!");
            Console.ReadKey();
        }

        static int Sum(int target)
        {
            target += 10;
            target -= 10;
            target *= 10;
            return target;
        }
    }
}

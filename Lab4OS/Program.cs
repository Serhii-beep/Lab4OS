using System;
using System.Threading.Tasks;

namespace Lab4OS
{
    public class Program
    {
        static object locker = new object();
        static int i1 = 0;
        static int i2 = 0;
        static void WithoutLock()
        {
            Task first = Task.Run(() => Increment1000WithoutLock());
            Task second = Task.Run(() => Increment1000WithoutLock());
            Task.WaitAll(first, second);
            Console.WriteLine($"Finished without critical segment, i = {i1}");
        }

        static void Increment1000WithoutLock()
        {
            for(int j = 0; j < 1000; ++j)
            {
                ++i1;
            }
        }
        static void WithLock()
        {
            Task first = Task.Run(() => Increment1000WithLock());
            Task second = Task.Run(() => Increment1000WithLock());
            Task.WaitAll(first, second);
            Console.WriteLine($"Finished with critical segment, i = {i2}");
        }
        static void Increment1000WithLock()
        {
            for(int j = 0; j < 1000; ++j)
            {
                lock(locker)
                {
                    ++i2;
                }
            }
        }

        static void Main(string[] args)
        {
            WithoutLock();
            WithLock();
        }
    }
}

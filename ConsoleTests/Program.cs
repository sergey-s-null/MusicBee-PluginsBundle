using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAsync();

            Console.WriteLine();
            Console.WriteLine("Press...");
            Console.ReadKey();
        }

        private static async void TestAsync()
        {
            Task t1 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("t1 done");
                throw new Exception("1");
            });
            Task t2 = Task.Run(() =>
            {
                Thread.Sleep(10000);
                Console.WriteLine("t2 done");
                throw new Exception("2");
            });

            try
            {
                await Task.WhenAll(t1, t2);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched in WaitAll: {e.Message}");
                Console.WriteLine(e);
            }

            try
            {
                await t1;
                await t2;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Catched in each wait: {e.Message}");
            }
        }

        private static bool GetAuthData(out string login, out string password)
        {
            login = "";
            password = "";
            return true;
        }

        private static bool InputCode(out string code)
        {
            Console.Write("Enter code? ");
            code = Console.ReadLine();
            return true;
        }
    }
}

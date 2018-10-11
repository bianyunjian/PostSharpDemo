using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Multithreading
{
    public class Multithreading
    {

        private static int _result = 0;

        [Background]
        public void Work(Action<object> action)
        {
            _result = 0;
            int max = 100;
            while (max > 0)
            {
                _result += max;
                max--;
                Thread.Sleep(100);
                Notify(action);
            }
        }

        [Dispatched]
        public void Notify(Action<object> action)
        {
            action.Invoke(_result);
        }

        private static object _lockA = new object();
        private static object _lockB = new object();

        public static void TestDeadlock()
        {

            var threadA = new Thread(() =>
            {
                lock (_lockA)
                {
                    Console.WriteLine("threadA get A");
                    Thread.Sleep(1000);
                    lock (_lockB)
                    {
                        Console.WriteLine("threadA get B");
                        Console.WriteLine("threadA do something");
                        Thread.Sleep(5000);
                        Console.WriteLine("threadA finished");


                    }
                    Console.WriteLine("threadA release B");
                }
                Console.WriteLine("threadA release A");
            });
            var threadB = new Thread(() =>
            {
                lock (_lockB)
                {
                    Console.WriteLine("threadB get B");
                    Thread.Sleep(1000);
                    lock (_lockA)
                    {
                        Console.WriteLine("threadB get A");
                        Console.WriteLine("threadB do something");
                        Thread.Sleep(5000);
                        Console.WriteLine("threadB finished");


                    }
                    Console.WriteLine("threadB release A");
                }
                Console.WriteLine("threadB release B");
            });

            threadA.Start();
            threadB.Start();
        }
    }
}

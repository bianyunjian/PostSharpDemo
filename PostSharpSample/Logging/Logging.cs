using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Logging
{
    public class Logging
    {
        public static void Test()
        {
            var log = new Logging();
            log.Add(1, 2);
            log.GetNowTime();

            //Parallel.Invoke(() =>
            //{
            //    log.AddDelay(1, 2, 2000);
            //}, () =>
            //{
            //    log.GetNowTime();
            //}, MultiMethods);

            //MultiMethods();

            log.DBOperation("update order");
        }


        public int Add(int a, int b)
        {
            return a + b;
        }
        public int AddDelay(int a, int b, int delayMillSeconds)
        {
            Thread.Sleep(delayMillSeconds);
            return a + b;
        }
        public string GetNowTime()
        {
            return DateTime.Now.ToString();
        }

        public void MultiMethods()
        {
            Add(2, 3);
            AddDelay(3, 4, 10);
            GetNowTime();
        }

        [Log(AttributeExclude = true)]
        [Audit]
        public void DBOperation(string update)
        { 
        }
    }
}

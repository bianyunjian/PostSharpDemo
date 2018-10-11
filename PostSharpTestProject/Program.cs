using PostSharpSample;
using PostSharpSample.Caching;
using PostSharpSample.Multithreading;
using PostSharpSample.Recordable;

using System;
using System.Windows.Forms;

namespace PostSharpTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Init();

            //Logging.Test();

            //Contracts.Test();

            //PropertyChange.Test();


            //RecordableTest.Test();

            //Application.Run(new Form1());
            //Multithreading.TestDeadlock();

            Caching.Test();

            Console.ReadKey();
        }


    }
}

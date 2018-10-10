using PostSharpSample;
using System;

namespace PostSharpTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Base.Init();

            //Logging.Test();

            //Contracts.Test();

            PropertyChange.Test();


            Console.ReadKey();
        }

      
    }
}

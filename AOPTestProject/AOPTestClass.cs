using AOP;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOPTestProject
{
    public class AOPTestClass : AOPBase
    {
        [AOPBeforeMethod(typeof(AOPTestClass), staticMethodName: "Before")]
        [AOPAfterMethod(typeof(AOPTestClass), staticMethodName: "After")]
        public int TestAdd(int a, int b)
        {
            Console.WriteLine($"a:{a},b:{b}");
            var result = a + b;
            Console.WriteLine($"result:{result}");

            return result;
        }

        public static void Before(ref int a, ref int b)
        {
            Console.WriteLine("Start ：" + a + "\t" + b);
            a = 200;
            b = 400;
        }

        public static void After(int result)
        {
            Console.WriteLine("End ：" + result);
        }


        [AOPBeforeMethod(typeof(AOPTestClass), staticMethodName: "BeforeUpdateParam")]
        [AOPAfterMethod(typeof(AOPTestClass), staticMethodName: "AfterUpdateParam")]
        public int TestUpdateParam(List<int> numberList)
        {
            foreach (var item in numberList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();

            var result = numberList.Sum();
            Console.WriteLine($"result:{result}");

            return result;
        }
        public static void BeforeUpdateParam(List<int> numberList)
        {
            Console.WriteLine("BeforeUpdateParam ：");
            foreach (var item in numberList)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();

            Console.WriteLine("UpdateParam ：");
            numberList.Add(9);
        }

        public static void AfterUpdateParam(int result)
        {
            Console.WriteLine("AfterUpdateParam ：" + result);
        }


        [AOPBeforeMethod(typeof(AOPTestClass), staticMethodName: "BeforeMultiCall")]
        [AOPAfterMethod(typeof(AOPTestClass), staticMethodName: "AfterMultiCall")]
        public void TestMultiCall()
        {
            var newInstance = (new AOPTestClass());
            newInstance.TestAdd(1, 2);
            newInstance.TestUpdateParam(new List<int>() { 3, 3, 3 });

        }
        public static void BeforeMultiCall()
        {
            Console.WriteLine("BeforeMultiCall");

        }

        public static void AfterMultiCall()
        {
            Console.WriteLine("AfterMultiCall");
        }
    }
}

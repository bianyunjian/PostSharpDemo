using PostSharp.Patterns.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Caching
{
    [CacheConfiguration(AbsoluteExpiration = 1)]
    public class Caching
    {
        public static void Test()
        {
            var instance = (new Caching());
            //instance.GetList();
            //instance.GetList();

            //instance.GetList(1);
            //instance.GetList(1);
            //instance.GetList(2);

            //Thread.Sleep(1000 * 65);

            //(new Caching()).GetList(1);

            var remoteService = new RemoteService();
            instance.GetCustomer(10001);
            instance.GetCustomer(10002);
            remoteService.GetRemoteCustomer(10001);
            remoteService.GetRemoteCustomer(10002);

            //导致缓存失效：GetCustomer   GetRemoteCustomer
            instance.UpdateCustomer(10001);

            instance.GetCustomer(10001);
            instance.GetCustomer(10002);
            remoteService.GetRemoteCustomer(10001);

            //instance.DeleteCustomer(10002);

        }



        private void DeleteCustomer(int customerID)
        {
            Console.WriteLine($">> Delete the customer {customerID} ");
            Thread.Sleep(1000);
        }

        [InvalidateCache(typeof(RemoteService), nameof(RemoteService.GetRemoteCustomer))]
        [InvalidateCache(nameof(GetCustomer))]
        private void UpdateCustomer(int customerID)
        {
            Console.WriteLine($">> Updating the customer {customerID} ");
            Thread.Sleep(1000);
        }



        [Cache]
        public List<int> GetList()
        {
            Console.WriteLine("GetList");
            return new List<int>() { 1, 2, 3 };
        }
        [Cache]
        public List<int> GetList(int param)
        {
            Console.WriteLine("GetList with param");
            return new List<int>() { param };
        }
        [Cache]
        public Customer GetCustomer(int customerID)
        {
            Console.WriteLine("GetCustomer with customerID=" + customerID);
            return new Customer()
            {
                CustomerID = customerID
            };

        }
    }

    public class SeachCustomerParam
    {
        public SeachCustomerParam()
        {
        }

        public int CustomerID { get; set; }
    }

    public class Customer
    {
        public Customer()
        {
        }

        public int CustomerID { get; set; }
    }

    [CacheConfiguration(AbsoluteExpiration = 5, IgnoreThisParameter = true)]
    public class RemoteService
    {
        [Cache]
        public Customer GetRemoteCustomer(int customerID)
        {
            Console.WriteLine("GetRemoteCustomer with customerID=" + customerID);
            return new Customer() { CustomerID = customerID };
        }
    }
}




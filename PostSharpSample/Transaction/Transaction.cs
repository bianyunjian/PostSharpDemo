using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Transaction
{
    public class Transaction
    {
        public static void Test()
        {
            try
            { 
                var orderService = new OrderService();

                orderService.CreateOrderAndPay(orderID: "D" + DateTime.Now.ToFileTimeUtc(), pay: 50);
                Thread.Sleep(1000);
                var orderInfo = orderService.GetOrderInfo(orderID: "D001");
                Console.WriteLine($"OrderID:{orderInfo.OrderID},Pay:{orderInfo.TotalPay }");

                orderService.CreateOrderAndPay(orderID: "D" + DateTime.Now.ToFileTimeUtc(), pay: 200);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    internal class OrderService
    {
        private string connectionString = "server=10.0.200.112;database=tempdb;user=sa;pwd=sa.";

        [RequiresTransaction]
        internal void CreateOrderAndPay(string orderID, int pay)
        {
            CreateOrder(orderID);
            Pay(orderID, pay);
        }
        internal void CreateOrder(string orderID)
        {
            var sql = $@"insert into OrderList (OrderID,CreatedTime) values('{orderID}',GETDATE())";

            ExecuteNonQuery(sql);
        }
        internal void Pay(string orderID, int pay)
        {
            if (pay >= 100)
            {
                throw new Exception("Pay should less than 100");
            }

            var sql = $@"insert into OrderPayRecord (OrderID, PayID, Pay, CreatedTime) values('{orderID}','{DateTime.Now.ToFileTimeUtc()}',{pay},GETDATE())";

            ExecuteNonQuery(sql);
        }

        internal OrderInfo GetOrderInfo(string orderID)
        {
            var sql = $@"select d.OrderID,d.CreatedTime,(select sum(p.pay) from OrderPayRecord p            where p.OrderID=d.OrderID)  as TotalPay from OrderList d
                        where d.OrderID = '{orderID}'";

            var dt = Query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];

                return new OrderInfo()
                {
                    OrderID = row["OrderID"].ToString(),
                    TotalPay = float.Parse(row["TotalPay"].ToString())
                };
            }

            return null;
        }

        public void ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable Query(string sql)
        {
            DataTable table = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(table);
                    adapter.Dispose();
                }
            }
            return table;
        }
    }

    internal class OrderInfo
    {
        public string OrderID { get; set; }
        public float TotalPay { get; set; }
    }
}

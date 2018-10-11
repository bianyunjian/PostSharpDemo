using PostSharp.Patterns.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpSample.Contracts
{
    public class Contracts
    {
        public static void Test()
        {
            SetName("123");
            SetProperty(new List<string>());
            SetProperty(null);
        }
        public static void SetName([Required][StringLength(minimumLength: 2, maximumLength: 5)]string userName)
        {
        }

        public static void SetProperty([NotNull] List<string> props)
        {
        }
    }
}

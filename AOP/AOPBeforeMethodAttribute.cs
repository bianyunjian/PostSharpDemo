using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    [AttributeUsage(AttributeTargets.Method )]
    public class AOPBeforeMethodAttribute : Attribute
    {
        public string FullClassName;
        public string StaticMethodName;
        public AOPBeforeMethodAttribute(Type classType, string staticMethodName)
        {
            FullClassName = classType.FullName;
            StaticMethodName = staticMethodName;
        }

    }
}

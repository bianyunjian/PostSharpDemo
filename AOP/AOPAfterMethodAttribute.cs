using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    [AttributeUsage(AttributeTargets.Method )]
    public class AOPAfterMethodAttribute : Attribute
    {
        public string FullClassName;
        public string StaticMethodName;
        public AOPAfterMethodAttribute(Type classType, string staticMethodName)
        {
            FullClassName = classType.FullName;
            StaticMethodName = staticMethodName;
        }
    }
}

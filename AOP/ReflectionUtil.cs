using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
  public static  class ReflectionUtil
    {
        public static T GetAttribute<T>(MethodInfo method) where T : Attribute
        {
            var attrs = method.GetCustomAttributes(typeof(T), false);
            if (attrs != null && attrs.Length > 0) {
                var attribute=attrs[0] as T;
                if (attribute != null) {
                    return attribute;
                }
            }
            return null;
        }
    }
}

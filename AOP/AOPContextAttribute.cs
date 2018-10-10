using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AOP
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AOPContextAttribute : ContextAttribute, IContributeObjectSink
    {
        public string AOPContextName { get; set; }
        public AOPContextAttribute(string name= "AOPContextAttribute") : base(name)
        {
            AOPContextName = name;
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new AOPHandler(nextSink);
        }
    }
}

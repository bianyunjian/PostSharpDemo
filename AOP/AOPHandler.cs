using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace AOP
{
    internal class AOPHandler : IMessageSink
    {
        private IMessageSink _nextSink;

        public AOPHandler(IMessageSink nextSink)
        {
            this._nextSink = nextSink;
        }

        public IMessageSink NextSink
        {
            get
            {
                return this._nextSink;
            }
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return this._nextSink.AsyncProcessMessage(msg, replySink);

        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMessage returnMessage = null;
            var callMessage = msg as IMethodCallMessage;
            if (callMessage != null)
            {

                var before = ReflectionUtil.GetAttribute<AOPBeforeMethodAttribute>(callMessage.MethodBase as MethodInfo);
                if (before != null)
                {
                    PreProceed(callMessage, before);
                }

                returnMessage = this._nextSink.SyncProcessMessage(callMessage);

                var after = ReflectionUtil.GetAttribute<AOPAfterMethodAttribute>(callMessage.MethodBase as MethodInfo);
                if (after != null)
                {
                    PostProceed(callMessage,returnMessage, after);
                }
            }
            else
            {
                returnMessage = this._nextSink.SyncProcessMessage(callMessage);
            }
            return returnMessage;
        }
        private void PreProceed(IMessage msg, AOPBeforeMethodAttribute before)
        {
            if (string.IsNullOrEmpty(before.FullClassName) || string.IsNullOrEmpty(before.StaticMethodName))
            {
                return;
            }

            var message = msg as IMethodCallMessage;
            var type = GetClassType(before.FullClassName);
            if (type == null || message == null)
            {
                throw new NullReferenceException("PreProceed GetClassType is null for [" + before.FullClassName + "]");
            }
            var param = message.Args;
            type.InvokeMember(before.StaticMethodName, BindingFlags.InvokeMethod, null, null, param);
        }
        private void PostProceed(IMessage callMessage,IMessage returnMessage, AOPAfterMethodAttribute after)
        {
            if (string.IsNullOrEmpty(after.FullClassName) || string.IsNullOrEmpty(after.StaticMethodName))
            {
                return;
            }

            var returnMsg = returnMessage as IMethodReturnMessage;
            var type = GetClassType(after.FullClassName);
            if (type == null || returnMsg == null)
            {
                throw new NullReferenceException("PostProceed GetClassType is null for [" + after.FullClassName + "]");
            }
            var returnValue = returnMsg.ReturnValue;
            type.InvokeMember(after.StaticMethodName, BindingFlags.InvokeMethod, null, null, new[] { returnValue });
        }

        private Type GetClassType(string fullClassName)
        {
            var type = Assembly.GetCallingAssembly().GetType(fullClassName);
            if (type != null) { return type; }

            var asmArray = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in asmArray)
            {
                var findType = asm.GetType(fullClassName);
                if (findType != null)
                {
                    return findType;
                }
            }

            return null;
        }
    }
}
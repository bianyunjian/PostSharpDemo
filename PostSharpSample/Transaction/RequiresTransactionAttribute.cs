using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using System.Transactions;

namespace PostSharpSample.Transaction
{
    [PostSharp.Serialization.PSerializable]
    public class RequiresTransactionAttribute : PostSharp.Aspects.OnMethodBoundaryAspect
    {

        public override void OnEntry(MethodExecutionArgs args)
        {
            var transactionScope = new TransactionScope(TransactionScopeOption.Required);
            args.MethodExecutionTag = transactionScope;

            base.OnEntry(args);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var transactionScope = args.MethodExecutionTag as TransactionScope;
            if (transactionScope != null)
            {
                transactionScope.Complete();
            }

            base.OnSuccess(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var transactionScope = args.MethodExecutionTag as TransactionScope;
            if (transactionScope != null)
            {
                transactionScope.Dispose();
            }
            base.OnExit(args);
        }

    }
}

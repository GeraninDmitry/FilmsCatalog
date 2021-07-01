using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BizLogic.GenericInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.BizRunners
{
    public class BizRunner<TIn, TOut>
    {
		readonly IBizAction<TIn, TOut> Action;
        readonly DbContext Context;

        public IImmutableList<ValidationResult> Errors => Action.Errors;
        public bool HasErrors => Action.HasErrors;

        public BizRunner(IBizAction<TIn, TOut> action, DbContext context)
        {
            Context = context;
            Action = action;
        }

        public TOut RunAction(TIn dataIn)
        {
            var result = Action.Action(dataIn);

            if (!HasErrors)
                Context.SaveChanges();

            return result;
        }
    }
}

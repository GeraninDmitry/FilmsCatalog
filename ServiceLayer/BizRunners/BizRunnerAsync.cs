using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using BizLogic.GenericInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.BizRunners
{
    public class BizRunnerAsync<TIn, TOut>
    {
        readonly IBizActionAsync<TIn, TOut> ActionClass;
        readonly DbContext Context;

        public IImmutableList<ValidationResult> Errors => ActionClass.Errors;
        public bool HasErrors => ActionClass.HasErrors;

        public BizRunnerAsync(IBizActionAsync<TIn, TOut> actionClass, DbContext context)
        {
            Context = context;
            ActionClass = actionClass;
        }

        public async Task<TOut> RunActionAsync(TIn dataIn)
        {
			var result = await ActionClass.ActionAsync(dataIn).ConfigureAwait(false);

            if (!HasErrors)
                await Context.SaveChangesAsync();

            return result;
        }
    }
}

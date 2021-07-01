using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLogic.GenericInterfaces;

namespace ServiceLayer.BizRunners
{
    public class SimpleBizRunner<TIn>
    {
        readonly ISimpleBizAction<TIn> m_Action;

        public IImmutableList<ValidationResult> Errors => m_Action.Errors;
        public bool HasErrors => m_Action.HasErrors;

        public SimpleBizRunner(ISimpleBizAction<TIn> action)
        {
            m_Action = action;
        }

        public void RunAction(TIn dataIn)
        {
            m_Action.Action(dataIn);
        }
    }
}

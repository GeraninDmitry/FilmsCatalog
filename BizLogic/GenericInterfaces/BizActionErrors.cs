using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BizLogic.GenericInterfaces
{
    public abstract class BizActionErrors
    {
        private readonly List<ValidationResult> _Errors = new List<ValidationResult>();

        public IImmutableList<ValidationResult> Errors => _Errors.ToImmutableList();

        public bool HasErrors => _Errors.Any();

        protected void AddError(string errorMessage, params string[] propertyNames)
        {
            _Errors.Add(new ValidationResult(errorMessage, propertyNames));
        }
    }
}

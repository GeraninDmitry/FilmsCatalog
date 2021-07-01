using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.GenericInterfaces
{
	public interface ISimpleBizAction<in TIn>
	{
		IImmutableList<ValidationResult> Errors { get; }
		bool HasErrors { get; }
		void Action(TIn film);
	}
}

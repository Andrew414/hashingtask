using System;
using System.Collections.Generic;
using System.Linq;
using DataStructs;
using FluentValidation;
using FluentValidation.Results;

namespace Implements.Static
{
    public static class ValidatorExtensions
    {
        public static string GetEnumErrorMessage<T>(this AbstractValidator<T> validator, Type @enum)
            where T : BaseModel => TypesExtensions.GetEnumProperties(@enum).AsString(", ");

        public static string AsString(this IList<ValidationFailure> errors) 
            => errors.Select(x => x.ErrorMessage).ToArray().AsString("\r\n");
    }
}

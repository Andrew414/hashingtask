using System;
using System.IO;
using DataStructs;
using DataStructs.Types;
using FluentValidation;
using Implements.Static;

namespace Implements.Validators
{
    public class HashOptionsValidator : AbstractValidator<HashOptions>
    {
        public HashOptionsValidator()
        {
            RuleFor(x => x.BlockSize)
                .InclusiveBetween((int) Math.Pow(2, 10), (int) Math.Pow(2, 20))
                .WithMessage("Block size should be between 2^10 and 2^20 values.");
            RuleFor(x => x.InputPath)
                .Must(File.Exists).WithMessage("Incorrect file path.");
            RuleFor(x => x.HashAlgorithm).IsInEnum()
                .WithMessage($"Incorrect hash algorithm. Supported algorithms: {this.GetEnumErrorMessage(typeof(HashAlgorithm))}.");
            RuleFor(x => x.ProgramMode).IsInEnum()
                .WithMessage($"Incorrect program mode. Supported modes: {this.GetEnumErrorMessage(typeof(ProgramMode))}.");
        }
    }
}

using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationValidator:AbstractValidator<Operation>
    {
        public OperationValidator()
        {
            RuleFor(o => o.YuklenenFormat).NotEmpty();
            RuleFor(o => o.DonusturulenFormat).NotEmpty();
            RuleFor(o => o.Foto).NotEmpty();

        }
    }
}

using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IDataResult<string> Run(params IDataResult<string>[] logics)
        {
            foreach (var logic in logics)
            {
                if (logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }
    }
}

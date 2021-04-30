using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOperationService
    {
        IDataResult<List<Operation>> GetAll();
        IDataResult<Operation> GetById(int Id);
        IDataResult<List<Operation>> GetAllByResponse(string response);
        IDataResult<string> Add(Operation operation);
        IResult Delete(Operation operation);
        IResult Update(Operation operation);
    }
}

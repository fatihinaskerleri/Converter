using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOperationService
    {
        List<Operation> GetAll();
        Operation GetById(int Id);
        List<Operation> GetAllByResponse(string response);
        void Add(Operation operation);
        void Delete(Operation operation); 
        void Update(Operation operation);
    }
}

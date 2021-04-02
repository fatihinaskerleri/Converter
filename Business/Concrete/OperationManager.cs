using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OperationManager : IOperationService
    {
        IOperationDal _operationDal;
        public OperationManager(IOperationDal operationDal)
        {
            _operationDal = operationDal;
        }
        public void Add(Operation operation)
        {
            _operationDal.Add(operation);
        }

        public void Delete(Operation operation)
        {
            _operationDal.Delete(operation);
        }

        public Operation Get()
        {
            throw new NotImplementedException();
        }

        public List<Operation> GetAll()
        {
            return _operationDal.GetAll();
        }

        public List<Operation> GetAllByResponse(string response)
        {
            return _operationDal.GetAll(o=>o.Response==response);
        }

        public void Update(Operation operation)
        {
            _operationDal.Update(operation);
        }
    }
}

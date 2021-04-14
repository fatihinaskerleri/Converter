using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
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

        [ValidationAspect(typeof(OperationValidator))]
        public IResult Add(Operation operation)
        {
           IResult result= BusinessRules.Run();

            if (result != null)
            {
                return result;
            }
            _operationDal.Add(operation);
            return new SuccessResult(Messages.OperationAdded);
        }

        public IResult Delete(Operation operation)
        {
            _operationDal.Delete(operation);
            return new SuccessResult(Messages.OperationDeleted);
        }

        public IDataResult<Operation> GetById(int Id)
        {
            return new SuccessDataResult<Operation>(_operationDal.Get(o => o.Id == Id), Messages.OperationListed);
        }

        public IDataResult<List<Operation>> GetAll()
        {
            return new SuccessDataResult<List<Operation>>(_operationDal.GetAll(), Messages.OperationListed);
        }

        public IDataResult<List<Operation>> GetAllByResponse(string response)
        {
            return new SuccessDataResult<List<Operation>>(_operationDal.GetAll(o => o.Response == response), Messages.OperationListed);
        }
         
        public IResult Update(Operation operation)
        {
            _operationDal.Update(operation);
            return new SuccessResult(Messages.OperationUpdated);
        }
    
    }
}

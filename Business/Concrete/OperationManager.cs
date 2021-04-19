using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
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
using System.Threading;

namespace Business.Concrete
{
    public class OperationManager : IOperationService
    {
        IOperationDal _operationDal;
        public OperationManager(IOperationDal operationDal)
        {
            _operationDal = operationDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(OperationValidator))]
        [PerformanceAspect(5)]
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

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IResult Delete(Operation operation)
        {
            _operationDal.Delete(operation);
            return new SuccessResult(Messages.OperationDeleted);
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IDataResult<Operation> GetById(int Id)
        {
            return new SuccessDataResult<Operation>(_operationDal.Get(o => o.Id == Id), Messages.OperationListed);
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<Operation>> GetAll()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Operation>>(_operationDal.GetAll(), Messages.OperationListed);
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<Operation>> GetAllByResponse(string response)
        {
            return new SuccessDataResult<List<Operation>>(_operationDal.GetAll(o => o.Response == response), Messages.OperationListed);
        }

        [SecuredOperation("admin")]
        [PerformanceAspect(5)]
        [ValidationAspect(typeof(OperationValidator))]
        public IResult Update(Operation operation)
        {
            _operationDal.Update(operation);
            return new SuccessResult(Messages.OperationUpdated);
        }
    
    }
}

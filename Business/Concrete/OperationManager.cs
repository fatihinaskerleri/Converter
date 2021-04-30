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
using System.Drawing;
using System.Drawing.Imaging;
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

      
        [ValidationAspect(typeof(OperationValidator))]
        [PerformanceAspect(5)]
        public IDataResult<string> Add(Operation operation)
        {
            IDataResult<string> result = BusinessRules.Run(Convert(operation.Foto));

            if (result != null)
            {
                return result;
            }
            _operationDal.Add(operation);
            return new SuccessDataResult<string>(Messages.OperationAdded);
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
        private IDataResult<string> Convert(string url)
        {
            System.Drawing.Image bmpImageToConvert = Image.FromFile(url);
            Image bmpNewImage = new Bitmap(bmpImageToConvert.Width,
                                           bmpImageToConvert.Height);
            Graphics gfxNewImage = Graphics.FromImage(bmpNewImage);
            gfxNewImage.DrawImage(bmpImageToConvert,
                                  new Rectangle(0, 0, bmpNewImage.Width,
                                                bmpNewImage.Height),
                                  0, 0,
                                  bmpImageToConvert.
                                  Width, bmpImageToConvert.Height,
                                  GraphicsUnit.Pixel);
            gfxNewImage.Dispose();
            bmpImageToConvert.Dispose();


            bmpNewImage.Save(@"C:\Users\sueda\Desktop\resim\flower.png", ImageFormat.Png);
            return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\flower.png");
        }
    }
}

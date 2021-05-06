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
using GroupDocs.Conversion;
using GroupDocs.Conversion.FileTypes;
using GroupDocs.Conversion.Options.Convert;
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
            IDataResult<string> result = BusinessRules.Run(Convert(operation.Foto,operation.DonusturulenFormat));

            if (result != null)
            {
                _operationDal.Add(operation);
                return new SuccessDataResult<string>(result.Data,result.Message); 
            }
            return new ErrorDataResult<string>(null, Messages.NotConvert);
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
        private IDataResult<string> Convert(string url, string donusturulecekTur)
        {

            using (Converter converter = new Converter(url))
            {
                if (donusturulecekTur == ImageFileType.Gif.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Gif
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.gif", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.gif", Messages.Convert);
                }

                if (donusturulecekTur == ImageFileType.Jp2.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Jp2
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.jp2", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.jp2", Messages.Convert);
                }

                if (donusturulecekTur == ImageFileType.Jpeg.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Jpeg
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.jpg", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.jpg", Messages.Convert);
                }

                if (donusturulecekTur == ImageFileType.Png.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Png
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.png", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.png", Messages.Convert);
                }

                if (donusturulecekTur == ImageFileType.Tiff.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Tiff
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.tiff", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.tiff", Messages.Convert);
                }

                if (donusturulecekTur == ImageFileType.Webp.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Webp
                    };
                    converter.Convert(@"C:\Users\sueda\Desktop\resim\a.webp", options);
                    return new SuccessDataResult<string>(@"C:\Users\sueda\Desktop\resim\a.webp", Messages.Convert);
                }
                return new ErrorDataResult<string>(Messages.NotConvert);
            }
           }
        }
}

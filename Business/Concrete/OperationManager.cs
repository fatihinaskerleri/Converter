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
        public string Server = "https://localhost:44331/";
        IOperationDal _operationDal;
        public OperationManager(IOperationDal operationDal)
        {
            _operationDal = operationDal;
        }

      
        [ValidationAspect(typeof(OperationValidator))]
        [PerformanceAspect(5)]
        public IDataResult<string> Add(Operation operation, string uniqueString)
        {
            
            IDataResult<string> result = BusinessRules.Run(Convert(operation.Foto,operation.DonusturulenFormat, uniqueString));

            if (result != null)
            {
                operation.Foto = uniqueString + "." + operation.DonusturulenFormat;
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

        //[SecuredOperation("admin")]
        [PerformanceAspect(5)]
        public IDataResult<List<Operation>> GetAll()
        {
            Thread.Sleep(500);
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
        private IDataResult<string> Convert(string url, string donusturulecekTur, string uniqueString)
        {
            donusturulecekTur = donusturulecekTur.ToLower();
            using (Converter converter = new Converter(url))
            {
                if (donusturulecekTur == ImageFileType.Gif.ToString())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Gif
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".gif", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".gif", Messages.Convert);
                }

                else if (donusturulecekTur == ImageFileType.Jp2.ToString().ToLower())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Jp2
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".jp2", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + "a.jp2", Messages.Convert);
                }

                else if (donusturulecekTur == "jpg")
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Jpeg
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".jpg", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".jpg", Messages.Convert);
                }

                else if (donusturulecekTur == "jpeg")
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Jpeg
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".jpeg", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".jpeg", Messages.Convert);
                }

                else if (donusturulecekTur == ImageFileType.Png.ToString().ToLower())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Png
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".png", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".png", Messages.Convert);
                }

                else if (donusturulecekTur == ImageFileType.Tiff.ToString().ToLower())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Tiff
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".tiff", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".tiff", Messages.Convert);
                }

                else if (donusturulecekTur == ImageFileType.Webp.ToString().ToLower())
                {
                    ImageConvertOptions options = new ImageConvertOptions
                    {
                        Format = ImageFileType.Webp
                    };
                    converter.Convert(@"wwwroot/converted/" + uniqueString + ".webp", options);
                    System.IO.File.Delete(url);
                    return new SuccessDataResult<string>(Server + "converted/" + uniqueString + ".webp", Messages.Convert);
                }
                return new ErrorDataResult<string>(Messages.NotConvert);
            }
           }
        }
}

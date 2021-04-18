using Core.Data_Access.Entity_Framework;
using DataAccess.Abstract;
using DataAccess.Concrete.Entity_Framework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.Entity_Framework
{
    public class EfOperationDal : EfEntityRepositoryBase<Operation,ConverterContext>,IOperationDal
    {
        
    }
}

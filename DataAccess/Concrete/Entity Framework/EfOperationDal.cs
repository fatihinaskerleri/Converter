using DataAccess.Abstract;
using DataAccess.Concrete.Entity_Framework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EF
{
    public class EfOperationDal : IOperationDal
    {
        public void Add(Operation entity)
        {
            using (ConverterContext context=new ConverterContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Operation entity)
        {
            using (ConverterContext context = new ConverterContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Operation Get(Expression<Func<Operation, bool>> filter)
        {
            using (ConverterContext context = new ConverterContext())
            {
                return context.Set<Operation>().SingleOrDefault(filter);
            }
        }


        public List<Operation> GetAll(Expression<Func<Operation, bool>> filter = null)
        {
            using (ConverterContext context = new ConverterContext())
            {
                return filter == null
                    ? context.Set<Operation>().ToList()
                    : context.Set<Operation>().Where(filter).ToList();
            }
        }
        public void Update(Operation entity)
        {
            using (ConverterContext context = new ConverterContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

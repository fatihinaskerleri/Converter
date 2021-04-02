using Business.Concrete;
using DataAccess.Concrete.EF;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            OperationManager operationManager = new OperationManager(new EfOperationDal());
            foreach (var operation in operationManager.GetAll())
            {
                Console.WriteLine(operation.YuklenenFormat);
            }
        }
    }
}

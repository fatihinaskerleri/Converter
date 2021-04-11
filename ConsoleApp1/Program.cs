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
            var result = operationManager.GetAll();
            if (result.Success)
            {
                foreach (var operation in result.Data)
                {
                    Console.WriteLine(operation.YuklenenFormat);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}

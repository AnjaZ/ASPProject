using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBRProject.Application.UseCases;

namespace TBRProject.Implementation.UseCases.UseCaseLogger
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(UseCaseLog log)
        {
            Console.WriteLine($"UseCase: {log.UseCaseName}, User: {log.User}, {log.ExecutionDateTime}, Authorized: {log.IsAuthorized}");
            Console.WriteLine($"Use Case Data: " + log.Data);
        }
    }
}

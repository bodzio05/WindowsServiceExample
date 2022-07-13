using Microsoft.Extensions.Logging;
using System;

namespace WindowsServiceExample
{
    public class ExamplaryService : IExamplaryService
    {
        private readonly ILogger<ExamplaryService> _logger;

        public ExamplaryService(ILogger<ExamplaryService> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            Console.WriteLine("Service started.");
        }

        public void Stop()
        {
            Console.WriteLine("Service stopped.");
        }
    }
}
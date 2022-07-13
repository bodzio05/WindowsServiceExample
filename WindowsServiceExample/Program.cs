using System;
using Topshelf;
using Unity;
using Unity.Lifetime;
using Topshelf.Unity;
using Microsoft.Extensions.Logging;

namespace WindowsServiceExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IExamplaryService, ExamplaryService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppInsightsService, AppInsightsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventLogService, EventLogService>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<ILoggerFactory>(new LoggerFactory());
            container.RegisterSingleton(typeof(ILogger<>), typeof(Logger<>));
            
            TopshelfExitCode exitCode = HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.UseUnityContainer(container);

                hostConfigurator.Service<IExamplaryService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsingUnityContainer();
                    serviceConfigurator.WhenStarted(service => service.Start());
                    serviceConfigurator.WhenStopped(service => service.Stop());
                });

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.StartAutomatically();

                hostConfigurator.SetServiceName("ExamplaryService");
                hostConfigurator.SetDisplayName("Examplary Service");
                hostConfigurator.SetDescription("Description of Examplary Service.");
                hostConfigurator.EnableShutdown();
                hostConfigurator.EnablePauseAndContinue();
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
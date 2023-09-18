using MassTransit;

using Quartz;

namespace TestMassTransit
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddMassTransitService(this IServiceCollection services)
		{
			services.AddMassTransit(x =>
			{
				Uri schedulerEndpoint = new Uri("queue:scheduler");

				x.AddMessageScheduler(schedulerEndpoint);

				x.UsingRabbitMq((context, cfg) =>
				{					
					cfg.UseMessageScheduler(schedulerEndpoint);

					cfg.ConfigureEndpoints(context);
				});
			});

			return services;
		}

		public static IServiceCollection AddWorkerServices(this IServiceCollection services)
		{
			services.AddQuartz(q =>
			{
				//q.SchedulerName = "MassTransit-Scheduler";
				//q.SchedulerId = "AUTO";

				q.UseMicrosoftDependencyInjectionJobFactory();

				//q.UseDefaultThreadPool(tp =>
				//{
				//	tp.MaxConcurrency = 10;
				//});

				//q.UseTimeZoneConverter();

				//q.UsePersistentStore(s =>
				//{
				//	s.UseProperties = true;
				//	s.RetryInterval = TimeSpan.FromSeconds(15);

				//	s.UseSqlServer("Server=tcp:localhost;Database=quartznet;Persist Security Info=False;User ID=sa;Password=Quartz!DockerP4ss;Encrypt=False;TrustServerCertificate=True;");

				//	s.UseJsonSerializer();

				//	s.UseClustering(c =>
				//	{
				//		c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
				//		c.CheckinInterval = TimeSpan.FromSeconds(10);
				//	});
				//});
			});

			services.AddMassTransit(x =>
			{
				x.AddPublishMessageScheduler();
				
				x.AddQuartzConsumers();

				x.AddConsumer<SendSmsCommandHandler>();

				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.UsePublishMessageScheduler();

					cfg.ConfigureEndpoints(context);
				});
			});

			services.Configure<MassTransitHostOptions>(options =>
			{
				options.WaitUntilStarted = true;
			});

			services.AddQuartzHostedService(options =>
			{
				options.StartDelay = TimeSpan.FromSeconds(5);
				options.WaitForJobsToComplete = true;
			});

			//services.AddHostedService<SuperWorker>();

			return services;
		}
	}
}

using System;
using Configgy.Cache;
using Configgy.Coercion;
using Configgy.Source;
using Configgy.Transformation;
using Configgy.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configgy.ConsoleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var configuration = InitConfig(args);
			var services = InitServices();
			AddConfiguration(services, configuration, args);


			var provider = services.BuildServiceProvider();


			var service = provider.GetRequiredService<MyService>();
			service.PrintConfigs();


			if (Environment.UserInteractive)
			{
				Console.WriteLine("Press [ENTER] to continue");
				Console.ReadLine();
			}
		}


		static IConfigurationRoot InitConfig(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile($"appsettings.json", true)
				.AddEnvironmentVariables()
				.AddCommandLine(args);

			var configuration = builder.Build();
			return configuration;
		}


		static IServiceCollection InitServices()
		{
			var services = new ServiceCollection();
			AddConfiggy(services);

			services.AddTransient<MyService>();
			services.AddTransient<IMyConfig, MyConfig>();
			services.AddOptions<MyConfig>();
			return services;
		}

		static void AddConfiguration(IServiceCollection services, IConfigurationRoot configuration, string[] args)
		{
			services.AddSingleton<IConfiguration>(configuration);
			services.AddSingleton<IConfigurationRoot>(configuration);

			services.AddSingleton<IValueSource, AggregateSource>(
				(p) => new AggregateSource(
					new DashedCommandLineSource(args),
					new Configgy.Microsoft.Extensions.Configuration.Source.ConfigurationRootSource(configuration),
					//new EnvironmentVariableSource(),
					//new FileSource(),
					new DefaultValueAttributeSource()
				));
		}

		static void AddConfiggy(IServiceCollection services)
		{
			services.AddSingleton<IValueSource, AggregateSource>();
			services.AddSingleton<IValueCache, DictionaryCache>();
			services.AddSingleton<IValueTransformer, AggregateTransformer>();
			services.AddSingleton<IValueValidator, AggregateValidator>();
			services.AddSingleton<IValueCoercer, AggregateCoercer>();
		}
	}
}

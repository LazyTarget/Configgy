using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Configgy.ConsoleClient
{
	public class MyService
	{
		private readonly IMyConfig _config;
		private readonly IConfiguration _configuration;

		public MyService(IMyConfig config, IConfiguration configuration/*, IOptions<MyConfig> options*/)
		{
			_config = config;
			_configuration = configuration;
		}


		public void PrintConfigs()
		{
			try
			{
				Console.WriteLine($"DatabaseConnectionString: {_config?.DatabaseConnectionString}");
				Console.WriteLine($"MinThingCount: {_config?.MinThingCount}");
				Console.WriteLine($"MaxThingCount: {_config?.MaxThingCount}");
				Console.WriteLine($"WhenToShutdown: {_config?.WhenToShutdown}");
			}
			catch (Exception ex)
			{

			}
		}

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Configgy.Cache;
using Configgy.Coercion;
using Configgy.Source;
using Configgy.Transformation;
using Configgy.Validation;

namespace Configgy.ConsoleClient
{
	public class MyConfig : Config, IMyConfig
	{
		public MyConfig()
			: base()
		{

		}

		public MyConfig(IValueCache cache, IValueSource source, IValueTransformer transformer, IValueValidator validator, IValueCoercer coercer)
			: base(cache, source, transformer, validator, coercer)
		{

		}

		[DefaultValue(100)] //assign a default value.
		public int MaxThingCount { get { return Get<int>(); } }

		[DefaultValue("Server=server;Database=db;User Id=usr;Password=pwd;")] //assign a default value.
		public string DatabaseConnectionString { get { return Get<string>(); } }

		public DateTime WhenToShutdown { get { return Get<DateTime>(); } }

		//use expression bodied statements
		public int MinThingCount => Get<int>();
	}

	public interface IMyConfig
	{
		int MaxThingCount { get; }
		string DatabaseConnectionString { get; }
		DateTime WhenToShutdown { get; }
		int MinThingCount { get; }
	}
}

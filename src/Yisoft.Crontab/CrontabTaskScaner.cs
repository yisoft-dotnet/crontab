//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Yisoft.Crontab
{
	public class CrontabTaskScaner
	{
		private readonly Assembly _mainAssembly;
		private readonly List<string> _typePrefix;

		public CrontabTaskScaner(Assembly mainAssembly = null, List<string> typePrefix = null)
		{
			_mainAssembly = mainAssembly;
			_typePrefix = typePrefix;

			if (_mainAssembly == null) _mainAssembly = Assembly.GetEntryAssembly();
			if (_typePrefix == null) _typePrefix = new List<string>();
		}

		public ReadOnlyCollection<CrontabTask> ScanTasks()
		{
			var types = _LoadTypes();

			if (types == null) return null;

			var taskMethods = (from type in types
			                   from method in type.GetMethods()
				                   .Where(m => !m.IsGenericMethodDefinition && !m.IsGenericMethod && m.IsPublic)
			                   let parameters = method.GetParameters()
			                   where parameters == null || parameters.Length < 4
			                   let cronAttributes = method.GetCustomAttributes<CronAttribute>()
			                   from cronAttribute in cronAttributes
			                   select new CrontabTask(method, cronAttribute)).ToList();

			return taskMethods.AsReadOnly();
		}

		private IEnumerable<Type> _LoadTypes()
		{
			var assemblies = _mainAssembly?.GetReferencedAssemblies()
				.Where(x => (_typePrefix == null || _typePrefix.Count == 0) || _typePrefix.Any(t => x.Name.StartsWith(t)))
				.Select(Assembly.Load)
				.Select(x => x.GetTypes())
				.SelectMany(x => x)
				.ToList();

			if (assemblies == null) return null;

			assemblies.AddRange(_mainAssembly.GetTypes());

			return assemblies.Where(x => x.GetTypeInfo().IsClass);
		}
	}
}

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
using CrontabSample.ClassLibrary.Schedulers;
using Yisoft.Crontab;

namespace CrontabSample.ConsoleApp
{
	internal class Program
	{
		protected static void Main(string[] args)
		{
			var taskScanner = new CrontabTaskScaner();
			var tasks = taskScanner.ScanTasks();

			if (tasks != null)
			{
				foreach (var task in tasks)
				{
					Console.WriteLine($"{task.ClassType}, {task.Method.Name}, {task.Cron.Expression}, {task.Cron.Format}");
				}

				Console.WriteLine($"{tasks.Count}");
			}

			Console.ReadKey();

			var executor = new CrontabTaskExecutor(m =>
			{
				var classType = m.DeclaringType;

				return classType == typeof(TestScheduler) ? new TestScheduler() : null;
			});

			executor.AddTasks(tasks);

			executor.Run();

			while (true)
			{
				// exit when press 'Q'
				if (Console.ReadKey().Key == ConsoleKey.Q) break;

				executor.Stop();

				Console.ReadKey();

				executor.Run();
			}
		}
	}
}

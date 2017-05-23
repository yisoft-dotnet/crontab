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
using System.Diagnostics;
using Yisoft.Crontab;

namespace CrontabSample.ClassLibrary.Schedulers
{
	public class TestScheduler
	{
		[Cron("18/1 * * * * ? *", format: CronStringFormat.WithSecondsAndYears)]
		public static void Task1()
		{
			Debug.WriteLine($"Task..............1111_{DateTime.Now}");
		}

		[Cron("28/1 * * * * ? *", format: CronStringFormat.WithSecondsAndYears)]
		public static void Task2(DateTime time, CrontabTask task)
		{
			Debug.WriteLine($"Task..............2222_{time}_{task.Method.Name}");
		}

		[Cron("28/1 * * * * ? *", format: CronStringFormat.WithSecondsAndYears)]
		public static void Task3(DateTime time, CrontabTask task)
		{
			Debug.WriteLine($"Task..............3333_{time}_{task.Method.Name}");
		}

		[Cron("1-8 * * * * ? *", format: CronStringFormat.WithSecondsAndYears)]
		[Cron("48/1 * * * * ? *", format: CronStringFormat.WithSecondsAndYears)]
		public static void Task4(DateTime time, CrontabTask task, CrontabTaskExecutor taskExecutor)
		{
			Debug.WriteLine($"Task..............Cron_{time}_{task.Method.Name}_{taskExecutor.Tasks.Count}");
		}

		[Cron("0/1 * * * * *", 100, CronStringFormat.WithSeconds)]
		public static void DeferTask1()
		{
			Debug.WriteLine($"Task..............5555_{DateTime.Now}");
		}
	}
}

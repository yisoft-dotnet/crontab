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
using System.Reflection;

namespace Yisoft.Crontab
{
	public class CrontabTask
	{
		public CrontabTask(MethodInfo method, CronAttribute cron)
		{
			Method = method ?? throw new ArgumentNullException(nameof(method));
			Cron = cron ?? throw new ArgumentNullException(nameof(cron));

			ClassType = Method.DeclaringType;
			Schedule = CrontabSchedule.TryParse(cron.Expression, cron.Format);
			Parameters = Method.GetParameters();
		}

		public ParameterInfo[] Parameters { get; }

		public Type ClassType { get; }

		public MethodInfo Method { get; }

		public CronAttribute Cron { get; }

		public CrontabSchedule Schedule { get; }

		internal Action<DateTime> Action { get; set; }

		public CrontabTaskStatus Status { get; set; } = CrontabTaskStatus.Pending;

		public DateTime LastExecuteTime { get; set; } = DateTime.MinValue;
	}
}

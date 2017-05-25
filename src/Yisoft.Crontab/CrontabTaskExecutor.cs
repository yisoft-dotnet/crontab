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
using System.Reflection;
using System.Threading;

namespace Yisoft.Crontab
{
	public class CrontabTaskExecutor
	{
		private readonly List<CrontabTask> _cronTasks = new List<CrontabTask>();

		private readonly Timer _timer;
		private readonly Func<MethodInfo, object> _typeInstanceCreator;

		public CrontabTaskExecutor(Func<MethodInfo, object> typeInstanceCreator)
		{
			_typeInstanceCreator = typeInstanceCreator ?? throw new ArgumentNullException(nameof(typeInstanceCreator));

			_timer = new Timer(s =>
			{
				var now = DateTime.Now;

				foreach (var task in _cronTasks)
				{
					if (task.Cron.Defer > task.WaitingTime)
					{
						task.WaitingTime++;

						continue;
					}

					if (!task.Schedule.IsMatch(now)) continue;

					task.Action(now);
				}
			}, null, -1, -1);
		}

		public IReadOnlyList<CrontabTask> Tasks => _cronTasks.AsReadOnly();

		public void AddTasks(IEnumerable<CrontabTask> tasks)
		{
			if (tasks == null) throw new ArgumentNullException(nameof(tasks));

			foreach (var task in tasks) AddTask(task);
		}

		public void AddTask(CrontabTask task)
		{
			if (task == null) throw new ArgumentNullException(nameof(task));
			if (task.Schedule == null) throw new ArgumentException("argument \"task.Schedule\" is null", nameof(task.Schedule));

			if (task.Action == null)
			{
				var action = _CreateAction(task);

				task.Action = action;
			}

			_cronTasks.Add(task);
		}

		public void Run() { _timer.Change(0, 1000); }

		public void Stop() { _timer.Change(-1, -1); }

		private Action<DateTime> _CreateAction(CrontabTask task)
		{
			if (task == null) throw new ArgumentNullException(nameof(task));

			var typeInstance = _typeInstanceCreator(task.Method);

			return time => { _TryRun(time, task, typeInstance); };
		}

		private void _TryRun(DateTime time, CrontabTask task, object typeInstance)
		{
			var method = task.Method;

			task.LastExecuteTime = time;
			task.Status = CrontabTaskStatus.Running;

			try
			{
				switch (task.Parameters.Length)
				{
					case 0:
						method.Invoke(typeInstance, null);
						break;
					case 1:
						method.Invoke(typeInstance, new object[] {time});
						break;
					case 2:
						method.Invoke(typeInstance, new object[] {time, task});
						break;
					case 3:
						method.Invoke(typeInstance, new object[] {time, task, this});
						break;
					default:
						throw new ArgumentException("the number of task parameters is incorrect.");
				}
			}
			catch
			{
				task.Status = CrontabTaskStatus.Failing;
			}
		}
	}
}

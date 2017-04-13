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

namespace Yisoft.Crontab.Filters
{
	/// <summary>
	/// No specific value filter for day-of-week and day-of -month fields
	/// <remarks>
	/// http://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontrigger.html
	/// https://en.wikipedia.org/wiki/Cron#CRON_expression
	/// </remarks>
	/// </summary>
	public class BlankDayOfMonthOrWeekFilter : ICronFilter
	{
		public BlankDayOfMonthOrWeekFilter(CrontabFieldKind kind)
		{
			if (kind != CrontabFieldKind.DayOfWeek && kind != CrontabFieldKind.Day)
				throw new CrontabException("The <?> filter can only be used in the Day-of-Week or Day-of-Month fields.");

			Kind = kind;
		}

		public CrontabFieldKind Kind { get; }

		public bool IsMatch(DateTime value) { return true; }

		public int? Next(int value)
		{
			var max = Constants.MaximumDateTimeValues[Kind];
			if (Kind == CrontabFieldKind.Day
			    || Kind == CrontabFieldKind.Month
			    || Kind == CrontabFieldKind.DayOfWeek) throw new CrontabException("Cannot call Next for Day, Month or DayOfWeek types");

			var newValue = (int?) value + 1;

			if (newValue >= max) newValue = null;

			return newValue;
		}

		public int First()
		{
			if (Kind == CrontabFieldKind.Day
			    || Kind == CrontabFieldKind.Month
			    || Kind == CrontabFieldKind.DayOfWeek) throw new CrontabException("Cannot call First for Day, Month or DayOfWeek types");

			return 0;
		}

		public override string ToString() { return "?"; }
	}
}

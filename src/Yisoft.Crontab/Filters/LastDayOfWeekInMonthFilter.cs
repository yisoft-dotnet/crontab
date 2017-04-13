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
using Yisoft.Crontab.Extensions;

namespace Yisoft.Crontab.Filters
{
	/// <summary>
	/// Handles filtering for the last specified day of the week in the month
	/// </summary>
	public class LastDayOfWeekInMonthFilter : ICronFilter
	{
		private readonly DayOfWeek _dateTimeDayOfWeek;

		/// <summary>
		/// Constructs a new instance of LastDayOfWeekInMonthFilter
		/// </summary>
		/// <param name="dayOfWeek">The cron day of the week (0 = Sunday...7 = Saturday)</param>
		/// <param name="kind">The crontab field kind to associate with this filter</param>
		public LastDayOfWeekInMonthFilter(int dayOfWeek, CrontabFieldKind kind)
		{
			if (kind != CrontabFieldKind.DayOfWeek) throw new CrontabException(string.Format("<{0}L> can only be used in the Day of Week field.", dayOfWeek));

			DayOfWeek = dayOfWeek;
			_dateTimeDayOfWeek = dayOfWeek.ToDayOfWeek();
			Kind = kind;
		}

		public int DayOfWeek { get; }

		public CrontabFieldKind Kind { get; }

		/// <summary>
		///     Checks if the value is accepted by the filter
		/// </summary>
		/// <param name="value">The value to check</param>
		/// <returns>True if the value matches the condition, False if it does not match.</returns>
		public bool IsMatch(DateTime value)
		{
			return value.Day == _dateTimeDayOfWeek.LastDayOfMonth(value.Year, value.Month);
		}

		public override string ToString() { return string.Format("{0}L", DayOfWeek); }
	}
}

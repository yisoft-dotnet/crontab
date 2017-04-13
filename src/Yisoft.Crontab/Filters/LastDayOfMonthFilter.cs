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
	/// Handles filtering for the last day of the month
	/// </summary>
	public class LastDayOfMonthFilter : ICronFilter
	{
		public LastDayOfMonthFilter(CrontabFieldKind kind)
		{
			if (kind != CrontabFieldKind.Day) throw new CrontabException("The <L> filter can only be used with the Day field.");

			Kind = kind;
		}

		public CrontabFieldKind Kind { get; }

		/// <summary>
		/// Checks if the value is accepted by the filter
		/// </summary>
		/// <param name="value">The value to check</param>
		/// <returns>True if the value matches the condition, False if it does not match.</returns>
		public bool IsMatch(DateTime value)
		{
			return DateTime.DaysInMonth(value.Year, value.Month) == value.Day;
		}

		public override string ToString() { return "L"; }
	}
}

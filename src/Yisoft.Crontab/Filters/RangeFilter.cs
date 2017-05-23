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
using Yisoft.Crontab.Extensions;

namespace Yisoft.Crontab.Filters
{
	/// <summary>
	/// Handles filtering ranges (i.e. 1-5)
	/// </summary>
	public class RangeFilter : ICronFilter, ITimeFilter
	{
		private int? _firstCache;

		/// <summary>
		/// Constructs a new RangeFilter instance
		/// </summary>
		/// <param name="start">The start of the range</param>
		/// <param name="end">The end of the range</param>
		/// <param name="steps">The steps in the range</param>
		/// <param name="kind">The crontab field kind to associate with this filter</param>
		public RangeFilter(int start, int end, int? steps, CrontabFieldKind kind)
		{
			var maxValue = Constants.MaximumDateTimeValues[kind];

			if (start < 0 || start > maxValue)
				throw new CrontabException($"Start = {start} is out of bounds for <{Enum.GetName(typeof(CrontabFieldKind), kind)}> field");

			if (end < 0 || end > maxValue) throw new CrontabException($"End = {end} is out of bounds for <{Enum.GetName(typeof(CrontabFieldKind), kind)}> field");

			if (steps != null && (steps <= 0 || steps > maxValue))
				throw new CrontabException($"Steps = {steps} is out of bounds for <{Enum.GetName(typeof(CrontabFieldKind), kind)}> field");

			Start = start;
			End = end;
			Kind = kind;
			Steps = steps;

			var filters = new List<SpecificFilter>();

			for (var evalValue = Start; evalValue <= End; evalValue++) if (_IsMatch(evalValue)) filters.Add(new SpecificFilter(evalValue, Kind));

			SpecificFilters = filters;
		}

		public int Start { get; }

		public int End { get; }

		public int? Steps { get; }

		/// <summary>
		/// Returns a list of specific filters that represents this step filter.
		/// NOTE - This is only populated on construction, and should NOT be modified.
		/// </summary>
		public IEnumerable<SpecificFilter> SpecificFilters { get; }

		public CrontabFieldKind Kind { get; }

		/// <summary>
		/// Checks if the value is accepted by the filter
		/// </summary>
		/// <param name="value">The value to check</param>
		/// <returns>True if the value matches the condition, False if it does not match.</returns>
		public bool IsMatch(DateTime value)
		{
			int evalValue;

			switch (Kind)
			{
				case CrontabFieldKind.Second:
					evalValue = value.Second;
					break;
				case CrontabFieldKind.Minute:
					evalValue = value.Minute;
					break;
				case CrontabFieldKind.Hour:
					evalValue = value.Hour;
					break;
				case CrontabFieldKind.Day:
					evalValue = value.Day;
					break;
				case CrontabFieldKind.Month:
					evalValue = value.Month;
					break;
				case CrontabFieldKind.DayOfWeek:
					evalValue = value.DayOfWeek.ToCronDayOfWeek();
					break;
				case CrontabFieldKind.Year:
					evalValue = value.Year;
					break;
				default: throw new ArgumentOutOfRangeException(nameof(Kind), Kind, null);
			}

			return _IsMatch(evalValue);
		}

		public int? Next(int value)
		{
			if (Kind == CrontabFieldKind.Day
			    || Kind == CrontabFieldKind.Month
			    || Kind == CrontabFieldKind.DayOfWeek) throw new CrontabException("Cannot call Next for Day, Month or DayOfWeek types");

			var max = Constants.MaximumDateTimeValues[Kind];

			var newValue = (int?) value + 1;

			while (newValue < max && !_IsMatch(newValue.Value)) newValue++;

			if (newValue >= max) newValue = null;

			return newValue;
		}

		public int First()
		{
			if (_firstCache.HasValue) return _firstCache.Value;

			if (Kind == CrontabFieldKind.Day
			    || Kind == CrontabFieldKind.Month
			    || Kind == CrontabFieldKind.DayOfWeek) throw new CrontabException("Cannot call First for Day, Month or DayOfWeek types");

			var max = Constants.MaximumDateTimeValues[Kind];

			var newValue = 0;

			while (newValue < max && !_IsMatch(newValue)) newValue++;

			if (newValue > max) throw new CrontabException($"Next value for {ToString()} on field {Enum.GetName(typeof(CrontabFieldKind), Kind)} could not be found!");

			_firstCache = newValue;
			return newValue;
		}

		private bool _IsMatch(int evalValue) { return evalValue >= Start && evalValue <= End && (!Steps.HasValue || (evalValue - Start) % Steps == 0); }

		public override string ToString() { return Steps.HasValue ? string.Format("{0}-{1}/{2}", Start, End, Steps) : $"{Start}-{End}"; }
	}
}

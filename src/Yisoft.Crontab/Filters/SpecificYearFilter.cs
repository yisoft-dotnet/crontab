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

namespace Yisoft.Crontab.Filters
{
	/// <summary>
	///     Handles filtering for a specific value
	/// </summary>
	public class SpecificYearFilter : SpecificFilter
	{
		/// <summary>
		/// Constructs a new RangeFilter instance
		/// </summary>
		/// <param name="specificValue">The specific value you wish to match</param>
		/// <param name="kind">The crontab field kind to associate with this filter</param>
		public SpecificYearFilter(int specificValue, CrontabFieldKind kind) : base(specificValue, kind)
		{
		}

		public override int? Next(int value) { return value < SpecificValue ? (int?) SpecificValue : null; }
	}
}

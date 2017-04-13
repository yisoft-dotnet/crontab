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

namespace Yisoft.Crontab
{
	/// <summary>
	///     The cron string format to use during parsing
	/// </summary>
	public enum CronStringFormat
	{
		/// <summary>
		///     Defined as "MINUTES HOURS DAYS MONTHS DAYS-OF-WEEK"
		/// </summary>
		Default = 0,

		/// <summary>
		///     Defined as "MINUTES HOURS DAYS MONTHS DAYS-OF-WEEK YEARS"
		/// </summary>
		WithYears = 1,

		/// <summary>
		///     Defined as "SECONDS MINUTES HOURS DAYS MONTHS DAYS-OF-WEEK"
		/// </summary>
		WithSeconds = 2,

		/// <summary>
		///     Defined as "SECONDS MINUTES HOURS DAYS MONTHS DAYS-OF-WEEK YEARS"
		/// </summary>
		WithSecondsAndYears = 3
	}
}

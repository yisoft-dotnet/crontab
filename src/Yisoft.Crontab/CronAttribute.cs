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

namespace Yisoft.Crontab
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class CronAttribute : Attribute
	{
		public CronAttribute(string expression, CronStringFormat format = CronStringFormat.Default)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
			Format = format;
		}

		public string Expression { get; }

		public CronStringFormat Format { get; }
	}
}

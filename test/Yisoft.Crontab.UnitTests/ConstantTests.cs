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
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yisoft.Crontab.UnitTests
{
	// Why do we test constants?  To ensure dictionaries that
	// use them are updated as soon as a new value is added!
	[TestClass]
	public class ConstantTests
	{
		[TestMethod]
		public void VerifyConstants()
		{
			_ValidateExists<CronStringFormat>(Constants.ExpectedFieldCounts);
			_ValidateExists<CrontabFieldKind>(Constants.MaximumDateTimeValues);
			_ValidateExists<DayOfWeek>(Constants.CronDays);
		}

		private static void _ValidateExists<T>(IDictionary dictionary)
		{
			Assert.IsNotNull(dictionary);

			foreach (var value in Enum.GetValues(typeof(T))) Assert.IsTrue(dictionary.Contains(value), "Contains <{0}>", Enum.GetName(typeof(T), value));
		}
	}
}

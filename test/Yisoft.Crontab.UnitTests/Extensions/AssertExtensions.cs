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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yisoft.Crontab.UnitTests.Extensions
{
	public static class Assert2
	{
		public static void Throws<T>(Action methodToCall, string message = "", params object[] values) where T : Exception
		{
			var additionalInfo = string.Format(message, values);

			try
			{
				methodToCall();
			}
			catch (T)
			{
				return;
			}
			catch (Exception e)
			{
				throw new AssertFailedException(
					$"Expected exception '{typeof(T).Name}', but '{e.GetType().Name}' was thrown\n\n{e}.  {additionalInfo}");
			}

			Assert.Fail("Expected exception '{0}', but no exception was thrown.  {1}", typeof(T).Name, additionalInfo);
		}
	}
}

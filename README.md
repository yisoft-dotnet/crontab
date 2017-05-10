# Yisoft.Crontab #

[![Build status](https://ci.appveyor.com/api/projects/status/mgms413qy8s0y181?svg=true)](https://ci.appveyor.com/project/yiteam/crontab)
[![NuGet](https://img.shields.io/nuget/v/Yisoft.Crontab.svg?style=flat&label=nuget)](https://www.nuget.org/packages/Yisoft.Crontab/)

cron expression parser and executor for dotnet core.

> this project based on [NCrontab-Advanced](https://github.com/jcoutch/NCrontab-Advanced).

If you have any problems, make sure to file an issue here on Github.

# Crontab task executor #

## CronAttribute ##
This library support annotation method only. if you want create a crontab task, simply add the `CronAttribute` on some method.

We also provide some advanced features that you can get by adding some parameters to the method to get the information associated with the current task.

Here are some [samples](sample):
```csharp
public class TestScheduler
{
	[Cron("18/1 * * * * ? *", CronStringFormat.WithSecondsAndYears)]
	public static void Task1()
	{
		Debug.WriteLine($"Task..............1111_{DateTime.Now}");
	}

	[Cron("28/1 * * * * ? *", CronStringFormat.WithSecondsAndYears)]
	public static void Task2(DateTime time, CrontabTask task)
	{
		Debug.WriteLine($"Task..............2222_{time}_{task.Method.Name}");
	}

	[Cron("28/1 * * * * ? *", CronStringFormat.WithSecondsAndYears)]
	public static void Task3(DateTime time, CrontabTask task)
	{
		Debug.WriteLine($"Task..............3333_{time}_{task.Method.Name}");
	}

	[Cron("1-8 * * * * ? *", CronStringFormat.WithSecondsAndYears)]
	[Cron("48/1 * * * * ? *", CronStringFormat.WithSecondsAndYears)]
	public static void Task4(DateTime time, CrontabTask task, CrontabTaskExecutor taskExecutor)
	{
		Debug.WriteLine($"Task..............Cron_{time}_{task.Method.Name}_{taskExecutor.Tasks.Count}");
	}
}
```

## Constructor ##

The `CrontabTaskExecutor` class contains a constructor that with one parameter, the parameter is `Func<MethodInfo, object> typeInstanceCreator`. `typeInstanceCreator` used to create an object instance where the task method is definded. this will be very useful!

In console application, you can initialize an instance of an object with the `new` keyword or reflection. and in web application, you can use `DI`(`dependency injection`) directly.

For better use this library in your Web application, see the [Yisoft.AspNetCore.Crontab](https://github.com/yisoft-aspnet/crontab) project.

# Support for the following cron expressions #

```
Field name   | Allowed values  | Allowed special characters
------------------------------------------------------------
Minutes      | 0-59            | * , - /
Hours        | 0-23            | * , - /
Day of month | 1-31            | * , - / ? L W
Month        | 1-12 or JAN-DEC | * , - /
Day of week  | 0-6 or SUN-SAT  | * , - / ? L #
Year         | 0001–9999       | * , - /
```

## Related community projects
* [Yisoft.AspNetCore.Crontab](https://github.com/yisoft-aspnet/crontab)

# License
Released under the [Apache License](License.txt).
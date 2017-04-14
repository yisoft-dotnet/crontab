# Yisoft.Crontab #

[![Build status](https://ci.appveyor.com/api/projects/status/mgms413qy8s0y181?svg=true)](https://ci.appveyor.com/project/yiteam/crontab)

cron expression parser and executor for dotnet core.

> this project based on [NCrontab-Advanced](https://github.com/jcoutch/NCrontab-Advanced).

If you have any problems, make sure to file an issue here on Github.

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

# License
Released under the [Apache License](License.txt).
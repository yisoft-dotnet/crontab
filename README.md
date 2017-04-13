# Yisoft.Crontab #
cron expression parser for dotnet core.

> this project based on [NCrontab-Advanced](https://github.com/jcoutch/NCrontab-Advanced).

**If you have any problems, make sure to file an issue here on Github.**

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
# HolisticWare.Xamarin.Tools.BinaryLogger

HolisticWare.Xamarin.Tools.BinaryLogger

*   http://msbuildlog.com/

*   MSBuild.StructuredLogger

    *   https://www.nuget.org/packages/MSBuild.StructuredLogger

*   https://natemcmaster.com/blog/2017/07/05/msbuild-task-in-nuget/


```
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Microsoft.Build.Logging.StructuredLogger;
```


```
        string binLogFilePath = @"C:\temp\test.binlog";

        var binLogReader = new BinLogReader();
        foreach (var record in binLogReader.ReadRecords(binLogFilePath))
        {
            var buildEventArgs = record.Args;

            // print command lines of all tool tasks such as Csc
            if (buildEventArgs is TaskCommandLineEventArgs taskCommandLine)
            {
                Console.WriteLine(taskCommandLine.CommandLine);
            }
```

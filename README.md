# HolisticWare.Xamarin.Tools.BinaryLogger

HolisticWare.Xamarin.Tools.BinaryLogger

*   http://msbuildlog.com/

*   MSBuild.StructuredLogger

    *   https://www.nuget.org/packages/MSBuild.StructuredLogger


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

## Copy files from nuget into install project

*   https://github.com/NuGet/Home/issues/6743

*   https://stackoverflow.com/questions/33911368/use-msbuild-targets-to-copy-files-recursively-from-nuget-package

*   https://stackoverflow.com/questions/51924129/copy-files-from-nuget-package-to-output-directory-with-msbuild-in-csproj-and-do

*   https://blog.nuget.org/20160126/nuget-contentFiles-demystified.html

## MSBuild custom tasks and distribution/installation via nuget

*   https://natemcmaster.com/blog/2017/07/05/msbuild-task-in-nuget/

*   https://blog.rsuter.com/implement-custom-msbuild-tasks-and-distribute-them-via-nuget/

*   https://github.com/Microsoft/msbuild/issues/2514

*   https://github.com/microsoft/msbuild/issues/2529

## Steps

1.  package content (`Directory.Build.rsp`)

    [DONE]

2.  unpack content (`Directory.Build.rsp`) during installation

    [DONE]

3.  correct BuildAction

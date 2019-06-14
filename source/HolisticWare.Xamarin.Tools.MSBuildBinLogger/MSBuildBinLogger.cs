using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HolisticWare.Xamarin.Tools
{
    public class MSBuildBinLogger : Microsoft.Build.Utilities.Task
    {
        [Microsoft.Build.Framework.Required]
        public string IntermediateOutputPath
        {
            get;
            set;
        }

        [Microsoft.Build.Framework.Required]
        public string FileOutputMetadataNamespaces
        {
            get;
            set;
        }

        public override bool Execute()
        {
            Log.LogMessage($"   mc++");
            Log.LogMessage($"       IntermediateOutputPath          = {IntermediateOutputPath}");
            Log.LogMessage($"       FileOutputMetadataNamespaces    = {FileOutputMetadataNamespaces}");

            // enforcing proper correlation between Log errors and build results (success and/or failures)
            return !Log.HasLoggedErrors;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HolisticWare.Xamarin.Tools.Bindings.XamarinAndroid.ApiXmlSpitter
{
    public class PackagesToNamespacesSpitter : Microsoft.Build.Utilities.Task
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

        string file_api_xml = null;
        System.Xml.XmlDocument xmldoc = null;
        System.Xml.XmlNamespaceManager ns = null;

        //  namespace is required, otherwise NRE
        string xml_namespace_name = "ax"; // could be "apixml" but it is irrelevant, keeping it short
        //  namespace can be empty string if not used inside XML file
        string xml_namespace = string.Intern("");

        string xpath_expression_nodes_to_find =
                    $@"/apixml:assemblies/apixml:assembly/apixml:namespaces/apixml:namespace"
                    ;
        public override bool Execute()
        {
            Log.LogMessage($"       IntermediateOutputPath          = {IntermediateOutputPath}");
            Log.LogMessage($"       FileOutputMetadataNamespaces    = {FileOutputMetadataNamespaces}");

            file_api_xml = $"{IntermediateOutputPath}/api.xml";
            xmldoc = new System.Xml.XmlDocument();
            xmldoc.Load(file_api_xml);

            Log.LogMessage($"       api.xml loaded");

            ns = new System.Xml.XmlNamespaceManager(xmldoc.NameTable);
            //  null namespace, is the namespace with no URI. I.e. the namespace that this element is in:
            //  <a xmlns=""></a>
            ns.AddNamespace(xml_namespace_name, xml_namespace);

            packages = GetPackagenames().ToArray();
            string[] lines = Dump("packages", packages).ToArray();

            File.WriteAllLines("./holisticware-generated/Transforms/Metadata.Namespaces.new.xml", lines);

            // enforcing proper correlation between Log errors and build results (success and/or failures)
            return !Log.HasLoggedErrors;
        }

        string[] packages = null;

        protected IEnumerable<string> GetPackagenames()
        {
            xpath_expression_nodes_to_find = $@"/ax:api/ax:package";

            System.Xml.XmlNodeList node_list = xmldoc.SelectNodes(xpath_expression_nodes_to_find, ns);

            foreach (System.Xml.XmlNode node in node_list)
            {
                string package_name = node.Attributes["name"].Value;

                yield return package_name;
            }
        }

        private IEnumerable<string> Dump(string type, string[] items)
        {
            Log.LogMessage($" mc++ ");
            Log.LogMessage($"   {type}s");

            string replacement = null;

            foreach(string p in items)
            {
                Log.LogMessage($"       {type} = {p}");

                string xpath = $@"/api/package[@name='{p}']"; 
                replacement = metadata_pattern_attr.Replace($@"$XPATH", $@"{xpath}");
                replacement = replacement.Replace($@"$NAME", $@"managedName");
                replacement = replacement.Replace($@"$VALUE", $@"{p}.HeyDev.DotNettifyThis");

                Log.LogMessage($"       {replacement}");

                yield return replacement;
            }
        }

        string metadata_pattern_remove =
            @"
    <remove-node
        path=""$XPATH""
        >
    </remove-node>
             ";

        string metadata_pattern_add =
            @"
    <add-node
        path=""$XPATH""
        >
        $NODE
    </add-node>
             ";

        string metadata_pattern_attr =
            @"
    <attr
        path=""$XPATH""
        name=""$NAME""
        >
        $VALUE
    </attr>
             ";

        string metadata =
            @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<metadata>
    
    <!-- replacement -->
    
</metadata>
            ";
    }
}

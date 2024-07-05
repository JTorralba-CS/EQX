using RGiesecke.DllExport;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EQX
{
    public class EQX
    {
        //private static SLRClass _slrClass = (SLRClass)null;
        private static string _logName = "";

        [DllExport("VendorName", CallingConvention.Cdecl)]
        public static int VendorName([MarshalAs(UnmanagedType.LPWStr)] out string name)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MainClass.CurrentDomain_AssemblyResolve);
            //if (MainClass._slrClass != null)
            //{
            //    MainClass._slrClass.Close();
            //    MainClass._slrClass = (SLRClass)null;
            //}
            //MainClass._slrClass = new SLRClass();
            name = "EQX\0";
            return 0;
        }

        [DllExport("Authenticate", CallingConvention.Cdecl)]
        public static int Authenticate([MarshalAs(UnmanagedType.LPWStr)] string userName, [MarshalAs(UnmanagedType.LPWStr)] string password, [MarshalAs(UnmanagedType.LPWStr)] string miscData)
        {
            int num = 1;
            try
            {
                //if (MainClass._slrClass == null)
                //    MainClass._slrClass = new SLRClass();
                //num = MainClass._slrClass.LogonSLR(userName, password, miscData);
                //MainClass._slrClass.WriteToDebug(string.Format("Authenticate- username: {0} miscData: {1} result: {2}", (object)userName, (object)miscData, (object)num));
            }
            catch (Exception ex)
            {
                //MainClass._slrClass.WriteToDebug(string.Format("Authenticate Error: {0}", (object)ex.Message));
            }
            return num;
        }

        [DllExport("SearchFields", CallingConvention.Cdecl)]
        public static int SearchFields([MarshalAs(UnmanagedType.LPWStr)] out string fieldData)
        {
            int num = 1;
            fieldData = "";
            try
            {
                //if (MainClass._slrClass != null)
                //{
                //    fieldData = string.Format("<search_fields>{0}", (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>ChannelName</name>{1}<heading>Channel Name</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>AgentName</name>{1}<heading>Operator</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>NatureOfCall</name>{1}<heading>Discipline</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>Position</name>{1}<heading>Position</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>Station</name>{1}<heading>Station</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>IncidentNumber</name>{1}<heading>Incident Number</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>CallerNumber</name>{1}<heading>Caller Number</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("<field>{0}<name>CalledNumber</name>{1}<heading>Called Number</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //    fieldData += string.Format("</search_fields>{0}", (object)Environment.NewLine);
                //    num = 0;
                //    MainClass._slrClass.WriteToDebug(string.Format("SearchFields: {0} result: {1}", (object)fieldData, (object)num));
                //}
            }
            catch (Exception ex)
            {
                //MainClass._slrClass.WriteToDebug(string.Format("SearchFields Error: {0}", (object)ex.Message));
            }
            return num;
        }

        [DllExport("SearchAudio", CallingConvention.Cdecl)]
        public static int SearchAudio([MarshalAs(UnmanagedType.LPWStr)] string searchData, [MarshalAs(UnmanagedType.LPWStr)] out string searchResults)
        {
            searchResults = "";
            int num = 0;
            try
            {
                //if (MainClass._slrClass != null)
                //{
                //    MainClass._slrClass.WriteToDebug(string.Format("SearchAudio: {0}", (object)searchData));
                //    List<Recording> recordingList = MainClass._slrClass.SearchSLR(searchData);
                //    num = recordingList.Count;
                //    if (num != 0)
                //    {
                //        searchResults = string.Format("<search_results>{0}", (object)Environment.NewLine);
                //        foreach (Recording recording in recordingList)
                //            searchResults += recording.ToString();
                //        searchResults += string.Format("</search_results>{0}", (object)Environment.NewLine);
                //    }
                //    MainClass._slrClass.WriteToDebug(string.Format("SearchAudio found: {0} result: {1}", (object)searchResults, (object)num));
                //}
            }
            catch (Exception ex)
            {
                //MainClass._slrClass.WriteToDebug(string.Format("SearchAudio Error: {0}", (object)ex.Message));
            }
            return num;
        }

        [DllExport("PlayAudio", CallingConvention.Cdecl)]
        public static int PlayAudio([MarshalAs(UnmanagedType.LPWStr)] string AudioId, [MarshalAs(UnmanagedType.LPWStr)] string miscData, [MarshalAs(UnmanagedType.LPWStr)] out string url)
        {
            url = "";
            //if (MainClass._slrClass == null)
            //    return 1;
            //MainClass._slrClass.WriteToDebug(string.Format("PlayAudio: {0} {1}", (object)AudioId, (object)miscData));
            //url = MainClass._slrClass.PlayAudio(AudioId, miscData);
            return 0;
        }

        public static int Terminate()
        {
            int num = 0;
            try
            {
                //if (MainClass._slrClass != null)
                //{
                //    MainClass._slrClass.WriteToDebug(string.Format("Terminate result: {0}", (object)num));
                //    MainClass._slrClass.Close();
                //    MainClass._slrClass = (SLRClass)null;
                //}
            }
            catch (Exception ex)
            {
                //if (MainClass._slrClass != null)
                //    MainClass._slrClass.WriteToDebug(string.Format("SearchAudio Error: {0}", (object)ex.Message));
                num = 1;
            }
            return num;
        }

        [DllExport("FreeMemory", CallingConvention.Cdecl)]
        public static int FreeMemory([MarshalAs(UnmanagedType.LPWStr)] string memoryToFree) => 0;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("Newtonsoft.Json"))
            {
                foreach (string manifestResourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                {
                    if (manifestResourceName.EndsWith("Newtonsoft.Json.dll"))
                    {
                        Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResourceName);
                        byte[] numArray = new byte[manifestResourceStream.Length];
                        manifestResourceStream.Read(numArray, 0, numArray.Length);
                        return Assembly.Load(numArray);
                    }
                }
            }
            return (Assembly)null;
        }

        private static byte[] StreamToBytes(Stream input)
        {
            using (MemoryStream memoryStream = new MemoryStream(input.CanSeek ? (int)input.Length : 0))
            {
                byte[] buffer = new byte[4096];
                int count;
                do
                {
                    count = input.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, count);
                }
                while (count != 0);
                return memoryStream.ToArray();
            }
        }
    }
}

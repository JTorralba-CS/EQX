using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

using RGiesecke.DllExport;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EQX
{
    public class EQX
    {
        private static string _Log;
        private static string _URL;
        private static string _U;
        private static string _P;

        [DllExport("VendorName", CallingConvention.Cdecl)]
        public static int VendorName([MarshalAs(UnmanagedType.LPWStr)] out string VendorName)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            VendorName = string.Format("EQX", (object) Environment.NewLine);

            return 0;
        }

        [DllExport("Authenticate", CallingConvention.Cdecl)]
        public static int Authenticate([MarshalAs(UnmanagedType.LPWStr)] string U, [MarshalAs(UnmanagedType.LPWStr)] string P, [MarshalAs(UnmanagedType.LPWStr)] string MiscData)
        {
            int Code = 1;

            _U = U;
            _P = P;

            XmlDocument XMLDocument = new XmlDocument();
            XMLDocument.LoadXml(MiscData);

            foreach (XmlNode misc_data in XMLDocument.SelectNodes("misc_data"))
            {
                _URL = misc_data["uri"].InnerText;
                _Log = misc_data["logfilename"].InnerText;
            }

            try
            {
                WriteToDebug("AUTHENTICATE --------------------------------------------------");
                WriteToDebug(string.Format("_Log: {0}", (object) _Log));
                WriteToDebug(string.Format("_URL: {0}", (object) _URL));
                WriteToDebug(string.Format("_U: {0}", (object) _U));
                WriteToDebug(string.Format("_P: {0}", (object) _P));
                WriteToDebug("");

                string AuthenticateResponse = @"{'success':true,'reason':0}";

                AuthenticateResponse_Root AuthenticateResponse_DeJSON = JsonConvert.DeserializeObject<AuthenticateResponse_Root>(AuthenticateResponse);

                WriteToDebug("Success = " + AuthenticateResponse_DeJSON.Success.ToString());
                WriteToDebug("Reason = " + AuthenticateResponse_DeJSON.Reason.ToString());

                Code = 0;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("Authenticate Error: {0}", (object) ex.Message));
            }
            finally
            {
                WriteToDebug("");
            }

            return Code;
        }

        [DllExport("SearchFields", CallingConvention.Cdecl)]
        public static int SearchFields([MarshalAs(UnmanagedType.LPWStr)] out string FieldData)
        {
            int Code = 1;

            FieldData = "";

            try
            {
                WriteToDebug("SEARCHFIELDS --------------------------------------------------");
                string SearchFieldsResponse = @"{'success':true,'parameters': [{'description':'The name or address of the callee','displayName':'Destination Device','name':'destinationdevice'},{'description':'The account/name of the callee','displayName':'Destination User','name':'destinationuser'},{'description':'The name or address of a device','displayName':'Device','name':'device'},{'description':'The maximum duration of a call in milliseconds','displayName':'Maximum Duration','name':'durationMax'},{'description':'The minimum duration of a call in milliseconds','displayName':'Minimum Duration','name':'durationMin'},{'description':'The end of a time range','displayName':'End Time','name':'endtime'},{'description':'The name or address of the caller','displayName':'Source Device','name':'sourcedevice'},{'description':'The account/name of the caller','displayName':'Source User','name':'sourceuser'},{'description':'The beginning of a time range','displayName':'Start Time','name':'starttime'},{'description':'The account name of a user','displayName':'User','name':'user'}]}";

                SearchFieldsResponse_Root SearchFieldsResponse_DeJSON = JsonConvert.DeserializeObject<SearchFieldsResponse_Root>(SearchFieldsResponse);

                WriteToDebug("Success = " + SearchFieldsResponse_DeJSON.Success.ToString());

                var Item = SearchFieldsResponse_DeJSON.Parameters.Single(X => X.Name == "device");
                SearchFieldsResponse_DeJSON.Parameters.Remove(Item);

                Item = SearchFieldsResponse_DeJSON.Parameters.Single(X => X.Name == "durationMin");
                SearchFieldsResponse_DeJSON.Parameters.Remove(Item);

                Item = SearchFieldsResponse_DeJSON.Parameters.Single(X => X.Name == "endtime");
                SearchFieldsResponse_DeJSON.Parameters.Remove(Item);

                Item = SearchFieldsResponse_DeJSON.Parameters.Single(X => X.Name == "starttime");
                SearchFieldsResponse_DeJSON.Parameters.Remove(Item);

                Item = SearchFieldsResponse_DeJSON.Parameters.Single(X => X.Name == "user");
                SearchFieldsResponse_DeJSON.Parameters.Remove(Item);

                foreach (SearchFieldsResponse_Parameter Parameter in SearchFieldsResponse_DeJSON.Parameters)
                {
                    WriteToDebug(Parameter.DisplayName + " : " + Parameter.Name);
                }

                WriteToDebug("");

                XElement XML = new XElement("search_fields",
                    from Parameter in SearchFieldsResponse_DeJSON.Parameters
                    select new XElement("field", new XElement("heading", Parameter.DisplayName), new XElement("name", Parameter.Name))
                    );

                FieldData = XML.ToString(SaveOptions.DisableFormatting);

                WriteToDebug(FieldData);

                Code = 0;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchFields Error: {0}", (object) ex.Message));
            }
            finally
            {
                WriteToDebug("");
            }

            return Code;
        }

        [DllExport("SearchAudio", CallingConvention.Cdecl)]
        public static int SearchAudio([MarshalAs(UnmanagedType.LPWStr)] string SearchData, [MarshalAs(UnmanagedType.LPWStr)] out string SearchResults)
        {

            int Code = 0;

            SearchResults = "";

            try
            {
                WriteToDebug("SEARCHAUDIO ---------------------------------------------------");

                XmlDocument XMLDocument = new XmlDocument();
                XMLDocument.LoadXml(SearchData);
                
                foreach (XmlNode search_data in XMLDocument.SelectNodes("search_data"))
                {
                    string start_time = "";
                    string end_time = "";

                    start_time = search_data["start_time"].InnerText;
                    end_time = search_data["end_time"].InnerText;

                    WriteToDebug(string.Format("start_time: {0}", (object) start_time));
                    WriteToDebug(string.Format("end_time: {0}", (object) end_time));

                    XmlElement search_fields = (XmlElement)null;

                    search_fields = search_data["search_fields"];

                    if (search_fields != null)
                    {
                        foreach (XmlNode field in search_fields.ChildNodes)
                        {
                            string FieldName = field["name"].InnerText;
                            string FieldValue = field["value"].InnerText;

                            WriteToDebug(string.Format("{0}: {1}", (object) FieldName, (object) FieldValue));
                        }
                    }
                }

                WriteToDebug("");

                string SearchAudioResponse = @"{'success':true,'results':[{'dstAddr':'1128','dstName':'CSPD Phone','endTime':'2024-08-08 15:15:05.170','id':'2024080809124036330','len':145040,'srcAddr':'7197612536','srcName':'','staTime':'2024-08-08 15:12:40.130'},{'dstAddr':'1128','dstName':'CSPD Phone','endTime':'2024-08-08 15:16:30.450','id':'2024080809151060403','len':80080,'srcAddr':'7195114554','srcName':'','staTime':'2024-08-08 15:15:10.370'},{'dstAddr':'1128','dstName':'CSPD Phone','endTime':'2024-08-08 15:21:37.120','id':'2024080809175215032','len':224200,'srcAddr':'8644368579','srcName':'','staTime':'2024-08-08 15:17:52.920'},{'dstAddr':'1128','dstName':'CSPD Phone','endTime':'2024-08-08 15:33:27.820','id':'2024080809265976858','len':388280,'srcAddr':'7193132063','srcName':'','staTime':'2024-08-08 15:26:59.540'},{'dstAddr':'1128','dstName':'CSPD Phone','endTime':'2024-08-08 15:43:28.767','id':'2024080809403611357','len':171880,'srcAddr':'7196358306','srcName':'','staTime':'2024-08-08 15:40:36.887'}],'resultsTotal':5}";

                SearchAudioResponse_Root SearchAudioResponse_DeJSON = JsonConvert.DeserializeObject<SearchAudioResponse_Root>(SearchAudioResponse);

                WriteToDebug("Success = " + SearchAudioResponse_DeJSON.Success.ToString());

                foreach (SearchAudioResponse_Result Result in SearchAudioResponse_DeJSON.Results)
                {
                    WriteToDebug(Result.ID + " " + Result.StaTime + " " + Result.EndTime + " " + Result.DstName + " " + Result.DstAddr + " " + Result.SrcAddr);
                }

                WriteToDebug("ResultsTotal = " + SearchAudioResponse_DeJSON.ResultsTotal.ToString());

                WriteToDebug("");

                XElement XML = new XElement("search_results",
                    from Result in SearchAudioResponse_DeJSON.Results
                    select new XElement("audio",
                        new XElement("start_time", Result.StaTime),
                        new XElement("end_time", Result.EndTime),
                        new XElement("id", Result.ID),
                        new XElement("search_fields",
                            new XElement("field", new XElement("name", "destinationdevice"), new XElement("value", Result.DstAddr)),
                            new XElement("field", new XElement("name", "destinationuser"), new XElement("value", Result.DstName)),
                            new XElement("field", new XElement("name", "durationmax"), new XElement("value", Duration(Result.Len))),
                            new XElement("field", new XElement("name", "sourcedevice"), new XElement("value", Result.SrcAddr)),
                            new XElement("field", new XElement("name", "sourceuser"), new XElement("value", Result.SrcName))
                        )
                    )
                );

                SearchResults = XML.ToString(SaveOptions.DisableFormatting);
                WriteToDebug(SearchResults);

                Code = 1;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchAudio Error: {0}", (object) ex.Message));
            }
            finally
            {
                WriteToDebug("");
            }

            return Code;
        }

        [DllExport("PlayAudio", CallingConvention.Cdecl)]
        public static int PlayAudio([MarshalAs(UnmanagedType.LPWStr)] string ID, [MarshalAs(UnmanagedType.LPWStr)] string MiscData, [MarshalAs(UnmanagedType.LPWStr)] out string URL)
        {
            string StartPos = "";
            string WinHandle = "";

            string FilePath = ID.Length == 19 ? ID.Substring(0, 4) + "\\" + ID.Substring(4, 2) + "\\" + ID.Substring(6, 2) + "\\" + ID.Substring(8, 2) + "\\" + ID.Substring(10, 9) : "0000000000000000000";
            string FileExtension = _URL.ToUpper().Contains("RECORDER02") ? "WAV" : "MP4";
            string AudioFile = string.Format("{0}.{1}", (object) FilePath, (object)FileExtension);

            XmlDocument XMLDocument = new XmlDocument();
            XMLDocument.LoadXml(MiscData);

            foreach (XmlNode misc_data in XMLDocument.SelectNodes("misc_data"))
            {
                StartPos = misc_data["startpos"].InnerText;
                WinHandle = misc_data["winhandle"].InnerText;
            }

            WriteToDebug("PLAYAUDIO --------------------------------------------------"); 
            WriteToDebug(string.Format("ID: {0}", (object) ID));
            WriteToDebug(string.Format("StartPos: {0}", (object) StartPos));
            WriteToDebug(string.Format("WinHandle: {0}", (object) WinHandle));

            WriteToDebug("");

            string PlayAudioResponse = @"{'success':true,'size':279365,'url':'http://192.168.200.240/ViewPoint/getfile.ashx?fileid=" + @ID + "'}";

            PlayAudioResponse_Root PlayAudioResponse_DeJSON = JsonConvert.DeserializeObject<PlayAudioResponse_Root>(PlayAudioResponse);

            WriteToDebug("Success = " + PlayAudioResponse_DeJSON.Success.ToString());
            WriteToDebug("Size = " + PlayAudioResponse_DeJSON.Size.ToString());

            URL = PlayAudioResponse_DeJSON.URL;
            WriteToDebug("URL = " + URL);

            return 0;
        }

        [DllExport("Terminate", CallingConvention.Cdecl)]
        public static int Terminate()
        {
            int Code = 0;

            try
            {
                WriteToDebug("TERMINATE --------------------------------------------------");
                WriteToDebug(string.Format("Terminate result: {0}", (object) Code));
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchAudio Error: {0}", (object) ex.Message));
                Code = 1;
            }
            return Code;
        }

        [DllExport("FreeMemory", CallingConvention.Cdecl)]
        public static int FreeMemory([MarshalAs(UnmanagedType.LPWStr)] string MemoryToFree) => 0;

        private static byte[] StreamToBytes(Stream Input)
        {
            using (MemoryStream MemoryStream = new MemoryStream(Input.CanSeek ? (int)Input.Length : 0))
            {
                byte[] Buffer = new byte[4096];
                int Count;
                do
                {
                    Count = Input.Read(Buffer, 0, Buffer.Length);
                    MemoryStream.Write(Buffer, 0, Count);
                }
                while (Count != 0);
                return MemoryStream.ToArray();
            }
        }

        public static void WriteToDebug(string Data)
        {
            if (string.IsNullOrEmpty(_Log))
                return;

            using (StreamWriter streamWriter = System.IO.File.AppendText(_Log))
            {
                streamWriter.WriteLine(string.Format("{0} {1}", (object) DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff"), (object) Data));
                streamWriter.Close();
            }
        }

        public static void ListEmbeddedResourceNames()
        {
            WriteToDebug("RESOURCE ---------------------------------------------------");

            foreach (var Resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                WriteToDebug("Resource: " + Resource);

            WriteToDebug("");

        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EQX.EmbeddedAssemblies.Newtonsoft.Json.dll"))
            {
                var assemblyData = new Byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }

        public static string Duration(int MilliSeconds)
        {
            TimeSpan Time = TimeSpan.FromMilliseconds(MilliSeconds);
            return Time.Hours.ToString("D2") + ":" + Time.Minutes.ToString("D2") + ":" + Time.Seconds.ToString("D2");
        }
    }

    public class AuthenticateResponse_Root
    {
        public bool Success { get; set; }
        public int Reason { get; set; }
    }

    public class SearchFieldsResponse_Root
    {
        public bool Success { get; set; }
        public List<SearchFieldsResponse_Parameter> Parameters { get; set; }
    }

    public class SearchFieldsResponse_Parameter
    {
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
    }

    public class SearchAudioResponse_Root
    {
        public bool Success { get; set; }
        public List<SearchAudioResponse_Result> Results { get; set; }
        public int ResultsTotal { get; set; }
    }

    public class SearchAudioResponse_Result
    {
        public string DstAddr { get; set; }
        public string DstName { get; set; }
        public string EndTime { get; set; }
        public string ID { get; set; }
        public int Len { get; set; }
        public string SrcAddr { get; set; }
        public string SrcName { get; set; }
        public string StaTime { get; set; }
    }

    public class PlayAudioResponse_Root
    {
        public bool Success { get; set; }
        public int Size { get; set; }
        public string URL { get; set; }
    }
}

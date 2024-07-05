﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

using RGiesecke.DllExport;

namespace EQX
{
    public class EQX
    {
        private static string _Log = "";
        private static string _URL;
        private static string _U;
        private static string _P;

        [DllExport("VendorName", CallingConvention.Cdecl)]
        public static int VendorName([MarshalAs(UnmanagedType.LPWStr)] out string VendorName)
        {
            VendorName = string.Format("EQX", (object)Environment.NewLine);

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

            foreach (XmlNode selectNode in XMLDocument.SelectNodes("misc_data"))
            {
                _URL = selectNode["uri"].InnerText;

                XmlElement XMLElement = selectNode["logfilename"];
                if (XMLElement != null)
                {
                    _Log = XMLElement.InnerText;
                    using (System.IO.File.CreateText(_Log));
                }
            }

            try
            {
                WriteToDebug("HelloWorld");

                Code = 0;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("Authenticate Error: {0}", (object)ex.Message));
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
                FieldData = string.Format("<search_fields>{0}", (object)Environment.NewLine);

                FieldData += string.Format("<field>{0}<name>destinationdevice</name>{1}<heading>Destination Device</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>destinationuser</name>{1}<heading>Destination User</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>device</name>{1}<heading>Device</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>durationMax</name>{1}<heading>Duration Maximum</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>durationMin</name>{1}<heading>Duration Minimum</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //FieldData += string.Format("<field>{0}<name>endtime</name>{1}<heading>End Time</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>sourcedevice</name>{1}<heading>Source Device</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>sourceuser</name>{1}<heading>Source User</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                //FieldData += string.Format("<field>{0}<name>starttime</name>{1}<heading>Start Time</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);
                FieldData += string.Format("<field>{0}<name>user</name>{1}<heading>User</heading>{2}</field>{3}", (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine, (object)Environment.NewLine);

                FieldData += string.Format("</search_fields>{0}", (object)Environment.NewLine);

                Code = 0;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchFields Error: {0}", (object)ex.Message));
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
                string dstAddr = "1128";
                string dstName = "CSPD Phone";
                string endTime = "2024-06-25T15:58:57.303";
                string id = "2024062509570071852";
                string len = "116440";
                string srcAddr = "7195110413";
                string srcName = "";
                string staTime = "2024-06-25T15:57:00.863";

                SearchResults = string.Format("<search_results>{0}", (object)Environment.NewLine);

                string start_time = staTime;
                string end_time = endTime;

                string destinationdevice = dstAddr;
                string destinationuser = dstName;
                string device = "_device";
                string durationMax = "_durationMax";
                string durationMin = "_durationMin";
                string sourcedevice = srcAddr;
                string sourceuser = "_sourceuser";
                string user = "_user";

                string AudioData = "";

                AudioData = string.Format("<audio>{0}", (object)Environment.NewLine);

                AudioData += string.Format("<start_time>{0}</start_time>{1}", (object)start_time, (object)Environment.NewLine);
                AudioData += string.Format("<end_time>{0}</end_time>{1}", (object)end_time, (object)Environment.NewLine);

                AudioData += string.Format("<destinationdevice>{0}</destinationdevice>{1}", (object)destinationdevice, (object)Environment.NewLine);
                AudioData += string.Format("<destinationuser>{0}</destinationuser>{1}", (object)destinationuser, (object)Environment.NewLine);
                AudioData += string.Format("<device>{0}</device>{1}", (object)device, (object)Environment.NewLine);
                AudioData += string.Format("<durationMax>{0}</durationMax>{1}", (object)durationMax, (object)Environment.NewLine);
                AudioData += string.Format("<durationMin>{0}</durationMin>{1}", (object)durationMin, (object)Environment.NewLine);              
                AudioData += string.Format("<sourcedevice>{0}</sourcedevice>{1}", (object)sourcedevice, (object)Environment.NewLine);
                AudioData += string.Format("<sourceuser>{0}</sourceuser>{1}", (object)sourceuser, (object)Environment.NewLine);
                AudioData += string.Format("<user>{0}</user>{1}", (object)user, (object)Environment.NewLine);

                AudioData += string.Format("<id>{0}</id>{1}", (object)id, (object)Environment.NewLine);

                AudioData += string.Format("</audio>{0}", (object)Environment.NewLine);

                SearchResults += AudioData;

                SearchResults += string.Format("</search_results>{0}", (object)Environment.NewLine);
                Code = 1;
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchAudio Error: {0}", (object)ex.Message));
            }
            return Code;
        }

        [DllExport("PlayAudio", CallingConvention.Cdecl)]
        public static int PlayAudio([MarshalAs(UnmanagedType.LPWStr)] string ID, [MarshalAs(UnmanagedType.LPWStr)] string MiscData, [MarshalAs(UnmanagedType.LPWStr)] out string URL)
        {
            URL = string.Format("{0}/ViewPoint/getfile.ashx?fileid={1}{2}", (object)_URL, (object)ID, (object)Environment.NewLine);

            return 0;
        }

        public static int Terminate()
        {
            int Code = 0;

            try
            {
                WriteToDebug(string.Format("Terminate result: {0}", (object)Code));
            }
            catch (Exception ex)
            {
                WriteToDebug(string.Format("SearchAudio Error: {0}", (object)ex.Message));
                Code = 1;
            }
            return Code;
        }

        [DllExport("FreeMemory", CallingConvention.Cdecl)]
        public static int FreeMemory([MarshalAs(UnmanagedType.LPWStr)] string MemoryToFree) => 0;

        private static Assembly CurrentDomain_AssemblyResolve(object Sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("Newtonsoft.Json"))
            {
                foreach (string ManifestResourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                {
                    if (ManifestResourceName.EndsWith("Newtonsoft.Json.dll"))
                    {
                        Stream ManifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ManifestResourceName);
                        byte[] NumArray = new byte[ManifestResourceStream.Length];
                        ManifestResourceStream.Read(NumArray, 0, NumArray.Length);
                        return Assembly.Load(NumArray);
                    }
                }
            }
            return (Assembly)null;
        }

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
                streamWriter.WriteLine(string.Format("{0} {1}", (object)DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:fff"), (object)Data));
                streamWriter.Close();
            }
        }
    }
}

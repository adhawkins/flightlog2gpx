using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using SkyDemonUtils;
using System.IO;

namespace flightlog2gpx
{
    class FileProcessor: IDisposable
    {
        string m_SourceDir;
        string m_DestDir;
        Thread m_Thread;
        bool m_Run = false;
        FileList m_FileList;

        public FileProcessor(string SourceDir, string DestDir, FileList TheFileList)
        {
            m_SourceDir = SourceDir;
            m_DestDir = DestDir;
            m_FileList = TheFileList;

            m_Run = true;
            m_Thread = new Thread(new ThreadStart(ProcessFiles));
            m_Thread.Start();
        }

        public void Dispose()
        {
            m_Run = false;
            m_Thread.Join();
        }

        public void ProcessFiles()
        {
            while (m_Run)
            {
                Debug.Print("Processor thread checking");
                try
                {
                    string File = m_FileList.GetFile();
                    string SourceFile = m_SourceDir + "\\" + File;
                    string DestFile = m_DestDir + "\\" + File + ".gpx";

                    Debug.Print("Processing {0} into {1}", 
                        SourceFile,
                        DestFile);

                    FileInfo Source = new FileInfo(SourceFile);
                    FileInfo Dest = new FileInfo(DestFile);

                    if (Source.Exists && (!Dest.Exists || Source.LastWriteTime > Dest.LastWriteTime))
                    {
                        SkyDemonFlightLog FlightLog = new SkyDemonFlightLog(SourceFile);

                        GPXFile GPXFile = new GPXFile(DestFile);
                        GPXFile.Description = "Test GPX file";

                        foreach (SkyDemonFlightLog.FlightLogDataPoint DataPoint in FlightLog.DataPoints)
                        {
                            GPXFile.AddPoint(DataPoint.m_Latitude, DataPoint.m_Longitude, DataPoint.m_Elevation, DataPoint.m_Speed, DataPoint.m_Time);
                        }

                        GPXFile.WriteFile();
                    }
                }

                catch (InvalidOperationException)
                {
                    Debug.Print("No files for processing");
                }

                Thread.Sleep(100);
            }
        }
    }
}

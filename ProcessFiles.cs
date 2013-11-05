using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using SkyDemonUtils;

namespace flightlog2gpx
{
    class ProcessFiles: IDisposable
    {
        Queue<string> m_Queue = new Queue<string>();
        string m_Source;
        string m_Dest;
        Thread m_Thread;
        bool m_Run = false;
        FileSystemWatcher m_Watcher;
        AutoResetEvent m_Event = new AutoResetEvent(false);

        public ProcessFiles(string Source, string Dest)
        {
            m_Source = Source;
            m_Dest = Dest;

            m_Watcher = new FileSystemWatcher(Source, "*.flightlog");
            
            m_Watcher.Changed +=new FileSystemEventHandler(m_Watcher_Changed);
            m_Watcher.Created +=new FileSystemEventHandler(m_Watcher_Changed);
            m_Watcher.Renamed += new RenamedEventHandler(m_Watcher_Renamed);

            m_Watcher.EnableRaisingEvents = true;

            m_Run = true;
            m_Thread = new Thread(new ThreadStart(Process));
            m_Thread.Start();
        }

        public void Dispose()
        {
            m_Watcher.EnableRaisingEvents = false;

            m_Run = false;
            m_Event.Set();
            m_Thread.Join();
        }

        void m_Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Debug.Print("In Changed with {0}", e.FullPath);
            AddFile(e.FullPath);
        }

        void m_Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Debug.Print("In Renamed with {0}", e.FullPath);
            AddFile(e.FullPath);
        }

        private void Process()
        {
            foreach (string File in Directory.GetFiles(m_Source, "*.flightlog", SearchOption.TopDirectoryOnly))
            {
                if (!m_Run)
                    break;

                AddFile(File,false);
            }

            while (m_Run)
            {
                if (0 != m_Queue.Count)
                {
                    string File;

                    lock (m_Queue)
                    {
                        File = m_Queue.Dequeue();
                    }

                    string SourceFile = m_Source + "\\" + File;
                    string DestFile = m_Dest + "\\" + File + ".gpx";

                    Debug.Print("Processing {0} into {1}",
                        SourceFile,
                        DestFile);

                    SkyDemonFlightLog FlightLog = new SkyDemonFlightLog(SourceFile);

                    GPXFile GPXFile = new GPXFile(DestFile);
                    GPXFile.Description = "Test GPX file";

                    GPXFile.AddPoints(FlightLog.DataPoints);

                    GPXFile.WriteFile();
                }
                else
                {
                    Debug.Print("Nothing to do, waiting for event");
                    m_Event.WaitOne();
                    Debug.Print("Event signalled");
                }
            }
        }

        private void AddFile(string File, bool Force = true)
        {
            bool Add = Force;

            int index = File.IndexOf(m_Source);
            string StripFile = (index < 0)
                ? File
                : File.Remove(index, m_Source.Length);

            while (StripFile[0] == '\\')
                StripFile = StripFile.Remove(0, 1);

            if (!Add)
            {
                FileInfo Source = new FileInfo(File);

                string DestFile = m_Dest + "\\" + StripFile + ".gpx";

                FileInfo Dest = new FileInfo(DestFile);

                if (Source.Exists && (!Dest.Exists || Source.LastWriteTime > Dest.LastWriteTime))
                    Add = true;
            }

            if (Add)
            {

                lock (m_Queue)
                {
                    Debug.Print("Storing {0}", StripFile);
                    m_Queue.Enqueue(StripFile);
                    m_Event.Set();
                }
            }
        }

        public string GetFile()
        {
            lock (m_Queue)
            {
                return m_Queue.Dequeue();
            }
        }
    }
}

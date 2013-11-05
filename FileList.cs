using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace flightlog2gpx
{
    class FileList: IDisposable
    {
        Queue<string> m_Queue = new Queue<string>();
        string m_Directory;
        Thread m_Thread;
        bool m_Run = false;
        FileSystemWatcher m_Watcher;

        public FileList(string Directory)
        {
            m_Directory=Directory;
            m_Watcher = new FileSystemWatcher(Directory, "*.flightlog");
            
            m_Watcher.Changed +=new FileSystemEventHandler(m_Watcher_Changed);
            m_Watcher.Created +=new FileSystemEventHandler(m_Watcher_Changed);
            m_Watcher.Renamed += new RenamedEventHandler(m_Watcher_Renamed);

            m_Watcher.EnableRaisingEvents = true;

            m_Run = true;
            m_Thread = new Thread(new ThreadStart(ScanDirectory));
            m_Thread.Start();
        }

        public void Dispose()
        {
            m_Watcher.EnableRaisingEvents = false;

            m_Run = false;
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

        private void ScanDirectory()
        {
            foreach (string File in Directory.GetFiles(m_Directory, "*.flightlog", SearchOption.TopDirectoryOnly))
            {
                if (!m_Run)
                    break;

                AddFile(File);
            }
        }

        private void AddFile(string File)
        {
            int index = File.IndexOf(m_Directory);
            string StripFile = (index < 0)
                ? File
                : File.Remove(index, m_Directory.Length);

            while (StripFile[0] == '\\')
                StripFile = StripFile.Remove(0, 1);

            lock (m_Queue)
            {
                Debug.Print("Storing {0}", StripFile);
                if (!m_Queue.Contains(StripFile))
                {
                    m_Queue.Enqueue(StripFile);
                    Debug.Print("Stored {0}", StripFile);
                }
                else
                {
                    Debug.Print("{0} already exists", StripFile);
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

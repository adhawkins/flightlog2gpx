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
        bool m_Run=false;

        public FileList(string Directory)
        {
            m_Directory=Directory;

            m_Run=true;
            m_Thread=new Thread(new ThreadStart(ScanDirectory));
            m_Thread.Start();
        }

        public void Dispose()
        {
            m_Run=false;
            m_Thread.Join();
        }

        public void ScanDirectory()
        {
            foreach (string File in Directory.GetFiles(m_Directory))
            {
                if (!m_Run)
                    break;

                lock (m_Queue)
                {
                    Debug.Print("Storing {0}", File);
                    m_Queue.Enqueue(File);
                }

                Thread.Sleep(1000);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace flightlog2gpx
{
    class FileProcessor: IDisposable
    {
        string m_Directory;
        Thread m_Thread;
        bool m_Run=false;
        FileList m_FileList;

        public FileProcessor(string Directory, FileList TheFileList)
        {
            m_Directory=Directory;
            m_FileList=TheFileList;

            m_Run=true;
            m_Thread=new Thread(new ThreadStart(ProcessFiles));
            m_Thread.Start();
        }

        public void Dispose()
        {
            m_Run=false;
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
                    Debug.Print("Processing {0}", File);
                }

                catch (InvalidOperationException e)
                {
                    Debug.Print("No files for processing");
                }

                Thread.Sleep(750);
            }
        }
    }
}

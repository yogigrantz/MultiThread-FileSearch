using System;
using System.IO;
using System.Threading;

namespace DelegateAndEvents
{
    public delegate void DGGetResult(string[] output);

    public static class MTFileSearch
    {
        public static event DGGetResult OnMTFilesFound;

        public static void Search(string[] subfolders, string pattern)
        {
            foreach (string s in subfolders)
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine(string.Format("Folder: {0} =====", s));
                    STFileSearch stf = new STFileSearch();
                    stf.OnFilesFound += new DGGetResult(getEvent);
                    stf.Search(s, pattern);
                });
                thread.Start();
            }
        }

        private static void getEvent(string[] result)
        {
            OnMTFilesFound(result);
            return;
        }
    }

    //internal delegate void DGSubscribe(string[] output);

    internal class STFileSearch
    {
        internal event DGGetResult OnFilesFound;

        internal void Search(string dirName, string pattern)
        {
            string[] filenames = Directory.GetFiles(dirName, pattern);
            if (filenames.Length > 0)
            {
                OnFilesFound(filenames);
            }

            string[] subFolders = Directory.GetDirectories(dirName);
            foreach (string s in subFolders)
            {
                Search(s, pattern);
            }
        }
    }
}

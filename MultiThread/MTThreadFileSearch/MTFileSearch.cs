using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTThreadFileSearch
{
    public static class MTFileSearch
    {
        public static void Search(string pattern, string[] folders)
        {

            Adder a = new Adder();
            a.OnMultipleOfFiveReached += new Adder.dgEventRaiser(a_MultipleOfFiveReached);
            dgPointer pAdder = new dgPointer(a.Add);
            int iAnswer = pAdder(4, 3);

            foreach (string folder in folders)
            {
                FileSearch fs = new FileSearch(pattern);
                fs.Search(folder);
            }

        }

        static void a_MultipleOfFiveReached()
        {
            Console.WriteLine("Multiple of five reached!");
        }

        static string[] showFileFound(string[] filenames)
        {
            return filenames;
        }

    }

    delegate int dgPointer(int a, int b);

    delegate EventHandler FileFound(string[] fileNames);

    internal class Adder
    {
        public delegate void dgEventRaiser();
        public event dgEventRaiser OnMultipleOfFiveReached;


        internal int Add(int x, int y)
        {
            int iSum = x + y;
            if ((iSum % 5 == 0) && (OnMultipleOfFiveReached != null))
            {
                OnMultipleOfFiveReached();
            }
            return iSum;
        }
    }

    internal class FileSearch
    {
        private string _pattern;

        public event FileFound OnFileFound;

        internal FileSearch(string pattern)
        {
            _pattern = pattern;
            
        }


        internal void Search(string subFolder)
        {
            OnFileFound(Directory.GetFiles(subFolder, _pattern));

            string[] subFolderNames = Directory.GetDirectories(subFolder);

            foreach(string s in subFolderNames)
            {
                Search(s);
            }
            return;
        }

    }

}

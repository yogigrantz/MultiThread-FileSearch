using DelegateAndEvents;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.lstFolders.Items.Count > 0 && this.txtPattern.Text.Contains("*."))
            {
                List<string> folders = new List<string>();
                foreach (string s in this.lstFolders.Items)
                    folders.Add(s);

                MTFileSearch.OnMTFilesFound += new DGGetResult(getEventRunningInClient);
                MTFileSearch.Search(folders.ToArray(), this.txtPattern.Text);
            }
        }

        private void getEventRunningInClient(string[] result)
        {
            if (this.lstSearchResult.InvokeRequired)
            {
                DGGetResult g = new DGGetResult(PopulateListBox);
                this.Invoke(g, new object[] { result });
            }
            else
            {
                PopulateListBox(result);
            }
        }

        private void PopulateListBox(string[] result)
        {
            foreach (string s in result)
            {
                lstSearchResult.Items.Add(s);
                lstSearchResult.Refresh();
            }
        }

        private void btnOpenDirDialog_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                fb.ShowDialog();
                if (fb.SelectedPath != null && !this.lstFolders.Items.Contains(fb.SelectedPath))
                {
                    this.lstFolders.Items.Add(fb.SelectedPath);
                    this.btnSearch.Enabled = true;
                    this.lstSearchResult.Enabled = true;
                }
            }
        }
    }
}

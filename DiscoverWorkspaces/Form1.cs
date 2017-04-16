using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscoverWorkspaces
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public Dictionary<string, IWorkspace> Workspaces = new Dictionary<string, IWorkspace>();

        public void addWorkspace(string key)
        {
            comboBox1.Items.Add(key);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string g = comboBox1.SelectedItem.ToString();
          IWorkspace pWorkspace = Workspaces[g];

            IPropertySet ps = pWorkspace.ConnectionProperties;
            string workspacestring = ps.GetProperty("DATABASE").ToString();
            workspacestring = workspacestring + Environment.NewLine;
            if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
            {

                workspacestring = workspacestring + Environment.NewLine + ps.GetProperty("USER").ToString();
                workspacestring = workspacestring + Environment.NewLine + ps.GetProperty("VERSION").ToString();

            }

            textBox1.Text = workspacestring;

        }
    }
}

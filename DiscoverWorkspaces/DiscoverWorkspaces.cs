using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace DiscoverWorkspaces
{
    public class DiscoverWorkspaces : ESRI.ArcGIS.Desktop.AddIns.Button
    {

        private Dictionary<string, IWorkspace> _workspaces = new Dictionary<string, IWorkspace>();

        public void AddWorkspace(IWorkspace pWorkspace)
        {

            foreach (KeyValuePair<string, IWorkspace> pair in _workspaces)
            {
                if (pair.Value.Equals(pWorkspace))
                    return;
            }
             
            _workspaces.Add(Guid.NewGuid().ToString(), pWorkspace);

        }

        public void DiscoverWorkspacesMethod()
        {

            IMxDocument pMxdoc = ArcMap.Application.Document as IMxDocument;
           IEnumLayer eLayers = pMxdoc.FocusMap.Layers;
            eLayers.Reset();
            ILayer pLayer = eLayers.Next();

            while (pLayer != null)
            {

                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IDataset ds = pFLayer.FeatureClass as IDataset;
                    AddWorkspace(ds.Workspace);
                }

                pLayer = eLayers.Next();
            }

        }

        public void ListWorkspaces()
        {

            foreach (KeyValuePair<string, IWorkspace> pair in _workspaces)
            {

                IPropertySet ps = pair.Value.ConnectionProperties;
                string workspacestring = ps.GetProperty("DATABASE").ToString();
                workspacestring = workspacestring + Environment.NewLine;
                if (pair.Value.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {

                    workspacestring = workspacestring +Environment.NewLine + ps.GetProperty("USER").ToString();
                    workspacestring = workspacestring + Environment.NewLine + ps.GetProperty("VERSION").ToString();

                }
                MessageBox.Show(pair.Key + ":" + workspacestring);
                 
            }
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;

            DiscoverWorkspacesMethod();

            Form1 f = new Form1();
            f.Workspaces = _workspaces;
            foreach(string k in _workspaces.Keys)
            {
                f.addWorkspace(k);
            }
            f.ShowDialog();
             
          //  ListWorkspaces();
        }


        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace CreateRowTable
{
    public class CreateRowTable : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public CreateRowTable()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            IMxDocument pMxdoc = ArcMap.Application.Document as IMxDocument;
           IFeatureLayer pFlayer = pMxdoc.ActiveView.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDS = pFlayer.FeatureClass as IDataset;
            IFeatureWorkspace pFWS = pDS.Workspace as IFeatureWorkspace;
            ITable pTable = pFWS.OpenTable("restaurants");

            IWorkspaceEdit pWSE = pFWS as IWorkspaceEdit;
            pWSE.StartEditing(true);
            pWSE.StartEditOperation();

            IRow pRow = pTable.CreateRow();
            pRow.Value[pRow.Fields.FindField("NAME")] = "DARK HORSE";
            pRow.Value[pRow.Fields.FindField("RANKING")] = 5;
            pRow.Store();

            pWSE.StopEditOperation();
            pWSE.StopEditing(true);

            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

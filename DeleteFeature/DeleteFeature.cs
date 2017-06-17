using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace DeleteFeature
{
    public class DeleteFeature : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public DeleteFeature()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;

            IMxDocument pMxdoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFLayer = pMxdoc.ActiveView.FocusMap.Layer[0] as IFeatureLayer;
            IFeature pFeature = pFLayer.FeatureClass.GetFeature(0);
            IWorkspaceEdit pWS = (pFLayer.FeatureClass as IDataset).Workspace as IWorkspaceEdit;
            pWS.StartEditing(false);
            pWS.StartEditOperation();

            pFeature.Delete();

            pWS.StopEditOperation();
            pWS.StopEditing(true);

             
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

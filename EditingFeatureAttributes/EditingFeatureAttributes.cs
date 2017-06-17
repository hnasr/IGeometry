using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace EditingFeatureAttributes
{
    public class EditingFeatureAttributes : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public EditingFeatureAttributes()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;

            IFeature pFeature = pFLayer.FeatureClass.GetFeature(1);
            IWorkspaceEdit pWSE = ((IDataset)pFLayer.FeatureClass).Workspace as IWorkspaceEdit;
            pWSE.StartEditing(false);
            pWSE.StartEditOperation();

            pFeature.Value[pFeature.Class.FindField("CONNECTEDTOWERID")] = "My New Tower ID";
            pFeature.Store();

            pWSE.StopEditOperation();
            pWSE.StopEditing(true);
                 
             
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

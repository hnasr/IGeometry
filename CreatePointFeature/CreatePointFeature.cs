using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace CreatePointFeature
{
    public class CreatePointFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreatePointFeature()
        {

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }


        protected override void OnMouseUp(MouseEventArgs arg)
        {

            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
           IPoint pPoint =  pMxDoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);

          //  IPoint pPoint = new Point();
           // pPoint.X = -16992306.259;
           // pPoint.Y = 16328111.877;

            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;
           
            
            IWorkspaceEdit pWorkspaceEdit = ((IDataset)(pFLayer.FeatureClass)).Workspace as IWorkspaceEdit;

            pWorkspaceEdit.StartEditing(false);
            pWorkspaceEdit.StartEditOperation();

            IFeature pFeature = pFLayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPoint;
            pFeature.Store();

            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
        

            pMxDoc.ActiveView.Refresh();
        }

    }

}

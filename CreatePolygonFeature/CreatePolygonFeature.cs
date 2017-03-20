using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;

namespace CreatePolygonFeature
{
    public class CreatePolygonFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreatePolygonFeature()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument; 
            IRubberBand pRubber = new RubberPolygon();
            IPolygon pPolygon;
            // IPolygon pPolygon = pRubber.TrackNew(pMxDoc.ActiveView.ScreenDisplay, null) as IPolygon;



            IPoint pPoint1 = new Point();
            pPoint1.X = -2140489.683;
            pPoint1.Y = 33264366.694;

            IPoint pPoint2 = new Point();
            pPoint2.X = 19727215.727;
            pPoint2.Y = 27942068.023;

            IPoint pPoint3 = new Point();
            pPoint3.X = 30400767.253;
            pPoint3.Y = 14570156.281;

             

            IPointCollection pPointCollection = new Polygon();
            pPointCollection.AddPoint(pPoint1);
            pPointCollection.AddPoint(pPoint2);
            pPointCollection.AddPoint(pPoint3);
            pPointCollection.AddPoint(pPoint1);

            pPolygon = pPointCollection as IPolygon;

            IArea pArea = pPolygon as IArea;
            if (pArea.Area < 0)
                pPolygon.ReverseOrientation();

            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDS = pFLayer.FeatureClass as IDataset;
            IWorkspaceEdit pWSE = pDS.Workspace as IWorkspaceEdit;
            pWSE.StartEditing(false);
            pWSE.StartEditOperation();

            IFeature pFeature = pFLayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPolygon;
            pFeature.Store();
            pWSE.StopEditOperation();
            pWSE.StopEditing(true);

            pMxDoc.ActiveView.Refresh();
        }
    }

}

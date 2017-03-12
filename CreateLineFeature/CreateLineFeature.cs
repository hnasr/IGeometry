using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace CreateLineFeature
{
    public class CreateLineFeature : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreateLineFeature()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            IMxDocument pMxdoc = ArcMap.Application.Document as IMxDocument;
            IRubberBand pRubber = new RubberLine();
            IPolyline pPolyline;// pRubber.TrackNew(pMxdoc.ActiveView.ScreenDisplay, null) as IPolyline;

            IPoint pPoint1 = new Point();
            pPoint1.X = -2140489.683;
            pPoint1.Y = 33264366.694;

            IPoint pPoint2 = new Point();
            pPoint2.X = 19727215.727;
            pPoint2.Y = 27942068.023;

            IPoint pPoint3 = new Point();
            pPoint3.X = 30400767.253;
            pPoint3.Y = 14570156.281;


            IPointCollection pPointCollection = new Polyline();
            pPointCollection.AddPoint(pPoint1);
            pPointCollection.AddPoint(pPoint2);
            pPointCollection.AddPoint(pPoint3);


            IFeatureLayer pFLayer = pMxdoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDS = pFLayer.FeatureClass as IDataset;
            IWorkspaceEdit pWSE = pDS.Workspace as IWorkspaceEdit;
            pWSE.StartEditing(false);
            pWSE.StartEditOperation();

            IFeature pFeature = pFLayer.FeatureClass.CreateFeature();
            pFeature.Shape = pPointCollection as IGeometry;
            pFeature.Store();
            pWSE.StopEditOperation();
            pWSE.StopEditing(true);

            pMxdoc.ActiveView.Refresh();

        }



    }

}

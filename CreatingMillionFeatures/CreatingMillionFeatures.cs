using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;

namespace CreatingMillionFeatures
{
    public class CreatingMillionFeatures : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public CreatingMillionFeatures()
        {
        }

        protected override void OnClick()
        {
            //time before running/
            DateTime fDate = DateTime.Now;

            int features = 1000000;
            IMxDocument pmxdoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pflayer = pmxdoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDs = pflayer.FeatureClass as IDataset;
            IWorkspaceEdit pWorkspace = pDs.Workspace as IWorkspaceEdit;
            pWorkspace.StartEditing(false);
            pWorkspace.StartEditOperation();


            IFeatureBuffer pBuffer = pflayer.FeatureClass.CreateFeatureBuffer();
            IFeatureCursor pFcursor = pflayer.FeatureClass.Insert(true);
            Random r = new Random();
            for (int i =0; i < features; i ++)
            {
                //-12,232,313.174  7,222,833.142
                IPoint pPoint = new Point();
                pPoint.X = -12232313.174 + r.NextDouble() * 20000;
                pPoint.Y = 7222833.142 + r.NextDouble() * 20000;

                pBuffer.Shape = pPoint;
                pBuffer.Value[pBuffer.Fields.FindField("NAME")] = "myfeature" + i.ToString();
                pFcursor.InsertFeature(pBuffer);
                if (i % 1000 == 0)
                    pFcursor.Flush();
            }

            pFcursor.Flush();
            pWorkspace.StopEditOperation();
            pWorkspace.StopEditing(true);

            //time after completing
            double timeinseconds = (DateTime.Now - fDate).TotalSeconds;
            MessageBox.Show("Time to create " + features + ": " + timeinseconds + " seconds");
            pmxdoc.ActiveView.Refresh();


        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

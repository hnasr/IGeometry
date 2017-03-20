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
            //
            //  TODO: Sample code showing how to access button host
            //
            DateTime current = DateTime.Now;
            long features = 10000;
            ArcMap.Application.CurrentTool = null;

            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;
            IDataset pDS = pFLayer.FeatureClass as IDataset;

            IWorkspaceEdit pWorkspaceEdit = pDS.Workspace as IWorkspaceEdit;
            pWorkspaceEdit.StartEditing(false);
            pWorkspaceEdit.StartEditOperation();

            IFeatureBuffer pFeatureBuffer = pFLayer.FeatureClass.CreateFeatureBuffer();
            IFeatureCursor pFCursor = pFLayer.FeatureClass.Insert(true);

            IPoint pPoint;
            Random r = new Random();
            for (int i = 0; i < features; i++)
            {
                //create a random point after each 
                pPoint = new Point();
                //-11,223,459.260  6,890,973.469 Meters
                
               
                pPoint.X = -11223459.260 + r.NextDouble() * 2000; 
                pPoint.Y = 6890973.469 + r.NextDouble() * 2000;

                pFeatureBuffer.Shape = pPoint;
                pFeatureBuffer.Value[pFeatureBuffer.Fields.FindField("NAME")] = i.ToString();

                pFCursor.InsertFeature(pFeatureBuffer);
                //flush to geodatabase after each 1000 features
                if (i % 1000 == 0)
                    pFCursor.Flush();
            }
            //final flush
            pFCursor.Flush();
            pWorkspaceEdit.StopEditOperation();
            //save edits
            pWorkspaceEdit.StopEditing(true);

            double elapsed = (DateTime.Now - current).TotalMilliseconds;
            MessageBox.Show("Created " + features + " features in " + elapsed / 1000 + " seconds");

            pMxDoc.ActiveView.Refresh();
             

        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

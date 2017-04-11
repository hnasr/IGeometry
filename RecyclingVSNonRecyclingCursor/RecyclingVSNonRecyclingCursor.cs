using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

namespace RecyclingVSNonRecyclingCursor
{
    public class RecyclingVSNonRecyclingCursor : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public RecyclingVSNonRecyclingCursor()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            double recyclingTime = ProcessFeatures(true);
            double nonrecyclingTime = ProcessFeatures(false);
            MessageBox.Show("Time to process with recycling cursor: " + recyclingTime + " seconds " + Environment.NewLine +
                "Time to process with non-recycling cursor: " + nonrecyclingTime + " seconds");
        }


        public double ProcessFeatures(bool recycling)
        {
            DateTime current = DateTime.Now;
            List<IFeature> pfeatures = new List<IFeature>();
            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IFeatureLayer pFLayer = pMxDoc.FocusMap.Layer[0] as IFeatureLayer;
            IFeatureCursor pFCursor = pFLayer.FeatureClass.Search(null, recycling);
            IFeature pFeature = pFCursor.NextFeature();
            while (pFeature != null)
            {
                pfeatures.Add(pFeature);
                pFeature = pFCursor.NextFeature();
            }

            return (DateTime.Now - current).TotalSeconds;

        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

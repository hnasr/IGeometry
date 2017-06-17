using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace FindLayer
{
    public class FindLayer : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public FindLayer()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;

            IMxDocument pMxDoc = ArcMap.Application.Document as IMxDocument;
            IEnumLayer eLayers = pMxDoc.FocusMap.Layers;
            eLayers.Reset();
            ILayer pLayer = eLayers.Next();
            while (pLayer != null)
            {
               
                if (pLayer is IFeatureLayer)
                {

                    IFeatureLayer pFLayer = (IFeatureLayer)pLayer;
                   if (pFLayer.FeatureClass.AliasName == "Device")
                    {
                        MessageBox.Show(pFLayer.FeatureClass.FeatureCount(null).ToString());

                        return;
                    }

                }

                pLayer = eLayers.Next();
            }


        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

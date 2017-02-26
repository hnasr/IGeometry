using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;

namespace ListMyLayers
{
    public class ListAllLayersButton : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ListAllLayersButton()
        {
        }

        protected override void OnClick()
        {


            IMxDocument pMxDocument = (IMxDocument) ArcMap.Application.Document;
            string layers = "";

            for (int i = 0; i< pMxDocument.FocusMap.LayerCount; i++)
            {
                layers = layers + pMxDocument.FocusMap.Layer[i].Name + Environment.NewLine;

            }


            MessageBox.Show("Hello, World" + Environment.NewLine + layers);

    
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

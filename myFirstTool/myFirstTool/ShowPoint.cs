using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geometry;

namespace myFirstTool
{
    public class ShowPoint : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public ShowPoint()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseUp(MouseEventArgs arg)
        {

            MessageBox.Show("Mouse X: " + arg.X + Environment.NewLine
                 + "Mouse Y: " + arg.Y);

            IMxDocument pMxDoc = (IMxDocument) ArcMap.Application.Document;

            IPoint pPoint = pMxDoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);


            MessageBox.Show("Map X: " + pPoint.X + Environment.NewLine
                + "Map Y: " + pPoint.Y);

        }
    }

}

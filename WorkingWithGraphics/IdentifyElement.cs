using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace WorkingWithGraphics
{
    public class IdentifyElement : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public IdentifyElement()
        {
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            IMxDocument pmxdoc = ArcMap.Application.Document as IMxDocument;

            IPoint pPoint = pmxdoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);

            IGraphicsContainer pgc = pmxdoc.ActiveView.GraphicsContainer;
            IEnumElement eElements = pgc.LocateElements(pPoint, 5);
            if (eElements == null) return;

            eElements.Reset();
            IElement pElement = eElements.Next();
            while(pElement != null)
            {
                IElementProperties pElementProps = pElement as IElementProperties;
                
                MessageBox.Show(pElementProps.Name);

                pElement = eElements.Next();
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace WorkingWithGraphics
{
    public class CreatePoint : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreatePoint()
        {
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseDown(MouseEventArgs arg)
        {
            base.OnMouseDown(arg);
             

            IMxDocument pMxdoc = ArcMap.Application.Document as IMxDocument;
            IPoint pPoint = pMxdoc.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(arg.X, arg.Y);
            IGraphicsContainer pGC = pMxdoc.ActiveView.GraphicsContainer;

            IElement pElement = new MarkerElement();
            IMarkerElement pMarkerElement = pElement as IMarkerElement;

            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
            pMarkerSymbol.Size = 10;

            IRgbColor pColor = new RgbColor();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
           
            pMarkerSymbol.Color = pColor;

            pMarkerElement.Symbol = pMarkerSymbol;
            pElement.Geometry = pPoint;
              

            pGC.AddElement(pElement, 0);
            pMxdoc.ActiveView.Refresh();
           
        }
    }

}

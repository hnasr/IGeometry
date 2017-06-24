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
    public class CreatePolyline : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public CreatePolyline()
        {
        }

        protected override void OnUpdate()
        {

        }

        int i = 0;
        protected override void OnMouseDown(MouseEventArgs arg)
        {


            IMxDocument pmxdoc = ArcMap.Application.Document as IMxDocument;
            IRubberBand pRubber = new RubberLine();
            IPolyline pPolyline = pRubber.TrackNew(pmxdoc.ActiveView.ScreenDisplay, null) as IPolyline;

            IElement pElement = new LineElement();
            ILineElement pLineElement = pElement as ILineElement;
            pLineElement.Symbol = new SimpleLineSymbol();
            pElement.Geometry = pPolyline;

            IElementProperties pElementProp = pElement as IElementProperties;
            pElementProp.Name = "Hussein Element " + ++i;

            IGraphicsContainer pgc = pmxdoc.ActiveView.GraphicsContainer;
            pgc.AddElement(pElement, 0);
            pmxdoc.ActiveView.Refresh();
                

        }


    }

}

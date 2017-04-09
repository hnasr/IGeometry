using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;

namespace WorkingWithCodedValueDomains
{
    public class WorkingWithCodedValueDomains : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public WorkingWithCodedValueDomains()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            IWorkspaceFactory pWorkspaceFactory = new FileGDBWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(@"C:\IGeometry\IGeometryGDB\IGeometry.gdb", ArcMap.Application.hWnd);

            IWorkspaceDomains pWorkspaceDomains = pWorkspace as IWorkspaceDomains;
            ICodedValueDomain pDomain = pWorkspaceDomains.DomainByName["RestaurantTypes"] as ICodedValueDomain;

            for (int i = 0; i < pDomain.CodeCount; i ++)
            {
                MessageBox.Show("Code: " + pDomain.Value[i] + Environment.NewLine + "Description: " + pDomain.Name[i]);

            }
                
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.esriSystem;

namespace MyFirstFancyContextMenu
{
    /// <summary>
    /// Summary description for SetupContextMenu.
    /// </summary>
    [Guid("bfeb6821-3b5e-4d62-8d3a-cf9700eb6aac")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MyFirstFancyContextMenu.SetupContextMenu")]
    public sealed class SetupContextMenu : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        public SetupContextMenu()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Setup_Menu"; //localizable text
            base.m_caption = "Setup_Menu";  //localizable text
            base.m_message = "Setup_Menu";  //localizable text 
            base.m_toolTip = "Setup_Menu";  //localizable text 
            base.m_name = "Setup_Menu_unique";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add SetupContextMenu.OnClick implementation

            IDocument pDoc = m_application.Document;

            ICommandBars pCommandBars = pDoc.CommandBars;
            UID featureclassContextMenu = new UID();
            featureclassContextMenu.Value = "esriCatalogUI.GxEnterpriseFeatureClassContextMenu";

            UID getfeaturecountContextMenu = new UID();
            getfeaturecountContextMenu.Value = "MyFirstFancyContextMenu.GetFeatureCountMenuContext";

           ICommandBar pFeatureClassContextMenu =  pCommandBars.Find(featureclassContextMenu, false, false) as ICommandBar;
           if ( pFeatureClassContextMenu.Find(getfeaturecountContextMenu) == null)
             pFeatureClassContextMenu.Add(getfeaturecountContextMenu);
        }

        #endregion
    }
}

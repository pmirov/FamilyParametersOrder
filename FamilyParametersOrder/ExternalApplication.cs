//
// (C) Copyright 2003-2019 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;


namespace Revit.SDK.Samples.FamilyParametersOrder.CS
{
    /// <summary>
    /// A class inherits IExternalApplication interface and provide an entry of the sample.
    /// This class controls other function class and plays the bridge role in this sample.
    /// It create a custom menu "Track Revit Events" of which the corresponding 
    /// external command is the command in this project.
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class ExternalApplication : IExternalApplication
    {
        #region IExternalApplication Members

        /// <summary>
        /// Implement this method to implement the external application which should be called when 
        /// Revit starts before a file or default template is actually loaded.
        /// </summary>
        /// <param name="application">An object that is passed to the external application 
        /// which contains the controlled application.</param> 
        /// <returns>Return the status of the external application. 
        /// A result of Succeeded means that the external application successfully started. 
        /// Cancelled can be used to signify that the user cancelled the external operation at 
        /// some point.
        /// If false is returned then Revit should inform the user that the external application 
        /// failed to load and the release the internal reference.</returns>
        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Sort parameters";

            //application.ControlledApplication.DocumentOpened += new EventHandler<DocumentOpenedEventArgs>(SortLoadedFamiliesParams);
            application.CreateRibbonTab(tabName);


            RibbonPanel panel = application.CreateRibbonPanel(tabName, "Сортировка");
            string assmeblyPath = Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName(assmeblyPath);
            string iconBut1 = Path.Combine(dir, "img", "fromfile.png");
            string iconBut2 = Path.Combine(dir, "img", "fromdocument.png");
            var button1 = new PushButtonData
                (
                    "FamilyFileSortButton",
                    "Из файла",
                    assmeblyPath,
                    "Revit.SDK.Samples.FamilyParametersOrder.CS.FirstCommand"
                );
            BitmapImage bitmapImage1 = new BitmapImage(new Uri(iconBut1));
            button1.LargeImage = bitmapImage1;
            button1.ToolTip = "Сортировка параметров в семействе, сохраненном на локальном диске";
            panel.AddItem(button1);

            var button2 = new PushButtonData
            (
                "FamilyLoadedSortButton",
                "В документе",
                assmeblyPath,
                "Revit.SDK.Samples.FamilyParametersOrder.CS.SecondCommand"
            );
            BitmapImage bitmapImage2 = new BitmapImage(new Uri(iconBut2));
            button2.LargeImage = bitmapImage2;
            button2.ToolTip = "Сортировка параметров всех семейств, находящихся в документе";

            panel.AddItem(button2);

            return Autodesk.Revit.UI.Result.Succeeded;
        }

        /// <summary>
        /// Implement this method to implement the external application which should be called when 
        /// Revit is about to exit,Any documents must have been closed before this method is called.
        /// </summary>
        /// <param name="application">An object that is passed to the external application 
        /// which contains the controlled application.</param>
        /// <returns>Return the status of the external application. 
        /// A result of Succeeded means that the external application successfully shutdown. 
        /// Cancelled can be used to signify that the user cancelled the external operation at 
        /// some point.
        /// If false is returned then the Revit user should be warned of the failure of the external 
        /// application to shut down correctly.</returns>
        public Autodesk.Revit.UI.Result OnShutdown(UIControlledApplication application)
        {
            return Autodesk.Revit.UI.Result.Succeeded;
        }
        #endregion


    }
}

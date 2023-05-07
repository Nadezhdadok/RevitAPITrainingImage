using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingImage
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            using (var ts = new Transaction(doc, "export image"))
            {
                ts.Start();

                ViewPlan viewPlan = new FilteredElementCollector(doc)
                                        .OfClass(typeof(ViewPlan))
                                        .Cast<ViewPlan>()
                                        .FirstOrDefault(v => v.ViewType == ViewType.FloorPlan &&
                                                           v.Name.Equals("Level 1"));
                var imageOption = new ImageExportOptions();
                doc.ExportImage(imageOption);
                ts.Commit();
            }

            return Result.Succeeded;

        }
    }
}

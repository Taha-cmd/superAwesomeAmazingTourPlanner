using Models;
using System;
using System.Collections.Generic;
using System.Text;
using QuestPDF;
using QuestPDF.Helpers;
using QuestPDF.Elements;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using System.Diagnostics;
using System.IO;

namespace BusinessLogic
{
    public class PdfGenerator : IPdfGenerator
    {
        private string saveDirectoryPath;
        public PdfGenerator(string savePath)
        {
            saveDirectoryPath = savePath;
        }
        public void GenerateReport(Tour tour)
        {
            var document = new TourPdfDocument(tour);
            document.GeneratePdf(Path.Join(saveDirectoryPath, Guid.NewGuid() + "_" + tour.Name + "_report.pdf"));
        }
    }
}

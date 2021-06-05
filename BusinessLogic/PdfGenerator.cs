using Models;
using QuestPDF.Fluent;
using System;
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

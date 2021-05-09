using Models;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IPdfGenerator
    {
        void GenerateReport(Tour tour);
    }
}

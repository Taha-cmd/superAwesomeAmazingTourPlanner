using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Elements;
using QuestPDF.Helpers;
using QuestPDF.Drawing.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.IO;
using System.Linq;

namespace BusinessLogic
{
    public class TourPdfDocument : IDocument
    {
        private readonly Tour tour;
        public TourPdfDocument(Tour tour) => this.tour = tour;
        public void Compose(IContainer container)
        {
            container
                .PaddingHorizontal(50)
                .PaddingVertical(50)
                .Page(page =>
                {
                    page.Header().PaddingBottom(50).Row(row =>
                    {
                        row.RelativeColumn().Stack(stack =>
                        {
                            stack.Item().Text($"{DateTime.Now}", TextStyle.Default.Size(12).AlignRight().Light());
                            stack.Item().PaddingBottom(10).Text($"Report for tour {tour.Name}", TextStyle.Default.Bold().Size(24).AlignCenter());
                        });
                    });

                    page.Content().Padding(25).Stack(stack =>
                    {
                        byte[] image = File.ReadAllBytes(tour.Image);

                        stack.Item().PaddingBottom(5).Text($"From: {tour.StartingArea}");
                        stack.Item().PaddingBottom(5).Text($"To: {tour.TargetArea}");
                        stack.Item().PaddingBottom(5).Text($"Distance: {tour.Distance} km");
                        stack.Item().PaddingBottom(5).Text("Route: ");
                        stack.Item().PaddingBottom(5).Image(image);

                        if (tour.Logs.Count == 0)
                        {
                            stack.Item().Padding(25).Text($"No logs were found for this tour", TextStyle.Default.Bold().Size(20).AlignCenter());
                            return;
                        }
                            
                        stack.Item().Padding(25).Text($"This tour has {tour.Logs.Count} logs", TextStyle.Default.Bold().Size(20).AlignCenter());

                        tour.Logs.ForEach(log =>
                        {
                            stack.Item().Border(1).Padding(5).Stack(stack =>
                            {
                                stack.Item().Padding(5).Text($"Author: {log.Author}", TextStyle.Default.SemiBold().Size(18));
                                stack.Item().Padding(5).Text($"Date: {log.DateTime}");
                                stack.Item().Padding(5).Text($"Time spent on the tour in hours: {log.TotalTime}");
                                stack.Item().Padding(5).Text($"Total number of participants: {log.Members}");
                                stack.Item().Padding(5).Text($"Time spent on the tour in hours: {log.TotalTime}");
                                stack.Item().Padding(5).Text($"Rating: {log.Rating} / 10");
                                stack.Item().Padding(5).Text($"Type of Accomodation: {log.Accomodation}");
                                stack.Item().Padding(5).Text($"Has at least one McDonalds: {BoolToText(log.HasMcDonalds)}");
                                stack.Item().Padding(5).Text($"Has camping possibilities: {BoolToText(log.HasCampingSpots)}");
                                stack.Item().Padding(5).Text($"Personal comment:");
                                stack.Item().PaddingVertical(5).PaddingHorizontal(15).Border((float)0.2).BorderColor("#C0C0C0").Padding(5).Text(log.Report, TextStyle.Default.Italic());

                            });
                            
                        });

                        stack.Item().Padding(25).Text($"Statistical Logs Report", TextStyle.Default.Bold().Size(20).AlignCenter());

                        stack.Item().Stack(stack =>
                        {
                            var accoTypes = new List<string>() { "hotel", "camping", "appartment" };

                            stack.Item().Padding(5).Text($"Total logs: {tour.Logs.Count}");

                            accoTypes.ForEach(type =>
                            {
                                stack.Item().Padding(5).Text($"Total accomodations in {type}: {tour.Logs.Where(log => log.Accomodation == type).Count()}");
                            });

                            stack.Item().Padding(5).Text($"Total time: {tour.Logs.Sum(log => log.TotalTime)}");
                            stack.Item().Padding(5).Text($"Total KMs: {Math.Round(tour.Distance * tour.Logs.Count, 2)}");
                            stack.Item().Padding(5).Text($"Average time: {tour.Logs.Average(log => log.TotalTime)}");
                            stack.Item().Padding(5).Text($"Average rating: {tour.Logs.Average(log => log.Rating)}");
                            stack.Item().Padding(5).Text($"Average number of participants: {tour.Logs.Average(log => log.Members)}");
                        });
                    });

                    page.Footer().AlignCenter().PageNumber("Page {number}");
                });
        }

        private string BoolToText(bool value) => value ? "Yes" : "No";
        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata() { Author = "Taha", CreationDate = DateTime.Now };
        }
    }
}

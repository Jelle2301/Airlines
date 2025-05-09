using Domains.Entities;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using QRCoder;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.PDF.interfaces;

namespace Util.PDF
{
    public class CreatePDF : ICreatePDF
    {
        public MemoryStream CreatePDFDocumentAsync(string logoPath, Boeking boeking, Vlucht vlucht)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdf);

                iText.Layout.Element.Image logo = new iText.Layout.Element.Image(ImageDataFactory.Create(logoPath)).ScaleToFit(50, 50);
                logo.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                document.Add(logo);

                string companyName = "Gilwe Airlines";
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(companyName, QRCodeGenerator.ECCLevel.Q);   
                var qrCode = new QRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(10);

                iText.Layout.Element.Image qrCodeImageElement = new iText.Layout.Element.Image(ImageDataFactory.Create(BitmapToBytes(qrCodeImage))).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                document.Add(qrCodeImageElement);
                document.Add(new Paragraph("Ticket").SetFontSize(20));
                document.Add(new Paragraph($"Boeking ID: {boeking.BoekingId}").SetFontSize(12));
                document.Add(new Paragraph("Datum: " + DateTime.Now.ToShortDateString()));
                document.Add(new Paragraph(""));

                document.Add(new Paragraph("Ticket Informatie").SetFontSize(16));
                document.Add(new Paragraph($"Ticket-nummer: {boeking.TicketId}").SetFontSize(12));
                document.Add(new Paragraph($"Zitplaats: {boeking.Ticket.Zitplaats.Zitnummer}").SetFontSize(12));
                document.Add(new Paragraph($"Maaltijd: {boeking.Ticket.Maaltijd?.Naam ?? "Geen maaltijd gekozen"}").SetFontSize(12));
                document.Add(new Paragraph($"Reis klasse: {boeking.Ticket.Reisklasse.SoortReisklasse}")).SetFontSize(12);
                document.Add(new Paragraph(""));

                document.Add(new Paragraph("Vlucht Informatie").SetFontSize(20));
                document.Add(new Paragraph($"Vertrekplaats: {vlucht.Vertrekplaats.Plaats.Naam}").SetFontSize(12));
                document.Add(new Paragraph($"Bestemming: {vlucht.Bestemming.Plaats.Naam}").SetFontSize(12));

                document.Close();
                return new MemoryStream(stream.ToArray());
            }
        }

        // This method is for converting bitmap into a byte array
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }


}

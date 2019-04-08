using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Fonts;
using UglyToad.PdfPig.Geometry;
using UglyToad.PdfPig.Writer;

namespace Atividade6_Cassandra.Controllers
{

    /// <summary>
    /// Based on https://github.com/UglyToad/PdfPig
    /// </summary>
    public class GeraPdf
    {
        List<Models.NotaFiscalModel> _notas;

        public GeraPdf(List<Models.NotaFiscalModel> notas)
        {
            _notas = notas;
        }

        public void SaveToFile(string filePath)
        {
            //TODO savetoFile

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            PdfPageBuilder page = builder.AddPage(PageSize.A4);

            // Fonts must be registered with the document builder prior to use to prevent duplication.
            PdfDocumentBuilder.AddedFont font = builder.AddStandard14Font(Standard14Font.Helvetica);
            page.AddText("Hello World!", 12, new PdfPoint(25, 520), font);
            byte[] documentBytes = builder.Build();
            File.WriteAllBytes(@"C:\git\newPdf.pdf", documentBytes);
        }

        private void Execute()
        {

        }

        //private void TestLoadTextFromPdf()
        //{
        //    using (PdfDocument document = PdfDocument.Open(@"C:\my-file.pdf"))
        //    {
        //        int pageCount = document.NumberOfPages;

        //        Page page = document.GetPage(1);

        //        decimal widthInPoints = page.Width;
        //        decimal heightInPoints = page.Height;

        //        string text = page.Text;
        //    }
        //}
    }
}
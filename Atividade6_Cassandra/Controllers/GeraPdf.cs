using Atividade6_Cassandra.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        PdfDocumentBuilder _builder;
        PdfPageBuilder _page;
        int _linha = 800;
        int _lineHeight = 15;

        // Fonts must be registered with the document builder prior to use to prevent duplication.
        PdfDocumentBuilder.AddedFont _font;
        private int _col;

        public GeraPdf(List<Models.NotaFiscalModel> notas)
        {
            _notas = notas;

            _builder = new PdfDocumentBuilder();
            _page = _builder.AddPage(PageSize.A4);

            // Fonts must be registered with the document builder prior to use to prevent duplication.
            //_font = _builder.AddTrueTypeFont();
            _font = _builder.AddStandard14Font(Standard14Font.Helvetica);
        }

        private void AddLinha(string texto)
        {
            _page.AddText(texto, 10, new PdfPoint(10, _linha), _font);
            _linha = _linha - _lineHeight;
        }
        private void MontaHeader()
        {
            _col = 10;
            AddColuna("Servico", 200);
            AddColuna("Qtd.", 30);
            AddColuna("Vl.Unit.", 30);
            AddColuna("Recurso", 100);
            AddColuna("Funcao", 80);
            AddColuna("Taxa", 40);
            AddColuna("Desc.", 40);
            AddColuna("SubTotal", 50);
            _linha = _linha - _lineHeight;
        }
        private void AddItem(NotaFiscalModel nota)
        {
            _col = 10;
            AddColuna(nota.DescricaoServico, 200);
            AddColuna(nota.Quantidade.ToString(), 30);
            AddColuna(nota.ValorUnitario.ToString("0,00"), 30);
            AddColuna(nota.NomeRecurso, 100);
            AddColuna(nota.FuncaoRecurso, 80);
            AddColuna(nota.Taxa.ToString("0.00"), 40);
            AddColuna(nota.Desconto.ToString("0.00"), 40);
            AddColuna(nota.SubTotal.ToString("0.00"), 50);
            _linha = _linha - _lineHeight;
        }
        /// <summary>
        /// Adiciona um novo valor na mesma linha, incrementando a coluna.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="incCol"></param>
        private void AddColuna(string texto, int incCol)
        {
            _page.AddText(texto, 10, new PdfPoint(_col, _linha), _font);
            _col = _col + incCol;
        }

        public void SaveToFile(string filePath)
        {
            if ((_notas == null) || (_notas.Count == 0))
                throw new Exception("A lista de notas está vazia ou nula.");

            var nf = _notas[0].NF.ToString("000000000#");
            var cliente = _notas[0].NomeCliente;
            var endereco = _notas[0].Endereco;

            AddLinha($"Nota fiscal: {nf}");
            AddLinha($"Cliente: {cliente}");
            AddLinha($"Endereco: {endereco}");
            AddLinha("");
            MontaHeader();

            foreach (var item in _notas)
            {
                AddItem(item);
            }

            byte[] documentBytes = _builder.Build();
            File.WriteAllBytes(@filePath, documentBytes);
            
        }

        public static void WriteFileToResponse(HttpContext context, byte[] bytes, string fileName)
        {
            var bytesLength = bytes.Length.ToString(CultureInfo.InvariantCulture);
            var response = context.Response;
            response.Clear();
            response.Buffer = true;
            response.AddHeader("Content-Length", bytesLength);
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.ContentType = MimeMapping.GetMimeMapping(fileName);
            response.BinaryWrite(bytes);
            response.Flush();
            response.End();
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
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

        /// <summary>
        /// Prepara o Gerador de PDF com a lista de notas.
        /// </summary>
        /// <param name="notas"></param>
        public GeraPdf(List<Models.NotaFiscalModel> notas)
        {
            _notas = notas;

            _builder = new PdfDocumentBuilder();
            _page = _builder.AddPage(PageSize.A4);

            // Fonts must be registered with the document builder prior to use to prevent duplication.
            //_font = _builder.AddTrueTypeFont();
            _font = _builder.AddStandard14Font(Standard14Font.Helvetica);
        }

        /// <summary>
        /// Adiciona uma linha de detalhe.
        /// </summary>
        /// <param name="texto"></param>
        private void AddLinha(string texto)
        {
            _page.AddText(texto, 10, new PdfPoint(10, _linha), _font);
            _linha = _linha - _lineHeight;
        }

        /// <summary>
        /// Monta o header da tabela de itens da NF.
        /// </summary>
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

        /// <summary>
        /// Adicionar uma linha na tabela de itens da NF.
        /// </summary>
        /// <param name="nota"></param>
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

        /// <summary>
        /// Salva o relatório PDF em arquivo local em filePath.
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveToFile(string filePath)
        {
            var documentBytes = GetPdfBytes();
            File.WriteAllBytes(@filePath, documentBytes);
            
        }

        /// <summary>
        /// Monta o stream do relatório, em bytes[].
        /// </summary>
        /// <returns></returns>
        public byte[] GetPdfBytes()
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
            return documentBytes;
        }


    }
}
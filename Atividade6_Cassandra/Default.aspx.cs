using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Atividade6_Cassandra
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void executeButton_Click(object sender, EventArgs e)
        {
            var cas = new Controllers.CassandraCtr();
            var nf = nfNumber.Text;
            int nfNum = 0;

            if (!Int32.TryParse(nf, out nfNum))
            {
                ShowMessage("O valor deve ser numérico.");
                return;
            }
            var msg = "";
            if (!cas.DownloadPdf(Context, nfNum, out msg))
            {
                ShowMessage(msg);
            }
        }

        protected void ShowMessage(string texto)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", $"<script language = 'javascript'>alert('{texto}')</script>");

        }
    }
}
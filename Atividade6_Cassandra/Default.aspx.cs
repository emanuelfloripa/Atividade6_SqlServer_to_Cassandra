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
                Context.AddError(new Exception("O valor deve ser numérico."));
                return;
            }

            cas.ExportaPdfNota(nfNum);
        }
    }
}
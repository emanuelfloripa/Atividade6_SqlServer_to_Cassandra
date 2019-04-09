using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Atividade6_Cassandra
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var m = new Controllers.Migracao();
            m.ExecutaMigracao();
        }

        protected void ShowMessage(string texto)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "Message Box", $"<script language = 'javascript'>alert('{texto}')</script>");

        }
    }
}
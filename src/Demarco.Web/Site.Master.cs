using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demarco.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected bool estaLogado = false;
        protected string usuario = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            estaLogado = !string.IsNullOrWhiteSpace(Session["JwtToken"] as string);
            usuario = Session["Usuario"] as string;
        }
    }
}
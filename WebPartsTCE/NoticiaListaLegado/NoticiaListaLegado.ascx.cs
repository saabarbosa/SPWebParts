using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.NoticiaListaLegado
{
    [ToolboxItemAttribute(false)]
    public partial class NoticiaListaLegado : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public NoticiaListaLegado()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br"))
                {
                    using (SPWeb web = site.RootWeb)
                    {

                        SPList list = web.GetList("/Lists/Conteudo");
                        SPListItemCollection itens = list.GetItems();
                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 100;

                        itens = list.GetItems(oQuery);

                        StringBuilder sbLista = new StringBuilder();

                        //ltlLista.Text = itens.Count.ToString();

                        foreach (SPListItem item in itens)
                        {

                             sbLista.Append("<article class=\"post post-medium\">");
                             sbLista.Append("	<div class=\"row\">");
                             sbLista.Append("		<div class=\"col-md-12\">");
                             sbLista.Append("			<div class=\"post-content\">");
                             sbLista.Append("			<h2><a href=\"noticiaDetalhesLegado.aspx?postID=" + item.ID + "\">" + Convert.ToString(item["Título"]) + "</a></h2>");
                             sbLista.Append("				<p> " + Convert.ToString(item["Descrição"]) + "[...]</p>");
                             sbLista.Append("   		    </div>");
                             sbLista.Append("   		</div>");
                             sbLista.Append("	</div>");
                             sbLista.Append("	<div class=\"row\">");
                             sbLista.Append("		<div class=\"col-md-12\">");
                             sbLista.Append("			<div class=\"post-meta\">");
                             sbLista.Append("				<span><i class=\"icon icon-calendar\"></i> " + Convert.ToDateTime(item["Modified"]).ToString("dd-MM-yyyy HH:mm") + " </span>");
                             //sbLista.Append("				<span><i class=\"icon icon-user\"></i> By <a href=\"#\">" + Convert.ToString(item["Author"]).Split('#')[1] + "</a> </span>");
                             sbLista.Append("				<a href=\"noticiaDetalhesLegado.aspx?postID=" + item.ID + "\" class=\"btn btn-xs btn-primary pull-right\">Leia mais...</a>");
                             sbLista.Append("			</div>");
                             sbLista.Append("		</div>");
                             sbLista.Append("	</div>");
                             sbLista.Append("</article><hr/>");
                        }

                        ltlLista.Text = sbLista.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

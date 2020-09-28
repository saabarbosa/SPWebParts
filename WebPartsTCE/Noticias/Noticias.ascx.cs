using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.Noticias
{
    [ToolboxItemAttribute(false)]
    public partial class Noticias : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Noticias()
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
 
                        SPList list = web.GetList("/noticias/Lists/Postagens");
                        //SPList list = web.GetList("/noticias/SitePages");

                   
                        SPListItemCollection itens = list.GetItems();



                        StringBuilder sb = new StringBuilder();

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query   = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);

                        sb.Append("<ul>");
                        foreach (SPListItem item in itens)
                        {

			                sb.Append("<div class=\"col-md-3\">");	
				            sb.Append("    <div class=\"portfolio-item img-thumbnail\">");
                            sb.Append("        <img alt=\"noticia\" title=\"noticia\" src=\"" + item["Imagem"].ToString().Split(',')[0] + "\" class=\"img-responsive\"/>");

                            //sb.Append("        <img alt=\"noticia\" title=\"noticia\" src=\"" + item["ImagemPrincipal"].ToString() + "\" class=\"img-responsive\"/>");
				            sb.Append("    </div>");
                            sb.Append("    <a href=\"noticia.aspx?postID=" + item["ID"].ToString() + "\">");
                            sb.Append("       <p class=\"tall\">" + item["Título"].ToString().Replace("\"", "'") + "</p>");
                            sb.Append("    </a>");
                            //sb.Append("    <a href=\"noticia.aspx?postID=" + item["ID"].ToString() + "\" class=\"btn btn-success btn-icon\"><i class=\"icon icon-external-link\"></i>Saiba mais</a>");
                            //sb.Append("    <a href=\"noticias/" + item["Nome"].ToString() + "\" class=\"btn btn-success btn-icon\"><i class=\"icon icon-external-link\"></i>Saiba mais</a>");
                            //sb.Append("    <a href=\"noticia.aspx?postID=" + item["ID"].ToString() + "\" class=\"btn btn-success btn-icon\"><i class=\"icon icon-external-link\"></i>Saiba mais</a>");
                            sb.Append("</div>");
                        }
                        //sb.Append(itens.Count.ToString());
                        sb.Append("</ul>");
                        ltConteudo.Text = sb.ToString();



                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}

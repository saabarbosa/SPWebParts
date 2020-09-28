using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.NoticiaPrincipal
{
    [ToolboxItemAttribute(false)]
    public partial class NoticiaPrincipal : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public NoticiaPrincipal()
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
                        SPListItemCollection itens = list.GetItems();
                        SPQuery oQuery = itens.SourceQuery;

                        //oQuery.Query = "<Where> <Lt><FieldRef Name='Modified'/> <Value Type='DateTime' IncludeTimeValue='TRUE'>" + "2017-11-05T00:00:00Z" + "</Value></Lt></Where> <OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";

                        oQuery.Query = "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 3;

                        itens = list.GetItems(oQuery);
                        StringBuilder sbNoticia = new StringBuilder();

                        //bool noticiaDestaque = true;

                        foreach (SPListItem item in itens)
                        {
                           sbNoticia.Append("<li>");
                           sbNoticia.Append("   <a href=\"http://novosite.tce.se.gov.br/SitePages/noticia.aspx?postID=" + item["ID"].ToString() + "\">");

                           sbNoticia.Append("       <img src=\"" + item["Imagem"].ToString().Split(',')[0] + "\"  alt='" + Convert.ToString(item["Título"]) + "'>");
                           //+ "\" data-description=\"" + Convert.ToString(item["Resumo"])
                            //if (noticiaDestaque)
                           //{
                           //noticiaDestaque = false;
                           //}
                           //else
                           //{
                           //    sbNoticia.Append("   <img src=\"" + imagem + "\">");
                           //    sbNoticia.Append("   <span>" + Convert.ToString(item["Título"]) + "</span>");
                           //}
                           sbNoticia.Append("   </a>");
                           sbNoticia.Append("</li>");
 
                        }

                        ltlNoticiaSlider.Text = sbNoticia.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

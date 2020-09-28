using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;
namespace WebPartsTCE.BannersRotativosMPC
{
    [ToolboxItemAttribute(false)]
    public partial class BannersRotativosMPC : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public BannersRotativosMPC()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            label1.Text = "<!-- Title WebPart: " + DisplayTitle + " -->";
            try
            {
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br/"))
                {
                    using (SPWeb web = site.RootWeb)
                    {
                        SPList list = web.GetList("mpc/Lists/BannerPublicidadeMPC");
                       
                        //SPList list = web.Lists[DisplayTitle];
                        label1.Text += "<!--" + list.Title + "-" + list.ItemCount.ToString() + "-->";
    
                        SPListItemCollection itens = list.GetItems();
                        StringBuilder sb = new StringBuilder();
                        string[] banner;

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Ordem' Ascending='TRUE' /></OrderBy>";
                        itens = list.GetItems(oQuery);



                        foreach (SPListItem item in itens)
                        {
                            banner = item["Background"].ToString().Split(',');
                            string imgBanner = banner[0];
                            string urlBanner = banner[1];

                            label1.Text += "<!-- imgBanner = " + imgBanner + "; urlBanner = " + urlBanner + " -->";

                            sb.Append("  <div>");
                            sb.Append("     <div class=\"portfolio-item img-thumbnail\">");
                            sb.Append("         <a class=\"thumb-info lightbox\" href=\"" + urlBanner + "\" data-plugin-options='{\"type\":\"inline\", preloader: false}'>");
                            sb.Append("             <img alt=\"imagem\" title=\"imagem\" class=\"img-responsive\" src=\"" + imgBanner + "\">");
                            sb.Append("                 <span class=\"thumb-info-title\">");
                            sb.Append("                     <span class=\"thumb-info-inner\">" + item["Título"].ToString() + "</span>");
                            //sb.Append("                     <span class=\"thumb-info-type\">"+ item["Título"].ToString() +"</span>"); 
                            sb.Append("                 </span>");
                            sb.Append("                 <span class=\"thumb-info-action\">");
                            sb.Append("                     <span title=\"Universal\" class=\"thumb-info-action-icon\"><i class=\"icon icon-link\"></i></span>");
                            sb.Append("                 </span>");
                            sb.Append("         </a>");
                            sb.Append("     </div>");
                            sb.Append("  </div>");

                        }

                        ltConteudo.Text = sb.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                label1.Text += ex.Message;
            }
        }
    }
}

using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.BannerPrincipal
{
    [ToolboxItemAttribute(false)]
    public partial class BannerPrincipal : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public BannerPrincipal()
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
                        //web.AllowUnsafeUpdates = true;
                        SPList list = web.GetList("/Lists/Banner Rotativo");
                        SPListItemCollection itens = list.GetItems();
                        StringBuilder sb = new StringBuilder();
                        string[] banner;

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Ordem' Ascending='TRUE' /></OrderBy>";
                        itens = list.GetItems(oQuery);

                        
                        foreach (SPListItem item in itens)
                        {

                            sb.Append("  <li data-transition=\"fade\" data-slotamount=\"13\" data-masterspeed=\"300\" >");
                            banner = item["Background"].ToString().Split(',');
                            string urlBanner = banner[0];
                            string imgBanner = banner[1];
                            string startTagLink = Convert.ToString(item["URL"]);
                            if ((startTagLink != null) && (!startTagLink.Equals("")))
                                sb.Append("    <a href=\"" + startTagLink + "\" target=\"_blank\">");

                            sb.Append("     <img title=\"noticia\" alt=\"noticia\"  class=\"img-responsive\"  src=\"" + imgBanner + "\" data-bgfit=\"cover\" data-bgposition=\"left top\" data-bgrepeat=\"no-repeat\">");
                            sb.Append("     <div class=\"tp-caption main-label\" data-x=\"center\" data-y=\"center\"	data-speed=\"300\" data-start=\"500\" data-easing=\"easeOutExpo\">" + item["Título"].ToString() + "</div>");
                            
                            if ((startTagLink != null) && (!startTagLink.Equals("")))
                                sb.Append("    </a>");

                            sb.Append("  </li>");

                            //lblTitulo.Text = item["Título"].ToString() + " - " + item["Background"].ToString() + " - " + item["Ordem"].ToString();
                        }
                        
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

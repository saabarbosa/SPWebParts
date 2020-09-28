using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.BannerPrincipalMenor
{
    [ToolboxItemAttribute(false)]
    public partial class BannerPrincipalMenor : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public BannerPrincipalMenor()
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
                        StringBuilder sbli  = new StringBuilder();
                        StringBuilder sbdiv = new StringBuilder();
                        StringBuilder sbctrl = new StringBuilder();
                        string[] banner;

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Ordem' Ascending='TRUE' /></OrderBy>";
                        itens = list.GetItems(oQuery);

                        bool ativo = true;
                        int i = 0;
                        if (itens.Count > 0)
                        {

                            sbli.Append("<ol class=\"carousel-indicators\">");

                            sbdiv.Append("<div class=\"carousel-inner\">");

                            foreach (SPListItem item in itens)
                            {
                                banner = item["Background"].ToString().Split(',');
                                string urlBanner = banner[0];
                                string imgBanner = banner[1];


                                if (ativo)
                                {
                                    sbli.Append("  <li data-target=\"#myCarousel\" data-slide-to=\"" + i.ToString() + "\" class=\"active\"></li>");
                                    sbdiv.Append("   <div class=\"item active\">");
                                    string startTagLink = Convert.ToString(item["URL"]);
                                    if ((startTagLink != null) && (!startTagLink.Equals("")))
                                        sbdiv.Append("    <a href=\"" + startTagLink + "\" target=\"_blank\">");
                                    sbdiv.Append("      <img src=\"" + imgBanner + "\" alt=\"" + item["Título"].ToString() + "\" style=\"width:100%;\">");
                                    if ((startTagLink != null) && (!startTagLink.Equals("")))
                                        sbdiv.Append("    </a>");
                                    sbdiv.Append("   </div>");
                                    ativo = false;
                                }
                                else
                                {
                                    sbli.Append("  <li data-target=\"#myCarousel\" data-slide-to=\"" + i.ToString() + "\"></li>");
                                    sbdiv.Append("  <div class=\"item\">");
                                    string startTagLink = Convert.ToString(item["URL"]);
                                    if ((startTagLink != null) && (!startTagLink.Equals("")))
                                        sbdiv.Append("    <a href=\"" + startTagLink + "\" target=\"_blank\">");
                                    sbdiv.Append("     <img src=\"" + imgBanner + "\" alt=\"" + item["Título"].ToString() + "\" style=\"width:100%;\">");
                                    if ((startTagLink != null) && (!startTagLink.Equals("")))
                                        sbdiv.Append("    </a>");
                                    sbdiv.Append("  </div>");
                                }

                                
                               i++;
                            }


		                    sbctrl.Append("<a class=\"left carousel-control\" href=\"#myCarousel\" data-slide=\"prev\">");
		                    sbctrl.Append("  <span class=\"glyphicon glyphicon-chevron-left\"></span>");
		                    sbctrl.Append("  <span class=\"sr-only\">Anterior</span>");
		                    sbctrl.Append("</a>");
                            sbctrl.Append("<a class=\"right carousel-control\" href=\"#myCarousel\" data-slide=\"next\">");
		                    sbctrl.Append("  <span class=\"glyphicon glyphicon-chevron-right\"></span>");
                            sbctrl.Append("  <span class=\"sr-only\">Próximo</span>");
		                    sbctrl.Append("</a>");

                            sbli.Append("</ol>");
                            sbdiv.Append("</div>");
                            
                            //sb.Append( "<div id=\"myCarousel\" class=\"carousel slide\" data-ride=\"carousel\">" + sbli.ToString() +  sbdiv.ToString() +  sbctrl.ToString() + "</div>");
                            sb.Append("<div id=\"myCarousel\" class=\"carousel slide\" data-ride=\"carousel\">").Append(sbli.ToString()).Append(sbdiv.ToString()).Append(sbctrl.ToString()).Append("</div>");
                            ltConteudo.Text = sb.ToString();

                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}

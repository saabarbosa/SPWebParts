using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

namespace WebPartsTCE.Fotos
{
    [ToolboxItemAttribute(false)]
    public partial class Fotos : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Fotos()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            //ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("~/somefile.js"));
            //ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("~/SiteAssets/template/gallery/js/jquery.timers-1.2.js"));
            //ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("~/SiteAssets/template/gallery/js/jquery.easing.1.3.js"));
            //ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("~/SiteAssets/template/gallery/js/jquery.galleryview-3.0-dev.js"));
            try
            {
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br"))
                {
                    using (SPWeb web = site.RootWeb)
                    {
                        string album = Page.Request.QueryString["album"];
                        SPList list = web.GetList("/noticias/Galeria/" + album);
                        SPListItemCollection itens = list.GetItems();

                        StringBuilder sbFotos = new StringBuilder();
                        foreach (SPListItem item in itens)
                        {
                                //Label1.Text += "    <li><img src=\"/noticias/" + item.Url + "\"/></li>";
                                sbFotos.Append("    <li><img src=\"/noticias/" + item.Url + "\"/></li>");

                        }
                        sbFotos.ToString();
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.Exemplo
{
    [ToolboxItemAttribute(false)]
    public partial class Exemplo : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Exemplo()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Tambem deu erro ao tentar carregar
            SPSite site1 = SPContext.Current.Site;
            SPWeb web1 = site1.RootWeb;
            SPList list1 = web1.GetList("/Lists/Banner Rotativo");
            SPListItemCollection itens1 = list1.GetItems();
            Label1.Text = "Itens: " + itens1.Count.ToString();
        }
    }
}

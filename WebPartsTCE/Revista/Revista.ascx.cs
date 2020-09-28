using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Web.UI.WebControls;

namespace WebPartsTCE.Revista
{
    [ToolboxItemAttribute(false)]
    public partial class Revista : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Revista()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            (lvCustomers.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            this.BindListView();
        }

        private void BindListView()
        {
            try
            {
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br"))
                {
                    using (SPWeb web = site.RootWeb)
                    {

                        SPList list = web.GetList("/noticias/Revistas");
                        SPListItemCollection itens = list.GetItems();


                        StringBuilder sb = new StringBuilder();

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        //oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);
                        lvCustomers.DataSource = itens.GetDataTable();
                        lvCustomers.DataBind();
                   }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                this.BindListView();
            }

        }
    }
}

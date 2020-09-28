using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Web.UI.WebControls;

namespace WebPartsTCE.Videos
{
    [ToolboxItemAttribute(false)]
    public partial class Videos : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public Videos()
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
            label1.Text = "<!-- Title WebPart: " + DisplayTitle + " -->";
            try
            {
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br"))
                {
                    using (SPWeb web = site.RootWeb)
                    {

                        SPList list = web.GetList(DisplayTitle);
                        //SPList list = web.GetList("/noticias/Lists/Videos");
                        SPListItemCollection itens = list.GetItems();


                        StringBuilder sb = new StringBuilder();

                        SPQuery oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        //oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);
                        lvCustomers.DataSource = itens.GetDataTable();
                        lvCustomers.DataBind();

                        //sb.Append("<ul>");
                        //foreach (SPListItem item in itens)
                        //{


                        //    sb.Append("<div class=\"col-md-3\">");
                        //    sb.Append("    <div>");
                        //    sb.Append("        <iframe width=\"420\" height=\"315\" src=\"" + item["Video"].ToString() + "\"></iframe>");
                        //    sb.Append("    </div>");
                        //    sb.Append("    <p class=\"tall\">" + item["Título"].ToString() + "</p>");
                        //    sb.Append("    <p class=\"tall\">" + item["Descritivo"].ToString() + "</p>");

                        //    sb.Append("</div>");
                        //}
                        //sb.Append(itens.Count.ToString());
                        //sb.Append("</ul>");
                        //ltConteudo.Text = sb.ToString();



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

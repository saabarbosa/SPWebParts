using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.NoticiasLista
{
    [ToolboxItemAttribute(false)]
    public partial class NoticiasLista : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public NoticiasLista()
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
                        oQuery.Query = "<OrderBy><FieldRef Name='Created' Ascending='FALSE' /></OrderBy>";
                        itens = list.GetItems(oQuery);
                        StringBuilder sbLista = new StringBuilder();
                        string corpo = "";
                        foreach (SPListItem itemPopular in itens)
                        {

                            corpo = itemPopular["Corpo"].ToString();
 
                            sbLista.Append("<article class=\"post post-medium\">");
                            sbLista.Append("	<div class=\"row\">");
                            sbLista.Append("		<div class=\"col-md-12\">");
                            sbLista.Append("			<div class=\"post-content\">");
                            sbLista.Append("			<h2><a href=\"noticia.aspx?postID=" + itemPopular.ID + "\">"+  itemPopular["Título"].ToString()  +"</a></h2>");
                            sbLista.Append("				<span> " + corpo + "[...]</span>");
                            sbLista.Append("   		    </div>");
                            sbLista.Append("   		</div>");
                            sbLista.Append("	</div>");
                            sbLista.Append("	<div class=\"row\">");
                            sbLista.Append("		<div class=\"col-md-12\">");
							sbLista.Append(	"			<div class=\"post-meta\">");
                            sbLista.Append("				<span><i class=\"icon icon-calendar\"></i> " + Convert.ToDateTime(itemPopular["Modified"]).ToString("dd-MM-yyyy HH:mm") + " </span>");
                            //sbLista.Append("				<span><i class=\"icon icon-user\"></i> By <a href=\"#\">" + Convert.ToString(itemPopular["Author"]).Split('#')[1] + "</a> </span>");
                            sbLista.Append("				<span><i class=\"icon icon-user\"></i> Por <a href=\"#\">" + "DICOM/TCE" + "</a> </span>");
                            sbLista.Append("				<a href=\"noticia.aspx?postID=" + itemPopular.ID + "\" class=\"btn btn-xs btn-primary pull-right\">Leia mais...</a>");
							sbLista.Append(	"			</div>");
							sbLista.Append(	"		</div>");
							sbLista.Append(	"	</div>");
                            sbLista.Append( "</article><hr/>");

                        }

                        ltlLista.Text = sbLista.ToString();

                        oQuery = itens.SourceQuery;
                        oQuery.Query = "<Where><Geq><FieldRef Name='Created' /><Value Type='DateTime'><Today OffsetDays='-10' /></Value></Geq></Where> "
                                     + "<OrderBy><FieldRef Name='Contador' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);

                        foreach (SPListItem itemPopular in itens)
                        {

                            string imagemMin = itemPopular["Imagem"].ToString().Split(',')[0];

                            ltlPopulares.Text += "<li>";
                            ltlPopulares.Text += "	<div class=\"post-image\">";
                            ltlPopulares.Text += "		<div class=\"img-thumbnail\">";
                            ltlPopulares.Text += "			<a href=\"noticia.aspx?postID=" + itemPopular.ID + "\"></a>";
                            ltlPopulares.Text += "				<img src=\"" + imagemMin + "\" alt=\"\" style=\"width:50px; height:50px\">";
                            ltlPopulares.Text += "			</a>";
                            ltlPopulares.Text += "		</div>";
                            ltlPopulares.Text += "	</div>";
                            ltlPopulares.Text += "	<div class=\"post-info\">";
                            ltlPopulares.Text += "		<a href=\"noticia.aspx?postID=" + itemPopular.ID + "\">" + itemPopular["Título"].ToString() + "</a>";
                            ltlPopulares.Text += "		<div class=\"post-meta\">";
                            ltlPopulares.Text += Convert.ToDateTime(itemPopular["Modified"]).ToString("dd-MM-yyyy HH:mm");
                            ltlPopulares.Text += "		</div>";
                            ltlPopulares.Text += "	</div>";
                            ltlPopulares.Text += "</li>";
                        }





                        oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);

                        foreach (SPListItem itemRecente in itens)
                        {

                            string imagemMin = itemRecente["Imagem"].ToString().Split(',')[0];

                            ltlRecentes.Text += "<li>";
                            ltlRecentes.Text += "	<div class=\"post-image\">";
                            ltlRecentes.Text += "		<div class=\"img-thumbnail\">";
                            ltlRecentes.Text += "			<a href=\"noticia.aspx?postID=" + itemRecente.ID + "\"></a>";
                            ltlRecentes.Text += "				<img src=\"" + imagemMin + "\" alt=\"\" style=\"width:50px; height:50px\">";
                            ltlRecentes.Text += "			</a>";
                            ltlRecentes.Text += "		</div>";
                            ltlRecentes.Text += "	</div>";
                            ltlRecentes.Text += "	<div class=\"post-info\">";
                            ltlRecentes.Text += "		<a href=\"noticia.aspx?postID=" + itemRecente.ID + "\">" + itemRecente["Título"].ToString() + "</a>";
                            ltlRecentes.Text += "		<div class=\"post-meta\">";
                            ltlRecentes.Text += Convert.ToDateTime(itemRecente["Modified"]).ToString("dd-MM-yyyy HH:mm");
                            ltlRecentes.Text += "		</div>";
                            ltlRecentes.Text += "	</div>";
                            ltlRecentes.Text += "</li>";
                        }


                        SPList listCategorias = web.GetList("/noticias/Lists/Categorias");
                        SPListItemCollection itensCategorias = listCategorias.GetItems();
                        foreach (SPListItem cat in itensCategorias)
                        {
                            ltlCategorias.Text += "<li><a href=\"/noticias/Lists/Categorias/Category.aspx?CategoryId=" + cat.ID + "&Name=" + cat.Title + "\">" + cat.Title + "</a></li>";
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                //lblTitulo.Text = ex.Message.ToString();
            }

        }
    }
}

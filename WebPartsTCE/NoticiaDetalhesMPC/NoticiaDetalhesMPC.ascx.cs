using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.NoticiaDetalhesMPC
{
    [ToolboxItemAttribute(false)]
    public partial class NoticiaDetalhesMPC : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public NoticiaDetalhesMPC()
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
                string ID = Page.Request.QueryString["postID"];
                using (SPSite site = new SPSite("http://novosite.tce.se.gov.br"))
                {
                    using (SPWeb web = site.RootWeb)
                    {


                        SPList list = web.GetList("mpc/noticias/Lists/Postagens");
                        //SPList list = web.GetList("/noticias/SitePages");

                        SPListItemCollection itens = list.GetItems();
                        //SPList list2 = web.GetListFromWebPartPageUrl("");
                        SPListItem item = itens.GetItemById(Convert.ToInt32(ID));

                        //web.ParentWeb.Title = "xxxxxx";
                        //string sTitle = ((WebPart)this.Parent).Title;
                        //Label1.Text = "";


                        //System.Web.UI.MasterPage yourMaster = (System.Web.UI.MasterPage)Page.Master;
                        //yourMaster.Parent.Page.Title = "yyyyyyyy";

                        //Label1.Text = web.Title;
                        //web.Title = "xxx";

                        //list.BreakRoleInheritance(true);
                        //list.Update();

                        //web.RoleAssignments.Add()

                        string categoria = Convert.ToString(item["Categoria"]);
                        if (!categoria.Equals(""))
                        {
                            if (categoria.Contains("#"))
                                lblIDCategoria.Text = categoria.Split('#')[1];
                            else
                                lblIDCategoria.Text = categoria;
                        }


                        // Necessário permissão para adicionar o contador
                        //string contador_str = Convert.ToString(item["Contador"]);
                        //int contador = 1;
                        //if (!contador_str.Equals(""))
                        //    contador = Convert.ToInt32(contador_str)+1;

                        //web.AllowUnsafeUpdates = true;
                        //item["Contador"] = Convert.ToString(contador);
                        //item.Update();
                        //web.AllowUnsafeUpdates = false;


                        SPQuery oQuery = itens.SourceQuery;
                        //Pega apenas as matérias populares dos últimos 10 dias
                        oQuery.Query = "<Where><Geq><FieldRef Name='Created' /><Value Type='DateTime'><Today OffsetDays='-10' /></Value></Geq></Where> "
                                     + "<OrderBy><FieldRef Name='Contador' Ascending='FALSE' /></OrderBy>";

                        //oQuery.Query = "<OrderBy><FieldRef Name='Contador' Ascending='FALSE' /></OrderBy>";

                        //oQuery.Query = "<Where><Eq><FieldRef Name='ID'/>" +
                        //                 "<Value Type='Text'>"+ ID +"</Value></Eq></Where>";

                        oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);

                        foreach (SPListItem itemPopular in itens)
                        {
                            //string imagemMin = itemPopular["ImagemPrincipal"].ToString().Split(',')[0];
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


                        string imagem = item["Imagem"].ToString().Split(',')[0];
                        //string imagem = item["ImagemPrincipal"].ToString();

                        ltlImagens.Text += "<div class=\"owl-item\">";
                        ltlImagens.Text += "   <div>";
                        ltlImagens.Text += "		<div class=\"img-thumbnail\">";
                        ltlImagens.Text += "			<img class=\"img-responsive\" src=\"" + imagem + "\" alt=\"\">";
                        ltlImagens.Text += "		</div>";
                        ltlImagens.Text += "	</div>";
                        ltlImagens.Text += "</div>";


                        DateTime dtCriacao = Convert.ToDateTime(item["Modified"]);
                        lblDia.Text = dtCriacao.Day.ToString().PadLeft(2, '0');
                        lblMes.Text = dtCriacao.Month.ToString().PadLeft(2, '0');

                        lblTitulo.Text = Convert.ToString(item["Título"]);
                        //lblTitulo.Text = Convert.ToString(item["Titulo"]);


                        //lblUsername.Text = Convert.ToString(item["Author"]).Split('#')[1];
                        lblUsername.Text = "DICOM/TCE";
                        ltlCorpo.Text = "<span style=\"line-height: 150%; text-align:justify; color: black; \">" + Convert.ToString(item["Corpo"]) + "<span>";

                        HtmlMeta wppMeta = new HtmlMeta();
                        wppMeta.Attributes.Add("property", "og:title");
                        wppMeta.Content = lblTitulo.Text;
                        Page.Header.Controls.Add(wppMeta);

                        wppMeta = new HtmlMeta();
                        wppMeta.Attributes.Add("property", "og:description");
                        wppMeta.Content = "Tribunal de Contas do Estado de Sergipe - TCE/SE";
                        Page.Header.Controls.Add(wppMeta);

                        wppMeta = new HtmlMeta();
                        wppMeta.Attributes.Add("property", "og:url");
                        wppMeta.Content = "http://www.tce.se.gov.br/mpc/SitePages/noticia.aspx?postID=" + ID;
                        Page.Header.Controls.Add(wppMeta);

                        wppMeta = new HtmlMeta();
                        wppMeta.Attributes.Add("property", "og:image");
                        wppMeta.Content = imagem;
                        Page.Header.Controls.Add(wppMeta);

                        oQuery = itens.SourceQuery;
                        oQuery.Query = "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        oQuery.RowLimit = 4;
                        itens = list.GetItems(oQuery);

                        foreach (SPListItem itemRecente in itens)
                        {

                            string imagemMin = itemRecente["Imagem"].ToString().Split(',')[0];
                            //string imagemMin = itemRecente["ImagemPrincipal"].ToString();

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
                            ltlCategorias.Text += "<li><a href=\"/mpc/noticias/Lists/Categorias/Category.aspx?CategoryId=" + cat.ID + "&Name=" + cat.Title + "\">" + cat.Title + "</a></li>";
                        }



                        // addthis_inline_share_toolbox
                        ltlShare.Text = "<script type=\"text/javascript\" src=\"//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-4fd1d3bd5a4b404a\"></script>";
                        ltlShare.Text += "<div class=\"addthis_inline_share_toolbox_u0cl\" addthis:url=\"/noticia.aspx?postID=" + ID + "\" addthis:title=\"" + lblTitulo.Text + "\" addthis:description=\"" + lblTitulo.Text + "\" data-title=\"" + lblTitulo.Text + "\" data-description=\"" + lblTitulo.Text + "\" addthis:media=\"" + imagem + "\"></div>";
                        //ltlShare.Text = "<div class=\"addthis_inline_share_toolbox\" data-url=\"/noticia.aspx?postID=" + ID + "\" data-description=\"" + lblTitulo.Text + "\" data-title=\"" + lblTitulo.Text + "\" data-media=\"" + imagem + "\"></div>";



                    }
                }
            }
            catch (Exception ex)
            {
                lblTitulo.Text = ex.Message.ToString();
            }
        }
    }
}

using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.NoticiaDetalhesLegado
{
    [ToolboxItemAttribute(false)]
    public partial class NoticiaDetalhesLegado : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public NoticiaDetalhesLegado()
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

                        SPList list = web.GetList("/Lists/Conteudo");
                        SPListItemCollection itens = list.GetItems();
                        SPListItem item = itens.GetItemById(Convert.ToInt32(ID));

                        lblIDCategoria.Text = Convert.ToString(item["Categoria"]);

                        DateTime dtCriacao = Convert.ToDateTime(item["Modified"]);
                        lblDia.Text = dtCriacao.Day.ToString().PadLeft(2, '0');
                        lblMes.Text = dtCriacao.Month.ToString().PadLeft(2, '0');

                        lblTitulo.Text = Convert.ToString(item["Título"]);

                        lblUsername.Text = Convert.ToString(item["Author"]).Split('#')[1];

                        string imagem = Convert.ToString(item["Imagem"]).Split(',')[0];
                        if (imagem.Equals(""))
                            imgNoticia.Visible = false;

                        imgNoticia.ImageUrl = imagem;
                        imgNoticia.ToolTip  = imagem;
                        ltlCorpo.Text = "<span style=\"line-height: 150%; text-align:justify\">" + Convert.ToString(item["Conteudo"]) + "<span>";
                        
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

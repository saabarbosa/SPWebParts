using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.AtosTipos
{
    [ToolboxItemAttribute(false)]
    public partial class AtosTipos : WebPart
    {
        private string connectionString =
"Data Source=10.140.100.12;Initial Catalog=jurisprudencia;User ID=aps_etce;Password=1&tc&se7!;"; // Produção
        public AtosTipos()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
   
                string op = System.Web.HttpContext.Current.Request.Form["Op"];
                Page.Response.Write("Op = " + op);
                if (!String.IsNullOrEmpty(op))
                {
                    switch (op)
                    {
                        case "I":
                            pnlNovo.Visible = true;
                            pnlLista.Visible = false;
                            break;
                        case "E":
                            break;
                        case "D":
                            break;
                        default:
                            pnlNovo.Visible = false;
                            pnlLista.Visible = true;
                            getDados();
                            break;
                    }
                }
                else
                {
                    pnlNovo.Visible = false;
                    pnlLista.Visible = true;
                    getDados();
                }
            }
            else
            {
                pnlNovo.Visible = false;
                pnlLista.Visible = true;
                getDados();
            }

            
        }

        private void getDados()
        {
            string queryString = "select * from dbo.tb_tipo_ato";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                   
                    SqlDataReader reader = command.ExecuteReader();
                    gvwAtosTipos.DataSource = reader;
                    gvwAtosTipos.DataBind();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        protected void lnkNovo_Click(object sender, EventArgs e)
        {
            Op.Value = "I";

        }

        protected void btnCancelar_Click1(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = tbxNome.Text;
            string sigla = tbxSigla.Text;
            string numeracao = tbxNumeracao.Text;
            string glossario = tbxGlossario.Text;
            string insertString = "insert into dbo.tb_tipo_ato (tpat_tx_nome, tpat_tx_sigla, tpat_in_numeracao, tpat_tx_glossario) values (@tpat_tx_nome, @tpat_tx_sigla, @tpat_in_numeracao, @tpat_tx_glossario)";


            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertString, connection);

                try
                {
                    connection.Open();
                    if (nome == null)
                        command.Parameters.AddWithValue("@tpat_tx_nome", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@tpat_tx_nome", nome);
                    if (sigla == null)
                        command.Parameters.AddWithValue("@tpat_tx_sigla", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@tpat_tx_sigla", sigla);
                    if (numeracao == null)
                        command.Parameters.AddWithValue("@tpat_in_numeracao", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@tpat_tx_glossario", glossario);
                    if (glossario == null)
                        command.Parameters.AddWithValue("@tpat_tx_glossario", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@tpat_in_numeracao", numeracao);

                    command.ExecuteNonQuery();
                    tbxNome.Text = tbxSigla.Text = tbxNumeracao.Text = tbxGlossario.Text = String.Empty;

                    //getDados();
 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();

                }

            }


        }

        protected void lnkNovo_Click1(object sender, EventArgs e)
        {
            Op.Value = "I";
        }

        protected void lnkNovo_Click2(object sender, EventArgs e)
        {
            Op.Value = "I";
        }


    }
}

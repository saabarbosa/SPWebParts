using Microsoft.SharePoint;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;
using System.Data.SqlClient;
using System.Diagnostics;

using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net;
using System.Web;
using System.Text;
using System.Web.UI;

namespace WebPartsTCE.Pareceres
{
    [ToolboxItemAttribute(false)]
    public partial class Pareceres : WebPart
    {
        private string connectionString =
"Data Source=10.140.100.12;Initial Catalog=jurisprudencia;User ID=aps_etce;Password=1&tc&se7!;"; // Produção
        public Pareceres()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                Populate("PROCURADOR", ddl_procurador);
                Populate("TIPO_PROCESSO", ddl_tp_processo);
                Populate("UG", ddl_ug);

            }
        }


        protected string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto.ToLower();
        }

        protected void btn_Ok_Click(object sender, EventArgs e)
        {
            EnviarDados();
        }

        protected void Search(string procurador, string tpprocesso, string ug,
                   string data_ini, string data_fim, string termo)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string queryString = "";
            queryString =
            "SELECT COD, COD_ATO, PROCURADOR, TIPO_PROCESSO, DT_FINALIZACAO, UG, CAMINHO, CRIPTO, SUBSTRING(CONTEUDO, CHARINDEX('" + termo + "', CONTEUDO), 2000) AS CONTEUDO, DT_PUBLICACAO, ELETRONICO FROM PARECER_MP " +
            "WHERE " +
            "(PROCURADOR = @PROCURADOR OR @PROCURADOR IS NULL) AND (TIPO_PROCESSO = @TPPROCESSO OR @TPPROCESSO IS NULL) " +
            "AND (UG = @UG OR @UG IS NULL) " +
            "AND (DT_FINALIZACAO BETWEEN @DTINICIO AND @DTFIM OR @DTINICIO IS NULL OR @DTFIM IS NULL)" +
            "AND (CONTEUDO LIKE @CONTENT OR @CONTENT IS NULL)";


            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                if (procurador == null)
                    command.Parameters.AddWithValue("@PROCURADOR", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@PROCURADOR", procurador);
                if (tpprocesso == null)
                    command.Parameters.AddWithValue("@TPPROCESSO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@TPPROCESSO", tpprocesso);
                if (ug == null)
                    command.Parameters.AddWithValue("@UG", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@UG", ug);
                if (data_ini == null)
                    command.Parameters.AddWithValue("@DTINICIO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@DTINICIO", Convert.ToDateTime(data_ini).ToString("yyyy-MM-dd"));
                if (data_fim == null)
                    command.Parameters.AddWithValue("@DTFIM", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@DTFIM", Convert.ToDateTime(data_fim).ToString("yyyy-MM-dd"));
                if ((termo == null) || (string.IsNullOrEmpty(termo)))
                    command.Parameters.AddWithValue("@CONTENT", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@CONTENT", string.Format("%{0}%", termo));
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("COD", typeof(string));
                    dt.Columns.Add("ATO", typeof(string));
                    dt.Columns.Add("PROCURADOR", typeof(string));
                    dt.Columns.Add("TIPO PROCESSO", typeof(string));
                    dt.Columns.Add("DATA FINALIZACAO", typeof(string));
                    dt.Columns.Add("UG", typeof(string));
                    dt.Columns.Add("URL", typeof(string));
                    dt.Columns.Add("CRIPTO", typeof(string));
                    dt.Columns.Add("CONTEUDO", typeof(string));
                    dt.Columns.Add("DATA PUBLICACAO", typeof(string));
                    dt.Columns.Add("ELETRONICO", typeof(string));
                    string preview_conteudo = "";

                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(termo))
                        {
                            preview_conteudo = reader[8].ToString();
                            preview_conteudo = preview_conteudo.Replace(termo, "<span style='background-color:yellow; font-weight:bold'>" + termo + "</span>") + "...";
                        }
                        dt.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], preview_conteudo, reader[9], reader[10]);

                    }
                    gvw_resultado.DataSource = dt;
                    gvw_resultado.DataBind();
                    reader.Close();

                    sw.Stop();

                    lbl_registros.Text = dt.Rows.Count.ToString() + " registro(s) encontrado(s) em " + sw.Elapsed.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }


        protected void Populate(string field, DropDownList drop)
        {
            string queryString =
            "select distinct " + field + " from parecer_mp";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    drop.DataSource = reader;
                    drop.DataTextField = field;
                    drop.DataValueField = field;
                    drop.DataBind();
                    drop.Items.Insert(0, "");


                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        protected void EnviarDados()
        {
            string termo = txt_termo.Text;
            string procurador = (ddl_procurador.SelectedValue.Equals("") ? null : ddl_procurador.SelectedValue);
            string tpprocesso = (ddl_tp_processo.SelectedValue.Equals("") ? null : ddl_tp_processo.SelectedValue);
            string ug = (ddl_ug.SelectedValue.Equals("") ? null : ddl_ug.SelectedValue);
            string dt_inicio = (txt_dt_inicio.Text.Equals("") ? null : txt_dt_inicio.Text);
            string dt_fim = (txt_dt_fim.Text.Equals("") ? null : txt_dt_fim.Text);

            Search(procurador, tpprocesso, ug, dt_inicio, dt_fim, removerAcentos(termo));

        }
        protected void gvw_resultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EnviarDados();
            gvw_resultado.PageIndex = e.NewPageIndex;
            gvw_resultado.DataBind();
        }


        protected void gvw_resultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Download")
            {
                LinkButton lbtn = (LinkButton)e.CommandSource;


                string url = ((System.Web.UI.WebControls.WebControl)e.CommandSource).ToolTip;
                string file = url.Split('/')[url.Split('/').Length - 1].Replace(".pdf", "");

                //FtpWebRequest request = (FtpWebRequest) FtpWebRequest.Create(new Uri("ftp://10.140.100.55/se/"));
               

                //request.Proxy = null;
                //request.Credentials = new NetworkCredential("tce\\usr_sharepoint", "@(tce)");
                //request.Method = WebRequestMethods.Ftp.GetFileSize;

                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //long size = response.ContentLength;

                //response.Close();

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("tce\\usr_sharepoint", "@(tce)");
                    byte[] fileData = request.DownloadData("ftp://10.140.100.55/se/" + url);
                    string arrBytes = Convert.ToBase64String(fileData);

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterClientScriptBlock(this.GetType(), "jsDownload", getJSDownload(arrBytes, file), true);

                }
            }

        }

        protected void gvw_resultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtn = (HyperLink)e.Row.FindControl("lnk_DownloadPDFAto");

                if (lbtn != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string possuiEletronico = drv["ELETRONICO"].ToString().ToLower();
                    if (possuiEletronico.Equals("false"))
                    {
                        lbtn.Text = "Ato Físico";
                        lbtn.ToolTip = "Não gera peça única";
                        lbtn.Enabled = false;
                        lbtn.CssClass = "btn btn-danger";
                    }

                }
            }
        }

        protected string getJSDownload(string arrBytes, string file)
        {
            StringBuilder jscript = new StringBuilder();
            jscript.Append("function base64ToArrayBuffer(base64)");
            jscript.Append("{");
            jscript.Append("    var binaryString = window.atob(base64);");
            jscript.Append("    var binaryLen = binaryString.length;");
            jscript.Append("    var bytes = new Uint8Array(binaryLen);");
            jscript.Append("    for (var i = 0; i < binaryLen; i++)");
            jscript.Append("    {");
            jscript.Append("        var ascii = binaryString.charCodeAt(i);");
            jscript.Append("        bytes[i] = ascii;");
            jscript.Append("    }");
            jscript.Append("    return bytes;");
            jscript.Append("}");
            jscript.AppendLine();
            jscript.Append("function saveByteArray(reportName, byte)");
            jscript.Append("{");
            jscript.Append("    var blob = new Blob([byte], {type: 'application/pdf'});");
            jscript.Append("    var link = document.createElement('a');");
            jscript.Append("    link.href = window.URL.createObjectURL(blob);");
            jscript.Append("    var fileName = reportName + \".pdf\";");
            jscript.Append("    link.download = fileName;");
            jscript.Append("    link.click();");
            jscript.Append("}");
            jscript.AppendLine();
            jscript.Append("var base64 = base64ToArrayBuffer('" + arrBytes + "'); saveByteArray('" + file.Replace(".pdf", "") + "', base64);");
            return jscript.ToString();
        }
    }
}

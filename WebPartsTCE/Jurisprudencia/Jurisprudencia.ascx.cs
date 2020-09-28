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

namespace WebPartsTCE.Jurisprudencia
{
    [ToolboxItemAttribute(false)]
    public partial class Jurisprudencia : WebPart
    {
        private string connectionString =
    "Data Source=;Initial Catalog=;User ID=;Password=;"; // Produção



        public Jurisprudencia()
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
                Populate("RELATOR", ddl_relator);
                Populate("TIPO_PROCESSO", ddl_tp_processo);
                Populate("TIPO_DECISAO", ddl_tp_decisao);
                Populate("UG", ddl_jurisdicionado);



            }
        }

        protected bool SearchPdfFile(string fileName, String searchText)
        {

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Arquivo não encontrado", fileName);

            using (PdfReader reader = new PdfReader(fileName))
            {

                var strategy = new SimpleTextExtractionStrategy();

                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    var currentPageText = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    currentPageText = removerAcentos(currentPageText);
                    if (currentPageText.Contains(searchText))
                        return true;
                }
            }

            return false;
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

        protected void Search(string relator, string tpprocesso, string tpdecisao, string jurisdicionado,
                   string data_ini, string data_fim, string termo)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string queryString = "";
            queryString =
            "SELECT COD, COD_PROCESSO, RELATOR, TIPO_PROCESSO, TIPO_DECISAO, DT_JULGAMENTO, UG, CAMINHO, CRIPTO, SUBSTRING(CONTEUDO, CHARINDEX('" + termo + "', CONTEUDO), 2000) AS CONTEUDO, DT_PUBLICACAO, ELETRONICO FROM JURISPRUDENCIA " +
            "WHERE " +
            "(RELATOR = @RELATOR OR @RELATOR IS NULL) AND (TIPO_PROCESSO = @TPPROCESSO OR @TPPROCESSO IS NULL) " +
            "AND (TIPO_DECISAO = @TPDECISAO OR @TPDECISAO IS NULL) AND (UG = @JURISDICIONADO OR @JURISDICIONADO IS NULL) " +
            "AND (DT_JULGAMENTO BETWEEN @DTINICIO AND @DTFIM OR @DTINICIO IS NULL OR @DTFIM IS NULL)" +
            "AND (CONTEUDO LIKE @CONTENT OR @CONTENT IS NULL)";
            //if (string.IsNullOrEmpty(termo))
            //{
            //    queryString =
            //    "SELECT COD, COD_PROCESSO, RELATOR, TIPO_PROCESSO, TIPO_DECISAO, DT_JULGAMENTO, UG, CAMINHO, CRIPTO, SUBSTRING(CONTEUDO, CHARINDEX('" + termo + "', CONTEUDO), 2000) AS CONTEUDO, DT_PUBLICACAO, ELETRONICO FROM JURISPRUDENCIA " +
            //    "WHERE (CONTEUDO LIKE @CONTENT OR @CONTENT IS NULL)";
            //    if ((string.IsNullOrEmpty(relator)) || (string.IsNullOrEmpty(tpprocesso)) ||
            //         (string.IsNullOrEmpty(tpdecisao)) || (string.IsNullOrEmpty(jurisdicionado)) ||
            //         (string.IsNullOrEmpty(data_ini)) || (string.IsNullOrEmpty(data_fim)))
            //    {
            //        queryString =
            //       "SELECT COD, COD_PROCESSO, RELATOR, TIPO_PROCESSO, TIPO_DECISAO, DT_JULGAMENTO, UG, CAMINHO, CRIPTO, SUBSTRING(CONTEUDO, CHARINDEX('" + termo + "', CONTEUDO), 2000) AS CONTEUDO, DT_PUBLICACAO, ELETRONICO FROM JURISPRUDENCIA " +
            //       "WHERE " +
            //       "(RELATOR = @RELATOR OR @RELATOR IS NULL) AND (TIPO_PROCESSO = @TPPROCESSO OR @TPPROCESSO IS NULL) " +
            //       "AND (TIPO_DECISAO = @TPDECISAO OR @TPDECISAO IS NULL) AND (UG = @JURISDICIONADO OR @JURISDICIONADO IS NULL) " +
            //       "AND (DT_JULGAMENTO BETWEEN @DTINICIO AND @DTFIM OR @DTINICIO IS NULL OR @DTFIM IS NULL)";
            //    }

            //}
            //else
            //{
            //    queryString =
            //    "SELECT COD, COD_PROCESSO, RELATOR, TIPO_PROCESSO, TIPO_DECISAO, DT_JULGAMENTO, UG, CAMINHO, CRIPTO, SUBSTRING(CONTEUDO, CHARINDEX('" + termo + "', CONTEUDO), 2000) AS CONTEUDO, DT_PUBLICACAO, ELETRONICO FROM JURISPRUDENCIA " +
            //    "WHERE " +
            //    "(RELATOR = @RELATOR OR @RELATOR IS NULL) AND (TIPO_PROCESSO = @TPPROCESSO OR @TPPROCESSO IS NULL) " +
            //    "AND (TIPO_DECISAO = @TPDECISAO OR @TPDECISAO IS NULL) AND (UG = @JURISDICIONADO OR @JURISDICIONADO IS NULL) " +
            //    "AND (DT_JULGAMENTO BETWEEN @DTINICIO AND @DTFIM OR @DTINICIO IS NULL OR @DTFIM IS NULL) " +
            //    "AND (CONTEUDO LIKE @CONTENT OR @CONTENT IS NULL)";
            //}

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                if (relator == null)
                    command.Parameters.AddWithValue("@RELATOR", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@RELATOR", relator);
                if (tpprocesso == null)
                    command.Parameters.AddWithValue("@TPPROCESSO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@TPPROCESSO", tpprocesso);
                if (tpdecisao == null)
                    command.Parameters.AddWithValue("@TPDECISAO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@TPDECISAO", tpdecisao);
                if (jurisdicionado == null)
                    command.Parameters.AddWithValue("@JURISDICIONADO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@JURISDICIONADO", jurisdicionado);
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
                    dt.Columns.Add("PROCESSO", typeof(string));
                    dt.Columns.Add("RELATOR", typeof(string));
                    dt.Columns.Add("TIPO PROCESSO", typeof(string));
                    dt.Columns.Add("TIPO DECISAO", typeof(string));
                    dt.Columns.Add("JULGAMENTO", typeof(string));
                    dt.Columns.Add("JURISDICIONADO", typeof(string));
                    dt.Columns.Add("URL", typeof(string));
                    dt.Columns.Add("CRIPTO", typeof(string));
                    dt.Columns.Add("CONTEUDO", typeof(string));
                    dt.Columns.Add("ELETRONICO", typeof(string));
                    string preview_conteudo = "";

                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(termo))
                        {
                            preview_conteudo = reader[9].ToString();
                            preview_conteudo = preview_conteudo.Replace(termo, "<span style='background-color:yellow; font-weight:bold'>" + termo + "</span>") + "...";
                        //    if (preview_conteudo.Length < 600)
                        //        preview_conteudo = preview_conteudo.Substring(0, preview_conteudo.Length).Replace(termo, "<span style='background-color:yellow; font-weight:bold'>" + termo + "</span>") + "...";
                        //    else
                        //        preview_conteudo = preview_conteudo.Substring(preview_conteudo.IndexOf(termo), 600).Replace(termo, "<span style='background-color:yellow; font-weight:bold'>" + termo + "</span>") + "...";
                        }
                        dt.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8], preview_conteudo, reader[11]);

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
            "select distinct " + field + " from Jurisprudencia";

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
            string relator = (ddl_relator.SelectedValue.Equals("") ? null : ddl_relator.SelectedValue);
            string tpprocesso = (ddl_tp_processo.SelectedValue.Equals("") ? null : ddl_tp_processo.SelectedValue);
            string tpdecisao = (ddl_tp_decisao.SelectedValue.Equals("") ? null : ddl_tp_decisao.SelectedValue);
            string jurisdicionado = (ddl_jurisdicionado.SelectedValue.Equals("") ? null : ddl_jurisdicionado.SelectedValue);
            string dt_inicio = (txt_dt_inicio.Text.Equals("") ? null : txt_dt_inicio.Text);
            string dt_fim = (txt_dt_fim.Text.Equals("") ? null : txt_dt_fim.Text);

            Search(relator, tpprocesso, tpdecisao, jurisdicionado, dt_inicio, dt_fim, removerAcentos(termo));

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

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("", "");
                    byte[] fileData = request.DownloadData("" + url);
                    string arrBytes = Convert.ToBase64String(fileData);
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterClientScriptBlock(this.GetType(), "jsDownload", getJSDownload(arrBytes, file), true);

                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + file);
                    //HttpContext.Current.Response.ContentType = "application/pdf";
                    //HttpContext.Current.Response.BinaryWrite(fileData);
                    //HttpContext.Current.Response.End();

                }
            }
            //if (e.CommandName == "DownloadPecaUnica")
            //{
            //    string url = ((System.Web.UI.WebControls.WebControl)e.CommandSource).ToolTip;
            //    string file = url.Split('=')[url.Split('=').Length - 1];
            //    using (WebClient request = new WebClient())
            //    {
            //        request.Credentials = new NetworkCredential("", "");
            //        Uri uri = new Uri(url);
            //        ServicePointManager.Expect100Continue = false;
            //        ServicePointManager.MaxServicePointIdleTime = 10000;
            //        byte[] fileData = request.DownloadData(uri);
            //        //string arrBytes = Convert.ToBase64String(fileData);
            //        //ClientScriptManager cs = Page.ClientScript;
            //        //cs.RegisterClientScriptBlock(this.GetType(), "jsDownloadPecaUnica", getJSDownload(arrBytes, file), true);

            //        HttpContext.Current.Response.Clear();
            //        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + file + ".pdf"); //Informa o nome do arquivo.extensão
            //        HttpContext.Current.Response.ContentType = "application/pdf"; //Informa o Mime Type do Arquivo
            //        HttpContext.Current.Response.BinaryWrite(fileData);
            //        HttpContext.Current.Response.End();
            //    }

            //}
        }

        protected void gvw_resultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lbtn = (HyperLink)e.Row.FindControl("lnk_DownloadPDFProcesso");

                if (lbtn != null)
                {
                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    string possuiEletronico = drv["ELETRONICO"].ToString().ToLower();
                    if (possuiEletronico.Equals("false"))
                    {
                        lbtn.Text = "Processo Físico";
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

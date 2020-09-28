using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebPartsTCE.Resolucoes
{
    [ToolboxItemAttribute(false)]
    public partial class Resolucoes : WebPart
    {
        private string connectionString =
    "Data Source=;Initial Catalog=;User ID=;Password=;"; // 

        public Resolucoes()
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


                ddl_status.DataBind();
                ddl_status.Items.Insert(0, "");

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

        protected void Search(string numeroano, string status, string ementa,
                   string data_ini, string data_fim, string termo)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string ano = "";
            string numero = "";

            if (!String.IsNullOrEmpty(numeroano))
            {
                if (numeroano.Contains("/"))
                {
                    numero = numeroano.Split('/')[0];
                    ano = numeroano.Split('/')[1];
                }
            }

            string queryString = "";
            queryString =

            "SELECT l.atoo_ch_identificador as COD, l.atoo_nr_numero as NUMERO , l.atoo_nr_ano as ANO, CONCAT(l.atoo_nr_numero, '/', l.atoo_nr_ano) as NUMEROANO, " +
            "l.atoo_in_status as STATUS, l.atoo_tx_resumo as EMENTA, l.atoo_dt_publicacao as PUBLICACAO, l.atoo_tx_path as URL,  " +
            "SUBSTRING(l.atoo_tx_conteudo, CHARINDEX('" + termo + "', l.atoo_tx_conteudo), 2000) AS CONTEUDO FROM dbo.tb_ato_legis_adm as l "+
            "JOIN dbo.tb_tipo_ato as t "+
            "ON l.tpat_ch_identificador = t.tpat_ch_identificador " +
            "WHERE (l.tpat_ch_identificador = 21) " +
            "AND (l.atoo_nr_numero LIKE @NUMERO OR @NUMERO IS NULL) " +
            "AND (l.atoo_nr_ano LIKE @ANO OR @ANO IS NULL) " +
            "AND (l.atoo_in_status = @STATUS OR @STATUS IS NULL) " +
            "AND (l.atoo_tx_resumo LIKE @EMENTA OR @EMENTA IS NULL) " +
            "AND (l.atoo_dt_publicacao BETWEEN @DTINICIO AND @DTFIM OR @DTINICIO IS NULL OR @DTFIM IS NULL) " +
            "AND (l.atoo_tx_conteudo LIKE @CONTENT OR @CONTENT IS NULL) ORDER BY l.atoo_dt_publicacao DESC";


            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                //command.Parameters.Add("@NUMERO", SqlDbType.Int);
                //command.Parameters["@NUMERO"].Value = numero;

                if (numero == null)
                    command.Parameters.AddWithValue("@NUMERO", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@NUMERO", string.Format("%{0}%", numero));
                if (ano == null)
                    command.Parameters.AddWithValue("@ANO",DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ANO", string.Format("%{0}%", ano));
                if (status == null)
                    command.Parameters.AddWithValue("@STATUS", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@STATUS", status);
                if (ementa == null)
                    command.Parameters.AddWithValue("@EMENTA", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@EMENTA", string.Format("%{0}%", ementa));
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
                    dt.Columns.Add("NUMERO", typeof(string));
                    dt.Columns.Add("ANO", typeof(string));
                    dt.Columns.Add("NUMEROANO", typeof(string));
                    dt.Columns.Add("STATUS", typeof(string));
                    dt.Columns.Add("EMENTA", typeof(string));
                    dt.Columns.Add("PUBLICACAO", typeof(string));
                    dt.Columns.Add("URL", typeof(string));
                    dt.Columns.Add("CONTEUDO", typeof(string));

                    string preview_conteudo = "";
                    string preview_conteudo_sensitive = "";
                    while (reader.Read())
                    {
                        
                        if (!string.IsNullOrEmpty(termo))
                        {
                           preview_conteudo = reader[8].ToString();
                           preview_conteudo_sensitive = preview_conteudo.ToLower();

                           preview_conteudo_sensitive = preview_conteudo_sensitive.Replace(termo, "<span style='background-color:yellow; font-weight:bold'>" + termo + "</span>") + "...";
                           int termo_encontrado = preview_conteudo_sensitive.IndexOf(termo);
                           preview_conteudo_sensitive = preview_conteudo_sensitive.Substring(0, preview_conteudo_sensitive.Length);
                           if (termo_encontrado != -1)
                               preview_conteudo = preview_conteudo_sensitive;
                         
                        }
                        dt.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], preview_conteudo);
                        
                       
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



        protected void EnviarDados()
        {
            string termo = txt_termo.Text;
            string numero_ano = (txt_ano_numero.Text.Equals("") ? null : txt_ano_numero.Text);
            string status = (ddl_status.SelectedValue.Equals("") ? null : ddl_status.SelectedValue);
            string ementa = (txt_ementa.Text.Equals("") ? null : txt_ementa.Text);
            string dt_inicio = (txt_dt_inicio.Text.Equals("") ? null : txt_dt_inicio.Text);
            string dt_fim = (txt_dt_fim.Text.Equals("") ? null : txt_dt_fim.Text);

            Search(numero_ano, status, ementa, dt_inicio, dt_fim, removerAcentos(termo));

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
                string file = System.IO.Path.GetFileName(url);

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential("", "");
                    byte[] fileData = request.DownloadData("" + url);
                    string arrBytes = Convert.ToBase64String(fileData);
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterClientScriptBlock(this.GetType(), "jsDownload", getJSDownload(arrBytes, file), true);

                }
            }
            if (e.CommandName == "DownloadAnexos")
            {
                LinkButton lbtn = (LinkButton)e.CommandSource;

                string url = ((System.Web.UI.WebControls.WebControl)e.CommandSource).ToolTip;
                string[] files = url.Split(';');
                string[] fileBytesDownload;
                List<string[]> fileBytesDownloadList = new List<string[]>();
                foreach (string path in files)
                {
                    string file = System.IO.Path.GetFileName(path);
                    if (!String.IsNullOrEmpty(file))
                    {
                        using (WebClient request = new WebClient())
                        {
                            request.Credentials = new NetworkCredential("", "");
                            byte[] fileData = request.DownloadData("" + path);
                            string arrBytes = Convert.ToBase64String(fileData);
                            fileBytesDownload = new string[2];
                            fileBytesDownload[0] = arrBytes;
                            fileBytesDownload[1] = file;
                            fileBytesDownloadList.Add(fileBytesDownload);

                        }
                    }

                }
                getJSDownloads(fileBytesDownloadList);
            }

            if (e.CommandName == "ResolucoesAlteradoras")
            {
                LinkButton lbtn = (LinkButton)e.CommandSource;

                string url = ((System.Web.UI.WebControls.WebControl)e.CommandSource).ToolTip;
                string[] files = url.Split(';');
                string[] fileBytesDownload;
                List<string[]> fileBytesDownloadList = new List<string[]>();
                foreach (string path in files)
                {
                    string file = System.IO.Path.GetFileName(path);
                    if (!String.IsNullOrEmpty(file))
                    {
                        using (WebClient request = new WebClient())
                        {
                            request.Credentials = new NetworkCredential("", "");
                               byte[] fileData = request.DownloadData("" + path);
                            string arrBytes = Convert.ToBase64String(fileData);
                            fileBytesDownload = new string[2];
                            fileBytesDownload[0] = arrBytes;
                            fileBytesDownload[1] = file;
                            fileBytesDownloadList.Add(fileBytesDownload);
  
                        }
                    }

                }
                getJSDownloads(fileBytesDownloadList);
               

            }
   
        }

        protected void getJSDownloads(List<string[]> fileBytesDownloadList)
        {
            ClientScriptManager cs = Page.ClientScript;
            int i = 1;
            foreach (string[] fileBytesDownload in fileBytesDownloadList)
            {
                cs.RegisterClientScriptBlock(this.GetType(), "jsDownloads" + i, getJSDownload(fileBytesDownload[0], fileBytesDownload[1]), true);
                i++;
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


        protected void gvw_resultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {


                    HiddenField hdfId = (HiddenField)e.Row.FindControl("hdfAnexos");
                    LinkButton lkbAnexos = (LinkButton)e.Row.FindControl("btn_DownloadAnexoPDF");
                    LinkButton lkbResolucoes = (LinkButton)e.Row.FindControl("btn_ResolucoesAlteradoresPDF");

                    string select = "select * from dbo.tb_anexo_ato where atoo_ch_identificador = " + hdfId.Value;
                    SqlCommand command1 = new SqlCommand(select, connection);


                    try
                    {
                        connection.Open();

                        SqlDataReader reader1 = command1.ExecuteReader();
                        int i = 0;
                        while (reader1.Read())
                        {
                            lkbAnexos.ToolTip += Convert.ToString(reader1[3]) + ";";
                            i++;
                        }
                        lkbAnexos.Text = i.ToString() + " " + lkbAnexos.Text;
                        if (i > 0)
                            lkbAnexos.Visible = true;
                        else
                            lkbAnexos.Visible = false;
                        reader1.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {


                    }
                    //select = "select l.* from dbo.tb_ato_legis_adm l ";
                    //select += "left join dbo.tb_relacionamento_ato r  ";
                    //select += "on l.atoo_ch_identificador = r.atoo_ch_identificador_ref ";
                    //select += "where r.atoo_ch_identificador_ref = " + hdfId.Value;


                    select = "select r.*, 'ref' = (select l.atoo_tx_path from dbo.tb_ato_legis_adm l where l.atoo_ch_identificador = r.atoo_ch_identificador) ";
                    select += "from dbo.tb_relacionamento_ato r where r.atoo_ch_identificador_ref = " + hdfId.Value;
                    SqlCommand command2 = new SqlCommand(select, connection);

                    try
                    {
                        connection.Open();

                        SqlDataReader reader2 = command2.ExecuteReader();
                        int i = 0;
                        while (reader2.Read())
                        {
                            lkbResolucoes.ToolTip += Convert.ToString(reader2[2]) + ";";
                            i++;
                        }
                        lkbResolucoes.Text = i.ToString() + " " + lkbResolucoes.Text;
                        if (i > 0)
                            lkbResolucoes.Visible = true;
                        else
                            lkbResolucoes.Visible = false;
                        reader2.Close();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
            
        }

    }
}

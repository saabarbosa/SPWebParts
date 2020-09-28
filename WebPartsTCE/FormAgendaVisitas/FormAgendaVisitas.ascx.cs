using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace WebPartsTCE.FormAgendaVisitas
{
    [ToolboxItemAttribute(false)]
    public partial class FormAgendaVisitas : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public FormAgendaVisitas()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnEnviarEmail_Click(object sender, EventArgs e)
        {

            try
            {
                string nome = tbxNome.Text;
                string email = tbxEmail.Text;
                string fone = tbxTelefone.Text;
                string instituicao = tbxInstituicao.Text;
                string alunos = tbxAlunos.Text;
                string disciplina = tbxDisciplina.Text;
                string dtAgendamento = tbxAgendamento.Text;

                MailMessage msg = new MailMessage();
                msg.To.Add(email);

                MailAddress address = new MailAddress("ecojan@tce.se.gov.br");
                //MailAddress address = new MailAddress("suporte.sistemas@tce.se.gov.br");
                //MailAddress address = new MailAddress("sergioantonio.barbosa@tce.se.gov.br");
                msg.From = address;
                msg.CC.Add("dmt@tce.se.gov.br");
                msg.Subject = "Formulario de Agendamento - Escola de Contas";
                msg.IsBodyHtml = true;
                msg.Body = bodyHTML(nome, email, fone, instituicao, alunos, disciplina, dtAgendamento).ToString();
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("dmt@tce.se.gov.br", "DmT@Tc3");
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ltlMensagem.Visible = true;
                client.Send(msg);
                ltlMensagem.Text = "<div class=\"alert alert-success\">Sua mensagem foi enviada com êxito</div>";
            }
            catch (Exception ex)
            {
                ltlMensagem.Text = "<div class=\"alert alert-danger\">" + ex.Message + "</div>";
            }

        }

        public StringBuilder bodyHTML(string nome, string email, string fone, string instituicao, string alunos, string disciplina, string agendamento)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            sb.Append("<link rel=\"stylesheet\" href=\"http://novosite.tce.se.gov.br/SiteAssets/template/vendor/bootstrap/css/bootstrap.css\">");
            //sb.Append("<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>");
            sb.Append("<script src=\"http://novosite.tce.se.gov.br/SiteAssets/template/vendor/bootstrap/css/bootstrap.css\"></script>");

            sb.Append("<div class=\"container\">");
            sb.Append("<h2>Dados do contato:</h2>");
            sb.Append("<div class=\"table-responsive\">");
            sb.Append("  <table border=\"1\" class=\"table\">");
            sb.Append("    <thead>");
            sb.Append("      <tr>");
            sb.Append("        <th>Nome</th>");
            sb.Append("        <th>Email</th>");
            sb.Append("        <th>Fone</th>");
            sb.Append("        <th>Instituição</th>");
            sb.Append("      </tr>");
            sb.Append("    </thead>");
            sb.Append("    <tbody>");
            sb.Append("      <tr>");
            sb.Append("      <td>").Append(nome).Append("</td>");
            sb.Append("      <td>").Append(email).Append("</td>");
            sb.Append("      <td>").Append(fone).Append("</td>");
            sb.Append("      <td>").Append(instituicao).Append("</td>");
            sb.Append("    </tr>");
            sb.Append("    </tbody>");
            sb.Append("  </table>");
            sb.Append("  <table class=\"table\">");
            sb.Append("    <thead>");
            sb.Append("      <tr>");
            sb.Append("        <th>Disciplina</th>");
            sb.Append("        <th>Número de Alunos</th>");
            sb.Append("        <th>Data pretendida</th>");
            sb.Append("      </tr>");
            sb.Append("    </thead>");
            sb.Append("    <tbody>");
            sb.Append("      <tr>");
            sb.Append("      <td>").Append(disciplina).Append("</td>");
            sb.Append("      <td>").Append(alunos).Append("</td>");
            sb.Append("      <td>").Append(agendamento).Append("</td>");
            sb.Append("    </tr>");
            sb.Append("    </tbody>");
            sb.Append("  </table>");
            sb.Append("</div>");
            sb.Append("</div>");

            return sb;
        }
    }
}

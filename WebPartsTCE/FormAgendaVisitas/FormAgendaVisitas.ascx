<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormAgendaVisitas.ascx.cs" Inherits="WebPartsTCE.FormAgendaVisitas.FormAgendaVisitas" %>

<div class="row">
    <div class="form-group">                 
        <div class="col-md-5">
            <label>Seu nome *</label><asp:TextBox ID="tbxNome" runat="server" class="form-control" required="false" maxlength="100" placeholder="Entre com o seu nome."></asp:TextBox></div>
        <div class="col-md-4">
            <label>Seu e-mail *</label><asp:TextBox ID="tbxEmail" TextMode="Email" runat="server" class="form-control" required="false" maxlength="100" placeholder="Entre com o seu email."></asp:TextBox></div>
        <div class="col-md-3">
            <label>Seu Telefone *</label><asp:TextBox ID="tbxTelefone" runat="server"  class="form-control" required="false" maxlength="80" placeholder="Entre com o seu número de telefone."></asp:TextBox></div>
    </div>
    </div>
<div class="row">
    <div class="form-group">                 
        <div class="col-md-12">
            <label>Instituição de Ensino</label><asp:TextBox ID="tbxInstituicao" runat="server" class="form-control" required="false" maxlength="250" placeholder="Entre com a instituição de ensino."></asp:TextBox></div>
    </div>
    </div>
<div class="row">
<div class="form-group">
    <div class="col-md-6">
        <label>Disciplina</label><asp:TextBox ID="tbxDisciplina" runat="server" class="form-control" required="false" maxlength="80" placeholder="Entre com a disciplina que leciona."></asp:TextBox></div>
    <div class="col-md-3">
        <label>Agendamento</label><asp:TextBox ID="tbxAgendamento" runat="server" class="form-control" required="false" maxlength="10"></asp:TextBox></div>
    <div class="col-md-3">
        <label>Número de Alunos</label><asp:TextBox ID="tbxAlunos" runat="server" class="form-control" required="false" maxlength="3" placeholder="Entre com a quantidade de alunos."></asp:TextBox> </div>
</div>
</div>
<div class="row">
<div class="col-md-12">
    <hr/>&#160;<asp:Literal ID="ltlMensagem" runat="server" Visible="false"></asp:Literal>
    </div>&#160;</div>
<div class="row">
<div class="col-md-6">
    &nbsp;
</div>
<div class="col-md-6">
    <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar" class="mb-xs mt-xs mr-xs btn btn-primary btn-lg btn-block" Font-Size="15px" AccessKey="e" OnClick="btnEnviarEmail_Click"  />
</div>
</div>



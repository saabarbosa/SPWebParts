<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FaleConoscoMPC.ascx.cs" Inherits="WebPartsTCE.FaleConoscoMPC.FaleConoscoMPC" %>


<div class="row">
    <div class="form-group">
                 
        <div class="col-md-6">
            <label>Seu nome *</label><asp:TextBox ID="tbxNome" runat="server" class="form-control" required="true" maxlength="100" placeholder="Entre com o seu nome."></asp:TextBox></div>
        <div class="col-md-6">
            <label>Seu e-mail *</label><asp:TextBox ID="tbxEmail" TextMode="Email" runat="server" class="form-control" required="true" maxlength="100" placeholder="Entre com o seu email."></asp:TextBox></div>
    </div>
    </div>
<div class="row">
<div class="form-group">
    <div class="col-md-6">
        <label>Seu Telefone *</label><asp:TextBox ID="tbxTelefone" runat="server"  class="form-control" required="true" maxlength="80" placeholder="Entre com o seu número de telefone."></asp:TextBox></div>
    <div class="col-md-6">
        <label>Seu CPF *</label><asp:TextBox ID="tbxCpf" runat="server" class="form-control" required="true" maxlength="14" placeholder="Entre com o seu CPF."></asp:TextBox></div>
</div>
</div>
<div class="row">
<div class="form-group">
    <div class="col-md-12">
        <label>Assunto</label><asp:TextBox ID="tbxAssunto" runat="server" class="form-control" required="true" maxlength="150" placeholder="Entre com o assunto."></asp:TextBox> </div>
</div>
</div>
<div class="row">
<div class="form-group">
    <div class="col-md-12">
        <label>Mensagem *</label><asp:TextBox ID="tbxMensagem" Rows="10" TextMode="MultiLine" required="true" runat="server" class="form-control" maxlength="5000" placeholder="Entre com a mensagem."></asp:TextBox></div>
</div>
</div>
<div class="row">
<div class="col-md-12">
    <div class="g-recaptcha" data-sitekey="6Ld7iSIUAAAAAKbv2dFIFv3Xx66jK4_guPeewm48">&#160;</div>&#160;</div>&#160;</div>
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
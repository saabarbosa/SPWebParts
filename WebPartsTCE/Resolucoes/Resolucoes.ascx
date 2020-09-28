<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Resolucoes.ascx.cs" Inherits="WebPartsTCE.Resolucoes.Resolucoes" %>

<script runat=server>
    protected String GetStatus(string stat)
    {

        var status = String.Empty;
        switch (stat)
        {
            case "RV":
                status = "REVOGADO";
                break;
            case "EA":
                status = "EM ELABORAÇÃO";
                break;
            case "VG":
                status = "VIGENTE";
                break;
            case "VA":
                status = "VIGENTE (Alterado)";
                break;
            case "CA":
                status = "CANCELADO";
                break;
            default:
                break;
        }
        return status;
    }          
      
      
      
</script>

<div class="jumbotron">
      <fieldset> 
         <legend>Busca textual:</legend>
          <asp:panel runat="server" ID="pnlJurisprudencia" DefaultButton="btn_Ok">
             <div class="form-group  has-feedback">

                 <asp:TextBox runat="server" ID="txt_termo" CssClass="form-control"  placeholder="Digite a palavra-chave e tecle enter..."></asp:TextBox>
                 <!--<i class="glyphicon glyphicon-search form-control-feedback"></i>-->
             </div>
          </asp:panel>
      </fieldset>
   </div>
    <div class="jumbotron" style="background-color: #fcfcfc;">
      <fieldset> 
         <legend>Opcional:</legend> 
         <div class="row">
              <div class="col-sm-6">
                 <div class="form-group">
             
                     <label>Ano/Número:</label>
                     <asp:TextBox runat="server" ID="txt_ano_numero" CssClass="form-control"></asp:TextBox>
                </div>
             </div>
             <div class="col-sm-6">
                 <div class="form-group">
                    <label>Status:</label>
                        <asp:DropDownList ID="ddl_status" runat="server" CssClass="form-control select2-single">
                            <asp:ListItem Value="EA">Em elaboração</asp:ListItem>
                            <asp:ListItem Value="VG">Vigente</asp:ListItem>
                            <asp:ListItem Value="RV">Revogado</asp:ListItem>
                            <asp:ListItem Value="VA">Vigente (Alterado)</asp:ListItem>
                            <asp:ListItem Value="CA">Cancelado</asp:ListItem>
                        </asp:DropDownList>
                 </div>
             </div>
         </div>
         <div class="row">
            <div class="col-sm-12">
               <div class="form-group">
                   <label>Ementa:</label>
                    <asp:TextBox runat="server" ID="txt_ementa" CssClass="form-control"></asp:TextBox>
               </div>
            </div>
         </div>
         <div class="row">
            <div class="col-sm-6">
               <div class="form-group">
                  <label>Data início:</label>
                   <div class="input-group date" data-provide="datepicker">
                   <asp:TextBox runat="server" ID="txt_dt_inicio" CssClass="form-control"></asp:TextBox>
                       <div class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                      </div>
                    </div>
               </div>
            </div>
            <div class="col-sm-6">
               <div class="form-group">
                  <label>Data fim:</label>
                   <div class="input-group date" data-provide="datepicker">
                   <asp:TextBox runat="server" ID="txt_dt_fim" CssClass="form-control" ></asp:TextBox>
                       <div class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                      </div>
                   </div>
               </div>
            </div>
         </div>
        <div class="form-group">
                 <asp:Button runat="server" CssClass="btn btn-primary btn btn-block" ID="btn_Ok" Text="Pesquisar" OnClick="btn_Ok_Click"  />
        </div>
     </fieldset>
   </div>
    <div class="table-responsive">

    <div class="col-sm-12" style="text-align:right"><asp:Label runat="server" ID="lbl_registros" Text=""></asp:Label></div>

<%--        <asp:GridView ID="gvw_resultado" runat="server" CssClass="table table-bordered table-striped table-condensed mb-none" EmptyDataText="Não há dados para essa consulta.">
        </asp:GridView>--%>

        <asp:GridView ID="gvw_resultado" runat="server" PageSize="6" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed mb-none" EmptyDataText="Não há dados para essa consulta." EmptyDataRowStyle-HorizontalAlign="Center" AllowPaging="True" ShowHeader="False" OnPageIndexChanging="gvw_resultado_PageIndexChanging" OnRowCommand="gvw_resultado_RowCommand" OnRowDataBound="gvw_resultado_RowDataBound">
            <Columns>
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                     <br />
                     <div class="col-sm-8">
                         <span style="color:white; padding:6px;font-size:11px;background-color:#bfbfbf; border-radius: 4px;">Número/ano</span><div style="padding:6px"><%#Eval("NUMERO") + "/" + Eval("ANO") %> </div>
                     </div>
                     <div class="col-sm-4">
                         <span style="color:white; padding:6px;font-size:11px;background-color:#bfbfbf; border-radius: 4px;">Publicação</span><div style="padding:6px"><%#(Convert.ToDateTime(Eval("PUBLICACAO"))).ToShortDateString()%> </div>
                     </div>
                     
                     <div class="col-sm-12">
                         <hr />
                         <span style="color:white; padding:6px;font-size:11px;background-color:#bfbfbf; border-radius: 4px;">Ementa</span><div style="padding:6px"><%#Eval("EMENTA") %></div>
                     </div>
                     
                     <div class="col-sm-7">
                         <hr />
                          <span style="color:white; padding:6px;font-size:11px;background-color:#bfbfbf; border-radius: 4px;">Status</span><div style="padding:6px"><%# GetStatus(Eval("STATUS").ToString()) %> </div>
                     </div>   
                    <div class="col-sm-5" style="text-align:right">
                        <hr />
                        <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-success" ID="btn_DownloadPDF" Text="Download Resolução" ToolTip='<%#Eval("URL") %>' CommandName="Download"><i class="glyphicon glyphicon-download"></i>&nbsp;Resolução</asp:LinkButton>
                        <asp:HiddenField runat="server" ClientIDMode="Static"  ID="hdfAnexos"  Value='<%#Eval("COD") %>'></asp:HiddenField>
                        <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-warning" ID="btn_DownloadAnexoPDF" Text="Download Anexo" CommandName="DownloadAnexos"><i class="glyphicon glyphicon-paperclip"></i>&nbsp;Anexo(s)</asp:LinkButton>
                        <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-info" ID="btn_ResolucoesAlteradoresPDF" Text="Resoluções Alteradoras" CommandName="ResolucoesAlteradoras"><i class="glyphicon glyphicon-link"></i>&nbsp;Referência(s)</asp:LinkButton>
                    </div>
                     <div class="col-sm-12">
                  
                          <h4><div style="font-size:10px"><%#Eval("CONTEUDO") %></div></h4> 
                     </div> 
                    <br/>
                </ItemTemplate>
            </asp:TemplateField>    
            </Columns>
            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />           
            <PagerStyle  CssClass="pagination-bootstrap" HorizontalAlign="Center" />
        </asp:GridView>


    </div>
<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Jurisprudencia.ascx.cs" Inherits="WebPartsTCE.Jurisprudencia.Jurisprudencia" %>



    <div class="jumbotron">
      <fieldset> 
         <legend>Busca textual:</legend>
          <asp:panel runat="server" ID="pnlJurisprudencia" DefaultButton="btn_Ok">
             <div style="text-align:right"><a title="Clique aqui e faça sua pesquisa pelo Tesauro" style="cursor:pointer" data-toggle="modal" data-target="#myModal">Busca pelo Tesauro de Contas Nacional</a></div>
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
         <div class="form-group">
            <label>Relator:</label>
             <asp:DropDownList runat="server" ID="ddl_relator" DataTextField="relator" DataValueField="relator" CssClass="form-control select2-single"></asp:DropDownList>
          </div>
         <div class="row">
            <div class="col-sm-6">
               <div class="form-group">
                   <label>Tipo Processo:</label>
                    <asp:DropDownList runat="server" ID="ddl_tp_processo" DataTextField="tpprocesso" DataValueField="tpprocesso" CssClass="form-control select2-single"></asp:DropDownList>
               </div>
            </div>
            <div class="col-sm-6">
               <div class="form-group">
                   <label>Tipo Decisão:</label>
                   <asp:DropDownList runat="server" ID="ddl_tp_decisao" DataTextField="tpdecisao" DataValueField="tpdecisao" CssClass="form-control select2-single"></asp:DropDownList>
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
            <label>Jurisdicionado:</label>
                <asp:DropDownList runat="server" ID="ddl_jurisdicionado" DataTextField="jurisdicionado" DataValueField="jurisdicionado" CssClass="form-control select2-single" ></asp:DropDownList>
             </div  >
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
                     <div class="col-sm-4">
                         <h4><small>Processo:</small></h4><%#Eval("PROCESSO") %> 
                     </div>
                     <div class="col-sm-4">
                         <h4><small>Relator:</small></h4><%#Eval("RELATOR") %> 
                     </div>
                     <div class="col-sm-4">
                         <h4><small>Julgamento:</small></h4><%#Eval("JULGAMENTO") %> 
                     </div>
                     <div class="row"><hr /></div>
                     <div class="col-sm-6">
                         <h4><small>Tipo Processo:</small></h4><%#Eval("TIPO PROCESSO") %> 
                     </div>
                      <div class="col-sm-6">
                          <h4><small>Tipo Decisão:</small></h4><%#Eval("TIPO DECISAO") %> 
                     </div>            
                     <div class="row"><hr /></div>
                     <div class="col-sm-9">
                          <h4><small>Jurisdicionado:</small></h4><%#Eval("JURISDICIONADO") %> 
                     </div>   
                    <div class="col-sm-3">
                        <asp:LinkButton runat="server" ClientIDMode="Static" CssClass="btn btn-success" ID="btn_DownloadPDF" Text="Download Decisão" ToolTip='<%#Eval("URL") %>' CommandName="Download"><i class="glyphicon glyphicon-download"></i>&nbsp;Decisão</asp:LinkButton>
                        <asp:HyperLink runat="server" CssClass="btn btn-primary" ID="lnk_DownloadPDFProcesso" Text="Download Processo" ToolTip='<%# "http://etce.tce.se.gov.br/PecaUnica/pdf.aspx?c=" + Eval("CRIPTO") %>' NavigateUrl='<%# "http://etce.tce.se.gov.br/PecaUnica/pdf.aspx?c=" + Eval("CRIPTO") %>'><i class="glyphicon glyphicon-download"></i>&nbsp;Processo</asp:HyperLink>
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

<div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Tesauro de Contas Nacional - TCN</h4>
      </div>
      <div class="modal-body">
        <iframe  class="embed-responsive-item" src="irb.aspx" frameborder="0" style="width:100%; height:400px;"></iframe>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
      </div>
    </div>

  </div>
</div>

    </div>



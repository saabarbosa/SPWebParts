<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AtosTipos.ascx.cs" Inherits="WebPartsTCE.AtosTipos.AtosTipos" %>
<script>
    function setValue() {
        document.getElementById('ctl00_ctl27_g_f56a58fb_8958_4a3c_ad7b_7fef5e1fc1df_Op').value = 'I';
        alert(document.getElementById('ctl00_ctl27_g_f56a58fb_8958_4a3c_ad7b_7fef5e1fc1df_Op').value);
    }
</script>
<asp:Panel ID="pnlLista" runat="server">

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tbody>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td class="ms-list-addnew ms-textXLarge ms-list-addnew-aligntop ms-soften">
                                <asp:LinkButton ID="lnkNovo" runat="server" class="ms-heroCommandLink" OnClientClick="setValue()" OnClick="lnkNovo_Click2">

                                    <span class="ms-list-addnew-imgSpan20"><img class="ms-list-addnew-img20" src="/_themes/4/spcommon-B35BB0A9.themedpng?ctag=6"></span>
                                    <span>novo tipo de ato</span>

                                </asp:LinkButton>
                               

                            </td>
                        </tr>
                    </tbody>
                </table>




                <table class="ms-listviewtable" border="0" cellspacing="0" cellpadding="1" summary="Atos da Presidencia">
                    <tr class="ms-viewheadertr ms-vhltr" valign="top">
                        <th class="ms-vh-icon ms-minWidthHeader">
                            <div class="ms-vh-div">
                                  AAAAA  
                            </div>
                        </th>
                        <th class="ms-vh-icon ms-minWidthHeader">
                            <div class="ms-vh-div">
                                   BBBBB 
                            </div>
                        </th>
                    </tr>
                    <tr>
                        <td class="ms-headerCellStyleIcon ms-vh-icon ms-vh-selectAllIcon" style="max-width: 500px;">
                                <div class="ms-vh-div">oooooooooooooooooooooooo</div>
                        </td>
                        <td class="ms-vh2" style="max-width: 500px;">
                                <div class="ms-vh-div">oooooooooooooooooooooooo</div>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
    </tbody>
</table>
<hr />
<asp:GridView ID="gvwAtosTipos" runat="server" AutoGenerateColumns="false" CssClass="ms-listviewtable" border="0" cellspacing="0" cellpadding="1">
    <Columns>
      <asp:BoundField DataField="tpat_tx_nome" HeaderText="Nome"/>
      <asp:BoundField DataField="tpat_tx_sigla" HeaderText="Sigla" />
      <asp:BoundField DataField="tpat_in_numeracao" HeaderText="Numercao" />
    </Columns>
</asp:GridView>

    <asp:HiddenField ID="Op" runat="server" Value="" />

</asp:Panel>

<asp:Panel ID="pnlNovo" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tbody>
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <td colspan="2" class="ms-list-addnew ms-textXLarge ms-list-addnew-aligntop ms-soften">
                                    <span>Novo tipo de ato</span>
                            </td>
                        </tr>
                        <tr>
		                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                                <h3 class="ms-standardheader">
		                        <nobr>Nome<span title="Este é um campo obrigatório." class="ms-accentText"> *</span></nobr>
	                            </h3></td>
		                    <td width="350" class="ms-formbody" valign="top">
                                <asp:TextBox ID="tbxNome" Width="100%" runat="server"></asp:TextBox>
                    		</td>
	                    </tr>
                        <tr>
		                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                                <h3 class="ms-standardheader">
		                        <nobr>Sigla<span title="Este é um campo obrigatório." class="ms-accentText"> *</span></nobr>
	                            </h3></td>
		                    <td width="350" class="ms-formbody" valign="top">
                                <asp:TextBox ID="tbxSigla" Width="30%" MaxLength="25" runat="server"></asp:TextBox>
                    		</td>
	                    </tr>
                        <tr>
		                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                                <h3 class="ms-standardheader">
		                        <nobr>Numeracao<span title="Este é um campo obrigatório." class="ms-accentText"> *</span></nobr>
	                            </h3></td>
		                    <td width="350" class="ms-formbody" valign="top">
                                <asp:TextBox ID="tbxNumeracao" Width="40%" MaxLength="2" runat="server"></asp:TextBox>
                    		</td>
	                    </tr>
                        <tr>
		                    <td width="113" class="ms-formlabel" nowrap="true" valign="top">
                                <h3 class="ms-standardheader">
		                        <nobr>Glossário<span title="Este é um campo obrigatório." class="ms-accentText"> *</span></nobr>
	                            </h3></td>
		                    <td width="350" class="ms-formbody" valign="top">

                                <asp:TextBox ID="tbxGlossario" Width="100%" MaxLength="1000" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                    		</td>
	                    </tr>
	
                        <tr>
                            <td colspan="2">
                               <div style="text-align:right">
                                   <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="70px" OnClick="btnSalvar_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="70px"/>
                               </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
</asp:Panel>


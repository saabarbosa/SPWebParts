﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannersRotativosMPC.ascx.cs" Inherits="WebPartsTCE.BannersRotativosMPC.BannersRotativosMPC" %>
<section>
    <asp:Label ID="label1" runat="server"></asp:Label>
	<div class="container">
		<div class="row" id="projects">
			<div class="col-md-12 hidden-xs">
				<div class="owl-carousel owl-carousel-spaced" data-plugin-options='{"items": 4, "singleItem": false, "autoHeight": true}'>
               

                        <asp:Literal ID="ltConteudo" runat="server"></asp:Literal>

                </div>
            </div>
        </div>
    </div>
</section>
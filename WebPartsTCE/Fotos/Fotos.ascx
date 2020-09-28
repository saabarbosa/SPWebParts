<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Fotos.ascx.cs" Inherits="WebPartsTCE.Fotos.Fotos" %>

<link type="text/css" rel="stylesheet" href="/SiteAssets/template/gallery/css/jquery.galleryview-3.0-dev.css" />

<ul id="myGallery">
    <asp:Literal ID="ltlFotos" runat="server"></asp:Literal>
</ul>

<script type="text/javascript" src="/SiteAssets/template/vendor/jquery.js"></script>
<script type="text/javascript" src="/SiteAssets/template/gallery/js/jquery.timers-1.2.js"></script>
<script type="text/javascript" src="/SiteAssets/template/gallery/js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="/SiteAssets/template/gallery/js/jquery.galleryview-3.0-dev.js"></script>

<script type="text/javascript">
//    $(document).ready(function () {
        $('#myGallery').galleryView();
//    });
</script>

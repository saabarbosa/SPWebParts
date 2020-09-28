<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoticiaDetalhesLegado.ascx.cs" Inherits="WebPartsTCE.NoticiaDetalhesLegado.NoticiaDetalhesLegado" %>


<section class="page-top" style="color:#ffffff; background-color: rgb(0, 126, 70)">
    <div class="container">
		    <div class="row">
			    <div class="col-md-12">
				    <ul class="breadcrumb">
					    <li><a href="/">Home</a></li>
					    <li class="active">Detalhes da Notícia</li>
				    </ul>
			    </div>
		    </div>
		    <div class="row">
			    <div class="col-md-12">
				    <h2 style="color:#ffffff"><asp:Label ID="lblIDCategoria" runat="server" Text=""></asp:Label></h2>
			    </div>
		    </div>
	</div>
</section>

<div class="container">

	<div class="row">
		<div class="col-md-12">
			<div class="blog-posts single-post">

				<article class="post post-large blog-single-post">

					<div class="post-image">
						<div class="owl-carousel owl-theme owl-carousel-init" data-plugin-options="{&quot;items&quot;:1}" style="display: block; opacity: 1;">
							<div class="owl-wrapper-outer">
                                <div class="owl-wrapper" style="width: 3392px; left: 0px; display: block;">
                                    <asp:Image ID="imgNoticia" runat="server" />
                                </div>
							</div>
						</div>
					</div>

					<div class="post-date">
						<span class="day"><asp:Label ID="lblDia" runat="server" Text=""></asp:Label></span>
						<span class="month"><asp:Label ID="lblMes" runat="server" Text=""></asp:Label></span>
					</div>

					<div class="post-content">

						<h2><a href="#"><asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></a></h2>
                        
						<div class="post-meta">
							<span><i class="icon icon-user"></i> Por <a><asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></a></span>
						</div>

                        <asp:Literal ID="ltlCorpo" runat="server"></asp:Literal>



					</div>
				</article>

			</div>
		</div>

	</div>

</div>
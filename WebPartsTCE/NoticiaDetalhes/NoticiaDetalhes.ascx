﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoticiaDetalhes.ascx.cs" Inherits="WebPartsTCE.NoticiaDetalhes.NoticiaDetalhes" %>

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
				    <h2 style="color:#ffffff"><asp:Label ID="lblIDCategoria" runat="server" Text="" ForeColor="White"></asp:Label></h2>
			    </div>
		    </div>
	</div>
</section>

<div class="container" id="container">

	<div class="row">
		<div class="col-md-9">
			<div class="blog-posts single-post">

				<article class="post post-large blog-single-post">
					<div class="post-image">
						<div>
							<div class="owl-wrapper-outer">
                                <div class="owl-wrapper" style="left: 0px; display: block;">
                                    <asp:Literal ID="ltlImagens" runat="server"></asp:Literal>
                                </div>
							</div>
						<div class="owl-controls"><div class="owl-pagination"><div class="owl-page active"><span class=""></span></div><div class="owl-page"><span class=""></span></div></div></div></div>
					</div>

					<div class="post-date">
						<span class="day"><asp:Label ID="lblDia" runat="server" Text=""></asp:Label></span>
						<span class="month"><asp:Label ID="lblMes" runat="server" Text=""></asp:Label></span>
					</div>

					<div class="post-content">

						<h2><a href="#"><asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></a></h2>
                        
						<div class="post-meta">
							<span><i class="icon icon-user"></i> Por <a><asp:Label ID="lblUsername" runat="server" Text=""></asp:Label></a></span>
<%--							<span><i class="icon icon-tag"></i> <a href="#">Duis</a>, <a href="#">News</a> </span>
							<span><i class="icon icon-comments"></i> <a href="#">12 Comments</a></span>--%>
						</div>

                        <asp:Literal ID="ltlCorpo" runat="server"></asp:Literal>



						<div class="post-block post-share">
                        
                            <asp:Literal ID="ltlShare" runat="server"></asp:Literal>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
						</div>


					</div>
				</article>

			</div>
		</div>

		<div class="col-md-3">
			<aside class="sidebar">
                <!--
                <h4><a href="noticiaLegado.aspx"><i class="icon icon-star"></i>Notícias Legado</a></h4>
                -->
                <h4><a href="http://antigo.tce.se.gov.br/sitev2/conteudo.lista.frame.php?id=4" target="_blank"><i class="icon icon-star"></i>Notícias Legado</a></h4>

				<hr>

				<h4>Categorias</h4>
				<ul class="nav nav-list primary push-bottom">
                    <asp:Literal ID="ltlCategorias" runat="server"></asp:Literal>
				</ul>

				<div class="tabs">
					<ul class="nav nav-tabs">
						<li class="active"><a href="#recentPosts" data-toggle="tab">Recentes</a></li>
						<li><a href="#popularPosts" data-toggle="tab"><i class="icon icon-star"></i> Populares</a></li>
					</ul>
					<div class="tab-content">
						<div class="tab-pane active" id="recentPosts">
							<ul class="simple-post-list">
	
                                <asp:Literal ID="ltlRecentes" runat="server"></asp:Literal>
	
							</ul>
						</div>
						<div class="tab-pane" id="popularPosts">
							<ul class="simple-post-list">
                                <asp:Literal ID="ltlPopulares" runat="server"></asp:Literal>
							</ul>
						</div>
					</div>
				</div>

				<hr>

				<h4>RSS Feed</h4>
				<p>

				<a class="ms-calloutLink" id="ctl00_ctl26_g_3fe5c3e7_d646_457e_9449_a96513a7bf92_diidRSSFeed" onclick="return STSNavigate(this.href);" href="/noticias/_layouts/15/listfeed.aspx?List={C6E23A99-CDB5-4496-97FA-78358A1089D0}"><span class="s4-clust ms-blog-linkCommandImage" style="width: 16px; height: 16px; overflow: hidden; display: inline-block; position: relative;"><img style="border-width: 0px; left: -236px !important; top: -66px !important; position: absolute;" src="/_layouts/15/images/spcommon.png?rev=23"></span>&nbsp;<span class="ms-splinkbutton-text">RSS Feed</span></a>
			


				</p>

			</aside>
		</div>
	</div>

</div>
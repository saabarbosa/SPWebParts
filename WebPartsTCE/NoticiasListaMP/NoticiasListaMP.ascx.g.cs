﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36399
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPartsTCE.NoticiasListaMP {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    using System.CodeDom.Compiler;
    
    
    public partial class NoticiasListaMP {
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltlLista;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltlCategorias;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltlRecentes;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected global::System.Web.UI.WebControls.Literal ltlPopulares;
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebPartCodeGenerator", "12.0.0.0")]
        public static implicit operator global::System.Web.UI.TemplateControl(NoticiasListaMP target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltlLista() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltlLista = @__ctrl;
            @__ctrl.ID = "ltlLista";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltlCategorias() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltlCategorias = @__ctrl;
            @__ctrl.ID = "ltlCategorias";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltlRecentes() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltlRecentes = @__ctrl;
            @__ctrl.ID = "ltlRecentes";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private global::System.Web.UI.WebControls.Literal @__BuildControlltlPopulares() {
            global::System.Web.UI.WebControls.Literal @__ctrl;
            @__ctrl = new global::System.Web.UI.WebControls.Literal();
            this.ltlPopulares = @__ctrl;
            @__ctrl.ID = "ltlPopulares";
            return @__ctrl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void @__BuildControlTree(global::WebPartsTCE.NoticiasListaMP.NoticiasListaMP @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"


<section class=""page-top"" style=""color:#ffffff; background-color: #993300"">
    <div class=""container"">
		    <div class=""row"">
			    <div class=""col-md-12"">
				    <ul class=""breadcrumb"">
					    <li><a href=""/"">Home</a></li>
					    <li class=""active"">Lista de Notícia</li>
				    </ul>
			    </div>
		    </div>
		    <div class=""row"">
			    <div class=""col-md-12"">
				    <h2 style=""color:#ffffff"">Todas as notícias</h2>
			    </div>
		    </div>
	</div>
</section>

<div class=""container"" id=""container"">

	<div class=""row"">
		<div class=""col-md-9"">
			<div class=""blog-posts single-post"">

                "));
            global::System.Web.UI.WebControls.Literal @__ctrl1;
            @__ctrl1 = this.@__BuildControlltlLista();
            @__parser.AddParsedSubObject(@__ctrl1);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"

			</div>
		</div>

		<div class=""col-md-3"">
			<aside class=""sidebar"">
                <!--
                <h4><a href=""noticiaLegado.aspx""><i class=""icon icon-star""></i>Notícias Legado</a></h4>
                -->
                <h4><a href=""http://antigo.tce.se.gov.br/sitev2/conteudo.lista.frame.php?id=4"" target=""_blank""><i class=""icon icon-star""></i>Notícias Legado</a></h4>

				<hr>

				<h4>Categorias</h4>
				<ul class=""nav nav-list primary push-bottom"">
                    "));
            global::System.Web.UI.WebControls.Literal @__ctrl2;
            @__ctrl2 = this.@__BuildControlltlCategorias();
            @__parser.AddParsedSubObject(@__ctrl2);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
				</ul>

				<div class=""tabs"">
					<ul class=""nav nav-tabs"">
						<li class=""active""><a href=""#recentPosts"" data-toggle=""tab"">Recentes</a></li>
						<li><a href=""#popularPosts"" data-toggle=""tab""><i class=""icon icon-star""></i> Populares</a></li>
					</ul>
					<div class=""tab-content"">
						<div class=""tab-pane active"" id=""recentPosts"">
							<ul class=""simple-post-list"">
	
                                "));
            global::System.Web.UI.WebControls.Literal @__ctrl3;
            @__ctrl3 = this.@__BuildControlltlRecentes();
            @__parser.AddParsedSubObject(@__ctrl3);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\t\r\n\t\t\t\t\t\t\t</ul>\r\n\t\t\t\t\t\t</div>\r\n\t\t\t\t\t\t<div class=\"tab-pane\" id=\"popularPosts\">\r\n" +
                        "\t\t\t\t\t\t\t<ul class=\"simple-post-list\">\r\n                                "));
            global::System.Web.UI.WebControls.Literal @__ctrl4;
            @__ctrl4 = this.@__BuildControlltlPopulares();
            @__parser.AddParsedSubObject(@__ctrl4);
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(@"
							</ul>
						</div>
					</div>
				</div>

				<hr>

				<h4>RSS Feed</h4>
				<p>


				<a class=""ms-calloutLink"" id=""ctl00_ctl26_g_3fe5c3e7_d646_457e_9449_a96513a7bf92_diidRSSFeed"" onclick=""return STSNavigate(this.href);"" href=""/mpc/noticias/_layouts/15/listfeed.aspx?List={C6E23A99-CDB5-4496-97FA-78358A1089D0}""><span class=""s4-clust ms-blog-linkCommandImage"" style=""width: 16px; height: 16px; overflow: hidden; display: inline-block; position: relative;""><img style=""border-width: 0px; left: -236px !important; top: -66px !important; position: absolute;"" src=""/_layouts/15/images/spcommon.png?rev=23""></span>&nbsp;<span class=""ms-splinkbutton-text"">RSS Feed</span></a>
			


				</p>

			</aside>
		</div>
	</div>

</div>
"));
        }
        
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        [GeneratedCodeAttribute("Microsoft.VisualStudio.SharePoint.ProjectExtensions.CodeGenerators.SharePointWebP" +
            "artCodeGenerator", "12.0.0.0")]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}

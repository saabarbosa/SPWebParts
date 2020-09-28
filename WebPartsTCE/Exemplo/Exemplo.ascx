<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exemplo.ascx.cs" Inherits="WebPartsTCE.Exemplo.Exemplo" %>

  <script type="text/javascript" src="_layouts/15/sp.runtime.js"></script>
  <script type="text/javascript" src="_layouts/15/sp.js"></script>

  <script type="text/javascript">
  
   //var collListItem; 
   //function retrieveListItems() { 
   //   var context = new SP.ClientContext();
   //   var site = context.get_site();
   //   var list = context.get_web().get_lists().getByTitle('Slides');      
   //   var camlQuery = new SP.CamlQuery();
   //   camlQuery.set_viewXml('<View><RowLimit>8</RowLimit></View>');
   //   this.collListItem = list.getItems(camlQuery);
   //   context.load(collListItem);
   //   context.executeQueryAsync(Function.createDelegate(this, this.onQuerySucceeded), Function.createDelegate(this, this.onQueryFailed));        
   // }
    
   // function onQuerySucceeded(sender, args) {
    
   //     var listItemInfo = '';
   //     var listItemEnumerator = this.collListItem.getEnumerator();
   //     while (listItemEnumerator.moveNext()) {
   //         var oListItem = listItemEnumerator.get_current();
   //         //alert(TemplateFiqueAtento(oListItem));
   //         alert(oListItem.get_item('FileRef'));
   //         listItemInfo += '<img src="' + oListItem.get_item('FileRef') + '" style="width:100%; height=20%" alt="" >';
   //     }
   //     //alert(listItemInfo.toString());
   //     $('#slides').html(listItemInfo.toString());
   //     $("#slides").slidesjs({
   //          width: 840,
   //          height: 228,
   //          navigation: {
   // 		      active: true,
   // 		      effect: "slide"
   // 		        // [string] Can be either "slide" or "fade".
   // 		    },
   // 		  pagination: {
   // 		      active: false,
   // 		        // [boolean] Create pagination items.
   // 		        // You cannot use your own pagination. Sorry.
   // 		      effect: "slide"
   // 		        // [string] Can be either "slide" or "fade".
   // 		    }
   //        	      });
   // }

   // function onQueryFailed(sender, args) {
   //     alert('Request failed. ' + args.get_message() + '\n' + args.get_stackTrace());
   // }

  
   // $(function(){
   //   retrieveListItems() ;
   // });
    
    
  </script>


<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
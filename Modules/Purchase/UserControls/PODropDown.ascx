<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PODropDown.ascx.cs" Inherits="UserControls_PODropDown" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<script type="text/javascript">
function loadXMLDoc()
{
    var xmlhttp;
    if (window.XMLHttpRequest)
    {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp=new XMLHttpRequest();
    }
    else
    {// code for IE6, IE5
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange=function()
    {
    if (xmlhttp.readyState==4 && xmlhttp.status==200)
    {
        document.getElementById("dvList").innerHTML=xmlhttp.responseText;
        dvList.style.display='';  
        IsVisible=true;
    }
    }
    xmlhttp.open("GET", "ItemsList.aspx?Item=PO&AddId=<%=AddId%>&AddText=<%=AddText%>&VSL=<%=ShipId%>&Param=" + document.getElementById("<%=txtFilter.ClientID%>").value, true);
xmlhttp.send();
}

function SetValue(rid,rname)
{
    document.getElementById("<%=txtFilter.ClientID%>").value = rname;
    document.getElementById("<%=hfdInvId.ClientID%>").value = rid;
    Hide();
}
function Hide()
{
dvList.style.display='none';  
}
function handleClick()
{
if(dvList.style.display=='')
    Hide();
else
    loadXMLDoc();
}
function OpenInvoice()
{
    var URL="InviceDetailsPopUp.aspx?InvId=" + document.getElementById('<%=txtFilter.ClientID%>').value; 
    //window.open ("http://www.javascript-coder.com", "mywindow","location=1,status=1,scrollbars=1, width=100,height=100");
    window.open(URL,"InvoiceDetails","location=1,status=1,scrollbars=1, width=450,height=150");
}
</script>
<div style="width:300px;position:relative; display:inline; ">
<asp:TextBox MaxLength="30" ID="txtFilter" onclick="handleClick();" onkeyup="loadXMLDoc()" Width="164px" style="color:Black;padding-left :3px; background-image : url(Images/ddl_bg.jpg); background-repeat: repeat-x; border :none; font-family : Verdana; height:15px; text-transform:uppercase;" runat ="server"></asp:TextBox>
<asp:HiddenField runat="server" ID="hfdInvId" /> 
<img src="Images/magnifier.png" width="12px" style=" cursor :pointer" onclick="OpenInvoice();"/>
<div id="dvList" style="display:none ;float:left ; position :absolute;left:0px;top:23px;width:300px; height:200px;">
</div>
<%--<asp:TextBox MaxLength="30" ID="txtFilter"  Width="168px" AutoPostBack="true" style="color:Black;padding-left :3px; background-image : url(Images/ddl_bg.jpg); background-repeat: repeat-x; border :none; font-family : Verdana;" runat ="server"></asp:TextBox>
<asp:AutoCompleteExtender runat="server" ID="autoComplete" 
    TargetControlID="txtFilter"
    ServiceMethod="GetVesselCompletionList"
    ServicePath="~/MyDropdownData.asmx"
    MinimumPrefixLength="0" 
    CompletionSetCount="5" 
    CompletionListCssClass="autocomplete_completionListElement" 
    CompletionListItemCssClass="autocomplete_listItem" 
    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
    ShowOnlyCurrentWordInCompletionListItem="true">
</asp:AutoCompleteExtender>--%>   
</div>





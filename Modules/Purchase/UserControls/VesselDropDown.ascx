<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselDropDown.ascx.cs" Inherits="UserControls_MyDropDown" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<script type="text/javascript">
    //alert('<%=IncludeInActive.ToString()%>');
   
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
            document.getElementById("dvList").innerHTML = xmlhttp.responseText;
            //alert(document.getElementById("dvList").innerHTML);
            dvList.style.display='';  
            IsVisible=true;
        }
    }
    var IA = document.getElementById("ctl00_ContentMainMaster_ddlVessel_hdfINA").value;
    var NWC = document.getElementById("ctl00_ContentMainMaster_ddlVessel_hfdNWC").value;
    //alert(IA);
    //alert(NWC);
    xmlhttp.open("GET", "ItemsList.aspx?Item=VSL&InActive=" + IA + "&NWC=" + NWC + "&Param=" + document.getElementById("<%=txtFilter.ClientID%>").value + "&Rnd=" + Math.random(), true);
    xmlhttp.send();
   /* alert('Hi');*/
}

function SetValue(ob)
{
    document.getElementById("<%=txtFilter.ClientID%>").setAttribute("value", ob);
   // $("#ctl00_ContentMainMaster_ddlVessel_txtFilter").setAttribute("value", ob);
   //document.getElementById('ctl00_ContentMainMaster_ddlVessel_txtFilter').setAttribute("value",ob);
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
</script>
<div style="width:300px;position:relative; display:inline; padding:0px;margin:0px; border :none;top:-2px;"> 
<asp:HiddenField runat="server" ID="hdfINA" Value="1" />
<asp:HiddenField runat="server" ID="hfdNWC" Value="1" />
<asp:TextBox MaxLength="30" ID="txtFilter" onclick="handleClick();" onkeyup="loadXMLDoc()" Width="164px" style="color:Black;padding-left :3px; background-image : url(../../HRD/Images/ddl_bg.jpg); background-repeat: repeat-x; border :none; font-family : Verdana; height:19px; text-transform:uppercase;" runat ="server"></asp:TextBox>
<div id="dvList" style="display:none ;float:left ; position :absolute;left:0px;top:23px;width:300px; height:300px; ">
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





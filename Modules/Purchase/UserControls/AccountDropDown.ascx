<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountDropDown.ascx.cs" Inherits="UserControls_AccountDropDown" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<script type="text/javascript">
var <%=ClientID%>_destCtl;
function <%=ClientID%>_loadXMLDoc()
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
        <%=ClientID%>_destCtl.innerHTML = xmlhttp.responseText;
        <%=ClientID%>_destCtl.style.display = '';  
        IsVisible=true;
    }
    }
    xmlhttp.open("GET","ItemsList.aspx?PK=<%=ClientID%>&Item=ACC&Param=" + document.getElementById("<%=txtFilter.ClientID%>").value ,true);
    xmlhttp.send();
}

function <%=ClientID%>_SetValue(ob)
{
    document.getElementById("<%=txtFilter.ClientID%>").value=ob;
    <%=ClientID%>_Hide();
}
function <%=ClientID%>_Hide()
{
    <%=ClientID%>_destCtl.style.display = 'none';  
}
function <%=ClientID%>_handleClick()
{
    if (<%=ClientID%>_destCtl.style.display == '')
    <%=ClientID%>_Hide();
    else
    <%=ClientID%>_loadXMLDoc();
}
</script>
<div style="width:300px;position:relative; display:inline; padding:0px;margin:0px; border :none;top:-2px;">
<asp:TextBox MaxLength="30" ID="txtFilter" Width="164px" style="color:Black;padding-left :3px; background-image : url(Images/ddl_bg.jpg); background-repeat: repeat-x; border :none; font-family : Verdana; height:19px; text-transform:uppercase;" runat ="server"></asp:TextBox>
<div id="<%=ClientID%>_dvList" style="display:none ;float:left ; position :absolute;left:0px;top:23px;width:300px; height:300px; ">
</div>
</div>
<script type="text/javascript">
<%=ClientID%>_destCtl = document.getElementById('<%=ClientID%>_dvList');
document.getElementById("<%=txtFilter.ClientID%>").onclick=<%=ClientID%>_handleClick;
document.getElementById("<%=txtFilter.ClientID%>").onkeyup=<%=ClientID%>_loadXMLDoc;

</script> 





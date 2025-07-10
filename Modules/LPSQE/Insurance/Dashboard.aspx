<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="InsuranceRecordManagement_InsuranceHome" %>
    
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--<%@ Register Src="~/HSSQE/MOC/MocRequest.ascx" TagName="MOCRequest" tagprefix="user" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>EMANAGER</title>
<style type="text/css">
.fh1
{ 
    font-weight:bold;
    font-size:14px;
}
.fh2
{ 
    font-size:14px;
}
.fh3
{ 
    font-size:14px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:14px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
.btn_Close
{
    background-color:Red;
    border:solid 1px grey;
    color:White;
    width:100px;
}
.cls_I
{
    background-color:#80E680;
}
.cls_O
{
    background-color:#FFAD99;
}

</style>
    </head>

    
 <body style="margin:0 0 0 0">
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                <td>
                <table cellpadding="20" cellspacing="15" width="100%" border="0">
                <tr>
                <td style="text-align:left; background-color:#FFAD99;" class="fh2">
                    Policies expiring in next 30 Days :</td>
                <td style="text-align:center; background-color:#FFAD99;" class="fh2">
                   <asp:Label runat="server" ID="lblc1" onclick="Show5();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label></td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#80E680;" class="fh2">
                    Open Cases :
                    
                </td>
                <td style="text-align:center; background-color:#80E680;" class="fh2">
                   <asp:Label runat="server" ID="lblc2" onclick="Show1();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> 
                </td>
                </tr>
               <%-- <tr>
                <td style="text-align:left; background-color:#FFE6E6;" class="fh2">
                    Crew Awaiting Approval :
                </td>
                <td style="text-align:center; background-color:#FFE6E6;" class="fh2">
                    <asp:Label runat="server" ID="lblc2" onclick="Show2();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label> 
                </td>
                </tr>               
                <tr>
                <td style="text-align:left; background-color:#FFEB99;" class="fh2">
                    Vessel exceeding budgeted crew complement :</td>
                <td style="text-align:center; background-color:#FFEB99;" class="fh2">
                   <asp:Label runat="server" ID="lblc6" onclick="Show6();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label></td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#C2E0FF;" class="fh2">
                    Open PEAP :</td>
                <td style="text-align:center; background-color:#C2E0FF;" class="fh2">
                   <asp:Label runat="server" ID="lblc7" onclick="Show7();" style="cursor:pointer; text-decoration:underline;" Text="0"></asp:Label></td>
                </tr>--%>
                </table>
                </td>                
                </tr>
                </table>
            </td>
            </tr>
        </table> 
        </td>
        </tr>
      </table>


</form>
</body>
    

</html>


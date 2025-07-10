<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeExperienceReport.aspx.cs" Inherits="EmployeeExperienceReport" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Page</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .style1
    {
        text-align :left; 
        font-size :13px;  
        font-family:Arial Unicode MS; 
        color :#222222; 
        padding :5px; 
        border-style:none;
        text-align :left; 
        width:600px;
    }
    .gridheadings
    {
    	background-color :#C2C2C2;
    	color : Red ;
    	font-size :13px; 
    	border :dotted 1px Black;
    	padding :2px;
    }
    *
    {
        font-family:Calibri;
        font-size:14px;
    }
    div
    {
        vertical-align:middle;
    }
    </style>
</head>
<body style="">
    <form id="form1" runat="server" > 
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="background-color: #4371a5; text-align: center; font-size:15px;color:White; padding:10px;" >Shore Staff Competency Report</div>
        <div style="padding:5px; background-color:#ECF1F6;">
        <asp:UpdatePanel runat="server" ID="fad">
        <ContentTemplate>
        <div style="padding:5px; overflow:hidden;">
        <div style="float:left; overflow:auto; padding-right:5px; margin:2px;min-width:400px;">
            <div style="display:inline-block;width:100px;"><b>Select Office : </b></div>
            <div style="display:inline-block;"><asp:DropDownList runat="server" ID="ddlOffice" Width="300px"></asp:DropDownList></div>
        </div>
        <div style="float:left;overflow:auto; margin:2px; min-width:400px; ">
            <div style="display:inline-block;width:100px;"><b>Select Position :</b>  </div>
            <div style="display:inline-block;"><asp:DropDownList runat="server" ID="ddlPosition" Width="300px"></asp:DropDownList> </div>
        </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <div style="border-bottom:solid 1px #cccccc; padding:5px;"><b>Select Vessel Types : </b></div>
        <div style="margin:2px; overflow:auto; overflow-y:scroll; max-height:200px;">
        <asp:Repeater runat="server" ID="rptVTypes">
            <ItemTemplate>
                <div style="float:left;width:230px;"><asp:CheckBox runat="server" ID="chkVType" Text='<%#Eval("VesselTypeName") %>' CssClass='<%#Eval("VesselTypeId") %>' /></div>
            </ItemTemplate>
        </asp:Repeater>
        </div>
         <div style="border-top:solid 1px #cccccc; padding:5px;">
           <asp:Button Text="Show Report" runat="server" ID="btnShow" OnClick="btnShow_Click" CommandArgument="R" />
           <asp:Button Text="Export To Excel" runat="server" ID="btnExcel" OnClick="btnShow_Click"  CommandArgument="E"/>
           <asp:Label runat="server" ID="lblMessage" ForeColor="Red" Font-Bold="true" ></asp:Label> 
         </div>
        </div>
        <div style="padding:5px; border-top:solid 1px #4371a5;">
            <iframe style="width:100%" height="400px" runat="Server" id="frmReport">
            </iframe>      
        </div>
    </div>  
    </form>
</body>
</html>

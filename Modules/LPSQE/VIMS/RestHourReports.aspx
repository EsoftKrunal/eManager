<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestHourReports.aspx.cs" Inherits="Vims_RestHourReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipSoft- PMS - Rest Hour Entry</title>
    <script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    </head>

<body style="font-size:13px; color:#333">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align: center; padding:8px; background-color:#3f90ff;color:white; font-size:17px;">Rest Hour Entry - Reports </div>
    <div style="text-align: center" id="content">
       
            <div style="text-align: center;background-color:#d4d1d1; padding:5px;">
            <b> Month & Year :  </b>
            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlYear" CssClass="control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonthYear_Changed"></asp:DropDownList>
            </div>
         
        
             <div>
                <asp:Button runat="server" ID="btnShow" Text=" Save " CssClass="btn1" OnClientClick="ReadytoSave();" OnClick="btnShow_Click" />
                
            </div>
           

            <div style="text-align:left; padding:5px;">
                Any change in date line please click on the save button to save the record. 
                </div>
            <div style="text-align:left; padding:5px;">
                <asp:Label runat="server" ForeColor="Red" Font-Bold="true" ID="lblMsg" Text=""></asp:Label>
                </div>
        </div>
    
         <div style="display:none; "  id="dvModal">
            <div style="position:absolute;top:0px; left:0px; width:100%; height:100%; z-index:50; background-color:black; opacity: .5;filter: alpha(opacity=50);" ></div>
            <div style="position : absolute; top:170px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue;z-index:52; ">
            <center>

            </center>
            </div>
         </div>

    </form>
    <script type="text/javascript">
        var lastday = null;
        var drag = -1;
        $(document).ready(function () {

            $(".day>input").click(function () {
                $("#hfdid").val($(this).attr('id'));
                $("#btnpost").click();
            });

           
        });

       
       
    </script>
</body>
</html>

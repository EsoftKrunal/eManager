<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RHMissingLog.aspx.cs" Inherits="VIMS_RHMissingLog" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    </head>
    <style type="text/css">
        .bordered tr td{
            padding:8px;
            color:#333;
            border:solid 1px #e1dada;
        }
        .missing {
            width:10px;height:10px;background-color:#f84e4e;
        }
        .present {
            width:10px;height:10px;background-color:#58d43a;
        }
        .my_bordered {
            
            border-collapse:collapse;
        }
        .my_bordered td{
            border:solid 1px #efe9e9;
            padding:5px;
            text-align:center;
        }
        
        .NC {
            background-color:#ea4040;
        }
    </style>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid;" width="100%">
            <tr>
                <td style=" background-color:#4371a5; color:White; font-size:14px; padding:7px;text-align:center; font-weight:bold; ">
                    Crew List ( Missing Log in <asp:Label runat="server" ID="lblmy"></asp:Label>  )
                </td>
            </tr>
        </table>
            <div style="padding:5px;text-align:center;">
               <b> Select Crew : </b><asp:DropDownList runat="server" ID="ddlcrew" style="font-size:13px;padding:4px;" AutoPostBack="true" OnSelectedIndexChanged="ddlcrew_OnSelectedIndexChanged"></asp:DropDownList>
            </div>

            <asp:Literal ID="litData" runat="server"></asp:Literal>

            <table cellpadding="0" cellspacing="0" border="0" style="text-align:left; border-collapse:collapse;display:none;" width="100%" class="bordered">   
                 <tr style="background-color:#e4c82c;color:white; font-weight:bold;">
                    <td style="width:100px;">Crew#</td>
                    <td>Crew Name</td>
                     <td>Rank Name</td>
                    <td style="width:100px;">Missing Date</td>
                </tr>                         
                <asp:Repeater runat="server" ID="rptLog">
                    <ItemTemplate>
                    <tr>
                    <td><%#Eval("CrewNumber")%></td>
                    <td><%#Eval("CrewName")%></td>
                    <td><%#Eval("RankName")%></td>
                    <td><%#Common.ToDateString(Eval("FDATE"))%></td>
                    </tr>
                        </ItemTemplate>
                </asp:Repeater>
                </table>     

        </td>
        </tr>
        </table>  
     </div>     
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NM_Report.aspx.cs" Inherits="Reports_NM_Report" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Near Miss Report</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function CheckAll(self)
        {
            for(i=0;i<=document.getElementById("chklst_Vsls").cells.length-1;i++)  
            {
                if(document.getElementById("chklst_AllVsl").checked==true)
                {
                    document.getElementById("chklst_Vsls_" + i).checked=true;
                } 
                else
                {
                    document.getElementById("chklst_Vsls_" + i).checked=false;
                }
            }
        }
        function UnCheckAll(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
        {
            for(i=0;i<=document.getElementById("chklst_Vsls").cells.length-1;i++)
            {
                if(document.getElementById("chklst_Vsls_" + i).checked==false)
                {
                     document.getElementById("chklst_AllVsl").checked=false;
                }
            }        
        }
        function CheckAllCat(self)
        {
            for(i=0;i<=document.getElementById("ckhlst_Cat").cells.length-1;i++)  
            {
                if(document.getElementById("chkls_AllCat").checked==true)
                {
                    document.getElementById("ckhlst_Cat_" + i).checked=true;
                } 
                else
                {
                    document.getElementById("ckhlst_Cat_" + i).checked=false;
                }
            }
        }
        function UnCheckAllCat(selfid) //if any internal checkbox is unchecked then select all will also become unchecked
        {
            for(i=0;i<=document.getElementById("ckhlst_Cat").cells.length-1;i++)
            {
                if(document.getElementById("ckhlst_Cat_" + i).checked==false)
                {
                     document.getElementById("chkls_AllCat").checked=false;
                }
            }        
        }
    </script>

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" valign="top" style="height: 235px">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
                        <tr>
                            <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">Near Miss Report</td>
                        </tr>
                        <tr>
                            <td style="height: 10px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td style="width: 50px; text-align: left">
                                        </td>
                                        <td style="text-align: left">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; text-align: left">
                                        </td>
                                        <td style="text-align: left">
                                            &nbsp;<asp:CheckBox ID="chklst_AllVsl" runat="server" onclick="javascript:CheckAll(this);" Text="All Vessels" /></td>
                                        <td style="text-align: left">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            </td>
                                        <td style="text-align: left;">
                                            </td>
                                        <td style="text-align: left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px; text-align: left">
                                        </td>
                                        <td rowspan="8" style="padding-right: 10px; text-align: left" valign="top">
                                            <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                overflow-x: hidden; border-left: #8fafdb 1px solid; width: 218px; border-bottom: #8fafdb 1px solid;
                                                height: 120px; text-align: left">
                                                <asp:CheckBoxList ID="chklst_Vsls" runat="server" onclick="return UnCheckAll(this);"
                                                    Width="216px">
                                                </asp:CheckBoxList></div>
                                        </td>
                                        <td rowspan="8" style="text-align: left" valign="top">
                                                <asp:DropDownList ID="ddl_Cat" runat="server" Width="216px" CssClass="input_box" >
                                                    <asp:ListItem Value="1">Report by Category</asp:ListItem>
                                                    <asp:ListItem Value="2">Report by Root Cause</asp:ListItem>
                                                </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; padding-right: 5px;" valign="top">
                                            From Date :</td>
                                        <td style="text-align: left;" valign="top">
                                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="input_box" Width="89px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"
                                                TabIndex="-1" /></td>
                                        <td style="text-align: left" valign="top">
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: right; padding-right: 5px;" valign="top">
                                            To Date :</td>
                                        <td style="text-align: left" valign="top">
                                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="input_box" Width="89px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"
                                                TabIndex="-1" /></td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click" Text="Show" Width="75px" /></td>
                                        <td style="text-align: left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td style="text-align: left;" valign="bottom">
                                            &nbsp;</td>
                                        <td style="text-align: left" valign="bottom">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: right" valign="top">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left;">
                                            </td>
                                        <td style="text-align: left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50px">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="1" style="width: 50px">
                                        </td>
                                        <td colspan="4">
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txt_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td colspan="1">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px; text-align: left">
                                <iframe runat="server" id="IFRAME1" frameborder="0" style="width: 100%; height: 460px;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

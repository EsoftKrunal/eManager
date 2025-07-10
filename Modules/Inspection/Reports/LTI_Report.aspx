<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LTI_Report.aspx.cs" Inherits="Reports_LTI_Report" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Lost Time Incidents</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function CheckAll(self)
        {
            for(i=0;i<=document.getElementsByTagName("input").length-1;i++)  
            {
                if(document.getElementsByTagName("input").item(i).getAttribute("type")=="checkbox" && document.getElementsByTagName("input").item(i).getAttribute("id")!=self.id)
                {
                    document.getElementsByTagName("input").item(i).checked=self.checked;
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
    </script>
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
                            <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">LTI Report</td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
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
                                        <td style="padding-right: 5px; width: 173px; text-align: right">
                                        </td>
                                        <td style="width: 243px; text-align: left">
                                            &nbsp;<asp:CheckBox ID="chklst_AllVsl" runat="server" onclick="javascript:CheckAll(this);" Text="All Vessels" /></td>
                                        <td style="padding-right: 5px; width: 75px; text-align: right">
                                            </td>
                                        <td style="text-align: left">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 173px">
                                        </td>
                                        <td style="width: 243px; text-align: left" valign="top">
                                            <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                overflow-x: hidden; border-left: #8fafdb 1px solid; width: 218px; border-bottom: #8fafdb 1px solid;
                                                height: 120px; text-align: left">
                                                <asp:CheckBoxList ID="chklst_Vsls" runat="server" onclick="return UnCheckAll(this);"
                                                    Width="216px">
                                                </asp:CheckBoxList></div>
                                        </td>
                                        <td style="width: 75px; text-align: right;" valign="top">
                                            Year :</td>
                                        <td style="text-align: left" valign="top">
                                            <asp:DropDownList ID="ddl_Year" runat="server" CssClass="input_box">
                                                <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                                <asp:ListItem Value="2005">2005</asp:ListItem>
                                                <asp:ListItem Value="2006">2006</asp:ListItem>
                                                <asp:ListItem Value="2007">2007</asp:ListItem>
                                                <asp:ListItem Value="2008">2008</asp:ListItem>
                                                <asp:ListItem Value="2009">2009</asp:ListItem>
                                                <asp:ListItem>2010</asp:ListItem>
                                                <asp:ListItem>2011</asp:ListItem>
                                                <asp:ListItem>2012</asp:ListItem>
                                                <asp:ListItem>2013</asp:ListItem>
                                                <asp:ListItem>2014</asp:ListItem>
                                            </asp:DropDownList><br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click"
                                                Text="Show" Width="75px" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                &nbsp;</td>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetManipulation.aspx.cs" Inherits="Vessel_BudgetManipulation" Culture="auto" UICulture="auto" %>
<%@ Register Src="VesselBudget.ascx" TagName="VesselBudget" TagPrefix="uc2" %>
<%@ Register Src="VesselDocuments.ascx" TagName="VesselDocuments" TagPrefix="uc1" %>
<%@ Register Src="VesselDetailsGeneral.ascx" TagName="VesselDetailsGeneral" TagPrefix="uc3" %>
<%@ Register Src="VesselDetailsOther.ascx" TagName="VesselDetailsOther" TagPrefix="uc4" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/Gridstyle.css" />
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style=" text-align:center">
        <tr>
            <td align="center" valign="top" >
            </td>
        </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width:100% ;border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >Vessel Budget Manipulation</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="">&nbsp;</td></tr>
                        <tr><td style="padding-right:10px; text-align:center; color:Red; height: 13px;"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td></tr>
                        <tr><td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left; width: 825px;">
                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; height :80px;">
                            <legend><strong>Budget Manipulation</strong></legend>
                            <center >
                            <table>
                            <tr><td colspan="7">&nbsp;</td></tr>
                            <tr>
                            <td>Veseel : </td>
                            <td>
                            <asp:DropDownList ID="ddl_VesselName" runat="server" Width="220px" CssClass="input_box" ></asp:DropDownList> 
                            </td>
                            <td>Budget Year : </td>
                            <td>
                            <asp:DropDownList runat="Server" ID="ddlYear" CssClass="input_box"></asp:DropDownList> 
                            </td>
                            <td>Budget Type :</td>
                            <td>
                               <asp:DropDownList ID="ddlBudgetType" runat="server" CssClass="input_box" Width="112px" ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" OnClick="btn_Show_Click" Text="Show" CssClass="btn" Width="59px" />
                            </td>
                            </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_VesselName"
                                            ErrorMessage="Required." MaximumValue="9999" MinimumValue="1" Type="Integer"
                                           ></asp:RangeValidator></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlBudgetType"
                                            ErrorMessage="Required." MaximumValue="9999" MinimumValue="1" Type="Integer"
                                            ></asp:RangeValidator></td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            </center>
                            </fieldset> 
                        </td></tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px; text-align: center;">
                                
                                <asp:GridView ID="gv_Budget" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Width="100%">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FromDate" HeaderText="From Month"></asp:BoundField>
                                        <asp:BoundField DataField="ToDate" HeaderText="To Month"></asp:BoundField>--%>
                                        <asp:BoundField DataField="AccountHead" HeaderText="Acc.Head"></asp:BoundField>
                                        <asp:TemplateField HeaderText="JAN">
                                        <ItemTemplate >
                                        <asp:HiddenField runat="server" ID="hfd_VBId" Value='<% #Eval("VESSELBUDGETID") %>' />
                                        <asp:TextBox runat="Server" ID="txt_0" CssClass="input_box" Text='<%# Eval("JAN") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FEB">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_1" CssClass="input_box" Text='<%# Eval("FEB") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAR">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_2" CssClass="input_box" Text='<%# Eval("MAR") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="APR">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_3" CssClass="input_box" Text='<%# Eval("APR") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAY">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_4" CssClass="input_box" Text='<%# Eval("MAY") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JUN">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_5" CssClass="input_box" Text='<%# Eval("JUN") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JUL">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_6" CssClass="input_box" Text='<%# Eval("JUL") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AUG">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_7" CssClass="input_box" Text='<%# Eval("AUG") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SEP">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_8" CssClass="input_box" Text='<%# Eval("SEP") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OCT">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_9" CssClass="input_box" Text='<%# Eval("OCT") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NOV">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_10" CssClass="input_box" Text='<%# Eval("NOV") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DEC">
                                        <ItemTemplate ><asp:TextBox runat="Server" ID="txt_11" CssClass="input_box" Text='<%# Eval("DEC") %>' MaxLength="8" style="text-align :right; width :70px;"></asp:TextBox></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                                    </Columns>
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <PagerStyle CssClass="pagerstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" HorizontalAlign="Center" />
                                    <RowStyle CssClass="rowstyle" />
                                </asp:GridView>
                                <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding-top: 5px">
                        Total Annual Budget :
                        <asp:Label ID="lbl_Total" runat="server"></asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
                                <br />
                                <asp:Button ID="btnSave" runat="server" OnClick="btn_Save_Click" Text="Save" CssClass="btn" Width="59px" />
                                
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
                                        

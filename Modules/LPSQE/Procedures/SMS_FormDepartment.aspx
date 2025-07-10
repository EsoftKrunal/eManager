<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMS_FormDepartment.aspx.cs" Inherits="Modules_LPSQE_Procedures_SMS_FormDepartment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style.css" />
</head>
<body>
    <form id="form1" runat="server">
       
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    
    <div style="text-align:left">
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center; border-top:solid 1px black;">
                <table cellpadding="0" cellspacing="0" style="width:100%">
                    <colgroup>
                    <col style="width:860px;" />
                    <col />
                       
                    <tr>
                    <td style="padding:3px;" >
                        <div style="width:100%; height: 29px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
                        <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="0">
                            <colgroup>
                        <col width="40px" />
                        <col />
                         <col width="150px" />
                                </colgroup>
                        <tr class= "headerstylegrid" >
                            <td><b>&nbsp;Edit</b></td>
                            <td><b>Forms Department</b></td>
                            <td><b>Department ShortName</b></td>
                        </tr>
                        </table>
                        </div>
                        <div style="width:100%; height: 240px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
                        <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="1">
                            <colgroup>
                        <col width="40px" />
                        <col />
                        <col width="150px" />
                                 </colgroup>
                            <asp:Repeater ID="rptFormDepartment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" OnClick="btnEdit_OnClick" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.jpg" ToolTip="Edit" Visible='<%#Auth.IsUpdate %>'/>                                            
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnDepartmentId" runat="server" Value='<%# Eval("DepartmentId") %>' /><%#Eval("DepartmentName")%>
                                        </td>
                                        <td>
                                            <%#Eval("DepartmentShortName")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        
                            </table>
                        </div>
                    </td>
                    <td style="vertical-align:top;">                        
                            
                                
                    </td>
                    </tr>
                       </colgroup>  
                </table>
            <%--<asp:HiddenField runat="server" ID="hfdManualId" />--%>
            </td>
         </tr>
         <tr>
            <td style="text-align: right;padding:5px;border:#8fafdb 1px solid;">
            <asp:Panel runat="server" ID="p1" Visible="false">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-collapse:collapse">
                <tr>
                <td>
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                <tr>
                    <td style="width:100px; text-align:left"> Department Name :&nbsp;</td>
                    <td style="text-align:left; width:380px;"><asp:TextBox runat="server" ID="txtFormDepartmentName" MaxLength="100" Width="300px" CssClass="required_box"></asp:TextBox> &nbsp; <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtFormDepartmentName" ErrorMessage="Required."></asp:RequiredFieldValidator>
                    </td>
                    <td style="width:100px; text-align:left"> Department ShortName :&nbsp;</td>
                     <td style="text-align:left; width:380px;"><asp:TextBox runat="server" ID="txtFormDepartmentShortName" MaxLength="3" Width="300px" CssClass="required_box"></asp:TextBox> &nbsp; <asp:RequiredFieldValidator runat="server" ID="r2" ControlToValidate="txtFormDepartmentShortName" ErrorMessage="Required."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        
                    </td>
                   
                </tr>
                </table>
                </td>
                <td>
                </td>
                </tr>
                </table>
            </asp:Panel>
            <div style="margin-top:5px;" >
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" style="float:left;"></asp:Label>

            <asp:Button runat="server" ID="btnAdd" onclick="btnAdd_Click" Text="Add New" CssClass="btn" />&nbsp;
            <asp:Button runat="server" ID="btnSave" onclick="btnSave_Click" Text="Save" CssClass="btn" Visible="false" />&nbsp;
            <asp:Button runat="server" ID="btnCancel" onclick="btnCancel_Click" Text="Cancel" CssClass="btn" Visible="false" CausesValidation="false"/>&nbsp;
            </div>
            </td>
         </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMS_ManualCat.aspx.cs" Inherits="SMS_ManualCat" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style.css" />
    <script type="text/javascript">
        function CheckUnCheckAll(obj) 
        {
            var ChkBox = document.getElementById('dvPopupEditVessel').getElementsByTagName('input');
            for (var i = 0; i < ChkBox.length; i++) {
                if (ChkBox[i].type == "checkbox") {
                    ChkBox[i].checked = obj.checked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    
    <div style="text-align:left">
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center; border-top:solid 1px black;">
                <table cellpadding="0" cellspacing="0" style="width:100%">
                    <col style="width:860px;" />
                    <col />
                    <tr>
                    <td style="padding:3px;" >
                        <div style="width:100%; height: 29px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
                        <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="0">
                        <col width="40px" />
                        <col />
                        <tr class= "headerstylegrid" >
                            <td><b>&nbsp;Edit</b></td>
                            <td><b>Manual Category</b></td>
                        </tr>
                        </table>
                        </div>
                        <div style="width:100%; height: 240px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #2F9DBA">
                        <table cellpadding="2" cellspacing="0" rules="rows" style="text-align:left; border-collapse:collapse;" width="100%" border="1">
                        <col width="40px" />
                        <col />
                        <col width="100px" />
                            <asp:Repeater ID="GridView_ManualsCat" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" OnClick="btnEdit_OnClick" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.jpg" ToolTip="Edit" Visible='<%#Auth.IsUpdate %>'/>                                            
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="Hidden_ManualCatId" runat="server" Value='<%# Eval("ManualCatId") %>' /><%#Eval("ManualCatName")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>
                    </td>
                    <td style="vertical-align:top;">                        
                            <div style="width:100%;overflow-y:hidden;overflow-x:hidden;" id="divManualVesselGrid" runat="server" visible="false">                                
                                <div style="width:100%;overflow-y:hidden;overflow-x:hidden;" >
                                    <table cellpadding="2" cellspacing="2" rules="rows" style="text-align:left;" width="100%">
                                        <col width="60px" />
                                        <col />
                                        <tr class= "headerstylegrid" >
                                            <td><b> Sr#</b> </td>
                                            <td><b> Manual Name</b> </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="width:100%;overflow-y:scroll;overflow-x:hidden;height:200px;" >
                                <table cellpadding="2" cellspacing="2" rules="rows" style="text-align:left;" width="100%">
                                        <col width="60px" />
                                        <col />
                                    <asp:Repeater ID="grdManualVessel" runat="server" >
                                        <ItemTemplate>
                                            <tr >
                                                <td> <%#Eval("RowNo")%> </td>
                                                <td> <%#Eval("ManualName")%> </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                </div>
                                <asp:Button ID="btnEditVessels" runat="server" Text=" Modify Manual List" CssClass="btn" OnClick="btnEditManuals_OnClick" />
                                </div>
                                
                    </td>
                    </tr>
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
                    <td style="width:100px; text-align:left">Manual Category</td>
                    <td style="text-align:left; width:380px;">:&nbsp;<asp:TextBox runat="server" ID="txtManualCatName" MaxLength="100" Width="300px" CssClass="required_box"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtManualCatName" ErrorMessage="Required."></asp:RequiredFieldValidator>
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

    <%--Check list box--%>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPopupEditVessel" runat="server" visible="false">
        <center>
        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; height:320px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">
            <div style="margin:5px;">
                
                <div class="text headerband" style="width:100%;text-align:center;"> <b> Select Manual </b></div>
                <div style="width:100%;border-bottom:solid 1px #c2c2c2;text-align:left;padding-left:4px;"> 
                    <input type="checkbox" onclick="CheckUnCheckAll(this)"  />
                    <b> Select All </b>
                </div>
                <div style="width:100%; height:230px; overflow-x:hidden;overflow-y:scroll;text-align:left;border:solid 1px #c2c2c2;">
                    <asp:CheckBoxList runat="server" RepeatDirection="Vertical" RepeatColumns="1" ID="chkManusls" DataValueField="ManualID" DataTextField="ManualName"></asp:CheckBoxList>
                </div>
            </div>
            <asp:Button ID="btnSavePopupEditVessel" runat="server" Text=" Save " CssClass="btn" OnClick="btnSavePopupEditVessel_OnClick" />
            <asp:Button ID="btnClosePopupEditVessel" runat="server" Text=" Close " CssClass="btn" OnClick="btnClosePopupEditVessel_OnClick" />
        </div>   
        </center>
    </div>
    </form>
</body>
</html>


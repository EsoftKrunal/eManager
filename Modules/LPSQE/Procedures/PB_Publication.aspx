<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PB_Publication.aspx.cs" Inherits="PB_Publication"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
   <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <div>
    <div style="min-height:425px;" style="width:100%;border-bottom:solid 1px gray;"> 
    <div style="padding:0px; text-align:right">
    <table width="100%" cellpadding="3" style="text-align:center; border-collapse:collapse;text-align:left; background-color:#E0EBFF;" border="1"> 
    <tr>
    <td>Publication :</td>
        <td><asp:TextBox runat="server" ID="txtPubName_F" MaxLength="100"></asp:TextBox></td>
    <td>Office / Ship :</td>
        <td>
            <asp:DropDownList ID="ddlOfficeShip_F" runat="server" Width="100px" >
                <asp:ListItem Text="< -- All -- >" Value="0"></asp:ListItem>
                <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                <asp:ListItem Text="Ship" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </td>
    <td>Type :</td>
        <td><asp:DropDownList ID="ddlType_F" runat="server" Width="150px" ></asp:DropDownList></td>
    <td>Mode :</td>
        <td><asp:DropDownList ID="ddlMode_F" runat="server" Width="100px" ></asp:DropDownList></td>
    <td>Publisher :</td>
        <td><asp:DropDownList ID="ddl_Publisher_F" runat="server" Width="200px" ></asp:DropDownList></td>
    <td>
        <asp:Button runat="server" id="btnSearch" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" CssClass="btn"  />
        <asp:Button runat="server" id="btnAdd" Text="Add Publication" OnClick="btnAdd_Click" CausesValidation="false" CssClass="btn"  />
    </td>
    </tr>
    </table>
    </div>
    <div style=" overflow-x:hidden; overflow-y:scroll; height:22px; border:solid 1px gray;border-bottom:none;">
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left; background-color:Wheat;" border="1"> 
        <colgroup>
            <col width="50px" />
            <col width="50px" />
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="250px" />
            <col width="100px" />
        </colgroup>
            <tr class= "headerstylegrid">
                <td style="text-align:center;">Edit</td>
                <td style="text-align:center;">Delete</td>
                <td>Publication</td>
                <td>Type</td>
                <td>Office/Ship</td>
                <td>Mode</td>
                <td>Publisher</td>
                <td>New Edition</td>
            </tr>
    </table>
    </div>
    <div style=" overflow-x:hidden; overflow-y:scroll; height:334px; border:solid 1px gray; border-top:none;">
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1"> 
        <colgroup>
            <col width="50px" />
            <col width="50px" />
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="250px" />
            <col width="100px" />
        </colgroup>
        <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align:center;">
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("PublicationId")%>' ForeColor="Blue" OnClick="lnlEdit_OnClick" Text="Edit" CausesValidation="false"></asp:LinkButton>
                    </td>
                    <td style="text-align:center;">
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("PublicationId")%>' ForeColor="Red" OnClick="lnlDelete_OnClick" OnClientClick="return window.confirm('Are you sure to delete?');" Text="Delete" CausesValidation="false"></asp:LinkButton>
                    </td>
                    <td><%#Eval("PublicationName")%></td>
                    <td><%#Eval("TYPENAME")%></td>
                    <td><%#Eval("OfficeShip")%></td>
                    <td><%#Eval("MODENAME")%></td>
                    <td><%#Eval("PUBLISHERNAME")%></td>
                    <td>&nbsp;&nbsp;
                        <asp:LinkButton ID="lnlRelease" runat="server" CommandArgument='<%#Eval("PublicationId")%>' ForeColor="Blue" OnClick="lnlRelease_OnClick" OnClientClick="return window.confirm('Are you sure to release new version?');" Text="Release" CausesValidation="false"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>
    <!-- Add / Edit Publication -->
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPopUp" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:310px; padding :5px; text-align :center; border :solid 0px #FFADD6; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                 <div>
                 <div style="padding:4px; " class="text headerband"> Add / Edit Publication</div>
                 <div style="border:solid 1px #66E0FF; height:285px;">
                    <br />
                    <table cellpadding="1" cellspacing="0" border="0" width="100%" style="text-align:left">
                        <tr>
                        <td> &nbsp;</td>
                            <td>
                                Publication Name :
                            </td>
                        <td style="text-align:left"> 
                            <asp:TextBox runat="server" ID="txtPublicationName" Width="395px" MaxLength="100" style=" background-color:#FFFFC2"></asp:TextBox> 
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="txtPublicationName"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp; &nbsp;</td>
                        <td>Publication Type :</td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlType" runat="server" Width="400px" style=" background-color:#FFFFC2"></asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlType" Operator="GreaterThan" ErrorMessage="*" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>Publication Mode :</td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlMode" runat="server" Width="400px" style=" background-color:#FFFFC2"></asp:DropDownList>
                            <asp:CompareValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlMode" Operator="GreaterThan" ErrorMessage="*" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>Publisher :</td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlPublisher" runat="server" Width="400px" ></asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>Office / Ship :</td>
                        <td style="text-align:left">
                            <asp:DropDownList ID="ddlOfficeShip" runat="server" Width="400px" style=" background-color:#FFFFC2">
                                <asp:ListItem Text="< -- Select -- >" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                                <asp:ListItem Text="Ship" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Both" Value="B"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlOfficeShip" Operator="GreaterThan" ErrorMessage="*" ValueToCompare="0"></asp:CompareValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>
                            Required By :
                        </td>
                        <td style="text-align:left">
                            <asp:CheckBoxList runat="server" ID="chkRequiredBy" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>
                        </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                Edition ( Year ):
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtEditionYear" runat="server" MaxLength="4" style="background-color:#FFFFC2" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEditionYear" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>
                            Edition ( # ):
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtEditionNo" Width="100px" MaxLength="50" style="background-color:#FFFFC2"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEditionNo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>
                            Validity Date :
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtValidityDate" Width="100px" MaxLength="15" ></asp:TextBox> 
                            <asp:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MMM-yyyy" TargetControlID="txtValidityDate"></asp:CalendarExtender>
                        </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button runat="server" id="btnSave" Text=" Save " OnClick="btnSave_Click" CssClass="btn"/> 
                    <asp:Button runat="server" id="btnClose" Text=" Close " OnClick="btnClose_Click" CssClass="btn" CausesValidation="false" /> 
                 </div>
                 </div>
            </div>
        </center>
      </div>
    <!-- Releaase new version -->
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPopUp1" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:210px; padding :5px; text-align :center; border :solid 0px #FFADD6; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                 <div>
                 <div style="padding:4px; " class="text headerband"> Release New Edition</div>
                 <div style="border:solid 1px #66E0FF; height:185px;">
                    <br />
                    <table cellpadding="1" cellspacing="0" border="0" width="100%" style="text-align:left">
                        <tr>
                        <td> &nbsp;</td>
                            <td style='width:250px;'>
                                Publication Name :
                            </td>
                            <td style="text-align:left"> 
                                <asp:Label runat="server" ID="lblPubName"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                New Edition ( Year ):
                            </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtNewEditionYear" runat="server" MaxLength="4" style="background-color:#FFFFC2" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewEditionYear" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>
                            New Edition ( # ):
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtNewEditionNo" Width="100px" MaxLength="50" style="background-color:#FFFFC2"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewEditionNo" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>
                            New Validity Date :
                        </td>
                        <td style="text-align:left">
                            <asp:TextBox runat="server" ID="txtNewValidityDate" Width="100px" MaxLength="15" ></asp:TextBox> 
                            <asp:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MMM-yyyy" TargetControlID="txtNewValidityDate"></asp:CalendarExtender>
                        </td>
                        </tr>
                        </table>
                        <br />
                        <asp:Button runat="server" id="btnRelease" Text=" Save " OnClick="btnRelease_Click" CssClass="btn"/> 
                        <asp:Button runat="server" id="btnClose1" Text=" Close " OnClick="btnClose1_Click" CssClass="btn" CausesValidation="false" /> 
                </div>
                </div>
            </div>
        </center>
        </div>                
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

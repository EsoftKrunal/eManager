<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupPositionMaster.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopupPositionMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             window.opener.document.getElementById("btnhdn").click();
             window.close();
         }
     </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
            <tr>
               <td valign="top" style="border:solid 1px #4371a5; height:80px;">
                    <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                        Add/Modify Position  
                    </div>
                    <table width="100%" cellpadding="2" cellspacing ="0" border="0">
                               <tr>
                                   <td style="text-align :right">
                                       Position Code :</td>
                                   <td style="text-align :left">
                                        <asp:TextBox ID="txtPosCode" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                   </td>
                                   <td style="text-align :right">
                                       Manager :</td>
                                   <td style="text-align :left">
                                        <asp:RadioButtonList runat="server" ID="radmangaer" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected></asp:ListItem>
                                        </asp:RadioButtonList>
                                        </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                        &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                        &nbsp;</td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                   Position Name : </td>
                                   <td style="text-align :left">
                                   <asp:TextBox ID="txtPosName" runat="server" Width="200px" required='yes' MaxLength="50" ></asp:TextBox>
                                   </td>
                                   <td style="text-align :right">
                                       Inspector :</td>
                                   <td style="text-align :left">
                                        <asp:RadioButtonList runat="server" ID="radInspector" 
                                           RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0" Selected></asp:ListItem>
                                        </asp:RadioButtonList>
                                   </td>
                               </tr>
                                <tr>
                               <td style="text-align :right" valign="top">
                                   &nbsp;</td>
                               <td style="text-align :left" colspan="3">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                               ControlToValidate="txtPosName" ErrorMessage="Required."></asp:RequiredFieldValidator>
                               </td>
                               </tr>
                               <tr>
                                <td> Vessel Position </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlVesselPosition" RepeatDirection="Horizontal"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                               </tr>
                               
                               <tr>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:HiddenField id="hdnYear" runat="server"/>
                                       
                                       <asp:HiddenField id="hdnOfficeId" runat="server"/>
                                   </td>
                                   <td style="text-align :left">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                       
                                        <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" 
                                        onclick="btnsave_Click"></asp:Button>
                                        <asp:Button ID="btnCancel" CssClass="btn"  runat="server" Text="Close" CausesValidation="false"   
                                            onclick="btnCancel_Click" OnClientClick="CloseWindow();">
                                        </asp:Button>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceManagement.aspx.cs" Inherits="Invoice_InvoiceManagement" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/jquery.min.js" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div style="font-family:Arial;font-size:12px;">
    <center>
    <div style="border:solid 1px #008AE6;">
    <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>
        <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td class="text headerband">
             <strong>Approval Rights Management</strong></td>
         </tr>
           <tr>
               <td style=" background-color:#FFFFCC">
               &nbsp;
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
               <td>
                   <table width="100%">
                    <tr>
                        <td style="text-align:right;">
                            Office :
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlOffice" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged" runat="server"></asp:DropDownList>
                        </td>
                        <td style="text-align:right;">
                            Position :
                        </td>
                        <td style="text-align:left;">
                            <asp:DropDownList ID="ddlPosition" AutoPostBack="true" OnSelectedIndexChanged="ddlPosition_OnSelectedIndexChanged" Width="150px" runat="server">
                            <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                   </table>
               </td>
           </tr>
         <tr>
           <td>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 50px ; text-align:center; border-bottom:none;">
                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:50px;  border-collapse:collapse;">
                    <colgroup>
                    <col style="width:20px;"/>
                    <col style=" text-align:left;padding-left:10px;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:25px;"/>
                    </colgroup>
                    <tr style="text-align:center;" class= "headerstylegrid">
                        <td colspan="2">

                        </td>
                        <td colspan="7">
                            Payment Approval
                        </td>
                        <td colspan="2">
                            Vendor Approval
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr align="left" class= "headerstylegrid" > 
                    <td>&nbsp;</td>
                    <td>&nbsp;User Name</td>
                    <td>Entry</td>
                    <td>Approval-1</td>
                    <td>Approval-2</td>
                    <td>Approval-3</td>
                    <td>Approval-4</td>
                    <td>Payment</td>
                    <td>Cancel</td>
                    <td>Approval-1</td>
                    <td>Approval-2</td>
                    <td>&nbsp;</td>
                    </tr>
            </table>
            </div>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;" class="ScrollAutoReset">
            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
            <colgroup>
                    <col style="width:20px;"/>
                    <col style=" text-align:left;padding-left:10px;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:75px; text-align:center;" />
                    <col style="width:25px;"/>
            </colgroup>
            <asp:Repeater runat="server" ID="rptUsers">
            <ItemTemplate>
            <tr >
                <td style="width:20px;">&nbsp;</td>
                <td style=" text-align:left;padding-left:10px;">&nbsp;<%#Eval("FirstName")%>&nbsp;<%#Eval("LastName")%><asp:HiddenField runat="server" ID="hfdUserId" Value='<%#Eval("LoginId")%>' />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Entry").ToString()=="True"%>' ID="chk_Entry" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval").ToString()=="True"%>' ID="chk_App" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Verification").ToString()=="True"%>' ID="chk_Verify" />
                </td>
                 <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval3").ToString()=="True"%>' ID="chk_App3" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval4").ToString()=="True"%>' ID="chk_App4" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Payment").ToString()=="True"%>' ID="chkPayment" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Cancel").ToString()=="True"%>' ID="chkCancel" />
                </td>
                  <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("VendorApproval1").ToString()=="True"%>' ID="chkVendorApp1" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("VendorApproval2").ToString()=="True"%>' ID="chkVendorApp2" />
                </td>
                <td>&nbsp;</td>
            </tr>
            </ItemTemplate>
             <AlternatingItemTemplate>
            <tr >
                <td style="width:20px;">&nbsp;</td>
                <td style=" text-align:left;padding-left:10px;">&nbsp;<%#Eval("FirstName")%>&nbsp;<%#Eval("LastName")%><asp:HiddenField runat="server" ID="hfdUserId" Value='<%#Eval("LoginId")%>' />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Entry").ToString()=="True"%>' ID="chk_Entry" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval").ToString()=="True"%>' ID="chk_App" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Verification").ToString()=="True"%>' ID="chk_Verify" />
                </td>
                 <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval3").ToString()=="True"%>' ID="chk_App3" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Approval4").ToString()=="True"%>' ID="chk_App4" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Payment").ToString()=="True"%>' ID="chkPayment" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("Cancel").ToString()=="True"%>' ID="chkCancel" />
                </td>
                 <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("VendorApproval1").ToString()=="True"%>' ID="chkVendorApp1" />
                </td>
                <td>
                    <asp:CheckBox runat="server" Checked='<%#Eval("VendorApproval2").ToString()=="True"%>' ID="chkVendorApp2" />
                </td>
                <td>&nbsp;</td>
            </tr>
            </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
            </div>
            <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style="text-align:center">
                     <asp:Button ID="btn_Save" runat="server" Text="Save" Width="150px" OnClick="btn_Save_Click" style="  border:none; padding:4px;" OnClientClick="this.value='Loading..Please Wait';" CssClass="btn"/>
                     <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" OnClientClick="window.close()" style="  border:none; padding:4px;" CssClass="btn"/>--%>
                  </td>
                </tr>
              </table>
           </td>
         </tr>
       </table>
       </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </center>
    </div>
 </asp:Content>

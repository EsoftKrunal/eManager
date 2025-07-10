<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeStatus.aspx.cs" Inherits="Registers_ChangeStatus" Title="Change Status" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
        <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" ></ajaxToolkit:ToolkitScriptManager> 
    <center>
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
        <tr>
            <td colspan="5" style=" height:23px; color :White; text-align :center " class="text headerband"><b>Change Status</b></td>
        </tr>
        <tr>
            <td>
            <table cellpadding="0" cellspacing="0" style="width: 95%;">
             <tr>
                <td colspan="4" >
                  <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <asp:Panel ID="pnl_ChaptersEntry" runat="server" Width="100%">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          &nbsp;</td>
                        <td colspan="1" style="height: 15px">
                        </td>
                        <td colspan="1" style="height: 15px">
                        </td>
                        <td colspan="1" style="height: 15px">
                        </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Inspection#:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtInspectionNo" runat="server" CssClass="input_box" MaxLength="49"
                                  TabIndex="1" AutoPostBack="True" OnTextChanged="txtInspectionNo_TextChanged" Width="214px"></asp:TextBox></td>
                          <td style="text-align: right; padding-right: 15px;">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList id="ddlStatus" runat="server" CssClass="input_box" tabIndex="2" Width="210px">
                              </asp:DropDownList></td>
                          <td style="text-align: left">
                              <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="3" OnClick="btn_Save_Click" />
                              <asp:Button ID="btn_Cancel" runat="server" CausesValidation="False" CssClass="btn" TabIndex="3" Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" /></td>
                      </tr>
                                                        </table>
                                                    
           <table style="width: 100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td style="text-align: right; padding-right: 17px;">
                    <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
                       </td>
               </tr>
           </table>
           
           <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="padding-right: 5px; padding-left: 5px;">
                    <asp:HiddenField id="HiddenField_Status" runat="server">
                    </asp:HiddenField><asp:HiddenField id="HiddenField_InspDueId" runat="server"></asp:HiddenField>&nbsp;&nbsp;</td>
            </tr>
           </table>&nbsp;
           </asp:Panel>
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
                </td>
                </tr> 
                <tr>
                <td>
                     <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                </td>
                </tr> 
        </table>
            </td>
        </tr>
        </table>
    </center>
</form>
</body>
</html>



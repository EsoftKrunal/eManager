<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDocumentsDetail.aspx.cs" Inherits="CrewRecord_CrewDocumentsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        .style1
        {
            width: 147px;
            height: 14px;
        }
        .style2
        {
            width: 270px;
            height: 14px;
        }
        .style3
        {
            width: 42px;
            height: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <div style="text-align: center;font-family:Arial;" >
     
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9; width: 85%; height: 341px;">
                         <tr>
                <td align="center"  class="text headerband" >
                    Candidate Documents</td>
            </tr>
                <tr>
                    <td style="height: 318px" >
                    <table cellpadding="0" cellspacing="0">
                    <tr><td>  
                       
                        <asp:Label ID="lbl_info" runat="server" Width="303px" ForeColor="Red"></asp:Label></td></tr>
                    <tr><td style="padding-right: 10px; padding-left: 10px;text-align: center;" align="center">
                  
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                    <legend><strong>Documents </strong></legend>
                        <br />
                   
                      <div style="overflow:auto; width:785px; height:135px;" >
                         <table><tr>
                             <td style="width: 147px; height: 23px" align="left">
                                 <asp:CheckBox ID="chkpassport" runat="server" Text="Passport" /></td>
                             <td style="height: 23px; width: 270px;" align="left">
                                 <asp:CheckBox ID="chkvisa" runat="server" Text="Visa" /></td>
                             <td style="width: 42px; height: 23px" align="left">
                                 <asp:CheckBox ID="chkseamanbook" runat="server" Text="Seaman Book" Width="113px" /></td>
                         </tr>
                             <tr>
                                 <td style="width: 147px; height: 14px" align="left">
                                     <asp:CheckBox ID="chklicense" runat="server" Text="License" /></td>
                                 <td style="width: 270px; height: 14px" align="left">
                                     <asp:CheckBox ID="chkcourse" runat="server" Text="Course & Certificate" /></td>
                                 <td align="left" style="width: 42px; height: 14px">
                                     <asp:CheckBox ID="chkcargo" runat="server" Text="Cargo Endorsement" Width="163px" /></td>
                             </tr>
                             <tr>
                                 <td align="left" class="style1">
                                     <asp:CheckBox ID="chkother" runat="server" Text="Other Documents" /></td>
                                 <td class="style2">
                                 </td>
                                 <td align="center" class="style3">
                                 </td>
                             </tr>
                         </table>
                    </div>
                    </fieldset>
                  

         <table width="100%" style="background-color:#f9f9f9;" cellpadding="0" cellspacing="0"  >
     
             <tr id="Trcargo" runat="server">
                 <td align="right" style="height: 50px; text-align: right">
          
                 </td>
             </tr>
             <tr>
                <td align="right" style="text-align: right; height: 50px;">
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button  ID="btn_New" runat="server" Width="70px" Text="Send Mail" CssClass="btn"  CausesValidation="False" OnClientClick="javascript:shownew();" OnClick="btn_New_Click" />
                    <asp:Button  ID="Button1" runat="server" Width="70px" Text="Back" CssClass="btn"  CausesValidation="False" OnClick="Button1_Click" /></td>
            </tr></table> </td></tr></table>

                    </td>
                </tr>
            </table>
           
        </div>   
    </form>
</body>
</html>

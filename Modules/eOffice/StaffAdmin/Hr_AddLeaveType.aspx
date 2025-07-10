<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_AddLeaveType.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_AddLeaveType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Type</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table width="100%">
        <tr>
            <td valign="top" style="border:solid 1px #4371a5; height:100px;">
                <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                    Add/Update Leave Type
                </div>
                <table id="tblLeaveType" runat="server" width="100%" cellspacing ="0" cellpadding="2" border="0">
                    <tr>
                        <td style ="text-align:right;"><span lang="en-us">&nbsp;</span>&nbsp;</td>
                        <td style="text-align :left; width:200px;">
                            &nbsp;</td>
                   </tr>
                    <tr>
                        <td style ="text-align:right;">Leave Type :</td>
                        <td style="text-align :left; width:200px;">
                          <asp:TextBox ID="txtLeaveType" runat="server" required='yes' Width="180px" ></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvLeaveType" runat="server" ControlToValidate="txtLeaveType" ErrorMessage="*"></asp:RequiredFieldValidator>  
                       </td>
                   </tr>
                    <tr>
                        <td style ="text-align:right;"><span lang="en-us">&nbsp;</span>&nbsp;</td>
                        <td style="text-align :left; width:200px;">
                            &nbsp;</td>
                   </tr>
                   <tr>
                        <td style="text-align :center" colspan="2">
                                <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click"></asp:Button>
                                <input type="button" onclick="window.opener.document.getElementById('btnhdn').click(); window.close();" value="Close" class="btn" />
                         </td>
                    </tr>
                   <tr>
                        <td style="text-align :center" colspan="2">
                                <span lang="en-us">&nbsp;</span>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr> 
      </table>                       
    </div>
    </form>
</body>
</html>

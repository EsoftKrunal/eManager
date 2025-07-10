<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dummy.aspx.cs" Inherits="CrewApproval_Dummy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; padding-right: 10px; padding-left: 10px; width:100%" >
            <tr>
                <td style="background-color:#4371a5; height: 23px; text-align:center; width: 980px;" class="text" >
                    Crew Approval</td>
            </tr>
            <tr>
                <td style="width: 980px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_msg" runat="server" Text="You Are Not Authorized To View This Page." Font-Size="12px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
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

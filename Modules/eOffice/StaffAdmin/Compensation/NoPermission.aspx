<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoPermission.aspx.cs" Inherits="emtm_StaffAdmin_NoPermission" %>

<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pay Slip</title>
    <link href="./style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" >
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    
                <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                            <div style="padding:10px;font-size:22px;text-align:center;margin-top:100px;"> You have not sufficient permission to view this page. </div>
                            <br />
                            
                            <div style="padding:10px;font-size:12px;text-align:center;margin-top:20px;"> <a href="#" onclick="GoBack()" style="cursor:pointer;"> Go Back </a> </div>
                        </td>
                    </tr>
            </table>     
    </div>

    <script type="text/javascript">
        function GoBack() {
            history.go(-1);
        }
    </script>
    </form>
</body>
</html>

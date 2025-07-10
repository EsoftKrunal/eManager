<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_CompanyHoliday.aspx.cs" Inherits="Emtm_Profile_CompanyHoliday" EnableEventValidation="false" %>
<%@ Register src="Profile_ExperienceHeaderMenu.ascx" tagname="Profile_ExperienceHeaderMenu" tagprefix="uc2" %>
<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Report</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj,Mode,Office) 
        {
            winref = window.open('../MyProfile/Profile_ShoreExperience.aspx?ShoreId=' + obj + ' &Mode=' + Mode + ' &Office=' + Office, '', 'title=no,toolbars=no,scrollbars=yes,width=800,height=220,left=250,top=150,addressbar=no,resizable=1,status=0');
             return false;   
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
          <center>
          <table width="98%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top" style="border:solid 1px #4371a5;" >
                           <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                               Holiday List
                           </div>
                           <div style=" font-size :15px; padding-left : 15px; text-align :center; padding :5px;"  >
                               <asp:Label runat="server" ID="lblPageheader" Font-Size="Large"></asp:Label> 
                           </div>
                           <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:25px;" />
                                <tr align="left" class="blueheader" style="height:25px">
                                    <td>
                                        Date From</td>
                                    <td>
                                        Date To</td>
                                    <td>
                                        Holiday</td>    
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </colgroup>
                            </table> 
                            </div>          
                            <div id="divinfo" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 345px ; text-align:center;">
                            <table border="1" cellpadding="5" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptHoliday" runat="server"> 
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%#Eval("FromDate")%></td>
                                    <td align="center">
                                        <%#Eval("ToDate")%></td>    
                                    <td align="left">
                                        <%#Eval("HolidayReason")%></td>    
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div>
                        </td>
                    </tr>
                </table>
          </center> 
          </ContentTemplate>
          </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

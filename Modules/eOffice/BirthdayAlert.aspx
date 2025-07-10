<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BirthdayAlert.aspx.cs" Inherits="Emtm_BirthdayAlert" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Office Absence</title>
    <style>
     .monthtd
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #C2C2C2;
     	cursor:pointer;  
     }
     .monthtdselected
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #E5A0FC;
     	cursor:pointer;
     }
     .box_
     {
     	background-color :White; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_H
     {
     	background-color :Orange; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_W
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px gray;
     }	
     .box_L
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px gray;
     }
     .box_P
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_B
     {
     	background-color :Purple;
     	color :White; 
     	border :solid 1px gray;
     }
     </style> 
     <script language="javascript" type="text/javascript">
         function CloseWindow() {             
             window.close();
         }

         function showLeaveDays() {
             //document.getElementById("btnhdn").click();
         }

         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('txtMonthId').setAttribute('value', Mnth);
             document.getElementById('hdnShowMonth').click();
         } 
     </script>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
                      <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :6px; font-weight: bold;">Birthday Alert</div>
                      <table cellpadding="0" cellspacing="3" border="0" width="100%">
                      <tr>
                      <td style="padding:5px; color:#555555; font-size:14px;">Today's Birthday</td>
                      <td style="padding:5px; color:#555555; font-size:14px;">This Month's Birthday</td>
                      </tr>
                      <tr>
                      <td style="width:50%; vertical-align:top">
                        <div style="overflow-x:hidden; overflow-y:scroll; max-height:360px; border:solid 1px #dddddd;">
                        <asp:GridView runat="server" ID="gv1" CellPadding="4" GridLines="Both" Width='100%' BackColor="White" BorderColor="#dddddd" BorderStyle="None"  BorderWidth="1px">
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="#eeeeee" ForeColor="Black" />
                            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>                        
                        </div>
                      </td>
                        <td style="width:50%; vertical-align:top">
                        <div style="overflow-x:hidden; overflow-y:scroll; max-height:360px; border:solid 1px #dddddd;">
                            <asp:GridView runat="server" ID="gv2" CellPadding="4" ForeColor="Black" GridLines="Both" Width='100%' BackColor="White" BorderColor="#dddddd" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <AlternatingRowStyle BackColor="#eeeeee" ForeColor="Black" />
                            <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>      
                        </div>
                        </td>
                      </tr>
                      </table>                        
                  
    </div>
    </form>
</body>
</html>

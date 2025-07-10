<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHome1.aspx.cs" Inherits="emtm_MyProfile_Emtm_PopupLeaveRequest1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Management</title>
     <link href="style.css" rel="stylesheet" type="text/css" />
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
     .box_LO
     {
     	background-color :#24dbbf;
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
     .box_C
     {
     	background-color :#B8FF94; 
     	color :Black; 
     	border :solid 1px gray;
     }
     </style> 
     <script language="javascript" type="text/javascript">
         function CloseWindow()
         {
             //window.opener.document.getElementById("btnhdn").click();
             window.close();
         }

         function showLeaveDays() {
             document.getElementById("btnhdn").click();
         }

         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('txtMonthId').setAttribute('value', Mnth);
             document.getElementById('hdnShowMonth').click();
         } 
     </script>
</head>
<body style=" padding :10px;" >
    <form id="form1" runat="server">
    <div style="border:solid 1px #4371a5" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
        &nbsp;Office Absence
        </td>
        </tr>
        </table> 
        <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:100px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img src="../Images/Emtm/loading.gif" alt="Loading..."/> Loading ...
            </div>
            </center>
        </div>
        </center>
        </ProgressTemplate>
        </asp:UpdateProgress>   
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style =" background-color : #75B2DD;">
        <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
        <td>Location : </td><td>
        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td>Department : </td><td><asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" ></asp:DropDownList></td>
        <td>
        <asp:DropDownList runat="server" ID="ddlYear" Width="60px" AutoPostBack="True" onselectedindexchanged="ddlYear_SelectedIndexChanged"></asp:DropDownList> 
        </td>
        <td style="display:none;">
           <asp:TextBox ID="txtMonthId" runat="server"></asp:TextBox>
           <asp:Button ID="hdnShowMonth" runat="server" CausesValidation="false" onclick="hdnShowMonth_Click"/> 

        </td>
        <td style="width:60%" >
            <%--<div style="color:white;font-size:16px;font-weight:bold;float:right;">
                Office Absence : <asp:Literal ID="lblOfficeAbsenceCount" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;&nbsp;
                Office Absence : 15
            </div>--%>
        </td>
                
            
        </tr>
        </table>
        </td>
        </tr>
        
        <tr>
        <td>
            
            <table cellpadding="3" cellspacing="0" border="0" style="border-collapse :collapse; text-align :center;" width="100%">
<tr>
<td id="td" runat="server" class="monthtd" width="200px"></td>
<td id="tdJan" runat="server" class="monthtd" onclick="ShowCurrentMonth(1,this);">Jan</td>
<td id="tdFeb" runat="server" class="monthtd" onclick="ShowCurrentMonth(2,this);">Feb</td>
<td id="tdMar" runat="server" class="monthtd" onclick="ShowCurrentMonth(3,this);">Mar</td>
<td id="tdApr" runat="server" class="monthtd" onclick="ShowCurrentMonth(4,this);">Apr</td>
<td id="tdMay" runat="server" class="monthtd" onclick="ShowCurrentMonth(5,this);">May</td>
<td id="tdJun" runat="server" class="monthtd" onclick="ShowCurrentMonth(6,this);">Jun</td>
<td id="tdJul" runat="server" class="monthtd" onclick="ShowCurrentMonth(7,this);">Jul</td>
<td id="tdAug" runat="server" class="monthtd" onclick="ShowCurrentMonth(8,this);">Aug</td>
<td id="tdSep" runat="server" class="monthtd" onclick="ShowCurrentMonth(9,this);">Sep</td>
<td id="tdOct" runat="server" class="monthtd" onclick="ShowCurrentMonth(10,this);">Oct</td>
<td id="tdNov" runat="server" class="monthtd" onclick="ShowCurrentMonth(11,this);">Nov</td>
<td id="tdDec" runat="server" class="monthtd" onclick="ShowCurrentMonth(12,this);">Dec</td>
<td class="monthtd" width="200px" >&nbsp;</td>
</tr>
</table>            
            <asp:Literal ID="MonthView" runat="server"></asp:Literal>              
                    
            
            
        </td>
        </tr>
            <tr>
        <td style =" background-color : #75B2DD; text-align:center; text-transform :uppercase; font-weight:normal; font-size :6px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td width="201px" style=" text-align:left">&nbsp;</td>
        <td class="box_H" style="border:none;font-size :9px;">Holiday</td>
        <td class="box_W" style="border:none;font-size :9px;">Weekly Off</td>
        <td style="color:White;border:none; font-size :9px;background-color :Green">Approved Leave (Annual)</td>
        <td class="box_LO" style="border:none;font-size :9px;">Other Leave </td>
        <td class="box_P" style="border:none;font-size :9px;">Planned Leave</td>
        <td class="box_B" style="border:none;font-size :9px;">Business Trip</td>
            
        </tr>
        </table>
        </td>
        </tr>
        </table> 
        </ContentTemplate> 
        </asp:UpdatePanel>
        <br />
    </div>
    </form>
</body>
</html>

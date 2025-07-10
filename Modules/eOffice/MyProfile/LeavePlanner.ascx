<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeavePlanner.ascx.cs" Inherits="emtm_MyProfile_LeavePlanner" %>
 <link href="../style.css" rel="stylesheet" type="text/css" />
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
     	border :solid 1px #F0F0F5;
     }
     .box_H
     {
     	background-color :Orange; 
     	color :Black; 
     	border :solid 1px #F0F0F5;
     }
     .box_H_S
     {
     	background-color :Orange; 
     	color :Black; 
        border :solid 1px blue;
        border-right :solid 1px #F0F0F5;
     }
     .box_H_M
     {
     	background-color :Orange; 
     	color :Black; 
        border :solid 1px blue;
        border-left :solid 1px #F0F0F5;
        border-right :solid 1px #F0F0F5;
     }
     .box_H_E
     {
     	background-color :Orange; 
     	color :Black; 
        border :solid 1px blue;
        border-left :solid 1px #F0F0F5;
     }
     .box_W
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px #F0F0F5;
     }	
     .box_W_S
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px blue;
     	border-right :solid 1px #F0F0F5;
     }	
      .box_W_M
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px blue;
     	border-right :solid 1px #F0F0F5;
     	border-left :solid 1px #F0F0F5;
     }	
      .box_W_E
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     }	
     .box_L
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_L_S
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px blue;
     	border-right :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_L_M
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     	border-right :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_L_E
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_P
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px #F0F0F5;
     	cursorcursor:pointer;
     }
     .box_P_S
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px blue;
     	border-right :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_P_M
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     	border-right :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_P_E
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     	cursor:pointer;
     }
     .box_B
     {
     	background-color :#FF70B8;
     	color :White; 
     	border :solid 1px #F0F0F5;
     }
     
     .box_B_S
     {
     	background-color :#FF70B8;
     	color :White; 
     	border :solid 1px blue;
     	border-right :solid 1px #F0F0F5;
     }
     .box_B_M
     {
     	background-color :#FF70B8;
     	color :White; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     	border-right :solid 1px #F0F0F5;
     }
     .box_B_E
     {
     	background-color :#FF70B8;
     	color :White; 
     	border :solid 1px blue;
     	border-left :solid 1px #F0F0F5;
     }
     
      .box_C
     {
     	background-color :#B8FF94; 
     	color :Black; 
     	border :solid 1px #F0F0F5;
     }
     </style>
     <script language="javascript" type="text/javascript">
         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('LeavePlanner1_txtMonthId').setAttribute('value', Mnth);
             document.getElementById('LeavePlanner1_hdnShowMonth').click();
         }
         function CallPost(Lrid) {
             document.getElementById("LeavePlanner1_hfd_LRId").setAttribute("value",Lrid);
             document.getElementById("LeavePlanner1_btn_Post").click();
         } 
     </script> 
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
        &nbsp;Leave Planner  
        </td>
        </tr>
        </table> 
        <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:100px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img src="../../Images/Emtm/loading.gif" alt="Loading..."/> Loading ...
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
        <table cellpadding="5" cellspacing="0" border="0">
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
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td style =" background-color : #75B2DD; text-align:center; text-transform :uppercase; font-weight:bold; font-size :13px;">
        <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
        <td width="201px" style=" text-align:left">&nbsp;</td>
        <td class="box_H" style="border:none;">Holiday</td>
        <td class="box_W" style="border:none;">Weekly Off</td>
        <td style="color:White;border:none; background-color :Green">Approved Leave</td>
        <td class="box_P" style="border:none;">Planned Leave</td>
        <td class="box_B" style="border:none;">Business Trip</td>
        </tr>
        </table>
            <asp:Button runat="server" ID="btn_Post" OnClick="Leave_Selected" Text="Post" style='display:none'/>
            <asp:HiddenField runat="server" ID="hfd_LRId" />
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
        </table> 
        </ContentTemplate> 
        </asp:UpdatePanel>
       
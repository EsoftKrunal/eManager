<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_Peap.aspx.cs" Inherits="emtm_MyProfile_Emtm_Profile_Peap" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 

        <div style="font-family:Arial;font-size:12px;">
        
          <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div style="">
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
                            <tr>
                                <td> 
                                   <div class="text headerband" style=" text-align :left; font-size :14px;  padding :3px; font-weight: bold;">
                                        Peap Details : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                                  </div>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing ="0" border="1" style=" border-collapse:collapse;" width ="100%">
                                <tr>
                                   <td style="background-color:#d2d2d2; text-align:left; height:25px; font-size:14px; font-weight:bold; padding-left:5px; font-style:italic;">My Peap Records</td>
                                </tr>
                                <tr>
                                 <td>
                                   <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td colspan="6">
                                                <div style=" width:100%;  overflow:hidden;" > 
                                                <center>
                                                    <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                </center>
                                               
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                   <col style="width:25px;" />
                                                   <col style="width:100px;" />
                                                   <col style="width:160px;" />
                                                   <col style="width:80px;"/>
                                                   <col style="width:110px;"/>
                                                   <col />
                                                   <col style="width:25px;" />
                                                   <tr align="left" class= "headerstylegrid"> 
                                                       <td></td>   
                                                       <td>Peap Level</td>
                                                       <td>Appraisal Type</td>
                                                       <td>From</td>
                                                       <td>To</td>
                                                       <td> Status </td>
                                                       <td>&nbsp;</td>
                                                   </tr>
                                           </table>
                                           </div>           
                                               <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 100px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                   <col style="width:25px;" />
                                   <col style="width:100px;" />
                                   <col style="width:160px;" />
                                   <col style="width:80px;"/>
                                   <col style="width:110px;"/>
                                   <col />
                                   <col style="width:25px;" />
                                   </colgroup>
                                                   
                                   
                                 <asp:Repeater ID="rptData" runat="server" >
                               <ItemTemplate>
                                  <tr class='<%# (Common.CastAsInt32(Eval("PEAPID"))==PeapID)?"selectedrow":"row"%>'>
                                       <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"/></td>
                                       <td align="center"><%#Eval("CATEGORY")%></td>
                                       <td align="left"><%#Eval("AppraiselType")%></td>
                                        <td align="center"><%#Eval("PEAPPERIODFROM")%></td>
                                        <td align="center"><%#Eval("PEAPPERIODTO")%></td>
                                        <td align="center"><%#Eval("STATUS")%></td>
                                           
                                       <td>&nbsp;</td>
                                   </tr>
                               </ItemTemplate>
                              </asp:Repeater>
                                                </table>
                                    </div>
                                                </div> 
                                               
                                           
                                 </td>
                                 </tr>
                                  <tr>
                                   <td style="background-color:#d2d2d2; text-align:left; height:25px; font-size:14px; font-weight:bold; padding-left:5px; font-style:italic;">Team Appraisal</td>
                                </tr>
                                <tr>
                                 <td>
                                   <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td colspan="6">
                                                <div style=" width:100%; overflow:hidden;" > 
                                                <center>
                                                    <asp:Label ID="Label1" runat="Server"></asp:Label>
                                                </center>
                                                <div style="padding:5px 5px 5px 5px;" >
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                   <col style="width:25px;" />
                                                  <%-- <col style="width:60px;" />--%>
                                                   <col style="width:100px;" />
                                                   <col style="width:160px;" />
                                                   <col />
                                                   <col style="width:80px;" />
                                                   <%--<col style="width:115px;" />--%>
                                                   <%--<col style="width:80px;" />--%>
                                                   <col style="width:80px;"/>
                                                   <col style="width:80px;"/>
                                                   <col style="width:200px;"/>
                                                   
                                                   <col style="width:25px;" />
                                                   <tr align="left" class= "headerstylegrid"> 
                                                       <td></td>   
                                                       <%--<td>EMP#</td>--%>
                                                       <td>Peap Level</td>
                                                       <td>Appraisal Type</td>
                                                       <td>Name</td>
                                                       <td>Office</td>
                                                       <%--<td>Position</td>--%>
                                                       <%--<td>Dept.</td>--%>
                                                       <td>From</td>
                                                       <td>To</td>
                                                       <td>Status</td>
                                                       <td>&nbsp;</td>
                                                   </tr>
                                           </table> 
                                           </div>          
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 260px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup> 
                                   <col style="width:25px;" />
                                  <%-- <col style="width:60px;" />--%>
                                   <col style="width:100px;" />
                                   <col style="width:160px;" />
                                   <col />
                                   <col style="width:80px;" />
                                   <%--<col style="width:115px;" />--%>
                                   <%--<col style="width:80px;" />--%>
                                   <col style="width:80px;"/>
                                   <col style="width:80px;"/>
                                   <col style="width:200px;"/>
                                   <col style="width:25px;" />
                                   </colgroup>
                                 <asp:Repeater ID="rptAssessment" runat="server" >
                               <ItemTemplate>
                                  <tr class='<%# (Common.CastAsInt32(Eval("PEAPID"))==PeapID)?"selectedrow":"row"%>'>
                                       <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif"/></td>
                                       
                                       <%--<td align="left"><%#Eval("EMPCODE")%></td>--%>
                                       <td align="left"><%#Eval("CATEGORY")%></td>
                                        <td align="left"><%#Eval("AppraiselType")%></td>
                                       <td align="left"><%#Eval("EMPNAME")%></td>
                                       <td align="left"><%#Eval("OFFICENAME")%></td>
                                       
                                        <%--<td align="left"><%#Eval("POSITIONNAME")%></td>--%>
                                        <%--<td align="left"><%#Eval("DEPARTMENTNAME")%></td>--%>
                                        <td align="center"><%#Eval("PEAPPERIODFROM")%></td>
                                        <td align="center"><%#Eval("PEAPPERIODTO")%></td>
                                        <td align="center"><%#Eval("STATUS")%></td>
                                           
                                       <td>&nbsp;</td>
                                   </tr>
                               </ItemTemplate>
                              </asp:Repeater>
                                                </table>
                                                
                                            
                                 </td>
                                 </tr>
                                 <tr>
                                 <td>
                                    </td>
                                 </tr>
                                 </table>
                           
         
         
    
    </div>

   </asp:Content>

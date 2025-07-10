<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_RunningHour.aspx.cs" Inherits="Office_RunningHour" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
     
     <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
    </script>
    <style>
    input
    {
    	background-color:white;  
    }
    
    input:active
    {
    	background-color:Yellow;
    }
    input:focus
    {
    	background-color:Yellow;
    }
    </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
<%--<body>
    <form id="form1" runat="server" defaultbutton="btnShow">--%>
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" AsyncPostBackTimeout="300" runat="server"></asp:ToolkitScriptManager>
             <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
           Running Hour Update
     </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                            <td style="padding-right: 10px;padding-left:2px">
                            <div style="width:100%; height:452px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
             <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>
             <asp:UpdatePanel runat="server" id="up1" UpdateMode="Always">
                <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                            <tr>
                            <td style="text-align :left; padding-left:5px;">
                             <div style="height:5px;"></div>
      <asp:Panel ID="PlRunningHour" runat="server" Width="100%">
     <%--<div style=" padding-left:200px; padding-top:3px; padding-bottom:3px;">
        <asp:DropDownList ID="ddlVessels" runat="server" Width="272px" ></asp:DropDownList>  <asp:DropDownList runat="server" id="ddlyears"></asp:DropDownList
     </div>--%>
     <div>
     <table cellpadding="2" cellspacing="0" width="100%" >
     <tr>
         <td style=" text-align:right ; width :195px;">Vessel :</td>
         <td style=" text-align:left; width :272px; "><asp:DropDownList ID="ddlVessels" runat="server" Width="272px" ></asp:DropDownList> </td>
         <td style=" text-align:right ; width :195px;">Year :</td>
         <td style=" text-align:left;"><asp:DropDownList runat="server" id="ddlyears"></asp:DropDownList></td>
         <td style=" text-align:left;">
         <asp:Button Text="Show" runat="server" ID="btnShow" cssClass="btn" style="width:80px; height:20px;  background-color : LightGray" OnClick="Show_Click" />
     <asp:Button ID="btnPring" Text=" Print" CssClass="btn" style="width:80px; height:20px; margin-right:7px;  background-color : LightGray" runat="server" onclick="btnPring_Click" />
         </td>
     </tr>
     <%--<tr>
     <td style=" text-align:right ; width :195px;">Component Code :</td>
     <td style=" text-align:left; width :105px; "> <asp:TextBox runat="server" ID="txtCompCode" MaxLength="15" ></asp:TextBox>
     
      </td>
     <td style=" text-align:right ; width :195px;">Component Name :</td>
     <td style=" text-align:left;"> <asp:TextBox runat="server" ID="txtCompName" MaxLength="50" ></asp:TextBox>
      
     <asp:Button Text="Show" runat="server" ID="btnShow" cssClass="btn" style="width:80px; height:20px;  background-color : LightGray" OnClick="Show_Click" />
     <asp:Button ID="btnPring" Text=" Print" CssClass="btn" style="width:80px; height:20px; margin-right:7px;  background-color : LightGray" runat="server" onclick="btnPring_Click" />
     </td>
     </tr>--%>
     </table>
     </div>
     <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%" >
                             <tr>
                             <td>
           <div id="dv1" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 23px ; text-align:center;vertical-align:top;">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <%--<col style="width:25px;" />--%>
                   <col />
                   <col style="width:150px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:17px;" />
             </colgroup>
                   <tr align="left" class="headerstylegrid">
                  <%-- <td>&nbsp;</td>--%>
                   <td>Component Code & Name </td>
                   <td>Last R.H./ As On</td>
                   <td>Jan</td>
                   <td>Feb</td>
                   <td>Mar</td>
                   <td>Apr</td>
                   <td>May</td>
                   <td>Jun</td>
                   <td>Jul</td>
                   <td>Aug</td>
                   <td>Sep</td>
                   <td>Oct</td>
                   <td>Nov</td>
                   <td>Dec</td>  
                   <td>&nbsp;</td>  
                   </tr>
           </table>           
           </div>
           <div id="dvRH" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                   <col />
                   <col style="width:150px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:60px;" />
                   <col style="width:17px;" />
             </colgroup>
               <asp:Repeater ID="rptRunningHour" runat="server">
                  <ItemTemplate>
                      <tr class="">
                           <td align="left">[ <b><%#Eval("ComponentCode")%> </b>] <%#Eval("ComponentName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           <asp:HiddenField ID="hfRhCompId" Value='<%#Eval("ComponentId")%>' runat="server" />
                            <asp:HiddenField ID="hfdRHCcode" Value='<%#Eval("ComponentCode")%>' runat="server" />
                           </td>
                           <td ><%#Eval("LASTHOURS")%> / <%#Common.ToDateString(Eval("LASTHOURDATE"))%></td>
                           <td><%#Eval("JAN_CONS")%></td>
                           <td><%#Eval("FEB_CONS")%></td>
                           <td><%#Eval("MAR_CONS")%></td>
                           <td><%#Eval("APR_CONS")%></td>
                           <td><%#Eval("MAY_CONS")%></td>
                           <td><%#Eval("JUN_CONS")%></td>
                           <td><%#Eval("JUL_CONS")%></td>
                           <td><%#Eval("AUG_CONS")%></td>
                           <td><%#Eval("SEP_CONS")%></td>
                           <td><%#Eval("OCT_CONS")%></td>
                           <td><%#Eval("NOV_CONS")%></td>
                           <td><%#Eval("DEC_CONS")%></td>
                           <td>&nbsp;</td>
                       </tr>

                      
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
            
           </div>
           </td>
          </tr>
          <tr>
              <td>
                 <div style="padding-top:5px; float:left;">
                     <uc1:MessageBox ID="msgRunHour" runat="server" />
                 </div> 
                 <div style="padding-top:5px; float:right;">
                 <asp:Label runat="server" ID="lblCount" Font-Bold="true"></asp:Label>
                 </div>
              </td>
          </tr>
          </table>
    
    </asp:Panel>
     </td>
     </tr></table>
    </ContentTemplate>
             </asp:UpdatePanel>
      </div> 
      </td>
      </tr>
      </table>
      </td>
      </tr>
    </table>
    </td> 
    </tr>
    </table>
     </div>
<%--    </form>
</body>--%>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ship_RunningHour.aspx.cs" Inherits="Ship_RunningHour" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
     <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function fncInputNumericValuesOnly(evnt) {
                 if (!( event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                     event.returnValue = false;
                 }
             }
        function Post(ctl)
        {
            document.getElementById("txtMainCode").value = ctl.value;
            document.getElementById("btnMainSelect").click();
        }
    </script>
    <%--<style>
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
    </style>--%>
</head>
<body>
    <form id="form1" runat="server" >
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300"></asp:ToolkitScriptManager>
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
             <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up2" ID="UpdateProgress1">
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
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                            <tr>
                            <td style="text-align :left; padding-left:5px;">
                            <div style="height:5px;"></div>
                            <asp:Panel ID="PlRunningHour" runat="server" Width="100%">
     <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
           Running Hour Update
     </div>
     <div style=" margin-top:4px">Select Year : 
        <asp:DropDownList runat="server" id="ddlyears" AutoPostBack="true" OnSelectedIndexChanged="Onyear_Changed"></asp:DropDownList>
     </div>
     <table width="100%" >
     <tr>
     <td style="vertical-align:top">
     <div style="display:none">
           <asp:Button runat="server" ID="btnMainSelect" OnClick="btnMainSelect_Click" Text=">" style="border:solid 1px blue; background-color:orange; width:25px;" />
           <asp:TextBox runat="server" ID="txtMainCode"></asp:TextBox> 
           </div>
           <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
           <ContentTemplate>
           <div style="vertical-align:top">
           <div id="dv1" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 23px ; text-align:center;vertical-align:top;">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:25px;" />
                   <col style="width:300px;"/>
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
                   <tr align="left" class= "headerstylegrid">
                   <td>&nbsp;</td>
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
           <div id="Div1" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 280px ; text-align:center;vertical-align:top;">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                   <col style="width:25px;" />
                   <col style="width:300px;"/>
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
               <asp:Repeater ID="rptRunningHourMaster" runat="server">
                  <ItemTemplate>
                      <tr class='<%#(MainCode==Eval("ComponentCode").ToString().Trim())?"selectedrow":"row"%>'>
                           <td align="center">
                            <input type="radio" id="ckh1" name="a" onclick='Post(this);' value='<%#Eval("ComponentCode")%>' mainid='<%#Eval("ComponentId")%>' />
                            <asp:HiddenField ID="hfRhCompId" Value='<%#Eval("ComponentId")%>' runat="server" />
                            <asp:HiddenField ID="hfdRHCcode" Value='<%#Eval("ComponentCode")%>' runat="server" />
                           </td>
                           <td align="left">[ <b><%#Eval("ComponentCode")%> </b>] <%#Eval("ComponentName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           </td>
                           <td >
                           <%if (Session["UserName"].ToString().Trim().ToUpper() == "CE")
                             {%>
                                  <asp:LinkButton ID="lnkEditLasthr" OnClick="lnkEditLasthr_Click" ToolTip="Click to edit." CommandArgument='<%#Eval("ComponentCode")%>' runat="server" Text='<%#Eval("maxhour") + "/ " + Common.ToDateString(Eval("maxdate")) %>' ></asp:LinkButton>
                           <% }%>
                           <%else
                               { %>
                                    <%#Eval("maxhour").ToString() + " / " + Common.ToDateString(Eval("maxdate"))%>
                           <% }%>
                           </td>
                           <%--<td><%#getLastHours(Eval("ComponentCode").ToString())%></td>--%>
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
           </div>
            <div id="divEditRH"  style="position:absolute;top:0px;left:0px; height :120px; width:100%;z-index:100;" runat="server" visible="false" >
                 <center>
                       <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:400px; height:150px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <div style="background-color:#c2c2c2; padding:7px; text-align:center;"><b style="font-size:14px;">Edit Last Running Hrs</b></div>
                            <div style=" height:2px;"></div>
                              <table border="0" cellpadding="2" cellspacing="2" rules="all" style="width:100%;border-collapse:collapse;">
                               <tr>
                                    <td colspan="2" style=" text-align:center; font-weight:bold; color:Red;" >NOTE : This will modify last running hrs entry in the system.</td>
                               </tr>
                               <tr>
                                 <td style=" text-align:right; font-weight:bold; width:35%; " > Date :&nbsp;</td>
                                 <td style=" text-align:left;">
                                 <asp:Label ID="lblLastDt" runat="server" ></asp:Label>
                                              </td>
                               </tr>
                               <tr>
                                 <td style=" text-align:right; font-weight:bold;" > New Running Hrs :&nbsp;</td>
                                 <td style=" text-align:left;">
                                 <asp:TextBox ID="txtEditRunningHrs" runat="server" Width="60px" required="yes"></asp:TextBox>
                                              </td>
                               </tr>
                               <tr>
                               <td colspan="2" style="text-align:right; padding-right:10px;">
                                 <asp:Button ID="btnEditRH" OnClick="btnEditRH_Click" CssClass="btn" Text="Save" runat="server" />
                                 <asp:Button ID="btnCancelEditRH" OnClick="btnCancelEditRH_Click" CssClass="btn" Text="Close" runat="server" /></td>
                               </tr>
                               <tr>
                               <td colspan="2"><asp:Label ID="lblEditRHMsg" Style="color:Red; float:left;"  runat="server"></asp:Label></td>
                               </tr>
                              </table>
                        </div>
                </center>
            </div>

            <div id="divConfirm"  style="position:absolute;top:0px;left:0px; height :140px; width:100%;z-index:100;" runat="server" visible="false" >
                 <center>
                       <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:350px; height:140px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <div style="background-color:#c2c2c2; padding:7px; text-align:center;"><b style="font-size:14px;">Confirmation</b></div>
                            <div style=" height:2px;"></div>
                              <table border="0" cellpadding="2" cellspacing="2" rules="all" style="width:100%;border-collapse:collapse;">
                               <tr>
                                 <td style=" text-align:left;">
                                 <asp:Label ID="lblConfirmMsg" ForeColor="Red" Font-Size="14px" Font-Bold="true" runat="server"></asp:Label>
                                              </td>
                               </tr>
                               <tr>
                               <td style="text-align:right; padding-right:10px;">
                                 <asp:Button ID="btnYes" OnClick="btnConfirmYes_Click" CssClass="btn" Text="Yes" runat="server" />
                                 <asp:Button ID="btnNo" OnClick="btnConfirmNo_Click"  CssClass="btn" Text="No" runat="server" /></td>
                               </tr>
                               <%--<tr>
                               <td colspan="2"><asp:Label ID="Label1" Style="color:Red; float:left;"  runat="server"></asp:Label></td>
                               </tr>--%>
                              </table>
                        </div>
                </center>
            </div>


           </ContentTemplate>
           </asp:UpdatePanel>  
            <div id="divRHEntry" runat="server" style="text-align:center">
           <table  width="100%" style="font-weight:bold; border-collapse:collapse; border:solid 1px gray" border="1" cellpadding="2" cellspacing="0" >
                <tr>
                <td>
                    New Running Hrs</td>
                <td>
                    Date</td>
                <td>
                    Avg Run.Hrs /Day</td>
                <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtRhrs" runat="server" Width="60px" required="yes"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" 
                            onfocus="showCalendar('',this,this,'','holder1',5,-200,1)" Width="100px" required="yes"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAvg" runat="server" Width="60px" required="yes"></asp:TextBox>
                    </td>
                    <td>
                        <div style="float: right;">
                            <asp:Button ID="btnRunHSave" Text="Save" OnClick="btnRunHSave_Click" CssClass="btnorange"
                                Width="100px" Style="padding-right: 5px;" runat="server" />
                        </div>
                        <div style="float: left;">
                            <uc1:MessageBox ID="msgRunHour" runat="server" />
                        </div>
                    </td>
                </tr>
               <%-- <tr>
                <td colspan="3">
                 <div style="padding-top:5px; float:left;">
                     <uc1:MessageBox ID="msgRunHour" runat="server" />
                 </div> 
                 <div style="padding-top:5px; float:right;">
                 <asp:Button ID="btnRunHSave" Text="Save" OnClick="btnRunHSave_Click" CssClass="btnorange" Width="100px" style="padding-right:5px;" runat="server" />
                 </div>
                </td>
                </tr>--%>
           </table>
           </div>
     </td>
    
     </tr>
    </table>
    </asp:Panel>
     </td>
     </tr></table>
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
     
    </form>
   <script type="text/javascript">
        var mainid = <%=MainId%>;
        var ctls = document.getElementById("Div1").getElementsByTagName("input");
            for (i = 0; i <= ctls.length - 1; i++) {
                if (ctls[i].type == "radio") 
                {
                    if(parseInt(ctls[i].getAttribute("mainid"))== parseInt(mainid))
                    {
                        ctls[i].checked=true;
                    }
                }
            }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
    function CheckPlan()
    {
    return true; 
    }
    function openplanwindow(VC,CID,JID)
    {
       window.open('PlanMaster.aspx?VC=' + VC + '&&CID=' + CID + '&&JID=' + JID,'','resizable=1, status=1,scrollbars=0,toolbar=0,menubar=0,width=1150,height=450');
    }
    function selectAll(invoker) {             
             var inputElements = document.getElementsByTagName('input');
             for (var i = 0; i < inputElements.length; i++) {
                 if (i < 10) {
                     chkbox = document.getElementById('rptSearch' + '_ctl0' + i + '_rdoSelect');
                 }
                 else {
                     chkbox = document.getElementById('rptSearch' + '_ctl' + i + '_rdoSelect');
                 }
                 if (chkbox) {                     
                     chkbox.checked = invoker.checked;
                 }
             }
         }
     function CheckOnOff(rdoId,gridName)
        {
            var rdo = document.getElementById(rdoId);
            /* Getting an array of all the "INPUT" controls on the form.*/
            var all = document.getElementsByTagName("input");
            for(i=0;i<all.length;i++)
            {  
                /*Checking if it is a radio button, and also checking if the
                id of that radio button is different than "rdoId" */
                if(all[i].type=="radio" && all[i].id != rdo.id)
                {
                    var count=all[i].id.indexOf(gridName);
                    if(count!=-1)
                    {
                       all[i].checked=false;
                    }
                }
             }
             rdo.checked=true;/* Finally making the clicked radio button CHECKED */
        }  
        function selectcomp(ctl)
        {
          var vid = ctl.vesselid + ',' + ctl.compid + ',' + ctl.jobid;
          document.getElementById('txtSelect').value = vid;
          document.getElementById('btnSelect').click();
        }
        function selectrow(ctrl)
        {
          var ctl =ctrl.childNodes[0].childNodes[0];
          ctl.checked = true;
          selectcomp(ctl);
        }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upSelect" runat="server">
        <ContentTemplate>
         <div style="display:none">
           <asp:TextBox ID="txtSelect" runat="server"></asp:TextBox>
           <asp:Button ID="btnSelect" runat="server" onclick="btnSelect_Click" />
         </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <table style="width :100%" cellpadding="0" cellspacing="0">
         <tr><td>
        <hm:HMenu runat="server" ID="menu2" />  
        </td></tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:5px; padding-left:2px;">
                                <div id="header">
                                  <ul>
                                    <li><a href="Search.aspx"><span>Search Jobs</span></a></li>
                                    <%if (Session["UserType"].ToString().Trim() == "S")
                                      {%>
                                    <li><a href="MaintenancePlanning.aspx"><span>Report Job</span></a></li>
                                    <% }%>
                                    <li><a href="Vsl_JobDescription.aspx"><span>Job Description</span></a></li>
                                    <li><a href="Vsl_SpareRequired.aspx"><span>Spares</span></a></li>
                                    <%--<li><a href="Vsl_WorkPermit.aspx"><span>Work Permit</span></a></li>--%>
                                    <li><a href="JobUpdateHistory.aspx"><span>History</span></a></li>
                                  </ul>
                                </div>
                                <script>
                                function tabing(){
                                     var strHref = window.location.href;
	                                 var strQueryString = strHref.split(".aspx");
	                                 var aQueryString = strQueryString[0].split("/");
	                                 var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
	                                
	                                 var menunum=document.getElementById('header').getElementsByTagName('li').length;
	                                 var menu=document.getElementById('header').getElementsByTagName('li')
                                     for(x=0; x<menunum; x++)
	                                 {
	 	                                if(unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1 ){
		                                menu[x].className='current';
	                                    }
	                                 }
                                }
                                tabing();
                                </script>
                            </td>
                        </tr>
                        <tr style=" padding-top:2px;">
                            <td colspan="2" style="padding-right: 5px; padding-left: 2px;">
                            
                            <DIV style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                        <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" rules="cols" >
                        <tr style=" font-weight: bold ;"  >
                            <td id="tdByVessel" runat="server" style="text-align:left; ">&nbsp;By Vessel :</td>                                                      
                            <td style="text-align:left; ">&nbsp;By Component :</td>                            
                            <td style="text-align:left; ">&nbsp;By Job Type :</td>                                                         
                            <td style="text-align:left; ">&nbsp;By Period :</td>
                            <td style="text-align:left; ">&nbsp;By Rank :</td>
                            <%--<td style="text-align:left; ">&nbsp;By Job Status :</td>--%>
                            <td style="text-align:left; ">&nbsp;By Int. Type :</td>
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                        </tr>
                        <tr style=" background-color :#F2F2F2" >
                        <td id="tdFleet" style="text-align:left" runat="server" >
                        <table  cellpadding="0"  cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px"><asp:DropDownList ID="ddlFleet" runat="server" Width="110px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList></td>
                            </tr>
                            <tr >
                                <td style="text-align:left; font-weight:bold; padding-bottom:1px; padding-top:1px;"></td>
                            </tr>
                            <tr>
                               <td style="text-align:left; padding-left:2px"><asp:DropDownList ID="ddlVessels" 
                                       runat="server" Width="110px" ></asp:DropDownList></td>
                           </tr>
                        </table>
                        </td>
                        <td style="text-align:left"  >
                        <table  cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px">Code : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompCode" MaxLength="8" runat="server" ></asp:TextBox></td>
                           </tr>
                           <tr>
                               <td style="text-align:left; padding-left:2px" >Name : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompName" MaxLength="50" runat="server" ></asp:TextBox></td>
                           </tr>
                        </table>
                        </td>                       
                        <td style="text-align:left" >
                          <table cellpadding="0" cellspacing="0" width="100%">
                           <tr>
                              <td style="text-align:left">
                              <asp:ListBox ID="lbJobTypes" runat="server" Width="110px" SelectionMode="Multiple">                               
                              </asp:ListBox>
                              </td>                        
                           </tr>
                          </table>
                        </td>                                           
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="2" width="100%">
                           <tr>
                              <td style="text-align:right">From Date :&nbsp;</td><td><asp:TextBox ID="txtFromDt" runat="server" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" ></asp:TextBox></td>               
                           </tr>
                           <tr >
                               <td style="text-align:right">To Date :&nbsp;</td><td><asp:TextBox ID="txtToDt" runat="server" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" ></asp:TextBox></td>
                           </tr>
                          </table>
                        </td>
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="0" width="100%">
                           <tr>
                              <td style="text-align:left">
                              <asp:ListBox ID="lbAssignedTo" runat="server" Width="99px" SelectionMode="Multiple">
                              </asp:ListBox>
                              </td>                        
                           </tr>
                          </table>
                        </td>
                        <%--<td style="text-align:left" >
                            <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td>
                                          <asp:ListBox ID="lbWorkOrderStatus" runat="server" SelectionMode="Multiple" Width="117px" Visible="false">
                                              <asp:ListItem Text="Issued" Value="1" Selected="True" ></asp:ListItem>
                                              <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                              <asp:ListItem Text="Postponed" Value="3"></asp:ListItem>
                                              <asp:ListItem Text="Cancelled" Value="4"></asp:ListItem>
                                          </asp:ListBox>
                                     </td>
                                 </tr>
                          </table>
                        </td>--%>
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                               <td style="text-align:left">
                                 <asp:DropDownList ID="ddlIntType" runat="server" Width="100px" >
                                    <asp:ListItem Text="< SELECT >" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Calender" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Hour" Value="2"></asp:ListItem>
                                 </asp:DropDownList>
                               </td>
                             </tr>
                             <tr>
                             <td style="text-align:left; font-weight:bold; padding-bottom:2px; padding-top:3px;"></td>
                             </tr>
                           </table>
                        </td>
                        <td style="text-align:left" >
                            <table cellpadding="0" cellspacing="0" width="100%">
                              <tr>
                              <td style="text-align:left">
                              <asp:CheckBox ID="chkCritical" Text="Critical Jobs" runat="server" />
                              </td>                        
                              </tr>
                              <tr>
                              <td><asp:CheckBox ID="chkOverdue" AutoPostBack="true" Text="Overdue" runat="server" runat="server" OnCheckedChanged="OverDue_Changed" ></asp:CheckBox></td>                           
                              </tr>
                            </table>
                        </td>
                        <td style="text-align:center">
                        <table >
                        <tr><td><asp:Button ID="btnSearch" Text="Search" CssClass="btnorange" runat="server" onclick="btnSearch_Click" /><asp:Button ID="btnClear" Text="Clear" CssClass="btnorange" runat="server" onclick="btnClear_Click" /> </td></tr>
                        <%--<tr><td><asp:Button ID="btnNewPlan" Text="Plan Jobs" CssClass="btnorange" style="width:120px" runat="server" onclick="btnNewPlan_Click" OnClientClick="return CheckPlan();"/></td></tr>--%>
                        </table>
                        </td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width: 30px;" />
                                        <col style="width: 50px;" />
                                        <col style="width: 100px;" />
                                        <col />
                                        <col style="width: 160px;" />
                                        <col style="width: 60px;" />
                                        <col style="width: 90px;" />
                                        <col style="width: 40px;" />
                                        <col style="width: 60px;" />
                                        <col style="width: 85px;" />
                                        <col style="width: 60px;" />
                                        <col style="width: 70px;" />
                                        <col style="width: 150px;" />
                                        <col style="width: 17px;" />
                                        <tr align="left" class= "headerstylegrid">
                                            <td style="width: 20px;">
                                                <input type="radio" id="chkAll" onclick='selectAll(this)' style="display:none;" /> 
                                            </td>
                                            <td>
                                                Vessel
                                            </td>
                                            <td>
                                                Comp.Code
                                            </td>
                                            <td>
                                                Comp. Name
                                            </td>
                                            <td>
                                                Job Description
                                            </td>
                                            <td>
                                                Interval
                                            </td>                                            
                                            <td>
                                                Last Done Dt.
                                            </td>
                                            <td>
                                                Diff.
                                            </td>
                                            <td>
                                                Last Hrs
                                            </td>
                                            <td>
                                                Next Due Dt.
                                            </td>
                                            <td>
                                                Next Hrs
                                            </td>
                                            <td>
                                                Status
                                            </td>
                                            <td style="padding:0px;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                <td colspan="2" style="padding-bottom:3px;" >Job Planning</td>
                                                </tr>
                                                <tr style="border-top:solid 1px ;">
                                                <td>Rank</td>
                                                <td>Date</td>
                                                </tr>
                                                </table>
                                                <%--Job Planning <br/>
                                                <div style="width:100%;">____________________</div>
                                                <div style="padding-top:3px; padding-left:10px; float:left">&nbsp;Rank</div>
                                                <div style="padding-top:3px; padding-right:15px; float:right">Date</div>--%>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                                <div id="dvSearch" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 260px ; text-align:center;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width: 30px;" />
                    <col style="width: 50px;" />
                    <col style="width: 100px;" />
                    <col />
                    <col style="width: 160px;" />
                    <col style="width: 60px;" />
                    <col style="width: 90px;" />
                    <col style="width: 40px;" />
                    <col style="width: 60px;" />
                    <col style="width: 85px;" />
                    <col style="width: 60px;" />
                    <col style="width: 70px;" />
                    <col style="width: 150px;" />
                    <col style="width: 17px;" />
                    </colgroup>
               <asp:Repeater ID="rptSearch" runat="server">
                  <ItemTemplate>
                      <tr class="alternaterow" onclick="selectrow(this);">
                           <td align="center">
                           <input type="radio" onclick="selectcomp(this);" vesselid='<%#Eval("VesselCode")%>' compid='<%#Eval("ComponentId") %>' jobid='<%#Eval("CompJobId") %>' name="rd" id="rdSelect" >
                           </td>
                           <td align=left><%#Eval("VesselCode")%> </td>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="hfCompId" Value='<%#Eval("ComponentId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%><asp:HiddenField ID="hfVesselCode" Value='<%#Eval("VesselCode") %>' runat="server" /></td>
                           <td align="left"><span style="text-transform:uppercase"><%#Eval("JobCode")%></span>-<%#Eval("JobName")%><asp:HiddenField ID="hfjobId" Value='<%#Eval("CompJobId") %>' runat="server" />
                           </td>
                           <td align="center"><%#Eval("Interval")%>-<%#Eval("IntervalName")%></td>                           
                           <td align="center"><%#Eval("LastDone")%><asp:HiddenField ID="hfLastDone" Value='<%#Eval("LastDone") %>' runat="server" /></td>
                           <td><%#Eval("Difference")%></td>
                           <td align="center"><%# Eval("LastHour").ToString() == "0" ? "" : Eval("LastHour")%><asp:HiddenField ID="hfLastHour" Value='<%#Eval("LastHour") %>' runat="server" /></td>                           
                           <td align="center"><%#Eval("NextDueDate")%><asp:HiddenField ID="hfNextDueDate" Value='<%#Eval("NextDueDate") %>' runat="server" /></td>                           
                           <td align="center"><%#Eval("NextHour").ToString() == "0" ? "" : Eval("NextHour")%><asp:HiddenField ID="hfNextHour" Value='<%#Eval("NextHour") %>' runat="server" /></td>
                           <td align="left" class='<%# (Eval("DueStatus").ToString()=="OVER DUE")?"highlightrow":"row" %>'><asp:Label ID="woStatus" Text='<%#Eval("WorkOrderStatus")%>' runat="server"></asp:Label></td>
                           <td>
                               <%--<asp:DropDownList ID="ddlAssignTO" runat="server" DataSource="<%#BindRanksForPlanning() %>" DataTextField="RankCode" DataValueField="RankId" selectedvalue='<%#Eval("PlannedRank") %>' Width="60px"></asp:DropDownList>--%>
                                              <asp:Label ID="lblAssigneTo" Text='<%#Eval("PlannedRank") %>' style="float:left; padding-left:10px;" runat="server"></asp:Label> 
                                          <%--<asp:TextBox ID="txtPlanDate" Text='<%#Eval("PlanDate") %>' onfocus="showCalendar('',this,this,'','holder1',-205,-150,1)" MaxLength="11" Width="70px" runat="server" ></asp:TextBox>--%>
                                              <asp:Label ID="lblPlanDate" Text='<%#Eval("PlanDate") %>' style="float:right" runat="server"></asp:Label>
                                          
                           </td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                   <%--<AlternatingItemTemplate>
                       <tr class="alternaterow" >
                           <td align="center"><input type="radio" value="" name="rd" id="rdSelect" runat="server" ></td>
                           <td align=left><%#Eval("VesselCode")%> </td>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="hfCompId" Value='<%#Eval("ComponentId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%><asp:HiddenField ID="hfVesselCode" Value='<%#Eval("VesselCode") %>' runat="server" /></td>
                           <td align="left"><span style="text-transform:uppercase"><%#Eval("JobCode")%></span>-<%#Eval("JobName")%>
                           <asp:HiddenField ID="hfjobId" Value='<%#Eval("CompJobId") %>' runat="server" />
                           </td>
                           <td align="center"><%#Eval("Interval")%>-<%#Eval("IntervalName")%></td>                           
                           <td align="center"><%#Eval("LastDone")%><asp:HiddenField ID="hfLastDone" Value='<%#Eval("LastDone") %>' runat="server" /></td>
                           <td><%#Eval("Difference")%></td>
                           <td align="center"><%# Eval("LastHour").ToString() == "0" ? "" : Eval("LastHour")%><asp:HiddenField ID="hfLastHour" Value='<%#Eval("LastHour") %>' runat="server" /></td>                           
                           <td align="center"><%#Eval("NextDueDate")%><asp:HiddenField ID="hfNextDueDate" Value='<%#Eval("NextDueDate") %>' runat="server" /></td>                           
                           <td align="center"><%#Eval("NextHour").ToString() == "0" ? "" : Eval("NextHour")%><asp:HiddenField ID="hfNextHour" Value='<%#Eval("NextHour") %>' runat="server" /></td>
                           <td align="left" class='<%# (Eval("DueStatus").ToString()=="OVER DUE")?"highlightrow":"alternaterow" %>'><asp:Label ID="woStatus" Text='<%#Eval("WorkOrderStatus")%>' runat="server"></asp:Label></td>
                           <td>
                               <table>
                                      <tr>
                                          <td align="left"><asp:DropDownList ID="ddlAssignTO" runat="server" DataSource="<%#BindRanksForPlanning() %>" DataTextField="RankCode" DataValueField="RankId" selectedvalue='<%#Eval("PlannedRank") %>' Width="60px"></asp:DropDownList></td>
                                          <td align="left"><asp:TextBox ID="txtPlanDate" Text='<%#Eval("PlanDate") %>' onfocus="showCalendar('',this,this,'','holder1',-205,-150,1)" MaxLength="11" Width="70px" runat="server" ></asp:TextBox></td>
                                      </tr>
                               </table>
                           </td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                           </tr>
                    </AlternatingItemTemplate>--%>
                  </asp:Repeater>
              </table>
           </div>
                            </td>                            
                        </tr>
                        <tr>
                        <td>
                        <table>
                        <tr>
                        <td colspan="4" style="width:900px; text-align:left;">
                             <div style="padding:0px; float:left">
                                 <uc1:MessageBox ID="MessageBox1" runat="server" />
                             </div>
                             <asp:Label runat="server" ID="lblEMsg" Font-Size="15px"  ForeColor="Red" style="float :left;padding-left :3px;"></asp:Label>
                             
                        </td>
                        
                        <td style=" padding-top:3px; padding-right:2px; padding-bottom:3px; width:225px; text-align:right">
                            <div style="float:right; padding-right:5px;">
                                 <asp:Label ID="lblRecordCount" style=" font-size:15px; font-weight:bold" runat="server"></asp:Label>
                            </div>
                          
                                </td>
                                </tr></table>
                                </td>
                                </tr>
                        </table> 
                                </DIV>
                            
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
     <mtm:footer ID="footer1" runat ="server" />
      </form>
   
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobPlanning.aspx.cs" Inherits="JobPlanning" MasterPageFile="~/MasterPage.master" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="../HRD/JS/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[type=radio]').click(function () {
                var cname = $(this).attr('class');

                $('.' + cname).each(function () {
                    $(this).prop('checked', false);
                });

                $(this).prop('checked', true);
            });
        });
    </script>
    <%--<script type="text/javascript">
        $('.tdrbn input:radio').click(function () {
            alert('hi');
            $('.tdrbn input:radio').each(function () {
                $(this).prop('checked', false);
            });
            $(this).prop('checked', true)
        });
    </script>--%>
    <script type="text/javascript" language="javascript">
        //$(function () {
        //    $("[id*=ctl00_ContentMainMaster_rptSearch_ctl00_rdbRadio]").click(function () {
        //        var index = $(this).index() - 1;
        //        var flag = this.checked;
        //        $("[id*=ctl00_ContentMainMaster_rptSearch_ctl00_rdbRadio]").not(':eq(' + index + ')').each(function (i, elem) {
        //            if (i != index)
        //                elem.checked = !flag;
        //        });
        //    });
        //});
        function CheckPlan() {
            return true;
        }
        function openplanwindow(VC, CID, JID) {
            window.open('PlanMaster.aspx?VC=' + VC + '&&CID=' + CID + '&&JID=' + JID, '', 'resizable=1, status=1,scrollbars=0,toolbar=0,menubar=0,width=1150,height=450');
        }
        function selectAll(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                if (i < 10) {
                    chkbox = document.getElementById('rptSearch' + '_ctl0' + i + '_chkSelect');
                }
                else {
                    chkbox = document.getElementById('rptSearch' + '_ctl' + i + '_chkSelect');
                }
                if (chkbox) {
                    chkbox.checked = invoker.checked;
                }
            }
        }
        function CheckOnOff(rdoId, gridName) {
            var rdo = document.getElementById(rdoId);
            /* Getting an array of all the "INPUT" controls on the form.*/
            var all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                /*Checking if it is a radio button, and also checking if the
                id of that radio button is different than "rdoId" */
                if (all[i].type == "checkbox" && all[i].id != rdo.id) {
                    var count = all[i].id.indexOf(gridName);
                    if (count != -1) {
                        all[i].checked = false;
                    }
                }
            }
            rdo.checked = true;/* Finally making the clicked radio button CHECKED */
        }
        function showdescr(ctl) {
            //var JID =ctl.childNodes[2].value;
            //window.open('PopUpJobPlanningDescr.aspx?JID=' + JID,'','resizable=1, status=1,scrollbars=0,toolbar=0,menubar=0,width=580,height=345');
            window.open('PopUpJobPlanningDescr.aspx?JID=' + ctl.getAttribute("compjobid") + '&VSL=' + ctl.getAttribute("vesselcode"), '', 'resizable=1, status=1,scrollbars=0,toolbar=0,menubar=0,width=580,height=500');
        }
        function refresh() {
            document.getElementById('ctl00_ContentMainMaster_btnSearch').click();
        }
       
        function msieversion() {
            var ua = window.navigator.userAgent
            var msie = ua.indexOf("MSIE ")

            if (msie > 0)      // If Internet Explorer, return version number
                return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)))
            else                 // If another browser, return 0
                return 0
        }
        var Version = msieversion();
        var CSSName = "class";
        if (Version == 7) { CSSName = "className" };
        var lastSel = null;

        function Selectrow(trSel, prid) {
            if (lastSel == null) {
                trSel.setAttribute(CSSName, "SelectedPage");
                lastSel = trSel;
                document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
            }
            else {
                if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
                {
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                    trSel.setAttribute(CSSName, "SelectedPage");
                    lastSel = trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "SelectedPage");
                    lastSel = trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                }
            }
            trSel.childNodes[0].click();
        }

        function planthis(args) {
           // Alert('Alert 1');
           /* var args = args;*/
            
            document.getElementById('ctl00_ContentMainMaster_txtarg').value = args;
            document.getElementById('ctl00_ContentMainMaster_btnoplan').style.display = '';
        }
        function OpenWinPlan(args) {
            alert(args);
            document.getElementById('ctl00_ContentMainMaster_txtarg').value = args;
            window.open('JobPlanningComments.aspx?key=' + args, '');
        }
    </script>
    <style type="text/css">
    .Page
    {
    	font-weight:normal;
    	font-size:8px;
    }
    .SelectedPage
    {
        border:dotted 1px red;
        font-size:8px;
    }
  .highlight { border: dotted 1px Red}
</style>

    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <%--<form id="form1" runat="server" defaultbutton="btnSearch">--%>
    <div style="text-align: center" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:TextBox runat="server" ID="txtarg" style="display:none;"></asp:TextBox>
        <div class="text headerband">
             Job Planning
        </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">        
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style=" padding-top:2px;">
                            <td colspan="2" style="padding-right: 5px; padding-left: 2px;">
                            
                            <div style="width:100%; height:532px; border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
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
                            <td style="text-align:left; ">&nbsp;By Job Status :</td>
                            <td style="text-align:left; ">&nbsp;</td>
                            <td style="text-align:left; ">&nbsp;</td>
                        </tr>
                        <tr style=" background-color :#F2F2F2" >
                        <td id="tdFleet" style="text-align:left" runat="server" >
                        <table  cellpadding="0"  cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px"><asp:DropDownList ID="ddlFleet" runat="server" Width="125px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList></td>
                            </tr>
                           
                            <tr>
                               <td style="text-align:left; padding-left:2px"><asp:DropDownList ID="ddlVessels" 
                                       runat="server" Width="125px" ></asp:DropDownList></td>
                           </tr>
                             <tr >
                                <td style="text-align:left; font-weight:bold; padding-bottom:1px; padding-top:1px;">
                                 
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td style="text-align:left"  >
                        <table  cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px">Code : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompCode" MaxLength="15" runat="server" ></asp:TextBox></td>
                           </tr>
                           <tr>
                               <td style="text-align:left; padding-left:2px" >Name : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompName" MaxLength="50" runat="server" ></asp:TextBox></td>
                           </tr>
                            <tr>
                                <td colspan="2">
                                     <asp:CheckBox Text="Class Equip. Code" ID="chkClass" runat="server"/>
                                     <asp:TextBox runat="server" ID="txtClassCode" Width="75px" MaxLength="20" ></asp:TextBox> 
                                </td>           
                            </tr>
                        </table>
                           
                        
                        </td>                       
                        <td style="text-align:left" >
                          <table cellpadding="0" cellspacing="0" width="100%" height="110px">
                           <tr>
                              <td style="text-align:left">
                              <asp:ListBox ID="lbJobTypes" runat="server" Width="125px" SelectionMode="Multiple" height="100px">                               
                              </asp:ListBox>
                              </td>                        
                           </tr>
                          </table>
                        </td>                                           
                        <td style="text-align:left" >
                            
                           <table cellpadding="0" cellspacing="2" width="100%">
                           <tr>
                              <td style="text-align:right">Int. Type : </td>
                              <td width="70px" colspan="2">
                                 <asp:DropDownList ID="ddlIntType" runat="server" Width="100px" >
                                    <asp:ListItem Text="< SELECT >" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Calender" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Hour" Value="2"></asp:ListItem>
                                 </asp:DropDownList>
                               </td>               
                           </tr>
                           <tr>
                              <td style="text-align:right">Due In :</td>
                              <td width="70px"><asp:TextBox ID="txtDueDays" runat="server" Text="180" MaxLength="10" Width="65px" ></asp:TextBox></td>               
                              <td>Days</td>               
                           </tr>
                           <tr >
                               <td style="text-align:right">Overdue :</td><td><asp:CheckBox ID="chkOverdue" Checked="true" runat="server"></asp:CheckBox>

                                  
                                                                          </td>
                                                                                    <td>&nbsp;</td>
                           </tr>
                          </table>
                        </td>
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="0" width="100%" height="110px">
                           <tr>
                              <td style="text-align:left">
                              <asp:ListBox ID="lbAssignedTo" runat="server" Width="99px" SelectionMode="Multiple" height="100px">
                              </asp:ListBox>
                              </td>                        
                           </tr>
                          </table>
                        </td>
                        <td style="text-align:left" >
                            <table cellpadding="0" cellspacing="0" width="100%" height="110px">
                                 <tr>
                                     <td>
                                          <asp:ListBox ID="lbWorkOrderStatus" runat="server" SelectionMode="Multiple" Width="117px" height="100px">
                                              <asp:ListItem Text="Issued" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="Postponed" Value="3"></asp:ListItem>
                                              <asp:ListItem Text="To be Planned" Value="0"></asp:ListItem>
                                          </asp:ListBox>
                                     </td>
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
                             <td style="text-align:left; ">
                              <asp:CheckBox ID="chkDefect" Text="Defect Jobs" runat="server" />
                                 </td>
                             </tr>
                             <tr>
                              <td style="text-align:left">
                              <asp:CheckBox ID="chkUnPlanned" Text="Random Jobs" runat="server" />
                              </td>                        
                           </tr>
                               <tr>
                                   <td style="text-align:left">
                                           <asp:CheckBox runat="server" ID="chkvjobs" Text="Office Comments"></asp:CheckBox>
                                   </td>
                               </tr>
                           </table>
                        </td>
                        <td style="text-align:center">
                        <table >
                        <tr><td>
                            <asp:Button ID="btnSearch" Text="Search" CssClass="btn" runat="server" onclick="btnSearch_Click" />
                            <asp:Button ID="btnClear" Text="Clear" CssClass="btn" runat="server" onclick="btnClear_Click" /> 
                            <button value="" class="btn" id="btnoplan" style="display:none;width:90px" onclick="OpenWinPlan();" >Plan Job</button>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnNewPlan" Text="Plan Jobs" CssClass="btn" style="width:120px" runat="server" onclick="btnNewPlan_Click" OnClientClick="return CheckPlan();"/>                                
                            </td>
                        </tr>
                             <tr>
                            <td>
                                <asp:Button ID="btnStart" Text="Comments for Ship" CssClass="btn" style="width:150px" runat="server" onclick="btnStartDiscussion_Click"/>                                
                            </td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                          </table>
                     <asp:UpdatePanel ID="UP123" runat="server">
                     <ContentTemplate>
                     
                        <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                    
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                    <%if (Session["UserType"].ToString().Trim() == "S")
                                      {%>
                                        <col style="width: 2%;" />
                                        <% }%>
                                        <%if (Session["UserType"].ToString().Trim() == "O")
                                         {%>
                                        <col style="width: 3%;" />
                                          <col style="width: 3%;" />
                                        <% }%>
                                       
                                       
                                        <col style="width: 6%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 11%;"/>
                                        <col style="width: 9%;" />
                                        <col style="width: 8%;" />
                                        <col style="width: 8%;" />
                                        <col style="width: 4%;" />
                                       <%-- <col style="width: 40px;" />--%>
                                        <col style="width: 8%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 6%;" />
                                        <%if (Session["UserType"].ToString().Trim() == "S")
                                            {%>
                                        <col style="width: 5%;" />
                                            <% }%>
                                        <col style="width: 13%;" />
                                        <col style="width: 2%;" />
                                        <tr align="left" class="headerstylegrid">
                                        <%if (Session["UserType"].ToString().Trim() == "S")
                                           {%>
                                            <td style="width: 2%;">
                                                <input type="radio" id="chkAll" onclick='selectAll(this)' style="display:none;" /> 
                                            </td>
                                            <% }%>
                                            <%if (Session["UserType"].ToString().Trim() == "O")
                                                {%>
                                            <td style="width: 3%;">
                                                VSL
                                            </td>
                                              <td style="width: 3%;">
                                                Plan
                                            </td>
                                            <% }%>
                                            <td style="width: 6%;">View</td>
                                            <td style="width: 9%;">
                                                Comp.Code
                                            </td>
                                            <td style="width: 5%;">
                                                Job Type
                                            </td>
                                            <td style="width: 11%;">
                                                Comp. Name
                                            </td>
                                            <td style="width: 9%;">
                                                Int.
                                            </td>                                            
                                            <td style="width: 8%;">
                                                Last Done Dt.
                                            </td>
                                            <td style="width: 8%;">
                                                Last Done Hrs
                                            </td>
                                            <td style="width: 4%;">
                                                Due In
                                                Days
                                            </td>
                                            <%--<td>
                                               Diff(H)
                                            </td>--%>
                                            <td style="width: 8%;">
                                                Next Due Dt.
                                            </td>
                                            <td style="width: 5%;">
                                                Next Hrs
                                            </td>
                                            <td style="width: 6%;">
                                                Status
                                            </td>
                                             <%if (Session["UserType"].ToString().Trim() == "S")
                                                {%>
                                                <td style="width: 5%;">
                                                    Action                                           
                                                </td>
                                                <% }%>
                                            
                                            <td style="padding:0px;width: 13%;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                <td colspan="2" style="padding-bottom:3px;" >Job Assigned For</td>
                                                </tr>
                                                <tr style="border-top:solid 1px ;">
                                                <td style="width: 6%;">Rank</td>
                                                <td style="width: 7%;">Date</td>
                                                </tr>
                                                </table>
                                                <%--Job Planning <br/>
                                                <div style="width:100%;">____________________</div>
                                                <div style="padding-top:3px; padding-left:10px; float:left">&nbsp;Rank</div>
                                                <div style="padding-top:3px; padding-right:15px; float:right">Date</div>--%>
                                            </td>
                                            <td style="width: 2%;">
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                                <div id="dvJP"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
               <%if (Session["UserType"].ToString().Trim() == "S")
                                      {%>
                                        <col style="width: 2%;" />
                                        <% }%>
                                        <%if (Session["UserType"].ToString().Trim() == "O")
                                         {%>
                                        <col style="width: 3%;" />
                                          <col style="width: 3%;" />
                                        <% }%>
                                       
                                       
                                        <col style="width: 6%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 11%;"/>
                                        <col style="width: 9%;" />
                                        <col style="width: 8%;" />
                                        <col style="width: 8%;" />
                                        <col style="width: 4%;" />
                                       <%-- <col style="width: 40px;" />--%>
                                        <col style="width: 8%;" />
                                        <col style="width: 5%;" />
                                        <col style="width: 6%;" />
                                        <%if (Session["UserType"].ToString().Trim() == "S")
                                            {%>
                                        <col style="width: 5%;" />
                                            <% }%>
                                        <col style="width: 13%;" />
                                        <col style="width: 2%;" />
                    </colgroup>
                    <%--OnItemCreated="rptSearch_OnItemCreated" --%>
               <asp:Repeater ID="rptSearch" runat="server" OnItemDataBound="rptSearch_OnItemDataBound" >
                  <ItemTemplate>
                      <tr class="alternaterow" onMouseOver="this.className='highlight'" onMouseOut="this.className='alternaterow'">
                      <%if (Session["UserType"].ToString().Trim() == "S")
                      {%>
                           <td align="center" style="width: 2%;"><asp:CheckBox ID="chkSelect" runat="server" Visible='<%#(Eval("WorkOrderStatus").ToString()!="Postpone Requested")%>' /></td>
                               <% }%>
                         <%if (Session["UserType"].ToString().Trim() == "O")
                                      {%>       
                           <td align="left" style="width: 3%;"><%#Eval("VesselCode")%> </td>
                          <td style="width: 3%;">
                                <%--<input type="radio" runat="server"  id='cj<%#Eval("CompJobId")%>' name="plan" style='<%#(Eval("JobCode").ToString().ToLower()=="ren" || Eval("JobCode").ToString().ToLower()=="ovh" || Eval("JobCode").ToString().ToLower()=="sur" || Eval("JobCode").ToString().ToLower()=="cha")?"display:block":"display:none"%>'/>--%>

                                  <input id="rdbRadio" type="radio" runat="server"  name="plan" class="myClass" />
                                 <asp:HiddenField ID="hdnJobCode" runat="server" Value='<%#Eval("JobCode") %>' />
                            </td>
                           <% }%>
                           <td align="center" style="width: 6%;">
                            <span style='display:<%#(Eval("ResultType").ToString()=="P")?"":"none"%>'>
                            <!--Vsl_SpareRequired.aspx?VC=" + Eval("VesselCode").ToString() + "&&CID=" + Eval("ComponentId").ToString() + "&&JID=" + Eval("CompJobId").ToString() %>-->
                            <a runat="server" ID="ankhistory"  href='<%#"JobUpdateHistory.aspx?VC=" + Eval("VesselCode").ToString() + "&&CID=" + Eval("ComponentId").ToString() + "&&JID=" + Eval("CompJobId").ToString() %>' target="_blank"   title="Job History" >
                            <img src="Images/gtk-history.png" style="border:none"  /></a>                                                                                                                  
                            <a target="_blank" style='<%#(Common.CastAsInt32(Eval("nos"))<=0)?"display:none":""%>' href="ShipJobSpareRequirement.aspx?ComponentId=<%#Eval("ComponentId")%>&CJID=<%#Eval("CompJobID")%>&VesselCode=<%#Eval("VesselCode" +"")%>"> 
                                <img src="Images/gtk-spares.png" style="border:none;" title="Spare Requirement." /></a>
                            <a target="_blank" style='<%#(Common.ToDateString(Eval("date_latest_comm"))=="")?"display:none":""%>' href="JobPlanningComments.aspx?key=<%#Eval("VesselCode")%>|<%#Eval("CompJobID")%>"> 
                                <img src='<%#(Eval("last_comm").ToString().Trim()==Session["UserType"].ToString().Trim())?"Images/discussion-bubble.png":"Images/discussion-bubble-blue.png"%>' style="border:none;" title="Communication" /> 
                            </a>
                            </span>
                           </td>
                           <td align="left" style="width: 9%;"><%#Eval("ComponentCode")%><asp:HiddenField ID="hfCompId" Value='<%#Eval("ComponentId") %>' runat="server" /></td>
                           <td onclick='<%#(Eval("ResultType").ToString()=="P")?"showdescr(this);":""%>' style="cursor:pointer;width: 5%;" align="center" compjobid='<%#Eval("CompJobId") %>' vesselcode='<%#Eval("VesselCode") %>'>
                           <span style="text-transform:uppercase;color:blue"><u><%#Eval("JobCode")%></u></span>
                           <asp:HiddenField ID="hfjobId" Value='<%#Eval("CompJobId") %>' runat="server" />
                           </td>
                           <td align="left" style="font-size :9px;color:Purple;width: 11%;"><%#Eval("ComponentName")%> <b style='<%#(Eval("ClassEquipCode").ToString().Trim()=="")?"display:none":""%>'> [ <%#Eval("ClassEquipCode")%> ]</b>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           <div style="color:DarkGreen;" ><i><%#Eval("JobName")%></i></div>
                           <asp:HiddenField ID="hfdCompJobId" Value='<%#Eval("CompJobId") %>' runat="server" />
                           <asp:HiddenField ID="hfVesselCode" Value='<%#Eval("VesselCode") %>' runat="server" /></td>
                           <td align="center" style="width: 9%;" >
                           <span style='display:<%#(Eval("ResultType").ToString()=="P")?"":"none"%>'>
                           <%#Eval("Interval")%>-<%#Eval("IntervalName")%></span></td>                           
                           <td align="center" style="width: 8%;">
                           <span style='display:<%#(Eval("ResultType").ToString()=="P")?"":"none"%>'>
                           <%# Common.ToDateString(Eval("LastDone"))%>
                           </span>
                           <asp:HiddenField ID="hfLastDone" Value='<%#Common.ToDateString(Eval("LastDone")) %>' runat="server" /></td>
                           <td align="center" style="width: 8%;"><%# Eval("LastHour").ToString() == "0" ? "" : Eval("LastHour")%><asp:HiddenField ID="hfLastHour" Value='<%#Eval("LastHour") %>' runat="server" /></td>
                           <td style="width: 4%;"><%# Eval("Difference").ToString() == "0" ? "" : Eval("Difference")%></td>
                           <%--<td><%# Eval("DiffHour").ToString() == "0" ? "" : Eval("DiffHour")%></td>--%>
                           <td align="center" style="width: 8%;">
                           <%#Common.ToDateString(Eval("NextDueDate"))%>
                           <asp:HiddenField ID="hfNextDueDate" Value='<%#Common.ToDateString(Eval("NextDueDate")) %>' runat="server" /></td>                           
                           <td align="center" style="width: 5%;"><%#Eval("NextHour").ToString() == "0" ? "" : Eval("NextHour")%><asp:HiddenField ID="hfNextHour" Value='<%#Eval("NextHour") %>' runat="server" /></td>
                           <td align="left" style="width: 6%;" class='<%# (Eval("DueStatus").ToString()=="OVER DUE")?"highlightrow":"row" %>'><asp:Label ID="woStatus" Text='<%#Eval("WorkOrderStatus")%>' runat="server"></asp:Label></td>
                           <%if (Session["UserType"].ToString().Trim() == "S")
                               {%>
                           <td style="width: 5%;">
                               <%--<a runat="server" ID="ancExecute"  href='<%#"Popup_ExecuteJob.aspx?CID=" + Eval("ComponentId").ToString() + "&&JID=" + Eval("CompJobId").ToString() %>' target="_blank" visible='<%#Eval("WorkOrderStatus").ToString()== "Issued" %>'  title="Execute Job" >--%>
                               <a runat="server" ID="ancExecute"  target="_blank" visible='<%#Eval("WorkOrderStatus").ToString()== "Issued" %>'  
                                    href='<%# ((Eval("ResultType").ToString()=="P")? "Popup_ExecuteJob.aspx?CID=" + Eval("ComponentId").ToString() + "&&JID=" + Eval("CompJobId").ToString()+"&&PR="+Eval("PlannedRank").ToString() :((Eval("ResultType").ToString()=="D")?"Popup_BreakDown.aspx?DN=" + Eval("DefectNo").ToString():"Popup_AddUnPlanJob.aspx?VSL=" + Eval("VESSELCODE").ToString() + "&UPId=" + Eval("DefectNo").ToString())) %>' 
                                    title="Execute Job" >
                                <img src="Images/gtk-execute.png" style="border:none"  />                                   
                               </a> 
                               <span style='display:<%#((Eval("ResultType").ToString()=="P")?"":"none")%>'>
                                <a runat="server" ID="ankPostpone"  href='<%#"Popup_Postponejob.aspx?CID=" + Eval("ComponentId").ToString() + "&&JID=" + Eval("CompJobId").ToString()+ "&&CriticalType=" + Eval("CriticalType").ToString() %>' target="_blank"  title="Postpone Job" visible='<%#ShowHidePostPoneBtn(Eval("WorkOrderStatus").ToString(),Eval("CriticalType").ToString()) %>' >
                                <img src="Images/gtk-postpone.png" style="border:none" />
                               <%--<asp:ImageButton src="Images/gtk-postpone.png" style="border:none" id="imgPostPone" runat="server"  />   --%>
                               </a>
                               </span>
                           
                           </td>
                           <% }%>
                           <%if (Session["UserType"].ToString().Trim() == "S")
                              {%>
                           <td style="width: 13%;">
                               <table>
                                      <tr>
                                          <td align="left"><asp:DropDownList  ID="ddlAssignTO" runat="server" Width="60px"></asp:DropDownList>
                                          <asp:HiddenField runat="server" ID="hfdPRank" Value='<%#Eval("PlannedRank")%>'/>
                                          <asp:HiddenField runat="server" ID="hfdAssRank" Value='<%#Eval("Rank")%>'/>
                                          <asp:HiddenField runat="server" ID="hfdCritical" Value='<%#Eval("CriticalType")%>'/>
                                          </td>
                                          <td align="left"><asp:TextBox ID="txtPlanDate"  Text='<%#Common.ToDateString(Eval("PlanDate"))%>' onfocus="showCalendar('',this,this,'','holder1',-205,-150,1)" MaxLength="11" Width="75px" runat="server" ></asp:TextBox></td>
                                      </tr>
                               </table>
                           </td>
                           <% }%>
                           <%if (Session["UserType"].ToString().Trim() == "O")
                             {%>
                           <td style="width: 13%;">
                                  <asp:Label ID="lblAssigneTo" Text='<%#Eval("Rank") %>' style="float:left; padding-left:17px;" runat="server"></asp:Label> 
                                  <asp:Label ID="lblPlanDate" Text='<%#Common.ToDateString(Eval("PlanDate"))%>' style="float:right" runat="server"></asp:Label>
                           </td>
                             <% }%>
                           <td ></td>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
                                </div>
                            </td>                            
                        </tr>
                        <tr>
                            <td align="center" style="text-align:center;" >
                               <center>
                                <table cellpadding="1" cellspacing="2" style="text-align:center; border-collapse:collapse" border="0">                                     
                                    <tr>
                                    <td style="width:150px;float:right;">
                                        <asp:LinkButton ID="lnkPrev20Pages" runat="server" Text='<< Previous 20' OnClick="lnkPrev20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" Visible="false" ></asp:LinkButton>
                                    </td>
                                    <asp:Repeater ID="rptPageNumber" runat="server" >
                                        <ItemTemplate>
                                            <td style="width:10px;"  id='tr<%#Eval("PageNumber")%>' class='<%#(Convert.ToInt32(Eval("PageNumber"))!=PageNo)?"Page":"SelectedPage"%>' onclick='Selectrow(this,<%#Eval("PageNumber")%>);' lastclass='Page'>
                                                <asp:LinkButton ID="lnPageNumber" runat="server" Text='<%#Eval("PageNumber") %>' OnClick="lnPageNumber_OnClick" Font-Size="10px" style="text-decoration:none;" ></asp:LinkButton>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <td style="width:150px;">
                                        <asp:LinkButton ID="lnkNext20Pages" runat="server" Text='Next 20 >>' OnClick="lnkNext20Pages_OnClick" Font-Size="11px" style="text-decoration:none;" ></asp:LinkButton>
                                        <asp:HiddenField ID="hfPRID" runat="server" />
                                    </td>
                                    </tr>
                                </table>
                                </center>
                             </td>
                            </tr>
                        </table>
                         
                        </ContentTemplate>
                     </asp:UpdatePanel>
                     
                        <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                    
                        <tr>
                        <td>
                            <table border="0" width="100%">
                            
                            <tr>
                                <td>
                                <div style="float:left"><b>Legend :&nbsp; </b></div>
                                <div style="float:left"><img src="Images/gtk-spares.png" style="border:none;float:left"/>&nbsp;Spares&nbsp;</div>
                                <div style="float:left"><img src="Images/gtk-history.png" style="border:none;float:left"/>&nbsp;Job History&nbsp;</div>
                                <div style="float:left"><img src="Images/gtk-execute.png" style="border:none;float:left"/>&nbsp;Execute Job&nbsp;</div>
                                <div style="float:left"><img src="Images/gtk-postpone.png" style="border:none;float:left"/>&nbsp;Postpone Job&nbsp;</div>
                                <div style="float:left;display:none;"><img src="Images/discussion-bubble.png" style="border:none;float:left"/>&nbsp;PMS Discussion ( Message Sent )&nbsp;</div>
                                <div style="float:left;display:none;"><img src="Images/discussion-bubble-blue.png" style="border:none;float:left"/>&nbsp;PMS Discussion ( New Message )&nbsp;</div>
                                <div class="highlightrow" style="float:left; text-align :center; height :20px;">&nbsp;<b>Overdue Job</b>&nbsp;</div>
                                <div style="float:left;padding:2px;"><span class="CriticalType_C">                                    <span class="CriticalType_C">[C]</span>&nbsp;Critical</div>
                                <div style="float:left;padding:2px;"><span class="CriticalType_E">[E] &nbsp;Critical for Environment</span></div>
                               
                                </td>
                                <td width="150px" >
                                    <asp:Label ID="lblRecordCount" style=" font-size:12px; " runat="server"></asp:Label>
                                </td>
                                <td align="right" style="text-align:right; width:80px; ">
                                    <asp:UpdatePanel ID="UPPrint" runat="server" >
                                        <ContentTemplate>
                                            <asp:Button ID="btnPring" Text="Print" CssClass="btn" style="width:80px; float:right; margin-left:10px;" runat="server" onclick="btnPring_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </td>
                           </tr>
                           <tr>
                               <td colspan="3">
                                         <div style="padding:0px; float:left">
                                             <uc1:MessageBox ID="MessageBox1" runat="server" />
                                         </div>
                               </td>
                           </tr>
                            </table>
                        </td>
                        </tr>
                        </table> 
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

        
    <%--<mtm:footer ID="footer1" runat ="server" />--%>
   <%-- </form>--%>
</asp:Content>

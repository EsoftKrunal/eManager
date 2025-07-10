<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="CrewApproval_Dashboard" Title="Crew Approval" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
   <%-- <link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <%--<link rel="stylesheet" href="../../../css/app_style.css" />--%>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <%-- <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />--%>
    <script type="text/javascript">
        function OpenPopUp(ctl) {
            window.open("..\\Applicant\\CandidateDetailPopUp.aspx?candidate=" + ctl.getAttribute("appid") + "&M=App");
        }

        function refreshPage() {
            document.getElementById('btnSearch').click();
        }
    </script>
    <style type="text/css">
       
        .mainheading {
                background-color: #bddbfd;
    color: #25486f;
    font-size: 25px;
    font-weight: bold;
        }
        .box{
            font-size:15px;
            color:white;
            width:80%;
            margin:5px auto;
            position:relative;
            line-height:35px;
            text-align:left;
            padding:3px;
            padding-left:20px;            
        }
        .box .counter
        {
            font-size: 25px;
            font-weight: bold;
            position: absolute;
            right: 0px;
            top: 2px;
            text-align: center;
            width: 100px;
        }
        .bordered
        {
          border:solid 1px #74b3f5;
        }
    </style>

    <style type="text/css">
    body
    {
        font-family:Calibri; 
        font-size:14px;
        margin:0px;
        color:#444;
    }
    h1 {
        font-size: 18px;
        background-color: #0D5D8C;
        /*background-color: rgba(13, 93, 140, 1);*/
        padding: 8px;
        margin: 0px;
        color: #fff;
    }
    
    h2 {
        font-size: 17px;
        color: #0D5D8C;
        /*color: rgb(13, 93, 140);*/
        margin: 0px;
        border-bottom: solid 1px #e2e2e2;
        text-align: left;
        padding: 10px;
        background-color: rgb(223, 243, 255);
        margin-top: 20px;
    }
    
    
    .data
    {
        font-size:14px;
    }
    .dataheader
    {
        font-size:14px;
        background-color:#FFE0C2;
    }
    a img
    {
        border:none;
    }
    
    .newbtn
    {
        border:solid 0px #c2c2c2;
        /*background-color:rgba(13, 93, 140, 1);*/
        background-color:#0D5D8C;
        color:white;
        padding:8px 15px;
        
        font-size:15px;
        margin-top:2px;
    }
    .heading>th
    {
        color:white;
        font-weight:bold;
        text-align:right;
        /*background-color: rgb(245, 245, 245);*/
        background-color: orange;
        padding:8px;
        text-align:left;
    }
    .headingvalue
    {
        text-align:left;
        font-weight:bold;
    }
    table
    {
        border-collapse:collapse;
       
    }
    .bordered tr td
    {
        border:solid 1px #e2e2e2;
        padding:8px;
    }
    .trs
    {
        font-weight:bold;
    }
    .success{
        float:left;margin-top:-5px;color:#31c315;width:35px;display:block;text-align:center;margin-right:10px;
    }
    .error{
            float:left;margin-top:-5px;color:RED;width:35px;display:block; text-align:center;margin-right:10px;
    }
    </style>
    </asp:Content>
        <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align: center;font-family:Arial;font-size:12px;">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;width: 100%" >
        <tr>
            <td class="text headerband" >Approvals</td>
        </tr>
        </table>

        <table border="0"  cellpadding="0" cellspacing="0" width="99%" style="border-collapse:collapse">
            <col width="50%"  />
            <tr>
                <td class="mainheading" style="padding:6px;">Top 4 Ranks</td>
                <td class="mainheading" style="padding:6px;border-left:solid 1px #4371a5">All Other Ranks</td>
            </tr>
<tr>
    <td style="text-align:center">
        <div style="background: #cc9900;" class="box">
            <div>Submission Pending</div> 
            <div class="counter">
                <asp:LinkButton ID="lblPendingSubmission" runat="server" OnClick="btnCouterClick" CommandArgument="PendingSubmission"></asp:LinkButton>                 
            </div> 
        </div>        
        <div style="background: lightslategray;" class="box">
            <div>Manning Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalCrew" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalCrew"></asp:LinkButton> </div>             
        </div>
         <div style="background: lightslategray;" class="box">
            <div>TechnicaTechnical Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalTechnical" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalTechnical"></asp:LinkButton> </div>             
        </div>
         <div style="background: lightslategray; top: 0px; left: 0px;" class="box">
            <div>Marine Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="AwaitingApprovalMarine" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalMarine"></asp:LinkButton> </div>             
        </div>
         <div style="background: lightslategray;" class="box">
            <div>FleetManager Approval Pending
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalFleetManager" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalFleetManager"></asp:LinkButton> </div>             
        </div>
         <div style="background: lightslategray;" class="box">
            <div>Owner Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalOR" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalOR"></asp:LinkButton> </div>             
        </div>
         <div style="background: lightslategray;" class="box">
            <div>Management Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalManagement" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalManagement"></asp:LinkButton> </div>             
        </div>
        <div style="background: Yellowgreen;" class="box">
            <div>Approved</div> 
            <div class="counter"><asp:LinkButton ID="lblApproved" runat="server" OnClick="btnCouterClick" CommandArgument="Approved"></asp:LinkButton> </div> 
        </div>
               
        <div style="background: #ff3300;" class="box">
            <div>Rejected</div> 
            <div class="counter"><asp:LinkButton ID="lblRejected" runat="server" OnClick="btnCouterClick" CommandArgument="Rejected"></asp:LinkButton> </div> 
        </div>
               
    </td>
   <td style="text-align:center;border-left:solid 1px #4371a5;vertical-align:top;" >
        <div style="background: #cc9900;" class="box">
            <div>Submission Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblPendingSubmissionAllOther" runat="server" OnClick="btnCouterClick" CommandArgument="PendingSubmissionAllOther"></asp:LinkButton> </div> 
        </div>
        <div style="background: lightslategray;" class="box">
            <div>Approval Pending</div> 
            <div class="counter"><asp:LinkButton ID="lblAwaitingApprovalAllOther" runat="server" OnClick="btnCouterClick" CommandArgument="AwaitingApprovalAllOther"></asp:LinkButton> </div> 
            
        </div>
        <div style="background: Yellowgreen;" class="box">
            <div>Approved</div> 
            <div class="counter"><asp:LinkButton ID="lblApprovedAllOther" runat="server" OnClick="btnCouterClick" CommandArgument="ApprovedAllOther"></asp:LinkButton> </div> 
        </div>               
        <div style="background: #ff3300;" class="box">
            <div>Rejected</div> 
            <div class="counter"><asp:LinkButton ID="lblRejectedAllOther" runat="server" OnClick="btnCouterClick" CommandArgument="RejectedAllOther"></asp:LinkButton> </div> 
        </div>
       <div class="mainheading" style="padding:6px;">
           Search Crew
       </div>
        <div style="padding:6px; text-align:center">
            <center>
           <asp:RadioButtonList runat="server" ID="rad_type" RepeatDirection="Horizontal">
               <asp:ListItem Text="Pending" Value="P" Selected="True" ></asp:ListItem>
               <asp:ListItem Text="Approved" Value="A"></asp:ListItem>
           </asp:RadioButtonList>
                </center>
       </div>
       <div style="padding:10px">
           <table style="margin:0 auto">
               <tr>
                   <td style="text-align:center;">
                        <asp:TextBox runat="server" ID="txtcrewsearch" Width="80px"  MaxLength="6" CssClass="form-control" ></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button runat="server" ID="btnSearch1" Width="120px" OnClick="btnSearch1_Click" Text="Search" CssClass="btn"></asp:Button>
                   </td>
               </tr>
           </table>
          
          
           <br />
           <br />
           <asp:LinkButton runat="server" ID="lnkfind" Visible="false"></asp:LinkButton>
       </div>
    </td>
</tr>
              </table>
           
                  
               
      </div>

        
        <div style="position:absolute;top:0px;left:0px; height :455px; width:100%; " id="divCounterList" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:95%;padding :5px; text-align :center;background : white; z-index:150;top:20px; border:solid 0px black;">
            <center >
                <div style=" font-size:14px; padding:8px " class="text headerband">
                    <strong> <asp:Literal ID="litHeading" runat="server"></asp:Literal> </strong>
                </div>
             <div style="padding:5px; background-color:#c2c2c2; font-size:14px;text-align:left;">                 
                     
                     <asp:TextBox runat="server" ID="txtcrewno"></asp:TextBox>

                     <asp:DropDownList ID="ddlPlannedRank" runat="server"></asp:DropDownList>
                         <asp:DropDownList ID="ddlPlannedVessel" runat="server"></asp:DropDownList>
                     <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn"></asp:Button>
                 
                 

             </div>

                
                          <asp:Label ID="lbl_GridView_Search" runat="server"></asp:Label>
                <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height:32px; background-color:white; ">
                    <table border="0" cellpadding="7" cellspacing="0" width="100%" style="border-collapse:collapse" class="bordered">
                        <tr class= "headerstylegrid">
                            <td  style="width:30px">View</td>
                            <td style="width:50px">Crew#</td>
                            <td>Crew Name</td>
                            <td style="width:80px">Current Rank</td>
                            <td style="width:80px">Planned Rank</td>
                            <td style="width:90px">Planned Vessel</td>
                            <td style="width:90px">Last Vessel</td>
                            <td style="width:50px">Crew</td>
                            <td style="width:50px">Technical</td>
                            <td style="width:50px">Marine</td>
                            <td style="width:100px">Fleet Manager</td>
                            <td style="width:100px">Mgmt Approval</td>
                        </tr>
                        </table>
                    </div>
                <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height:350px; background-color:white; ">
                        <table border="0" cellpadding="7" cellspacing="0" width="100%"  style="border-collapse:collapse"  class="bordered"> 
                           
                    <asp:Repeater runat="server" id="gv_CrewApproval">
                        <ItemTemplate>
                            <tr>
                                <td style="width:30px">
                                    <a target="_blank" href="../CrewOperation/CrewPlanningApproval.aspx?_P=<%#Eval("PlanningId") %>" ><img src="../Images/magnifier.png" title="View" /></a> 
                                </td>
                                <td style="width:50px">
                                     <%# Eval("EmpNo")%>                                    
                                    <asp:HiddenField ID="hfd_PlanningId" runat="server" Value='<% #Eval("PlanningId") %>' />
                                    <asp:HiddenField ID="hfd_AppStatus" runat="server" Value='<% #Eval("AppStatus") %>' />
                                </td>            
                                <td><%#Eval("CrewName") %>
                                    <asp:HiddenField ID="hfd_CrewId" runat="server" Value='<% #Eval("CrewId") %>' /></td>            
                                <td style="width:80px"><%#Eval("RankCode") %></td>            
                                <td style="width:80px"><%#Eval("PlannedRankCode") %></td>  
                                <td style="width:90px"><%#Eval("VesselCode") %></td>      
                              <%--  <td>
                                    <img src="../Images/cv.png" onclick="javascript:printrelivercv('<%# Eval("CrewId") %>');" style="cursor:pointer;" title="Open Crew CV" />
                                    <img src="../Images/report.gif" onclick="javascript:printvesselmatrix('<%# Eval("CrewId") %>','<%# Eval("RelieveId")%>','<%# Eval("VesselId") %>');" style="cursor:pointer;" title="Open Vessel Matrix Report" />
                                    <asp:ImageButton ID="btnCL" CommandName="img_dc" runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Open Document CheckList" OnClick='btnCL_Click' style="cursor:pointer;" CommandArgument='<%#Eval("PlanningId")%>'/>
                                </td>--%>
                                <td style="width:90px"><%#Eval("LastVessel") %></td>            
                                <td style="width:50px"><%#getCssStatus(1,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:50px"><%#getCssStatus(2,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:50px"><%#getCssStatus(3,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:100px"><%#getCssStatus(4,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:100px"><%#getCssStatus(99,Common.CastAsInt32(Eval("PlanningId"))) %></td>                               
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                        <%--<asp:GridView ID="gv_CrewApproval" runat="server" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_CrewApproval_PreRender" AllowSorting="false"  CssClass="bordered" BorderWidth="0" >
                         <Columns> 
                            

                            <asp:TemplateField HeaderText="Planning & Approval Details"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                
                                <ItemTemplate>                                    
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                           
                          </Columns>
                             <HeaderStyle CssClass="heading" HorizontalAlign="Left"  />                            
                            
                     </asp:GridView>--%>
                    </div>
                <div style="width:100%; text-align:center;">                                  
                    <asp:Button ID="btnCloseCounterList" runat="server" Text="Close" Width="80px" OnClick="btnCloseCounterList_Click" CausesValidation="false" CssClass="btn" />                    
                </div>
                <div style="text-align:center;padding:10px;">                                  
                    <table border="0" style="margin:0px auto;"  >
                        <col width="150px" />
                        <col width="150px" />
                        <col width="150px" />
                        <tr>
                            <td> <img src="../Images/red_circle.png" /> Pending </td>
                            <td> <img src="../Images/green_circle.gif" /> Approved </td>
                            <td> <img src="../Images/exclamation-mark-yellow.png"  />Rejected </td>
                        </tr>
                    </table>
                </div>
             </center>
        </div>
    </center>
    </div>
</asp:Content>


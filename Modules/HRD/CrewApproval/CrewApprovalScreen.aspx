<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CrewApprovalScreen.aspx.cs" Inherits="CrewApproval_CrewApprovalScreen" Title="Crew Approval" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
      <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <%--<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />--%>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style  type="text/css">
            .modalSignUp1 {
	                background-color:#EEEEEE;
	                border-width:3px;
	                border-style:solid;
	                border-color:Gray;
	                font-family:Verdana;
	                font-size:medium;
	                padding:5px;
	                width:720px;
                }

        .headerstylefixedheader1{
            background-color:orange;color:white;font-weight:bold;
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
    <script type="text/javascript">   
        function Remarks(sender, e) {
            $find('mdl_popup').hide();
            WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("<%=btn_Submit.ClientID %>", "", true, "&", "", false, true));
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
<div id="a" style="background-color: ivory; border: solid 1px black; width: 300px; height: 80px; text-align: left; padding-left: 3px; display: none"></div>
    <div style="text-align: center">
        <script language="javascript" type="text/javascript">
            function printrelivercv(eno)
            {
                if ((eno!=null) && (eno!=""))
                {
                    window.open('..\\Reporting\\PrintCV.aspx?crewid='+ eno,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
                }
            }
            function printrelieveecv(eno1)
            {
                if ((eno1!=null) && (eno1!=""))
                {
                    window.open('..\\Reporting\\PrintCV.aspx?crewid='+ eno1,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
                }
            }
            function printvesselmatrix(id1,id2,vslid)
            {
                if(((id1!=null) && (id1!="")) || ((id2!=null) && (id2!="")) || ((vslid!=null) && (vslid!="")))
                {
                    window.open('..\\Reporting\\VesselMatrix_CrewApproval.aspx?reliverid='+ id1 + '&relieveid='+ id2 + '&vesselid=' + vslid,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
                }
            }
        </script>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
       <%-- <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td  class="text headerband" >

            Ship Deployment Approval</td>
            </tr>
        </table>--%>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr>
                <td style=" padding: 0px;"  >
                   <asp:LinkButton ID="Main" runat="server"></asp:LinkButton>
                   <ajaxToolkit:ModalPopupExtender ID="mdl_popup" runat="server" TargetControlID="Main" PopupControlID="pnl_remarks" BackgroundCssClass="modalBackground" OnOkScript="Remarks()" OkControlID="btn_Submit" CancelControlID="btn_Cancel" ></ajaxToolkit:ModalPopupExtender>
                   <asp:Panel ID="pnl_remarks" runat="server" CssClass="modalSignUp1" style="display: none">
                        <table style="width:700px">
                            <col width="12%" />
                            <col />
                            <tr>
                                <td colspan="2" style="height: 13px">
                                    <span style="font-size: 11pt; text-decoration: underline"><strong> 
                                        <asp:Label ID="lblRemarksHeading" runat="server"></asp:Label>
                                        <%--Remarks--%>
                                    </strong></span></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbl_Modal" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    Remarks:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" Height="82px" TextMode="MultiLine" Width="98%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: left; padding-left: 5px;">
                                    <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn" OnClick="btn_Submit_Click" Width="60px" />&nbsp;<asp:Button
                                        ID="btn_Cancel" runat="server" Text="Cancel" CssClass="btn" Width="60px" /></td>
                            </tr>
                        </table>
                    
                    </asp:Panel>
                   <table style="width: 100%;background-color :#D5E5F7;color:#333;" cellpadding="2" cellspacing="0">
                      
                           <tr>
                               <td style="text-align: right">
                            Crew#: 
                        </td>
                               <td align="left">
                                <asp:TextBox ID="txtCrewNo" runat="server" CssClass="input_box" Width="173px"></asp:TextBox>
                            </td>

                              
                               <td style="text-align: right;">
                                   Rank Group:</td>
                               <td style="text-align: left;">
                                   <asp:DropDownList ID="ddl_RankGroup" runat="server" CssClass="input_box" Width="174px" AutoPostBack="True" OnSelectedIndexChanged="ddl_RankGroup_SelectedIndexChanged">
                                       <asp:ListItem Value="A">All</asp:ListItem>
                                       <asp:ListItem Value="O">Officer</asp:ListItem>
                                       <asp:ListItem Value="R">Rating</asp:ListItem>
                                   </asp:DropDownList></td>
                               <td style="text-align: right;">
                                   Rank:</td>
                               <td style="text-align: left;">
                                   <asp:DropDownList ID="ddl_Rank" runat="server" CssClass="input_box" Width="174px">
                                       <asp:ListItem Value="0">All</asp:ListItem>
                                   </asp:DropDownList></td>
                               <td style="text-align: right;">
                                   Crew Status:</td>
                               <td style="text-align: left;">
                                   <asp:DropDownList ID="ddl_CrewStatus" runat="server" CssClass="input_box" Width="114px">
                                       <asp:ListItem Value="0">All</asp:ListItem>
                                       <asp:ListItem Value="3">On Board</asp:ListItem>
                                       <asp:ListItem Value="2">On Leave</asp:ListItem>
                                       <asp:ListItem Value="1">New</asp:ListItem>
                                   </asp:DropDownList></td> 
                           </tr>
                           <tr>
                               
                               <td style="text-align: right">
                                   Owner:</td>
                               <td style="text-align: left">
                                   <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" Width="174px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged">
                                   </asp:DropDownList></td>
                               <td style="text-align: right">
                                   Vessel:</td>
                               <td style="text-align: left">
                                   <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="174px">
                                       <asp:ListItem Value="0">All</asp:ListItem>
                                   </asp:DropDownList></td>
                               <td style="text-align: right">
                                   Approval Status:</td>
                               <td style="text-align: left;">
                                   <asp:DropDownList ID="ddl_ApprovalStatus" runat="server" CssClass="input_box" Width="174px">
                                       <asp:ListItem Value="L">All</asp:ListItem>
                                       <asp:ListItem Value="A">Approved</asp:ListItem>                                       
                                       <asp:ListItem Value="R">Rejected</asp:ListItem>
                                       <asp:ListItem Value="S">Submitted for Approval</asp:ListItem>
                                       <asp:ListItem Value="N">Pending Submission</asp:ListItem>
                                   </asp:DropDownList></td>
                                
                       <td colspan="2">
                           <asp:Button ID="btn_Search" runat="server" CssClass="btn" Text="Search" OnClick="btn_Search_Click" Width="60px" />
                                    <asp:Button ID="btn_Clear" runat="server" Text="Clear" CssClass="btn" OnClick="btn_Clear_Click" Width="60px" />
                                    <%--<asp:Button ID="btn_Approve" runat="server" Text="Approve" CssClass="btn" OnClick="btn_Approve_Click" Width="60px" />
                                    <asp:Button ID="btn_Reject" runat="server" Text="Reject" CssClass="btn" OnClick="btn_Reject_Click" Width="60px/>--%>
                                    <asp:Button ID="btn_VesselMatrix" runat="server" Text="Vessel Matrix" CssClass="btn" Width="96px" OnClick="btn_VesselMatrix_Click" Visible="False" />
                                    <asp:Button ID="btn_ExportToExcel" runat="server" Text="Export To Excel" CssClass="btn" Width="115px" OnClick="btn_ExportToExcel_Click" Visible="False" />
                       </td>
                           </tr>
                      
                          <%-- <tr>
                               <td style="text-align: left; padding-left :50px;" colspan="2">
                               <strong >
                               Total Records :<asp:Label ID="lbl_Total" runat="server"></asp:Label>
                               </strong>
                               </td>
                               <td style="text-align: right">
                               </td>
                               <td style="text-align: right" colspan="5">
                                    
                                    &nbsp;</td>
                           </tr>--%>
                       </table>
                  </td>
             </tr>
             <tr>
             <td style=" padding:0px">
                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="text-align: left; width: 436px;">
                                </td>
                            <td style="text-align: left;">
                                <asp:Label ID="lbl_Msg" runat="server" ForeColor="#C00000"></asp:Label></td>
                        </tr>
                    </table>
                   <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                     <td>
                      <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height:430px; background-color:white; ">
                          <asp:Label ID="lbl_GridView_Search" runat="server"></asp:Label>
                        <asp:GridView ID="gv_CrewApproval" runat="server" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_CrewApproval_PreRender" AllowSorting="false" OnSorted="On_Sorted" OnSorting="On_Sorting" OnRowDataBound="gv_CrewApproval_RowDataBound" OnRowCommand="gv_CrewApproval_RowCommand" OnSelectedIndexChanged="gv_CrewApproval_SelectedIndexChanged" CssClass="bordered" BorderWidth="0" >
                         <Columns> 
                             <asp:TemplateField ItemStyle-Width="25px" HeaderText="View" >
                                <ItemTemplate>
                                    <a target="_blank" href="../CrewOperation/CrewPlanningApproval.aspx?_P=<%#Eval("PlanningId") %>" ><img src="../Images/magnifier.png" title="View" /></a> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crew#" SortExpression="EmpNo"  HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Eval("EmpNo")%>
                                    <%--<asp:LinkButton ID="lnk_Select" runat="server" CommandName="CrewDetails" Text='<%# Eval("EmpNo")%>'></asp:LinkButton>--%>
                                    <%--<a href="../CrewOperation/CrewPlanningApproval.aspx?_P=<%#Eval("PlanningId") %>" target="_blank"><%# Eval("EmpNo")%></a>--%>
                                    <asp:HiddenField ID="hfd_PlanningId" runat="server" Value='<% #Eval("PlanningId") %>' />
                                    <asp:HiddenField ID="hfd_AppStatus" runat="server" Value='<% #Eval("AppStatus") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crew Name" SortExpression="CrewName" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate >
                                    <%--<asp:LinkButton ID="lnk_CrewId" runat="server" Text='<% #Eval("CrewName") %>' CommandName="CrewDetails"></asp:LinkButton>--%>
                                    <%#Eval("CrewName") %>
                                    <asp:HiddenField ID="hfd_CrewId" runat="server" Value='<% #Eval("CrewId") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Rank" DataField="RankCode" SortExpression="RankCode" ItemStyle-HorizontalAlign="Left" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText=" Planned Vessel" DataField="VesselCode" SortExpression="VesselCode"  HeaderStyle-HorizontalAlign="Center" >
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="center" Visible="false">
                                <ItemTemplate>
                                    <img src="../Images/cv.png" onclick="javascript:printrelivercv('<%# Eval("CrewId") %>');" style="cursor:pointer;" title="Open Crew CV" />
                                    <img src="../Images/report.gif" onclick="javascript:printvesselmatrix('<%# Eval("CrewId") %>','<%# Eval("RelieveId")%>','<%# Eval("VesselId") %>');" style="cursor:pointer;" title="Open Vessel Matrix Report" />
                                    <asp:ImageButton ID="btnCL" CommandName="img_dc" runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Open Document CheckList" OnClick='btnCL_Click' style="cursor:pointer;" CommandArgument='<%#Eval("PlanningId")%>'/>
                                </ItemTemplate>
                                <ItemStyle Width="60px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="V.Matrix" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    
                                </ItemTemplate>
                                <ItemStyle Width="55px" />
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="Avl. Dt" DataField="AvailableFrom" SortExpression="AvailableFrom" HeaderStyle-HorizontalAlign="Center" >
                                <ItemStyle Width="75px"  />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Last Vessel" DataField="LastVessel" SortExpression="LastVessel" ItemStyle-Width="70px" ></asp:BoundField>
                             <asp:BoundField HeaderText="Approval Stage" DataField="ApprovalStatus"  ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
                             <asp:BoundField HeaderText="Approval Type" DataField="LastVessel"  ItemStyle-Width="80px" Visible="false" ></asp:BoundField>
                             <asp:BoundField HeaderText="Submitted By" DataField="FwdForApprovalBy"  ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
                             <asp:BoundField HeaderText="Submitted On" DataField="FwdForApprovalOn"  ItemStyle-Width="70px" ></asp:BoundField>
                           <%-- <asp:TemplateField >
                            <ItemTemplate>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>--%>
                           <%-- <asp:TemplateField HeaderText="Planner Rem." >
                                <ItemStyle />
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" style=" border-collapse:collapse; width:100%"><tr>
                                    <td style=" text-align:center; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("Remark") %>')" onmouseout="if(t1)t1.Hide(event)">
                                    <asp:ImageButton ID="img_plannerremarks" runat="server" CausesValidation="false" CommandName="PlannerRemark" ImageUrl="~/Modules/HRD/Images/magnifier.png" /></td>
                                    </tr></table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planner Rem." Visible="false" >
                                <ItemStyle/>
                                <ItemTemplate>
                                    <asp:Label ID="lbl1" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                           <%-- <asp:BoundField HeaderText="Planned By" DataField="PlannedBy" SortExpression="PlannedBy" />
                            <asp:BoundField HeaderText="Planned On" DataField="PlannedOn" SortExpression="PlannedOn" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Planning & Approval Details"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                
                                <ItemTemplate>
                                    <%--<table cellpadding="0" cellspacing="0" style=" border-collapse:collapse; width:100%"><tr>
                                    <td style=" text-align:center; cursor:hand" onmouseover="t1.Show(event,'<%# Eval("ApproverRemark") %>')" onmouseout="if(t1)t1.Hide(event)">
                                    <asp:ImageButton ID="img_approverremarks" runat="server" CausesValidation="false" CommandName="ApproverRemark" ImageUrl="~/Modules/HRD/Images/magnifier.png" /></td>
                                    </tr></table>--%>
                                    <div style="font-size:11px;  padding-bottom:2px;overflow:auto;">
                                        <em><%# Eval("Remark") %></em>
                                        <br />
                                        <span style="color:blue; float:right;"><%# Eval("PlannedBy") %> / <%# Eval("PlannedOn") %></span>
                                        <span style="clear:both;"></span>
                                    </div>
                                   <%--<div style="font-size:11px; padding-top:2px;">
                                    <em><%# Eval("AppRemark") %></em>
                                        <br />
                                        <span style="color:red; float:right;"><%# Eval("ApprovedRejectedBy") %> / <%#Common.ToDateString(Eval("ApprovedOn")) %></span>
                                        <span style="clear:both;"></span>
                                   </div>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Approval Details" Visible="false" >
                                <ItemStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl2" runat="server" Text='<%# Eval("ApproverRemark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Relieve Name" DataField="RelieveName" SortExpression="RelieveName" />
                            <asp:TemplateField HeaderText="Emp#" SortExpression="RelieveEmpNo">
                                <ItemTemplate>
                                    <a onclick="javascript:printrelieveecv('<%# Eval("RelieveId")%>');" href="#" ><%# Eval("RelieveEmpNo") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Rank" DataField="RelieveRank" SortExpression="RelieveRank" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Curr. VSL" DataField="RelieveVessel" SortExpression="RelieveVessel" >
                                <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Relief Due" DataField="RelieveReliefDue" SortExpression="RelieveReliefDue" >
                                <ItemStyle Width="85px" />
                            </asp:BoundField>                          --%>
                          </Columns>
                             <%--<SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstylefixedheader1" HorizontalAlign="Left"  />                            
                             <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />--%>
                             <HeaderStyle CssClass="headerstylefixedheadergrid"  />                            
                            
                     </asp:GridView>
                    </div>

                    <div style="width: 100%; padding:10px;border-top:solid 1px #c2c2c2;font-size:15px;">
                        <table cellpadding="3" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    Approved :  <b> <asp:Button ID="btnApprovedCnt" runat="server" CommandArgument="A" OnClick="Legent_Click" style="background-color:green;color:white; width:50px;" ></asp:Button></b>
                                </td>
                                <td>
                                    Rejected : <b> <asp:Button ID="btnRejectedCnt" runat="server" CommandArgument="R" OnClick="Legent_Click" style="background-color:red;color:white;width:50px;" ></asp:Button></b>
                                </td>
                                <td>
                                    Submitted for Approval : <b><asp:Button ID="btnSubmittedForApprovalCnt" runat="server" CommandArgument="S" OnClick="Legent_Click"  style="background-color:gray;color:white;width:50px;"></asp:Button></b>
                                </td>
                                <td>
                                    Pending Submission : <b> <asp:Button ID="btnPendingSubmissionCnt" runat="server" CommandArgument="N" OnClick="Legent_Click"  style="background-color:yellow;color:gray;width:50px;" ></asp:Button></b>
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
      </div>
 </form>
</body>
</html>

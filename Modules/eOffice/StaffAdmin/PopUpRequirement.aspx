<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpRequirement.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopUpRequirement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function RefreshParent() {
            window.opener.Refresh();
        }
        function OpenPlanningDetails(TPId) {            
            window.open('../StaffAdmin/Emtm_PopUpTrainingDetails.aspx?TrainingPlanningId=' + TPId, '', '');
        }
        function OpenPlanSchedule() {
            window.open('../StaffAdmin/Emtm_Popup_PlanNewSchedule.aspx', '', '');
        }
        function Refresh() {
            RefreshParent();   
            window.location.reload(true);
        }
    </script>
    <style type="text/css">
    .btn11sel
    {
        font-size:14px;
        background-color:#c2c2c2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    .btn11
    {
        font-size:14px;
        background-color:#e2e2e2;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #c2c2c2;
        padding:5px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
              <table width="100%">
                    <tr>
                        <td style="vertical-align:top; border:solid 1px inherit; width:100%; " >
                              <table cellpadding="0" cellspacing="0" width="100%" border="0">
                           <tr >
                        <td >
                        <div style="border:solid 1px gray"> 
                            <div style="padding:0px; text-align:center;"><asp:Label ID="lblPlanMsg" style="color:Red;" runat="server" Font-Size="12px" ></asp:Label></div>
                            <%--<table cellpadding="3" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td colspan="3" style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" align="center">
                                    <asp:Label ID="lblTrainingName" style="font-size:15px; font-weight:bold;" runat="server"></asp:Label>
                                </td>
                           </tr>

                            <tr>
                                <td colspan="3" 
                                    style=" height:25px; padding-left:20px; font-size:14px; background-color:#e2e2e2;" 
                                    align="center">Attendees List</td>
                            </tr>
                            <tr>
                            <td colspan="3">
                               <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:25px;" />       
                                        <col  />
                                                                         
                                        <col style="width:150px;" />
                                        <col style="width:150px;" />
                                        <col style="width:80px;" />
                                        <col style="width:17px;" />
                                        <tr align="left" class="blueheader">
                                           <td></td>
                                            
                                            <td>Emp Name</td>
                                            
                                            <td>Position</td>
                                            <td>Department</td>
                                            <td>Due Date</td>
                                            <td></td>
                                        </tr>
                                    </colgroup>
                                </table>
                               <div id="dvRecommEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 303px; text-align:center;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                   <colgroup>
                                        <col style="width:25px;" />
                                                                           
                                        <col  />
                                                                       
                                        <col style="width:150px;" />
                                        <col style="width:150px;" />
                                        <col style="width:80px;" />
                                        <col style="width:17px;" />
                                   </colgroup>
                                    <asp:Repeater ID="rptRecommEmp" runat="server">
                                        <ItemTemplate>
                                              <tr class="row">
                                                <td align="center">
                                                    <asp:ImageButton ID="btnREDelete" runat="server" CausesValidation="false" 
                                                        CommandArgument='<%# Eval("TrainingRecommId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                        OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"   
                                                        OnClick="btnREDelete_Click" Visible='<%# Eval("Important").ToString() != "Y"  %>'
                                                        ToolTip="Delete" />
                                                </td>                                                
                                                <td align="left">
                                                <%#Eval("EmpName")%>
                                                
                                                    <asp:HiddenField ID="hfEmpId" Value='<%#Eval("EmpId")%>' runat="server" />
                                                    <asp:HiddenField ID="hfPositionId" Value='<%#Eval("PositionId")%>' runat="server" />
                                                    <asp:HiddenField ID="hfOfficeId" Value='<%#Eval("OfficeId")%>' runat="server" />
                                                    <asp:HiddenField ID="hfDepartmentId" Value='<%#Eval("DepartmentId")%>' runat="server" />
                                                    <asp:HiddenField ID="hfRecommId" Value='<%#Eval("TrainingRecommId")%>' runat="server" />
                                                </td>
                                                
                                                <td align="left"><%#Eval("PositionName")%></td>
                                                <td align="left"><%#Eval("DepartmentName")%></td>
                                                <td align="left"><%#Eval("DueDate")%></td>
                                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                  </table>  
                                  </div>
                            </td>
                            </tr>
                            </table>--%>

                             <div  style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" align="center"> 
                                    <asp:Label ID="lblTrainingName" style="font-size:15px; font-weight:bold;" runat="server"></asp:Label> 
                             </div> 
                             <div style="border:solid 0px gray;" id="divExistingTraining" runat="server" >
                         
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <col width="30%" />
                                <col  />
                                <col width="30%" />
                                <tr style="text-align:center;height:25px; padding-left:20px; font-size:15px; background-color:#e2e2e2;">
                                    <td></td>
                                    <td>
                                        <div >Planned Training</div>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPlanSchedule" Text="Plan New Schedule" Width="150px" CssClass="btn" OnClientClick="OpenPlanSchedule();" runat="server" Style="padding-right:5px; float:right;" /> 
                                    </td>
                                </tr>
                            </table> 
                            
                            
                         

                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:40px;" />                                     
                                    <col  />
                                    <col style="width:80px;" />                                    
                                    <col style="width:80px;" />
                                    <col style="width:100px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader" >
                                        <td>Sr#</td>
                                        <td style="text-align:left">Training Title</td>
                                        <td>From Date</td>
                                        <td>To Date</td>
                                        <td>No of Seats</td>
                                        <td></td>
                                    </tr>
                                </colgroup>
                            </table> 
                        <div id="Div2" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 450px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:40px;" />                                     
                                    <col  />
                                    <col style="width:80px;" />                                    
                                    <col style="width:80px;" />
                                    <col style="width:100px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="rptPlannedTrainings" runat="server">
                                    <ItemTemplate>
                                          <tr class="row" >
                                            <td align="center">
                                                <%#Eval("SrNo")%>
                                            </td>
                                            
                                            <td align="left">
                                                <a href='#' title="View Training Details" onclick='OpenPlanningDetails("<%#Eval("TrainingPlanningId").ToString() %> ")'><%#Eval("TrainingName")%></a>
                                                <%#Common.CastAsInt32(Eval("ValidityPeriod")) > 0 ? "<span style='color:green;font-weight:bold;'>[R]</span>" : ""%>
                                            </td>
                                            <td align="center">
                                                <%# Common.ToDateString(Eval("StartDate"))%>
                                            </td>
                                            <td align="center">
                                                <%# Common.ToDateString(Eval("EndDate"))%>
                                            </td>
                                             
                                            <td align="center">
                                               <%#(Eval("NumOfSeats"))%> 
                                            </td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                          </div>
                                
                        </div>
                        </td>
                        </tr>
                         </table>
                         </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                    <%-----------------------------------------------------------------------------------------------------------------------%>
                    <tr>
                        <td valign="top"  >
                        
                         </td>
                         
                        </tr>  
                </table>
                <asp:Button ID="btnPlanNewTraining" Visible="false" runat="server" Text="Plan New Training" OnClick="btnPlanNewTraining_OnClick" CssClass="btn11sel" />
                <asp:Button ID="btnExistingTraining"    Visible="false" runat="server" Text="Add in Existing Training" OnClick="btnExistingTraining_OnClick" CssClass="btn11" />

                <div  style="text-align:left;   display:none; background-color: #c2c2c2; height:250px; border:solid 1px black;border-top:solid 0px black; padding:10px 5px 5px 5px;">
                    <div style="text-align:left; background-color: white; height:245px;">
                        <asp:Panel runat="server" ID="pnlPlanTraining" Visible="true">
                            <div  id="divPlanNewTraining" runat="server" >
                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                    <tr>
                            <td align="right" ><b>Start Date/Time :&nbsp;</b></td>
                            <td align="left" >
                              <asp:TextBox ID="txtStartDt"  MaxLength="12" Width="80px" runat="server" required='yes'> </asp:TextBox >&nbsp;/&nbsp;
                              <asp:DropDownList ID="ddlStHr" Width="40px" runat="server" required='yes'></asp:DropDownList>&nbsp;:&nbsp;
                                <asp:DropDownList ID="ddlStMin" Width="40px" runat="server" required='yes'></asp:DropDownList> 
                                (Hrs)
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtStartDt" TargetControlID="txtStartDt" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                              </td>
                              <td></td>
                              </tr>
                              <tr>
                            <td align="right" ><b>End Date/Time :&nbsp;</b></td>
                            <td align="left" >
                                <asp:TextBox ID="txtEndDt"  MaxLength="12" Width="80px" runat="server" required='yes'></asp:TextBox>&nbsp;/&nbsp;
                                <asp:DropDownList ID="ddlEtHr" Width="40px" runat="server" required='yes'></asp:DropDownList>&nbsp;:&nbsp;
                                <asp:DropDownList ID="ddlEtMin" Width="40px" runat="server" required='yes'></asp:DropDownList> 
                                (Hrs.) 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtEndDt" TargetControlID="txtEndDt" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td></td>
                            </tr>
                            <tr>  
                            <td align="right" ><b>Location :&nbsp;</b></td>
                            <td align="left" >
                                <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>
                            </td>
                            <td>
                               
                            </td>
                        </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                 <td align="center">
                                            <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" />&nbsp;
                                       <asp:Button ID="btnCancel" Text="Cancel" runat="server" onclick="btnCancel_Click" />&nbsp;
                                       <asp:Button ID="btnAddMore" Text="Add Attendees" runat="server"   Width="100px" 
                                            onclick="btnAddMore_Click" Visible="False" />
                                </td>
                                <td></td>
                            </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlExistinTraining" Visible="false">
                           
                        </asp:Panel>
                    </div>
                </div>
             
             <div id="divAddEmp"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
                 <center>
                       <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:1150px; height:450px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
 
             <div style="background-color:#d2d2d2; padding:7px; text-align:center;"><b style="font-size:14px;">Select Attendees</b></div>
             <div >
                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr >
                          <td>
                          <div style=" background-color:#e2e2e2">
                            <table border="0" cellpadding="2" cellspacing="0" rules="" style="width:100%;border-collapse:collapse;">
                            <tr>
                            <td style="width:100px; text-align:left; display:none;"><b>Emp Code : </b></td>
                            <td style="width:100px; text-align:left; display:none;">
                                <asp:TextBox runat="server" ID="txtSECode"></asp:TextBox>
                            </td>
                            <td style="width:160px; text-align:left"><b>Employee Name: </b></td>
                            <td style="width:150px; text-align:left"><asp:TextBox runat="server" ID="txtSEName"></asp:TextBox>
                            </td>
                            <td style="width:100px; text-align:left"><b>Position : </b></td>
                            <td style="width:100px; text-align:left">
                                <asp:DropDownList runat="server" ID="ddlSPosition"></asp:DropDownList>
                            </td>
                            <td style="text-align:left">
                                <asp:Button runat="server" ID="btnSearch" OnClick ="Search_Click" Text="Show" />
                            </td>
                            </tr>
                            </table>
                            </div>
                            
                          <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:25px;" /> 
                                    <%--<col style="width:80px;" /> --%>                                    
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">
                                        <td>
                                        </td>
                                        <%--<td>
                                            EmpCode
                                        </td>--%>
                                        <td>
                                            Emp Name
                                        </td>
                                        <td>Position
                                        </td>
                                        <td>Department
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            <div id="divAddEmployee" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px; text-align:center;">
                           
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />
                                  <%-- <col style="width:80px;" />--%>                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="rptAddEmp" runat="server">
                                    <ItemTemplate>                                        
                                        <tr class="row">
                                            <td>
                                             <asp:CheckBox ID="chkSelect" runat="server" />
                                            </td>
                                            <%--<td align="left">
                                                <%#Eval("EmpCode")%>
                                                <asp:HiddenField ID="hfAddEmpId" Value='<%#Eval("EmpId")%>' runat="server" />
                                            </td>--%>
                                            <td align="left">
                                                <%#Eval("EmpName")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("PositionName")%>
                                                </td>
                                            <td align="left">
                                                <%#Eval("DepartmentName")%></td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div> 
                              <div style="padding-right:20px; padding-top:5px;" >
                              
                              <asp:Button ID="btnHide" Text="Close" runat="server" style="float:right;" Width="100px" onclick="btnHide_Click"/>
                               <asp:Button ID="btnAddEmp" style="float:right;" Text="Add" runat="server" 
                                      Width="100px" onclick="btnAddEmp_Click"/>
                               <asp:Label ID="lblAddMsg" Text="" style="color:Red; float:right;" runat="server"></asp:Label>&nbsp;
                              </div>
                          </td>
                        </tr>
                            </table>
                        </div>

                        </div>
                        </center>
                        </div>
                        
             
                               
            </ContentTemplate>
            
        </asp:UpdatePanel>         
    </div>
    </form>
</body>
</html>

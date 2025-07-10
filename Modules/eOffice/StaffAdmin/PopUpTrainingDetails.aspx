<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpTrainingDetails.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopUpTrainingDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function RefreshParent() {
            window.opener.Refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
                    <table width="100%">
                    <tr>
                        <td valign="top" style="height:425px;" >
                        <div style="border:solid 5px #94B8FF; border-top:none;">
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td colspan="6" style=" height:25px; padding-left:20px; font-size:15px; background-color:#94B8FF;" align="center">
                            <asp:Label ID="lblTrainingName" style="font-size:15px; font-style:italic; font-weight:bold;" runat="server"></asp:Label>
                        </td>
                        </tr>
                        <tr id="trPlanningDesc" runat="server" >
                            <td colspan="6" style="height: 20px; padding-left: 20px; font-size: 12px; font-weight: bold;
                                background-color: #F2F2F6;" align="left">
                                Planning Details :
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Start Date &amp; Time :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblStartDt" runat="server"></asp:Label>
                                <asp:TextBox ID="txtStartDt" Visible="false" MaxLength="12" runat="server"></asp:TextBox>
                                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlStHr" runat="server" Visible="false" Width="40px">
                                </asp:DropDownList>
                                &nbsp;<span ID="Span1" runat="server" visible="false">:</span>&nbsp;
                                <asp:DropDownList ID="ddlStMin" runat="server" Visible="false" Width="40px">
                                </asp:DropDownList>
                                (Hrs.)
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtStartDt" TargetControlID="txtStartDt" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                            </td>
                            <td align="right"><b>End Date &amp; Time :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblEndDt" runat="server"></asp:Label>
                                <asp:TextBox ID="txtEndDt" Visible="false" MaxLength="12" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlEtHr" runat="server" Visible="false" Width="40px">
                                </asp:DropDownList>
                                &nbsp;<span ID="Span2" runat="server" visible="false">:</span>&nbsp;
                                <asp:DropDownList ID="ddlEtMin" runat="server" Visible="false" Width="40px">
                                </asp:DropDownList>
                               <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtEndDt" TargetControlID="txtEndDt" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                (Hrs.)</td>
                           <td align="right"><b>Training Location :</b>&nbsp;</td>
                            <td align="left">
                                <asp:TextBox ID="txtLocation" runat="server" Visible="false"></asp:TextBox>
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trTrainingStatus" runat="server">
                            <td  align="right"><b>Status :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label> 
                            </td>
                            <td align="right"><b><asp:Label ID="lblCancelBy" Text="Cancelled By :" runat="server" Visible="false"></asp:Label></b></td>
                            <td align="left">
                                <asp:Label ID="lblCancelledBy" runat="server" Visible="false"></asp:Label>
                            </td>
                           <td align="right"><b><asp:Label ID="lblCancelOn" Text="Cancelled On :" runat="server" Visible="false" ></asp:Label></b></td>
                            <td align="left">
                                <asp:Label ID="lblCancelledOn" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                            <tr>
                                <td align="right"><b>Remarks :</b>&nbsp;</td>
                                <td align="left"><asp:Label ID="lblremarks" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfTrainingId" runat="server" />
                     
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" id="tblDoneDetails" runat="server" visible="false">
                                <tr >
                                    <td align="left" colspan="6" 
                                        style=" height:20px; padding-left:20px; font-size:12px; font-weight:bold; background-color:#F2F2F6;">
                                        Training Status : Done
                                    </td>
                                </tr>
                                <tr >
                                    <td align="right">
                                        <b>Start Date &amp; Time :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTSDT" runat="server"></asp:Label>
                                        (Hrs.)
                                    </td>
                                    <td align="right">
                                        <b>End Date &amp; Time :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTEDT" runat="server"></asp:Label>
                                        (Hrs.)
                                    </td>
                                    <td align="right">
                                        <b>Training Location :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTLocation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <b>Cost :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <b>Currency :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTCurr" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <b>Exchange Rate :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTExcRate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td align="right">
                                        <b>Cost(USD) :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTUSD" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                        </table>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" align="center">
                            List of Attendees
                        </td>
                        </tr>
                        <tr>
                          <td >
                           <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                 <%if (Status.ToString() == "A" && Mode == "E")
                                   { %>
                                     <col style="width:25px;" /> 
                                    <% } %>
                                    <col style="width:150px;" />                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">
                                  <%if (Status.ToString() == "A" && Mode == "E")
                                   { %>
                                        <td></td>
                                         <% } %>
                                        <td>EmpCode</td>
                                        <td>Emp Name</td>
                                        <td>Source</td>
                                        <td>Position</td>
                                        <td>Department
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            <div id="dvEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 150px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                <%if (Status.ToString() == "A" && Mode=="E")
                                   { %>
                                   <col style="width:25px;" />
                                    <% } %> 
                                   <col style="width:150px;" />                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="rptEmpDetails" runat="server">
                                    <ItemTemplate>
                                          <tr>
                                  <%if (Status.ToString() == "A" && Mode == "E")
                                   { %>
                                            <td align="center">
                                                        <asp:HiddenField ID="hfTrainningRecommId" Value='<%#Eval("TrainingRecommId")%>' runat="server" />
                                                        <asp:HiddenField ID="hfImp" Value='<%#Eval("Important")%>' runat="server" />
                                               <asp:ImageButton ID="btnREDelete" runat="server" CausesValidation="false" 
                                                        CommandArgument='<%# Eval("TrainingRecommId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                        OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"   
                                                        OnClick="btnREDelete_Click" 
                                                        ToolTip="Delete" />
                                            </td>
                                             <% } %>
                                            <td align="left">
                                                <%#Eval("EmpCode")%>
                                            </td>
                                            
                                            <td align="left">
                                                <%#Eval("EmpName")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("Source")%>
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
                          
                          </td>
                        </tr>

                        <tr>
                           <td style="padding-right:20px; text-align:right; background-color:#94B8FF; padding-top:5px;" >
                                <asp:Label ID="lblChangePlan" runat="server" style="color:Red; float:left;"></asp:Label>
                                &nbsp;<asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text=" Edit " Width="100px" />
                                &nbsp;<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Visible="false" Width="100px" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="Cancel" Visible="false" Width="100px" />
                           </td>
                        </tr>
                        </table>
                        </div>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" runat="server" id="tblActions">
                           <tr>
                           <td style="padding-right:20px; text-align:center" >
                               <asp:Button ID="btnCancelTraining" runat="server" onclick="btnCancelTraining_Click" Text="Cancel Training" Width="120px" />
                               <asp:Button ID="btnUpdate" Text="Update Training" runat="server" Width="120px" onclick="btnUpdate_Click" />
                               <asp:Button ID="btnAddMore" Text="Add More Attendees " runat="server" onclick="btnAddMore_Click"  Width="133px" />
                           </td>
                        </tr>
                        </table>
                        
                        <!--- Cancel Training -->
                        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="tblCancellRemarks" visible="false" >
                        <center>
                        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:700px; height:170px; padding :3px; text-align :center; border :solid 5px gray; background : white; z-index:150;top:130px;opacity:1;filter:alpha(opacity=100)">
                            <table cellpadding="3" cellspacing="0" width="100%" border="0" >
                            <tr>
                            <td align="center">
                                <b>Enter Remarks...</b>
                            </td>
                            </tr>
                             <tr >
                                <td align="center">
                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Height="84px" required='yes' Width="613px" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                            <td style="text-align:center" >
                                    <asp:Label ID="lblCanMsg" style="color:Red;" runat="server"></asp:Label>&nbsp;
                                    <asp:Button ID="btnCanSave" Text="Save" Width="100px"  runat="server" OnClientClick="javascript:return confirm('Are you sure to cancel training?')" onclick="btnCanSave_Click"></asp:Button>&nbsp;
                                    <asp:Button ID="btnCanCancel" Text="Close" Width="100px" CausesValidation="false" runat="server" onclick="btnCanCancel_Click" ></asp:Button>
                            </td>
                            </tr>
                        </table>
                        </div>
                        </center>
                        </div>
                        <!--- Update Training -->
                        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="divUpdateTraining" visible="false" >
                        <center>
                        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:900px; height:170px; padding :3px; text-align :center; border :solid 5px gray; background : white; z-index:150;top:130px;opacity:1;filter:alpha(opacity=100)">
                            <table cellpadding="4" cellspacing="0" width="100%" border="0">
                                   <tr>
                                    <td colspan="4" 
                                           style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" 
                                           align="center">
                                     
                                     <b>   Update Training</b>
                                    </td>
                                    </tr>
                                   <tr>
                                       <td align="right">
                                           <b>Start Date &amp; Time :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtStartDt1"  MaxLength="12" required='yes' runat="server"></asp:TextBox>                                           
                                           <asp:DropDownList ID="ddlSTHour1" runat="server" Width="40px">
                                           </asp:DropDownList>
                                           &nbsp;
                                           <asp:DropDownList ID="ddlSTMin1" runat="server" Width="40px">
                                           </asp:DropDownList>
                                           (Hrs.)
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                               PopupButtonID="txtStartDt1" TargetControlID="txtStartDt1" PopupPosition="TopLeft">
                                           </ajaxToolkit:CalendarExtender>
                                       </td>
                                       <td align="right">
                                           <b>End Date &amp; Time :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtEndDt1" MaxLength="12" required='yes' runat="server"></asp:TextBox>
                                           <asp:DropDownList ID="ddlETHour1" runat="server"  Width="40px">
                                           </asp:DropDownList>
                                           &nbsp;
                                           <asp:DropDownList ID="ddlETMin1" runat="server" Width="40px">
                                           </asp:DropDownList>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                               PopupButtonID="txtEndDt1" TargetControlID="txtEndDt1" PopupPosition="TopLeft">
                                           </ajaxToolkit:CalendarExtender>
                                           (Hrs.)
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right">
                                           <b>Training Location :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtLocation1" required='yes' MaxLength="50" runat="server" ></asp:TextBox>                                           
                                       </td>
                                       <td align="right">
                                           <b>Cost :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtCost" runat="server" MaxLength="8" ></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right">
                                           <b>Currency &amp; Exch. Rate :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" Width="100px" onselectedindexchanged="ddlCurrency_SelectedIndexChanged">
                                           </asp:DropDownList>
                                       &nbsp;<asp:TextBox ID="txtExcRate" runat="server" MaxLength="8" Width="80px"></asp:TextBox>
                                       </td>                                   
                                       <td align="right">
                                           <b>Cost(USD) :</b>&nbsp;
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtCostUSD" runat="server" MaxLength = "8"></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right" colspan="4" style="text-align: center">
                                           <asp:Button ID="btnUpdateTrainingDetails" Text="Save" Width="100px" runat="server" onclick="btnUpdateTrainingDetails_Click" />
                                           <asp:Button ID="btnHide" Text="Close" runat="server" onclick="btnHide_Click" Width="100px"/>    
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="right" colspan="4" style="text-align: center">
                                          <asp:Label ID="lblUpdateTraining" style="color:Red;" runat="server"></asp:Label> 
                                       </td>
                                   </tr>
                                                                      
                               </table>
                        </div>
                        </center>
                        </div>
                        <!--- Add Attendees -->
                        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvAddEmp" visible="false" >
                        <center>
                        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:900px; height:400px; padding :3px; text-align :center; border :solid 5px gray; background : white; z-index:150;top:130px;opacity:1;filter:alpha(opacity=100)">
                            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                            <tr>
                          <td>
                          <div style="height: 25px; padding-left: 20px; font-size: 15px; padding-bottom:5px; background-color: #d2d2d2;"
                                align="center">
                                Add Attendees
                            </div>
                          <div style=" background-color:#e2e2e2">
                            <table border="0" cellpadding="2" cellspacing="0" style="width:100%;border-collapse:collapse;">
                            <tr>
                            <td style="width:100px; text-align:left"><b>Emp Code : </b></td>
                            <td style="width:100px; text-align:left">
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
                                    <col style="width:80px;" />                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">
                                        <td>
                                        </td>
                                        <td>
                                            EmpCode
                                        </td>
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
                            <div id="divAddEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 250px; text-align:center;">
                           
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />
                                   <col style="width:80px;" />                                     
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
                                            <td align="left">
                                                <%#Eval("EmpCode")%>
                                                <asp:HiddenField ID="hfAddEmpId" Value='<%#Eval("EmpId")%>' runat="server" />
                                                <asp:HiddenField ID="hfRecommId" Value='<%#Eval("TrainingRecommId")%>' runat="server" />
                                            </td>
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
                              
                          </td>
                        </tr>
                         <tr>
                            <td>
                            <asp:Label ID="lblAddMsg" Text="" style="color:Red; " runat="server"></asp:Label>
                            </td>
                         </tr>
                        <tr>
                          <td>
                          <asp:Button ID="btnAddEmp" Text="Add" runat="server" Width="100px" onclick="btnAddEmp_Click"/>
                              <asp:Button ID="btnCloseAdding" Text="Close" runat="server" Width="100px" onclick="btnCloseAdding_Click"/>
                              
                           </td>
                        </tr>
                            </table>
                        </div>
                        </center>
                        </div>
                        </td>
                    </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>         
    </div>
    </form>
</body>
</html>

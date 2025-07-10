<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_TrainingRecords.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_TrainingRecords" %>
<%@ Register src="HR_PersonalHeaderMenu.ascx" tagname="HR_PersonalHeaderMenu" tagprefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function ShowTrainingDetails(TrainingPlanningId) {
            if (TrainingPlanningId>0)
                window.open("PopUpTrainingDetails.aspx?TrainingPlanningId=" + TrainingPlanningId + "", "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1" >
        <ContentTemplate>
          <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                Training Record : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                             <div>
                                <uc2:HR_PersonalHeaderMenu ID="Emtm_HR_PersonalHeaderMenu1" runat="server" /> 
                             </div> 
                            </td>
                        </tr>
                        </table>  
                        <br />
                        <div style=" padding-top :5px;" >
                        <fieldset>
                        <legend></legend>
                        <table border="0" cellpadding="" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="6" style="padding-bottom:7px;">
                             <asp:LinkButton ID="lbAssigntraining" Text="Assign training" OnClick="lbAssigntraining_Click" style="float:left; padding-left:5px;" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <%--Here--%>
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td>
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                             <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;height:26px;">
                                <colgroup> 
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:90px;" />                              
                                    <col style="width:160px;" />
                                    <col style="width:160px;" />
                                    <col style="width:90px;" />
                                    <col style="width:110px;" />
                                    <col style="width:100px;" /> 
                                    <col style="width:25px;" />                                                                       
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>   
                                        <td></td>                                
                                        <td>Training Title</td>
                                        <td>Due Dt.</td>
                                        <td>Plan Dt.</td>
                                        <td>Done Dt.</td>
                                        <td>Expiry Dt.</td>
                                        <td>Status</td>
                                        <td>Source</td>
                                        <td></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                            <div id="dvEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:90px;" />    
                                    <col style="width:160px;" />
                                    <col style="width:160px;" />                          
                                    <col style="width:90px;" />
                                    <col style="width:110px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />                                    
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptRecordList" runat="server">
                                    <ItemTemplate>
                                          <tr class='<%# (Common.CastAsInt32(Eval("TrainingPlanningId"))==TrainingPlanningId)?"selectedrow":"row"%>' >
                                          <td align="center">
                                               <asp:ImageButton ID="imgDeleteRecord" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete Record" CommandArgument='<%#Eval("TrainingRecommId")%>' Visible='<%#Eval("STATUS").ToString() != "DONE" %>' OnClick="imgDeleteRecord_Click" runat="server" /> 
                                               </td>
                                          <td align="center">
                                               <asp:ImageButton ID="imgEditRecord" ImageUrl="~/Modules/HRD/Images/edit.jpg" ToolTip="Edit Record" CommandArgument='<%#Eval("TrainingRecommId")%>' Visible='<%#Eval("TrainingPlanningId").ToString() == "0" %>' OnClick="imgEditRecord_Click" runat="server" /> 
                                               </td>
                                            <td align="left">
                                            <a title="Click to view training details." href="#" style="cursor:pointer;" onclick="ShowTrainingDetails(<%#Eval("TrainingPlanningId")%>)">
                                            <%#Eval("TrainingName")%>
                                            </a>
                                            </td>                                            
                                            <td align="left"><%# Eval("DueDate")%></td>
                                            <td align="left"><%#Eval("PlanDate")%></td>                                             
                                            <td align="left"><%#Eval("DoneDate")%></td>
                                            <td align="left"><%#Eval("ExpiryDate")%></td>
                                            <td align="left"><%#Eval("Status")%></td>
                                            <td align="left"><%#Eval("SOURCE")%></td>
                                            <td align="center">
                                               <a runat="server" ID="ancFile"  href='<%# "~/EMANAGERBLOB/HRD/EmpTrainingRecord/" + Eval("FileName").ToString() %>' target="_blank" visible='<%#Eval("FileName").ToString()!= "" %>'  title="Show Certificates" >
                                                <img src="../../Images/Emtm/paperclip.png" style="border:none"  />
                                                </a>
                                            </td>
                                            <td align="center">
                                               <asp:ImageButton ID="btnEdit" ImageUrl="~/Modules/HRD/Images/upload.jpg" ToolTip="Upload Certificates" CommandArgument='<%#Eval("TrainingPlanningId")%>' Visible='<%# (Eval("Status").ToString().ToUpper()== "COMPLETED") %>' OnClick="btnEdit_Click" runat="server" /> 
                                               </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>                                    
                                </td>
                            </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                        </fieldset> 
                        </div>
                    <br />
                        <div id="divUploadDocs" runat="server" visible="false" style=" padding-top :5px;" >
                        <fieldset>
                        <legend><strong>Upload Document</strong></legend>
                        <table border="0" cellpadding="" cellspacing="0" width="100%">
                        <tr>
                             <td style="width:100px;">&nbsp;</td>
                             <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" >Select File :&nbsp;</td>
                            <td align="left">
                              <asp:FileUpload ID="FileUpload1" CssClass="btn" runat="server" Width="438px"/>    
                           </td>
                        </tr>
                        <tr>
                                <td colspan="2" style=" float:right; padding-right:20px;">
                                     <asp:Label ID="lblMsg" Style="color:Red;" runat="server"></asp:Label>
                                    <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" OnClick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn" runat="server" OnClick="btncancel_Click" Text="Cancel" CausesValidation="false"></asp:Button>
                                    
                                </td>
                            </tr>
                    </table>
                        </fieldset> 
                        </div>                    
                    </td>
                    </tr>
            </table>

              <div id="divAssignTraining"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
                 <center>
                       <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:1150px; height:450px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <div style="background-color:#c2c2c2; padding:7px; text-align:center;"><b style="font-size:14px;">Select Trainings</b></div>
                            <div style=" height:2px;"></div> 
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;"> 
                              <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup> 
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:120px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>                                  
                                        <td>Training Title</td>
                                        <td>Due Date</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                            <div id="Div1" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:120px;" />
                                    <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptAssignTraining" runat="server">
                                    <ItemTemplate>
                                          <tr class="row">
                                            <td>
                                             <asp:CheckBox ID="chkSelect" runat="server" />
                                            </td>
                                            <td align="left">
                                                <%#Eval("TrainingName")%>
                                                <asp:HiddenField ID="hfdTrainingId" Value='<%#Eval("TrainingId") %>' runat="server" />
                                            </td>
                                            <td align="center">
                                              <asp:TextBox ID="txtDueDt" MaxLength="11" Width="100px" runat="server"></asp:TextBox>

                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                               PopupButtonID="txtDueDt" TargetControlID="txtDueDt" PopupPosition="BottomRight">
                                           </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>&nbsp</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                              <div style=" float:right; padding:10px;">
                                 <asp:Label ID="lblAssignMsg" Style="color:Red;" runat="server"></asp:Label>
                                 <asp:Button ID="btnAssignTraining" OnClick="btnAssignTraining_Click" CssClass="btn" Text="Save" runat="server" />
                                 <asp:Button ID="btnCancelAssign" OnClick="btnCancelAssign_Click" CssClass="btn" Text="Cancel" runat="server" />

                              </div>
                        </div>
                </center>
            </div>  

            <div id="divEditTraining"  style="position:absolute;top:0px;left:0px; height :220px; width:100%;z-index:100;" runat="server" visible="false" >
                 <center>
                       <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:450px; height:220px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <div style="background-color:#c2c2c2; padding:7px; text-align:center;"><b style="font-size:14px;">Edit Training</b></div>
                            <div style=" height:2px;"></div>  
                             <%-- <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup> 
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:120px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>                                  
                                        <td>Training Title</td>
                                        <td>Due Date</td>
                                        <td></td>
                                    </tr>
                                </colgroup>
                            </table>
                            <div id="Div3" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                    <col style="width:25px;" />
                                    <col  /> 
                                    <col style="width:120px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                          <tr class="row">
                                            <td>
                                             <asp:CheckBox ID="chkSelect" runat="server" />
                                            </td>
                                            <td align="left">
                                                <%#Eval("TrainingName")%>
                                                <asp:HiddenField ID="hfdTrainingId" Value='<%#Eval("TrainingId") %>' runat="server" />
                                            </td>
                                            <td align="center">
                                              <asp:TextBox ID="txtDueDt" MaxLength="11" Width="100px" runat="server"></asp:TextBox>

                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                               PopupButtonID="txtDueDt" TargetControlID="txtDueDt" PopupPosition="BottomRight">
                                           </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                              <div style=" float:right; padding:10px;">
                                 <asp:Label ID="Label1" Style="color:Red;" runat="server"></asp:Label>
                                 <asp:Button ID="Button1" OnClick="btnAssignTraining_Click" CssClass="btn" Text="Save" runat="server" />
                                 <asp:Button ID="Button2" OnClick="btnCancelAssign_Click" CssClass="btn" Text="Cancel" runat="server" />

                              </div>--%>

                              <table border="0" cellpadding="2" cellspacing="2" rules="all" style="width:100%;border-collapse:collapse;">
                               <tr>
                                 <td style=" text-align:right; font-weight:bold;" > Training :&nbsp;</td>
                                 <td style=" text-align:left;"><asp:DropDownList ID="ddlEditTraining" Width="250px" runat="server"></asp:DropDownList>
                                              </td>
                               </tr>
                               <tr>
                                 <td style=" text-align:right; font-weight:bold;" > Due Date :&nbsp;</td>
                                 <td style=" text-align:left;"><asp:TextBox ID="txtEditDueDt" MaxLength="11" Width="120px" runat="server"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                               PopupButtonID="txtEditDueDt" TargetControlID="txtEditDueDt" PopupPosition="BottomRight">
                                           </ajaxToolkit:CalendarExtender></td>
                               </tr>
                               <tr>
                               <td></td>
                               <td></td>
                               </tr>
                               <tr>
                               <%--<td><asp:Label ID="lblEditMsg" Style="color:Red;" runat="server"></asp:Label></td>--%>
                               <td colspan="2" style="text-align:right; padding-right:10px;">
                                 <asp:Label ID="lblEditMsg" Style="color:Red; float:left;"  runat="server"></asp:Label>
                                 <asp:Button ID="btnEditTraining" OnClick="btnEditTraining_Click" CssClass="btn" Text="Save" runat="server" />
                                 <asp:Button ID="btnCancelEditTraining" OnClick="btnCancelEditTraining_Click" CssClass="btn" Text="Cancel" runat="server" /></td>
                               </tr>
                              </table>
                        </div>
                </center>
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
        </asp:UpdatePanel> 
        
    </div>

    </form>
</body>
</html>

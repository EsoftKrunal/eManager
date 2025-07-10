<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_Peap.aspx.cs" Inherits="Emtm_HR_Peap" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/PeapHeaderMenu.ascx" tagname="Hr_PeapHeaderMenu" tagprefix="Peap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <script type="text/javascript" src="../../Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function showReport(PID) {
            window.open('../../Reporting/PeapReport.aspx?PId=' + PID, 'asdf', '');
        }
        function showReportClosure(PID) {
            window.open('../../Reporting/PeapReport.aspx?Mode=C&PId=' + PID, 'asdf', '');
        }
        
        function selectrow(row) {
            var child = row.getElementsByTagName("a");
            child[0].click();
        }
    </script>
    <style type="text/css">
    .myselect
    {
        line-height:30px;
        padding:5px;
        height:30px;
    }
    </style>

    <div style="font-family:Arial;font-size:12px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <table width="100%">
                <tr>
                    
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div style="">
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;background-color:#f9f9f9">
                            <tr>
                                <td> <Peap:Hr_PeapHeaderMenu ID="uc_Peap" runat="server" />  </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px; text-align:center; color:Red">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing ="0" border="1" style=" border-collapse:collapse;" width ="100%">
                                <tr>
                                <td style=" background-color:#e2e2e2;">
                                    <table border="0" cellspacing="0" cellpadding="2" style="text-align: center  ; padding-right:5px;" width="100%">
                                        <tr>
                                            <td style="width: 120px; text-align: right;">Year:</td>
                                            <td style="width: 108px; text-align: left;"><asp:DropDownList ID="ddlYear" runat="server" Width="80px" TabIndex="1"></asp:DropDownList></td>
                                            <td style="width: 139px; text-align: right;">Office :</td>
                                            <td style="width: 159px; text-align: left"><asp:DropDownList ID="ddlOffice" runat="server" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged" ></asp:DropDownList></td>
                                            <td style="width: 120px; text-align: right;">Peap Level :</td>
                                            <td style="text-align: left"><asp:DropDownList ID="ddlPeapCategory" runat="server" Width="175px" TabIndex="7"></asp:DropDownList></td>
                                            <td></td>
                                            <td style="text-align:right">
                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">Name:</td>
                                            <td style="text-align: left"><asp:TextBox ID="txt_FirstName_Search" runat="server" MaxLength="24" Width="160px" TabIndex="2"></asp:TextBox></td>
                                            <td style="text-align: right;">Position :</td>
                                            <td style="text-align: left"><asp:DropDownList ID="ddlPosition" runat="server" Width="165px" ></asp:DropDownList></td>
                                            <td style="text-align: right;">Status :</td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="175px" >
                                                    <asp:ListItem Text=" All " Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Self Assessment" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Assessment By Appraiser" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Management FeedBack" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="With Management" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Peap Closed" Value="4"></asp:ListItem>
                                                </asp:DropDownList>    
                                            </td>
                                        </tr>
                                                                                                       
                                    </table>
                                    <div>
                                    <table border="0" cellspacing="0" cellpadding="2" style="text-align: center  ; padding-right:5px;" width="100%">
                                       <tr>
                                           <td style="text-align:left"><strong>Total Records :&nbsp;<asp:Label ID="RowsCounter" runat="server" ></asp:Label>&nbsp;</strong></td>
                                           <td style="text-align:right">
                                                <asp:Button ID="btn_add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_add_Click" Text="Add" Width="60px" />
                                                <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="60px" />
                                                <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="60px" />
                                                <asp:Button ID="btn_Print" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Print_Click" Text="Print" Width="60px" />
                                           </td>
                                        </tr>         
                                    </table>
                                    </div>
                                </td>
                                </tr>
                                 <tr>
                                 <td>
                                   <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td colspan="6">
                                                <div style=" width:100%; height:405px; overflow:hidden;" > 
                                                <center>
                                                    <asp:Label ID="lblGrid" runat="Server"></asp:Label>
                                                </center>
                                                <div >
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                <table border="1" cellpadding="2" cellspacing="0" rules="columns" style="width:100%;border-collapse:collapse; height:26px;" bordercolor="white">
                                                   <colgroup>
                                                   <col style="width:70px;" />
                                                   <col style="width:80px;" />
                                                   <col />
                                                   <col style="width:200px;" />
                                                   <col style="width:135px;" />
                                                   <col style="width:135px;" />
                                                   <col style="width:50px;" />
                                                   <col style="width:25px;" />
                                                   </colgroup>
                                                   <tr align="left" style="background-color:#4D94FF; font-weight:bold; color:White;"> 
                                                       <td></td>   
                                                       <td><asp:LinkButton runat="server" Text="App. Type" ID="lbAppType" CommandArgument="AppraiselType" OnClick="lnkSort_Click" style="color:White;"/></td>
                                                       <td><asp:LinkButton runat="server" Text="Name" ID="lnkName" CommandArgument="EMPNAME" OnClick="lnkSort_Click" style="color:White;"/></td>
                                                       <td><asp:LinkButton runat="server" Text="Position" ID="lnkPosition" CommandArgument="POSITIONNAME" OnClick="lnkSort_Click" style="color:White;"/></td>
                                                       <td><asp:LinkButton runat="server" Text="Dept." ID="lnkDept" CommandArgument="DEPARTMENTNAME" OnClick="lnkSort_Click" style="color:White;"/></td>
                                                       <td>Status</td>
                                                       <td></td>
                                                       <td>&nbsp;</td>
                                                   </tr>
                                           </table> 
                                           </div>          
                                           <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 370px ; text-align:center;" id="dvscroll_Peap"  onscroll="SetScrollPos(this)">
                                           <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                 <colgroup>
                                                   <col style="width:70px;" />
                                                   <col style="width:80px;" />
                                                   <col />
                                                   <col style="width:200px;" />
                                                   <col style="width:135px;" />
                                                   <col style="width:135px;" />
                                                   <col style="width:50px;" />
                                                   <col style="width:25px;" />
                                                   </colgroup>
                                                 <asp:Repeater ID="rptData" runat="server" >
                                               <ItemTemplate>
                                                  <tr  style="cursor:default; color:#555" >
                                                       <td align="left">
                                                       <asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnViewPeap_Click" ToolTip="View PEAP" ImageUrl="~/Modules/HRD/Images/magnifier.png" style="cursor:pointer"/>
                                                       <asp:ImageButton ID="btn_Delete" runat="server" ToolTip="Delete PEAP" CommandArgument='<%# Eval("PEAPID") %>' Visible='<%#Common.CastAsInt32(Eval("STATUS1"))<2%>' OnClick="btn_Delete_Click" OnClientClick="javascript:return confirm('Are you sure to delete?');" ImageUrl="~/Modules/HRD/Images/delete1.gif" style="cursor:pointer"/>
                                                       <asp:ImageButton ID="btnPeapReport" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnPeapReport_Click" ToolTip="View Peap Report" ImageUrl="~/Modules/HRD/Images/print_16.png"/>
                                                       <asp:HiddenField ID="hfStatus" Value='<%#Eval("STATUS1")%>' runat="server" />
                                                       </td>
                                       
                                                       <td align="left"><%#Eval("AppraiselType")%></td>
                                                       <td align="left"><%#Eval("EMPNAME")%><asp:HiddenField ID="hfEmpId" Value='<%#Eval("EMPID")%>' runat="server" /></td>
                                                       <td align="left"><%#Eval("POSITIONNAME")%></td>
                                                       <td align="left"><%#Eval("DEPARTMENTNAME")%></td>
                                                       <td align="left"><%#Eval("STATUS")%></td>
                                                       <td>
                                                            <asp:ImageButton ID="btnFordward" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnFordward_Click" ToolTip="Forward to Appraiser" Visible='<%# Eval("STATUS1").ToString() == "1" %>' ImageUrl="~/Modules/HRD/Images/forward.png"/>
                                                            <asp:ImageButton ID="btnForward_M" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnForward_M_Click" ToolTip="Forward to Management" Visible='<%# Eval("STATUS1").ToString() == "3" %>' ImageUrl="~/Modules/HRD/Images/forward.png"/>
                                                            <asp:ImageButton ID="btnCloasure" runat="server" CommandArgument='<%# Eval("PEAPID") %>' OnClick="btnCloasure_Click" ToolTip="Peap Closure" Visible='<%# Eval("STATUS1").ToString() == "6" %>' Height="12px" ImageUrl="~/Modules/HRD/Images/favicon.png"/>
                                                       </td>
                                                       <td>&nbsp;</td>
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
                                    
                                    <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_djc_from" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_djc_to" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>--%>
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
         
         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:500px; height:320px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                 <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                <table cellpadding="3" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td colspan="4" style="background-color:#d2d2d2; padding:4px;"><b> Create New Peap </b></td>
                    </tr>
                    <tr>
                        <td>
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100px; text-align:right;"> Occasion : </td>
                        <td style=" text-align:left;">
                            <asp:DropDownList ID="ddlOccasion" OnSelectedIndexChanged="ddlOccasion_SelectedIndexChanged" required="yes" AutoPostBack="true" runat="server" Width="160px" >
                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Routine" Value="R"></asp:ListItem>
                            <asp:ListItem Text="Interim" Value="I"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlOccasion"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr id="troffice" runat="server" visible="true">
                        <td style="width:100px; text-align:right;"> Office :</td>
                        <td style="text-align:left; ">
                            <asp:DropDownList ID="ddlOffice_CNP" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_CNP_SelectedIndexChanged" runat="server" required="yes" Width="160px"></asp:DropDownList>                         
                            
                        </td>
                        <td > </td>
                        <td style="text-align:left;"></td>
                    </tr>
                    <tr>
                        <td style="width:100px; text-align:right;"> Department :</td>
                        <td style="text-align:left; ">
                            <asp:DropDownList ID="ddlDepartment_CNP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_CNP_SelectedIndexChanged" required="yes" Width="160px"></asp:DropDownList>
                                                     
                            
                        </td>
                        <td > </td>
                        <td style="text-align:left;"></td>
                    </tr>
                    <tr>
                        <td style="width:100px; text-align:right;"> Position :</td>
                        <td style="text-align:left; ">
                            <asp:DropDownList ID="ddlPosition_CNP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPosition_CNP_SelectedIndexChanged" required="yes" Width="160px"></asp:DropDownList>
                                                     
                            
                        </td>
                        <td > </td>
                        <td style="text-align:left;"></td>
                    </tr>                    
                    <tr id="trEmployees" runat="server" visible="true">
                        <td style="width:100px; text-align:right;"> Employee :</td>
                        <td style="text-align:left; ">
                            <asp:DropDownList ID="ddlEmployeeList" runat="server" required="yes" Width="260px"></asp:DropDownList>&nbsp;<asp:CheckBox ID="chkSelAll" AutoPostBack="true" Text="All Employees" OnCheckedChanged="chkSelAll_CheckedChanged" runat="server" Visible="false" />                          
                            
                        </td>
                        <td > </td>
                        <td style="text-align:left;">
                            
                        </td>
                    </tr>
                    <tr id="trAppPeriod" runat="server" visible="true">
                        <td style="text-align:right;"> Period From :</td>
                        <td style="text-align:left; ">
                            <asp:TextBox ID="txtPeriodFrom" runat="server"  required="yes" MaxLength="15"  Width="80px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPeriodFrom" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtPeriodFrom"></asp:RequiredFieldValidator>                            
                            &nbsp; Period To :&nbsp;
                           <asp:TextBox ID="txtPeriodTo" runat="server"  required="yes" MaxLength="15" Width="80px"  ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPeriodTo" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtPeriodTo"></asp:RequiredFieldValidator>                            
                        </td>
                        <td  style="text-align:right;"> </td>
                        <td style="text-align:left;">
                            <%--<asp:TextBox ID="txtPeriodTo" runat="server"  required="yes" MaxLength="15" Width="80px"  ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPeriodTo" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtPeriodTo"></asp:RequiredFieldValidator>                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3" style="text-align:left;">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" Text="Submit" Width="100px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" onclick="btnCancel_Click" Text="Cancel" CausesValidation="false" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblMsgAddNew" runat="server" style="color:Red;"></asp:Label>                            
                        </td>
                    </tr>
                </table>
                
                 </ContentTemplate>
                 <Triggers>
                 <asp:PostBackTrigger ControlID="btnCancel" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>

         <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvForwardForAppraisal" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :510px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:550px; height:200px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                <center>
                <table cellpadding="3" cellspacing="0" border="0" width="540px;">
                    <tr><td style="padding: 4px; background-color: #d2d2d2;" colspan="2"><b>Please Select Appraiser from Below.</b></td></tr>
                    <tr>
                        <td >
                            <div style="OVERFLOW-Y: scroll; overflow-X: hidden; height: 140px; width:100%; border:1px solid #d2d2d2; float:right; text-align:left;" >
                             <asp:CheckBoxList ID="ddlAppraisers" runat="server" required="yes"></asp:CheckBoxList>
                            </div>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Button ID="btnForwardApp" runat="server" OnClick="btnForwardApp_Click" CssClass="btn" Text="Submit"  />
                            <asp:Button ID="btnCancelForward" runat="server" CssClass="btn"  OnClick="btnCancelForward_Click"  Text="Cancel" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="lblForwardMsg" runat="server" style="color:Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
                </center>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnCancelForward" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>

         <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvForwardForManagement" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :510px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:550px; height:200px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                <center>
                <table cellpadding="3" cellspacing="0" border="0" width="540px;">
                    <tr>
                        <td colspan="2" style="background-color:#d2d2d2; padding:4px;"><b> Forward for Management FeedBack </b></td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" style="background-color:#e2e2e2; padding:4px;"><b> Please Select Manager from Below.</b></td>
                    </tr>
                    <tr>
                        <td >
                            <div style="OVERFLOW-Y: scroll; overflow-X: hidden; height: 100px; width:350px; border:1px solid #d2d2d2; float:right; text-align:left;" >
                             <asp:CheckBoxList ID="cblManagers" runat="server" required="yes"></asp:CheckBoxList>
                            </div>
                        </td>
                        <td style="text-align:center">
                            <asp:Button ID="btnSave_MFB" runat="server" OnClick="btnSave_MFB_Click" CssClass="btn" Text="Submit"  />
                            <asp:Button ID="btnCancel_MFB" runat="server" CssClass="btn"  OnClick="btnCancel_MFB_Click"  Text="Cancel" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <asp:Label ID="lblMsg_MFB" runat="server" style="color:Red;"></asp:Label> 
                        </td>
                    </tr>
                </table>
                </center>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnCancel_MFB" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>

    </div>
   </asp:Content>

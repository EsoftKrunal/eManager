<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHoto.aspx.cs" Inherits="Emtm_OfficeAbsenceHoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function openwindow(id) {

            window.open("Emtm_PopupAttachment.aspx?expid=" + id, "att", "");
        }
        function openreport(id) {

            // window.open("../../Reporting/OfficeAbsence_Expense.aspx?id=" + id, "report", "");
            window.open('../../Reporting/OfficeAbsenceExpense.aspx?id=' + id, 'asdf', '');
        }
        function CheckVSL(check) {
            var Vsl = check.getAttribute('vsl');
            var X = document.getElementsByName(Vsl);
            var i = 0;
            for (i = 0; i <= X.length - 1; i++) {
                X[i].getElementsByTagName("input")[0].checked = check.checked;
            }
        }
        function CheckAll(Obj) {
            var X = document.getElementsByTagName('input');
            for (i = 0; i <= X.length - 1; i++) {
                X[i].checked = Obj.checked;
            }
        }
              
   </script>
    <style type="text/css">
        
    .btn11sel_1
    {
        font-size:14px;
        background-color:#FFEBE0;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #FFEBE0;
        padding:5px;
    }
    .btn11sel
    {
        font-size:14px;
        background-color:#C2E0FF;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #C2E0FF;
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
    .btnstlnew
    {
        background-color:#E6B2FF; 
        border:solid 1px #2e2e4c; 
        padding:3px; 
        color:Black;
    }
    .csslnkclose
    {
        text-decoration:none;
        color:Blue;
    }
    .csslnkclose:hover
    {
        text-decoration:underline;
        color:Red;
    }
    .csslnkclose:active
    {
        text-decoration:none;
        color:Blue;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
    <div style="position:fixed;top:0px; height:150px; z-index:0;" id="dvHandOverHeader" runat="server">
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Handover/TakeOver</div>
        <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0"  style="background-color:White">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                  <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblNotifiedon" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblTakeOverOn" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                </td>
                </tr>
        </table>
        <div style="background-color:White; padding:3px 0px 3px; text-align:right;">
            <asp:Button ID="btnFleetImportPopup" runat="server" Text="Import Fleet Notes" OnClick="btnOpenFleetNotePopup_OnClick" CssClass="btn" Width="150px"/>            
            <asp:Button ID="btnAddFleetPopup" runat="server" Text="Add Notes" OnClick="btnAddFleetPopup_OnClick" CssClass="btn" CommandArgument="H" Width="150px"/>            
        </div>
        <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
            <col width="20px" />
            <col width="20px" />
            <col width="20px" />
            <col  />
            <col width="120px" /> 
            <tr style=" background-color:#4DB8FF; height:30px; color:White; font-weight:bold;" >
                <td></td>
                <td></td>
                <td></td>
                <td>Handover Items List</td>
                <td>Closure</td>
            </tr>
        </table>
    </div>
    <div id="dvHandOverData" runat="server">
    <div style="margin-top:240px; overflow-y:scroll;overflow-x:hidden; height:240px;">
        <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="border-collapse:collapse;border:none;text-align:left; border-color:gray;">
            <col width="20px" />
            <col width="20px" />
            <col width="20px" />
            <col  />
            <%--<col width="100px" />--%>
            <col width="120px" /> 
            <asp:Repeater ID="rptHotoDetails" runat="server">
                            
                <ItemTemplate>
                    <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                        <td colspan="7" style="background-color:#FFEBCC">
                            <img src="../../Images/arrow_blue.gif" style="float:left; margin-top:3px;" /> <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                        </td>
                    </tr>
                    <tr style=" background:white" >
                                
                        <td style="text-align:center">
                            <asp:ImageButton ID="btnView" OnClick="btnViewFleetNotes_OnClick" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass1.gif" CommandArgument='<%#Eval("TID")%>' />
                        </td>
                                    
                        <td style="text-align:center">
                            <asp:ImageButton ID="btnEditFleetNotes" runat="server" OnClick="btnEditFleetNotes_OnClick" ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" CommandArgument='<%#Eval("TID")%>' />
                        </td>
                        <td style="text-align:center">
                            <asp:ImageButton ID="btnDeleteFleetNote" runat="server" OnClick="btnDeleteFleetNote_OnClick" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("TID")%>' Visible='<%# (!AnyAcceptedORTakeOver) %>' OnClientClick="return confirm('Are you sure to delete this fleet note?');" />
                        </td>
                        <td> 
                        <div style=" height:15px; overflow:hidden;"> 
                            <span style="color:Red"> ( <%#Common.ToDateString(Eval("CreatedOn"))%> )</span>
                            <span style="color:Blue"><i><%#Eval("CreatedByName")%></i></span>
                            <span><%#Eval("TOPICDETAILS")%></span>
                        </div>
                        </td>
                        <%--<td style="text-align:center"> &nbsp; <%#Common.ToDateString(Eval("DueDate"))%></td>--%>
                        <td style="text-align:center">
                            <asp:LinkButton ID="lnkTakeOverClosure" runat="server" Text=" Close Now " OnClick="lnkTakeOverClosure_OnClick" OnClientClick="return confirm('Are you sure to close ?');"  CommandArgument='<%#Eval("TID")%>' Visible='<%# (Eval("ClosedBy").ToString()=="") %>' CssClass="csslnkclose"></asp:LinkButton>
                            <%# Common.ToDateString( Eval("ClosedOn"))%> 
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div style="position:fixed;bottom:0px;">      
            <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;background-color:#C2E0FF; border-collapse:collapse; border-color:#C2E0FF;" rules="all">
            <tr>
            <td style="text-align: right;"> Handover To: </td>
            <td>
                <asp:DropDownList ID="ddlHandover" runat="server" required='yes'></asp:DropDownList>
                <asp:Label runat="server" id="lblHAccrejOn"></asp:Label>
            </td>
            <td style="text-align: right;"> Backup Handover To: </td>
            <td>
                <asp:DropDownList ID="ddlBackup" runat="server" ></asp:DropDownList>
                <asp:Label runat="server" id="lblBAccrejOn"></asp:Label>
            </td>           
            <td style="text-align:right">
                <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" onclick="btnSave_Click" Width="80px" />
                <asp:Button ID="btnNotify" Text="Notify" CssClass="btn" runat="server" onclick="btnNotify_Click"  Width="120px" OnClientClick="return PostanddisableMe(this);"/>
                <asp:Button ID="btnClose" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  Width="80px" />
            </td>
        </tr>
        <tr>
        <td colspan="5" style="text-align:right; background-color:#FFFFCC; padding:5px;">&nbsp;<b><asp:Label ID="lblUMsg" ForeColor="Red" runat="server" Font-Size="Large"  ></asp:Label></b>
        </td>
        </tr>
            </table>
       </div>
    </div>

    <div id="dvTakeOver" runat="server">
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Takeover</div>
         <table id="Table2" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation1" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose1" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod1" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay1" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName1" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections1" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                  <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblNotifiedon1" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblTakeOverOn1" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="lblRemarks1"></asp:Label>
                </td>
                </tr>
        </table>
        <div style='text-align:right;'>
         <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; border-color:#FFEBE0" rules="all">
            <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Departure Date :
            </td>
            <td style="text-align:left"><asp:TextBox ID="txtActLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes'
                        ValidationGroup="planleave"></asp:TextBox>
                    <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                    <asp:RequiredFieldValidator ID="rfvleavefrom" runat="server" ControlToValidate="txtActLeaveFrom"
                        ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlEtHr" runat="server" Width="40px">
                    </asp:DropDownList>
                    &nbsp;(Hrs.)&nbsp;
                    <asp:DropDownList ID="ddlEtMin" runat="server" Width="40px">
                    </asp:DropDownList>&nbsp;(Min.)&nbsp;
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" TargetControlID="txtActLeaveFrom">
                    </ajaxToolkit:CalendarExtender>
            </td>
            <td style="text-align: right; ">Arrival Date ( In office ) : </td>
            <td style="text-align: left;">
                    <asp:TextBox ID="txtEndDt" runat="server" MaxLength="11" Width="90px" required='yes'></asp:TextBox>
                    <asp:ImageButton ID="imgEndDt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />                                       
                    <asp:DropDownList ID="ddlEndHr" runat="server" Width="40px">
                    </asp:DropDownList>
                    &nbsp;(Hrs.)&nbsp;
                    <asp:DropDownList ID="ddlEndMin" runat="server" Width="40px">
                    </asp:DropDownList>&nbsp;(Min.)&nbsp;
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgEndDt" PopupPosition="TopLeft" TargetControlID="txtEndDt">
                    </ajaxToolkit:CalendarExtender>
            </td>
            </tr>
            <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Take Over Date :
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="txtTakeOverDate" runat="server" MaxLength="11" Width="90px" required='yes'></asp:TextBox>
                <asp:ImageButton ID="btntodt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />                                       
                 <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="btntodt" PopupPosition="TopLeft" TargetControlID="txtTakeOverDate">
                    </ajaxToolkit:CalendarExtender>
            </td>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                
            </td>
            <td style="text-align:left">
            </tr>
            <tr>
            <td colspan="4">&nbsp;
            <asp:Label ID="lblUMsg1" ForeColor="Red" runat="server" Font-Size="Large"  ></asp:Label>
                    <asp:Button ID="btnSave1" Text="Save" CssClass="btn" runat="server" onclick="btnSave1_Click" Width="80px" />
                    <asp:Button ID="btnNotify1" Text="Notify" CssClass="btn" runat="server" onclick="btnNotify1_Click"  Width="120px" OnClientClick="return PostanddisableMe(this);"/>
                    <asp:Button ID="Button1" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  Width="80px" />
            </td>
            </tr>
            </table>
            </div>
    </div>
        
        

        <%--------------  Import Fleet Popup  ------------  --%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvImportFleet" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color:Gray; z-index:155; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:160;top:80px;opacity:1;filter:alpha(opacity=100)">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <colgroup>
                            <col width="150px" />
                            <col  />
                            <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold; height:30px;">
                                <td style="text-align:left">
                                    <input type="checkbox" onclick='CheckAll(this)' title='Select All'/>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="center">
                                    <span style="font-size:13px;">List of Fleet Notes Entered By
                                    <%=Session["UserName"].ToString()%></span>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgClose" runat="server" 
                                        ImageUrl="~/Modules/HRD/Images/delete_button.jpg" OnClick="btnCloseFleetPopUp_OnClick" 
                                        style="float :right;" ToolTip="Close this Window." />
                                </td>
                            </tr>
                        </colgroup>
            </table>
            <table cellpadding="3" cellspacing="0" border="0" width="100%" style="border-collapse:collapse">
            <tr>
            <td style="vertical-align:top;" >
                <div style="overflow-y:scroll;overflow-x:hidden; height:320px; border:solid 1px gray;">
                <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;text-align:left;">
                        <colgroup>
                            <col width="30px" />
                            <col  />
                        </colgroup>
                    <asp:Repeater ID="rptFleetNote" runat="server">
                        <ItemTemplate>
                            <tr ID="Tr1" runat="server" style=" background-color:#FFEBC2" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                                <td colspan="2" align="left" valign="top">
                                    <input type="checkbox" onclick='CheckVSL(this)' vsl='<%#Eval("VESSELCODE")%>' title='Select All for this Ship' style=" font-size:13px"/>
                                    <span style="font-size:13px; color:Black; font-weight:bold;">
                                    <%#Eval("VESSELCODE")%></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <a name='<%#Eval("VESSELCODE")%>'>
                                    <asp:CheckBox ID="chkFN" runat="server" />
                                    </a>
                                    <asp:HiddenField ID="hfTID" runat="server" Value='<%#Eval("TID")%>' />
                                </td>
                                <td align="left" valign="top">
                                    <%#Eval("TOPICDETAILS")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                </div>
            </td>            
            </tr>
            <tr>
            <td style="text-align:right;">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
            <asp:Button ID="btnImportFleetNode" runat="server" Text=" Import " OnClick="btnImportFleetNode_OnClick" CssClass="btnstlnew" />
            </td>            
            </tr>
            </table>
          
            </div>
            </center>
        </div>

        <%--------------  Add Fleet Popup  ------------  --%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="divAddFleetPopup" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:155; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:160;top:80px;opacity:1;filter:alpha(opacity=100)">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <colgroup>
                            <col width="150px" />
                            <col  />
                            <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold;height:30px;">
                                <td style="text-align:left">
                                </td>
                                <td>
                                    <asp:Label ID="lblAddEditMsg" runat="server" Text="Add New Fleet Note "></asp:Label>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnCloseAddFleetPopup" runat="server" 
                                        ImageUrl="~/Modules/HRD/Images/delete_button.jpg" OnClick="btnCloseAddFleetPopup_OnClick" 
                                        style="float :right;" ToolTip="Close this Window." />
                                </td>
                            </tr>
                        </colgroup>
            </table>
            <table cellpadding="3" cellspacing="0" border="0" width="100%" style="border-collapse:collapse">
            <tr>
            <td style="vertical-align:top;" colspan="2">
                <table cellpadding="0" cellspacing="0" Width="100%" border="0">
                    <tr>
                        <td style="padding-left:5px; padding-right:10px; padding:2px; text-align:left ">
                        Select Vessel : <asp:DropDownList runat="server" ID="ddlVessel" Width="200px" required="yes"></asp:DropDownList>
                        ( <span style="font-size:10px; font-style:italic; color:#2554C7; font-weight:normal;"> max 1000 Characters </span>)
                        </td>
                        </tr>
                    <tr>
                        <td style="padding-left:5px; padding-right:10px;">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"  Width="100%" Height="270px" ></asp:TextBox>
                        </td>
                    </tr>
                  <%-- <tr>
                        <td style="padding-left:5px; padding-right:10px; padding:2px; text-align:left ">
                            Due Date : <asp:TextBox ID="txtDueDate" runat="server" Width="80px" ></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txtDueDate">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                  </tr>--%>
                </table>
            </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="lblMsgAddFleet" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                    <asp:Button ID="btnSaveNewFleet" runat="server" Text=" Save " OnClick="btnSaveNewFleet_OnClick" CssClass="btnstlnew"/>
                </td>
            </tr>
            </table>
            
            </div>
            </center>
        </div>

        <%--------------  Closure  ------------  --%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvClosure" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:155; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:400px; padding :0px; text-align :center; border :solid 1px black; background : white; z-index:160;top:30px;opacity:1;filter:alpha(opacity=100)">
                Closure Closure 
            </div>
            </center>
        </div>

        <%--------------  Show Notes  ------------  --%>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvViewNotes" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:155; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:160;top:80px;opacity:1;filter:alpha(opacity=100)">
             <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <colgroup>
                            <col width="150px" />
                            <col  />
                            <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold;height:30px;">
                                <td style="text-align:left">
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImageButton2" runat="server" 
                                        ImageUrl="~/Modules/HRD/Images/delete_button.jpg" OnClick="btnCloseViewFleetPopup_OnClick" 
                                        style="float :right;" ToolTip="Close this Window." />
                                </td>
                            </tr>
                        </colgroup>
            </table>
            <asp:TextBox ID="txtViewComments" runat="server" TextMode="MultiLine"  Width="100%" Height="354px" ></asp:TextBox>
            </div>
            </center>
        </div>

      

        
        </ContentTemplate>
        </asp:UpdatePanel>

       
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
    </form>
</body>
</html>

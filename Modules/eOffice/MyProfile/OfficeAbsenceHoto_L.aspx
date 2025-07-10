<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHoto_L.aspx.cs" Inherits="Emtm_OfficeAbsenceHoto_L" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
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
            var X= document.getElementsByName(Vsl);  
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

    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:250px;">
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%" >
            <tr>
                <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold; text-align: center; color: White">
                    &nbsp;<asp:Label ID="lblTitle" Font-Size="14px" runat="server" Text="Handover / TakeOver "></asp:Label>
                </td>
            </tr>
        </table> 
        <table cellpadding="4" cellspacing="0" width="100%" border="1" style="border-collapse:collapse;" rules="all">
        <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Purpose :
            </td>
            <td><asp:Label ID="lblPurpose" runat="server"></asp:Label>
            </td>
            <td style="text-align: right; ">Planned Period :</td>
            <td style="text-align: left; "><asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                    
            </td>
        </tr>
        
      
        <tr>
            <td style="text-align: right;"> Handover On: </td>
            <td>
                <asp:Label runat="server" id="lblNotifiedon"></asp:Label>
            </td>
            <td style="text-align: right;"> Takeover On : </td>
            <td>
                <asp:Label runat="server" id="lblTakeOverOn"></asp:Label>
            </td>
        </tr>
        </table>

        

        <br />
        <div style="text-align:left;">
            <asp:Button runat="server" Text="Handover" ID="btnCash" CssClass="btn11sel" />
            <%--<asp:Button runat="server" Text="TakeOver" ID="btnExp" onclick="btnTakeover_Click"  CssClass="btn11" />--%>
        </div>
        <div style="text-align:left; background-color:#C2E0FF; height:400px; border:solid 1px black;border-top:solid 0px black; padding:10px 5px 5px 5px;">
            <div style="text-align:left; background-color: #C2E0FF; height:395px;">
            <asp:Panel runat="server" ID="pnlHandover" Visible="true" style="padding:5px;">
                    <div style="background-color:White; padding-bottom:3px;">
                        <asp:Button ID="btnFleetImportPopup" runat="server" Text="Import Fleet Notes" OnClick="btnOpenFleetNotePopup_OnClick" CssClass="btnstlnew"/>            
                        <asp:Button ID="btnAddFleetPopup" runat="server" Text="Add Notes" OnClick="btnAddFleetPopup_OnClick" CssClass="btnstlnew" CommandArgument="H"/>            
                    </div>
                   <div style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 29px; text-align: center;">
                   <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="20px" />
                        <col width="20px" />
                        <col width="20px" />
                        <col  />
                        <%--<col width="100px" />--%>
                        <col width="120px" /> 
                        <tr style=" background-color:#5C8AE6; height:25px; color:White; font-weight:bold;" >
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Handover List</td>
                            <%--<td>Due Date</td>--%>
                            <td>Closure</td>
                        </tr>
                   </table>
                   </div>
                   <div runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 280px; text-align: center; background-color:White;">
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
            </asp:Panel>
            <%-- <asp:Panel runat="server" ID="pnlTakeover" Visible="false" style="padding:5px;">
            <div style="background-color:White; padding-bottom:3px;">
                    <asp:Button ID="btnAddTakeover" runat="server" Text="Add Notes" OnClick="btnAddFleetPopup_OnClick" CssClass="btnstlnew" CommandArgument="T"/>
                </div>
            <div id="Div2" runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;text-align: center;">
                      <table cellpadding="2" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="20px" />
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" /> 
                        <tr style=" background-color:#5C8AE6; height:25px; color:White; font-weight:bold;" >
                            <td></td>
                            <td>Handover List</td>
                            <td>Due Date</td>
                            <td>Closed On</td>
                            <td>Status</td>
                        </tr>
                   </table>
                </div>
            <div id="Div1" runat="server" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 340px; text-align: center;">
                    <table cellpadding="2" cellspacing="0" border="1" width="100%" rules="rows" style="border-collapse:collapse;border:none;text-align:left;">
                        <col width="20px" />
                        <col  />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="100px" />
                        <asp:Repeater ID="rptTakeover" runat="server">
                              <ItemTemplate>
                                <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>'>
                                    <td colspan="5" style="background-color:#FFEBCC">
                                       <img src="../../Images/arrow_blue.gif" style="float:left; margin-top:3px;" /> <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="text-align:center">
                                        <asp:ImageButton ID="btnEditFleetNotes" runat="server" OnClick="btnEditFleetNotes_OnClick" ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" CommandArgument='<%#Eval("TID")%>'  />
                                    </td>
                                    <td> &nbsp; <%#Eval("TOPICDETAILS")%></td>
                                    <td style="text-align:center"> &nbsp; <%#Common.ToDateString(Eval("DueDate"))%></td>
                                    <td style="text-align:center"> &nbsp; <%# Common.ToDateString( Eval("ClosedOn"))%></td>
                                    <td style="text-align:center">
                                        <asp:LinkButton ID="lnkTakeOverClosure" runat="server" Text=" Close Now " OnClick="lnkTakeOverClosure_OnClick" OnClientClick="return confirm('Are you sure to close ?');"  CommandArgument='<%#Eval("TID")%>' Visible='<%# (Eval("ClosedBy").ToString()=="") %>' CssClass="csslnkclose"></asp:LinkButton>
                                        <asp:Label ID="lblClosed" runat="server"  Visible='<%# (Eval("ClosedBy").ToString()!="") %>' Text=" Closed"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
            <hr />
            </asp:Panel>--%>
            
            <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;background-color:#C2E0FF; border-collapse:collapse; border-color:#C2E0FF;" rules="all">
            <tr>
            <td style="text-align: right;"> Handover To: </td>
            <td>
                <asp:DropDownList ID="ddlHandover" runat="server" data='yes'></asp:DropDownList>
                <asp:Label runat="server" id="lblHAccrejOn"></asp:Label>
            </td>
            <td style="text-align: right;"> Backup Handover To: </td>
            <td>
                <asp:DropDownList ID="ddlBackup" runat="server" ></asp:DropDownList>
                <asp:Label runat="server" id="lblBAccrejOn"></asp:Label>
            </td>           
        </tr>
            <tr>
        <td colspan="4" style="text-align:right">&nbsp;
            <b> &nbsp; <asp:Label ID="lblUMsg" ForeColor="Red" runat="server" Font-Size="Large"  ></asp:Label></b>
            <asp:Button ID="btnSave" Text="Save" CssClass="btnstlnew" runat="server" onclick="btnSave_Click" Width="80px" />
            <asp:Button ID="btnNotify" Text="Notify" CssClass="btnstlnew" runat="server" onclick="btnNotify_Click"  Width="120px" OnClientClick="return PostanddisableMe(this);"/>
        </td>
        </tr>
            </table>
            </div>
        </div>
        <br />
        <div style="text-align:left;">
            <asp:Button runat="server" Text="TakeOver" ID="btnExp" CssClass="btn11sel_1" />
        </div>
         <div style="text-align:left; background-color:#FFEBE0; height:90px; border:solid 1px black;border-top:solid 0px black; padding:10px 5px 5px 5px;">
            <div style="text-align:right; background-color: #FFEBE0; padding:5px;">
            <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse; border-color:#FFEBE0" rules="all">
            <%--<tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Travel Start Date :
            </td>
            <td style="text-align:left"><asp:TextBox ID="txtActLeaveFrom" runat="server" MaxLength="11" Width="90px" data='yes'
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
            <td style="text-align: right; ">Travel End Date : </td>
            <td style="text-align: left;">
                    <asp:TextBox ID="txtEndDt" runat="server" MaxLength="11" Width="90px" data='yes'></asp:TextBox>
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
            </tr>--%>
            <tr>
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Take Over Date :
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="txtTakeOverDate" runat="server" MaxLength="11" Width="90px" data='yes'></asp:TextBox>
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
                    <asp:Button ID="btnSave1" Text="Save" CssClass="btnstlnew" runat="server" onclick="btnSave1_Click" Width="80px" />
                    <asp:Button ID="btnNotify1" Text="Notify" CssClass="btnstlnew" runat="server" onclick="btnNotify1_Click"  Width="120px" OnClientClick="return PostanddisableMe(this);"/>
                    
            </td>
            </tr>
            </table>
            </div>
        </div>
        
        
        

        <%--------------  Import Fleet Popup  ------------  --%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvImportFleet" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:500px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:150;top:80px;opacity:1;filter:alpha(opacity=100)">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="150px" />  <col  />
                        <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold; height:30px;">
                            <td  style="text-align:left">
                                <input type="checkbox" onclick='CheckAll(this)' title='Select All'/> &nbsp;&nbsp;&nbsp;
                            </td> 
                                <td align="center">
                                   <span style="font-size:13px;"> List of Fleet Notes Entered By <%=Session["UserName"].ToString()%></span>
                                </td>
                            <td>
                            <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/delete_button.jpg" ID="imgClose" OnClick="btnCloseFleetPopUp_OnClick"  style="float :right;" ToolTip="Close this Window."/>
                            
                            </td>
                        </tr>
            </table>
            <table cellpadding="3" cellspacing="0" border="0" width="100%" style="border-collapse:collapse">
            <tr>
            <td style="vertical-align:top;" >
                <div style="overflow-y:scroll;overflow-x:hidden; height:420px; border:solid 1px gray;">
                <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;text-align:left;">
                        <col width="30px" /> <col  />
                        <asp:Repeater ID="rptFleetNote" runat="server">
                            <ItemTemplate>
                                <tr id="Tr1" runat="server" visible='<%#CompareVSl(Eval("VESSELCODE"))%>' style=" background-color:#FFEBC2">
                                    <td colspan="2" >
                                    <input type="checkbox" onclick='CheckVSL(this)' vsl='<%#Eval("VESSELCODE")%>' title='Select All for this Ship' style=" font-size:13px"/>
                                    <span style="font-size:13px; color:Black; font-weight:bold;"><%#Eval("VESSELCODE")%></span>
                                    </td>
                                </tr>
                                <tr >
                                    <td align="right" valign="top"> 
                                        <a name='<%#Eval("VESSELCODE")%>'><asp:CheckBox ID="chkFN" runat="server" /></a>
                                        <asp:HiddenField ID="hfTID" runat="server" Value='<%#Eval("TID")%>'/>
                                    </td>
                                    <td><%#Eval("TOPICDETAILS")%> </td>
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
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:150;top:80px;opacity:1;filter:alpha(opacity=100)">
            <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="150px" />  <col  />
                        <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold;height:30px;">
                            <td  style="text-align:left">
                                
                            </td> 
                            <td>
                                <asp:Label runat="server" ID="lblAddEditMsg" Text="Add New Fleet Note "></asp:Label>
                            </td>
                            <td>
                            <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/delete_button.jpg" ID="btnCloseAddFleetPopup" OnClick="btnCloseAddFleetPopup_OnClick"  style="float :right;" ToolTip="Close this Window."/>
                            
                            </td>
                        </tr>
            </table>
            <table cellpadding="3" cellspacing="0" border="0" width="100%" style="border-collapse:collapse">
            <tr>
            <td style="vertical-align:top;" colspan="2">
                <table cellpadding="0" cellspacing="0" Width="100%" border="0">
                    <tr>
                        <td style="padding-left:5px; padding-right:10px; padding:2px; text-align:left ">
                        Select Vessel : <asp:DropDownList runat="server" ID="ddlVessel" Width="200px" data="yes"></asp:DropDownList>
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
                    <asp:Button ID="btnSaveNewFleet" runat="server" Text=" Save " OnClick="btnSaveNewFleet_OnClick" CssClass="btnstlnew" CausesValidation="false"/>
                </td>
            </tr>
            </table>
            
            </div>
            </center>
        </div>

        <%--------------  Closure  ------------  --%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvClosure" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:400px; padding :0px; text-align :center; border :solid 1px black; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                Closure Closure 
            </div>
            </center>
        </div>

        <%--------------  Show Notes  ------------  --%>

        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvViewNotes" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:400px; padding :0px; text-align :center; border :solid 4px #C2E0FF; background : white; z-index:150;top:80px;opacity:1;filter:alpha(opacity=100)">
             <table cellpadding="0" cellspacing="0" border="0" width="100%" rules="all" style="border-collapse:collapse;border:none;">
                        <col width="150px" />  <col  />
                        <tr style="text-align:center; background-color:#5C8AE6; padding:3px; color:White; font-size:15px; font-weight:bold;height:30px;">
                            <td  style="text-align:left">
                                
                            </td> 
                            <td>
                                
                            </td>
                            <td>
                            <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/delete_button.jpg" ID="ImageButton2" OnClick="btnCloseViewFleetPopup_OnClick"  style="float :right;" ToolTip="Close this Window."/>
                            </td>
                        </tr>
            </table>
            <asp:TextBox ID="txtViewComments" runat="server" TextMode="MultiLine"  Width="100%" Height="354px" ></asp:TextBox>
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

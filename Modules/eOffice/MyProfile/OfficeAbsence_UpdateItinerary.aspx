<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsence_UpdateItinerary.aspx.cs" Inherits="emtm_MyProfile_Emtm_OfficeAbsence_UpdateItinerary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
   </script>
    <style type="text/css">
    .btn11sel
    {
        font-size:14px;
        background-color:#99CCFF;
        border-top:solid 1px black;
        border-right:solid 1px black;
        border-left:solid 1px black;
        border-bottom:solid 1px #99CCFF;
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
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Update Itinerary</div>
        <%--<table cellpadding="2" cellspacing="1" width="100%" border="1">
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
            <td style="text-align: right; padding-top: 5px; padding-bottom: 5px;">
                Travel Start Date :
            </td>
            <td><asp:TextBox ID="txtActLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes'
                        ValidationGroup="planleave"></asp:TextBox>
                    <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                        OnClientClick="return false;" />
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
                <td style="text-align: right; ">
                Travel End Date : </td>
            <td style="text-align: left;"><asp:TextBox ID="txtEndDt" runat="server" MaxLength="11" Width="90px" ></asp:TextBox>
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
        </table>--%>
        <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
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
                <tr>
                    <td style='text-align:left'>
                     <span style="color:Green; font-size:large;">Departure Date :</span>&nbsp; <asp:TextBox ID="txtActLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes'
                        ValidationGroup="planleave"></asp:TextBox>
                    <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                        OnClientClick="return false;" />
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
                    <td colspan="2" style='text-align:right'>
                    <span style="color:Purple; font-size:large;">Arrival Date ( In office ) :</span>&nbsp; <asp:TextBox ID="txtEndDt" runat="server" MaxLength="11" Width="90px" ></asp:TextBox>
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
        </table>
        <div style="width:100%;">
        <div style="float:left; " ><asp:Label ID="lblUMsg" ForeColor="Red" runat="server"></asp:Label></div>
        <div style="float:right;">
             <asp:Button ID="btnSave"  Text="Save" CssClass="btn" runat="server" onclick="btnSave_Click" />
             <asp:Button ID="btnPrint" Text="Print" CssClass="btn" runat="server" onclick="btnPrint_Click" />
             <asp:Button ID="btnClose" Text="Close" CssClass="btn" style="background-color:Red" runat="server" OnClientClick="window.parent.HideFrame();"  /> 
        </div>
        <div style="clear:both;"></div>
        </div>
        
        </ContentTemplate>
        </asp:UpdatePanel>
        

    
    
    </div>
    </form>
</body>
</html>

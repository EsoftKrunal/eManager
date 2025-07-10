<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestHourReport_N.aspx.cs" Inherits="RestHourReport_N" Async="true" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
   <%-- <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <style type="text/css" >
    body
    {
        font-family:Calibri;
        font-size:14px;
    }
    .table-border-header tr td
    {
        border:solid 1px #d3d3d3;
        background-color:#333;
        color:White;
        padding:4px;
    }
    .table-border-data tr td
    {
        border:solid 1px #d3d3d3;
        color:#333;
        padding:4px;
    }
    .ncgreen
    {
        background-color:yellow;
        color:black;
    }
    .ncred
    {
        background-color:#ff8c66;
        color:White;
    }
    </style>
    <script type="text/javascript">
        function PostForm(ctl) {

            $(".myrow").css('background-color', '');
            $(ctl).parent().parent().css('background-color', 'yellow');

            $("#hdfMnth").val($(ctl).attr('month'));
            $("#hdfVsl").val($(ctl).attr('vesselid'));
            $("#hdfVslCode").val($(ctl).attr('vesselCode'));
            $("#btnpost").click();
        }
        function PrintForm(ctl) {
            var month = $(ctl).attr('month');
            var vesselid = $(ctl).attr('vesselid');

            
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
    <div>
    <div style="padding:7px; " class="text headerband">Rest Hour Management</div>
     <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
     <tr>
        <td style="text-align:left; width:100px;">Select Fleet :</td>
        <td style="text-align:left; width:220px;"><asp:DropDownList runat="server" ID="ddlFleet" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="BindGrid"></asp:DropDownList></td>
        <td style="text-align:left; width:100px;">Select Year :</td>
        <td style="text-align:left; width:100px;"><asp:DropDownList runat="server" ID="ddlYear" Width="80px"  AutoPostBack="true" OnSelectedIndexChanged="BindGrid"></asp:DropDownList></td>
        <td style="text-align:left; width:100px;">Select Month :</td>
        <td style="text-align:left; width:300px;"><asp:DropDownList runat="server" ID="ddlMonth"  AutoPostBack="true" OnSelectedIndexChanged="BindGrid"></asp:DropDownList></td>
        <td>&nbsp;</td>
     </tr>
     </table>
    </div>
    <div>
        <div style="overflow-y:scroll; overflow-x:hidden; height:28px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;" >
         <tr class= "headerstylegrid">
            <td style="text-align:left">Vessel Name</td>
            <td style="width:100px;text-align:center">Month</td>
            <td style="width:200px;text-align:center">Any 24 Hour ( No of Crew )</td>
            <td style="width:250px;text-align:center"> &gt;=3 Days NC ( No of Crew )</td>
            <td style="width:200px;text-align:center">Replied By</td>
            <td style="width:100px;text-align:center">Replied On</td>
            <td style="width:50px;text-align:center">eMail</td>
            <td style="width:100px;text-align:center">View</td>
         </tr>
         </table>
         </div>
        <div style="overflow-y:scroll; overflow-x:hidden; height:240px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;" class="table-border-data">
         <asp:Repeater runat="server" ID="rpt_data">
         <ItemTemplate>
          <tr class="">
            <td style="text-align:left"><%#Eval("VesselName")%></td>
            <td style="width:100px;text-align:left"><%#Eval("MOnthName")%></td>
            <td style="width:200px;text-align:left"><%#Eval("NC_CREW_COUNT")%></td>
            <td style="width:250px;text-align:left"><%#Eval("NC_CREW_COUNT_3")%></td>
            <td style="width:200px;text-align:left"><%#Eval("OfficeCommentsBy")%></td>
            <td style="width:100px;text-align:left"><%#Common.ToDateString(Eval("OfficeCommentsOn"))%></td>
            <td style="width:50px;text-align:center">
                <asp:Image runat="server" ImageUrl="~/Modules/HRD/Images/favicon.png" Visible='<%#(Convert.ToString(Eval("MailSent"))=="True")%>' />
            </td>
            <td style="width:100px;text-align:center">
                <img month='<%#Eval("MNTH")%>'  vesselid='<%#Eval("vesselid")%>' vesselCode='<%#Eval("vesselCode")%>' src="../Images/Calendar.gif" style="cursor:pointer" onclick="PostForm(this);" />
                <asp:ImageButton runat="server" month='<%#Eval("MNTH")%>' vesselid='<%#Eval("vesselid")%>' ImageUrl="~/Modules/HRD/Images/print_16.png" OnClick="btnPrintForm_Click" />
                <asp:ImageButton ID="btnSendEmail" runat="server" month='<%#Eval("MNTH")%>' vesselid='<%#Eval("vesselid")%>' ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="btnSendEmail_Click" OnClientClick="return window.confirm('Are you sure to send mail?');" />
            </td>
         </tr>
         </ItemTemplate>
         </asp:Repeater>
          </table>
        </div>
    </div>
    </div>
    <asp:UpdatePanel runat="server" ID="re">
    <ContentTemplate>
    <div style="font-size:11px">
    <div style="padding:0px; background:#99ccff; color:#333; text-align:left; font-size:18px;">
    <table cellpadding="6" width="100%" >
    <tr>
    <td>
        <asp:Label ID="lblVesselName" runat="server"></asp:Label>
        <span style="display:none">
        <asp:Button runat="server" ID="btnpost" Text="post" OnClick="btnPost_Click"/>
        <asp:TextBox runat="server" ID="hdfMnth" value=""/>
        <asp:TextBox runat="server" ID="hdfVsl" value=""/>
            <asp:TextBox runat="server" ID="hdfVslCode" value=""/>
        </span>
    </td>
    <td style="color:White; width:200px; text-align:center; font-size:13px;"><div class="ncred" style="padding:5px;" >< 10 hrs rest taken</div></td>
    <td style="color:White; width:200px; text-align:center; font-size:13px;"> <div class="ncgreen" style="padding:5px;" > >= 10 hrs rest taken</div> </td>
    <td style="width:150px; text-align:center">
        <asp:Button runat="server" ID="btnAddremarks" Text=" + Add Office Remarks" OnClick="btnAddComments_Click" CssClass="btn"/>
    </td>
    </tr>
    </table>
    </div>
    <div style="overflow-y:scroll; overflow-x:hidden; height:28px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;" class="table-border-header">
         <tr style="height:28px">
            <asp:literal ID="litheader" runat="server"></asp:literal>
         </tr>
        </table>
         </div>
         <div style="overflow-y:scroll; overflow-x:hidden; height:200px;">
         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;" class="table-border-data">
         <tr>
            <asp:literal ID="litdata" runat="server"></asp:literal>
         </table>
         </div>
    </div>

    <!-- Add Remarks -->

    <div>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="dv_AddComments" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:800px; padding :0px; text-align :center;background : white; z-index:150;top:20px; border:solid 3px #4db8ff;">
                <center >
                    <div style="padding:5px; " class="text headerband" ><b><asp:Label  ID="lblVessel1" runat="server" Font-Size="18px"></asp:Label></b></div>
                    <div><b>Please enter your comments below..</b></div>
                    <div style="margin:5px">
                        <asp:TextBox runat="server" ID="txtComments" Width="100%" Rows="20" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div style="padding:5px">
                        <asp:Label ForeColor="Red" ID="lblpopmsg" runat="server"></asp:Label>
                    </div>
                    <div style="padding:10px">
                    <asp:Button ID="btnSaveComments"  Width="80px" runat="server" Text="Save" OnClick="btnSaveComments_Click" CssClass="btn" />
                    <asp:Button ID="btnClose" style=" border:none; padding:3px; " Width="80px" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn"/>
                    
                    </div>
                </center>
            </div>
        </center>
    </div>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;" class="table-border-data">
    <tr>
    <td style="text-align:left; width:120px; vertical-align:top;">Office Comments : </td>
    <td style="text-align:left">
        <asp:Label runat="server" ID="lblOfficeComments" Font-Italic="true"></asp:Label>
    </td>
    </tr>
    <tr>
    <td style="text-align:left; width:120px;">Comments By / On :</td>
    <td style="text-align:left;">
        <asp:Label runat="server" ID="lblOfficeCommentsByOn"></asp:Label>
    </td>
    </tr>
    </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger ControlID="btnSaveComments" />
    </Triggers>
    </asp:UpdatePanel>
    <div>
   
    </div>
    

    </form>
</body>
</html>

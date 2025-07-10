<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_SeasonsGreeting.aspx.cs" Inherits="CRMActivities_CRM_SeasonsGreeting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />    
    <style type="text/css">
.fh1
{ 
    font-weight:bold;
    font-size:14px;
}
.fh2
{ 
    font-size:14px;
}
.fh3
{ 
    font-size:14px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:14px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
.btn_Close
{
    background-color:Red;
    border:solid 1px grey;
    color:White;
    width:100px;   
}
.cls_I
{
    background-color:#80E680;
}
.cls_O
{
    background-color:#FFAD99;
}
.btn_All
{
    background-color:#FF9933;
    border:solid 1px grey;
    font-weight:bold;
    color:White;
    width:100px;
}
</style> 
<script type="text/javascript" language="javascript" >
    function openprintLabel(Hid, OffId) {
        window.open('PrintSGLabels.aspx?HId=' + Hid + '&OfficeId=' + OffId, '_blank', '', '');
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>  
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
        <div style="font-family:Arial;font-size:12px;">            
            <div style="width:100%;padding:5px; text-align:center;font-weight:bold;"> 
            <div class="text headerband" style=" font-size:13px; width:100%; height:25px; vertical-align:middle;  ">Seasons Greeting - ( <asp:Label ID="lblHoliday" runat="server" ></asp:Label> ) </div>
            <div style="float:right; ">
               <asp:Button ID="btnBack" Text="Back" OnClick="btnBack_Click" class="btn_Close" runat="server" />
            </div>
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; ">
            <tr>
                <td style="text-align:right;">Recruiting Office : </td>
                <td style="text-align:left;"><asp:Label ID="lblRecOffice" runat="server" ></asp:Label> <%--<asp:DropDownList ID="ddl_Recr_Office" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList>--%> </td>
                <td style="text-align:right;">Rank : </td>
                <td style="text-align:left;"><asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList></td>
                <td style="text-align:right;">Crew Status : </td>
                <td style="text-align:left;"><asp:DropDownList ID="ddl_CrewStatus_Search" runat="server" CssClass="input_box" Width="165px" ></asp:DropDownList></td>            
            </tr>
            <tr>
            <td colspan="3" style="text-align:left; padding-left:10px;"><asp:Button ID="btnSendCard" OnClick="btnSendCard_Click" Text="Update Card Delivery" Width="160px" runat="server" CssClass="btn"  />&nbsp;&nbsp;<asp:Button ID="btnPrintLabel" OnClick="btnPrintLabel_Click" Text="Print Label" runat="server" CssClass="btn" /></td>            
            <td colspan="3" style="text-align:right; padding-right:10px;"><asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>&nbsp;&nbsp;<asp:Label runat="server" ID="lblRcount1"></asp:Label>&nbsp;&nbsp;<asp:Button ID="btnSearch" OnClick="btnSearch_Click" Text="Search" runat="server" CssClass="btn" />
            
            </td>
            </tr>
            </table>
            </div>
            <div style="height:25px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse; height:25px; background-color:#FFEB99; font-weight:bold; ">
               <colgroup>
                    <col style="width:50px; text-align:center;" />
                    <col style="width:60px; text-align:center;" />
                    <col style="text-align:left;" />
                    <col style="width:70px; text-align:left;" />
                    <col style="width:90px; text-align:center;" />
                    <col style="width:100px; text-align:center;" />
                    <col style="width:100px; text-align:left;" />
                    <col style="width:110px; text-align:center;" />
                    <col style="width:120px; text-align:left;" />
                    <col style="width:190px; text-align:center;" />                    
                    <col style="width:20px;"/>
                </colgroup>
            <tr class= "headerstylegrid">
            <td ><asp:CheckBox ID="chkCheckAll_Crew" ToolTip="Select All" AutoPostBack="true"  OnCheckedChanged="chkCheckAll_CheckedChanged" runat="server"  /></td>            
            <td >Crew#</td>
            <td>&nbsp;Crew Name</td>
            <td >Rank</td>
            <td >DOB</td>
            <td >Crew Status</td>
            <td >Rect. Office</td>
            <td >Curr./ Last VSL</td>
            <td >City</td>
            <td >Sent By/ On</td>
            <td >&nbsp;</td>
            </tr>
            </table>
            </div>
            <br />
           
            <div style="height:400px; text-align:center; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
            <table cellpadding="2" cellspacing="1" width="100%" border="0" style="border-collapse:collapse;" >
            <colgroup>
                    <col style="width:50px; text-align:center;" />
                    <col style="width:60px; text-align:center;" />
                    <col style="text-align:left;" />
                    <col style="width:70px; text-align:left;" />
                    <col style="width:90px; text-align:center;" />                    
                    <col style="width:100px; text-align:left;" />
                    <col style="width:100px; text-align:left;" />
                    <col style="width:110px; text-align:center;" />
                    <col style="width:120px; text-align:left;" />
                    <col style="width:190px; text-align:left;" />                    
                    <col style="width:20px;"/>
                </colgroup>
            <asp:Repeater runat="server" ID="rpt_Crew">
            <ItemTemplate>
            <tr >
                <td ><asp:CheckBox ID="chkSent" CrewId='<%#Eval("CrewId")%>'  runat="server" Visible='<%#Eval("CardSent").ToString() == "" %>' /><img id="Img3" alt="" src="~/Modules/HRD/Images/favicon.png" runat="server" title='<%# (Eval("CardSent").ToString() == "" ? "" : "Sent On : " + Common.ToDateString(Eval("CardSent"))) %>' visible='<%#Eval("CardSent").ToString() != "" %>' /> </td>            
                <td ><%#Eval("CrewNumber")%></td>
                <td align="left">&nbsp;<%#Eval("CrewName")%></td>
                <td align="left"><%#Eval("RankCode")%></td>
                <td ><%#Common.ToDateString(Eval("DateOfBirth"))%></td>                
                <td align="left">&nbsp;<%#Eval("CrewStatusName")%></td>
                <td align="left">&nbsp;<%#Eval("RecruitingOfficeName")%></td> 
                <td >&nbsp;<%#Eval("Curr_Last_Vsl")%></td>
                <td align="left">&nbsp;<%#Eval("City")%></td>
                <td >&nbsp;<%# (Eval("UpdatedBy").ToString() == "" ? "" : (Eval("UpdatedBy").ToString() + "/ " + Common.ToDateString(Eval("CardSent"))))%></td>
                <td >&nbsp;</td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr style="background-color:#CCE6FF;" >
                <td ><asp:CheckBox ID="chkSent" CrewId='<%#Eval("CrewId")%>'  runat="server" Visible='<%#Eval("CardSent").ToString() == "" %>' /><img id="Img3" alt="" src="~/Modules/HRD/Images/favicon.png" runat="server" title='<%# (Eval("CardSent").ToString() == "" ? "" : "Sent On : " + Common.ToDateString(Eval("CardSent"))) %>' visible='<%#Eval("CardSent").ToString() != "" %>' /> </td>            
                <td ><%#Eval("CrewNumber")%></td>
                <td align="left" >&nbsp;<%#Eval("CrewName")%></td>
                <td align="left"><%#Eval("RankCode")%></td>
                <td ><%#Common.ToDateString(Eval("DateOfBirth"))%></td>
                <td align="left">&nbsp;<%#Eval("CrewStatusName")%></td>
                <td align="left">&nbsp;<%#Eval("RecruitingOfficeName")%></td> 
                <td >&nbsp;<%#Eval("Curr_Last_Vsl")%></td>
                <td align="left">&nbsp;<%#Eval("City")%></td>
                <td >&nbsp;<%# (Eval("UpdatedBy").ToString() == "" ? "" : (Eval("UpdatedBy").ToString() + "/ " + Common.ToDateString(Eval("CardSent"))))%></td>
                <td >&nbsp;</td>
            </tr>
            </AlternatingItemTemplate>
            </asp:Repeater>
            </table>
            </div>
                       
            <div style="margin-top:5px; height:25px; text-align:right; ">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
            <ProgressTemplate>
            <center>
            <div style="float:left;">
                <img src="../Images/loading.gif"/> Loading .... Please Wait
            </div>
            </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            </div>
        </div>
        <%-- div for sending Cards --%>
        <div style="position:absolute;top:0px;left:0px; height :200px; width:100%; " id="dv_SendCard" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:350px;  height:150px;padding :5px; text-align :center;background : white; z-index:150;top:180px; border:solid 0px black;font-family:Arial;font-size:12px;">
            <center >
             <div class="text headerband" style="padding:6px; font-size:14px; "><strong>Update Card Delivery</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:150px;">
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 130px; text-align: center; border-collapse:collapse; width:100%;">
                     <tr>                         
                          <td style="text-align: right; width:150px;">
                             <b>Card Delivery Date :</b>&nbsp;
                          </td>
                          <td style="text-align:left;">   
                             <asp:TextBox ID="txtSentDate" Width="100px" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="BD" runat="server" ControlToValidate="txtSentDate" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSentDate"></ajaxToolkit:CalendarExtender>  
                          </td>
                      </tr>                     
                        <tr>
                          <td colspan="2" style=" text-align:center;">
                              <asp:Button ID="btnSend_BDCard" runat="server" Text="Save" ValidationGroup="BD" Width="80px" OnClick="btnSend_BDCard_Click" style="  border:none; padding:4px;" CssClass="btn"/>                            
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn"/>
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
        </div>
    </center>
    </div>

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

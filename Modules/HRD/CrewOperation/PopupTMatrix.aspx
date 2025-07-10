<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupTMatrix.aspx.cs" Inherits="CrewOperation_PopupTMatrix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Matrix</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
           <style type="text/css">
.hd
{
	background-color : #c2c2c2;
}
a img
{
border:none;	
}
</style>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    var ScreenW=window.screen.availWidth-50;
    var ScreenH=window.screen.availHeight-50; 
    var selDiv;
    function showUpdate(ctl,pkid,tid,dd,mode,TRid)
    {
        var maxpos=0;
        var ctl_width=700;
        var ctl_height=220;
        
        var left=event.clientX;
        var top=event.clientY;
        
        if(left<ScreenW/2)
        {
            left=left+(ctl_width/2);
        }
        else
        {
            left=left-(ctl_width/2);
        }
        maxpos=left+ctl_width;
        if(maxpos>=ScreenW)
        {
        left=left-(maxpos-ScreenW);
        }
        if(left<=0)
        {
        left=0;
        }
        if(top<ScreenH/2)
        {
            top=top+30;
        }
        else
        {
            top=top-30-ctl_height;
        }
        
        document.getElementById("hfdPKId").value=pkid;
        document.getElementById("hfdTId").value=tid;
        document.getElementById("hfdDD").value=dd;
        
        //document.getElementById("dvInf1").innerHTML=ctl.getAttribute('data');
        document.getElementById("txtTrId").value=TRid;
        document.getElementById("btnLoadLit").click();
        document.getElementById("dvUP").style.display=((mode=="P")?"":"none");
        
        dvSch.style.left =left+'px';
        dvSch.style.top =top+'px';
        
        $("#dvSch").slideDown();
        document.getElementById("txt_FromDate").focus();
        selDiv="#dvSch";
    }
    
    function hideUpdate(val)
    {
        if(event.keyCode==27 )
            $("#dvSch").slideUp();
    }
    function hideLast()
    {
     if(event.keyCode==27 )
     {
        $(selDiv).slideUp();
     }
    }
</script>
</head>
<body onkeydown='hideLast();'>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
    <table style="border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">  
    <tr class="text headerband">
        <td style="border:#4371a5 1px solid;  height:25px;">
    <span style=" font-size:15px;">Training Matrix - <asp:Label runat="server" ID="lblCrew"></asp:Label></span>
    </td>
    </tr>
    </table>
    
    <asp:Literal runat="server" ID="litTraining"></asp:Literal>
    <table style="border-collapse:collapse;" cellpadding="3" cellspacing="0" border="1" width="100%">  
    <tr>
    <td style=" background-color:green;color:White;">Normal</td>
    <td style=" background-color:red;color:White;">Overdue</td>
    <td style=" background-color:yellow">Planned</td>
    <td>Done</td>
    <td>
        <asp:Button runat="server" ID="btnExportToPDF" Text="Print" style="float:right;padding-right:7px;margin:3px;" CssClass="btn" OnClick="Export_PDF" Width="200px" CausesValidation="false"  />
        <asp:Button runat="server" ID="btnExcel" Text="Download Training Requirement" CausesValidation="false" style="float:right;padding-right:7px;margin:3px;" CssClass="btn" Width="200px" OnClick="TrainingExcel_Click" />
        <a id="aDownLoadFile" runat="server" visible="false" style="float:right;" > Download(Right click and select Save target as)</a>
    </td>
    </tr>
    </table>
    </div>
    
     <!-- DIV FOR SHOW UPDATE TRAINING -->
     
                 <div id="dvSch" onkeydown='hideUpdate();' style="border :solid 1px gray; border-bottom:solid 4px gray;border-right:solid 4px gray; width :700px; height :220px; padding :10px; position :absolute; top:400px; left:200px; background-color:#FFE4B5; display :none; text-align :center">
                <span onclick='$(selDiv).slideUp();' style="cursor:pointer;float:right;"><img src="../Images/critical.gif" title="Close" /></span>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                <span style="font-size:11px">
                <asp:Button runat="server" ID="btnLoadLit" OnClick="Load_Lit" CausesValidation="false" style="display:none" />
                <asp:TextBox ID="txtTrId" runat="server" style="display:none"></asp:TextBox>
                    <asp:Literal runat="server" ID="litSummary"></asp:Literal> 
                </span>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div id="dvUP">
                <hr />
                <span style="font-size:12px">| Update Training |</span> 
                <hr />
                <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                <table style="border-collapse:collapse" border="0" cellpadding="3" cellspacing="0">
                           <tr>
                               <td style="text-align:right">From Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="rf1" ControlToValidate="txt_FromDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                               <td style="text-align:right">To Date :&nbsp;</td>
                               <td style="text-align: left">
                                   <asp:TextBox ID="txt_ToDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="75px"></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txt_ToDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                   <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                           <td style="text-align:right">Institute :</td>
                               <td style="text-align: left"><asp:DropDownList ID="ddl_TrainingReq_Training" runat="server" CssClass="required_box" TabIndex="2" Width="150px"></asp:DropDownList>
                               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddl_TrainingReq_Training" ErrorMessage="*"></asp:RequiredFieldValidator>
                               
                               </td>
                               <td style="text-align: left">&nbsp;</td>
                               <td><asp:Button ID="btn_Save_PlanTraining" runat="server" CssClass="btn" Text=" Update " Width="60px" TabIndex="9" OnClick="btn_UpdateTraining_Click" /></td>
                           </tr>
                        </table>      
                <asp:HiddenField runat="server" ID="hfdPKId" /><asp:HiddenField runat="server" ID="hfdTId" /><asp:HiddenField runat="server" ID="hfdDD" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_FromDate">
                       </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                           PopupButtonID="ImageButton4" PopupPosition="TopLeft" TargetControlID="txt_ToDate">
                       </ajaxToolkit:CalendarExtender>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btn_Save_PlanTraining" />
                </Triggers>
                </asp:UpdatePanel>
                </div>
                </div>
    </form>
</body>
</html>

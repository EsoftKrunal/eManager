<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="VIQHome.aspx.cs" Inherits="Vetting_VIQHome" Title="Untitled Page"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    font-size:17px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:17px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
</style>
<script type="text/javascript">
    
    function ShowDue(ctl) {
        $(".dv_ModalBox").show();
        $(".dv_ModalBox").height($("body").height());
        var bodystp = $("body").scrollTop();
        if (bodystp == 0) {
            bodystp = $(document).scrollTop();
        }
        $("#dv_Due").slideDown();
        $("#dv_Due").css('top', bodystp + 50 + 'px');
        $("#ctl00_ContentPlaceHolder1_btnshowdue").focus();
        $("#ctl00_ContentPlaceHolder1_btnshowdue").click();
    }
    function HideDue() {
        $(".dv_ModalBox").hide();
        $("#dv_Due").hide();
    }

    //-------------------------------------


    function ShowPlanned(ctl) {
        $(".dv_ModalBox").show();
        $(".dv_ModalBox").height($("body").height());
        var bodystp = $("body").scrollTop();
        if (bodystp == 0) {
            bodystp = $(document).scrollTop();
        }
        $("#dv_Plan").slideDown();
        $("#dv_Plan").css('top', bodystp + 50 + 'px');
        $("#ctl00_ContentPlaceHolder1_btnshowPlanned").focus();
        $("#ctl00_ContentPlaceHolder1_btnshowPlanned").click();
    }
    function HidePlanned() {
        $(".dv_ModalBox").hide();
        $("#dv_Plan").hide();
    }

    //-------------------------------------

    function ShowResponse(ctl) {
        $(".dv_ModalBox").show();
        $(".dv_ModalBox").height($("body").height());
        var bodystp = $("body").scrollTop();
        if (bodystp == 0) {
            bodystp = $(document).scrollTop();
        }
        $("#dv_Resp").slideDown();
        $("#dv_Resp").css('top', bodystp + 50 + 'px');
        $("#ctl00_ContentPlaceHolder1_btnshowResponse").focus();
        $("#ctl00_ContentPlaceHolder1_btnshowResponse").click();
    }
    function HideResponse() {
        $(".dv_ModalBox").hide();
        $("#dv_Resp").hide();
    }

    //-------------------------------------

    function ShowBonus(ctl) {
        $(".dv_ModalBox").show();
        $(".dv_ModalBox").height($("body").height());
        var bodystp = $("body").scrollTop();
        if (bodystp == 0) {
            bodystp = $(document).scrollTop();
        }
        $("#dv_Bonus").slideDown();
        $("#dv_Bonus").css('top', bodystp + 50 + 'px');
        $("#ctl00_ContentPlaceHolder1_btn_ShowBonus").focus();
        $("#ctl00_ContentPlaceHolder1_btn_ShowBonus").click();
    }
    function HideBonus() {
        $(".dv_ModalBox").hide();
        $("#dv_Bonus").hide();
    }

    //-------------------------------------

    function ShowDone(ctl) {
        $(".dv_ModalBox").show();
        $(".dv_ModalBox").height($("body").height());
        var bodystp = $("body").scrollTop();
        if (bodystp == 0) {
            bodystp = $(document).scrollTop();
        }
        $("#dv_Done").slideDown();
        $("#dv_Done").css('top', bodystp + 50 + 'px');
        $("#ctl00_ContentPlaceHolder1_btn_ShowDone").focus();
        $("#ctl00_ContentPlaceHolder1_btn_ShowDone").click();
    }
    function HideDone() {
        $(".dv_ModalBox").hide();
        $("#dv_Done").hide();
    }

    
    
</script>
<center>
<table cellpadding="10" cellspacing="0" width="100%">
          <tr>
            <td>
            <td style="text-align:center; vertical-align:top; font-size:17px;">
                <table cellpadding="15" cellspacing="15" width="100%" border="0">
                 <tr>
                <td style="text-align:left; background-color:#80E680;" class="fh2">
                    Vetting Inspections Due in Next 60 Days :
                </td>
                <td style="text-align:center; background-color:#80E680;" class="fh2">
                   <asp:Label runat="server" ID="lbl_InsDue" onclick="ShowDue();" style="cursor:pointer; text-decoration:underline;"></asp:Label> 
                </td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#FFE6E6;" class="fh2">
                    Vetting Inspections Planned for next 30 Days :
                </td>
                <td style="text-align:center; background-color:#FFE6E6;" class="fh2">
                    <asp:Label runat="server" ID="lbl_InsPlanned" onclick="ShowPlanned();" style="cursor:pointer; text-decoration:underline;"></asp:Label> 
                </td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#E0FFE0;" class="fh2">
                    Vetting Response to be Uploaded :</td>
                <td style="text-align:center; background-color:#E0FFE0;" class="fh2">
                    <asp:Label runat="server" ID="lbl_InsResp" onclick="ShowResponse();" style="cursor:pointer; text-decoration:underline;"></asp:Label> </td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#C2E0FF;" class="fh2">
                    Vetting Bonus to be Cleared :</td>
                <td style="text-align:center; background-color:#C2E0FF;" class="fh2">
                    <asp:Label runat="server" ID="lbl_InsBonus" onclick="ShowBonus();" style="cursor:pointer; text-decoration:underline;"></asp:Label> </td>
                </tr>
                <tr>
                <td style="text-align:left; background-color:#FFAD99;" class="fh2">
                    Vetting followup Item not Closed :</td>
                <td style="text-align:center; background-color:#FFAD99;" class="fh2">
                   <a href='FollowUPNoClosed.aspx'><asp:Label runat="server" ID="lbl_InsNotCleared" style="cursor:pointer; text-decoration:underline;"></asp:Label></a>
                </tr>
                <tr>
                    <td style="text-align:left; background-color:#D1B2FF;" class="fh2">
                        Vetting Inspections Done in Last 30 Days :
                    </td>
                    <td style="text-align:center; background-color:#D1B2FF;" class="fh2">
                        <asp:Label runat="server" ID="lbl_InsDone" onclick="ShowDone();" style="cursor:pointer; text-decoration:underline;"></asp:Label>
                    </td>
                </tr>
                </table>
            </td>
            <td style="padding:10px">
            <div style="background-color:#FFD699; width:5px ; height:450px;">
            </div>
            </td>
            <td style="text-align:center; vertical-align:top; font-size:17px; width:300px;">
                <table cellpadding="5" cellspacing="5" width="100%"  border="0">
                <tr runat="server" id="tr_VIQ">
                <td style="text-align:left; width:10px;" ><img src="../Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                   <a href="VIQ.aspx">VIQ</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VPR">
                <td style="text-align:left; " ><img src="../Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <a href="VIQPreparation.aspx">Vessel Preparation</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VP">
                <td style="text-align:left; " ><img src="../Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <A href="VetttingPlannerReport.aspx">Vetting Planner</a>
                </td>
                </tr>
                <tr runat="server" id="tr_VR">
                <td style="text-align:left" ><img src="../Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    <A href="VettingReports.aspx">Vetting Reports</a>
                    </td>
                </tr>
                <tr runat="server" id="tr_VA">
                <td style="text-align:left" ><img src="../Images/right-arrow_12.png" /></td>
                <td style="text-align:left" class="fh3">
                    Vetting Alerts</td>
                </tr>
                </table>
            </td>
            </td>
         </tr>
</table>
</center>  

<div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Due">
<center>
<div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
    <div>
            <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
            <div>
                <asp:Button runat="server" ID="btnshowdue" Text="Show Due" OnClick="btnShowDue_Click" style="display:none"></asp:Button>
                <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vetting Inspections Due in Next 60 Days</div>
                <div style="text-align:left">
                    <span style="float:right"><asp:Label runat="server" ID="recCount"></asp:Label></span>

                    <asp:RadioButton runat="server" ID="rad_s" GroupName="sc" Checked="true" OnCheckedChanged="btnShowDue_Click" AutoPostBack="true" /> SIRE / 
                    <asp:RadioButton runat="server" ID="rad_c" GroupName="sc" OnCheckedChanged="btnShowDue_Click" AutoPostBack="true"/>CDI
                    
                </div>
                <div style="height:30px; overflow:hidden; overflow-y:scroll; border:solid 2px #FFF0D1;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                    <thead>
                    <tr>
                    <td>Vessel Name</td>
                    <td style="width:100px; height:29px;">Last Done Dt.</td>
                    <td style="width:100px">Next Due Dt.</td>
                    <td style="width:150px">Status</td>
                    <td style="width:30px"></td>
                    </tr>
                    </thead>
                    </table>
                </div>
                <div style="height:300px; overflow-y:scroll; overflow-x:hidden;" id="dv_DueIns" class="ScrollAutoReset">
                    <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                    <tbody>
                    <asp:Repeater runat="server" ID="rpt_Due">
                    <ItemTemplate>
                    <tr>
                    <td style="text-align:left"><b>&nbsp;&nbsp;<%#Eval("VNAME2")%></b></td>
                    <td style="width:100px"><%#Eval("LASTDONE")%></td>
                    <td style="width:100px"><%#Eval("NEXTDUE")%></td>
                    <td style="width:150px"><%#Eval("NEXTINSPSTATUS")%></td>
                    <td style="width:30px"></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <div style="text-align:left; margin-top:5px; height:25px; text-align:right; ">
            <input type="button" id="btn_ClosebtnPopUP" class="Btn1" value="Close" onclick="HideDue();" style=" background-color:Red; color:White;"/>
    </div>
</div>
</center>
</div> 
<asp:UpdateProgress ID="upg_dv_Due" runat="server" AssociatedUpdatePanelID="up1">
<ProgressTemplate>
<center>
<div style="position:absolute;top:130px;left:0px; width:100%;z-index:150;">
<center>
    <img src="../Images/loading.gif" style="margin-top:200px;" />
</center>
</div>
</center>
</ProgressTemplate>
</asp:UpdateProgress>  

<div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Plan">
<center>
<div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
    <div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
            <div>
                <asp:Button runat="server" ID="btnshowPlanned" Text="Show Planned" OnClick="btnshowPlanned_Click" style="display:none"></asp:Button>
                <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vetting Inspections Planned for next 30 Days</div>
                <div style="text-align:left">
                    <span style="float:right"><asp:Label runat="server" ID="lblPlannedCount"></asp:Label></span>

                    <asp:RadioButton runat="server" ID="radS5" GroupName="sc51" Checked="true" OnCheckedChanged="btnshowPlanned_Click" AutoPostBack="true" /> SIRE / 
                    <asp:RadioButton runat="server" ID="radC5" GroupName="sc51" OnCheckedChanged="btnshowPlanned_Click" AutoPostBack="true"/>CDI
                    
                </div>
                <div style="height:30px; overflow:hidden; overflow-y:scroll; border:solid 2px #FFF0D1;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                    <thead>
                    <tr>
                    <td>Vessel Name</td>
                    <td style="width:200px; height:29px; text-align:left;">&nbsp;Inspection#.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Plan Date</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Port</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Suptd.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Attending</td>
                    <td style="width:30px"></td>
                    </tr>
                    </thead>
                    </table>
                </div>
                <div style="height:300px; overflow-y:scroll; overflow-x:hidden;" id="Div4" class="ScrollAutoReset">
                    <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                    <tbody>
                    <asp:Repeater runat="server" ID="rpt_Planned">
                    <ItemTemplate>
                    <tr>
                    <td style="text-align:left"><b>&nbsp;&nbsp;<%#Eval("VNAME2")%></b></td>
                    <td style="width:200px;text-align:left;">&nbsp;<%#Eval("NEXT_INSPNO")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Common.ToDateString(Eval("PLANDATE_DATE"))%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Eval("PORT")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#(Eval("ATTEND").ToString().Trim() == "")?Eval("REMOTE"):Eval("ATTEND")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#(Eval("ATTEND").ToString().Trim() == "") ? "No" : "Yes"%></td>
                    <td style="width:30px"></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <div style="text-align:left; margin-top:5px; height:25px; text-align:right; ">
            <input type="button" id="Button5" class="Btn1" value="Close" onclick="HidePlanned();" style=" background-color:Red; color:White;"/>
    </div>
</div>
</center>
</div> 
<asp:UpdateProgress ID="upg_dv_plan" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
<ProgressTemplate>
<center>
<div style="position:absolute;top:130px;left:0px; width:100%;z-index:150;">
<center>
    <img src="../Images/loading.gif" style="margin-top:200px;" />
</center>
</div>
</center>
</ProgressTemplate>
</asp:UpdateProgress>

<div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Resp">
<center>
<div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
    <div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
            <div>
                <asp:Button runat="server" ID="btnshowResponse" Text="Show Due" OnClick="btnshowResponse_Click" style="display:none"></asp:Button>
                <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vetting Response to be Uploaded</div>
                <div style="text-align:left">
                    <span style="float:right"><asp:Label runat="server" ID="lblRespCount"></asp:Label></span>

                    <asp:RadioButton runat="server" ID="rad_S1" GroupName="sc1" Checked="true" OnCheckedChanged="btnshowResponse_Click" AutoPostBack="true" /> SIRE / 
                    <asp:RadioButton runat="server" ID="rad_C1" GroupName="sc1" OnCheckedChanged="btnshowResponse_Click" AutoPostBack="true"/>CDI
                    
                </div>
                <div style="height:30px; overflow:hidden; overflow-y:scroll; border:solid 2px #FFF0D1;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                    <thead>
                    <tr>
                    <td>Vessel Name</td>
                    <td style="width:200px; height:29px; text-align:left;">&nbsp;Inspection#.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Insp. Status</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Days Remaining</td>
                    <td style="width:30px"></td>
                    </tr>
                    </thead>
                    </table>
                </div>
                <div style="height:300px; overflow-y:scroll; overflow-x:hidden;" id="dv_resp2" class="ScrollAutoReset">
                    <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                    <tbody>
                    <asp:Repeater runat="server" ID="rpt_Resp">
                    <ItemTemplate>
                    <tr <%#(Common.CastAsInt32(Eval("DAYSREM"))<=0)?"style='background-color:#ffe6e6'":""%> >
                    <td style="text-align:left"><b>&nbsp;&nbsp;<%#Eval("VESSELNAME")%></b></td>
                    <td style="width:200px;text-align:left;">&nbsp;<%#Eval("INSPECTIONNO")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Eval("STATUS")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Eval("DAYSREM")%></td>
                    <td style="width:30px"></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <div style="text-align:left; margin-top:5px; height:25px; text-align:right; ">
            <input type="button" id="Button2" class="Btn1" value="Close" onclick="HideResponse();" style=" background-color:Red; color:White;"/>
    </div>
</div>
</center>
</div> 
<asp:UpdateProgress ID="upg_dv_Resp" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
<center>
<div style="position:absolute;top:130px;left:0px; width:100%;z-index:150;">
<center>
    <img src="../Images/loading.gif" style="margin-top:200px;" />
</center>
</div>
</center>
</ProgressTemplate>
</asp:UpdateProgress>  

<div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Bonus">
<center>
<div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
    <div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
            <div>
                <asp:Button runat="server" ID="btn_ShowBonus" Text="Show Bonus" OnClick="btnshowBonus_Click" style="display:none"></asp:Button>
                <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vetting Bonus to be Cleared</div>
                <div style="text-align:left">
                    <span style="float:right"><asp:Label runat="server" ID="lbl_Bonus_Count"></asp:Label></span>

                    <asp:RadioButton runat="server" ID="rad_s2" GroupName="sc2" Checked="true" OnCheckedChanged="btnshowBonus_Click" AutoPostBack="true" /> SIRE / 
                    <asp:RadioButton runat="server" ID="rad_c2" GroupName="sc2" OnCheckedChanged="btnshowBonus_Click" AutoPostBack="true"/>CDI
                    
                </div>
                <div style="height:30px; overflow:hidden; overflow-y:scroll; border:solid 2px #FFF0D1;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                    <thead>
                    <tr>
                    <td>Vessel Name</td>
                    <td style="width:200px; height:29px; text-align:left;">&nbsp;Inspection#.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Done Dt.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;Status</td>
                    <td style="width:30px"></td>
                    </tr>
                    </thead>
                    </table>
                </div>
                <div style="height:300px; overflow-y:scroll; overflow-x:hidden;" id="Div2" class="ScrollAutoReset">
                    <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                    <tbody>
                    <asp:Repeater runat="server" ID="rpt_Bonus">
                    <ItemTemplate>
                    <tr>
                    <td style="text-align:left"><b>&nbsp;&nbsp;<%#Eval("VESSELNAME")%></b></td>
                    <td style="width:200px;text-align:left;">&nbsp;<%#Eval("INSPECTIONNO")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Common.ToDateString(Eval("ActualDATE"))%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Eval("STATUS")%></td>
                    <td style="width:30px"></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <div style="text-align:left; margin-top:5px; height:25px; text-align:right; ">
            <input type="button" id="Button3" class="Btn1" value="Close" onclick="HideBonus();" style=" background-color:Red; color:White;"/>
    </div>
</div>
</center>
</div> 
<asp:UpdateProgress ID="upg_dv_Bonus" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
<ProgressTemplate>
<center>
<div style="position:absolute;top:130px;left:0px; width:100%;z-index:150;">
<center>
    <img src="../Images/loading.gif" style="margin-top:200px;" />
</center>
</div>
</center>
</ProgressTemplate>
</asp:UpdateProgress>

<div style="position:absolute; z-index:110; top:100px;left :0px; width:100% ; display:none; text-align:center;" id="dv_Done">
<center>
<div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; background-color:White;">
    <div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
            <div>
                <asp:Button runat="server" ID="btn_ShowDone" Text="Show Done" OnClick="btnshowDone_Click" style="display:none"></asp:Button>
                <div style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Vetting Inspections Done in Last 30 Days</div>
                <div style="text-align:left">
                    <span style="float:right"><asp:Label runat="server" ID="lblDoneCount"></asp:Label></span>

                    <asp:RadioButton runat="server" ID="rad_s3" GroupName="sc3" Checked="true" OnCheckedChanged="btnshowDone_Click" AutoPostBack="true" /> SIRE / 
                    <asp:RadioButton runat="server" ID="rad_c3" GroupName="sc3" OnCheckedChanged="btnshowDone_Click" AutoPostBack="true"/>CDI
                    
                </div>
                <div style="height:30px; overflow:hidden; overflow-y:scroll; border:solid 2px #FFF0D1;">
                    <table cellpadding="0" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
                    <thead>
                    <tr>
                    <td>Vessel Name</td>
                    <td style="width:200px; height:29px; text-align:left;">&nbsp;Inspection#.</td>
                    <td style="width:100px; height:29px; text-align:left;">&nbsp;DoneDate</td>
                    <td style="width:30px"></td>
                    </tr>
                    </thead>
                    </table>
                </div>
                <div style="height:300px; overflow-y:scroll; overflow-x:hidden;" id="Div3" class="ScrollAutoReset">
                    <table cellpadding="0" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
                    <tbody>
                    <asp:Repeater runat="server" ID="rpt_Done">
                    <ItemTemplate>
                    <tr>
                    <td style="text-align:left"><b>&nbsp;&nbsp;<%#Eval("VESSELNAME")%></b></td>
                    <td style="width:200px;text-align:left;">&nbsp;<%#Eval("INSPECTIONNO")%></td>
                    <td style="width:100px;text-align:left;">&nbsp;<%#Common.ToDateString(Eval("ActualDATE"))%></td>
                    <td style="width:30px"></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
               </div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    <div style="text-align:left; margin-top:5px; height:25px; text-align:right; ">
            <input type="button" id="Button4" class="Btn1" value="Close" onclick="HideDone();" style=" background-color:Red; color:White;"/>
    </div>
</div>
</center>
</div> 
<asp:UpdateProgress ID="upg_dv_Done" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
<ProgressTemplate>
<center>
<div style="position:absolute;top:130px;left:0px; width:100%;z-index:150;">
<center>
    <img src="../Images/loading.gif" style="margin-top:200px;" />
</center>
</div>
</center>
</ProgressTemplate>
</asp:UpdateProgress>

</asp:Content>


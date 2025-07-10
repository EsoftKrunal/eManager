    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobTracking.aspx.cs" Inherits="Docket_JobTracking" Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>DryDock : Job Tracking </title>
    <script src="../JS/JQuery.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/JQScript.js" type="text/javascript"></script>

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
	<script src="http://code.jquery.com/jquery-1.10.2.js"></script>
	<script src="http://code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <style>
    *
    {
        font-family:Calibri;
        font-size:13px;
    }
    .cat
    {
        color:Red;
        font-weight:bold;
        background-color:#E0EBFF;
    }
    .head_row
    {
        color:grey;
        font-weight:bold;
        background-color:#eeeeee;
    }
    
    input:focus { 
        background-color: #FFE0EB;
    }
    .Completed
    {
        background-color:#70DB70;
        }
    
    .InProgress
    {
        background-color:#FFFFA3;
        
        }
    </style>    
</head>
<body>
    <div id="dvMain"  style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40); display:none; "><div style="font-size:25px; font-weight:bold; text-align:center; vertical-align:middle; color:White; top:400px; padding:200px 0px;">Loading.......</div></div> 
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="position:fixed;top:0px; width:100%; z-index:100; left:0px;">
    <div style="background-color:#1947A3;color:White; text-align:center; padding:3px; font-size:15px; "><b>JOB TRACKING</b></div>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="1">
        <tr>
        <td>
            <div style="border:none; background-color : #ADD6FF; padding:5px; font-size:13px; ">
            <table style="width :100%;">
            <tr>
            <td style="text-align:right; width:">Docket # :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblDocketNo"></asp:Label>
                <asp:ImageButton id="btnDocketView" runat="server" ImageUrl="~/Images/paperclipx12.png" OnClick="btnDocketView_Click" />
            </td>
            <td style="text-align:right">Vessel :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblVessel"></asp:Label></td>
            <td style="text-align:right">Type :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblType"></asp:Label></td>
            <td style="text-align:right">DD Duration :</td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPlanDuration"></asp:Label>&nbsp;
               <asp:LinkButton ID="lbUpdateDuration" Text="Update" runat="server" OnClick="lbUpdateDuration_Click" ></asp:LinkButton>
            </td>
            </tr>
            </table>
        </div>
            <div style="display:none; text-decoration:none;">
             <asp:TextBox ID="txtForDate" runat="server"></asp:TextBox>
             <asp:Button ID="btnReLoadData" OnClick="btnReLoadData_Click" runat="server" />
        </div>
        </td>
        </tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top; background-color:#eeeeee; padding:5px;" > 
           <table cellpadding="0" cellspacing="0" style="width:100%">
                <tr>
                <td style="width:80px"><b>Job Category :</b></td>
                <td style="width:300px"><asp:DropDownList runat="server" ID="ddlJObCat" AutoPostBack="true" OnSelectedIndexChanged="LoadCat_SelectIndexChanged" Width="275px"></asp:DropDownList></td>
                <td id="tdStatus" runat="server" style="width:370px;">
                     <table cellpadding="0" cellspacing="0" style="width:100%">
                      <tr>
                        <td style="width:80px"><b>Status :</b></td>
                        <td style="width:110px"><asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="LoadCat_SelectIndexChanged" Width="100px">
                        <asp:ListItem Text=" < -- All ---> " Value=""></asp:ListItem>
                        <asp:ListItem Text=" Not Started " Value="NS"></asp:ListItem>
                        <asp:ListItem Text=" In Progress " Value="IP"></asp:ListItem>
                        <asp:ListItem Text=" Completed " Value="CP"></asp:ListItem>
                        </asp:DropDownList></td>
                        <td style="width:80px"><b>Updated On :</b></td>
                        <td><asp:TextBox runat="server" ID="txtUpdatedOn" MaxLength="15"  Width="100px" AutoPostBack="true" OnTextChanged="LoadCat_SelectIndexChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtUpdatedOn"></asp:CalendarExtender>
                        </td>
                      </tr>
                      </table>
                </td>
                
                <td style="width:220px">
                    <div runat="server" id="spn_DR" visible="false">
                        <b>Update for Date : </b> <asp:TextBox runat="server" ID="txtSaveDate" MaxLength="15" Width="100px" BackColor="Yellow"></asp:TextBox>
                        <asp:CalendarExtender ID="c141" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtSaveDate"></asp:CalendarExtender>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="img_Refresh" OnClick="Refresh_Click" Text="Refresh" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
                    <asp:Button runat="server" ID="btn_DailyReport" Text="Daily Report" OnClick="btn_DailyReport_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />
                 </td>
                </tr>
            </table>
        </td> 
        </tr>
    </table>
    </div>
    <div style="position:absolute; top:87px; padding-bottom:50px;left:0px; width:100%;">
    <asp:Literal runat="server" ID="litData"></asp:Literal>
    </div>
    <div style="position:fixed;bottom:0px; width:100%; z-index:100; text-align:center; padding:5px; background-color:#FFFFCC; border-top:solid 1px #c2c2c2; left:0px;text-align:right">
            <asp:Label ID="lblMsgMain" ForeColor="Red" runat="server" Font-Bold="true" Font-Size="16px" style="margin:3px; float:left;"></asp:Label>
            <asp:Button runat="server" ID="btn_Save" Visible="false" Text="Save Planning" OnClientClick="this.value='Processing..';$('#dvMain').show();" OnClick="btn_SavePlanning_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />
            <asp:Button runat="server" ID="btnEnterComments" Text="Enter Progress" Visible="false" OnClick="btnEnterComments_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:150px;"  />
            <asp:Button runat="server" ID="btn_Cancel" Visible="false" Text="Cancel" OnClientClick="this.value='Processing..';$('#dvMain').show();" OnClick="btnCancel_Click" style=" padding:3px; border:none; color:White; background-color:red; width:150px;"  />
    </div>
     <%-- Update Remarks --%>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; display:none;" id="dv_Start" runat="server" >
        <center>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
        <div style="position: relative; width: 1000px; height: 350px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px gray;">
        <asp:UpdatePanel runat="server" id="UpdatePanel2">
        <ContentTemplate>
        <div style="padding:3px; background-color:orange" >
            <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px; font-weight:bold; font-size:16px;">
            <tr>
                <td style="text-align:center;"><span id="span_JC">Remarks</span></td>
            </tr>
            </table>
            <asp:HiddenField ID="hfdDocketJobId" runat="server" />
            <asp:HiddenField ID="hfdJObCode" runat="server" />
            <asp:HiddenField ID="hfdMode" runat="server" />
        </div>
        <div style="border-bottom:none;" class="scrollbox">
            <table cellspacing="0" rules="none" border="0" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <tr>
                    <td style="font-weight:bold;font-size:12px; text-align:left; " colspan="2">Enter Progress</td>
                </tr>                    
                <tr>
                    <td style="width:100px">Date :</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtStartDate" Width="100px" MaxLength="15" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="fd" ValidationGroup="ee" ErrorMessage="*" ControlToValidate="txtStartDate"></asp:RequiredFieldValidator>
                        <asp:CalendarExtender runat="server" ID="c1" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                                      
                    </td>
                    </tr>
                    <tr>
                    
                    <td style="width:100px">Progress (%) :</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtProgress" Width="30px" MaxLength="3" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ValidationGroup="ee" ErrorMessage="*" ControlToValidate="txtProgress"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    
                    <td style="width:100px">Remarks :</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtRemarks" Width="98%"  TextMode="MultiLine" Height="180px" runat="server" ></asp:TextBox>
                    </td>
                </tr>
                </table>
                <div style="text-align:center; padding:0px;">
                <div style="padding:5px;">
                <asp:Label ID="lbl_MsgRemarks" ForeColor="Red" runat="server"></asp:Label>
                </div>
              
                <div style="text-align:center;">
                    <asp:Button runat="server" ID="btn_UpdateStart" Text="Save" ValidationGroup="ee" OnClick="btn_UpdateStart_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                    <asp:Button runat="server" ID="btn_CloseStart" Text="Close" OnClick="btn_CloseStart_Click" OnClientClick="return Close_Box();" CausesValidation="false" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                </div>
                </div>
              </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_CloseStart" />
        </Triggers>
        </asp:UpdatePanel>
        </div>
        </center>
        </div>
        <div id="dvComments" style="border:solid 10px #c2c2c2; background-color:White;display:none; position:absolute; top:100px; left:200px; height:300px; width:600px;" >
            <div style="background-color:Green;vertical-align:middle; text-align:center; padding-top:5px; height:20px; position:relative; color:White;box-sizing:border-box;">Remarks</div>
            <div style='height:250px; position:relative;' id="dvCommentstext">


            </div>
            <div style="height:20px; vertical-align:middle;box-sizing:border-box; padding-top:5px; text-align:center; background-color:c2c2c2; position:relative;"><a href="#" style="color:Red;" onclick="$('#dvComments').slideUp();">Close</a> </div>
        </div>
        <div id="dvSubJobs" style="border:solid 10px #c2c2c2; background-color:White;display:none; position:absolute; top:50px; left:200px; height:450px; width:750px;" >
            <div style="background-color:Green;vertical-align:middle; text-align:center; padding-top:5px; height:20px; position:relative; color:White;box-sizing:border-box;">SubJob List</div>
            <div style='position:relative;'>
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
            <ContentTemplate>
            <div style="display:none;">
                 <asp:Button ID="lnkSubJobs" Text="adsda" runat="server" OnClick="lnkSubJobs_Click" ></asp:Button>
                 <asp:TextBox ID="txtSubJobCode" runat="server" ></asp:TextBox>
            </div>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 400px ; text-align:center;" class="ScrollAutoReset" id="d1232">
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="width:15px;" />                                                                        
                                <col style="width:120px;" />
                                <col/>
                                <col style="width:17px;" />
                            </colgroup>
                            <asp:Repeater ID="rptSubJobs" runat="server">
                                <ItemTemplate>
                                        <tr >
                                        <td style="text-align:center"></td>
                                        <td style="text-align:left; "> <%#Eval("SubJobCode")%></td>
                                        <td align="left"><b>Short Descr :&nbsp;</b><%#Eval("SubJobName")%><hr /><b>Long Descr :&nbsp;</b><%#Eval("LongDescr")%> </td>                                                                                
                                        <td>&nbsp;</td>
                                        </tr>
                                </ItemTemplate>      
                                                                        
                            </asp:Repeater>
                        </table>
        </div>
            </ContentTemplate>
            </asp:UpdatePanel>

            </div>
            <div style="height:20px; vertical-align:middle;box-sizing:border-box; padding-top:5px; text-align:center; background-color:c2c2c2; position:relative;"><a href="#" style="color:Red;" onclick="$('#dvSubJobs').slideUp();">Close</a> </div>
        </div>
        <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_RFQExecution" runat="server" visible="false">
                <center>
                  <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                  <div style="position: relative; width: 450px; height: 120px; padding: 0px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px #333;">
            <asp:UpdatePanel runat="server" id="UpdatePanel3">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange; text-align:center; font-weight:bold;" >Update DD Duration</div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:150px; ">Execution From : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtExecFrom" runat="server" Width="100px" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExecFrom" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; ">Execution To : </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtExecTo" runat="server" Width="100px" ></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtExecTo" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>
                </table>
             </div>           

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsg_Exec" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btnExecuteRFQ" Text="Save" OnClientClick="return confirm('This will remove data outside the DD duration. Are you sure ?');" OnClick="btnExecuteRFQ_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                        <asp:Button runat="server" ID="btnCloseExec" Text="Close" OnClick="btnCloseExec_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExecuteRFQ" />
                <asp:PostBackTrigger ControlID="btnCloseExec" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
                </center>
            </div>
        <script type="text/javascript">
   
        function ShowHistory(ctl) {
            $("#span_JC").html($(ctl).attr('linkid') + ' : ' + $(ctl).attr('JobName'));
            $("#hfdJObCode").val($(ctl).attr('linkid'));
            $("#hfdDocketJobId").val($(ctl).attr('docketjobid'));

            $("#hfdMode").val("P");
            $("#txtRemarks").val($(ctl).attr('ExecRemarks'));
            //$("#dv_Start").show();
            window.open('JobTrackingHistory.aspx?key=' + '<%=Request.QueryString["key"]%>' + '&CatId=' + <%=ddlJObCat.SelectedValue%> +  '&DocketJobId=' + $(ctl).attr('docketjobid'), '');
        }


        function ShowComments(ctl) {
            var jobcode = $(ctl).attr('jobcode');
            var comments = $('#cmt_' + jobcode).html();
            $("#dvCommentstext").html(comments);
            $("#dvComments").slideDown();
        }

        function Close_Box() {
            $("#dv_Start").hide();
            return false;
        }
        function ShowSubJobs(ctl) {            
            $("#txtSubJobCode").val($(ctl).attr('JobCode'));
            $("#lnkSubJobs").click();
            $("#dvSubJobs").slideDown();
        }
        function SetDate(ctl) {
            $("#dvMain").show();
            $("#txtForDate").val($(ctl).attr('ForDate'));
            $("#btnReLoadData").click();

        }
        function ShowAll() {
            $("#dvMain").show();
            $("#txtForDate").val('');
            $("#btnReLoadData").click();
            $("#dvMain").hide();
        }

        
         function SelectAll(ctl) {
            var catcode=$(ctl).attr('catcode');
            $(".subjobs[catcode='" + catcode +"']").prop("checked", $(ctl).prop('checked'));
         }
    </script>
        <div id='debug1'></div>
    </form>
</body>
</html>

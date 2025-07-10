<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupMonitorTrainingDetails_Due.aspx.cs" Inherits="CrewOperation_popupMonitorTrainingDetails_Due" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../JS/jquery.min.js"></script>
    <style type="text/css">
        .MainRow
        {
            background-color: #ffffff;
            padding:3px; 
            font-weight:bold;
            font-size: 13px;
            text-align:left;
        }
         .spanCrewNumber 
        {
            
        }
          .spanCrewName
        {
            color:Blue;
        }
           .spanRank
        {
             color:orange;
        }
        .ChildRow
        {
            font-size: 12px;
            text-align:left;
            margin-bottom:1px;
            border-bottom:solid 1px #333;
        }
         .ChildRow table thead tr td
        {
            font-weight:bold;
            color: #4371A5;
            padding:5px;
            border-bottom:dotted 1px #c2c2c2;
        }
        .ChildRow table tbody tr td
        {
            padding:5px;
        }
    </style>
</head>
<body style="margin:0px ; font-family:Calibri;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="position:fixed;top:0px;left:0px; width:100%; height:50px;">
        <table style="border-collapse: collapse; border: solid 2px #4371a5" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="border: #4371a5 1px solid; background-color: #4371a5; color: White; height: 25px; text-align:center">
                    <span style="font-size: 15px;"><asp:Label runat="server" ID="lblPageTitle"></asp:Label></span>
                </td>
            </tr>
           <tr>
           <td style="border: #FFECFF 1px solid; background-color: #FFECFF; color:Black; height: 25px;text-align:center">
               <span style="font-size: 15px;"><asp:Label runat="server" ID="lblVesselName"></asp:Label></span>
           </td>
           </tr>
           
            </table>
    </div>
        <div style='height:55px;'>&nbsp;</div>
        <table style="border-collapse: collapse; border: solid 2px #4371a5; margin-top:0px;" cellpadding="0" cellspacing="0" width="100%">
             <tr>
                <td><asp:Literal runat="server" ID="litTraining"></asp:Literal></td>
            </tr>
        </table>

        <div id="dvFrame" style="display:none;">
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
        <div>
           
        <div style="position:fixed; left:0px ; top:0px; height:100%; width:100%; background-color:rgba(0,0,0,0.4);">
           
            <center>

                <div style="width:80%;background-color:white; margin-top:5%;border:solid 5px black;">
                      <div style="display:none;">
                <asp:Button runat="server" ID="b1" Text="post" OnClick="b1_Click" CausesValidation="false" />
                <asp:TextBox runat="server" ID="hfdtrainingid"/>
                <asp:TextBox runat="server" ID="hfdcrewid"/>
                <asp:TextBox runat="server" ID="hfdrankid"/>
            </div>
                    <b style="font-size:22px; padding:6px;">Update Training</b>
                    <div class="ChildRow" style="border-bottom:none;">
                        <table cellspacing='0' cellpadding='0' width='100%' border="0" >
                            <thead>
                                <tr>
                                    <td style="width:30px"> <input type="checkbox" onclick="checkuncheck(this);" /></td>
                                    <td  style="width:150px"> Training Type</td>
                                    <td > Training Name</td>
                                    <td  style="width:60px"> Crew#</td>
                                    <td  style="width:240px"> Crew Name</td>
                                    <td  style="width:100px"> Rank Name </td>
                                    <td  style="width:100px"> Source </td>
                                    <td  style="width:100px"> Last Done </td>
                                    <td  style="width:100px"> Next Due Dt.</td>
                                    <td  style="width:5px; font-size:3px">&nbsp;</td>
                                </tr>
                            </thead>
                            </table>
                        </div>
                        <div style="height:410px; overflow-y:scroll;overflow-x:hidden; border-bottom:none;" class="ChildRow">
                            <table cellspacing='0' cellpadding='0' width='100%' border="0" >
                        <tbody>
                            <asp:Repeater runat="server" ID="rptdata">
                            <ItemTemplate>
                                    <tr>
                                    <td style="width:30px"><asp:CheckBox runat="server" CssClass="c1" ID="chkSel" />
                                        <asp:HiddenField runat="server" ID="hfdPK" Value='<%#Eval("TrainingRequirementId")%>' />

                                    </td>
                                    <td  style="width:150px"><%#Eval("TrainingTypeName")%></td>
                                    <td ><%#Eval("TrainingName")%></td>
                                    <td  style="width:60px"><%#Eval("CrewNumber")%></td>
                                    <td  style="width:240px"><%#Eval("CrewName")%></td>
                                    <td  style="width:100px"><%#Eval("RankName")%></td>
                                    <td  style="width:100px"><%#Eval("AssignSource")%></td>
                                    <td  style="width:100px"><%#Common.ToDateString(Eval("LASTDONECOMPUTED"))%></td>
                                    <td  style="width:100px"><%#Common.ToDateString(Eval("N_DueDate"))%></td>
                                    </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        </table>
                    </div>

                    <div style="padding:5px; background-color: #e8e8e8">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                           
                           <tr>
                               <td>
                                   From Date :</td>
                               <td style="text-align: left; width:100px;">
                                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                               </td>
                               <td style="text-align:left; width:100px;">
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_FromDate"  Display="Dynamic"  ErrorMessage="*"  ></asp:RequiredFieldValidator>  
                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_FromDate"  Display="Dynamic"  ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>

                               </td>
                               <td>To Date :</td>
                               <td style="text-align: left;width:100px;">
                                   <asp:TextBox ID="txt_ToDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                                   </td>
                               
                                   <td style="text-align:left; width:100px;">
                                       <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_ToDate" ErrorMessage="*" Display="Dynamic"  ></asp:RequiredFieldValidator>  
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_ToDate"  Display="Dynamic"   ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   </td>

                               <td>Institute :</td>
                               <td style="text-align: left; width:160px;">
                                   <asp:DropDownList ID="ddl_TrainingReq_Training" runat="server" CssClass="input_box" TabIndex="2" Width="150px"></asp:DropDownList>

                               </td>
                               <td style="text-align: left; width:30px;">
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddl_TrainingReq_Training" ErrorMessage="*" Display="Dynamic"  ></asp:RequiredFieldValidator>  
                               </td>
                               <td style="text-align:right;">
                                    <asp:Button ID="btn_Save_PlanTraining" runat="server" Text="Update Training" Width="130px" style="border:solid 0px red; background-color:#3194ff; color:white; padding:4px;"  TabIndex="9" OnClick="btn_Save_Training_Click" />
                                   <input type="button" value="Close" onclick="Close();" style="border:solid 0px red; background-color:red; color:white; padding:4px; width:80px;" />
                               </td>
                           </tr>
                           
                       </table>
                              <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_FromDate"></ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txt_ToDate"></ajaxToolkit:CalendarExtender>
                        </div>
                </div>

            </center>
        </div>
        </div>
        </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btn_Save_PlanTraining" />
            </Triggers>--%>
        </asp:UpdatePanel>
    <div>
    </form>
    <script type="text/javascript">
        function post(ctl)
        {
            $("#hfdcrewid").val($(ctl).attr("crewid"));
            $("#hfdtrainingid").val($(ctl).attr("trainingid"));
            $("#hfdrankid").val($(ctl).attr("rankid"));
            $("#dvFrame").show();

            $("#b1").focus();
            $("#b1").click();
        }
        function checkuncheck(ctl)
        {
            $(".c1 input").prop('checked', $(ctl).prop('checked'));
        }
        function Close()
        {
            $("#dvFrame").hide();
        }
        function OpenClose(ctl)
        {
            if ($(ctl).html() == "+") {
                $(ctl).html("-");
                $('#' + $(ctl).attr('target')).show();
            }
            else {
                $(ctl).html("+");
                $('#' + $(ctl).attr('target')).hide();
            }
        }
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTraining1.aspx.cs" Inherits="CrewOperation_ManageTraining1" EnableEventValidation="false" EnableViewStateMac="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Matrix</title>
    <link href="~/Styles/style.css" rel="stylesheet" type="text/css" /> 
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="../JS/jquery-1.10.2.js" type="text/jscript"></script>
    <script src="../JS/KPIScript.js"></script>
    <link href="../Styles/jquery.datetimepicker.css" rel="stylesheet" />
    <style type="text/css">
         th{
            border-top:solid 1px #e2e2e2;
            border-left:solid 1px #e2e2e2;
            font-size:13px;
            padding:6px 5px;

        }
        .bordered > tbody > tr > td{
            border-top:solid 1px #e2e2e2;
            border-left:solid 1px #e2e2e2;
            font-size:13px;
            padding:3px 5px;
        }
        .bordered > tbody > tr:hover{
          background-color:#ffd800 !important;
          cursor:pointer;
        }
        .selrow {
            background-color:#ffd800 !important;
          cursor:pointer;
        }
        .container
        {
            position:relative;border-bottom:solid 2px #c2c2c2;
        }
       
        .newbtn
        {
            padding:7px 9px;
            border:none;
            color:white;
            background-color:#00beff;
            display:inline;
            font-size:13px;
        }
        .btnback {
            background-color:#808080 !important;
        }
        .slidecrew {
            margin-left:600px;            
        }
        .slidecrewlist {
            position:absolute;top:0px;left:0px;width:600px;
            border-right:solid 2px  #c2c2c2;
        }
        .closeButton {
            background: none;
            border: none;
        }
    </style>
</head>
<body style="margin-left:0px;"> 
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    
    <div style="text-align:center;background-color:black;color:white;border-bottom:solid 2px black;padding-top:10px;">
            <span style="font-size:20px;font-weight:bold;">Training Matrix Compliance</span>
            <div style="padding:5px;font-size:14px;">Vessel : <span id="lblVesselName" runat="server"></span> as on <%=DateTime.Today.Date.ToString("dd-MMM-yyyy") %></div>
    </div>

    
               
    
                            
    
    <div id="container" class="container1" >
            <div class="slide slidecrewlist1"  >

            <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
            <table border="0" width="100%" cellpadding="0" cellspacing="0" class="bordered">   
                <col  width="70px;" />
                <col  />
                <col  width="60px;" />
                <col  width="55px;" />
                <col  width="55px;" />
                <col  width="55px;" />
                <col  width="20px;" />
                <thead>
                    <tr style="background-color:#00beff;color:white;">
                        <th >Crew#</th>
                        <th style="text-align:left">Crew Name</th>
                        <th style="text-align:left">Rank</th>
                        <th style="text-align:right">Plan.</th>
                        <th style="text-align:right">Comp.</th>
                        <th style="text-align:right">Rem.</th>
                        <th>&nbsp;</th>
                    </tr>
                </thead>
                </table>
            </div>
            <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:200px;" id="divCrewMemberList11" class="ScrollAutoReset">
               <%--<asp:UpdatePanel ID="updatepanel" runat="server" >
                   <ContentTemplate>--%>
                        <table border="0" width="100%" cellpadding="0" cellspacing="0" class="bordered">                
                <col  width="70px;" />
                <col  />
                <col  width="60px;" />
                <col  width="55px;" />
                <col  width="55px;" />
                <col  width="55px;" />
                <col  width="20px;" />
                <tbody>
                    <asp:Repeater ID="rptCrewMember" runat="server">
                        <ItemTemplate>

                            <tr  class='<%# ((CrewID==Common.CastAsInt32( Eval("CrewId")))?"selrow":"")  %>' >
                                <td>
                                    <asp:LinkButton ID="lnkCrewNumber" runat="server"  Text='<%# Eval("CrewNumber") %>' OnClick="lnkCrewNumber_OnClick" CommandArgument='<%# Eval("CrewId") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hfdRankGroupID" runat="server" Value='<%# Eval("RankGroupID") %>' />
                                    <asp:HiddenField ID="hfdCrewID" runat="server" Value='<%# Eval("CrewId") %>' />
                                </td>
                                <td style="text-align:left"> <asp:Label ID="lblCrewNumber" runat="server" Text=' <%# Eval("CREWNAME") %>'></asp:Label></td>
                                <td style="text-align:left"> <asp:Label ID="lblRankCode" runat="server" Text=' <%# Eval("RankCode") %>'></asp:Label></td>                                
                                <td style="text-align:right"><%# Eval("Planned") %></td>
                                <td style="text-align:right"><%# Eval("Completed") %></td>
                                <td style="text-align:right"><%# Eval("Remaining") %></td>
                                <td>&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                 </tbody>
        </table>
                   <%--</ContentTemplate>
               </asp:UpdatePanel>--%>
            </div>
            </div>
        <%--<asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
            <div class="slide slidecrew1" style="padding-top:1px;padding-bottom:5px;" id="divTrainings" runat="server" visible="false">
                <div style="text-align:center;padding:10px 15px;font-size:16px;font-weight:bold;">
                    <asp:label id="lblselectedUsername" runat="server"></asp:label>   : 
                    <asp:label id="lblselectedCrewNumber" runat="server"></asp:label>
                    <asp:label id="lblselectedRankCode" runat="server" style="color:#00beff"></asp:label>
                    <asp:label id="lblSignOnDate" runat="server" ToolTip="Sign On Date, Relief Due Date" style="color:#00beff"></asp:label>
                </div>
                <div style="text-align:right;padding:5px 10px;">
                    <div style="border:solid 0px red;text-align:center;">
                        <div class="newbtn"  ><i class="fa fa-plus"></i> 
                            <asp:Button ID="btnAssignTraining" runat="server" Text="Assign From Master" CssClass="newbtn" OnClick="btnAssignTraining_OnClick" Width="130px" /> </div>&nbsp;
                        
                        <div class="newbtn" ><i class="fa fa-download"></i> 
                            <asp:Button ID="btnImportFromMatrix" runat="server" Text="Assign From Matrix" CssClass="newbtn" OnClick="btnImportFromMatrix_OnClick" Width="120px"/> &nbsp;
                        </div>
                        &nbsp;
                        <div class="newbtn" ><i class="fa fa-edit"></i> 
                            <asp:Button ID="btnUpdatePlanDuePopup" runat="server" Text="Change Plan Date" CssClass="newbtn" OnClick="btnUpdatePlanDuePopup_OnClick" Width="130px"/> &nbsp;
                        </div>
                        &nbsp;
                        <div class="newbtn"><i class="fa fa-refresh"></i> 
                            <asp:Button ID="btnShowHistory" runat="server" Text="Show Training History" CssClass="newbtn" OnClick="btnShowHistory_OnClick" Width="140px"/>   </div>
                    &nbsp;
                    <div class="newbtn" ><i class="fa fa-check"></i> 
                        <asp:Button ID="btnCloseDue" runat="server" Text="Update Training" CssClass="newbtn" OnClick="btnCloseDue_OnClick" Width="100px"/> &nbsp;
                    </div>

                        &nbsp;
                    <div class="newbtn" ><i class="fa fa-trash"></i> 
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="newbtn" OnClick="btnDelete_OnClick" OnClientClick="return confirm('Are you sure to delete the records');"  Width="55px" Visible="false"/> &nbsp;
                    </div>
                </div>
                
                </div>
                <div id="dvdue" runat="server" visible="false">
                <div id="dvtrainings" style="background-color:white;overflow:hidden;"  >
                    <asp:Repeater ID="rptTrainingsDue" runat="server">
            <HeaderTemplate>
                <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                <col width="30px" />
                <col width="50px" />
                <col />
                <%--<col width="100px" />--%>
                <col width="150px" />
                <col width="130px" />
                
                <col width="100px" />
                <col width="20px" />          
                    <thead>
                <tr>
                    <th>
                        <asp:CheckBox ID="chkSelAll" runat="server" OnCheckedChanged="chkSelAll_OnCheckedChanged" AutoPostBack="true" />
                    </th>
                    <th>Sr#</th>
                    <th style="text-align:left;">Training Name</th>
                    <th style="text-align:center">Last Done</th>
                    <th>Source</th>
                    <th style="text-align:center">Plan Date</th>
                    <th style="text-align:center">&nbsp;</th>
                                            
                </tr>
                        </thead>
            </table>        
                </div>
                <div id="divTrainingDueContent" style="width:100%;overflow-x:hidden;overflow-y:scroll;height:250px;" class="ScrollAutoReset">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                    <col width="30px" />
                <col width="50px" />
                <col />
                <%--<col width="100px" />--%>
                <col width="150px" />
                <col width="130px" />
                
                <col width="100px" />    
                 <col width="20px" />
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <%--onclick="selectTraining(this)"--%>
                <tr >
                    <td>
                        <%--Visible='<%# (Eval("Source").ToString()!="PEAP" && Eval("MatrixCompatibility").ToString().Trim().ToLower()=="[ na ]" ) %>' --%>
                        <asp:CheckBox ID="chkTraining" runat="server" />
                        <asp:HiddenField ID="hfdTrainingRequirementId" runat="server" Value='<%#Eval("TrainingRequirementId")%>' />
                        <asp:HiddenField ID="hfdSource" runat="server" Value='<%#Eval("Source")%>' />
                    </td>
                    <td><%#Eval("SNO") %></td>
                    <td style="text-align:left;"><%#Eval("TrainingName") %></td>
                    <td style="text-align:center"><%#Common.ToDateString( Eval("LastDone")) %></td>
                    <td> 
                        <%#Eval("Source") %> 
                        <span style="color:red;"> <%# ((Eval("Source").ToString()=="MATRIX")? Eval("MatrixCompatibility").ToString():"") %></span>

                    </td>                    
                    <td style="text-align:center"><%#Common.ToDateString(Eval("PlanDate")) %></td>                                            
                    <%--<td style="text-align:center">
                        <asp:ImageButton ID="imgDelteTraining" runat="server" OnClick="btnDeleteTraining_OnClick" CommandArgument='<%#Eval("TrainingRequirementId")%>' ImageUrl="~/Modules/HRD/Images/delete_12.gif" Visible='<%# (Eval("Source").ToString()!="PEAP") %>' OnClientClick="return confirm('Are you sure to delete?');"/>

                    </td>--%>
                    <td></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
                </div>
            </FooterTemplate>
            </asp:Repeater>
                </div>
            </div>

            <div id="dvdone" runat="server" visible="false">
                <div id="dvtrainings1" style="background-color:white;overflow:hidden;"  >
                    <asp:Repeater ID="rptTrainingsDone" runat="server">
                    <HeaderTemplate>
                    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                    <col width="50px" />
                    <col />                    
                    <col width="100px" />
                    <col width="100px" />
                    <col width="100px" />
                    <col width="200px" />           
                        <col width="30px" />        
                    <thead>
                    <tr>
                        <th>Sr#</th>
                        <th style="text-align:left;">Training Name</th>
                        <th>Source</th>
                        <th style="text-align:center">From Due</th>
                        <th style="text-align:center">To Date</th>
                        <th style="text-align:left">Institute Name</th>                                            
                        <th style="text-align:center"></th>     
                    </tr>
                    </thead>
                    </table> 
                   </div>
                    <div id="divTrainingDueContent1" style="width:100%;overflow-x:hidden;overflow-y:scroll;" class="ScrollAutoReset">
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                            <col width="50px" />
                            <col />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="200px" />         
                            <col width="30px" />
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr >
                            <td><%#Eval("SNO") %></td>
                            <td style="text-align:left;"><%#Eval("TrainingName") %></td>
                            <td><%#Eval("SourceName") %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("FromDate")) %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("ToDate")) %></td>                                            
                            <td style="text-align:left"><%#Eval("institutename")%></td>
                            <td>
                                <asp:ImageButton ID="btndownload" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btndownload_OnClick" CommandArgument='<%#Eval("TrainingRequirementId") %>' Visible='<%# (Common.CastAsInt32( Eval("HasFile"))>0) %>' />
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                        </div>
                    </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
                <%-- Pop model------------------------------------------------------------------------------------------------------------------------------%>
                    <div id="myModal" class="modal" runat="server" visible="false">
        <!-- Modal content -->
        <div class="modal-content">
    <div class="modal-header">
      <%--<span class="close" onclick="CloseModel()">&times;</span>--%>
        <asp:Button ID="btnCloseMyModel" runat="server" Text="X" CssClass="close closeButton" OnClick="btnCloseMyModel_OnClick" />
      <h2>Select Trainings</h2>
    </div>
    <div class="modal-body">
        <asp:UpdatePanel runat="server" ID="uptra">
                <ContentTemplate>
                    <center >   
                        <div>
                        <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
                            <tr style="background-color:#c2c2c2 ; font-weight:bold; color:#333">
                                <td style="width:350px; padding:10px; font-size:13px;">
                                    Select Group
                                </td>
                                 <td style="padding:10px; font-size:13px;padding-left:3px;">
                                     <input type="checkbox" onclick="selectalltrainings(this);" checked="true" />
                                    Select Trainings
                                </td>
                            </tr>
                            <tr>
                                <td>
                                 <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 350px; border-bottom:solid 1px #c2c2c2;">
                                  <table width="100%" cellpadding="3" border="0" style=" border-collapse: collapse" class="bordered" >
                                                    <col width="30px" />
                                                    <col />
                                                        <asp:Repeater ID="rptTrainingGroup" runat="server" >
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelectGroup" AutoPostBack="true" runat="server" runat="server" OnCheckedChanged="chkSelectGroup_CheckedChanged" CssClass='<%#Eval("ChapterNo")%>'  />
                                                                    </td>
                                                                    <td align="left"><%#Eval("ChapterName")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                 </div>
                                </td>
                                <td id="tngs">
                                 <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 350px; border-bottom:solid 1px #c2c2c2;" id='uji6788768' class="ScrollAutoReset">
                                    <table width="100%" cellpadding="3" border="0" style=" border-collapse: collapse" class="bordered" >
                                    <col width="30px" />
                                    <col />
                                        <asp:Repeater ID="rptTrainings" runat="server" >
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSelect" runat="server" runat="server" CssClass='<%#Eval("trainingid")%>' />
                                                    </td>
                                                    <td align="left"><%#Eval("TrainingName")%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                  </div>
                                </td>
                            </tr>
                        </table>
                        <div style="padding:3px; text-align:right">
                            <table width="100%" border="0" style=" border-collapse: collapse" >
                                <tr>
                                    <td><asp:Label runat="server" ID="lblmsg1" style="float:left" Font-Bold="true" ForeColor="Red"></asp:Label></td>
                                    <td style="width:95px;text-align:right">
                                        Planned For :</td>
                                    <td style="width:125px; text-align:left">
                                        <asp:TextBox ID="txt_DueDate" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" PopupPosition="TopLeft" TargetControlID="txt_DueDate"></ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="width:210px">

                                        <asp:Button ID="btnFinalSave" runat="server" CssClass="btn" style="padding:5px; background-color:orangered;color:white;" Text="Assign Trainings" Width="120px" TabIndex="9" OnClick="btnFinalSave_Click" CausesValidation="False" />
                                        <%--<asp:Button ID="btnClose" runat="server" CssClass="btn" style="padding:5px; background-color:orangered;color:white;" Text="Close" Width="70px" TabIndex="9" OnClick="btnClose_Click" CausesValidation="False" />--%>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                            </div>
                    </center>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnFinalSave" />
                </Triggers>
            </asp:UpdatePanel>
    </div>
    <div class="modal-footer">
      
    </div>
  </div>
    </div>



    
                <%--------------------------------------------------------------------------------------------------------------------------------%>
                <div id="myModal1" class="modal" runat="server" visible="false">
        <!-- Modal content -->
        <div class="modal-content">
    <div class="modal-header">
      <%--<span class="close" onclick="CloseModelMatrix()">&times;</span>--%>

        <asp:Button ID="btnCloseMyModel1" runat="server" Text="X" CssClass="close  closeButton" OnClick="btnCloseMyModel1_OnClick" />
      <h2>Import From Matrix</h2>
    </div>
    <div class="modal-body">
        <div id="dvtrainingsFromMatrix" style="background-color:white;height:400px">
            <asp:Repeater ID="rptTrainingMatrix" runat="server">
            <HeaderTemplate>
                <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                <col width="30px" />
                
                <col />
                <col width="100px" />
                
                <col width="100px" />
                
                                        
                    <thead>
                <tr>
                    <th>
                        <asp:CheckBox ID="chkSeleAllMatrix" runat="server" AutoPostBack="true"  OnCheckedChanged="chkSeleAllMatrix_OnCheckedChanged"/>
                    </th>
                    <th style="text-align:left;">Training Name</th>
                    <th style="text-align:center">Last Done</th>                    
                    <th style="text-align:center">Next Due</th>
                </tr>
                        </thead>
            </table>        
                </div>
                <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:370px;">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                    <col width="30px" />                
                    <col />
                    <col width="100px" />                
                    <col width="100px" />
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <%--onclick="selectTraining(this)"--%>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkTraining" runat="server"  />
                        <asp:HiddenField ID="hfdTrainingId" runat="server" Value='<%#Eval("TrainingId")%>' />
                    </td>
                    <td style="text-align:left;"><%#Eval("TrainingName") %></td>
                    <td style="text-align:center"><%#Common.ToDateString( Eval("LastDoneDate")) %></td>
                    <td style="text-align:center"><%#  ((Common.ToDateString( Eval("LastDoneDate"))!="")? Common.ToDateString(Eval("DueDate")):"") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                </table>
                </div>
            </FooterTemplate>
            </asp:Repeater>
        </div>
        <div style="padding:4px;text-align:center;">
            &nbsp; <asp:Label ID="lblMsgTrainingMatrix" runat="server" style="color:red;"></asp:Label>
        </div>
        <div style="padding:4px;">
            <table cellpadding="4" cellspacing="0" border="0" width="100%">
                <col width="300px" />
                <col />
                <col width="200px" />
                <tr>
                    <td>
                        Planned For : <asp:TextBox ID="txtDueDateTrainingMatrix" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txtDueDateTrainingMatrix"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkApplyAllCrew" runat="server" /> <b> Apply same training to all on board crew members </b>
                    </td>
                    <td>
                        <asp:Button ID="btnSaveTrainingsFromMatrix" runat="server" Text="Save" OnClick="btnSaveTrainingsFromMatrix_OnClick"  CssClass="newbtn"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="modal-footer">
      
    </div>
  </div>
    </div>




    <%--------------------------------------------------------------------------------------------------------------------------------%>
    <div id="myModel3" class="modal" runat="server" visible="false">
<!-- Modal content -->
<div class="modal-content">
<div class="modal-header">
<asp:Button ID="btnCloseMyModel3" runat="server" Text="X" CssClass="close  closeButton" OnClick="btnCloseMyModel3_OnClick" />
<h2>Trainings Update</h2>
</div>
<div class="modal-body">
<div id="dvtrainingsComp" style="background-color:white;height:400px">
<asp:Repeater ID="rptTrainingMatrix3" runat="server">
<HeaderTemplate>
    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
    <col />
    <col width="100px" />
    <col width="100px" />    
    <col width="100px" />    
    <col width="100px" />    
    <col width="120px" /> 
    <col width="120px" />    
    <col width="150px" />   
        <col width="150px" />   
    <thead>
    <tr>
        <th style="text-align:left;">Training Name</th>
        <th style="text-align:center">Source</th>
        <th style="text-align:center">Last Done</th>
        <th style="text-align:center">Due Date</th>                    
        <th style="text-align:center">Planned For </th>
        <th style="text-align:center">From Date  </th>
        <th style="text-align:center">To Date</th>
        <th style="text-align:center">Training Institute</th>
        <th style="text-align:center">Attachment</th>
    </tr>
    </thead>
</table>        
    </div>
    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:370px;">
    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
            <col />
            <col width="100px" />
            <col width="100px" />    
            <col width="100px" />    
            <col width="100px" />  
            <col width="120px" />   
            <col width="120px" />    
            <col width="150px" />  
        <col width="150px" />   
        <tbody>
</HeaderTemplate>
<ItemTemplate>
    <tr>
        <td style="text-align:left;"><asp:HiddenField ID="hfdTrainingRequirementId" runat="server" Value='<%#Eval("TrainingRequirementId")%>' /><%#Eval("TrainingName") %></td>
        <td style="text-align:center"><%#Eval("SourceName")%></td>
        <td style="text-align:center"><%#Common.ToDateString( Eval("LastDone")) %></td>
        <td style="text-align:center"><%#Common.ToDateString(Eval("n_duedate")) %></td>        
        <td style="text-align:center"></td>
        <td style="text-align:center">
            <asp:TextBox runat="server" id="txtFromDate" width="95px" CssClass="dateonly" ></asp:TextBox>
        </td>
        <td style="text-align:center">
                <asp:TextBox runat="server" id="txtToDate"  width="95px" CssClass="dateonly" ></asp:TextBox>
        </td>
        <td style="text-align:center">
                <asp:DropDownList ID="ddlTrainingLocation" DataSource="<%#BindTrainingInstitute()%>" DataTextField="InstituteName" DataValueField="InstituteId" runat="server"  width="120px" CssClass="required_box"></asp:DropDownList>
        </td>
        <td>
            <asp:FileUpload ID="fuAttachment" runat="server" width="130px" />
        </td>
    </tr>
</ItemTemplate>
<FooterTemplate>
    </tbody>
    </table>
    </div>
</FooterTemplate>
</asp:Repeater>
</div>
    <div style="padding:4px;text-align:center;" >
        &nbsp;<asp:Label runat="server" id="lblMsg11" style="color:Red;font-size:15px;"></asp:Label>
    </div>
<div style="padding:4px;">
    
<asp:Button ID="btnSaveClosure" runat="server" Text="Save" OnClick="btnSaveClosure_OnClick" CssClass="newbtn" />

</div>
</div>
<div class="modal-footer">

</div>
</div>
</div>

    <%--------------------------------------------------------------------------------------------------------------------------------%>



                <%--------------------------------------------------------------------------------------------------------------------------------%>
                <div id="myModel2" class="modal" runat="server" visible="false" >
        <!-- Modal content -->
        <div class="modal-content" style="width:30%">
    <div class="modal-header">
      <%--<span class="close" onclick="CloseModelMatrix()">&times;</span>--%>

        <asp:Button ID="btnCloseMyModel2" runat="server" Text="X" CssClass="close closeButton" OnClick="btnCloseMyModel2_OnClick" />
      <h2>Update Planned Date</h2>
    </div>
    <div class="modal-body">
        <div style="padding:50px;">
            Planned For : <asp:TextBox ID="txtPlanDue" runat="server" CssClass="required_box" MaxLength="20" TabIndex="3" Width="90px"></asp:TextBox>
            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopLeft" TargetControlID="txtPlanDue"></ajaxToolkit:CalendarExtender>

            <br />
            <br />
            <asp:Button ID="btnSavePlanDue" runat="server" Text="Save" OnClick="btnSavePlanDue_OnClick" CssClass="newbtn" />
            <div>
                <asp:Label ID="lblMsgUpdateDueDate" runat="server" style="color:red;" ></asp:Label>
            </div>
            
        </div>
    </div>
    <div class="modal-footer">
      
    </div>
  </div>
    </div>

                </div>

                
                <%--</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveClosure" />
            </Triggers>
                </asp:UpdatePanel>--%>
            
    </div>

    <div style="padding:6px;">
        <div class="newbtn"  ><i class="fa fa-ship"></i> 
            <asp:Button ID="btnExportToShip" runat="server" Text="Export To Ship" CssClass="newbtn" OnClick="btnExportToShip_OnClick" /> &nbsp;
        </div>
    </div>
    
    
    
    <%--------------------------------------------------------------------------------------------------------------------------------%>


    <div>
    <asp:Literal runat="server" ID="litTreaining" Visible="false"></asp:Literal>
    </div>
        
        <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
        <%--Model Script--%>
        <script type="text/javascript">
            function assignTraining() {
                $("#myModal").show();
            }
            function CloseModel() {
                $("#myModal").hide();
                //process_selectedCrew();
            }

            function importFromMatrix() {
                $("#myModal1").show();
                var vesselid =<%=VesselId %>;
                LoadTrainingFromMatrix(selCrewid, vesselid);
            }
            function CloseModelMatrix() {
                $("#myModal1").hide();
                
            }
            function SetCalender() {
                $('#duedate,.dateonly').datetimepicker({ datepicker: true, timepicker: false, allowBlank: true, defaultSelect: false, validateOnBlur: false, format: 'd-M-Y', formatDate: 'd-M-Y' });
            }
            SetCalender();
            
        </script>
        <script type="text/javascript">
            var selrow = ''
            var selCrewid=0
            var selRankGroupid = 0;
            var selCrewNumber = ''
            var CrselewName = ''
            var selRankCode=''
            function showcrewlist()
            {
                
            }
            function clickrow(crewid, rankGroupid, CrewNumber, CrewName, RankCode, ctrl) {
                $(ctrl).addClass("selrow");
                $(selrow).removeClass("selrow");
                selectcrew(crewid, rankGroupid, CrewNumber, CrewName, RankCode);

                selrow = ctrl;
            }
            function selectcrew(crewid, rankGroupid,CrewNumber,CrewName,RankCode)
            {
                selCrewid = crewid;
                selRankGroupid = rankGroupid;
                selCrewNumber = CrewNumber;
                CrselewName = CrewName;
                selRankCode = RankCode;
                $(".slidecrew").css("visibility", "");
                
                process_selectedCrew();
            }
            function process_selectedCrew() {
                $("#selectedCrewNumber").html(selCrewNumber);
                $("#selectedUsername").html(CrselewName);
                $("#selectedRankCode").html(" [ " + selRankCode + " ] ");
                $("#dvtrainings").html("<img src='../Images/loading1.gif' style='margin-top:50px;'>");

                var vesselid =<%=VesselId %>;
                
                LoadTraining(selCrewid, selRankGroupid, vesselid);

            }


            function LoadTraining(Crewid, RankGroupid, Vesselid) {

                $.ajax({
                    url: "./ManageTraining1.aspx",
                    method: "POST",
                    type: "POST",
                    contentType: "application/x-www-form-urlencoded",
                    data: { crewid: Crewid, rankgroupid: RankGroupid, vesselid: Vesselid,LT:1 },
                    dataType: "html",
                    success: function (result) {
                        $("#dvtrainings").html(result);
                    },
                    error: function (result) {

                    },
                    complete: function (result) {

                    }
                });
            }

            
            function CancelTraining()
            {
                if (!confirm('Are you sure to delete?')) {
                    return;
                }

                var TRIDs ='';
                var vesselid =<%=VesselId %>;
                var checkedTrainings = $("#dvtrainings").find("input[type=checkbox]:checked");
                $(checkedTrainings).each(function (i, o) {
                    var TRID = $(o).attr("TRID");                    
                    TRIDs = TRIDs + ',' + TRID;
                })
                
                $.ajax({
                    url: "ManageTraining1.aspx/DeleteTraining",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ trids: TRIDs, vslid: vesselid}),
                    type: "POST",
                    success: function (data) {
                        if (data.d == "ok")
                        {
                            alert("Data deleted successfully");
                            //selectcrew(selCrewid, selRankid);                            
                            selectcrew(selCrewid, selRankGroupid, selCrewNumber, CrselewName, selRankCode);
                        }
                        else
                        {
                        }

                        
                    }
                });
                
            }
            function selectTraining(ctrl) {
                var x = $(ctrl).find("input[type=checkbox]:first")                
                x.prop('checked', x.prop('checked') ? false : true);
            }

            function LoadTrainingFromMatrix(Crewid, Vesselid, selRankGroupid) {

                $.ajax({
                    url: "./ManageTraining1.aspx",
                    method: "POST",
                    type: "POST",
                    contentType: "application/x-www-form-urlencoded",
                    data: { crewid: Crewid, vesselid: Vesselid, rankgroupid: selRankGroupid, TM: 1 },
                    dataType: "html",
                    success: function (result) {
                        $("#dvtrainingsFromMatrix").html(result);
                    },
                    error: function (result) {

                    },
                    complete: function (result) {

                    }
                });
            }
            function save_ImportTrainingFromMatrix() {
                
                var TRIDs = '';
                var VSLID =<%=VesselId %>;
                var Loginid =<%=Session["loginid"].ToString() %>;
                var checkedTrainings = $("#dvtrainingsFromMatrix").find("input[type=checkbox]:checked");
                $(checkedTrainings).each(function (i, o) {
                    var TRID = $(o).attr("TRID");                    
                    TRIDs = TRIDs + ',' + TRID;
                })

                if (TRIDs.trim() == "") {
                    alert('Please select any trainig');
                    return;
                }



                var dueDate = $("#duedate").val();
                if (dueDate.trim() == "") {
                    alert('Please enter due date');
                    return;
                }
                
                $.ajax({
                    url: "ManageTraining1.aspx/ImportTraining",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ trids: TRIDs, vslid: VSLID, crewid: selCrewid, duedate: dueDate, loginid: Loginid }),
                    type: "POST",
                    success: function (data) {
                        if (data.d == "ok") {
                            alert("Data imported successfully.");                            
                            selectcrew(selCrewid, selRankid, selCrewNumber, CrselewName, selRankCode);
                        }
                        else {
                        }


                    }
                });

            }
        </script>
    </form>
</body>
</html>

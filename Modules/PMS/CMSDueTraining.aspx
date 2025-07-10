<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSDueTraining.aspx.cs" Inherits="CMSDueTraining" EnableEventValidation="false" EnableViewStateMac="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>eMANAGER-HRD</title>
    <script src="./eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="./eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="./CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="./eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="./eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="./eReports/css/jquery.datetimepicker.css"/>


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
            margin-left:450px;            
        }
        .slidecrewlist {
            position:absolute;top:0px;left:0px;width:450px;
            border-right:solid 2px  #c2c2c2;
        }
        .closeButton {
            background: none;
            border: none;
        }
        h2 {
    display: block;
    font-size: 1.5em;
    -webkit-margin-before: 0.83em;
    -webkit-margin-after: 0.83em;
    -webkit-margin-start: 0px;
    -webkit-margin-end: 0px;
    font-weight: bold;
}
/* The Close Button */
.close {
    color: white;
    float: right;
    font-size: 28px;
    font-weight: bold;
}

    .close:hover,
    .close:focus {
        color: #000;
        text-decoration: none;
        cursor: pointer;
    }

.modal-header {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}

.modal-body {
    padding: 2px 16px;
}

.modal-footer {
    padding: 2px 16px;
    background-color: #5cb85c;
    color: white;
}


.modal {
     /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 1111; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
    position: relative;
    background-color: #fefefe;
    margin: auto;
    padding: 0;
    border: 1px solid #888;
    width: 80%;
    box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}
    </style>
</head>
<body style="margin-left:0px;"> 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div style="text-align:center;background-color:black;color:white;border-bottom:solid 2px black;padding-top:10px;">
            <span style="font-size:20px;font-weight:bold;">Due Trainings</span>
            <div style="padding:5px;font-size:14px;">Vessel : <span id="lblVesselName" runat="server"></span> as on <%=DateTime.Today.Date.ToString("dd-MMM-yyyy") %></div>
    </div>

    
               
    
                            
    
    <div id="container" class="container" style="position:relative;height:565px;">
            
        <%--<asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
            <div class="slide " style="padding-top:1px;padding-bottom:5px;" id="divTrainings" runat="server" >
                <div style="text-align:center;padding:10px 15px;font-size:16px;font-weight:bold;">
                    <asp:label id="lblselectedUsername" runat="server"></asp:label>   : 
                    <asp:label id="lblselectedCrewNumber" runat="server"></asp:label>
                    <asp:label id="lblselectedRankCode" runat="server" style="color:#00beff"></asp:label>
                    <asp:label id="lblSignOnDate" runat="server" ToolTip="Sign On Date, Relief Due Date" style="color:#00beff"></asp:label>
                </div>
                <div style="text-align:right;padding:5px 15px;">
                    <div style="">
                    <div class="newbtn" >
                        <asp:Button ID="btnCloseDue" runat="server" Text="Update Training" CssClass="newbtn" OnClick="btnCloseDue_OnClick" Width="100px"/> &nbsp;
                    </div>
                </div>
                
                </div>
                <div id="dvdue" runat="server" >
                    
                <div id="dvtrainings" style="background-color:white;height:500px;overflow:hidden;"  >
                    <asp:Repeater ID="rptTrainingsDue" runat="server">
            <HeaderTemplate>
                <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                <col width="30px" />
                <col width="50px" />
                <col />
                <%--<col width="100px" />--%>
                <%--<col width="150px" />--%>
                <col width="130px" />
                <col width="100px" />
                <col width="100px" />
                <col width="20px" />          
                    <thead>
                <tr>
                    <th>
                        <asp:CheckBox ID="chkSelAll" runat="server"  AutoPostBack="true" OnCheckedChanged="chkSelAll_OnCheckedChanged"/>                        
                    </th>
                    <th>Sr#</th>
                    <th style="text-align:left;">Training Name</th>
                    <%--<th style="text-align:center">Last Done</th>--%>
                    <th>Source</th>
                    <th style="text-align:center">Next Due</th>
                    <th style="text-align:center">Plan Date</th>
                    <th style="text-align:center">&nbsp;</th>
                                            
                </tr>
                        </thead>
            </table>        
                </div>
                <div id="divTrainingDueContent" style="width:100%;overflow-x:hidden;overflow-y:scroll;height:523px;" class="ScrollAutoReset">
                <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                    <col width="30px" />
                <col width="50px" />
                <col />
                <%--<col width="100px" />--%>
                <%--<col width="150px" />--%>
                <col width="130px" />
<col width="100px" />
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
                    <%--<td style="text-align:center"><%#Common.ToDateString( Eval("LastDone")) %></td>--%>
                    <td> 
                        <%#Eval("Source") %> 
                        <%--<span style="color:red;"> <%# ((Eval("Source").ToString()=="MATRIX")? Eval("MatrixCompatibility").ToString():"") %></span>--%>

                    </td>
                    <td style="text-align:center"><%#Common.ToDateString(Eval("N_DueDate")) %></td>
                    <td style="text-align:center">
                        <%#Common.ToDateString(Eval("PlannedFor")) %>

                    </td>
                    <%--<td style="text-align:center">
                        <asp:ImageButton ID="imgDelteTraining" runat="server" OnClick="btnDeleteTraining_OnClick" CommandArgument='<%#Eval("TrainingRequirementId")%>' ImageUrl="~/Modules/PMS/Images/delete_12.gif" Visible='<%# (Eval("Source").ToString()!="PEAP") %>' OnClientClick="return confirm('Are you sure to delete?');"/>

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
    <thead>
    <tr>
        <th style="text-align:left;">Training Name</th>
        <th style="text-align:center">Source</th>
        <th style="text-align:center">Last Done</th>
        <th style="text-align:center">Due Date</th>                    
        <th style="text-align:center">Planned For </th>
        <th style="text-align:center">From Date  </th>
        <th style="text-align:center">To Date</th>        
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
        <tbody>
</HeaderTemplate>
<ItemTemplate>
    <tr>
        <td style="text-align:left;"><asp:HiddenField ID="hfdTrainingRequirementId" runat="server" Value='<%#Eval("TrainingRequirementId")%>' /><%#Eval("TrainingName") %></td>
        <td style="text-align:center"><%#Eval("Source")%></td>
        <td style="text-align:center"><%#Common.ToDateString( Eval("LastDone")) %></td>
        <td style="text-align:center"><%#Common.ToDateString(Eval("n_duedate")) %></td>        
        <td style="text-align:center"></td>
        <td style="text-align:center">
            <asp:TextBox runat="server" id="txtFromDate" width="95px" CssClass="date_only" ></asp:TextBox>
        </td>
        <td style="text-align:center">
                <asp:TextBox runat="server" id="txtToDate"  width="95px" CssClass="date_only" ></asp:TextBox>
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
<div style="padding:10px;text-align:center;">
    <asp:Label runat="server" id="lblMsg11" style="float:left;color:Red;font-size:15px;"></asp:Label>
<asp:Button ID="btnSaveClosure" runat="server" Text="Save" OnClick="btnSaveClosure_OnClick" CssClass="newbtn" />

</div>
</div>
<div class="modal-footer">

</div>
</div>
</div>

                <%--</ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSaveClosure" />
            </Triggers>
                </asp:UpdatePanel>--%>
            
    </div>

    
    
    <%--------------------------------------------------------------------------------------------------------------------------------%>


    <div>
    
    </div>
        
        <script type="text/javascript">
            function assignTraining() {
                $("#myModal").show();
            }
            function CloseModel() {
                $("#myModal").hide();
                
            }
            
        </script>
        
        <script type="text/javascript" src="./eReports/js/jquery.datetimepicker.js"></script>
        <script type="text/javascript">
            function SetCalender() {
                $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
            }
            SetCalender();
        </script>
    </form>
</body>
</html>

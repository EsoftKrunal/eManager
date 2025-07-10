<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMSDoneTraining.aspx.cs" Inherits="CMSDoneTraining" EnableEventValidation="false" EnableViewStateMac="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="./eReports/JS/jquery.min.js" type="text/javascript"></script>
    <script src="./eReports/JS/KPIScript.js" type="text/javascript"></script>

    <link href="./CSS/tabs.css" rel="stylesheet" type="text/css" />

    <link href="./eReports/CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="./eReports/CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="./css/jquery.datetimepicker.css"/>


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
            <span style="font-size:20px;font-weight:bold;">Trainings Completed</span>
    </div>
    <div>
        <asp:Repeater ID="rptcrew" runat="server">
            <ItemTemplate>
                <div style="padding:5px; background-color:#e2e2e2;font-weight:bold;">
                    <%#Eval("crewnumber")%> : <%#Eval("crewname")%>
                </div>
           
            <asp:Repeater ID="rptTrainingsDone" runat="server" DataSource='<%#BindTrainings(Eval("crewid"))%>'>
                    <HeaderTemplate>
                    <%--<div style="width:100%;overflow-x:hidden;overflow-y:scroll;">--%>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                    <col width="50px" />
                    <col />                    
                    <col width="100px" />
                    <col width="100px" />
                    <col width="100px" />
                    <col width="35px" />        
                    <col width="35px" /> 
                    <thead>
                    <tr>
                        <th style="text-align:center">Sr#</th>
                        <th style="text-align:left;">Training Name</th>
                        <th style="text-align:center">Source</th>
                        <th style="text-align:center">From Due</th>
                        <th style="text-align:center">To Date</th>
                        <th style="text-align:center"></th>     
                        <th style="text-align:center"></th>   
                    </tr>
                    </thead>
                    </table> 
                   <%--</div>--%>
                    <%--<div id="divTrainingDueContent1" style="width:100%;overflow-x:hidden;overflow-y:scroll;height:523px;" class="ScrollAutoReset">--%>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                            <col width="50px" />
                            <col />
                            <col width="100px" />
                            <col width="100px" />
                            <col width="100px" />                               
                            <col width="35px" /> 
                            <col width="35px" /> 
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr >
                            <td style="text-align:center"><%#Eval("SNO") %></td>
                            <td style="text-align:left;"><%#Eval("TrainingName") %></td>
                            <td><%#Eval("Source") %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("FromDate")) %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("ToDate")) %></td>                                            
                            <td>
                                <asp:ImageButton ID="btndownload" runat="server" ImageUrl="~/Modules/PMS/Images/paperclip.gif" OnClick="btndownload_OnClick" CommandArgument='<%#Eval("TrainingRequirementId") %>' Visible='<%# (Common.CastAsInt32( Eval("hasFile"))>0) %>' />                                
                            </td>
                            <td>
                                    <asp:Image runat="server" ImageUrl="~/Modules/PMS/Images/warning.gif"  Visible='<%#( Convert.ToString(Eval("OfficeRecdOn"))=="") %>' style="height:15px;" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                        </div>
                    </FooterTemplate>
                    </asp:Repeater>   
    
         </ItemTemplate>
            </asp:Repeater>                    
    <%--</div>--%>
    
    
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingCounterDetails.aspx.cs" Inherits="CrewOperation_TrainingCounterDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trainings</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="~/Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="~/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />    
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
    </style>
</head>
<body>
<form id="form1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align:center;background-color:black;color:white;border-bottom:solid 2px black;padding-top:10px;padding-bottom:10px;">
            <span style="font-size:20px;font-weight:bold;">
                <asp:Literal ID="litPageHeading" runat="server"></asp:Literal>
            </span>
            
    </div>
    <div style="padding:5px;text-align:right;padding-right:25px;">
        <asp:Label id="TrainingCounter" runat="server"></asp:Label>
    </div>
    <div id="divVesselList" runat="server" >
    <div  style="overflow-x:hidden;overflow-y:scroll;" > 
        <asp:Repeater ID="rptTrainingsDone" runat="server">
                    <HeaderTemplate>
                    <div style="width:100%;overflow-x:hidden;overflow-y:scroll;">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                    <col width="50px" />
                    <col />                    
                    <col width="300px" />
				<col width="100px" />
                    <col width="100px" />
                    <col width="100px" />
                    <col width="200px" />           
                        <col width="30px" />        
                    <thead>
                    <tr>
                        <th>Sr#</th>
                        <th style="text-align:left;">Training Name</th>
			<th>Crew</th>
                        <th>Source</th>
                        <th style="text-align:center">From Due</th>
                        <th style="text-align:center">To Date</th>
                        <th style="text-align:left">Institute Name</th>                                            
                        <th style="text-align:center"></th>     
                    </tr>
                    </thead>
                    </table> 
                   </div>
                    <div id="divTrainingDueContent1" style="width:100%;overflow-x:hidden;overflow-y:scroll;height:523px;" class="ScrollAutoReset">
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                            <col width="50px" />
                            <col />
                            <col width="300px" />
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
			<td style="text-align:left;"><%#Eval("crewnumber") %>-<%#Eval("crewname") %></td>
                            <td><%#Eval("SourceName") %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("FromDate")) %></td>
                            <td style="text-align:center"><%#Common.ToDateString(Eval("ToDate")) %></td>                                            
                            <td style="text-align:left"><%#Eval("institutename")%></td>
                            <td>
                                <%--<asp:ImageButton ID="btndownload" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btndownload_OnClick" CommandArgument='<%#Eval("TrainingRequirementId") %>' Visible='<%# (Common.CastAsInt32( Eval("HasFile"))>0) %>' />--%>
                                
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

    
    </form>
</body>
</html>

<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="TrainingMatrixDetails.aspx.cs" Inherits="Emtm_TrainingMatrixDetails"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
     
 <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
 <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #c2c2c2;
            padding:5px;
        }
        .group
        {
            color:maroon;
            font-size:15px;
            font-weight:bold;
            padding:5px;
        }
        .tgroup
        {
            width:55px;
            background-color:#c2c2c2;
        }
        .tname
        {
            width:320px;
            
        }
        .interval
        {
            width:60px;
            text-align:center;
        }
        .posgroup
        {
            width:100px;
            overflow-wrap:break-word;
            word-break:break-all;
        }
        .posgroup:hover
        {
            background-color:#feeabb;
            cursor:pointer;
        }
        .assigned {
            background-color: #4da312;
        }
        
        .header
        {
            background-color:rgba(181, 218, 253, 1);
            font-weight:bold;
            font-size:12px;
        }
        .data
        {
            font-size:12px;
        }
        
    </style>
</head>
<body style="margin:0px;font-family:Arial;font-size:15px;" >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
        <div style="padding:7px;text-align:center;font-size:20px;font-weight:bold;background-color:#4371a5;color:white;">            
            Training Details
        </div>

        <table width='100%' cellpadding='5' cellspacing='0' border='1' style='border-collapse:collapse' class='bordered1'>
            <col width="150px" />
            <col />
            <tr>
                <td>Training Name :</td>
                <td>
                    <b><asp:Label ID="lblTrainigName" runat="server"></asp:Label></b> 
                </td>
            </tr>
            <tr>
                <td>Group Name :</td>
                <td>
                    <b> <asp:Label ID="lblGroupName" runat="server"></asp:Label></b> 
                </td>
            </tr>
            <tr>
                <td>Office :</td>
                <td>
                    <asp:DropDownList ID="ddlOffice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <table width="100%">
                    <tr>
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        
                        
                        <div style="height:25px; overflow-y:scroll; overflow-x:hidden">
                            <table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse' class='bordered'>
                                <col width="80px" />
                                <col  />
                                <col width="90px" />
                                <col width="120px" />
                                <col width="200px" />
                                <col width="60px" />
                                <col width="100px" />
                                <col width="100px" />
                                <tr align="left" class="">
                                    <td class="header">Emp Code</td>
                                    <td class="header">Name</td>
                                    <td class="header" style="text-align:center;">DJC</td>
                                    <td class="header">Office</td>
                                    <td class="header">Position</td>
                                    <td class="header" style="text-align:center;">Validity</td>
                                    <td class="header" style="text-align:center;">Last done dt.</td>
                                    <td class="header" style="text-align:center;">Next due dt.</td>
                                </tr>
                            </table>
                        </div>
                        <div style="height:480px; overflow-y:scroll; overflow-x:hidden">
                            <table width='100%' cellpadding='0' cellspacing='0' border='0' style='border-collapse:collapse' class='bordered'>
                                <colgroup>
                                <col width="80px" />
                                <col  />
                                <col width="90px" />
                                <col width="120px" />
                                <col width="200px" />
                                <col width="60px" />
                                <col width="100px" />
                                <col width="100px" />
                                    </colgroup>
                                <asp:Repeater ID="rptDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="data" style="text-align:center;"> <%#Eval("empcode") %> </td>
                                            <td class="data"> <%#Eval("EmpName") %> </td>
                                            <td class="data" style="text-align:center;"> <%#Common.ToDateString( Eval("djc")) %> </td>
                                            <td class="data"> <%#Eval("OfficeName") %> </td>
                                            <td class="data"> <%#Eval("PositionName") %> </td>
                                            <td class="data" style="text-align:right;"> <%#Eval("Validity") %> </td>
                                            <td class="data" style="text-align:center;"> <%#Common.ToDateString( Eval("LastDoneDt")) %> </td>
                                            <td class="data" style="text-align:center;"> <%#Common.ToDateString( Eval("NextDueDt")) %> </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                             </table>
                        </div>  
                        
                        </td>
                        </tr>
                </table>
    
    
   
      
    </form>
</body>
 </html>
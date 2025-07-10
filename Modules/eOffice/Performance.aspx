<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Inherits="emtm_Emtm_Performance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emtm > Performance</title>
    <style type="text/css">
    .leftpane
    {
        float:left; 
        width:400px; 
        overflow:hidden; 
     }
     .rightpane
     {
        text-align:left; 
        margin-left:400px; 
        overflow:auto; 
        white-space:nowrap; 
        width:600px;
     }
     .row
     {
         height:15px;
         vertical-align:top;
         border-bottom:dotted 1px grey;
         overflow:visible;
         display:table-row;
         text-align:left;
     }
     .row span
     {
          display:inline-block;
          height:18px;
          -webkit-box-sizing: border-box;
          -moz-box-sizing: border-box;
          box-sizing: border-box;
          border-left:dotted 1px grey;
          border-right:solid 1px white;
          border-top:solid 1px white;
          border-bottom:solid 1px white;
     }
     
    .rightpane .row
    {
        
    }

     .setleft
     {
         margin-left:-3px;
     }
     .center
     {
         text-align:center;
     }
     .success
     {
         background-color:#33CC33;
     }
     .error
     {
         background-color:#FF9980;
     }
     .week 
     {width:40px; text-align:center; }
     .month 
     {width:166px; text-align:center;}
     .month2 
     {width:334px; text-align:center;}
     .days15 
     {width:82px; text-align:center; }
    </style>
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#rightpane1").scroll(function () {
                $("#leftpane1").scrollTop($("#rightpane1").scrollTop());
                $("#toprightpane1").scrollLeft($("#rightpane1").scrollLeft());
            });

            var docWidth = document.documentElement.clientWidth || document.body.clientWidth;
            $(".rightpane").width(docWidth - 400 + 'px');
        });
    </script>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <div>
          <table width="100%" cellpadding="0" cellspacing="0" border="0">
          <tr>
          <td style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :6px; font-weight: bold;">
            My Performance : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="14px"></asp:Label>
          </td>
          </tr>
           <tr>
          <td style=" text-align :center; font-size :14px; color :#555555; padding :6px; font-weight: bold;">
            <table width="100%" cellpadding="3" cellspacing="0" border="0">
            <tr>
            <%--<td style="text-align: right"> Office : </td>
            <td style="text-align: left"><asp:DropDownList runat="server" Width="200px" ID="ddlOffice" DataTextField="OFFICENAME" DataValueField="OFFICEID" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td style="text-align: right"> Department : </td>
            <td style="text-align: left"><asp:DropDownList runat="server" Width="200px" ID="ddlDept"  DataTextField="DEPTNAME" DataValueField="DEPTID" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_OnSelectedIndexChanged"></asp:DropDownList></td>--%>

            <td style="text-align: right"> Vessel Position : </td>
            <td style="text-align: left">
            <asp:DropDownList runat="server" Width="200px" ID="ddlVPos" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged">
                <asp:ListItem Text=" < ALL > " Value="0"></asp:ListItem>
                <asp:ListItem Text=" Fleet Manager " Value="FLEETMANAGER"></asp:ListItem>
                <asp:ListItem Text=" Technical Suptd. " Value="TECHSUPDT"></asp:ListItem>
                <asp:ListItem Text=" Marine Suptd. " Value="MARINESUPDT"></asp:ListItem>
                <asp:ListItem Text=" Technical Assst. " Value="TECHASSISTANT"></asp:ListItem>
                <asp:ListItem Text=" Marine Assst. " Value="MARINEASSISTANT"></asp:ListItem>
                <asp:ListItem Text=" SPA " Value="SPA"></asp:ListItem>
                <asp:ListItem Text=" Account Officer " Value="ACCTOFFICER"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td style="text-align: right"> Emp Name : </td>
            <td style="text-align: left"><asp:DropDownList runat="server" Width="400px" ID="ddlEmp"  DataTextField="EMPNAME" DataValueField="USERID" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_OnSelectedIndexChanged"></asp:DropDownList></td>
            <td>
            <asp:Button runat="server" ID="btnPrint" Text="Print Vessel Compliance" OnClick="btnPrint_Click" />
            </td>
            </tr>
            </table>
            
          </td>
          </tr>
          <tr>
          <td>
          <div style="background-color:#d2d2d2; font-size:14px;overflow-x:hidden;overflow-y:scroll; ">
          <table width="100%" cellpadding="5" cellspacing="0" border="0" bordercolor="white">
          <tr>
          <td>Reports</td>
          <td style="width:150px; text-align:center;">Target</td>
          <td style="width:150px; text-align:center;">Compliance</td>
          <td style="width:150px; text-align:center;">Non-Compliance</td>
          <td style="width:30px;" >&nbsp;</td>
          </tr>
          </table> 
          </div>
          <div style="overflow-x:hidden; overflow-y:scroll; height:320px" >
          <table width="100%" cellpadding="5" cellspacing="0" border="1 " style="border-collapse:collapse">
               <asp:Repeater runat="server" ID="rptData">
               <ItemTemplate>
               <tr>
                  <td ><%#Eval("KPiname")%>-<span style='color:Blue;'><%#Eval("PERIODNAME")%></span></td>
                  <td style="width:150px; background-color:#eeeeee; text-align:center;">
                    <asp:LinkButton runat="server" OnClick="lnkKpi_DetailsClick" EmpId='<%#Eval("EmpId")%>' CommandArgument='<%#Eval("KPIID")%>' Text='<%#Eval("target")%>'></asp:LinkButton>
                  </td>
                  <td style="width:150px; background-color:#70DB70; text-align:center;"><%#Eval("Success")%></td>
                  <td style="width:150px; background-color:#FFCCCC; text-align:center;"><%#Eval("Error")%></td>
                  <td style="width:30px;" >&nbsp;</td>
               </tr>
               </ItemTemplate>
               </asp:Repeater>          
          </table>
          </div>
          </td>
          </tr>
          </table>
          </div>
    </div>
    </form>
</body>
</html>

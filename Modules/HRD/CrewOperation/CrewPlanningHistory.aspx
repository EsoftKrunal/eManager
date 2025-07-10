<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewPlanningHistory.aspx.cs" Inherits="CrewOperation_CrewPlanningHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .bordered tr td
        {
            border:solid 1px #e4d7d7;
        }
    </style>
</head>
<body style="margin:0px; text-align:center ; font-family:Calibri;">
    <form id="form1" runat="server">
    <div>
    <div style="padding:7px; background-color:#ffd800">
       <div style="color:black; font-size:25px;"> Crew Planning History </div>
        <hr />
        <b>
        Crew Name : <asp:Label runat="server" id="lblCrewDetails" ForeColor="#333"></asp:Label> , 
        Vessel Name : <asp:Label runat="server" id="lblVesselName" ForeColor="#333" ></asp:Label> 
            </b>
    </div>
        <table width="100%" class="bordered" cellspacing="0" cellpadding="2" style="border-collapse:collapse">
            <tr style="background-color:#535452;color:white;">
                <td style="min-width:350px" >Planned Crew Name</td>
                <td style="width:150px">Planned By</td>
                <td style="width:90px">PlannedOn</td>
                <td style="width:150px">Cancelled By</td>
                <td style="width:90px">Cancelled On</td>
                <td style="width:90px">App.Status</td>
                <td>ApprovedBy</td>
                <td style="width:90px">ApprovedOn</td>
            </tr>
            <asp:Repeater runat="server" ID="rptData">
                <ItemTemplate>
                <tr>
                    <td style="text-align:left;"><%#Eval("PlannedCrewName")%>
                        <div style="padding:3px;font-style:italic; color:blue;font-size:12px;">
                            <%#Eval("Remark")%>
                        </div>
                    </td>
                    <td  style="text-align:left;"><%#Eval("PlannedBy")%></td>
                    <td><%#Common.ToDateString(Eval("PlannedOn"))%></td>
                
                    <td><%#Eval("CancelledBy")%></td>
                    <td><%#Common.ToDateString(Eval("CancelledOn"))%></td>

                    <%--<td><%#Eval("Status")%></td>--%>
                    <td><%#Eval("AppStatus")%></td>
                    
                    <td style="text-align:left;"><%#Eval("ApprovedBy")%>
                         <div style="padding:3px;font-style:italic; color:blue;font-size:12px;">
                            <%#Eval("AppRemark")%>
                        </div>

                    </td>
                    <td><%#Common.ToDateString(Eval("ApprovedOn"))%></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>

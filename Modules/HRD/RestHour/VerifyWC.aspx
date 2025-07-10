<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyWC.aspx.cs" Inherits="VerifyWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="../stylesheet" type="text/css" href="Styles/StyleSheet.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Rest Hour Non-Conformities Verification</title>
    <style type="text/css">
    .data td
    {
    font-size:11px;
    }
    .header
    {
	    text-align :center; background-color :LightGray; height :20px; color:Black;
    }
    .header td
    {
	    font-size:12px;
    }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
     <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px" class="text" >
                    Rest Hour Non-Conformities Verification
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 23px">
                   <asp:Label runat="server" ID="lblRC"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
    <div>
        <table  cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse" >
            <%--<col width="50px" />--%>
            <col width="50px" />
            <col width="50px" />
            <col width="200px" />
            <col width="80px" />
            <col width="80px" />
            <col />
            <col width="80px" />
            <col width="320px" />
            <tr class="header">
                <%--<td></td>--%>
                <td>Vessel</td>
                <td>Crew#</td>
                <td>Name</td>
                <td>Rank</td>
                <td>Date</td>
                <td>NC Type</td>
                <td>Status</td>
                <td>NC Reason</td>
            </tr>
        </table>
        <div  style="overflow-x:hidden;overflow-y:scroll; height:405px; border-bottom:solid 1px gray;" >        
        <table cellpadding="2" cellspacing="0" border="1" width="100%" style=" border-collapse :collapse;" >
            <%--<col width="50px" />--%>
            <col width="50px" />
            <col width="50px" />
            <col width="200px" />
            <col width="80px" />
            <col width="80px" />
            <col />
            <col width="80px" />
            <col width="320px" />
            <asp:Repeater ID="rptVesselList" runat="server" >
                <ItemTemplate>
                    <tr class="data" >
                        <%--<td style="text-align:center;"><asp:CheckBox runat="server" ID="chkSel" /> </td>--%>
                        <td style="text-align:center;"><%#Eval("VESSELCode")%></td>
                        <td style="text-align:center;"><%#Eval("CrewNumber")%></td>
                        <td style="text-align:left;"><%#Eval("CrewName")%></td>
                        <td style="text-align:center;"><%#Eval("RankCode")%></td>
                        <td style="text-align:center;"><%#Common.ToDateString(Eval("NCDate"))%></td>
                        <td style="text-align:left;"><%#Eval("NCTypeName")%></td>
                        <td style="text-align:left;"><%#Eval("Status")%></td>
                        <td style="text-align:left;">
                        <%#Eval("ReasonName")%>
                        <%--<asp:DropDownList runat="server" ID="ddlReason" selectedvalue='<%#Eval("Reason")%>' Width="300px">
                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                            <asp:ListItem Text="At Sea Cargo operations" Value="1"></asp:ListItem>
                            <asp:ListItem Text="At Sea Navigation" Value="2"></asp:ListItem>
                            <asp:ListItem Text="At Sea Ship Maintenance" Value="3"></asp:ListItem>
                            <asp:ListItem Text="In Port Cargo Operations" Value="4"></asp:ListItem>
                            <asp:ListItem Text="In Port Ship Maintenance" Value="5"></asp:ListItem>
                            <asp:ListItem Text="In Port Maneuvering" Value="6"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:HiddenField runat="server" ID="hfdVessel" Value='<%#Eval("VESSELID")%>' />
                        <asp:HiddenField runat="server" ID="hfdCrew" Value='<%#Eval("CREWID")%>' />
                        <asp:HiddenField runat="server" ID="hfdNCDate" Value='<%#Common.ToDateString(Eval("NCDate"))%>' />
                        <asp:HiddenField runat="server" ID="hfdNCType" Value='<%#Eval("NCTYPE")%>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <div style="width:98%; padding:4px; background-color:#F7F8E0; margin:2px; border:solid 1px #F7D358; text-align:left;float:left"> <%#Eval("Remarks")%> </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            
        </table>
        </div>  
        <div style="padding:5px; text-align:left;display:none;">
        Verification Remark :<br />
        <div style="float:left;padding:5px;">
        <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="750px" Height="70px" CssClass="required_box"></asp:TextBox>
        <br />
        </div>
        <div style="float:left; padding:5px;">
        <asp:Button runat="server" ID="btnSave" Text="Save" Width="80px" CssClass="btn" onclick="btnSave_Click" Visible="false" /><br />
        <asp:Button runat="server" ID="btnVerify" Text="Verify" Width="80px" CssClass="btn" onclick="btnVerify_Click" /><br /><br />
        <asp:Label runat="server" ID="lblMessage" Font-Size="12px" ForeColor="Red" style="clear:both"></asp:Label>
        </div>
        </div>
</div>
        </td></tr>
        </table>
    </form>
</body>
</html>
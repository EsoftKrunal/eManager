<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePortCall.aspx.cs" Inherits="ManagePortCall" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
 <style type="text/css">
     .bordered tr td
     {
         border:solid 1px #ebe9e9;
     }
     .class_on {
     background-color:#f9fcbf;
     }
     .class_off {
     background-color:#ceffc9;
     }
 </style>
      <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
      <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />

</head>
<body style="background-color:white; margin:0px;padding:0px; font-family:Calibri;font-size:15px;">
    <div style="text-align:center;padding:4px; " class="text headerband">
        Port Call
    </div>
    <form id="form1" runat="server">
        <table cellpadding="3" cellspacing="0" width="100%" class="bordered" style="text-align:left; border-collapse:collapse;">
            <tr>
                <td>Country :</td>
                <td><asp:Label runat="server" ID="lblRefNo" Font-Bold="true"></asp:Label></td>
                <td>Country :</td>
                <td><asp:Label runat="server" ID="lblCountry" Font-Bold="true"></asp:Label></td>
                <td>Port :</td>
                <td><asp:Label runat="server" ID="lvlPort" Font-Bold="true"></asp:Label></td>
                <td>Vessel :</td>
                <td><asp:Label runat="server" ID="lblVSL" Font-Bold="true"></asp:Label></td>
                <td>Duration :</td>
                <td><asp:Label runat="server" ID="lblDuration" Font-Bold="true"></asp:Label></td>
                </tr>
            </table>
    <div style="overflow-x:hidden; overflow-y:scroll; height:400px;">
        <table cellpadding="3" cellspacing="0" width="100%" class="bordered" style="text-align:left; border-collapse:collapse;">
            <tr class="headerstylegrid">
                <td style="width:50px">Select</td>    
                <td style="width:50px">Type</td>
                <td style="width:50px">Crew#</td>
                <td style="width:50px">Crew Name</td>
                <td style="width:50px">Rank </td>
                <td style="width:50px">Nationality </td>
                <td style="width:50px">Relief Due Dt.</td>
                <td style="width:50px">Exp. Join Dt.</td>
            </tr>
        <asp:Repeater runat="server" ID="rptData">
            <ItemTemplate>
             <tr class='<%#((Eval("OnOff").ToString()=="1")?"class_on":"class_off")%>'>
                <td><asp:CheckBox runat="server" ID="chkSelect" CssClass='<%#Eval("crewid")%>' mode='<%#((Eval("OnOff").ToString()=="1")?"O":"I")%>' /></td>
                <td><%#((Eval("OnOff").ToString()=="1")?"SignOff":"SignOn")%></td>
                <td><%#Eval("CrewNumber")%></td>
                <td><%#Eval("CrewName")%></td>
                <td><%#Eval("RankName")%></td>
                <td><%#Eval("Nationality")%></td>
                <td><%#Eval("REliefDueDate")%></td>
                <td><%#Eval("ExpectedJoinDate")%></td>
            </tr>
                </ItemTemplate>
        </asp:Repeater>
        </table>
     
    </div>
        <div style="padding:3px;">
            <asp:Label runat="server" id="lblMessage" Font-Bold="true" Font-Size="Larger" ForeColor="Red" style="float:left;"></asp:Label>
            <asp:Button runat="server" ID="btnAddCrew" Text="Add Selected Crew" OnClick="btnAddCrew_Click" CssClass="btn"  />
        </div>
    </form>
</body>
</html>

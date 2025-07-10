<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCrewCheckList.aspx.cs" Inherits="Applicant_ViewCrewCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .rem
    {
        border:solid 1px #c2c2c2;
        font-size:11px;
    }
    .rem:focus
    {
        background-color:#FFFFCC;
    }
    body
    {
        font-family:Calibri; 
        font-size:15px;
    }
    h1
    {
        font-size:18px;
        background-color:#d2d2d2;
        padding:5px;
        margin:0px;
    }
     h2
    {
        font-size:17px;
        background-color:#d2d2d2;
        margin:0px;
    }
    .data
    {
        font-size:12px;
    }
    .dataheader
    {
        font-size:14px;
        background-color:#FFE0C2;
    }
    a img
    {
        border:none;
    }
    .newbtn
    {
        border:solid 1px #c2c2c2;
        background-color:Orange;
        padding:5px;
        width:100px;
        margin-top:2px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
         <div class="text headerband" > Document Checklist for Crew Joining </div> 
    <div style="text-align: center; width:90%; border:solid 1px black;">
       
        <table width='100%' style="text-align:left" >
        <colgroup>
        <col width='120px' />
        <col />
        <col width='120px' />
        <col />
        </colgroup>
        <tr>
            <td>Crew Number&nbsp; : </td>
            <td><asp:Label runat="server" ID="lblID"></asp:Label>  </td>
            <td>Crew Name : </td>
            <td>  <asp:Label runat="server" ID="lblName"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Nationality : </td>
            <td>  <asp:Label runat="server" ID="lblNationality"></asp:Label>  </td>
            <td>DOB :</td>
            <td>  <asp:Label runat="server" ID="lblDOB"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Planned Rank : </td>
            <td>  <asp:Label runat="server" ID="lblRank"></asp:Label>  </td>
            <td>Planned Vessel :</td>
            <td>  <asp:Label runat="server" ID="lblVName"></asp:Label>  </td>
        </tr>
         </table>
        <table width='100%' style="text-align:left; " cellspacing='0' cellpadding="3"  >
        <colgroup>
        <col width="400px" />
        <col width="100px" />
        <col width="80px" />
        <col width="80px" />
        <col width="20px" />
        <col width="80px" />
        <col />
        </colgroup>
        <tr class= "headerstylegrid" >
            <td>Document Name</td>
            <td>Document#</td>
            <td>Issue Dt.</td>
            <td>Expiry Dt.</td>
            <td><img src="../Images/paperclip12.gif" /></td>
            <td>Checked</td>
            <td>Remarks <i style="font-size:12px"> ( if any )</i></td>
        </tr>
        <tr class='dataheader'><td colspan="7">Licenses <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#Eval("DOCNO") %></td>
                <td><%#Common.ToDateString(Eval("ISSUEDATE")) %></td>
                <td><%#ExpireCheck(Eval("EXPIRYDATE"))%></td>
                <td><a href='<%#"../EMANAGERBLOB/HRD/Documents/Professional/" + Eval("FileName").ToString() %>' target="_blank" ><img src="../Images/paperclip12.gif" /></a></td>
                <td><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentNameId").ToString()%>' />
                <td><asp:TextBox runat="server" ID="txtRem" Width='98%' CssClass="rem" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="7">Course & Certificates <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater2">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#Eval("DOCNO") %></td>
                <td><%#Common.ToDateString(Eval("ISSUEDATE")) %></td>
                <td><%#ExpireCheck(Eval("EXPIRYDATE"))%></td>
                <td><a href='<%#"../EMANAGERBLOB/HRD/Documents/Professional/" + Eval("FileName").ToString() %>' target="_blank" ><img src="../Images/paperclip12.gif" /></a></td>
                <td><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentNameId").ToString()%>' />
                <td><asp:TextBox runat="server" ID="txtRem" Width='98%' CssClass="rem" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="7">Endorsements <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater3">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
               <td><%#Eval("DOCNO") %></td>
              <td><%#Common.ToDateString(Eval("ISSUEDATE")) %></td>
                <td><%#ExpireCheck(Eval("EXPIRYDATE"))%></td>
                <td><a href='<%#"../EMANAGERBLOB/HRD/Documents/Professional/" + Eval("FileName").ToString() %>' target="_blank" ><img src="../Images/paperclip12.gif" /></a></td>
                <td><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentNameId").ToString()%>' />
                <td><asp:TextBox runat="server" ID="txtRem" Width='98%' CssClass="rem" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="7">Travel Documents <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater4">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#Eval("DOCNO") %></td>
                 <td><%#Common.ToDateString(Eval("ISSUEDATE")) %></td>
                <td><%#ExpireCheck(Eval("EXPIRYDATE"))%></td>
                <td><a href='<%#"../EMANAGERBLOB/HRD/Documents/Travel/" + Eval("FileName").ToString() %>' target="_blank" ><img src="../Images/paperclip12.gif" /></a></td>
                <td><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentNameId").ToString()%>' />
                <td><asp:TextBox runat="server" ID="txtRem" Width='98%' CssClass="rem" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="7">Medical Documents <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater5">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#Eval("DOCNO") %></td>
                 <td><%#Common.ToDateString(Eval("ISSUEDATE")) %></td>
                <td><%#ExpireCheck(Eval("EXPIRYDATE"))%></td>
                <td><a href='<%#"../EMANAGERBLOB/HRD/Documents/Medical/" + Eval("FileName").ToString() %>' target="_blank" ><img src="../Images/paperclip12.gif" /></a></td>
                <td><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentNameId").ToString()%>' />
                <td><asp:TextBox runat="server" ID="txtRem" Width='98%' CssClass="rem" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    <div style="text-align: center; width:90%; padding:5px; text-align:right">
    <asp:Label runat="server" ID="lblMess" style="float:left" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Button runat="server" CssClass="btn" Text="Save" ID="btnSave" onclick="btnSave_Click" />
        <asp:Button runat="server" CssClass="btn" Text="Close" ID="btnClose" OnClientClick='window.close();' />
    </div>
    </center>
    </form>
</body>
</html>

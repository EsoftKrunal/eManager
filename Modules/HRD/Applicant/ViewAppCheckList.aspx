<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAppCheckList.aspx.cs" Inherits="Applicant_ViewAppCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Checklist</title>
    <style type="text/css">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div style="text-align: center; width:90%; border:solid 1px black;">
        <h1>Applicant Document Checklist </h1>
        <table width='100%' style="text-align:left" >
        <colgroup>
        <col width='120px' />
        <col />
        <col width='120px' />
        <col />
        </colgroup>
        <tr>
            <td>Applicant Id : </td>
            <td><asp:Label runat="server" ID="lblID"></asp:Label>  </td>
            <td>Applicant Name : </td>
            <td>  <asp:Label runat="server" ID="lblName"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Rank : </td>
            <td>  <asp:Label runat="server" ID="lblRank"></asp:Label>  </td>
            <td>Nationality : </td>
            <td>  <asp:Label runat="server" ID="lblNationality"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Passport No : </td>
            <td>  <asp:Label runat="server" ID="lblPPNo"></asp:Label>  </td>
            <td>DOB :</td>
            <td>  <asp:Label runat="server" ID="lblDOB"></asp:Label>  </td>
        </tr>
        </table>
        <table width='100%' style="text-align:left; " cellspacing='0' cellpadding="3"  >
        <colgroup>
        <col width="400px" />
        <col width="60px" />
        <col />
        </colgroup>
        <tr style=" background-color:#E6B88A" >
            <td>Document Name</td>
            <td>Checked</td>
            <td>Remarks <i style="font-size:12px"> ( if any )</i></td>
        </tr>
        <tr class='dataheader'><td colspan="3">Licenses <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater1">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#(Eval("STATUS").ToString()=="Y")?"Yes":""%></td>
                <td><I><%#Eval("Remark") %></I></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="3">Course & Certificates <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater2">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#(Eval("STATUS").ToString()=="Y")?"Yes":""%></td>
                <td><I><%#Eval("Remark") %></I></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="3">Endorsements <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater3">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#(Eval("STATUS").ToString()=="Y")?"Yes":""%></td>
                <td><I><%#Eval("Remark") %></I></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="3">Travel Documents <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater4">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#(Eval("STATUS").ToString()=="Y")?"Yes":""%></td>
                <td><I><%#Eval("Remark") %></I></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr class='dataheader'><td colspan="3">Medical Documents <span style="font-size:9px;">▼</span></td></tr>
        <asp:Repeater runat="server" ID="Repeater5">
        <ItemTemplate>
            <tr class="data" >
                <td>●&nbsp;<%#Eval("DocumentName") %></td>
                <td><%#(Eval("STATUS").ToString()=="Y")?"Yes":""%></td>
                <td><I><%#Eval("Remark") %></I></td>
            </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </center>
    </form>
</body>
</html>

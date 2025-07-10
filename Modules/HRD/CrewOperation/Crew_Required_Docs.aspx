<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_Required_Docs.aspx.cs" Inherits="CrewOperation_Crew_Required_Docs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>eMANAGER-HRD</title>
    <style type="text/css">
    body
    {
         font-family:Calibri;
         font-size:13px;
    }
     .selecteddiv
    {
    	width:100%; padding:5px; padding-bottom :5px;background-color:wheat;border:dotted 1px #4371a5;
    }
    .normaldiv
    {
    	width:100%; padding:5px; padding-bottom :5px;background-color:none;
    }
    .rem
    {
        border:solid 1px #c2c2c2;
        width:800px;
    }
    .rem:focus
    {
        background-color:#FFFFCC;
    }
    .c2+div:after
    {
        content:"*";
        font-size:small; 
        color:red; 
    }
    .c1
    {
    width:400px;
    float:left;
    }
    .c2
    {
    width:80px;
    float:left;    
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
    <div>
    <div style="border:solid 10px #E0E0FF; height:660px;">
            <div style=' font-size:18px; font-weight:bold; color:White; background-color:#8282A3; padding:5px;'>&nbsp;Document Check List</div>
            <div style="font-size:14px; color:Blue; font-family:Verdana; padding:2px; background-color:#EDEDF5; border:solid 1px gray; text-align:center">
            <asp:Label runat="server" ID="lblheader" Width="33%"></asp:Label>
            <asp:Label runat="server" ID="lblheader1" Width="33%"></asp:Label>
            <asp:Label runat="server" ID="lblheader2" Width="33%"></asp:Label>
            </div>
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5; display:inline; text-align:left; font-weight:bold;">
                <div style="float:left; width:400px">&nbsp;Document Name</div>
                <div style="float:left; width:80px">Checked</div>
                <div style="float:left">Remarks</div>
            </div>
            <div style=" text-align:left ; height: 538px; overflow-x:hidden; overflow-y:scroll; border:solid 1px #c2c2c2">
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5">&nbsp;Licenses</div>
            <asp:Repeater runat="server" ID="rptL">
            <ItemTemplate>
            <div style="text-align:left; width:100%; text-align:left;padding:2px;">
                <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                <div class='c2'><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                <div><asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
             </div>
            </ItemTemplate>
            </asp:Repeater>
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5">&nbsp;Course & Certificates</div>
            <asp:Repeater runat="server" ID="rptC">
            <ItemTemplate>
               <div style="text-align:left; width:100%; text-align:left;padding:2px;">
                <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                <div class='c2'><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                <div><asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
               </div>
            </ItemTemplate>
            </asp:Repeater>
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5">&nbsp;Endorsements</div>
            <asp:Repeater runat="server" ID="rptE">
            <ItemTemplate>
                <div style="text-align:left; width:100%; text-align:left;padding:2px;">
                <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                <div class='c2'><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                <div><asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
             </div>
            </ItemTemplate>
            </asp:Repeater>
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5">&nbsp;Travel Documents</div>
            <asp:Repeater runat="server" ID="rptT">
            <ItemTemplate>
                 <div style="text-align:left; width:100%; text-align:left;padding:2px;">
                <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                <div class='c2'><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                <div><asp:TextBox runat='server' ID='txt_Rems'  CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
             </div>
            </ItemTemplate>
            </asp:Repeater>
            <div style="font-size:12px; color:Purple; font-family:Verdana; padding:2px; background-color:#EDEDF5">&nbsp;Medical Documents</div>
            <asp:Repeater runat="server" ID="rptM">
            <ItemTemplate>
             <div style="text-align:left; width:100%; text-align:left;padding:2px;">
                <div class='c1'>&nbsp;<%#Eval("DocumentName") %></div>
                <div class='c2'><asp:CheckBox runat="server" ID="ckh_L" Checked='<%#(Eval("STATUS").ToString()=="Y")%>' ToolTip='<%#Eval("DocumentTypeId").ToString() + "|" + Eval("DocumentNameId").ToString()%>' /></div>
                <div><asp:TextBox runat='server' ID='txt_Rems' CssClass='rem' Text='<%#Eval("Remark") %>'></asp:TextBox></div>
             </div>
            </ItemTemplate>
            </asp:Repeater>
            </div>
            <div style="font-size:12px; color:red; font-family:Verdana; padding:2px; background-color:#Dd2d2d2; text-align:center">* -  Remarks are manditory if not checked.</div>
            <div style="text-align:right; padding-right:2px;"> 
                <asp:Label runat="server" ID="lblMess" ForeColor="Red" style="float:left" Font-Size="16px" ></asp:Label>
                <asp:Button ID="btnSaveCheckList" runat="server" CssClass="newbtn" Text=" Save " OnClick="btnSaveCC_OnClick" />
               </div>
            </div>   
    </div>
    </form>
</body>
</html>

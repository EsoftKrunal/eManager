<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckList.aspx.cs" Inherits="Transactions_MTMCheckList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
        .btn
        {
            border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
        }
        </style>
    <style type="text/css">
        .star-no,.star-0
        {
            display:none;
        }
        .star-1
        {
            background-image:url("../Images/star.png");
            height:24px;width:24px;
        }
        .star-2
        {
            background-image:url("../Images/star.png");
            height:24px;width:48px;
        }
        .star-3
        {
            background-image:url("../Images/star.png");
            height:24px;width:72px;
        }
        .star-4
        {
            background-image:url("../Images/star.png");
            height:24px;width:96px;
        }
        .star-5
        {
            background-image:url("../Image/star.png");
            height:24px;width:120px;
        }

        .comply-Yes{
            color:green;
        }
        .comply-No{
            color:red;
        }
        .comply-NA{
            color:#888;
        }
        .comply-NS{
            color:#333;
        }

    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div style="padding:8px; " class="text headerband">
            <asp:Label runat="server" ID="lblInspectionNo" Font-Size="Larger"></asp:Label>
        </div>
        <div style="padding:5px;" >
        <%--<table width="100%" class="myTable">
            <col width="70px" />
            <col width="150px" />
            <col width="150px" />
            <col width="50px" />
            <col  />
            <tr class="header">
                <td>Sr#</td>
                <td>Question#</td>
                <td>Comply</td>
                <td>Rating</td>
                <td>Question</td>
                
            </tr>
            <asp:Repeater runat="server" ID="rptchecklist1rptchecklist">
                <ItemTemplate>
                <tr class="row-top">
                    <td>
                        <span style="background-color:#ffd800;padding:4px;"> <%#Eval("sno")%></span>
                    </td>
                    <td><%#Eval("questionno")%></td>
                    <td><%#Eval("comply")%></td>
                    <td><%#(Eval("ratingneeded").ToString()=="Y")?Eval("ratings").ToString():"NA"%></td>                    
                    <td><%#Eval("questionname")%></td>
                </tr>
                <tr class="row">
                    <td colspan="5">
                        <div style="display:block;margin:0px auto;border:solid 0px red;">
                            <b>Comments : </b> <%#Eval("comments")%>
                        </div>
                        <div style="display:block;border:solid 0px red;">
                        <div style="display:inline-block;margin:0px auto;border:solid 0px red;">
                            <asp:Repeater runat="server" ID="rptchecklist_Attachments" DataSource="<%# Attachments(1,2)%>">
                                <ItemTemplate>
                                    <div style="margin:5px;display:inline-block;">
                                          <a href="/Images/aaa.jpg" target="_blank">
                                              <img src="/Images/aaa.jpg"  alt='<%#Eval("filename")%>' style="width:100px;"/>
                                          </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                                
                            
                        </div>
                        </div>

                        <div style="display:block;border:solid 2px #0e4972;margin:4px;"></div>
                    </td>
                </tr>
                <tr class="row">
                </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>--%>
        </div>
         <asp:Repeater runat="server" ID="rptchecklist">
                <ItemTemplate>
                <div style="margin:10px; background-color:#eeeeee;border-left:solid 5px #0e4972">
                    <table width="100%" cellpadding="5" cellspacing="0">
                        <tr>
                            <td style="width:30px;text-align:center; background-color:#a1d7fe;color:#777">
                                <span style="font-size:18px;font-weight:bold;color:#333"><%#Eval("sno")%>.</span>
                            </td>
                            <td style="width:100px;text-align:left;">
                                <span style="font-size:15px;font-weight:bold;color:#e87c08"><%#Eval("questionno")%></span>
                            </td>
                            <td style="text-align:left;font-weight:bold;">
                                <%#Eval("questionname")%>
                            </td>
                            
                            <td style="width:150px;text-align:center; ">
                                    <div class='<%#(Eval("ratingneeded").ToString()=="Y")?"star-"+ Eval("ratings").ToString():"star-no"%>'>
                                        
                                    </div>
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="width:50px;text-align:center;">
                                <span style="font-size:18px;font-weight:bold;" class='comply-<%#Eval("comply")%>'><%#Eval("comply")%></span>
                            </td>
                            <td></td>
                            <td colspan="2"><i>
                                <span style='font-size:11px;font-weight:normal;color:<%#((Eval("comply").ToString()=="No")?"red":"#666")%>'><%#Eval("comments")%></span>                                
                                </i>
                            </td>                                
                        </tr>
                    </table>
                    <div style="text-align:right;">
                     <asp:Repeater runat="server" ID="rptchecklist_Attachments" DataSource="<%# Attachments(1,2)%>">
                                <ItemTemplate>
                                    <div style="margin:5px;display:inline-block;">
                                          <a href="/Images/aaa.jpg" target="_blank">
                                              <img src="/Images/aaa.jpg"  alt='<%#Eval("filename")%>' style="width:100px;"/>
                                          </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                </div>
                   
                </ItemTemplate>
            </asp:Repeater>
    </div>

    </form>
</body>
</html>

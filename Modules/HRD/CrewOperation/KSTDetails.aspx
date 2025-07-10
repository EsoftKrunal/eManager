<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="KSTDetails.aspx.cs" Inherits="CrewOperation_KSTDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Matrix</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        *{
            font-size:12px;
            color:#5c5b5b;            
        }
        .card {
            background-color: #fff;
            padding:8px;
            margin: 10px;
            border:solid 1px #dcdcdc;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 6px;         
            text-align:left;   
        }
        .formvalue
        {
            font-weight:bold;
            color:#4371a5;

        }
        h2 {
            font-size:15px;
        }
        .comments{
            color: #4371a5;
            font-style:italic;            
            padding-bottom:5px;
        }
        .row
        {
           border-left: solid 5px #e4e4e4;
            padding: 10px;
            margin-bottom: 10px;
            border-bottom: solid 1px #e4e4e4;
        }
        .crewname{
            color: #144a71;
            font-size: 11px;
            font-weight:bold;
        }
        .updatedon{
            color: #303030;
            float: left;
            font-size: 11px;
        }
    </style>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
</head>
<body onkeydown='hideLast();'>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="border:#4371a5 1px solid; background-color:#4371a5;color:White; padding:10px;font-size:14px">
        KST - <asp:Label runat="server" ID="lblkstno" ForeColor="White" Font-Size="14px"></asp:Label>
    </div>
        <div class="card">
            <table width="100%" cellpadding="5">
                <tr>
                    <td style="width:100px;">Vessel </td>
                    <td style="text-align:left"> : <asp:Label runat="server" ID="lblvesselname" CssClass="formvalue"></asp:Label></td>
                    <td style="width:80px;">Incident No. </td>
                    <td style="text-align:left"> : <asp:Label runat="server" ID="lblIncidentNo" CssClass="formvalue"></asp:Label></td>
                    <td style="width:90px;">Incident Date </td>
                    <td style="text-align:left"> : <asp:Label runat="server" ID="lblIncidentDate" CssClass="formvalue"></asp:Label></td>
                    <td style="width:60px;">Severity </td>
                    <td style="text-align:left"> : <asp:Label runat="server" ID="lblSeverity" CssClass="formvalue"></asp:Label></td>                    
                </tr>
                <tr>
                    <td>Classification </td>
                    <td colspan="5" style="text-align:left"> : <asp:Label runat="server" ID="lblClasification" CssClass="formvalue"></asp:Label></td>
                </tr>
                <tr>
                    <td>Topic </td>
                    <td colspan="5" style="text-align:left"> : <asp:Label runat="server" ID="lblTopic" CssClass="formvalue"></asp:Label></td>
                </tr>
            </table>
        </div>
        <h2>Comments</h2>
        <div class="card">
            <asp:Repeater runat="server" ID="rptcomments">
                <ItemTemplate>
                    <div class="row">
                        <div class="comments">
                            <%#Eval("comments")%>
                        </div>
                        <div style="text-align:right">
                            <span class="updatedon"><%#Common.ToDateString(Eval("updatedon"))%></span>
                            <span class="crewname"><%#Eval("crewnumber")%> | ( <%#Eval("firstname")%> <%#Eval("middlename")%> <%#Eval("lastname")%>  )  | <%#Eval("rankcode")%></span>
                        </div>                        
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>

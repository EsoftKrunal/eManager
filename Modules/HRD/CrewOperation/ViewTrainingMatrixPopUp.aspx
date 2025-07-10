<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTrainingMatrixPopUp.aspx.cs" Inherits="ViewTrainingMatrixPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        </center>
        <center>
            <div style="color:BLUE"></div>
            <div class="headerstyle" style="font-size:20px; color:#003399; padding:3px; font-weight:bold;">Training History </div>
        </center>
        <center>
            <table cellpadding="2" cellspacing="1" border="0" width="100%">
            <tr>
                <td>
                    <asp:Button ID="txtShowHis" runat="server"  Text="Show History" OnClick="txtShowHis_OnClick"  CssClass="input_box" Visible="false"/>
                    <asp:Button ID="txtShowReq" runat="server"  Text="Show Requirement" CssClass="input_box" OnClick="txtShowReq_OnClick" Visible="false"/>
                </td>
            </tr>
            <tr>
                
                <td style="text-align:left;">
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="right" valign="top"></td>
                            <td><asp:Label ID="lblTraining" runat="server" style="font-weight:bold; font-size:13px;" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>Rank :&nbsp;&nbsp;</b>
                            </td>
                            <td>
                                <asp:Label ID="lblGroup" runat="server" style="font-weight:bold;" ></asp:Label>
                            </td>
                        </tr>
                    </table>            
                    
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="2" cellspacing="0" width="100%" rules="all">
                        <col width="60px" />
                        <col/>
                        <col width="50px" />
                        <%--<col />--%>
                        <col width="80px" />
                        <%--<col width="100px" />--%>
                        <col width="120px" />
                        <col width="250px" />
                        <col width="17px" />
                        <tr class="headerstylegrid" style="font-weight:bold;" >
                            <td>Crew #</td>
                            <td>Name</td>
                            <td>Rank</td>
                            <%--<td>Training</td>--%>
                             <td>Due Date</td>
                            <%--<td>Planned For</td>--%>
                            <td>Institute</td>
                            <td>Done Duration</td>
                            <td></td>
                        </tr>
                    </table>
                    
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height:360px; ">
                        <table cellpadding="2" cellspacing="1" width="100%" rules="all">
                            <col width="60px" />
                            <col/>
                            <col width="50px" />
                            <%--<col />--%>
                            <col width="80px" />
                            <%--<col width="100px" />--%>
                            <col width="120px" />
                            <col width="250px" />
                            <asp:Repeater ID="rptMatrixDetails" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("CREWNUMBER")%></td>
                                        <td align="left"><%#Eval("NAME")%></td>
                                        <td><%#Eval("RANKCODE")%></td>
                                        <td><%#Eval("DUEDATE")%></td>
                                        <%--<td><%#Eval("PLANNEDFOR")%></td>--%>
                                        <td><%#Eval("INSTITUTENAME")%></td>
                                        <td><%#Eval("DONEDURATION")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        
</center>
    </div>
    </form>
</body>
</html>

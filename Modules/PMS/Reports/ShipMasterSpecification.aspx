<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipMasterSpecification.aspx.cs" Inherits="Print_ShipMasterSpecification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ship Master Specification</title>
</head>
<body onclick="document.getElementById('btnprnt').style.display='';" >
    <form id="form1" runat="server">
    <div>
                            <div style="text-align :center; padding-top :4px; font-weight:bold;font-size:18px; padding:15px;"  visible="false" >
                              Component Specification
                              
                           </div>
                           <table cellpadding="4" cellspacing="2" rules="all" border="1" style="border-collapse:collapse;" width="99%" >
                           <tr>
                            <td><b> Linked To:</b>&nbsp;</td>
                            <td>
                                <asp:Label ID="lblLinkedto" runat="server" ></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td ><b>Component Code :</b></td>
                            <td>
                                <asp:Label ID="lblComponentCode" style="text-align:left" runat="server" Width="60px"></asp:Label>
                                <%--<asp:Label ID="lblUnitCode" style="text-align:left" runat="server" Visible="False" Width="30px"></asp:Label>--%>
                                </td>                                
                            </tr>
                            <tr>
                            <td><b>Component Name :</b></td>
                            <td>
                                <asp:Label  ID="lblComponentName" runat="server" Width="350px"></asp:Label>
                            </td>
                            </tr>
                            <tr><td><b>Maker :</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblMaker" runat="server"  ></asp:Label>
                                </td>
                                </tr>
                            <tr> 
                                <td>
                                    <b>Maker Type :</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblMakerType" runat="server" ></asp:Label>
                                </td>
                            <tr><td><b>Description :</b><asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblComponentDesc" runat="server" Width="350px" Height="66px" TextMode="MultiLine"></asp:Label>
                                </td>
                                </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfCompId" runat="server" />
                                </td>
                                <td style="text-align:left">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <col width="110px;" />
                                        <col  width="200px" />
                                        <col  width="140px" />
                                        <col />
                                        <tr>
                                            <td>
                                                Class EQIP
                                            </td>
                                            <td>
                                                <%--<asp:CheckBox ID="chkClass" runat="server" AutoPostBack="True" Enabled="false" />--%>
                                                <asp:Label ID="lblClass" runat="server" ></asp:Label>
                                            </td>
                                            <td style="vertical-align:middle">
                                                <b>Class EQIP Code :</b>&nbsp;</td>
                                            <td style="text-align:left">
                                                <asp:Label ID="lblClassCode" runat="server" ></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Critical EQIP
                                            </td>
                                            <td colspan="3">
                                                <%--<asp:CheckBox ID="chkCritical" runat="server" Enabled="false" />--%>
                                                <asp:Label ID="lblCritical" runat="server" ></asp:Label>
                                            </td>
                                            
                                        </tr>
                                      
                                        <tr>
                                            <td>
                                                Inactive
                                                
                                            </td>
                                            <td colspan="3">
                                                <%--<asp:CheckBox ID="chkInactive" runat="server" Enabled="false" />--%>
                                                <asp:Label ID="lblInactive" runat="server" ></asp:Label>
                                            </td>
                                            
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                   <img src ="../Images/PrintReport.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>   
                                </td>
                            </tr>
                           </table>
    </div>
    </form>
</body>
</html>

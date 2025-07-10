<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionResult.aspx.cs" Inherits="emtm_ActionResult" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Verdana; font-size:12px;">
    <center>
    <asp:Panel runat="server" ID="pnl1" Visible="false">
     <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
                        &nbsp;Leave Approval Discussion</td>
                </tr>
                <tr>
                    <td style=" padding :10px; vertical-align:top;">
                        <fieldset>
                        <legend>Leave Details</legend>             
                        <table id="tblLeaveType" runat="server" width="100%" cellspacing ="0" cellpadding="5" border="0">
                        <tr>
                            <%--<td style ="text-align:right;">
                                EmpCode :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblEmpCode" runat="server" ></asp:Label>  
                            </td>--%>
                            <td style ="text-align:right;">
                                Emp Name :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblEmpName" runat="server" ></asp:Label>  
                            </td>
                            <td style ="text-align:right;">
                                Designation :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </td>

                            <td style ="text-align:right;">
                                Department :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="text-align :right;">
                                Office :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblOffice" runat="server" ></asp:Label>&nbsp;
                            </td>

                            <td style ="text-align:right;">
                                Leave Type :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblLeaveType" runat="server" ></asp:Label>  
                            </td>

                            <td style="text-align :right;">
                                Leave Status :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblLeaveStatus" runat="server"></asp:Label>  </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                               Leave <span lang="en-us">Period</span> :
                            </td>
                            <td colspan="3" style="text-align :left;">
                                <asp:Label ID="lblLeaveFrom" runat="server" ></asp:Label>  
                                <span lang="en-us">&nbsp;to </span>
                                <asp:Label ID="lblLeaveTo" runat="server"></asp:Label>  
                                <asp:Label ID="lblHalfDay" runat="server"></asp:Label>
                                <asp:Label ID="lblLeaveDays" runat="server" Text=""></asp:Label> 
                            </td>
                            <td style="text-align :right;">
                                Total Office Absence :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblAbsentDays" runat="server" Text=""></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                Employee Remark :</td>
                            <td colspan="5" style="text-align :left;" >
                                <asp:Label ID="lblReason" runat="server"  Width="400px"></asp:Label>  
                            </td>
                            
                        </tr>
                            <tr>
                                <td style="text-align:right;">
                                    Approver Comments :</td>
                                <td colspan="5" style="text-align :left;">
                                    <asp:Label ID="lblMDComments" runat="server" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </fieldset>
                    </td> 
                </tr>
                </table>
                <br />
                Please Enter Your Comments below.
                <br /><br />
                <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine"  Width="90%"  Height="100px"></asp:TextBox> 
                <br /><br />
                <asp:Button runat="server" ID="btnPnl1Save" Text="Reply Comments" 
            onclick="btnPnl1Save_Click" />
                <br /><br />
    </asp:Panel>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Size="13px" ></asp:Label>
    </center>
    </div>
    </form>
</body>
</html>

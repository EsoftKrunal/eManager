<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaySlip.aspx.cs" Inherits="StaffAdmin_Compensation_PaySlip" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />

    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Register
                        </div>
                        <div style="background-color:white;">
                            <uc2:CB_Menu ID="CBMenu" runat="server" />
                        </div>
                       
                            <table cellpadding="0" cellspacing ="0" border="0" width="100%" style="margin-top:5px;">
                                    <tr>
                                        <td style="width:60px; padding-left:5px; text-align:right;">
                                            Office :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlOffice" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td style="width:100px; padding-left:50px; text-align:right;">
                                            Status :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlStatus"  AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged">
                                                <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                                <asp:ListItem Text="All Employees" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:100px; padding-left:5px; text-align:right;">
                                            Employee :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlEmp" ></asp:DropDownList>
                                        </td>
                                        <td style="width:100px; padding-left:20px; text-align:right;">
                                            Period :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>
                                            <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>
                                        </td>
                                        
                                        
                                    </tr>
                                <tr>
                                    <td colspan="2">
                                        <strong style="margin-left:5px;">Total Records :&nbsp;<asp:Label ID="EmpCount" runat="server" ></asp:Label>&nbsp;</strong>                                        
                                    </td>
                                    <td colspan="2" style="text-align:left;">
                                            <strong style="margin-left:5px;">Active Employees :&nbsp;<asp:Label ID="lblActiveEmployees" runat="server" ></asp:Label>&nbsp;</strong>
                                        
                                     </td>
                                    <td colspan="4" style="padding: 5px;text-align:right;">

                                        

                                            <a style="float:left;font-weight:bold; margin-left :0px; " href="HR_LeaveSummaryReport.aspx" target="_blank"></a>
                                            
                                            <asp:Button ID="btn_Add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Show_Click" Text=" Show " />

                                            <asp:Button ID="btn_Generate" runat="server" CausesValidation="false" CssClass="btn" Text="Process Salary" OnClientClick="window.open('GenerateSalary.aspx','');" />

                                            <asp:Button ID="btnPublishPayslipPopup" runat="server" CausesValidation="false" Visible="false" CssClass="btn" Text="Publish Payslip" OnClick="btnPublishPayslipPopup_OnClick"/>
                                            <asp:Button ID="btn_Download" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Download_Click" Text=" Download " />
                                        
                                            <asp:Button ID="btnPrintEmploiesSalary" runat="server" CausesValidation="false" CssClass="btn" Text="Print" OnClick="btnPrintEmploiesSalary_OnClick"/>
                      
                                          </td>
                                </tr>
                              </table>
                            <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                                
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center; border-bottom:none;">
                                
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" >
                                <colgroup>
                                         <col style="width:40px;" />
                                        <col style="width:90px;" /> 
                                        <col style="width:300px;" /> 
                                        <col style="width:120px;" /> 
                                        <col style="width:120px;" />  
                                        <col style="width:120px;" />                                          
                                        <col style="width:120px;" />  
                                        <%--<col style="width:60px;" />--%>
                                        <col /> 
                                    </colgroup>
                                        <tr align="left" class= "headerstylegrid">
                                            <td>Print</td>
                                            <td align="left">Period </td>
                                            <td align="left">Employee Name</td>
                                            <td align="left">Income</td>
                                            <td align="left">Deduction</td>
                                            <td align="left">Net Payable</td>
                                            <td align="left">Gross Amount</td>
                                            <%--<td style="text-align:center;">
                                                <img src="/Images/lock.png" style="border:none;" />
                                            </td>--%>
                                            <td align="left">Modified By / On</td>
                                        </tr>
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:center;">

                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow">
                                <colgroup>
                                       <col style="width:40px;" />
                                        <col style="width:90px;" /> 
                                        <col style="width:300px;" /> 
                                        <col style="width:120px;" /> 
                                        <col style="width:120px;" />                                          
                                        <col style="width:120px;" />  
                                        <col style="width:120px;" />  
                                        <%--<col style="width:60px;" />--%>
                                        <col />   
                                    </colgroup>
                                

                                <asp:Repeater ID="RptLeaveSearch" runat="server" >
                                    <ItemTemplate>
                                        <tr>
                                            <td align="center">
                                              <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Modules/HRD/Images/print_16.png" ToolTip="Print" OnClick="btnPrint_OnClick" />
                                                <asp:HiddenField ID="hfEmpID" runat="server" Value='<%#Eval("EmpID")%>' />
                                            </td>
                                            <td align="left"><%#Common.ToDateString(Eval("SalaryDate")).Substring(3)%></td>
                                            <td align="left"><%#Eval("EmpName")%></td>
                                            <td align="right"><%#Eval("TotalEncome")%></td>
                                            <td align="right"><%#Eval("TotalDeduction")%></td>
                                            <td align="right"><%#Eval("NetAmount")%></td>
                                            <td align="right"><%#Eval("GrossAmount")%></td>
                                                <%--<td><img src="/Images/lock.png" runat="server" visible='<%#(Eval("Locked").ToString()=="Y")%>'/></td>--%>
                                            <td align="left">
                                                <%#Eval("ModifiedBy")%> / <%#Common.ToDateString(Eval("ModifiedOn"))%>
                                            </td>
                                                  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                            </div> 
                                </div>
                        </td>
                    </tr>
            </table>      
  
     
    </div>

     <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvPuplishPaySlip" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
     <div style="position :relative; width:550px;text-align :center; border :solid 5px #000000;padding-bottom:5px; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
        <center>
                <table width="100%" cellpadding="10" cellspacing="10" border="0">
                    <tr>
                        <td colspan="2" style="text-align:center;font-size:20px;font-weight:bold;">
                            <span style="color:#808080;"> Publish report for </span>
                            <br />
                            <asp:Label ID="lblPublishReportPopupText" runat="server"  style="font-size:25px;color:#ff6a00;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <asp:Button ID="btnPublishReport" runat="server"  Text="Publish" CssClass="btn"  OnClick="btnPublishReport_OnClick" OnClientClick="return confirm('Are you sure?');"/>
                        </td>
                        <td>
                            <asp:Button ID="btnClosePublishPayslipPopup" runat="server" CssClass="btn"  Text=" Close " OnClick="btnClosePublishPayslipPopup_OnClick" />
                        </td>
                    </tr>
                </table>

            <table width="100%" cellpadding="5" cellspacing="5" border="0">
                <tr>
                    <td style="height:20px;text-align:center;">
                        <asp:Label ID="lblMsgPublish" runat="server" style="color:red;"></asp:Label>
                    </td>
                </tr>
            </table>
                    
        </center>
         </div>
        </center>
       </div>
     </asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompensationBenifits.aspx.cs" Inherits="StaffAdmin_Compensation_CompensationBenifits" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>

<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%">
                    <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Compensation and Benefits
                        </div>
                        <div style="background-color:white;">
                            <uc2:CB_Menu ID="CBMenu" runat="server" />
                        </div>
                        <asp:UpdatePanel id="UpdatePanel" runat="server" >
                        <ContentTemplate>
                            <table border="0" cellspacing="0" cellpadding="2"  style="text-align: center; padding-right:50px; height :25px;" width="100%">
                                <tr>
                                    <td style="text-align :right">
                                        Emp. Name :
                                    </td>
                                    <td style="text-align :left">
                                        <asp:TextBox ID="txtEmpName" runat="server" MaxLength="6"
                                            Width="160px" TabIndex="1"></asp:TextBox></td>
                                    <td style="text-align :right">
                                        Office :
                                    </td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlOffice" runat="server" Width="100px" AutoPostBack="true"  
                                            onselectedindexchanged="ddlOffice_SelectedIndexChanged" onchange="SetFocus();"></asp:DropDownList>
                                    </td>
                                    <td style="text-align :right">
                                        Department :</td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="100px"></asp:DropDownList>
                                    </td>
                                    <td style="text-align :right">
                                        Status :</td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                            <asp:Button ID="btn_Search" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="0" />
                                            <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                            <asp:Button ID="btn_Generate" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Generate_Click" Text="Generate" Width="85px" TabIndex="20" OnClientClick="window.open('GenerateSalary.aspx','');"  Visible="false"/>
                                     
                                    </td>
                                    <td>
                                    <asp:Label ID="EmpCount" runat="server" ></asp:Label> Records.
                                    </td>
                               </tr>
                             </table>
                         
                          <%--  <table cellpadding="0" cellspacing ="0" width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center" colspan="6" style="padding-right: 5px; text-align: right;">
                                            <a style="float:left;font-weight:bold; margin-left :10px; " href="HR_LeaveSummaryReport.aspx" target="_blank"></a>
                                            <strong>Total Filterd Records :&nbsp;&nbsp;</strong>
                                                
                                        </td>
                                    </tr>
                              </table>--%>
                            <div id="divTraveldocument" runat="server" style="padding:0px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%;text-align:center; border-bottom:none;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridheader">
                                    <colgroup>
                                        <col style="width:30px;" />
                                        <col style="width:30px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:350px;" />
                                        <col style="width:250px;" />                                     
                                        <tr class= "headerstylegrid">
                                            <td></td>
                                            <td></td>
                                            <td>Emp Code</td>
                                            <td style="text-align:left;">Employee Name</td>
                                            <td>Position</td>
                                            <td>Department</td>
                                        </tr>
                                    </colgroup>
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:center;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow">
                                <colgroup>
                                        <col style="width:30px;" />
                                        <col style="width:30px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:350px;" />
                                        <col style="width:250px;" />
                                          
                                </colgroup>
                                <asp:Repeater ID="RptLeaveSearch" runat="server" >
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("EmpId"))==SelectedId)?"selectedrow":" "%>'>
                                            <td align="center">
                                                <%--<a href="CB_Details.aspx?EmpID=<%#Eval("EmpId")%>" target="_blank" title="View" > <img src="../../../Images/HourGlass.gif" style="border:none;" /> </a>--%>
                                                <asp:ImageButton OnClick="ViewPayslip" runat="server" CommandArgument='<%#Eval("EmpId")%>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" style="border:none;" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton id="btnViewDocuments" runat="server" OnClick="btnViewDocuments_OnClientClick" CommandArgument='<%#Eval("EmpId")%>' ImageUrl="~/Modules/HRD/Images/paperclip12.gif" style="border:none;width:15px;" />
                                            </td>
                                            
                                            <td align="left">
                                                <%#Eval("EMPCODE")%></td>
                                            <td align="left">
                                                <%#Eval("EMPNAME")%></td>
                                            <td align="left">
                                                <%#Eval("PositionName")%></td>
                                            <td align="left">
                                                <%#Eval("Department")%></td>
                                                  
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                            </div> 
                                </div>
                                </ContentTemplate>
                         </asp:UpdatePanel>
                        </td>
                    </tr>
            </table>      
  
     
    </div>
     </asp:Content>

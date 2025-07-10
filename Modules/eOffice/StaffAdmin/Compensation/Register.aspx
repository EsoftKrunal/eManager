<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="StaffAdmin_Compensation_Register" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master"%>

<%@ Register src="~/Modules/eOffice/StaffAdmin/Compensation/CB_Menu.ascx" tagname="CB_Menu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
    <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />

    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :3px; font-weight: bold;">
                            Register
                        </div>
                        <div style="background-color:white;">
                            <uc2:CB_Menu ID="CBMenu" runat="server" />
                        </div>
                       
                            <table cellpadding="0" cellspacing ="0" width="100%">
                                    <tr>
                                        <td style="width:100px; padding-left:20px; text-align:right;">
                                            Office :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlOffice" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td align="center" colspan="6" style="padding-right: 5px;padding-top:5px; text-align: right;">
                                            <a style="float:left;font-weight:bold; margin-left :10px; " href="HR_LeaveSummaryReport.aspx" target="_blank"></a>
                                            <strong>Total Records :&nbsp;<asp:Label ID="EmpCount" runat="server" ></asp:Label>&nbsp;</strong>
                                            <asp:Button ID="btn_Add" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Add_Click" Text=" + Add" />
                                        </td>
                                    </tr>
                              </table>
                            <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center; border-bottom:none;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridheader">
                                    <colgroup>
                                        <col style="width:40px;" />
                                        <col style="width:60px;" />
                                        <col />
                                        <col style="width:150px;" />                                        
                                        <col style="width:150px;" />
                                        <%--<col style="width:120px;" /> 
                                        <col style="width:120px;" />  --%>                                        
                                        <col style="width:120px;" />                                          
                                        </colgroup>
                                        <tr align="left" class= "headerstylegrid">
                                            <td></td>
                                            <td align="left">Sr#</td>
                                            <td align="left">Head Name</td>
                                            <td align="left">Payment Type</td>
                                            <%--<td align="left">Calculation Mode</td>
                                            <td align="left">Amount Type</td>--%>
                                            <td align="left">Pay Out</td>
                                            
                                        </tr>
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:center;">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;border-collapse:collapse;" class="gridrow">
                                 <colgroup>
                                        <col style="width:40px;" />
                                        <col style="width:60px;" /> 
                                        <col />
                                        <col style="width:150px;" />                                        
                                        <col style="width:150px;" />
                                        <%--<col style="width:120px;" /> 
                                        <col style="width:120px;" /> --%>
                                        <col style="width:120px;" /> 
                                        </colgroup>
                               
                                        
                                <asp:Repeater ID="RptLeaveSearch" runat="server" >
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("HeadId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btndocedit" runat="server" CausesValidation="false" Visible='<%# (Eval("Locked").ToString()!="True") %>' CommandArgument='<%# Eval("HeadId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btndocedit_Click" ToolTip="Edit" />
                                            </td>
                                            <td align="left"><%#Eval("SrNumber")%></td>
                                            <td align="left">
                                                <%#Eval("HeadName")%></td>
                                            <td align="left">
                                                <%#Eval("Income_Ded_Text")%></td>
                                            <%--<td align="left">
                                                <%#Eval("CalcMode_Text")%></td>
                                            <td align="left">
                                                <%#Eval("Fixed_Per_Text")%></td>--%>

                                            <td  align="left"><%#((Eval("YearlyHead").ToString()=="Y")?"Yearly":"Monthly")%></td>
                                            
                                                  
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

        <%--------------------------------------------------------------------%>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvAddRegister" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position :relative; width:500px; height:230px;text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
            <div style="background-color:#333;padding:5px;font-weight:bold;color:white;"> Add/Edit Heads </div>
            <table cellpadding="5" cellspacing="5" border="0" width="100%" >
                <col width="120px" />
                <col />
                <tr> 
                    <td><b> Head Name</b></td>
                    <td>
                        <asp:TextBox id="txtHeadName" runat="server" Width="90%" ></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                    <td>
                        <b>Payment Type</b>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdolistDeduction" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="I" Text="Income"> </asp:ListItem>
                            <asp:ListItem Value="D" Text="Deduction"> </asp:ListItem>
                            <asp:ListItem Value="C" Text="CTC Only"> </asp:ListItem>
                        </asp:RadioButtonList>
                     </td>
                </tr>
                <tr style="display:none;"> 
                    <td><b>Calculation Mode</b></td>
                    <td>
                         <asp:RadioButtonList ID="rdolistCalMode" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="A" Text="Auto Calculate"> </asp:ListItem>
                            <asp:ListItem Value="M" Text="Mannual Mode"> </asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="display:none;"> 
                    <td><b> Amount Type</b></td>
                    <td>
                         <asp:RadioButtonList ID="rdolistType" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Value="F" Text="Fixed"> </asp:ListItem>
                            <asp:ListItem Value="P" Text="Percentage"> </asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr> 
                    <td><b> Pay Out </b></td>
                    <td>
                        <%-- <asp:CheckBox ID="chkYearlyHead" runat="server" AutoPostBack="true" OnCheckedChanged="chkYearlyHead_OnCheckedChanged" />--%>
                        <asp:RadioButtonList ID="rdoPayin" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="N" Text="Monthly"></asp:ListItem>
                            <asp:ListItem Value="Y" Text="Yearly"></asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:DropDownList ID="ddlPayingMonth" runat="server" Visible="false">
                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><b> Sr. Number </b></td>
                    <td>
                        <asp:TextBox ID="txtSrNumber" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr> 
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" Text="Save" Width="100px" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" onclick="btnAddNewCancel_Click" Text="Close" CausesValidation="false" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lblMsg" runat="server" style="color:red;"></asp:Label>
                    </td>
                </tr>
            </table>
                
            </center>
    </div>
        </center>
            </div>
   </asp:Content>

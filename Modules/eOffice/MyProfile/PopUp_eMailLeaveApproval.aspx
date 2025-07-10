<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_eMailLeaveApproval.aspx.cs" Inherits="emtm_MyProfile_Emtm_PopUp_eMailLeaveApproval" Async="true"  %>
<%@ Register src="LeavePlanner.ascx" tagname="LeavePlanner" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Approval</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function CloseWindow() {
            //window.opener.document.getElementById("btnhdnApproval").click();
            window.close();
        }
     </script>
     <style type="text/css">
        .style1
        {
            text-align :left; 
            font-size :13px;  
            font-family:Arial Unicode MS; 
            color :#222222; 
            padding :5px; 
            border-style:none;
            text-align :left; 
            width:600px;
        }
    </style> 
</head>
<body onunload="CloseWindow();" >
    <form id="form1" runat="server">
   <div>
            <%--<asp:ScriptManager ID="ScriptManager1"  runat="server"></asp:ScriptManager>--%>
            <table width="100%">
            <tr>
            <td valign="top" style="border:solid 1px #4371a5; height:100px;">
                <div>
                   <uc1:LeavePlanner ID="LeavePlanner1" runat="server" />
                </div>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
                    &nbsp;Leave Approval 
                    </td>
                </tr>
                <tr>
                    <td style=" padding :10px; vertical-align:top;">
                        <fieldset>
                        <legend>Leave History</legend>             
                        <table id="tblLeaveType" runat="server" width="100%" cellspacing ="0" cellpadding="5" border="0">
                        <tr>
                        <td style ="text-align:right;">
                                Application Date :
                            </td>
                            <td style="text-align :left;" colspan="5">
                                <asp:Label ID="lblAppDate" runat="server" ></asp:Label>  
                            </td>
                        </tr>
                        <tr>
                            <td style ="text-align:right;">
                                EmpCode :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblEmpCode" runat="server" ></asp:Label>  
                            </td>
                            <td style ="text-align:right;">
                                Emp Name :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblEmpName" runat="server" ></asp:Label>  
                            </td>
                            <td style="text-align :right;">
                                Office :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblOffice" runat="server" ></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style ="text-align:right;">
                                Department :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
                            <td style ="text-align:right;">
                                Designation :</td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label></td>
                            <td style="text-align :left;">
                                &nbsp;</td>
                            <td style="text-align :left;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                               Leave <span lang="en-us">Period</span> :
                            </td>
                            <td style="text-align :left;">
                                <asp:Label ID="lblLeaveFrom" runat="server" ></asp:Label>  
                                <span lang="en-us">&nbsp;: </span>
                                <asp:Label ID="lblLeaveTo" runat="server"></asp:Label>  
                                <asp:Label ID="lblHalfDay" runat="server"></asp:Label>  
                                <asp:Label ID="lblLeaveDays" runat="server" Text=""></asp:Label> 
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
                                Employee Remark :</td>
                            <td style="text-align :left;" colspan="3">
                                <asp:Label ID="lblReason" runat="server"  Width="400px"></asp:Label>  
                            </td>
                            <td style="text-align :right;">
                                Total Office Absence :</td>
                            <td colspan="3" style="text-align :left;">
                                <asp:Label ID="lblAbsentDays" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        </table>
                        </fieldset>
                    </td> 
                </tr>
                </table>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                <div style="text-align:left; padding-left:10px;font-size:13px; color:Red; ">
                    <asp:Literal runat="server" ID="litNotes"></asp:Literal>
                </div>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                   <tr>
                     <td style=" padding :10px; vertical-align:top;">
                        <div style="float:left; width:48%;">
                        <asp:UpdatePanel runat="server" ID="upp1">
                        <ContentTemplate>
                        <fieldset style="width:100%">
                        <legend>Leave Approval</legend> 
                        <div class="style1" >
                        <asp:Label id="lblCurrentMonth1" runat="server"></asp:Label>
                        <asp:Label ID="lblLeaveBalance" runat="server" MaxLength="100" Font-Size="14px" Font-Bold="true"  ForeColor="Green"></asp:Label>
                       </div>
                        <table id="Table1" runat="server" width="100%" cellspacing ="0" cellpadding="5" border="0">
                            <tr>
                                <td style="text-align:right;">
                                                          </td>
                                <td style="text-align :left;">
                                    <asp:RadioButton ID="rdoLeaveApprove" runat="server" GroupName="LeaveStatus" Text="Approve" Font-Bold="True" ForeColor="#009933" AutoPostBack="true" OnCheckedChanged="Action_Change"/>
                                    <asp:RadioButton ID="rdoLeaveReject" runat="server" GroupName="LeaveStatus" Text="Reject" Font-Bold="True" ForeColor="Red" AutoPostBack="true" OnCheckedChanged="Action_Change"/>
                                </td>
                                <td style="text-align :left;">
                                    &nbsp;</td>
                           </tr>
                            <tr>
                                <td style="text-align:right;" valign="top">
                                    Remarks :                         </td>
                                <td style="text-align :left;" colspan="2">
                                    <asp:TextBox ID="txtAppRejRemarks" runat="server" TextMode="MultiLine" Height="96px" 
                                        Width="100%"></asp:TextBox> 
                                </td>
                            </tr>
                         </table> 
                        </fieldset>
                        <asp:Button ID="btnDone" CssClass="btn" runat="server" 
                        Text="Save & Notify" onclick="btnDone_Click" Width="111px" style="float:right; margin-top:10px;" OnClientClick="this.value='Please Wait..';" />  
                        </ContentTemplate>
                        </asp:UpdatePanel>    
                        </div>
                     <div style="float:right; width:48%;">
                     
                     <fieldset style="width:100%">
                        <legend>Discussion</legend> 
                        <table id="Table2" runat="server" width="100%" cellspacing ="0" cellpadding="0" border="0">
                           <tr runat="server" id="trAddCommheader">
                                <td style="text-align:center">
                                    <asp:LinkButton runat="server" Text="Click Here to Initiate Discussion" ID="btnAdd"  ToolTip="Initiate Discussion" onclick="btnAdd_Click"></asp:LinkButton>
                                </td>
                           </tr>
                           <tr runat="server" id="trothercomm" >
                            <td style="text-align:center">
                                <div style="height:142px; overflow-x:hidden; overflow-y:scroll;">
                                <asp:Repeater runat="server" ID="rptComments">
                                <ItemTemplate>
                                 <div style="text-align:left"><span style='color:Red'> ■ <%#Eval("Emp") %> :</span><span style='color:Purple; font-style:italic'><%#Eval("Comments").ToString().Replace("''","'") %></span>
                                </div>
                                </ItemTemplate>
                                </asp:Repeater>
                                </div>
                            </td>
                           </tr>
                         </table> 
                        </fieldset>
                     </div>    
                    </td> 
                   </tr> 
                </table>
                  <!-- POPUP DIV -->
                        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvDisc" runat="server" visible="false">
                        <center>
                        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                        <div style="position :relative; width:750px; height:230px; padding :10px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                            <center>
                            <span style=" font-size:14px; font-weight:bold">Select Members & Enter Comments to Initiate Discussion</span><br /><br />
                            <div style="float:left; height:150px; width:200px; border:solid 1px #d2d2d2; text-align:left; padding:5px;">
                                <asp:CheckBoxList runat="server" ID="ckhMembers" DataTextField="EmpName" DataValueField="EmpId"></asp:CheckBoxList>
                            </div>
                            <div style="float:right; height:150px; width:500px; border:solid 1px #d2d2d2;text-align:right;padding:5px;">
                                <asp:TextBox runat="server" id="TextBox1" Width="95%" TextMode="MultiLine" Height="140px" required='yes' ValidationGroup="vg"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TextBox1" ValidationGroup="vg"></asp:RequiredFieldValidator> 
                            </div>
                            <asp:Button Text="Save" runat="server" id="btnSave" OnClick="btnSave_Click" ValidationGroup="vg" />&nbsp;
                            <asp:Button Text="Close" runat="server" id="btnClose1" OnClick="btnClose_Click" ValidationGroup="vg" CausesValidation="false"/>
                            </center>
                        </div>
                        </center>
                        </div>
                     <!-- POPUP DIV -->
                </ContentTemplate>
                </asp:UpdatePanel>       
                <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td style="text-align :right; padding :4px;" >
                                      
                            <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClientClick="CloseWindow();"  />  
                                 </td>
                            </tr>
                </table>  
            </td>
        </tr> 
      </table>                       
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ApplicantApproval.aspx.cs" Inherits="CrewApproval_ApplicantApproval" MasterPageFile="~/MasterPage.master" Title="Crew Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script type="text/javascript">
        function OpenPopUp(ctl) {
            window.open("..\\Applicant\\CandidateDetailPopUp.aspx?candidate=" + ctl.getAttribute("appid") + "&M=App");
        }

        function refreshPage() {
            document.getElementById('btnSearch').click();
        }
    </script>
    <style type="text/css">
        .bordered tr td
        {
            border :solid 1px #e5e5e5;
        }

    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="background-color:#f9f9f9; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;width: 100%" >
        <tr>
            <td class="text headerband" >Applicant Approval</td>
        </tr>
        <tr>
        <td>
        <table width="100%" cellpadding="1" border="0" style="background-color:#e5e5e5; border-collapse:collapse;" class="bordered" >
                            <tr>
                                <td>
                                            <table width="100%" border="0"  >
                                                <tr>
                                                    <td style=" text-align :right " >
                                                        Application Id/Name :</td>
                                                    <td style=" text-align :left " >
                                                    <asp:TextBox ID="txtIDName" runat="server" AutoPostBack="true" 
                                                            CssClass="input_box" OnTextChanged="UpdateList" Width="198px"></asp:TextBox>
                                                    </td>
                                                    <td style=" text-align :right " >
                                                        Rank :</td>
                                                    <td style=" text-align :left " >
                                                        <asp:DropDownList ID="ddlRank" runat="server" AutoPostBack="true" 
                                                            CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="100px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style=" text-align :right " >
                                                        Nationality :</td>
                                                    <td style=" text-align :left " >
                                                       
                                                        <asp:DropDownList ID="ddlNat" runat="server" AutoPostBack="true" 
                                                            CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style=" text-align :left ">
                                                        <asp:Button runat="server" Text="Search" ID="btnSearch" CssClass="btn" onclick="UpdateList" />
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style=" text-align :right " >
                                                      Vessel Exp. :  </td>
                                                    <td style=" text-align :left " >
                                                        <asp:DropDownList ID="ddlVType" runat="server" AutoPostBack="true" 
                                                            CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style=" text-align :right " >
                                                       Available From : </td>
                                                   <td style=" text-align :left " >
                                                       <asp:TextBox ID="txt_SignOn_Date" runat="server" CssClass="input_box" MaxLength="10"
                                                    Width="80px" TabIndex="8" OnTextChanged="UpdateList" AutoPostBack="true" ></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                    &nbsp;
                                                    </td>
                                                    <td style =" text-align:right">
                                                        To : </td>
                                                    <td style =" text-align:left">
                                                      <asp:TextBox ID="txt_SignOff_Date" runat="server" CssClass="input_box" MaxLength="10"
                                                    Width="80px" TabIndex="9"  OnTextChanged="UpdateList" AutoPostBack="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                    </td>
                                                        <td style =" text-align:left ;" >
                                                             <asp:Label ID="lblRCount" runat="server" Font-Bold="true"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style=" text-align :right " >
                                                      Status :  </td>
                                                    <td style=" text-align :left " >
                                                          <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="input_box" OnSelectedIndexChanged="UpdateList" Width="150px">
                                                <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                                              <%--  <asp:ListItem Text="Applicant" Value="1" Selected="True" ></asp:ListItem>--%>
                                               <%-- <asp:ListItem Text="Ready for Approval" Value="2" Selected="True" ></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="3" ></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="4" ></asp:ListItem> --%>                                               <asp:ListItem Text="Awaiting Manning Approval" Value="2" ></asp:ListItem>
                                                <asp:ListItem Text="Ready for Proposal" Value="3" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Manning Rejected" Value="4" ></asp:ListItem>
                                                </asp:DropDownList>
                                                    </td>
                                                    <td style=" text-align :right " >
                                                       </td>
                                                   <td style=" text-align :left " >
                                                      
                                                    </td>
                                                    <td style =" text-align:right">
                                                       </td>
                                                    <td style =" text-align:left">
                                                      
                                                    </td>
                                                        <td style =" text-align:left ;" >
                                                            
                                                        </td>
                                                </tr>
                                                </table>
                                    
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txt_SignOff_Date" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txt_SignOn_Date"></ajaxToolkit:CalendarExtender>
                                        <div style="overflow-x:hidden; overflow-y :scroll; width:100%;height:25px; ">
                                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                                        <colgroup>
                                        <col width="60px;" /> 
                                        <col width="70px;" /> 
                                        <col /> 
                                        <col width="100px;" />  
                                        <col width="90px;" /> 
                                        <col width="90px;" /> 
                                        <col width="205px;" />
                                        <col width="150px;" />
                                        <col width="140px;" />
                                        </colgroup>
                                        <tr class= "headerstylegrid">
                                        <td>Approve</td>
                                        <td>App. ID</td>
                                        <td>Name</td>
                                        <td>Rank</td>
                                        <td>Nat.</td>
                                        <td>Avail.From</td>
                                        <td>Submited By/On</td>
                                        <td>Approved By/On</td>
                                            <td>Status</td>
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'>&nbsp;</td>":""%>
                                        </tr>
                                        </table>
                                        </div>
                                        <div style="overflow-x:hidden; overflow-y :scroll; width:100%;height:370px;">
                                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                                        <colgroup >
                                        <col width="60px;" /> 
                                        <col width="70px;" /> 
                                        <col /> 
                                        <col width="100px;" />  
                                        <col width="90px;" /> 
                                        <col width="90px;" /> 
                                        <col width="205px;" />
                                        <col width="150px;" />
                                        <col width="140px;" />
                                        </colgroup>
                                        <asp:Repeater runat="server" ID="rptData">
                                        <ItemTemplate>
                                        <tr id='tr<%#Eval("CandidateId")%>' lastclass='row'>
                                            <td style =" text-align :center; ">
                                                <asp:ImageButton runat="server" ID="imgView" ToolTip="Approve" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" appid='<%#Eval("CandidateId")%>' CommandArgument='<%#Eval("CandidateId")%>' OnClientClick='OpenPopUp(this);' />
                                            </td>
                                            <td>    <%#Eval("candidateid")%>   </td>
                                            <td style=" text-align :left;" >&nbsp;<%#Eval("Name")%></td>
                                            <td style=" ">&nbsp;<%#Eval("Rank")%></td>
                                            <td style=" ">&nbsp;<%#Eval("Country")%></td>
                                            <td style=" ">&nbsp;<%#Eval("AvailableFrom")%></td>
                                            <td style=" ">&nbsp;<%#Eval("ModifiedBy")%>/ <%#Eval("ModifiedOn")%></td>
                                            <td style=" ">&nbsp;<%#Eval("AppRejBy")%>/ <%#Eval("AppRejOn")%></td>
                                            <td style=" ">&nbsp;<%#Eval("StatusName")%></td>

                                            <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'>&nbsp;</td>" : ""%>
                                        </tr> 
                                        </ItemTemplate> 
                                        </asp:Repeater> 
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table> 
                                        </div>

                                        <%------------------------------------------------------------------------------------%>
                                             <div style="position:fixed;bottom:0px; left:0px; width:100%; padding:5px; text-align:center; background-color:#c2c2c2;">
                                              <table cellpadding="3" cellspacing="0" border="0" style="margin:0px auto">
                                                  <tr>
                                                      <td>
                                                          <asp:Button ID="btnPrev" runat="server" OnClick="btnPrev_Click" style="padding:5px;background-color:orange;" Text="&lt;&lt;" />
                                                      </td>
                                                      <td>
                                                          <asp:Label ID="lblCounter" runat="server"></asp:Label>
                                                      </td>
                                                      <td>
                                                          <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" style="padding:5px;background-color:orange;" Text="&gt;&gt;" />
                                                      </td>
                                                  </tr>
                                              </table>

                                        </div>
                                        <%------------------------------------------------------------------------------------%>
                                        <asp:Label runat="server" ID="lblmsg" Font-Size="Large" ></asp:Label>
                                </td>
                            </tr>
                        </table>
        </td>
        </tr>
        </table>
      </div>
 </asp:Content>

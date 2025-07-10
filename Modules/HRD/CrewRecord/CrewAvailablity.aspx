<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewAvailablity.aspx.cs"
    Inherits="CrewAvailablity" MasterPageFile="~/MasterPage.master" Title ="Crew Availablity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/KPIScript.js"></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
    <style type="text/css">
        .bordered tr td
        {
            border :solid 1px #e5e5e5;
        }

    </style>
       </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel51">
            <ContentTemplate>
                    <div>
                        <div style="position:fixed;top:0px; left:0px; width:100%">
                          <div style="padding: 5px; font-size: 15px; text-align: center; font-weight: bold; background-color:#0094ff;color:white;">
                            Crew Availability expire in next
                            <asp:Label ID="lblAvailabilityDays" runat="server"></asp:Label>
                            days
                            <asp:Label runat="server" ID="lblRcount51"></asp:Label>
                        </div>
                          <div style="background-color: #cac4c4; padding: 5px;">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;">
                            <tr>
                                <td style="width: 120px">Recruitment Off. :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlRO" Style="" class="input_box"></asp:DropDownList></td>
                                <td style="width: 60px">Crew # :</td>
                                <td style="text-align: left"><asp:TextBox runat="server" ID="txtCrewNo" Style="width: 50px" class="input_box" MaxLength="6"></asp:TextBox></td>
                                <td style="width: 80px">Crew Name :</td>
                                <td style="text-align: left"><asp:TextBox runat="server" ID="txtCrewName" Style="" class="input_box"></asp:TextBox></td>
                                <td style="width: 50px">Rank :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlRank" Style="" class="input_box"></asp:DropDownList></td>
                                <td style="width: 80px">Nationality :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlNat" Style="" class="input_box"></asp:DropDownList></td>
                                <td style="width: 120px">Expire in next :</td>
                                <td style="text-align: left; width:50px"><asp:TextBox ID="txtExpireIn" runat="server" Width="30px" CssClass="NumberValidation" MaxLength="4" style="text-align:center" ></asp:TextBox></td>
                                <td style=" width:50px; text-align:left;">days</td>
                                <td><asp:Button runat="server" ID="btn_Show51" Text="Show" OnClick="btn_Show51_Click"></asp:Button></td>
                            </tr>
                        </table>
                    </div>
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
                            <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                            <colgroup>
                                <col width="30px" />
                                <col width="70px" />
                                <col width="220px" />
                                <col width="60px" />
                                <col width="70px" />
                                <col width="70px" />
                                <col width="80px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col  />
                                <%--<col width="200px" />--%>
                                <col width="50px" />
                                <tr>
                                    <td style="text-align: center;">Sr#</td>
                                    <td style="text-align: center;">Crew #</td>
                                    <td>&nbsp;Crew Name</td>
                                    <td style="text-align: center;">Rank</td>
                                    <td style="text-align: center;">Status</td>
                                    <td style="text-align: center;">Rec. off.</td>
                                    <td style="text-align: center;">Last Vessel</td>
                                    <td style="text-align: center;">Signoff Date</td>
                                    <td style="text-align: center;">Exp. Join Dt.</td>
                                    <td style="text-align: center;">DAYS ON LEAVE</td>
                                    <td style="text-align: center;">Avl Date</td>
                                    <td style="text-align: center;">Remark / Updated By & On</td>
                                    <%--<td style="text-align: center;">Updated By/On</td>--%>
                                    <td style="text-align: center;">&nbsp;</td>
                                </tr>
                            </colgroup>
                        </table>
                            </div>
                        <div style="margin-top:85px;">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                            <colgroup>
                                <col width="30px" />
                                <col width="70px" />
                                <col width="220px" />
                                <col width="60px" />
                                <col width="70px" />
                                <col width="70px" />
                                <col width="80px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col width="100px" />
                                <col />
                                <%--<col width="200px" />--%>
                                <col width="50px" />
                            </colgroup>
                        <asp:Repeater ID="rpt51" runat="server">
                            <ItemTemplate>
                                <tr class="DataRow">
                                    <td style="text-align: center;"><%#Eval("Sno")%></td>
                                    <td style="text-align: center;"><%#Eval("CREWNUMBER")%>
                                        <asp:HiddenField ID="hfCrewId" runat="server" Value='<%#Eval("CrewId")%>' />
                                    </td>
                                    <td style="text-align: left">&nbsp;<%#Eval("CrewName")%></td>
                                    <td style="text-align: center;"><%#Eval("RankCode")%></td>
                                    <td><%#Eval("CREWSTATUSNAME")%></td>
                                    <td><%#Eval("RECRUITINGOFFICENAME")%></td>
                                    <td style="text-align: center;"><%#Eval("VesselCode")%></td>
                                    <td style="text-align: center;"><%#Common.ToDateString(Eval("SignOffDate"))%></td>
                                    <td style="text-align: center;"><%#Common.ToDateString(Eval("EXPECTEDJOINDATE"))%></td>
                                    <td style="text-align: center;"><%#Eval("DAYS_ON_LEAVE")%></td>
                                    <td style="text-align: center;"><%#Common.ToDateString(Eval("AvailableFrom"))%>
                                        <asp:HiddenField ID="hfAvailablefrom" runat="server" Value='<%#Common.ToDateString(Eval("AvailableFrom"))%>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <%#Eval("AvalRemark")%>
                                        <div style="color:blue; padding:3px; text-align:right; font-weight:bold;">
                                           <i><span style="color:crimson"><%#Eval("Avl_By")%></span> | <%#Common.ToDateString(Eval("AvlOn"))%></i>
                                        </div>
                                        <asp:HiddenField ID="hfAvalRemark" runat="server" Value='<%#Eval("AvalRemark")%>' />
                                    </td>
                                    <%--<td style="text-align: left; "><%#Eval("Avl_By")%>/ <%#Common.ToDateString(Eval("AvlOn"))%></td>--%>
                                    <td style="text-align: center;">
                                        <asp:ImageButton ID="lnlEdit" runat="server" OnClick="lnlEdit_OnClick" ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit"></asp:ImageButton>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>&nbsp;</td>
                                    <td colspan="10"><%#Eval("AvalRemark")%></td>
                                    <td>&nbsp;</td>
                                </tr>--%>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
                    </div>
                    <div style="margin-top: 5px; height: 25px; text-align: right;">
                        <asp:UpdateProgress ID="UpdateProgress51" runat="server" AssociatedUpdatePanelID="UpdatePanel51">
                            <ProgressTemplate>
                                <center>
                                    <div style="float: left;">
                                        <img src="../Images/loading.gif" />
                                        Loading .... Please Wait
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div style="position: absolute; z-index: 110; top: 100px; left: 0px; width:100%; text-align: center;" id="dv_Pop51" runat="server" visible="false">
                    <center>
                        <div style="border: solid 5px #333; width: 650px; background-color: White; text-align: left;padding: 10px;background-color: White;">
                           
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: right;" class="style2">
                                            Next Available Date : 
                                        </td>
                                        <td style="text-align: left;" class="style4">
                                            <asp:TextBox ID="txt_AvailableDt" runat="server"  MaxLength="20"
                                                TabIndex="4" Width="80px"></asp:TextBox>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                        </td>
                                        <td style="text-align: left" class="style6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                        </td>
                                        <td style="text-align: left;" class="style4">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_AvailableDt"
                                                ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$"
                                                ErrorMessage="Invalid Date."></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;" class="style2">
                                            Remark : 
                                        </td>
                                        <td class="style4" style="text-align: left;">
                                            <asp:TextBox ID="txtAvlRem" runat="server" CssClass="input_box" Height="150px" MaxLength="19" TabIndex="2" TextMode="MultiLine" Width="439px"></asp:TextBox>
                                        </td>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style4" style="text-align: left;">
                                            &nbsp;
                                        </td>
                                        <td class="style6">
                                            
                                        </td>
                                    </tr>
                                </table>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="ImageButton2" TargetControlID="txt_AvailableDt" PopupPosition="TopLeft">
                            </ajaxToolkit:CalendarExtender>
                            <table border="0" width="100%">
                                <tr>
                                    <td style="text-align:center">
                                        <asp:Button ID="Button2" runat="server" CssClass="btn" OnClick="btn_Update_AvailabelDate_Click" TabIndex="7" Text="Update" Visible="true" Width="59px" />
                                        <asp:Button ID="btnClose" runat="server" CssClass="btn"  Text="Close" OnClick="btnClose_OnClick" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%"></asp:Label></td>
                                        <asp:HiddenField ID="HiddenPK" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            
                            
                        </div>
                    </center>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--//-----------------------------------------------------------------------------------------------------------------%>
        <script type="text/javascript">
        $(document).ready(function () {
            $(".NumberValidation").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });
    </script>
    </asp:Content>

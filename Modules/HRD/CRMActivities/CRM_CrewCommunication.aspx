<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRM_CrewCommunication.aspx.cs" Inherits="CRM_CrewCommunication" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/KPIScript.js"></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
    <style type="text/css">
        .bordered tr td
        {
            border :solid 1px #e5e5e5;
        }

    </style>
</head>
<body style="margin: 0 0 0 0;">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
        <center>
                    <div>
                    <div style="position:fixed;top:0px; left:0px; width:100%">
                          <div style="padding: 5px; font-size: 15px; text-align: center; font-weight: bold;" class="text headerband">
                            Crew Availability 
                        </div>
                          <div style="background-color: #cac4c4; padding: 5px;">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;">
                            <tr>
                                <td style="width: 120px">Recruitment Off. :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlRO" Style="" Width="120px" class="input_box"></asp:DropDownList></td>
                                <td style="width: 50px">Rank :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlRank" Style="" Width="120px" class="input_box"></asp:DropDownList></td>
                                <td style="width: 80px">Nationality :</td>
                                <td style="text-align: left"><asp:DropDownList runat="server" ID="ddlNat" Width="120px" Style="" class="input_box"></asp:DropDownList></td>
                                <td style="width: 80px">Off/Rat :</td>
                                <td style="text-align: left">
                                    <asp:DropDownList runat="server" ID="ddlOR" Style="" class="input_box">
                                    <asp:ListItem Text="< All >" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Officer" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="Rating" Value="R"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 120px"> Availability in Month :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID = "ddlMonths" runat="server" Width="120px" class="input_box">
</asp:DropDownList>
                                    </td>
                                <td>
                                    <asp:Button runat="server" ID="btn_Show" CssClass="btn" Text="Show" OnClick="btn_Show_Click"></asp:Button>
                                    <asp:Button runat="server" ID="btn_Send" CssClass="btn" Text="Send Mail" OnClick="btn_Send_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;height: 25px; background-color: Orange; color: White;" class="bordered">
                          <colgroup>
                                <col width="40px" />
                                <col width="40px" />
                                <col width="70px" />
                                <col />
                                <col width="100px" />
                                <col width="150px" />
                                <col width="100px" />
                                <col width="220px" />
                                <col width="90px" />
                                <col width="90px" />
                                <col width="150px" />
                            </colgroup>
                                <tr class= "headerstylegrid">
                                    <td style="text-align: center;"><input type="checkbox" onclick="selectall(this);" /></td>
                                    <td style="text-align: center;">Sr#</td>
                                    <td style="text-align: center;">Crew #</td>
                                    <td>&nbsp;Crew Name</td>
                                    <td style="text-align: center;">Rank</td>
                                    <td style="text-align: center;">Nationality</td>
                                    <td style="text-align: center;">Rec. off.</td>
                                    <td style="text-align: center;">Last Vessel</td>
                                    <td style="text-align: center;">Sign Off Dt.</td>
                                    <td style="text-align: center;">Avl From</td>
                                    <td style="text-align: center;"></td>
                                </tr>
                        </table>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0" style="border-collapse: collapse;" class="bordered">
                            <colgroup>
                                <col width="40px" />
                                <col width="40px" />
                                <col width="70px" />
                                <col />
                                <col width="100px" />
                                <col width="150px" />
                                <col width="100px" />
                                <col width="220px" />
                                <col width="90px" />
                                <col width="90px" />
                                <col width="150px" />
                            </colgroup>
                        <asp:Repeater ID="rpt51" runat="server">
                            <ItemTemplate>
                                <tr <%# FormatColorRow(DataBinder.Eval(Container.DataItem,"days").ToString()) %>>
                                    <td style="text-align: center;">
                                        <asp:CheckBox runat="server" Visible='<%#(Eval("Email").ToString().Trim()!="")%>' ID="chkSel" CssClass="cclist" title='<%#Eval("Email")  + ", "  + Eval("MobileNumber")%>' />
                                        <img src="~/Modules/HRD/Images/exclamation.gif" runat="server"  Visible='<%#(Eval("Email").ToString().Trim()=="")%>' />
                                    </td>
                                    <td style="text-align: center;"><%#Eval("Sno")%></td>
                                    <td style="text-align: center;"><%#Eval("CREWNUMBER")%>
                                        <asp:HiddenField ID="hfCrewId" runat="server" Value='<%#Eval("CrewId")%>' />
                                        <asp:HiddenField ID="hfdEmail" runat="server" Value='<%#Eval("Email")%>' />
                                    </td>
                                    <td style="text-align: left">&nbsp;<%#Eval("CrewName")%></td>
                                    <td style="text-align: center;"><%#Eval("RankCode")%></td>
                                    <td><%#Eval("CountryName")%></td>
                                    <td><%#Eval("RECRUITINGOFFICENAME")%></td>
                                    <td style="text-align: left;"><%#Eval("LastVesselName")%></td>
                                    <td style="text-align: center;"><%#Common.ToDateString(Eval("SignOffDate"))%></td>
                                     <td style="text-align: center;"><%#Common.ToDateString(Eval("AvailableFrom"))%>
                                        <asp:HiddenField ID="hfAvailablefrom" runat="server" Value='<%#Common.ToDateString(Eval("AvailableFrom"))%>' />
                                          <asp:HiddenField ID="hfAvalRemark" runat="server" Value='<%#Eval("AvalRemark")%>' />
                                    </td>
                                    
                                    <td style="text-align: center;">
                                        <asp:LinkButton ID="lnlEdit" runat="server" OnClick="lnlEdit_OnClick" Text="Update Avl Dt"></asp:LinkButton>
                                    </td>
                                </tr>
                           
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div>

                    
                    </div>
                    <div style="position: fixed; z-index: 110; top: 0px; left: 0px; width:100%; height:100%; text-align: center; background:rgba(5,78,96,0.5);" id="dv_Pop" runat="server" visible="false">
                                <center>
                                  <div style="border: solid 5px #333; width: 95%; background-color: White; text-align: left;padding: 0px;background-color: White; position:relative;top:50px; font-size:13px;">
                    <div style="padding:3px; text-align:center;  font-size:18px;" class="text headerband"> Send Email </div>
                            <div style="padding:5px;">
                                <div style="padding:3px; text-align:center;"> 
                                    <asp:Label runat="server" ID="txtMessage" ForeColor="Red"></asp:Label>
                                </div>
                                <div style="padding:3px">
                                    <table width="100%">
                                        <tr>
                                            <td style="width:20%;padding-right:5px;text-align:right;">  To Address :</td>
                                            <td style="width:40%;padding-left:5px;"> <asp:TextBox runat="server" Width="98%" Height="95%" id="txtToAddress" Enabled="false"  style=" padding:5px;"></asp:TextBox>  </td>
                                            <td style="width:40%;padding-left:5px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%;padding-right:5px;text-align:right;">  Subject :</td>
                                            <td style="width:40%;padding-left:5px;"> <asp:TextBox runat="server" Width="98%" Height="95%" id="txtSubject" style=" padding:5px;"></asp:TextBox>  </td>
                                            <td style="width:40%;padding-left:5px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%;padding-right:5px;text-align:right;">  Attachment : </td>
                                            <td style="width:40%;padding-left:5px;"> <asp:FileUpload runat="server" Width="98%" Height="95%" id="flpFile" style=" padding:5px;"></asp:FileUpload>  </td>
                                            <td style="width:40%;padding-left:5px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%;padding-right:5px;text-align:right;">  Message : </td>
                                            <td  colspan="2"  style="padding-left:5px;"> <asp:TextBox runat="server" Width="98%" Height="300px" id="txtMesssage" TextMode="MultiLine" style="display:none"></asp:TextBox>
                                    <div style="width:98% ; height:300px; border:solid 1px #cac4c4; line-height:20px; padding:5px;" contenteditable="true" id="dvmsg" >
                                        <%=txtMesssage.Text%>
                                    </div>  </td>
                                           
                                        </tr>
                                    </table> 
                                    </div>
                               
                                
                                    
                                    

                                </div>
                                <div style="text-align:center; padding:5px;">
                                    <asp:Button runat="server" ID="btnFinalSend" Text="Send Mail" CssClass="btn" OnClick="btnFinalSend_Click" OnClientClick="CopyText(this);"></asp:Button>
                                    <asp:Button runat="server" ID="btnClose" CssClass="btn" Text="Close" OnClick="btnClose_Click"></asp:Button>
                                </div>
                            </div>
                     </center>      
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
                                        <asp:Button ID="btn_Update_AvailabelDate" runat="server" CssClass="btn" OnClick="btn_Update_AvailabelDate_Click" TabIndex="7" Text="Update" Visible="true" Width="59px" />
                                        <asp:Button ID="btnClose_AvailabelDate" runat="server" CssClass="btn"  Text="Close" OnClick="btnClose_AvailabelDate_OnClick" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1" Width="100%"></asp:Label>
                                         <asp:HiddenField ID="HiddenPK" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            
                            
                        </div>
                    </center>
                </div>
                </center>
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

        function selectall(ctl)
        {
            $(".cclist>input").prop('checked', $(ctl).is(":checked"));
        }

        function CopyText(ctl) {
            $(ctl).val('Processing... Please wait.');
            $("#txtMesssage").val($("#dvmsg").html());
        }
        
    </script>
    </form>
  
</body>
</html>

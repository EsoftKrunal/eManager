<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="VIQPreparation.aspx.cs" Inherits="Vetting_VIQPreparation" Title="Untitled Page"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    .bar
    {
        background-color:LightGreen; 
        height:14px;
    }
    </style>
<script type="text/javascript">
    var pageid = 2;
    $.ajaxSetup({ cache: false });

    function ShowAddDiv() {

        $('#dvAdd').slideToggle();
        $("#<%=radSireAdd.ClientID%>").focus();
        
        $('#dvAdd').keyup(function () {
            if (event.keyCode == 27) {
                    $('#dvAdd').hide(); 
            } 
        });
        }

    

    function SetDate() {
        var Group = $("#<%=radSireAdd.ClientID%>").attr('checked') ? 1 : 2;
        var Vsl = $("#<%=ddlVesselAdd.ClientID%>").val();
        
        $.get("getPartialData.ashx?PageId=" + pageid + "&RequestData=NextDueDate&Param=" + Vsl + '|' + Group,
                    function (data) {
                        var obj = $.parseJSON(data);

                        $("#spn_LD").html(obj.LastInspName + ' / ' + obj.LastDoneDate);
                        $("#spn_NID").html(obj.NextDueDate);
                        $("#SPN_lviq").html(obj.LastVIQDate);
                        

                    });
                }


    function Page_CallAfterRefresh() {
        $(document).ready(function () {
            $("#<%=ddlVesselAdd.ClientID%>,#<%=radSireAdd.ClientID%>,#<%=radCdiAdd.ClientID%>").change(function () {
                SetDate();
            });
        });
    }
    Page_CallAfterRefresh();
</script>
  <script type="text/javascript">
      function OpenVIQDetails(vslcode,vid) {
          window.open('VIQDetails.aspx?VSLCODE=' + vslcode + '&VIQId=' + vid, '');
      }
    </script>
<!-- POPUP DIV START -->
<div style="position:absolute; width:100%; top:0px;left:0px; height:100%; display:none;" id="dvAdd" >
<div style="position:static;top:0px; left:0px;opacity:0.4;filter:alpha(opacity=40); background-color:Black; height:100%; width:100%;"></div>
    <center>
    <div style="position:absolute; top:0px; left:0px; width:100%;"> 
        <center>
        <div style="width:400px; margin-top:50px; background-color:White; border:solid 15px #66C285; text-align:left; padding:3px;" >
        <div style="padding:5px; background-color:#FFD1A3; text-align:center; font-size:14px;"><b>VIQ For Ship</b></div>
            <table border="1" cellpadding="3" cellspacing="0" style="text-align: center; background-color:#E2E2E2; border-collapse:collapse;" width="100%" bordercolor="gray">
            <tr>
                <td>SIRE / CDI </td>
                <td style="text-align: left;">
                <asp:RadioButton Text="SIRE" ID="radSireAdd" GroupName="grpAdd" runat="server" Checked="true" />
                <asp:RadioButton Text="CDI" ID="radCdiAdd" GroupName="grpAdd" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>Vessel : </td>
                <td style="text-align: left;"><asp:DropDownList ID="ddlVesselAdd" runat="server" CssClass="input_box" Width="90%" TabIndex="4" ></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlVesselAdd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Last Insp Done :</td>
                <td><span id="spn_LD"></span> </td>
            </tr>
            <tr>
                <td>Next Insp Due :</td>
                <td><span id="spn_NID"></span> </td>
            </tr>
            <tr>
                <td>Last VIQ Closure Date  :</td>
                <td><span id="SPN_lviq"></span> </td>
            </tr>
            <tr>
                <td>VIQ Closure Date : </td>
                <td style="text-align: left;">
                    <asp:TextBox runat="server" id="txt_ClosureDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="r1d" ControlToValidate="txt_ClosureDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:ImageButton id="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
                    <ajaxToolkit:CalendarExtender id="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txt_ClosureDate"></ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            
            </table>
            <center style=" margin-top:5px; margin-bottom:5px">
                <asp:Button runat="server" ID="btnCreateNow" Text="Save" CssClass="Btn1" OnClick="btnCreateNow_Click" Width="90px" OnClientClick="DisableMe(this);"/>
                <input type="button" id='btnClose' value="Close" class="Btn1" onclick="$('#dvAdd').slideToggle();" />
            </center>
        </div>
        </center>
    </div>
    </center>
</div>
<!-- POPUP DIV END -->

    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <asp:UpdatePanel runat="server" id="up1">
                <ContentTemplate>
              

                    <table border="1" cellpadding="3" cellspacing="0" style="text-align: center; background-color:#E2E2E2; border-collapse:collapse;" width="100%" bordercolor="gray">
                      <tr>
                          <td style="text-align: left;">
                            <asp:RadioButton Text="SIRE" ID="radSire" GroupName="grp" runat="server" Checked="true" />
                            <asp:RadioButton Text="CDI" ID="radCdi" GroupName="grp" runat="server"/>
                          </td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" Width="90px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"></asp:DropDownList></td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlVessel" runat="server" CssClass="input_box" Width="203px" TabIndex="4" ></asp:DropDownList></td>
                          <td style="text-align:left"><asp:CheckBox runat="server" ID="chk_Inactive" OnCheckedChanged="chk_Inactive_OnCheckedChanged" Text="Include Inactive Vessels" AutoPostBack="true"  />  </td>
                          <td style="text-align: right; padding-right: 5px;">Period :</td>
                          <td style="text-align: left">
                            <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
                            <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton> 
                            &nbsp;&nbsp; TO
                            <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox>
                            <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif"></asp:ImageButton>
                            <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender> 
                          </td>
                          <td style="text-align:center"> 
                            <asp:Button runat="server" ID="btnShow" CausesValidation="false" Text="Show" CssClass="Btn1" OnClick="btnShow_Click" Width="90px" OnClientClick="DisableMe(this);"/> 
                          </td>
                          <td style="text-align:center"> 
                            <asp:Button runat="server" ID="btnCreateNew" Text="Create New" CssClass="Btn1" Width="90px" OnClientClick="ShowAddDiv();"/>
                          </td>
                          <td style="text-align: center"><asp:ImageButton runat="server" ID="btnHome" ImageUrl="~/Images/home.png" PostBackUrl="~/Vetting/VIQHome.aspx" CausesValidation="false" /> </td>
                      </tr>
                </table>
                </ContentTemplate>
                 <Triggers>
                    <asp:PostBackTrigger ControlID="btnShow" />
                </Triggers>
                </asp:UpdatePanel>
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom: 2px">
                    <tr>
                        <td>
                            <div style="height:23px; overflow-y:scroll;overflow-x:hidden;">
                            <table cellpadding="1" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#07B3D3">
                            <thead>
                                <tr >
                                    <td style="width:30px;color:White;">Sr#</td>
                                    <%--<td style="width:30px;color:White;">View</td>--%>
                                    <td style="color:White;">Vessel Name</td>
                                    <td style="width:150px;color:White;">VIQ #</td>
                                    <td style="width:80px;color:White;">SIRE /CDI </td>
                                    <td style="width:100px;color:White; text-align:center;">Closure Date</td>
                                    <td style="width:200px;color:White;">Progress Bar</td>
                                    <td style="width:100px;color:White;">Send to Ship</td>
                                    <td style="width:20px;color:White;">&nbsp;</td>
                                    <td style="width:20px;color:White;">&nbsp;</td>
                                </tr>
                            </thead>
                            </table>
                            </div>
                            <div style="height:370px; overflow-y:scroll;overflow-x:hidden;" class='ScrollAutoReset' id='dv_Questions'>
                            <table cellpadding="1" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#07B3D3">
                            <asp:Repeater runat="server" ID="rpt_Questions"> 
                            <ItemTemplate>
                            <tr>
                                <td style="width:30px"><%#Eval("SNO")%></td>
                                <%--<td style="width:30px">
                                    <img src="../Images/HourGlass1.gif" alt="Edit" onclick="ShowEditDiv(<%#Eval("VIQId")%>)" style="cursor:pointer"/>
                                </td>--%>
                               
                                <td style="text-align:left;"><%#Eval("VesselName")%></td>
                                <td style="width:150px"><%#Eval("VIQNO")%></td>
                                <td style="width:80px;text-align:center;" ><%#(Common.CastAsInt32(Eval("InspGroupId"))==1)?"SIRE":"CDI"%></td>
                                <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("TargetDate"))%></td>
                                <td style="width:100px;text-align:center;width:200px;">
                                 <div style="width:180px; background-color:White; border:solid 1px green; height:14px; position:relative; cursor:pointer;" onclick='OpenVIQDetails(<%#"\"" + Eval("VesselCode").ToString() + "\""%>,<%#Eval("VIQID")%>);'>
                                    <div style="position:relative; z-index:100; top:-4px; text-align:center; color:Red;"><%#Eval("DONEQ")%> of <%#Eval("NOQ")%></div>
                                    <div style='position:absolute; top:0px ;left:0px;width:<%#Eval("PERDONE")%>%' class='bar'></div>
                                </div>
                                </td>
                                 <td style="width:100px">
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/mail.png" OnClientClick="this.src='../Images/progress_16.gif';" OnClick="btnDownloadAndMail_Click" CausesValidation="false" CommandArgument='<%#Eval("VIQId")%>' CssClass='<%#Eval("VesselCode")%>' />
                                </td>
                                <td style="text-align:center;width:20px;">
                                        <span runat="server" visible='<%#Eval("VIQType").ToString()=="1" && Auth.IsAdd%>'>
                                            <a target="_blank" title="Sub Questionnaire" href='AddSubVIQDetails.aspx?VSL=<%#Eval("VesselCode")%>&PVIQId=<%#Eval("VIQID")%>&VIQID=0'><img src="../Images/addX12.jpg" /></a>   
                                        </span>
                                        <span runat="server" visible='<%#(Eval("VIQType").ToString()=="2") && (Eval("VIQStatus").ToString()=="0") && (Eval("MailSent").ToString()=="0") && Auth.IsUpdate%>'>
                                            <a target="_blank" title="Sub Questionnaire" href='AddSubVIQDetails.aspx?VSL=<%#Eval("VesselCode")%>&PVIQId=<%#Eval("PVIQID")%>&VIQID=<%#Eval("VIQID")%>'><img src="../Images/editX12.jpg" /></a>   
                                        </span>
                                </td>
                                <td style="text-align:left;width:20px;">&nbsp;</td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%; background-color:#F0FAFF;">
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
                </fieldset>
                </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
     <script type="text/javascript">
         $(".autohide").click(function () {
             if ($(this).css('overflow') == 'hidden') {
                 $(this).css('height', '');
                 $(this).css('overflow', 'auto');
             }
             else {
                 $(this).css('height', '20px');
                 $(this).css('overflow', 'hidden');
             }
         });
     </script>
</asp:Content>


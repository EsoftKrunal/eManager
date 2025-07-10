<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="VIQ.aspx.cs" Inherits="Vetting_VIQ" Title="Untitled Page"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    var pageid = 1;
    $.ajaxSetup({ cache: false });
    function ShowEditDiv(id) 
    {
            if(!($('#dvEdit').is(":visible")))
            {
                $.get("getPartialData.ashx?PageId=" + pageid + "&RequestData=QuestionDetails&Param=" + id,
                    function (data) {
                        var obj = $.parseJSON(data);
                        $("#<%=hfdQid.ClientID%>").val(id);
                        $("#spnQno").html(obj.Qno);
                        $("#<%=txtOfficeRemarks.ClientID%>").val(obj.OfficeRemarks);
                        $("#<%=chkPreVetting.ClientID%>").attr('checked', obj.PreVetting==1);
                        $("#<%=spn_Q.ClientID%>").val(obj.Question);
                        $("#<%=spn_G.ClientID%>").val(obj.Description);

                        $.each($("#cvChks").find(":checkbox"), function (i, d) {
                            $(d).attr('checked', obj.Responsibilites[i] == 1);
                        });

                        $.each($("#cvChks1").find(":checkbox"), function (i, d) {
                            $(d).attr('checked', obj.Responsibilites1[i] == 1);
                        });
                    });
             }
                $('#dvEdit').slideToggle();
    }

</script>
<!-- POPUP DIV START -->
<div style="position:absolute; width:100%; top:0px;left:0px; height:100%; display:none;" id="dvEdit" >
    <center>
        <div style="width:900px; height:400px; margin-top:50px; background-color:White; border:solid 15px #66C285; text-align:left; padding:3px;" >
            <asp:UpdatePanel runat="server" ID="up2">
            <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfdQid" />
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">Question : <span id="spnQno"></span></span>
            <div style=""><textarea id="spn_Q" runat="server" style="border:none; width:100%; height:35px; background-color:White; border:solid 1px #e2e2e2;"/></div>
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">VIQ Guidance : </span>
            <div style=""><textarea id="spn_G" runat="server" style="border:none; width:100%; height:65px; background-color:White; border:solid 1px #e2e2e2;" /></div>
            <div style=" display:none;">
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">Pre Vetting : </span><asp:CheckBox runat="server" ID="chkPreVetting" />
            </div>
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">Office Guidance : </span>
            <asp:TextBox runat="server" id="txtOfficeRemarks" Width="98%" TextMode="MultiLine" Height="95px"></asp:TextBox>
            
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">Responsibility : </span>
            <div id='cvChks'>
                <asp:CheckBoxList runat="server" ID="chklstRanks" RepeatColumns="14" RepeatDirection="Horizontal" /> 
            </div>
            <span style="color: Maroon ; font-size:12px; font-weight:bold;">Involved Ranks : </span>
            <div id='cvChks1'>
                <asp:CheckBoxList runat="server" ID="chklstRanks1" RepeatColumns="18" RepeatDirection="Horizontal" /> 
            </div>
            <center>
                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="Btn1" OnClick="btnSave_Click" />
                <asp:Button runat="server" ID="btnClosePOP" Text="Close" CssClass="Btn1" OnClick="btnClosePOP_Click" OnClientClick="DisableMe(this);" />
                <%--<input type="button" id='btnClose' value="Close"  class="Btn1" onclick="$('#dvEdit').slideToggle();" />--%>
            </center>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnClosePOP" />
            </Triggers>
            </asp:UpdatePanel>
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
                <asp:UpdatePanel runat="server" id="up1">
                <ContentTemplate>
              <table border="1" cellpadding="3" cellspacing="0" style="text-align: center; background-color:#E2E2E2; border-collapse:collapse;" width="100%" bordercolor="gray">
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Inspection Group:</td>
                          <td style="text-align: left;">
                            <asp:RadioButton Text="SIRE" ID="radSire" GroupName="grp" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="radSireCdi_OnCheckedChanged" />
                            <asp:RadioButton Text="CDI" ID="radCdi" GroupName="grp" runat="server" AutoPostBack="true" OnCheckedChanged="radSireCdi_OnCheckedChanged"/>
                          </td>
                          <td align="right" style="text-align: right; padding-right:5px;">Chapter:</td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlChapterName" runat="server" CssClass="input_box" Width="253px" TabIndex="2" ></asp:DropDownList></td>
                          <td align="right" style="text-align: right; padding-right:5px;">Vessel Type:</td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlVesselType" runat="server" CssClass="input_box" Width="203px" TabIndex="4" ></asp:DropDownList></td>
                          <td style="text-align: right; padding-right: 5px;">QNo.</td>
                          <td style="text-align: left"><asp:TextBox runat="server" Width="50px" id="txtQno" CssClass="input_box"></asp:TextBox></td>
                          <td style="text-align: center"><asp:Button runat="server" ID="btnShow" Text="Show" CssClass="Btn1" OnClick="btnShow_Click" Width="90px" OnClientClick="DisableMe(this);"/> </td>
                          <td style="text-align: center"><asp:ImageButton runat="server" ID="btnHome" ImageUrl="~/Images/home.png" PostBackUrl="~/Vetting/VIQHome.aspx" CausesValidation="false"/> </td>
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
                                    <td style="width:30px;color:White;">Edit</td>
                                    <td style="width:150px;color:White;">
                                        Vessel Type</td>
                                    <td style="width:80px;color:White;">
                                        Version #</td>
                                    <td style="width:80px;color:White;">
                                        Question #</td>
                                    <td style="text-align:left;color:White;">
                                        Question</td>
                                    <td style="text-align:center;color:White; width:80px;">VIQ Guidance</td>
                                    <td style="text-align:center;color:White; width:80px;">Off.Guidance</td>
                                    <td style="text-align:left;color:White; width:200px;">Responsibility</td>
                                    <td style="text-align:left;color:White; width:20px;">&nbsp;</td>
                                </tr>
                            </thead>
                            </table>
                            </div>
                            <div style="height:410px; overflow-y:scroll;overflow-x:hidden; border:solid 1px #c2c2c2;" class='ScrollAutoReset' id='dv_Questions'>
                            <table cellpadding="1" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#07B3D3">
                            <asp:Repeater runat="server" ID="rpt_Questions"> 
                            <ItemTemplate>
                            <tr>
                                <td style="width:30px"><%#Eval("SNO")%></td>
                                <td style="width:30px">
                                    <span runat="server" visible='<%#Auth.IsUpdate%>'>
                                        <img src="../Images/editX12.jpg" alt="Edit" onclick="ShowEditDiv(<%#Eval("Id")%>)" style="cursor:pointer"/>
                                    </span>
                                </td>
                                <td style="width:150px; text-align:left;"><%#Eval("VslTypeName")%></td>
                                <td style="width:80px"><%#Eval("VersionName")%></td>
                                <td style="width:80px;text-align:left;" ><%#Eval("QuestionNo")%></td>
                                <td style="text-align:left;">
                                    <div style="height:20px;overflow:hidden;" class='autohide' title='Click to see/hide whole text..'> 
                                        <%#Eval("Question")%>
                                    </div>
                                </td>
                                <td style="text-align:left;width:80px; text-align:center;">
                                    <a href='../Registers/CheckListDescPopUp.aspx?ChkLstId=<%#Eval("Id")%>' style='display:<%#Eval("Desc_Visible")%>' target="_blank"><img src="../Images/cv.png" style="height:12px"  alt="Guidance"/></a>
                                </td>
                                <td style="text-align:left;width:80px; text-align:center; vertical-align:middle;">
                                    <a href='../Registers/CheckListDescPopUp.aspx?ChkLstId=<%#Eval("Id")%>&Mode=O' style='display:<%#Eval("OfficeRemarks_Visible")%>' target="_blank"><img src="../Images/icon_comment.gif" alt="Office Remarks"/></a>
                                </td>
                                <td style="text-align:left;width:200px;"><%#Eval("Responsibilites")%></td>
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


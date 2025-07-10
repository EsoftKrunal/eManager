<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselDetailsGeneral.ascx.cs" Inherits="VesselRecord_VesselDetailsGeneral"  %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"  %>
<style type="text/css">
    .style1
    {
        width: 127px;
        height: 5px;
    }
    .style2
    {
        width: 120px;
        height: 5px;
    }
    .style3
    {
        width: 132px;
        height: 5px;
    }
    .style4
    {
        width: 98px;
        height: 5px;
    }
    .style5
    {
        width: 140px;
        height: 5px;
    }
    .style6
    {
        text-align: center;
    }
    .style8
    {
        width: 127px;
        height: 13px;
    }
    .style9
    {
        width: 120px;
        height: 13px;
    }
    .style10
    {
        width: 132px;
        height: 13px;
    }
    .style11
    {
        width: 134px;
        height: 13px;
    }
    .style12
    {
        width: 98px;
        height: 13px;
    }
    .style13
    {
        width: 140px;
        height: 13px;
    }
    .auto-style1 {
        width: 134px;
    }
    .auto-style2 {
        height: 19px;
        width: 134px;
    }
    .auto-style3 {
        height: 18px;
        width: 134px;
    }
    .auto-style4 {
        width: 134px;
        height: 5px;
    }
    .auto-style5 {
        height: 10px;
        width: 134px;
    }
    .auto-style6 {
        width: 127px;
        height: 10px;
    }
    .auto-style7 {
        width: 120px;
        height: 10px;
    }
    .auto-style8 {
        width: 132px;
        height: 10px;
    }
    .auto-style9 {
        width: 140px;
        height: 10px;
    }
</style>
<script type="text/javascript">
 function ShowPrint()
 {
    var str;
    str='<% Response.Write(this.VesselId.ToString()); %>';
    window.open('../Reporting/Vessel_Details_Report.aspx?VID=' + str);
 }
 </script>
 <link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<table cellspacing="0" cellpadding="0" width="100%" style="font-family:Arial;font-size:12px;">
            <tr>
                <td style="width: 100%">
                <div id="vessel_general_div" runat="server" style="width:100%">
                    <table width="100%" cellpadding="0" cellspacing ="0" border ="1" >
                         <tr>
                         <td style="text-align:center">
                          <asp:Label ID="lbl_message_general" runat="server" Text="Record Successfully Saved." Visible=False ForeColor="#C00000"></asp:Label>
                          <asp:Label ID="Label1" runat="server" ForeColor="#C00000" Text="Status can't be set Inactive becasuse some crew Members are on this Vessel." Visible="False"></asp:Label>
                          <asp:Label ID="Label2" runat="server" ForeColor="#C00000" Visible="False">Already Entered.</asp:Label></td>
                         </tr>
                         <tr>
                         <td style=" background-color :#e2e2e2" >
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                                              <tr>
                                                 <td style="padding-right:15px; height: 19px; width: 127px; text-align: right;" >Vessel Name:</td>
                                                 <td align="left" style="width: 120px"><asp:TextBox ID="txtvesselname_general" runat="server" CssClass="required_box" MaxLength="49" Width="184px" TabIndex="1"></asp:TextBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtvesselname_general" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                 <td style="padding-right:15px; height: 19px; width: 132px; text-align: right;">Former Name:</td>
                                                 <td align="left" style="width: 134px"><asp:TextBox ID="txtformaer_vessel" runat="server" CssClass="input_box" MaxLength="49" Width="122px" ReadOnly="True" TabIndex="2"></asp:TextBox></td>
                                                 <td style="padding-right:15px; height: 19px; width: 98px; text-align: right;">Flag:</td>
                                                 <td align="left" style="width: 140px"><asp:DropDownList ID="ddflag_general" runat="server" CssClass="required_box" Width="128px" TabIndex="3"></asp:DropDownList>
                                                     <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddflag_general" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                  </tr>
                                          </table>
                         </td></tr>
                         <tr>
                         <td>
                         <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     &nbsp;&nbsp;
                                                     &nbsp;&nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td style="padding-right:15px; height: 19px; width: 127px; text-align: right;">
                                                     Vessel Code:</td>
                                                 <td align="left" style="width: 120px">
                                                     <asp:TextBox ID="txtvessel_code" runat="server" CssClass="required_box" MaxLength="3" TabIndex="3"
                                                         Width="170px"></asp:TextBox></td>
                                                 <td  style="padding-right:15px; height: 19px; width: 132px; text-align: right;">
                                                     Management Type:</td>
                                                 <td align="left" class="auto-style1"><asp:DropDownList ID="ddmgmttype" runat="server" CssClass="required_box" Width="175px" TabIndex="5">
                                                 </asp:DropDownList></td>
                                                 <td  style="padding-right:15px; height: 19px; width: 98px; text-align: right;">
                                                     Owner:</td>
                                                 <td align="left" style="width: 140px"><asp:DropDownList ID="ddowner" runat="server" CssClass="required_box" Width="175px" TabIndex="6">
                                                 </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; " class="style8">
                                                  </td>
                                                  <td align="left" >
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtvessel_code"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td  style="padding-right: 15px; " class="style10">
                                                      </td>
                                                  <td align="left" class="auto-style1" >
                                                      <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddmgmttype"
                                                          ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                  <td  style="padding-right: 15px; " >
                                                  </td>
                                                  <td align="left" >
                                                      <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ddowner"
                                                          ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                              </tr>
                                              <tr>
                                                 <td  style="padding-right:15px; height: 19px; width: 127px; text-align: right;">
                                                     MLC Owner:</td>
                                                 <td align="left" style="height: 19px; width: 120px;"><asp:DropDownList ID="ddl_MLCOwner" runat="server" CssClass="required_box" Width="175px" TabIndex="7" >
                                                 </asp:DropDownList></td>
                                                 <td  style="padding-right:15px; height: 19px; width: 132px; text-align: right;">
                                                     Ship Manager:</td>
                                                 <td align="left" class="auto-style2"><asp:DropDownList ID="ddship_manager" runat="server" CssClass="required_box" Width="175px" TabIndex="8" >
                                                 </asp:DropDownList></td>
                                                 <td  style="padding-right:15px; height: 19px; width: 98px; text-align: right;">
                                                     Manning Agent:</td>
                                                 <td align="left" style="height: 19px; width: 140px;"><asp:DropDownList ID="ddl_ManningAgent" runat="server" CssClass="required_box" Width="175px" TabIndex="9" >
                                                 </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 120px; height: 18px">
                                                  </td>
                                                  <td  style="padding-right: 15px; width: 132px; height: 18px">
                                                  </td>
                                                  <td align="left" class="auto-style3">
                                                      <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="ddship_manager"
                                                          ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                  <td  style="width: 98px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 140px; height: 18px">
                                                  </td>
                                              </tr>
                                            
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 12px; text-align: right;">
                                                      Owner Agent: </td>
                                                  <td align="left" style="width: 120px; height: 12px">
                                                     <%-- <asp:DropDownList ID="ddcrew_nationality1" runat="server" CssClass="input_box" Width="175px" TabIndex="12">
                                                      </asp:DropDownList>--%>
                                                     
                                                      <asp:DropDownList ID="ddlOwnerAgent" runat="server" CssClass="input_box" Width="175px" TabIndex="10">
                                                  </asp:DropDownList>
                                                  </td>
                                                  <td  style="padding-right:15px; height: 19px; width: 132px; text-align: right;">
                                                      
                                                      Account Company:</td>
                                                  <td align="left" class="auto-style2">

                                                      <asp:DropDownList ID="ddlAccountCompany" runat="server" CssClass="input_box" Width="175px" TabIndex="11">
                                                  </asp:DropDownList>
                                                  </td>
                                                  <td  style="height: 12px; width: 98px; padding-right: 15px; text-align: right;">
                                                     Charterer:</td>
                                                  <td align="left" style="width: 140px; height: 12px">
                                                       <asp:DropDownList ID="ddcharterer" runat="server" CssClass="input_box" Width="175px" TabIndex="12">
                                                 </asp:DropDownList></td>

                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; " >
                                                  </td>
                                                  <td align="left" >
                                                      &nbsp;</td>
                                                  <td  style="padding-right: 15px; " >
                                                      &nbsp;&nbsp;
                                                  </td>
                                                  <td  class="auto-style4">
                                                      <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAccountCompany" InitialValue="Please select" ErrorMessage="Required." />
                                                  </td>
                                                  <td align="left" valign="top" >
                                                      &nbsp;</td>
                                              </tr>
                               <tr>
                                                 <td  style="padding-right:15px; height: 14px; width: 127px; text-align: right;" 
                                                      valign="top">
                                                     Crew Nat.(Off):</td>
                                                 <td align="left" style="height: 14px; width: 120px;" valign="top">
                                                    <%-- <asp:DropDownList ID="ddcrew_nationality" runat="server" CssClass="input_box" Width="175px" TabIndex="9">
                                                 </asp:DropDownList>--%>
                                                     <div style="width:200px;height:59px;overflow-y:auto;">  <asp:CheckBoxList ID="ddcrew_nationality" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="1" RepeatDirection="Horizontal" TabIndex="13" Width="175px" Font-Size="11px">
                                            </asp:CheckBoxList> </div>
                                                     
                                                 </td>
                                                 <td  style="padding-right:15px; height: 14px; width: 132px; text-align: right;" 
                                                          valign="top">
                                                     Crew Nat.(Rat):</td>
                                                 <td align="left" style="vertical-align:top;" class="auto-style1" >
                                               <%--  <div style="overflow-x:hidden;overflow-y:scroll;height:57px;border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; width: 140px;">--%>
                                                <%-- </div>--%>
                                                     <div style="width:200px;height:59px;overflow-y:auto;">
                                                         <asp:CheckBoxList ID="ddcrew_nationality1" runat="server" CellPadding="0" CellSpacing="0" Font-Size="11px" RepeatColumns="1" RepeatDirection="Horizontal" TabIndex="14" Width="175px">
                                                         </asp:CheckBoxList>
                                                     </div>
                                                </td>
                                                 <td  style="padding-right:15px; height: 14px; width: 98px; text-align: right;" 
                                                          valign="top">
                                                     Wage Scale:</td>
                                                 <td align="left" style="height: 14px; width: 140px;" valign="top">
                                                    <asp:ListBox ID="cklwagescale" runat="server" CssClass="input_box" TabIndex="15" Width="170px" Height="59px"  ></asp:ListBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; " valign="top" class="auto-style6">
                                                  </td>
                                                  <td align="left" valign="top" class="auto-style7">
                                                  </td>
                                                  <td  style="padding-right: 15px; " valign="top" class="auto-style8">
                                                  </td>
                                                  <td align="left" class="auto-style5">
                                                  </td>
                                                  <td  style="text-align: left;" valign="top" class="auto-style9">
                                                      </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 18px; text-align: right;">
                                                      Vessel Type:&nbsp;</td>
                                                  <td align="left" style="width: 120px; height: 18px">
                                                      <asp:DropDownList ID="ddvesselType_General" runat="server" CssClass="input_box" Width="175px" TabIndex="16">
                                                  </asp:DropDownList></td>
                                                  <td  style="padding-right: 15px; width: 132px; height: 18px; text-align: right;">
                                                      Takeover Dt:</td>
                                                  <td align="left" valign="top" class="auto-style3">
                                                     
                                                       <asp:TextBox ID="txt_EffectiveDate" runat="server" MaxLength="20" CssClass="required_box" TabIndex="17" Width="90px"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="Left" TargetControlID="txt_EffectiveDate">
                    </ajaxToolkit:CalendarExtender>
                                                      <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  />
                                                  </td>
                                                  <td style="padding-right: 15px;height: 18px; text-align: right;">
                                                      Status:</td>
                                                  <td>
                                                       <asp:DropDownList ID="dd_status_general" runat="server" CssClass="input_box" Width="175px" TabIndex="18">
                                                 </asp:DropDownList>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 120px; height: 18px">
                                                  </td>
                                                  <td  style="padding-right: 15px; width: 132px; height: 18px">
                                                             
                                                      </td>
                                                  <td align="left" class="auto-style3">
                                                       <asp:RegularExpressionValidator ID="CompareValidator12" runat="server" ControlToValidate="txt_EffectiveDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                              <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_EffectiveDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  </td>
                                                  <td  style="width: 98px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 140px; height: 18px" valign="top">
                                                     <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dd_status_general"
                                                         ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                                  </td>
                                              </tr>
                             <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 18px; text-align: right;">
                                                      Image :</td>
                                                  <td align="left" style="width: 120px; height: 18px">
                                                      <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input_box" Width="250px" TabIndex="19" /></td>
                                                  <td  style="padding-right: 15px; width: 132px; height: 18px; text-align: right;">
                                                      Vetting Required:
                                                      </td>
                                                  <td align="left" valign="top" class="auto-style3">
                                                      <asp:CheckBox ID="chkVettingRequired" runat="server"  TabIndex="20"/>
                                                      </td>
                                                  <td style="padding-right: 15px;height: 18px; text-align: right;">
                                                     
                                                     P &amp; I Club:</td>
                                                  <td>
                                                      
                                                      <asp:DropDownList ID="ddp_i_club" runat="server" CssClass="input_box" Width="175px" TabIndex="21">
                                                 </asp:DropDownList>
                                                      
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 127px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 120px; height: 18px">
                                                      &nbsp;</td>
                                                  <td  style="padding-right: 15px; width: 132px; height: 18px">
                                                      &nbsp;</td>
                                                  <td align="left" class="auto-style3">
                                                       &nbsp;</td>
                                                  <td  style="width: 98px; height: 18px">
                                                  </td>
                                                  <td align="left" style="width: 140px; height: 18px" valign="top">
                                                       &nbsp;</td>
                                              </tr>
                                          </table>
                         </td></tr>
                         <tr><td> 
                          <table border="0" cellpadding="0" cellspacing="0" 
                                 style="text-align: center; height: 135px;" width="100%">
                      <tr>
                          <td  style="padding-right: 15px; " class="style6" colspan="6">
                              <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Min. 2000 & Max is Current Year." ControlToValidate="txtYearBuilt" MinimumValue="2000" Width="214px"></asp:RangeValidator>&nbsp;
                              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                              &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                          </td>
                      </tr>
                      <tr>
                          <td  style="text-align: right; padding-right:15px; width: 127px;">
                              LR/IMO # :</td>
                          <td style="text-align: left; width: 120px;">
                              <asp:TextBox ID="txtLRIMONumber" runat="server" Width="170px" MaxLength="49" CssClass="input_box" TabIndex="13"></asp:TextBox></td>
                          <td  style="padding-right: 15px; width: 132px; height: 18px; text-align: right;">
                              Registry :</td>
                          <td style="text-align: left; width: 187px;">
                              <asp:DropDownList ID="ddlPortOfRegistry" runat="server" Width="175px" CssClass="input_box" TabIndex="14">
                                  <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td style="text-align: right; padding-right:15px; width: 147px;">
                              Official # :</td>
                          <td style="text-align: left; width: 226px;">
                              <asp:TextBox ID="txtOfficialNumber" runat="server" Width="170px" MaxLength="49" CssClass="input_box" TabIndex="15"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td  style="text-align: right; padding-right:15px; width: 127px;">
                              MMSI # :</td>
                          <td style="text-align: left; width: 120px;">
                              <asp:TextBox ID="txtMMSINumber" runat="server" Width="170px" CssClass="input_box" MaxLength="49" TabIndex="16"></asp:TextBox></td>
                          <td  style="padding-right: 15px; width: 132px; height: 18px; text-align: right;">
                              Call Sign :</td>
                          <td  style="text-align: left">
                                                           <asp:TextBox ID="txtCallSign" runat="server" Width="170px" CssClass="input_box" MaxLength="50" TabIndex="16"></asp:TextBox>
                          </td>
                          <td style ="padding-right: 15px; text-align: right; width: 147px;"><%--Group Email:--%>
                              Current Class :
                          </td>
                          <td style="text-align: left"> 
                              <%--<asp:TextBox ID="txtVEmail" runat="server" Width="170px" CssClass="input_box" MaxLength="100" TabIndex="16"></asp:TextBox>--%>
                               <asp:DropDownList ID="ddlCurrentClassName" runat="server" CssClass="input_box" Width="174px" TabIndex="17">
                                  <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td  style="padding-right: 15px; text-align: right; width: 127px;">
                              Year Built :</td>
                          <td style="text-align: left; width: 120px;">
                              <asp:TextBox ID="txtYearBuilt" runat="server" CssClass="input_box" Width="170px" MaxLength="4" TabIndex="18"></asp:TextBox>
                              </td>
                          <td  style="padding-right: 15px; width: 132px; height: 18px; text-align: right;">
                              Age :</td>
                          <td style="text-align: left; width: 187px;">
                              <asp:TextBox ID="txtAge" runat="server" Width="170px" CssClass="input_box" MaxLength="2" TabIndex="19"></asp:TextBox></td>
                          <td style="padding-right: 15px; text-align: right; width: 147px;">
                              Yard :</td>
                          <td style="text-align: left; width: 226px;">
                              <asp:TextBox ID="txtYard" runat="server" Width="170px" CssClass="input_box" MaxLength="49" TabIndex="20"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td  style="padding-right: 15px; text-align: right; width: 127px;">
                              GRT(M) :</td>
                          <td style="text-align: left; width: 120px;">
                              <asp:TextBox ID="txtLDT" runat="server" Width="170px" CssClass="input_box" MaxLength="49" TabIndex="21"></asp:TextBox></td>
                          <td  style="padding-right: 15px; text-align: right; width: 132px;">
                              TEU(M) :</td>
                          <td style="text-align: left; width: 187px;">
                              <asp:TextBox ID="txtTEU" runat="server" Width="170px" CssClass="input_box" MaxLength="49" TabIndex="22"></asp:TextBox></td>
                          <td style="padding-right: 15px; text-align: right; width: 147px;">
                              NRT(M) :</td>
                          <td style="text-align: left; width: 226px;">
                              <asp:TextBox ID="txtDraught" runat="server" Width="170px" CssClass="input_box" MaxLength="49" TabIndex="23"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td  style="padding-right: 15px; text-align: right; width: 127px;">
                              Length(M) :</td>
                          <td style="text-align: left; width: 120px;">
                              <asp:TextBox ID="txtLength" runat="server" Width="170px" CssClass="input_box" MaxLength="19" TabIndex="24"></asp:TextBox></td>
                          <td  style="padding-right: 15px; text-align: right; width: 132px;">
                              Breadth(M) :</td>
                          <td style="text-align: left; width: 187px;">
                              <asp:TextBox ID="txtBreath" runat="server" Width="170px" CssClass="input_box" MaxLength="19" TabIndex="25"></asp:TextBox></td>
                          <td style="padding-right: 15px; text-align: right; width: 147px;">
                              Depth(M) :</td>
                          <td style="text-align: left; width: 226px;">
                              <asp:TextBox ID="txtDepth" runat="server" Width="170px" CssClass="input_box" MaxLength="19" TabIndex="26"></asp:TextBox></td>
                      </tr>
                        <tr>
                             <td style="padding-right: 15px; text-align: right; width: 127px;">
                                 Multiple PO Header : 
                             </td>
                             <td style="text-align: left; width: 120px;">
                                 <asp:CheckBox ID="chkIsMultiAccount" runat="server" TabIndex="27" AutoPostBack="True" OnCheckedChanged="chkIsMultiAccount_CheckedChanged"/> 
                                 </td>
                            <td  style="padding-right: 15px; text-align: right; width: 132px;">
                                Fleet : 
                                </td>
                             <td style="text-align: left; " >
                                <asp:DropDownList ID="ddlFleet" runat="server" 
                                   TabIndex="28" CssClass="required_box" Width="175px">
                               </asp:DropDownList>  <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="ddlFleet"
                                                          ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                 </td>
                              <td style="padding-right: 15px; text-align: right; width: 147px;">
                              PO Issuing Company :</td>
                          <td style="text-align: left; width: 226px;">
                              <asp:DropDownList ID="ddlPOIssuingCompany" runat="server" Width="170px" CssClass="input_box"  TabIndex="29" Visible="false"></asp:DropDownList>
                              <div style="width:225px;height:59px;overflow-y:auto;" Id="divPOIssingCompany" runat="server" Visible="false">
                                <asp:CheckBoxList ID="chkPOIssingCompany" runat="server" Width="220px" TabIndex="29" ></asp:CheckBoxList>
                                  </div>
                          </td>
                        </tr>
                      </table>
                         </td></tr>
                         </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td  style=" padding-top :10px;" >
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtLDT" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtTEU" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtDraught" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtLength" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtBreath" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtDepth" ValidChars=".">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                        FilterType="Numbers" TargetControlID="txtYearBuilt">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    &nbsp;<asp:Button ID="btn_vessel_general_save" runat="server" CssClass="btn" OnClick="btn_vessel_general_save_Click"
                            Text="Save" Width="59px" Visible="true" TabIndex="27" />
                    <asp:Button ID="btn_Print_vessel_general" runat="server" CausesValidation="False" CssClass="btn" TabIndex="28" Text="Print" Width="59px" OnClientClick="javascript:ShowPrint();return false;"  Visible="true" />                                                 
                </td>
            </tr>
             <tr>
                <td  style="height: 4px; width: 795px;">
                </td>
            </tr>
        </table>
 <asp:HiddenField ID="HiddenvesselGeneral" runat="server" />
                         
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_PersonalDetail.aspx.cs" Inherits="Emtm_Profile_PersonalDetail" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master"%>
<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function Show_Image_Large(obj)
    {
    window.open(obj.src,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
    }
    function Show_Image_Large1(path)
    {
    window.open(path,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
    }
    </script>

    <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
          <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="text headerband" style=" text-align :left; font-size :14px; padding :3px; font-weight: bold;">
                                Personal Details : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                             <div>
                                <uc2:Profile_PersonalHeaderMenu ID="Emtm_Profile_PersonalHeaderMenu1" runat="server" /> 
                             </div> 
                            </td>
                        </tr>
                        </table>  
                        <br />
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                        <tr>
                        <td>
                        <fieldset style=" height :130px"> 
                          <table border="0" cellpadding="0" cellspacing="6" style="width:100%;">
                          <tr>
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp; &nbsp;</td>  
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp;</td> 
                              <td style="text-align :right">&nbsp;</td>
                              <td>
                                  &nbsp;</td>
                              
                          </tr>
                              <tr>
                                  <td style="text-align :right">
                                      &nbsp;First Name :</td>
                                  <td>
                                      <asp:TextBox ID="txt_FirstName" runat="server" required='yes' MaxLength="24" TabIndex="1"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_FirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                                  <td style="text-align :right">
                                      &nbsp;Middle Name :</td>
                                  <td>
                                      <asp:TextBox ID="txt_Middlename" runat="server" MaxLength="24" TabIndex="2"></asp:TextBox>
                                  </td>
                                  <td style="text-align :right">
                                      &nbsp;Last Name :</td>
                                  <td>
                                      <asp:TextBox ID="txt_familyName" runat="server" required='yes' MaxLength="24" TabIndex="3"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_familyName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                          <tr>
                          <td style="text-align: right;"></td>
                          <td>
                          <div style="display :inline;">
                              
                          </div> 
                          </td>
                          <td></td>
                          <td>
                              &nbsp;</td>
                          <td></td>
                          <td>
                              
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align :right">Passport # :</td>
                              <td>
                              <asp:TextBox ID="txt_Passport" runat="server" MaxLength="49" TabIndex="3" ></asp:TextBox>
                              <asp:RequiredFieldValidator id="RFVpassport" runat="server" Enabled="false" ErrorMessage="*" ControlToValidate="txt_Passport" ></asp:RequiredFieldValidator>
                              </td>
                              <td style="text-align:right;">NRIC/FIN : </td>
                              <td>
                                  <asp:TextBox ID="txt_nricfin" runat="server" MaxLength="49" TabIndex="3"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RFVnric" runat="server" Enabled="false" ControlToValidate="txt_nricfin" ErrorMessage="*"></asp:RequiredFieldValidator>
                              </td>
                              <td style="text-align:right;">&nbsp;Status :</td>
                              <td>
                                  <asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td>&nbsp;</td>
                              <td>
                               
                              </td>
                               <td></td>
                              <td>
                                  
                              </td>
                              <td></td>
                              <td></td>
                              
                          </tr>
                           <tr>
                               <td style="text-align :right">Peap Category :</td>
                              <td>
                               <asp:DropDownList ID="ddlPeapCat" Visible="false" CssClass="input_box" runat="server"></asp:DropDownList>
                               <asp:Label ID="lblPeapCat" style="color:#003399;" runat="server"></asp:Label>
                              </td>
                               <td style="text-align :right">Reporting To :</td>
                              <td>
                                  <asp:DropDownList ID="ddlReportingTo" Visible="false" CssClass="input_box" runat="server"></asp:DropDownList>
                                  <asp:Label ID="lblReportingTo" style="color:#003399;" runat="server"></asp:Label>
                              </td>
                              <td></td>
                              <td></td>
                              
                          </tr>
                          <tr>
                              <td>&nbsp;</td>
                              <td>
                               
                              </td>
                               <td></td>
                              <td>
                                  
                              </td>
                              <td></td>
                              <td></td>
                              
                          </tr>
                        </table>
                        </fieldset>
                        </td>
                        <td style="padding:0px 0px 0px 10px;" >
                        <fieldset style=" height :130px"  > 
                        <center>
                        <table cellspacing="0" cellpadding="0" width="100px" border="0">
                        <tr>
                            <td style="text-align: center; padding-top :5px;">
                              <asp:Image ID="img_Crew" style="cursor:hand" ImageUrl="~/Modules/HRD/Images/emtm/noimage.jpg" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Width="60px"/>
                             </td>
                        </tr>
                        <tr>
                        <td style="text-align: center;">
                            <div style="border:0px solid; overflow:hidden; width:100px; display:block;">
                            <asp:FileUpload ID="FileUpload1" size="1" runat="server" style="left:-0px; position:relative; border:0px solid; background-color:#f9f9f9" Width="60px"/>
                            </div>
                            <%--<img src="../../Images/Emtm/browse.jpg" title="Click here to upload new image." alt="Browse..." onclick="document.getElementById('FileUpload1').click();" visible="false"/>  --%>
                        </td>
                        </tr>
                        </table>
                        </center> 
                        </fieldset> 
                        </td>
                        </tr>
                        </table>
                        <div style=" padding-top :13px;" >
                        <fieldset>
                        <legend><strong>Personal Details</strong></legend>
                            <table border="0" cellpadding="" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="6">&nbsp;</td></tr>
                        <tr>
                        <td>
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                        <tr>
                        <td style="text-align :right;width:110px;">Date Of Birth :&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txt_DOB" runat="server" required='yes'  Width="140px" TabIndex="4" AutoPostBack="True" ontextchanged="txt_DOB_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" ControlToValidate="txt_DOB" ErrorMessage="*"></asp:RequiredFieldValidator>  
                            &nbsp;
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false" /></td>
                        <td style="text-align :right">Age<span lang="en-us"> (Years)</span> :&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txt_Age" runat="server" ReadOnly="true" CssClass="input_box" MaxLength="10" Width="155px"></asp:TextBox>
                        </td>
                        <td style="text-align :right">POB :&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txt_placeofbirth" required='yes' runat="server"  Width="155px" MaxLength="25" TabIndex="5"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_placeofbirth" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align :right">COB :&nbsp;</td>
                        <td><asp:DropDownList ID="ddlcob" runat="server" required='yes'  Width="144px" TabIndex="6"></asp:DropDownList>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlcob" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td style="text-align :right">Nationality :&nbsp;</td>
                        <td><asp:DropDownList id="ddlnationality" required='yes' runat="server" Width="160px" TabIndex="7"></asp:DropDownList>
                        <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="ddlnationality" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td style="text-align :right">Gender :&nbsp;</td>
                        <td><asp:DropDownList id="ddlgender" runat="server" required='yes' Width="160px" TabIndex="8"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" ErrorMessage="*" ControlToValidate="ddlgender" meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align :right">Civil Status :&nbsp;</td>
                        <td><asp:DropDownList ID="ddlcivilstatus" runat="server" required='yes' Width="144px" TabIndex="9" CausesValidation="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator27" ControlToValidate="ddlcivilstatus" ErrorMessage="*"></asp:RequiredFieldValidator>  
                        </td>
                        <td style="text-align :right">Blood Group :&nbsp;</td>
                        <td><asp:DropDownList id="ddlbloodgroup" runat="server" Width="160px" CssClass="input_box" TabIndex="10"></asp:DropDownList></td>
                        <td style="text-align :right">Office :&nbsp;</td>
                        <td><asp:DropDownList ID="ddloffice" runat="server" required='yes'   Width="160px" TabIndex="19" CausesValidation="false"></asp:DropDownList>
                        <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="ddloffice" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align :right">Height(cms) :&nbsp;</td>
                        <td><asp:TextBox ID="txtheight" runat="server" CssClass="input_box" MaxLength="9" TabIndex="11" CausesValidation="false" AutoPostBack="True" ontextchanged="Update_BMI" Width="141px"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtheight" ErrorMessage="Max. 250." MaximumValue="250" MinimumValue="0" Type="Double"></asp:RangeValidator>
                        </td>
                        <td style="text-align :right">Weight(Kg.) :&nbsp;</td>
                        <td><asp:TextBox ID="txtweight" runat="server" CssClass="input_box" Width="155px" MaxLength="9"  AutoPostBack="True" TabIndex="12" CausesValidation="false" ontextchanged="Update_BMI"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtweight" ErrorMessage="Max. 125." MaximumValue="125" MinimumValue="0" Type="Double"></asp:RangeValidator>
                        </td>
                        <td style="text-align :right">BMI :&nbsp;</td>
                        <td><asp:TextBox ID="txt_Bmi" runat="server" CssClass="input_box" MaxLength="10" Width="155px" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>&nbsp;</td>
                    </tr>
                            <tr>
                                <td style="text-align:right">
                                    Shirt Size :</td>
                                <td>
                                    <asp:DropDownList ID="ddlShirtSize" runat="server" CssClass="input_box" 
                                        Width="144px">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align :right">
                                    Head of Department :</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkHOD" Enabled="false" /> </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align:right">DJC :&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtdatefirstjoin" runat="server" required='yes' Width="140px" TabIndex="17"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtdatefirstjoin" ErrorMessage="*"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="false"/></td>
                        <td style="text-align:right">Position :&nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlposition" runat="server" required='yes' Width="160px" TabIndex="18"></asp:DropDownList>
                            <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="ddlposition" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                        <td style="text-align:right"> Dept :</td>
                        <td>
                            <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="18" Width="160px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txt_DOB" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" TargetControlID="txtdatefirstjoin" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                        </fieldset> 
                        </div>
                    <br />
                    <table width="100%" cellpadding="0" cellspacing="0" > 
                    <tr>
                    <td align="right">
                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click"></asp:Button>
                    <asp:Button ID="brncancel" CssClass="btn"  runat="server" Text="Cancel" PostBackUrl="~/emtm/MyProfile/Emtm_Profile_PersonalDetail.aspx"  CausesValidation="false"></asp:Button>
                    </td>
                    </tr>
                    </table>
                    </td>
                    </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers>
        </asp:UpdatePanel> 
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipjobAppform.aspx.cs" Inherits="Modules_HRD_Applicant_ShipjobAppform" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %> 
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>eMANAGER </title>
    <script src="../../PMS/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../../PMS/JS/Common.js" type="text/javascript"></script>
    <script src="../../PMS/JS/JQScript.js" type="text/javascript"></script>
    <style type="text/css">
        .input_box {}
        .btn
{
	/*color:White ;
	background-image:url(images/bar_bg.png);
	height :25px;*/
	border:1px solid #fe0034; font-family:arial; font-size:12px; color:#fff; border-radius:3px; -webkit-border-radius:3px; -ms-border-radius:3px; background:#fe0030; background:linear-gradient(#ff7c96, #fe0030); background:-webkit-linear-gradient(#ff7c96, #fe0030); background:-ms-linear-gradient(#ff7c96, #fe0030); padding:4px 6px; cursor:pointer;
}

        .RowColumn1 {
            border : 1px solid black;
            text-align:Left;
            padding-left:5px;
            width:10%;
           
        }

        .RowColumn2 {
            border : 1px solid black;
            text-align:Left;
            padding-left:5px;
            width:40%;
        }

        .RowHeaderColumn {
             border : 1px solid black;
            text-align:Left;
            padding-left:5px;
            width:33%;
        }

         .RowDocColumn1 {
            border : 1px solid black;
            text-align:center;
            width : 20%;
        }

         .RowOTColumn1 {
            border : 1px solid black;
            text-align:Left;
            padding-left:5px;
            width:20%;
           
        }

        .RowOTColumn2 {
            border : 1px solid black;
            text-align:Left;
            padding-left:5px;
            width:30%;
        }

         
        
        </style>
    <script type="text/javascript">
        function getAge() {
            var enteredValue = document.getElementById('txtDOB').value;
            var today = new Date();
            var birthDate = new Date(enteredValue);
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }
            if (age < 18) {
                alert("Age must be > 18.");
                document.getElementById('txtDOB').value = "";
                document.getElementById('txtDOB').focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
      <%--    <div style="display:flex;padding:5px;" >
            <div style="width:20%;height:60px;">
                <asp:ImageButton ID="imgbtn" runat="server" Width="120px" Height="50px" /> 
            </div>
           <div style="font-family:Arial, Helvetica, sans-serif;text-align:center;font-size:18px;background-color:#4c7a6f;color:White;width:80%; height:35px;vertical-align:central;">
                 <asp:Label ID="lblCompanyName" runat="server" Text=""  ></asp:Label>
                  Application Form
            </div>
        </div>--%>
        <div width="100%" style="font-family:Arial, Helvetica, sans-serif; font-size:12px;text-align:center;border:solid 1px #008AE6;">
            <div >
                <div style="background-color:#4c7a6f;color:White; text-align:center; padding:3px; font-size:15px; height:25px;vertical-align:central;width :100%;">
                APPLICATION FORM
                </div>
                <div style=" padding-left:50px;padding-right:50px;">
                   <%-- <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" Height="25px"></asp:Label>--%>
                    <table style="width:100%;">
                         <tr>
                          <td style="width:85%;border:solid 1px #008AE6;" colspan="2" >
                           <span style="vertical-align:middle;"> Application for the Rank of</span>&nbsp; <asp:DropDownList ID="ddlRank" runat="server" CssClass="input_box" Style="background-color: lightyellow" Width="200px" > </asp:DropDownList>    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlRank"  InitialValue="0" ErrorMessage="*" ></asp:RequiredFieldValidator>&nbsp; <span style="vertical-align:middle;"> Available to join by </span> &nbsp; <asp:TextBox ID="txtAvalFrom" runat="server" CssClass="input_box" MaxLength="15" Style="text-align: center" Width="120px" ></asp:TextBox>   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtAvalFrom" ErrorMessage="mandatory"  Display="Dynamic" ></asp:RequiredFieldValidator> &nbsp;  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAvalFrom" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                                                       <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtAvalFrom" TargetControlID="txtAvalFrom" PopupPosition="TopRight" >
                                                                        </asp:CalendarExtender> 
                           </td>
                             <td rowspan="2" style="width:15%;">
                                 <table style="width:100%;border:solid 1px #008AE6;">
                            <tr>
                             <td>
                                  <asp:Image ID="img_Crew" style="cursor:hand" ImageUrl="~/Modules/HRD/Images/ImgUpload.png" runat="server" ToolTip="Only .jpg,.png,.jpeg,.gif Files are allowed" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px"  Height="120px" Width="160px"  />
                                
                             </td>
                            </tr>
                                 <tr>
                             <td style="text-align: center;" >
                                  <div >
                                       <asp:FileUpload ID="FileUpload1" size="1" runat="server" style="position:relative; border:0px solid; background-color:#f9f9f9" />
                                          
                                       <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                        ControlToValidate="FileUpload1"
                        ErrorMessage="Only .jpg,.png,.jpeg,.gif,.bmp Files are allowed" Font-Bold="True"
                        Font-Size="Smaller"
                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|bmp|JPG|JPEG|PNG|GIF|BMP)$"></asp:RegularExpressionValidator>
                                  </div>
                                 <div>
                                    <%--  <asp:ImageButton ID="btnUpload" runat="server" AlternateText="Upload" ImageUrl="~/Modules/HRD/Images/UploadLogo.gif"  OnClick="btn_Upload_Click"  CausesValidation="false" /> --%>
                                      <asp:Button ID="btnUpload" ToolTip="Upload Photo" runat="server"  Text="&nbsp;&nbsp;&nbsp;&nbsp;Upload"  Width="110px" OnClick="btn_Upload_Click" style="background-position: 3px 3px; background-image:url('/Modules/HRD/Images/Upload.png'); background-repeat: no-repeat; border :solid 1px gray; background-color:gray;"  ForeColor="White" Height="30px" CausesValidation="false"/>
                                 </div>

                             </td>
                         </tr>
                            </table>
                             </td>
                       </tr>
                        <tr>
                          <td style="width:85%;" colspan="2">
                            <table style="width:100%;border:solid 1px #008AE6;">
                            <tr style="height:20px;">
                            <td class="RowHeaderColumn">
                                <span>
                                    Last Name (Strictly as in Passport)
                                </span>
                            </td>
                            <td class="RowHeaderColumn">
                                 <span>
                                   Given Name
                                </span>
                            </td>
                            <td class="RowHeaderColumn">
                                 <span>
                                   Middle Name
                                </span>
                            </td>
                            </tr>
                                 <tr style="height:30px;">
                            <td class="RowHeaderColumn">
                               <asp:TextBox ID="txtLastName" runat="server" Width="80%" CssClass="input_box" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td class="RowHeaderColumn">
                                 <asp:TextBox ID="txtFirstName" runat="server"  Width="80%" CssClass="input_box" MaxLength="50" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td class="RowHeaderColumn">
                                 <asp:TextBox ID="txtMiddleName" runat="server"  Width="80%" MaxLength="25" Style="text-align: center; " ></asp:TextBox>
                            </td>
                            </tr>
                                <tr style="height:20px;">
                            <td class="RowHeaderColumn">
                                <span>
                                    Date of Birth
                                </span>
                            </td>
                            <td class="RowHeaderColumn">
                                 <span>
                                  Place of Birth
                                </span>
                            </td>
                            <td class="RowHeaderColumn">
                                 <span>
                                   Nationality
                                </span>
                            </td>
                            </tr>
                                <tr style="height:30px;">
                            <td class="RowHeaderColumn">
                               <asp:TextBox ID="txtDOB" runat="server" Width="150px" CssClass="input_box" MaxLength="15" Style="text-align: center; background-color: lightyellow" onchange="getAge()" ></asp:TextBox>
                                &nbsp;<%--<asp:ImageButton ID="ibDOB" runat="server" ImageUrl="~/Images/Calendar.gif" />--%>
                                 <asp:RequiredFieldValidator runat="server" ID="rfvDOB" ControlToValidate="txtDOB" ErrorMessage="mandatory"  Display="Dynamic" ></asp:RequiredFieldValidator> 
                                <asp:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDOB" TargetControlID="txtDOB" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender> 
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDOB" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                               <%--  <asp:RangeValidator ID="rngValDateOfBirth" runat="server" ErrorMessage="must be between 18 and 60" ControlToValidate="txtDOB" Type="Date"></asp:RangeValidator>--%>
                                             <%-- <asp:RangeValidator ID="rngValDateOfBirth" runat="server" ControlToValidate="txtDOB" Display="Dynamic"
 ErrorMessage="Please enter valid Date Of Birth (Your age should be above 18-yrs)" 
  ></asp:RangeValidator>                
                               <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtDOB"
   OnServerValidate="DOBValidation" Display="Static"
   ErrorMessage="Please enter valid Date Of Birth (Your age should be above 18-yrs)"
   ForeColor="Red" Font-Name="verdana" Font-Size="10pt" runat="server"
    /> --%>
                            </td>
                            <td class="RowHeaderColumn">
                                 <asp:TextBox ID="txtPOB" runat="server"  Width="150px" CssClass="input_box" MaxLength="30" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvPOB" runat="server" ControlToValidate="txtPOB"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td class="RowHeaderColumn">
                                <asp:DropDownList ID="ddlNat" runat="server"  CssClass="input_box" Style="background-color: lightyellow" Width="150px">
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlNat"  InitialValue="0" ErrorMessage="mandatory" ></asp:RequiredFieldValidator>
                            </td>
                            </tr>

                            </table>
                          </td>
                         <%--  <td style="width:20%;" >
                            
                           </td>--%>
                        </tr>
                       
                        </table>
                      <table style="width:100%;">
                          <tr>
                              <td style="width:50%;">
                                  <table style="width:100%;border:solid 1px #008AE6;margin-top:5px;border-radius:5px;">
                           <tr style="height:20px;" >
                            <td style="text-align:Left;padding-left:5px;width:50%;" class="RowColumn" colspan="2">
                                <span>
                                    <b>Contact Details  </b>
                                </span>
                               
                            </td>
                           
                            </tr>
                            <tr style="height:30px;">
                                <td  class="RowColumn1">
                                    Address 1
                                    :</td>
                            <td class="RowColumn2">
                               <asp:TextBox ID="txtAddressPE1" runat="server" Width="332px" MaxLength="100"  Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddressPE1"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    Address 2
                                    :</td>
                            <td class="RowColumn2">
                               <asp:TextBox ID="txtAddressPE2" runat="server" Width="332px" MaxLength="100"  Style="text-align: center; " ></asp:TextBox>
                            </td>
                           
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    Address 3
                                    :</td>
                            <td class="RowColumn2">
                               <asp:TextBox ID="txtAddressPE3" runat="server" Width="332px" MaxLength="100"  Style="text-align: center; " ></asp:TextBox>
                            </td>
                           
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    City / Pin / Zip code
                                    :</td>
                            <td class="RowColumn2">
                               <asp:TextBox ID="txtCityPE" runat="server" Width="179px" MaxLength="50"  Style="text-align: center; background-color: lightyellow" placeholder="City"></asp:TextBox>

                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCityPE"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                                 <asp:TextBox ID="txtPincode" runat="server" Width="179px" MaxLength="10"  Style="text-align: center; background-color: lightyellow" placeholder="Pin/Zip code" ></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPincode"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                                 
                            </td>
                            
                            </tr>
                            
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    State
                                    :</td>
                            <td class="RowColumn2">
                               <asp:TextBox ID="txtStatePE" runat="server" Width="332px" MaxLength="30"  Style="text-align: center; " ></asp:TextBox>
                            </td>
                            
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    Country
                                    :</td>
                            <td class="RowColumn2">
                               <asp:DropDownList ID="ddl_P_Country" OnSelectedIndexChanged="ddl_P_Country_SelectedIndexChanged"  runat="server" AutoPostBack="True" CssClass="required_box"
                                                                                        Width="210px" TabIndex="33">
                                                                                    </asp:DropDownList> &nbsp; 
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_P_Country"
                                                                                        ErrorMessage="mandatory" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                            </td>
                           
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                    Mobile No : 
                                </td>
                            <td class="RowColumn2">
                                 <asp:DropDownList ID="ddl_P_CountryCode_Mobile" runat="server" CssClass="input_box"
                                                                                                    Width="76px" meta:resourcekey="ddl_P_CountryCode_MobileResource1" TabIndex="42">
                                                                                                </asp:DropDownList> &nbsp;
                               <asp:TextBox ID="txtMobileNoPE" runat="server" Width="248px" MaxLength="11" TextMode="Number"  Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobileNoPE"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            
                            </tr>
                           </table>
                              </td>
                               <td style="width:50%;">
                                   <table style="width:100%;border:solid 1px #008AE6;margin-top:5px;border-radius:5px;">
                           <tr style="height:20px;" >
                            
                            <td style="text-align:Left;padding-left:5px;"  colspan="4">
                                 <span>
                                   <b>Other Details  </b> 
                                </span>
                                
                            </td>
                           
                            </tr>
                           <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                 Marital Status 
                                 :</td>
                            <td class="RowOTColumn2">
                                 <asp:DropDownList ID="ddmaritalstatus" runat="server" CssClass="required_box" 
                                                                                        Width="100px"  TabIndex="9"><asp:ListItem Text="&lt; Select &gt;" Value="0" ></asp:ListItem></asp:DropDownList> <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddmaritalstatus" ErrorMessage="mandatory"  InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                                 <td class="RowOTColumn1">
                                Gender 
                                     :</td>
                            <td class="RowOTColumn2">
                                <asp:RadioButtonList ID="radSex" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Male" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                            </td>
                            </tr>
                            <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                 Height (in cms)
                                 :</td>
                            <td class="RowOTColumn2">
                                <asp:TextBox ID="txtHeight" runat="server" Width="100px"  MaxLength="4" TextMode="Number" Style="text-align: center; " ></asp:TextBox>
                            </td>
                               <td class="RowOTColumn1">
                                 Weight (in Kgs)
                                   :</td>
                            <td class="RowOTColumn2">
                                <asp:TextBox ID="txtWeight" runat="server"  Width="100px"  MaxLength="6" TextMode="Number" Style="text-align: center; " ></asp:TextBox>
                            </td>
                            </tr>
                           
                            
                            <tr style="height:30px;">
                                
                            <td class="RowOTColumn1">
                                 Bolier Suit Size : 
                            </td>
                            <td style=" border : 1px solid black;text-align:Left; padding-left:5px;" colspan="3">
                                 <asp:TextBox ID="txtSuitSize" runat="server"  Width="100px" MaxLength="9" meta:resourcekey="txtwaistResource1" TabIndex="14"></asp:TextBox>
                            </td>
                            </tr>
                            
                            <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                  Shirt Size :
                            </td>
                            <td style=" border : 1px solid black;text-align:Left; padding-left:5px;" colspan="3" >
                                 <asp:DropDownList ID="ddl_Shirt" runat="server"  Width="160px" meta:resourcekey="ddmaritalstatusResource1" TabIndex="16">
                                                                                    <asp:ListItem meta:resourceKey="ListItemResource2" Value="0" Text="&lt; Select &gt;"></asp:ListItem>
                                                                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                Shoe Size : 
                            </td>
                            <td style=" border : 1px solid black;text-align:Left; padding-left:5px;" colspan="3">
                               <asp:DropDownList ID="ddl_Shoes" runat="server"  Width="160px" meta:resourcekey="ddmaritalstatusResource1" TabIndex="15">
                                                                                    <asp:ListItem meta:resourceKey="ListItemResource2" Value="0" Text="&lt; Select &gt;"></asp:ListItem>
                                                                                </asp:DropDownList> 
                            </td>
                            </tr>
                            <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                   Waist (in cms) :</td>
                            <td style=" border : 1px solid black;text-align:Left; padding-left:5px;" colspan="3">
                               <asp:TextBox ID="txtwaist" runat="server"  Width="155px" MaxLength="9" meta:resourcekey="txtwaistResource1" TabIndex="14"></asp:TextBox>
                            </td>
                            </tr>
                                        <tr style="height:30px;">
                               
                            <td class="RowOTColumn1">
                                   </td>
                            <td style=" border : 1px solid black;text-align:Left; padding-left:5px;" colspan="3">
                              
                            </td>
                            </tr>
                           </table>
                              </td>
                          </tr>
                      </table>
                       
                       <table style="width:100%;border:solid 1px #008AE6; margin-top:5px;border-radius:5px;">
                           <tr style="height:30px;">
                                <td class="RowColumn1">
                                   Email : 
                                </td>
                            <td class="RowColumn2">
                                <asp:TextBox ID="txt_P_EMail1" runat="server" Width="327px" MaxLength="99" meta:resourcekey="txt_P_EMail1Resource1" TabIndex="47"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_P_EMail1"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" Visible="False">
                                    </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_P_EMail1"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                            <td class="RowColumn1" rowspan="2" style="vertical-align:top;">
                                   Vessel Experience :</td>
                            <td style=" vertical-align:top;" class="RowColumn2" rowspan="2">
                                <asp:CheckBoxList ID="chkVeselType" runat="server" CellPadding="0" CellSpacing="0" RepeatColumns="3" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                            </td>
                           
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowColumn1">
                                   Nearest Intl. Airport to Hometown : 
                                </td>
                            <td class="RowColumn2">
                                <asp:DropDownList ID="dd_nearest_airport" runat="server" CssClass="input_box" TabIndex="18"
                                                                                Width="236px">
                                                                                <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                            </asp:DropDownList>


                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dd_nearest_airport"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                            </td>
                             
                           
                            </tr>
                       </table>
                        <table style="width:100%;border:solid 1px #008AE6;margin-top:5px;border-radius:5px;">
                           <tr style="height:20px;">
                            <td class="RowDocColumn1">
                                <b> Mandatory Documents </b>
                            </td>
                            <td class="RowDocColumn1">
                              <b> Number </b>
                            </td>
                            <td class="RowDocColumn1">
                            <b> Issue Date </b>
                            </td>
                            <td class="RowDocColumn1">
                                <b> Place of Issue </b>
                            </td>
                            <td class="RowDocColumn1">
                               <b> Expiry </b>
                            </td>
                            </tr>
                             <tr style="height:30px;">
                                <td class="RowDocColumn1" style="text-align:left;padding-left:5px;">
                                    CDC (NATIONAL)
                                </td>
                            <td class="RowDocColumn1"> 
                                <asp:TextBox ID="txtCdcNo" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox> 
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCdcNo"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtCDCIssueDate" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvCDCIssueDt" runat="server" ControlToValidate="txtCDCIssueDate"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator>  &nbsp; <asp:RegularExpressionValidator ID="revCDCIssueDt" runat="server" ControlToValidate="txtCDCIssueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtCDCIssueDate" TargetControlID="txtCDCIssueDate" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                 </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtCDCIssuePlace" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCDCIssuePlace"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                      </asp:RequiredFieldValidator>

                            </td>
                                  <td class="RowDocColumn1">
                                       <asp:TextBox ID="txtCDCExpiry" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCDCExpiry" runat="server" ControlToValidate="txtCDCExpiry"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                      </asp:RequiredFieldValidator> &nbsp; <asp:RegularExpressionValidator ID="revCDCExpiry" runat="server" ControlToValidate="txtCDCExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                       <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtCDCExpiry" TargetControlID="txtCDCExpiry" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                
                            </td>
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowDocColumn1" style="text-align:left;padding-left:5px;">
                                    PASSPORT
                                </td>
                            <td class="RowDocColumn1"> 
                                <asp:TextBox ID="txtPassportNo" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPassportNo"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtPassportIssueDt" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtPassportIssueDt"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator> &nbsp;
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPassportIssueDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                  <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtPassportIssueDt" TargetControlID="txtPassportIssueDt" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                 </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtPassportIssuePlace" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtPassportIssuePlace"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                            </td>
                                  <td class="RowDocColumn1">
                                    <asp:TextBox ID="txtPassportExpiry" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPassportExpiry"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator> &nbsp;
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPassportExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                         <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtPassportExpiry" TargetControlID="txtPassportExpiry" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                
                            </td>
                            </tr>
                              <tr style="height:30px;" id="trIndos" runat="server" visible="false">
                                <td class="RowDocColumn1" style="text-align:left;padding-left:5px;">
                                    INDOS
                                </td>
                            <td class="RowDocColumn1"> 
                                <asp:TextBox ID="txtIndocNo" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvIndocNo" runat="server" ControlToValidate="txtIndocNo"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic"  Visible="false">
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtIndocIssueDt" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                   <asp:RequiredFieldValidator ID="rfvIndocIssueDt" runat="server" ControlToValidate="txtIndocIssueDt"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" Visible="false">
                                    </asp:RequiredFieldValidator> &nbsp;
                                      <asp:RegularExpressionValidator ID="revIndocIssueDt" runat="server" ControlToValidate="txtIndocIssueDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                  <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtIndocIssueDt" TargetControlID="txtIndocIssueDt" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                 </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtIndocIssuePlace" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvIndocIssuePlace" runat="server" ControlToValidate="txtIndocIssuePlace"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" Visible="false">
                                    </asp:RequiredFieldValidator>

                            </td>
                                  <td class="RowDocColumn1">
                                    <asp:TextBox ID="txtIndocExpiry" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                      &nbsp;
                                      <asp:RegularExpressionValidator ID="revIndocExpiry" runat="server" ControlToValidate="txtIndocExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                       <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtIndocExpiry" TargetControlID="txtIndocExpiry" PopupPosition="TopRight">
                                                                        </asp:CalendarExtender>
                                
                            </td>
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowDocColumn1" style="text-align:left;padding-left:5px;">
                                    UPLOAD CV 
                                </td>
                            <td colspan="4" style=" border : 1px solid black;text-align:Left; padding-left:5px;" > 
                                <asp:FileUpload ID="FileUpload2" runat="server" Style="background-color: white; border: solid 1px #4371a5; font-size: 11px; width: 252px;" /> &nbsp;
                                <asp:RequiredFieldValidator ID="rfvFileupload"  runat="server"
                                                ControlToValidate="FileUpload2" Display="Dynamic" ErrorMessage="mandatory"></asp:RequiredFieldValidator>
                            </td>
                           
                            </tr>
                            </table>
                            <div>  
   <%-- <cc1:CaptchaControl ID="cptCaptcha" runat="server"  
        CaptchaBackgroundNoise="Low" CaptchaLength="5"  
        CaptchaHeight="60" CaptchaWidth="200"  
        CaptchaLineNoise="None" CaptchaMinTimeout="5"  
        CaptchaMaxTimeout="240" FontColor = "#529E00" />  
                               <br />
        <b> Enter Captcha Text :  </b>    <asp:TextBox ID="txtCaptcha" runat="server" ></asp:TextBox>  <asp:ImageButton CausesValidation="false" runat="server" ImageUrl="~/Modules/HRD/Images/reset.png"/>   <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" ControlToValidate = "txtCaptcha"></asp:RequiredFieldValidator> 
                    <asp:CustomValidator ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha" runat="server"  />--%>
    </div>  
    
                   
                    <div style="display:flex;padding-top:5px;">
                        <span > 
                            <asp:Label ID="lbl_info" runat="server" ForeColor="Red" Width="500px" style="float:left;"></asp:Label>
                        </span>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn" style="margin-left:150px;" Width="100px" OnClick="btnSubmit_Click"/> &nbsp; 
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn"  Width="100px" CausesValidation="false" OnClick="btnClear_Click"/>
                    </div>
                    <br /> 
        
      
                </div>
            </div>
        </div>
        <script type="text/jscript">
            Sys.UI.Point = function Sys$UI$Point(x, y) {
                /// <summary locid="M:J#Sys.UI.Point.#ctor" />
                /// <param name="x" type="Number"></param>
                /// <param name="y" type="Number"></param>
                /// <field name="x" type="Number" integer="true" locid="F:J#Sys.UI.Point.x"></field>
                /// <field name="y" type="Number" integer="true" locid="F:J#Sys.UI.Point.y"></field>
                /// <field name="rawX" type="Number" locid="F:J#Sys.UI.Point.rawX"></field>
                /// <field name="rawY" type="Number" locid="F:J#Sys.UI.Point.rawY"></field>
                var e = Function._validateParams(arguments, [
                    { name: "x", type: Number },
                    { name: "y", type: Number }
                ]);
                if (e) throw e;
                this.rawX = x;
                this.rawY = y;
                this.x = Math.round(x);
                this.y = Math.round(y);
            }
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShipjobAppDetailPopUp.aspx.cs" Inherits="Modules_HRD_Applicant_ShipjobAppDetailPopUp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>eMANAGER</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <script src="../../PMS/JS/jquery.min.js" type="text/javascript"></script>
    <script src="../../PMS/JS/Common.js" type="text/javascript"></script>
    <script src="../../PMS/JS/JQScript.js" type="text/javascript"></script>
    
    <style type="text/css">
        .selecteddiv {
            width: 100%;
            padding: 5px;
            padding-bottom: 5px;
            background-color: wheat;
            border: dotted 1px #4371a5;
        }

        .normaldiv {
            width: 100%;
            padding: 5px;
            padding-bottom: 5px;
            background-color: none;
        }

        .rem {
            border: solid 1px #c2c2c2;
            width: 650px;
        }

            .rem:focus {
                background-color: #FFFFCC;
            }

        .c2 + div:after {
            content: "*";
            font-size: small;
            color: red;
        }


        .newbtn {
            border: solid 1px #c2c2c2;
            background-color: Orange;
            padding: 5px;
            width: 100px;
            margin-top: 2px;
            height:30px;
            margin-bottom:5px;
        }

        .c1 {
            width: 400px;
            float: left;
        }

        .c2 {
            width: 80px;
            float: left;
        }
    </style>
    <script type="text/javascript">
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
        function SetLastFocus(ctlid) {
            pos = getCookie(ctlid);
            if (isNaN(pos))
            { pos = 0; }
            if (pos > 0) {
                document.getElementById(ctlid).scrollTop = pos;
            }
        }
        function SetScrollPos(ctl) {
            setCookie(ctl.id, ctl.scrollTop, 1);
        }

       

      

    </script>
    <style type="text/css">
        .btn11sel {
            font-size: 14px;
            background-color: #808080;
            border-top: solid 1px black;
            border-right: solid 1px black;
            border-left: solid 1px black;
            border-bottom: solid 1px #4371a5;
            padding: 5px;
            height: auto;
            color: White;
        }

        .btn11 {
            font-size: 14px;
            background-color: #e2e2e2;
            border-top: solid 1px black;
            border-right: solid 1px black;
            border-left: solid 1px black;
            border-bottom: solid 1px #c2c2c2;
            padding: 5px;
            height: auto;
        }

        input {
            padding: 2px;
            height: 16px;
        }

        .mybutton {
            background-color: #0094ff;
            color: white;
            padding: 5px 10px 5px 10px;
            border: none;
            height: auto;
        }

        select {
            padding: 2px;
            height: 23px;
            line-height: 23px;
        }

        .bordered tr td {
            border: solid 1px #e5e5e5;
        }
    </style>
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
    function DownLoadFile(TableID, FileName) {
            document.getElementById('hfTableID').value = TableID;
            document.getElementById('hfFileName').value = FileName;
            document.getElementById('btnDownLoadFile').click();
         }
         </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager  ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div width="100%" style="font-family:Arial, Helvetica, sans-serif; font-size:12px;text-align:center;border:solid 1px #008AE6;">
            <div >
                <div style="background-color:#4c7a6f;color:White; text-align:center; padding:3px; font-size:15px; height:25px;vertical-align:central;width :100%;">
                Applicant Details -  <%=candidateid.ToString()%>
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
                                <%--  <div >
                                       <asp:FileUpload ID="FileUpload1" size="1" runat="server" style="position:relative; border:0px solid; background-color:#f9f9f9" />
                                          
                                       <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                        ControlToValidate="FileUpload1"
                        ErrorMessage="Only .jpg,.png,.jpeg,.gif,.bmp Files are allowed" Font-Bold="True"
                        Font-Size="Smaller"
                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|bmp|JPG|JPEG|PNG|GIF|BMP)$"></asp:RegularExpressionValidator>
                                  </div>
                                 <div>
                               
                                      <asp:Button ID="btnUpload" ToolTip="Upload Photo" runat="server"  Text="&nbsp;&nbsp;&nbsp;&nbsp;Upload"  Width="110px" OnClick="btn_Upload_Click" style="background-position: 3px 3px; background-image:url('/Modules/HRD/Images/Upload.png'); background-repeat: no-repeat; border :solid 1px gray; background-color:gray;"  ForeColor="White" Height="30px" CausesValidation="false"/>
                                 </div>--%>

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
                               <asp:TextBox ID="txtLastName" runat="server" Width="80%" CssClass="input_box" MaxLength="50" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td class="RowHeaderColumn">
                                 <asp:TextBox ID="txtFirstName" runat="server"  Width="80%" CssClass="input_box" MaxLength="50" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                                                        ErrorMessage="mandatory" Display="Dynamic" ></asp:RequiredFieldValidator>
                            </td>
                            <td class="RowHeaderColumn">
                                 <asp:TextBox ID="txtMiddleName" runat="server"  Width="80%" MaxLength="50" Style="text-align: center; " ></asp:TextBox>
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
                            <td style="vertical-align: top; padding: 5px; width:75%; text-align:left;">
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
                                                                                        ErrorMessage="*" Display="Dynamic" ></asp:RequiredFieldValidator>
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
                                 <asp:TextBox ID="txtPincode" runat="server" Width="179px" MaxLength="15"  Style="text-align: center; background-color: lightyellow" placeholder="Pin/Zip code" ></asp:TextBox>
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
                                    CDC (National)
                                </td>
                            <td class="RowDocColumn1"> 
                                <asp:TextBox ID="txtCdcNo" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCdcNo"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator>
                            </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtCDCIssueDate" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCDCIssueDate"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                    </asp:RequiredFieldValidator> &nbsp; <asp:RegularExpressionValidator ID="revCDCIssueDt" runat="server" ControlToValidate="txtCDCIssueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtCDCIssueDate" PopupButtonID="txtCDCIssueDate"  PopupPosition="TopRight">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCDCExpiry"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" >
                                      </asp:RequiredFieldValidator> &nbsp; <asp:RegularExpressionValidator ID="revCDCExpiry" runat="server" ControlToValidate="txtCDCExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                       <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtCDCExpiry" PopupButtonID="txtCDCExpiry" PopupPosition="TopRight">
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
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPassportIssueDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                  <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtPassportIssueDt" PopupPosition="TopRight" PopupButtonID="txtPassportIssueDt">
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
                                    </asp:RequiredFieldValidator> &nbsp;  <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPassportExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                         <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtPassportExpiry" PopupPosition="TopRight" PopupButtonID="txtPassportExpiry">
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
                                    </asp:RequiredFieldValidator> &nbsp;  <asp:RegularExpressionValidator ID="revIndocIssueDt" runat="server" ControlToValidate="txtIndocIssueDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                  <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtIndocIssueDt" PopupPosition="TopRight" PopupButtonID="txtIndocIssueDt">
                                                                        </asp:CalendarExtender>
                                 </td>
                            <td class="RowDocColumn1">
                                <asp:TextBox ID="txtIndocIssuePlace" runat="server" Width="150px" MaxLength="25" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvIndocIssuePlace" runat="server" ControlToValidate="txtIndocIssuePlace"
                                                                                        ErrorMessage="mandatory"  Display="Dynamic" Visible="false">
                                    </asp:RequiredFieldValidator>

                            </td>
                                  <td class="RowDocColumn1">
                                    <asp:TextBox ID="txtIndocExpiry" runat="server" Width="150px" MaxLength="15" Style="text-align: center; background-color: lightyellow" ></asp:TextBox>  <asp:RegularExpressionValidator ID="revIndocExpiry" runat="server" ControlToValidate="txtIndocExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> &nbsp;
                                       <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"  TargetControlID="txtIndocExpiry" PopupPosition="TopRight" PopupButtonID="txtIndocExpiry">
                                                                        </asp:CalendarExtender>
                                
                            </td>
                            </tr>
                            <tr style="height:30px;">
                                <td class="RowDocColumn1" style="text-align:left;padding-left:5px;">
                                    Attached CV 
                                </td>
                            <td colspan="4" style=" border : 1px solid black;text-align:Left; padding-left:5px;" > 
                                <%--<asp:FileUpload ID="FileUpload2" runat="server" Style="background-color: white; border: solid 1px #4371a5; font-size: 11px; width: 252px;" /> &nbsp;
                                <asp:RequiredFieldValidator ID="rfvFileupload"  runat="server"
                                                ControlToValidate="FileUpload2" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                 <a id="ancCV" runat="server" target="_blank">
                                    <img src="../Images/paperclip.gif" style="border: none" />
                                </a>
                            </td>

                           
                            </tr>

                            </table>
                                
                                

                              
                            </td>
                            <td style="vertical-align: top; padding: 5px; width:25%; text-align:left;">
                     
                             <div style="color: Black; padding: 5px;">
                                 <span id="lblActivity" runat="server" style="font-size: 13px;" visible="false"></span>
                                <div  style='padding:5px;line-height:20px;'> <b> Application Received On :</b> 
                                    <asp:Label ID="lblAppRecivedOn" runat="server"></asp:Label> 

                                </div>
                                 <div id="divMain" runat="server" visible="false"   style='padding:5px;line-height:20px;font-size:12px;'>
                                     <span style="color:red;">  <br />
                                         A - Send Mail to Applicant for updating Sea Experience:
                                         <br />
                                         <asp:Button ID="btnSendMail" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnSendMail_Click"  Text="Send Mail" /> 
                                          <br />
                                         B - If Applicant is suitable for employment:
                                         <br />
                                         <asp:Button ID="btnReqForApprove" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnRApprove_Click" Text="Submit for approval" /> 
                                         <br />
                                         C - If Applicant is not suitable for employment:
                                         <br />
                                         <asp:Button ID="btnArchive" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnArchive_Click" OnClientClick="return confirm('Are you sure to archive this applicant?');" Text="Archive" Visible="false" />
                                         <br />
                                        
                                     </span>  <br />
                                      
                                 </div>
                                 
                                 <div id="div1" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'> <b> 
                                    <%-- Requested for App. By/On--%> Approval Requested By/On :

                                                                                                                       </b> 
                                    <asp:Label ID="lblReqforAppBy" runat="server"></asp:Label> <br />
                                     <div style="vertical-align:top;"> 
                                         <b>Remarks :  </b> <asp:TextBox ID="txtApprovalRemarks" runat="server" ReadOnly="true" Width="200px"  ToolTip="" Height="50px" Style="border: solid 1px gray" TextMode="MultiLine" ></asp:TextBox> <br />
                                     <b> Status : </b> <asp:Label ID="lblStatus" runat="server"></asp:Label> <br />
                                     </div>
                                 </div>
                                 <div id="divAppRej" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'>
                                     <asp:Button ID="btnApprove" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnApprove_Click" Text="Approve" Visible="false" />
                        <asp:Button ID="btnReject" runat="server" CssClass="btn" OnClick="btnReject_Click" Height="25px" OnClientClick="confirm('Are you sure to reject this application?');" Text=" Reject " Visible="false" />
                                 </div>
                                  <div id="div2" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'> <b> Approved By/On : </b> 
                                    <asp:Label ID="lblApprovedBy" runat="server"></asp:Label> <br />
                                      <div style='color:#FF0066;font-size:15px;margin-top:10px;'>
                                          Approval No : <asp:Label ID="lblApprovalId" runat="server" ></asp:Label>
                                      </div>
                                        
                                </div>
                                 
                                 <div id="div3" runat="server" visible="false" style='padding:5px;line-height:20px;font-size:12px;'> <b> Rejected By/On : </b> 
                                     <asp:Label ID="lblRejBy" runat="server"></asp:Label>
                                     </div> 
                                  <div id="div4" runat="server" visible="false" style='padding:5px;line-height:20px;font-size:12px;'> <b> Archived By/On : </b> 
                                     <asp:Label ID="lblArchivedby" runat="server"></asp:Label>
                                 </div>
                                 <div id="divnotify" runat="server" visible="false"  style='padding:5px;line-height:20px;font-size:12px;'>
                                     <span runat="server" visible="true">
                                        <asp:Button ID="btnNotify" runat="server" CausesValidation="false" CssClass="btn" Height="25px" OnClick="btnNotify_Click" Text="Notify" Visible="false" />
                                            </span>
                                 </div>
                             </div>
                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table Id="tblSeaExp" runat="server" visible="false" border="0" cellpadding="3" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">        
        <tr style="height:20px; color:#ffffff; font-size:14px;" >
            <td style="background-color:#4c7a6f;">
                <asp:ImageButton ID="imgbtnAddSeaExp" runat="server" OnClick="imgbtnAddSeaExp_Click" ImageUrl="~/Modules/HRD/Images/add_16.gif" style="float:left"  CausesValidation="false" ToolTip="Add Sea Experience"/>
            <%--<asp:LinkButton runat="server" id="imgbtnAddSeaExp" OnClick="imgbtnAddSeaExp_Click" Text="Add Sea Experience" ForeColor="White" style="float:left"  CausesValidation="false"/>--%>
            <b> Sea Experience List </b></td>
        </tr>
       <tr>
                        <td>
                            <div style="width:100%;  overflow-x:scroll; overflow-y:scroll;" id="rptheader" runat="server">
                            <table cellpadding="0" cellspacing="0" border="0" width="98%">
                            <tr style="background-color:#E0F5FF;">
                            <td class="blueheader" align="center"  style="width:4%">S No.</td>
                            <td class="blueheader" align="center" style="width:18%">Company Name</td>
                            <td class="blueheader" align="center" style="width:18%">Vessel Name</td>
                            <td class="blueheader" align="center" style="width:10%">Vessel Type</td> 
                            <td class="blueheader" align="center" style="width:10%">From Date</td>
                            <td class="blueheader" align="center" style="width:10%">To Date</td>
                            <td class="blueheader" align="center" style="width:10%">Rank</td>
                            <td class="blueheader" align="center" style="width:10%">BHP(kw)</td>
                            <td class="blueheader" align="center" style="width:10%">GRT</td>
                            <td class="blueheader" align="left" style="width:4%">Del</td>
                            </tr>
                            <asp:Repeater runat="Server" ID="rptData" OnItemDataBound=" rptData_OnItemDataBound" onitemcommand="rptData_ItemCommand" ><%--rptData  rptPRData--%>
                            <ItemTemplate>
                            <tr style="padding-top:1px;" class="rowstyle" align="center"  >
                                <td style="text-align:center;">
                                    <asp:Label ID="lblRowNumber" runat="server" Text="1"  ></asp:Label>
                                    <asp:HiddenField ID="hfExpID" runat="server" Value='<%#Eval("CandidateExpId") %>' />
                                </td>
                                <td style="text-align:center;">
                                   <asp:TextBox ID="txtcompname" runat="server" CssClass="required_box" Width="182px" MaxLength="49" Text='<%#Eval("CompanyName") %>' ></asp:TextBox>
                                </td>
                                <td  style="text-align:center;">
                                    <asp:TextBox ID="txtvesselname" runat="server" CssClass="required_box" Width="182px" MaxLength="49"  Text='<%#Eval("VesselName") %>' ></asp:TextBox>
                                </td>
                                <td  style="text-align:center;">
                                     <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="required_box" Width="155px" >
                                                                                </asp:DropDownList>
                                </td>
                                <td style="text-align:center;"  >
                                   <asp:TextBox ID="txtfromdate" runat="server" CssClass="required_box" Width="94px" MaxLength="15"  Text='<%#Common.ToDateString(Eval("SignOn")) %>'></asp:TextBox>  
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfromdate" PopupPosition="TopRight" PopupButtonID="txtfromdate"></asp:CalendarExtender>                                                 
                                </td>
                                 <td style="text-align:center;" >
                                 <asp:TextBox ID="txtToDate" runat="server" CssClass="required_box" Width="94px" MaxLength="15"  Text='<%#Common.ToDateString(Eval("SignOff")) %>'></asp:TextBox>
                                     <asp:RegularExpressionValidator ID="revToDate" runat="server" ControlToValidate="txtToDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                     <asp:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtToDate" PopupPosition="TopRight" PopupButtonID="txtToDate" > </asp:CalendarExtender>     
                                </td>
                                <td  style="text-align:center;" >
                                     <asp:DropDownList ID="ddl_Rank1" runat="server" CssClass="required_box" Width="115px" >
                                                                                </asp:DropDownList>
                                </td>
                                <td  style="text-align:center;">
                                    <asp:TextBox ID="txtbhp1" runat="server" CssClass="required_box" MaxLength="20"  Width="110px" Text='<%#Eval("BHP1") %>'></asp:TextBox>
                                </td>
                                 <td  style="text-align:center;">
                                    <asp:TextBox ID="txtGRT" runat="server" CssClass="required_box" Width="110px" MaxLength="20" Text='<%#Eval("BHP") %>'></asp:TextBox>
                                </td>
                               <td style="padding-left:10px; padding:2px;" >
                                    <asp:ImageButton ID="imgDelete" runat="server" ToolTip="Delete" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/trash.gif" onkeypress="DeleteSpareRows(this);" OnClick="imgDelete_Click" OnClientClick="javascript:return window.confirm('Are you Sure to Delete Sea Experience?');" />
                                    <asp:Label ID="lblSrNo" style="font-size: smaller; color: Maroon; display: none;" runat="server" ></asp:Label>
                                </td>
                                                                                             
                            </tr>
                           <tr>
                            <td  align="left" width="4%"></td>
                            <td  align="left" width="18%">   
                               
                            </td>
                            <td align="left" width="18%"></td>
                            <td align="left" width="10%"></td>
                            <td align="left" width="10%"></td>
                            <td align="left" style="width: 10%"></td>                     
                            <td align="left" width="10%"></td>
                            <td align="left" width="10%"></td>
                            <td align="left" width="10%" ></td>
                            <td align="left" width="4%" ></td>
                           </tr>
                           
                            </ItemTemplate> 
                            </asp:Repeater>
                            </table>
                            </div>
                        </td>
                    </tr>
        </table>
                                <table Id="tblAttachment" runat="server" visible="false" border="0" cellpadding="3" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%"> 
                                     <tr style="height:20px; color:#ffffff; font-size:14px;" >
            <td style="background-color:#4c7a6f;">
             <asp:ImageButton Text=" + Add" OnClick="btnAddAtt_Click" ID="btnAddAtt" ImageUrl="~/Modules/HRD/Images/add_16.gif"  runat="server"  CausesValidation="false" style="float:left"/>
            <%--<asp:LinkButton runat="server" id="imgbtnAddSeaExp" OnClick="imgbtnAddSeaExp_Click" Text="Add Sea Experience" ForeColor="White" style="float:left"  CausesValidation="false"/>--%>
           <b> Documents </b></td>
        </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlAtt" runat="server" Visible="true">
                                <div style="">
                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; height: 30px; border-collapse: collapse;" class="bordered">
                                        <colgroup>
                                            <col width="40px" />
                                            <col />
                                            <col width="60px" />
                                            <tr style="font-weight: bold; text-align: left; color:#333">
                                                <td>File</td>
                                                <td>Description </td>
                                                <td>Delete </td>
                                            </tr>
                                        </colgroup>
                                    </table>
                                </div>
                               
                                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; border-collapse: collapse;">
                                        <colgroup>
                                            <col width="40px" />
                                            <col />
                                            <col width="60px" />
                                        </colgroup>
                                        <asp:Repeater ID="rptCommAtt" runat="server">
                                            <ItemTemplate>
                                                <tr style="text-align: left;">
                                                    <td style="text-align: center;"><%--<asp:ImageButton ID="btnCommFile" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnDownloadClick" CommandArgument='<%#Eval("TableID")%>' ToolTip='<%#Eval("FileName") %>' />  --%><a onclick='DownLoadFile(<%#Eval("TableID")%>,&#039;<%#Eval("FileName")%>&#039;)' style="cursor: pointer;">
                                                        <img src="../Images/paperclip.gif" title=" Download " />
                                                    </a></td>
                                                    <td><%#Eval("Descr")%></td>
                                                    <td style="text-align: center;">
                                                        <asp:ImageButton ID="btnDeleteCommFile" runat="server" CommandArgument='<%#Eval("TableID")%>' ImageUrl="~/Modules/HRD/Images/trash.gif" OnClick="btnDeleteCommFile_OnClick" OnClientClick="return confirm('Are you sure to delete this file?');" Style="cursor: pointer;" ToolTip="Delete" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                <asp:ImageButton ID="btnDownLoadFile" runat="server" OnClick="btnDownLoadFile_OnClick" Style="display: none;" />
                                <asp:HiddenField ID="hfTableID" runat="server" Value="" />
                                <asp:HiddenField ID="hfFileName" runat="server" Value="" />
                                
                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width:100%;border:solid 1px #008AE6;margin-top:5px;border-radius:5px;">
                                    <tr style="height:30px;">
                                        <td class="RowColumn1">
                                            Created By/On : <asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label>
                                        </td>
                                       
                                        
                                    </tr>
                                   <%-- <tr id="tr_Comm" runat="server">
                                        <td class="RowColumn1">
                                            Req. Comments :
                                        </td>
                                        <td class="RowColumn2">
                                             <asp:TextBox ID="txtComm" runat="server" Height="50px" ReadOnly="True" Style="border: solid 1px gray" TextMode="MultiLine" Width="255px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                  <div style="display:flex;padding-top:5px;">
                        <span > 
                            <asp:Label ID="lbl_info" runat="server" ForeColor="Red" Width="500px" style="float:left;"></asp:Label>
                        </span>
                      <asp:Button ID="btnEdit" runat="server" CssClass="btn" Height="25px" OnClick="btnEdit_Click" Text=" Edit "  CausesValidation="false"/>
  <asp:Button ID="btnSave" runat="server" CssClass="btn" Height="25px" OnClick="btnSave_Click" Text=" Save " Visible="false" />
                    </div>
                              <br /> 
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddAttachment" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 100px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">

                    <table border="0" cellpadding="1" cellspacing="3" style="width: 100%; width: 100%;">
                        <tr>
                            <td colspan="4"><b>Attach Files </b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtAttDesc" runat="server" CssClass="input_box" Width="300px" placeholder="File Description"></asp:TextBox>
                            </td>
                            <td>
                                <asp:FileUpload ID="fuCommAttachment" runat="server" CssClass="input_box" Width="350px" placeholder="Add Attachment" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>

                    <div style="padding: 10px;">
                        <asp:Button ID="btnAddCommAtt" runat="server" CssClass="btn" CausesValidation="false" Height="25px" OnClick="btnAddCommAtt_OnClick" Text=" Save " />
                        <asp:Button ID="Button1" runat="server" CssClass="btn" Height="25px" CausesValidation="false" OnClick="btnClose1_OnClick" Text=" Close " />
                    </div>
                </div>
            </center>
        </div>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvAddComm" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 180px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px; border: solid 10px #E0E0FF;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Width="100%"></asp:Label>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr>
                            <td style="vertical-align: top">Date</td>
                            <td style="vertical-align: top; text-align: center" valign="top">Details</td>
                            <td rowspan="2" style="text-align: left;">
                                <asp:RadioButtonList ID="radCommType" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Text="Phone" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="EMail" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="InPerson" Value="I"></asp:ListItem>
                                </asp:RadioButtonList>
                                <div style="width: 150px; text-align: left">
                                    &nbsp;&nbsp;
                            <%--<asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Modules/HRD/Images/add_16.gif" onclick="" style="height: 16px" ToolTip="Add/Udpate this conversation."  />--%>
                            &nbsp;
                            <asp:ImageButton ID="btnClear" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/clear.png" OnClick="btnClear_Click" Style="height: 16px" ToolTip="Clear Text." />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <asp:TextBox ID="txtConDate" runat="server" AutoPostBack="true" CssClass="input_box" MaxLength="15" OnTextChanged="Validate" Style="text-align: center" Width="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtConDate" Display="Static" ErrorMessage="mandatory" ValidationGroup="det"></asp:RequiredFieldValidator>
                                <br />
                                <asp:Label ID="lblDateMess" runat="server" ForeColor="Red" Text="Invalid date."></asp:Label>
                            </td>
                            <td style="vertical-align: top; text-align: center;" valign="top">
                                <asp:TextBox ID="txtCon" runat="server" CssClass="input_box" Height="75px" TextMode="MultiLine" Width="570px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtCon" Display="Static" ErrorMessage="mandatory" ValidationGroup="det"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>


                    <div style="padding: 10px;">
                        <asp:Button ID="Button2" runat="server" CssClass="btn" OnClick="btnAdd_Click" Text=" Save " ValidationGroup="det" />
                        <asp:Button ID="Button3" runat="server" CssClass="btn" OnClick="btnClose2_OnClick" Text=" Close " />
                    </div>
                </div>
            </center>
        </div>
                <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dvComments" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 0px; left: 0px; height: 4100%50px; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 800px; height: 180px; padding: 3px; text-align: center; background: white; z-index: 150; top: 100px;">
                    <div style="border: solid 10px #E0E0FF;">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                            <tr>
                                <td style="text-align: left; font-weight: bold;">Enter your comments here :-</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtComments" runat="server" Style="width: 98%; height: 100px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSaveComments" runat="server" CssClass="btn" Height="25px" Text=" Save " OnClick="btnSaveComments_OnClick" />
                                    <asp:Button ID="btnCloseSaveComments" runat="server" CssClass="btn" Height="25px" Text=" Close " OnClick="btnCloseSaveComments_OnClick" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCommentsMsg" runat="server" Style="font-weight: bold; color: Red;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </center>
        </div>
                <div style="position: absolute; top: 25px; left: 100px; height: 550px; width: 85%;" id="dvMailSend" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 25px; left: 100px; height: 550px; width: 85%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 85%; height: 500px; padding: 3px; text-align: center; background: white; z-index: 150; top: 50px; border: solid 1px Black">
                      <div style=" text-align: center;vertical-align:central " class="text headerband">
                   Mail to Applicant for Sea Experience
                           <div style=" float :right " >
                          <asp:ImageButton ID="ibClose" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="ibClose_Click" CausesValidation="false" />
                               </div>
             </div>
                    <div style=" text-align: center;width:100%;padding-left:100px;padding-right:100px;padding:5px;">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label> 
                    </div>
            <div style=" text-align: center;width:100%;">
                <table style="width:100%;" >
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            From Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtFromAddress" runat="server" TabIndex="3" TextMode="SingleLine" MaxLength="200"   Width="700px" ReadOnly="true" ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvFromAddress" runat="server" ControlToValidate="txtFromAddress" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Applicant Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtToEmail" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="rfvToEmail" runat="server" ControlToValidate="txtToEmail" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                        </td>
                       
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            BCC Email : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtBCCEmail" runat="server" TabIndex="4" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;">
                            Subject : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <asp:TextBox ID="txtSubject" runat="server" TabIndex="5" TextMode="SingleLine" MaxLength="500"  Width="700px" Style="background-color: lightyellow"  ></asp:TextBox>
                        </td>
                        
                    </tr>
                     <tr>
                        <td style="padding-right:5px;text-align:right;width:200px;padding:5px;vertical-align:top;">
                            Email Body : 
                        </td>
                        <td style="padding-left:5px;text-align:left;vertical-align:middle;padding:5px;" >
                             <%--<asp:TextBox ID="txtEmailBody" runat="server" TabIndex="6" TextMode="MultiLine" MaxLength="4000"   CssClass="input_box" Width="700px" Height="200px" ></asp:TextBox> &nbsp;  <asp:RequiredFieldValidator ID="rfvEmailBody" runat="server" ControlToValidate="txtEmailBody" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>--%>
                            <div contenteditable="true" runat="server" id="dvContent" style="overflow-x:hidden; overflow-y :scroll; width:100%;height:250px;"  onscroll="SetScrollPos(this)">
                            <asp:Literal runat="server" ID="litMessage"></asp:Literal> 
                            </div>
                        </td>
                      
                    </tr>
                </table>
            </div>
             <div style=" text-align: center;width:70%;padding-left:100px;padding-right:100px;padding:5px;">
                 <asp:Button ID="btnSendMailforExp" runat="server" CssClass="btn" Width="150px" Height="30px" CausesValidation="false" Text="Send Mail to Applicant" OnClientClick="return confirm('Are you sure to Mail Send this applicant for Sea Experience?');" OnClick="btnSendMailforExp_Click" />
                 <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text=" Close " Height="30px" CausesValidation="false" OnClick="btnCancel_Click"  />
             </div>
                    
                </div>
            </center>
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

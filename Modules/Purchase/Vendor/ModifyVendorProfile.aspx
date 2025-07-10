<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyVendorProfile.aspx.cs" Inherits="Docket_ModifyVendorProfile" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>EMANAGER</title>  
     <script src="JS/JQuery.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>   
    <script type="text/javascript" language="javascript">
        function fncInputNumericValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function GoToPage(pageno) {
            $("#hid_PageNo").val(pageno);
            $("#btnPageNo").click();
        }
    </script>
    
<script type="text/javascript">
    function Validate(sender, args) {
        var repeator = document.getElementById("tblVendors");
        var checkBoxes = repeator.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }
</script>

    <style type="text/css">
    body
    {
        font-family:Calibri;
        font-size:14px;
        margin:0px;
        padding:0px;
    }
   *
    {
        box-sizing:border-box;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }
    .center
    {
        text-align:center;
    }
    .center div
    {
        margin:0 auto;
    }
    .left
    {
        text-align:left;
    }
    .right
    {
        text-align:left;
    }
    h1
    {
        font-size:22px;
        background-color: #35356f;
        border-bottom: 5px solid #00001f;
        color:#fff;
        padding:10px;
        margin:0px;
    }
    
    h2 {
  background-color: #fafafa;
  color: #666;
  font-size: 17px;
  margin: 0;
  padding: 10px;
  text-align: left;
}
    .formcontainer
    {
        padding:5px 30px 5px 30px;
      
    }
    .label
    {display:inline-block; text-align:left; float:left; width:400px; position:relative;}
    .label:after
    {content:":"; text-align:right; width:100px; padding-right:5px; display:inline; float:right; position:absolute; right:5px;top:0px;}
    .controlarea
    {display:inline-block; text-align:left; }
    .row
    {
        padding:3px;
        text-align:left;
    }
    .control
    {
        border:solid 1px #ddd;
        padding:5px;
        line-height:14px;
    }
  
    .actionbox
    {
        text-align:center;
        padding:5px;
    }
    .stages
    {
        margin:5px;
        padding:15px;
    }
    .stage
    {
       display:inline-block;
       background-color:#eee;
       padding:5px 20px 5px 5px;
       margin:0px;
       width:120px;
    }
     .stage:hover,.active
    {
       background-color:#7cb02c;
       color:White;
    }
    .stage a
    {
        color:black;
        text-decoration:none;
    }
    .stage:hover a,.active a
    {
        color:white;
    }
    
    .path
    {
        background-color: #eeeeee;
        display: inline-block;
        margin: 0 -3px !important;
        padding: 2px;
        width: 10px;
    }
    .info
    {
        color:Maroon;
        font-style:italic;
        font-size:11px;
    }
    .bold
    {
    font-weight:bold;
    }
    .circle
    {
        border-radius:8px;
        width:18px;
        height:18px;
        line-height:18px;
        color:#7cb02c;
        font-size:12px;
        display:inline-block;
        float:left;
        background-color:#fff;
        margin-right:10px;
    }
.bgmodal
{
    background-color:Black;
    opacity: 0.6;
    filter: alpha(opacity=60);
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:5;
}
.modalframe
{
    position:fixed;
    top:0px;
    left:0px;
    height:100%;
    width:100%;
    z-index:6;
    text-align:center;
    margin:0px auto;
    padding-top:5%;    
}
.modalborder
{
    background:rgba(0,0,0,0.3);
    width:80%;
    margin:0px auto;
    padding:0px;
    border:solid 10px grey;
}
.modalcontainer
{
    width:100%;
    height:100%;
    background-color:White;
    padding:10px;
}

.close
{
    background-color:#FF6262;
    border: 1px solid #FF6262;
    color:#fff;
}
.btn:hover, .btn:focus {
  background-color: #35356f;
  border-color: #35356f;
  color: #fff;
}
.close:hover,.close:focus
{
   background-color:#FF7373;
   border: 1px solid #FF7373;
}
hr
{
    margin-bottom:0px;
    padding-bottom:0px;
}

.alternate_table 
{
    border:solid 1px #f1f1f1;
}
.alternate_table tr:nth-child(even)
{
    background-color:#f1f1f1;
}
.alternate_table tr:nth-child(odd)
{
    background-color:white;
}
.pageframe
{
    border: 1px solid #eee;
    border-radius: 4px;
    display: inline-block;
    overflow: hidden;
    margin-top:10px !important;
    width:80%;
}
.msgbox
{
    padding:5px 5px 7px 5px;
    border:solid 1px #eee;
    text-align:center;
    margin-left:10px;
    margin-right:10px;
}
.success
{
    background-color:#00A300;
    color:White;
}
.error
{
    background-color:#FF7E5E;
    color:White;
}
</style>
    <style type="text/css">
        .btn {
	/*color:White ;
	background-image:url(images/bar_bg.png);
	height :25px;*/
	border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="bgmodal" runat="server" id="modalBox" visible="false">2</div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:Button runat="server" ID="btnPageNo" OnClick="btnPageNo_Click" CausesValidation="false" style="display:none" />
    <asp:HiddenField runat="server" ID="hid_PageNo" />
    <div style="text-align:center;color:white;background-color:#4c7a6f;width:100%;font-family:Arial;font-size:16px;height:30px;vertical-align:central;">
            Vendor / Supplier Registration
        </div>
    <div class="center">
        <div class="stages">
        <table cellpadding="0" cellspacing="0" style="display:none;">
        <tr>
        <td><div id="stg_1" runat="server" class="stage active"><a href="#" onclick="GoToPage(1);" ><span class="circle">1</span>Profile</a></div></td>
        <td><div class="path"></div></td>
        <td><div id="stg_2" runat="server" class="stage"><a id="A1" href="#" onclick="GoToPage(2);"><span class="circle">2</span>Description</a></div></td>
        <td><div class="path"></div></td>
        <td><div id="stg_3" runat="server" class="stage"><a id="A2" href="#" onclick="GoToPage(3);"><span class="circle">3</span>Bank Details</a></div>       </td>
        </tr>
        </table>
         <div class="actionbox">
            <asp:Button runat="server" ID="btnPrev1" CssClass="btn" Text="< Previous" onclick="btnPrev_Click" CausesValidation="false" Visible="false" />
            <asp:Button runat="server" ID="btnNext1" CssClass="btn" Text="Next > " onclick="btnNext_Click" />
        </div>
        <!-- First Page (profile) -->
        <div class="pageframe" id="dv_Page_1" runat="server"> 
            <div style="text-align:center;color:white;background-color:#4c7a6f;width:100%;font-family:Arial;font-size:14px;height:30px;vertical-align:central;">
           Company Profile
        </div>
            
            <div class="formcontainer">
                         <div class="row">
            <span class="label">Name of Company</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_NoC" CssClass="control mandate" Width="500px" MaxLength="250" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatonoc" runat="server" ErrorMessage="*" ControlToValidate="txt_NoC"></asp:RequiredFieldValidator>
            </span>
        </div>
        
            <div class="row">
            <span class="label">Address Line 1</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_address1" CssClass="control mandate" Width="500px" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_address1"></asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="row">
            <span class="label">Address Line 2</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_address2" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txt_address2"></asp:RequiredFieldValidator>
            </span>
        </div>
             <div class="row">
            <span class="label">City / State</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_city_state" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txt_city_state"></asp:RequiredFieldValidator>
            </span>
        </div>
         <div class="row">
            <span class="label">ZIP Code / Postal Code </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_zipcode" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txt_zipcode"></asp:RequiredFieldValidator>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderzipcode" runat="server" TargetControlID="txt_zipcode" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            </span>
        </div>
         <div class="row">
            <span class="label">Country</span>
            <span class="controlarea"><asp:DropDownList runat="server" ID="ddlCountry" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="ddlCountry" InitialValue="0"></asp:RequiredFieldValidator>
            </span>
        </div>
          <div class="row">
            <span class="label">Phone No. </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_phone_no" CssClass="control mandate" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_phone_no1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_phone_no2" CssClass="control mandate" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_phone_no" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_phone_no2" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_phone_no" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_phone_no1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_phone_no2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
        <div class="row">
            <span class="label">Fax No. </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_fax_no" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_fax_no1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_fax_no2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Fax No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_fax_no" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_fax_no1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_fax_no2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
        <div class="row">
            <span class="label">Email Address </span>
            <span class="controlarea">
                <asp:TextBox runat="server" ID="txt_email" CssClass="control mandate" Width="500px"  MaxLength="250" ></asp:TextBox> </span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatoremail" runat="server" ErrorMessage="*" ControlToValidate="txt_email" Display="Dynamic"></asp:RequiredFieldValidator> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*;?\s*)*)+$"> </asp:RegularExpressionValidator>
            <asp:Label ID="lblEmail" runat="server" Text="Multiple email addresses should be seperated by Semicolon (;)" ForeColor="Red"></asp:Label>
            
        </div>
        <div class="row">
            <span class="label">Web Site </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_website" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Company Reg No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_company_reg_no" CssClass="control" Width="500px" MaxLength="250"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Tax Reg No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_tax_reg_no" CssClass="control" Width="500px" MaxLength="250"></asp:TextBox></span>
        </div>
        <div class="row">
            <span class="label">Number of Years in Business (Subject Office)</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_no_of_year_business" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txt_no_of_year_business" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            </span>
        </div>
        <div class="row bold">
            <span>Contact Person Details</span>            
        </div>
         <div class="row">
         <hr />
        </div>
           <div class="row">
            <span class="label">Contact Person Name</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person" CssClass="control mandate" Width="500px" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="*" ControlToValidate="txt_Contact_Person"></asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="row">
            <span class="label">Position</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_Position" CssClass="control mandate" Width="500px" MaxLength="250"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="txt_Contact_Person_Position"></asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="row">
            <span class="label">Direct Phone No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_DirectPhone" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_DirectPhone1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_DirectPhone2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt_Contact_Person_DirectPhone" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txt_Contact_Person_DirectPhone1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txt_Contact_Person_DirectPhone2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
          <div class="row">
            <span class="label">Mobile No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_cp_mobile" CssClass="control mandate" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_cp_mobile1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_cp_mobile2" CssClass="control mandate" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txt_cp_mobile" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_cp_mobile2" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txt_cp_mobile" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txt_cp_mobile1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txt_cp_mobile2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
         <div class="row">
            <span class="label">Fax No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_FaxNo" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_FaxNo1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_FaxNo2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txt_Contact_Person_FaxNo" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txt_Contact_Person_FaxNo1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_Contact_Person_FaxNo2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
      <div class="row">
            <span class="label">Email Address</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_Contact_Person_Email" CssClass="control mandate" Width="500px" MaxLength="250"></asp:TextBox></span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorContact_Person_Email" runat="server" ErrorMessage="*" ControlToValidate="txt_Contact_Person_Email" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_Contact_Person_Email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*;?\s*)*)+$"> </asp:RegularExpressionValidator>
            
        </div>
        <div class="row">
        <hr />
        </div>
        <div class="row">
            <span class="label">Head of Subject Company, Title (Mr / Ms) </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorhead_of_company" runat="server" ErrorMessage="*" ControlToValidate="txt_head_of_company" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </span>
        </div>
        <div class="row">
            <span class="label">Position</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company_postion" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox>
           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="txt_head_of_company_postion" Display="Dynamic"></asp:RequiredFieldValidator>--%>
            </span>
        </div>
        <div class="row">
            <span class="label">Phone No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company_no" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company_no1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company_no2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt_head_of_company_no" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt_head_of_company_no1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txt_head_of_company_no2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>


        <div class="row">
            <span class="label">Email Address</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_company_email" CssClass="control mandate" Width="500px"  MaxLength="250"></asp:TextBox></span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_head_of_company_email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*,?\s*)*)+$"> </asp:RegularExpressionValidator>
            
        </div>
        <div class="row">
        <hr />
        </div>
        <div class="row">
            <span class="label">Head of Finance / Accounts (Mr /Ms)</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_finance" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox></span>
        </div>
         <div class="row">
            <span class="label">Phone No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_finance_no" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_finance_no1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_finance_no2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderhead_of_finance_no" runat="server" TargetControlID="txt_head_of_finance_no" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderhead_of_finance_no1" runat="server" TargetControlID="txt_head_of_finance_no1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderhead_of_finance_no2" runat="server" TargetControlID="txt_head_of_finance_no2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>

        <div class="row">
            <span class="label">Email Address</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_finance_email" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox></span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorhead_of_finance_email" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_head_of_finance_email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*,?\s*)*)+$"> </asp:RegularExpressionValidator>
            
        </div>
        <div class="row">
        <hr />
        </div>
        <div class="row">
            <span class="label">Head of  Quality Management/Equivalent(Mr / Ms) </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_quality" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox></span>
        </div>
        <div class="row">
            <span class="label">Phone No.</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_quality_no" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_quality_no1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_of_quality_no2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server" TargetControlID="txt_head_of_quality_no" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txt_head_of_quality_no1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="txt_head_of_quality_no2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>

        <div class="row">
            <span class="label">Email Address</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_head_of_quality_email" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox> </span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_head_of_quality_email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*,?\s*)*)+$"> </asp:RegularExpressionValidator>
           
        </div>
        <div class="row">
        <hr />
        </div>
        <div class="row">
            <span class="label">Name of Head Office (or Parent Company)</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_head_office_name" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox></span>
        </div>
             <div class="row">
            <span class="label">City / State</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_City_State" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            </span>
        </div>
         <div class="row">
            <span class="label">ZIP Code / Postal Code </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_Zip_Postal_Code" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="txt_HO_Zip_Postal_Code" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            </span>
        </div>
         <div class="row">
            <span class="label">Country</span>
            <span class="controlarea"><asp:DropDownList runat="server" ID="ddl_HO_Country" CssClass="control" Width="500px"  MaxLength="250"></asp:DropDownList>
            </span>
        </div>
          <div class="row">
            <span class="label">Phone No. </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_PhoneNo" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_PhoneNo1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_PhoneNo2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Phone No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="txt_HO_PhoneNo" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="txt_HO_PhoneNo1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txt_HO_PhoneNo2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
        <div class="row">
            <span class="label">Fax No. </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_FaxNo" CssClass="control" Width="100px"  MaxLength="250" placeholder="Country Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_FaxNo1" CssClass="control" Width="100px"  MaxLength="250" placeholder="Area Code"></asp:TextBox></span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_FaxNo2" CssClass="control" Width="293px"  MaxLength="250" placeholder="Fax No."></asp:TextBox></span>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" TargetControlID="txt_HO_FaxNo" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" TargetControlID="txt_HO_FaxNo1" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" TargetControlID="txt_HO_FaxNo2" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
        </div>
        <div class="row">
            <span class="label">Email Address </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_Email" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox></span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txt_HO_Email" CssClass="requiredFieldValidateStyle" ForeColor="Red" ValidationExpression="^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,}(\s*,?\s*)*)+$"> </asp:RegularExpressionValidator>
            
        </div>
        <div class="row">
            <span class="label">Web Site </span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_HO_WebSite" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Number of Years in Business (HQ / Parent Co.)</span>
            <span class="controlarea"><asp:TextBox runat="server" ID="txt_no_of_year_business_HQPC" CssClass="control" Width="500px"  MaxLength="250"></asp:TextBox>
            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" TargetControlID="txt_no_of_year_business_HQPC" ValidChars="0123456789" FilterMode="ValidChars"></asp:FilteredTextBoxExtender>
            </span>
        </div>
         <div class="row">
            <span class="label">Company OwnerShip Structure</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_CompanyOwnerStruct" CssClass="control" Width="500px" TextMode="MultiLine" Rows="8"  MaxLength="500"></asp:TextBox>
            </span>
        </div>
        </div>
       
         </div>

        <!-- Second Page (Description)-->
        <div class="pageframe"  id="dv_Page_2" runat="server" visible="false"> 
             <div style="text-align:center;color:white;background-color:#4c7a6f;width:100%;font-family:Arial;font-size:14px;height:30px;vertical-align:central;">
          Description
        </div>
          
            <div class="formcontainer"> 
              <div class="row center bold">
            <span>Please tick as applicable (multiple selection) and provide additional description</span><hr />
        </div>     
           <div class="row">
            <span class="label">Short Description of Business </span>
            <span class="controlarea">
            <asp:CheckBoxList id="chkVendorbusinesseslist" runat="server"></asp:CheckBoxList>               
            </span>
            </div>
                <br />
        <div class="row">
            <span class="label">Services (Specify)</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_Services_Specify" CssClass="control" Width="500px" MaxLength="500" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Additional Description</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_Add_Description" CssClass="control" Width="500px" MaxLength="500" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Other Services (Specify)</span>
            <span class="controlarea">
            <asp:TextBox runat="server" ID="txt_description_other" CssClass="control" Width="500px" MaxLength="500"  TextMode="MultiLine" Rows="5"></asp:TextBox>
            </span>
        </div>
       
        </div>
        </div>

        <!-- Seven Page (Bank Details)-->
        <div class="pageframe"  id="dv_Page_3" runat="server" visible="false">
             <div style="text-align:center;color:white;background-color:#4c7a6f;width:100%;font-family:Arial;font-size:14px;height:30px;vertical-align:central;">
          Bank Details
        </div>
        
          <div class="formcontainer">  
        <div class="row">
            <span class="label">Bank Name </span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_name" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
         <div class="row">
            <span class="label">Address 1</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_address1" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Address 2</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_address2" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Post Code</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_postcode" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
         <div class="row">
            <span class="label">City</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_city" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
         <div class="row">
            <span class="label">Country</span>
            <span class="controlarea">
          <asp:DropDownList ID="ddlbank_counrty" runat="server"  CssClass="control" ></asp:DropDownList>
            </span>
        </div>
         <div class="row">
            <span class="label">Account No</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_account_no" CssClass="control" Width="450px" MaxLength="4" TextMode="Password"></asp:TextBox> (Last 4 digits only)
            </span>
        </div>
         <div class="row">
            <span class="label">SWIFT Code</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_swift_code" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
      <div class="row">
            <span class="label">IBAN</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_bank_IBAN" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>
               <div class="row">
            <span class="label">IFSC Code</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txtIFSCCode" CssClass="control" Width="500px" MaxLength="16"></asp:TextBox>
            </span>
        </div>
        <div class="row">
            <span class="label">Preferred Single Currency</span>
            <span class="controlarea">
           <asp:TextBox runat="server" ID="txt_pref_single" CssClass="control" Width="500px"   MaxLength="500"></asp:TextBox>
            </span>
        </div>    
              
        <div class="row">
            <span class="label">
                Company Data Protection Policy
            </span>
            <span class="controlarea">
           <asp:CheckBox runat="server" ID="chkaccept" Text="I agree with Company Data Protection Policy"  ></asp:CheckBox>
                ( <asp:Label runat="server" ID="lblacceptedon"></asp:Label>  )
                <br />
                <a href="http://192.168.1.18/External/Purchase/Vendor/dpp.htm" target="_blank">Company Data Protection Policy</a>
            </span>
        </div>       
        
      
        </div>
        </div>
        <div class="actionbox">
            <asp:Button runat="server" ID="btnPrev" CssClass="btn" Text="< Previous" onclick="btnPrev_Click" CausesValidation="false" Visible="false" />           
            <asp:Button runat="server" ID="btnNext" CssClass="btn" Text="Next > " onclick="btnNext_Click" />
            <asp:Button runat="server" ID="btnSubmit" CssClass="btn" Text="Save" onclick="btnSubmit_Click" CausesValidation="false" Visible="false" />
        </div>
        <div class="row">
            <div class="msgbox" runat="server" id="lblMessage"></div>
        </div>
    </div>
</div>
    <script type="text/javascript"></script>
    </form>
</body>
</html>

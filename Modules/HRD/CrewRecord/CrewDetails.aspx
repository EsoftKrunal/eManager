<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="CrewDetails" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%--<tr>
                                        <td style="height: 38px;">
                                           
                                        </td>
                                        <td style="height: 38px; width: 50px;">
                                        </td>
                                        <td style="height: 38px;width: 50px;"></td>
                                        <td style="height: 38px;width: 50px;"></td>                                    </tr>--%>
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function Show_Image_Large(obj) {
            window.open(obj.src, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }
    </script>
    <script type="text/javascript">
        function ShowRemarks(CBId, MUM) {
            document.getElementById('hfCBId').value = CBId;
            document.getElementById('hfMUM').value = MUM;
            document.getElementById('btnShowRemarks').click();
        }
    </script>
    <style type="text/css">
       .Grade_A
       {
           background:#CCFF66; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_B
       {
           background:yellow; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_C
       {
           background:#FFC2B2; 
           color:Black ;
           width:15px;
           height:15px;
           border:solid 1px grey;
       }
       .Grade_D
       {
           background:red; 
           width:15px;
           height:15px;
           color:white;
           border:solid 1px grey;
       }
    </style>
     <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
    </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
    <script language="javascript" type="text/javascript">
    function printcv()
    {
     var crewid=<%try{Response.Write(Session["CrewId"].ToString());}catch{}%>;
        if(!(parseInt(crewid)==0 || crewid==""))
        {
        window.open('..\\Reporting\\PrintCV.aspx?crewid='+ crewid,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    </script>
       <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; width :100%">
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" ><img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;Personal</td>
            </tr>
            <tr>
                <td style="text-align:left;height:25px;padding-left:10px;background-color:#fff">
                    <asp:LinkButton ID="b5" runat="server" Text ="Documents" Font-Bold="True" OnClick="b5_Click" ForeColor="#206020"></asp:LinkButton>
                    &nbsp; &nbsp;&nbsp;
                    <asp:LinkButton ID="b6" runat="server" Text ="Notes & History" Font-Bold="True" OnClick="b6_Click" ForeColor="#206020"></asp:LinkButton>
                    &nbsp; &nbsp;&nbsp;  
                    <asp:HyperLink runat="Server" ID="doc_Alert" Target="_blank" NavigateUrl="DocumentAlerts.aspx" ForeColor="Red" ></asp:HyperLink> 
                    <asp:Label ID="crm_Alert" runat="server" ForeColor="Red"></asp:Label>
                     &nbsp; &nbsp;&nbsp; 
                    <asp:LinkButton runat="server" ID="lnkPopUp" Visible ="false" ForeColor="Red" Font-Size="14pt" OnClientClick ="window.open('../CriticalRemarkPopUp.aspx');return false;" Text ="Remarks" Font-Bold="True" ></asp:LinkButton>
                </td>
                <td style="text-align:center;background-color:#fff;font-size:14px;">
                    <strong>[<asp:Label id="txt_MemberId" runat="server"></asp:Label>]</strong>
                </td>
            </tr>
            <tr>
                <td style="width: 75%;">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left;" >
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: center;font-weight: bold;" colspan="4"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                        <%#Eval("FirstName")%>
                                    </tr><%#Eval("LastName")%>
                                    
                                </table>
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="Tab1" runat="server">
                                        <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="display:none;" >
                                          <tbody>
                                          <tr>
                                                  <td align="right" style="width: 190px; text-align: right">
                                                      </td>
                                                  <td style="padding-left: 5px; width: 230px; text-align: left">
                                                      
                                                     </td>
                                                  <td style="padding-left: 5px; width: 193px; text-align: right">
                                                      Status:</td>
                                                  <td style="padding-left: 5px; text-align: left" colspan="3">
                                                      <asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" MaxLength="10" meta:resourcekey="txt_MemberIdResource1" ReadOnly="True" Width="400px"></asp:TextBox>
                                                  </td>
                                                  
                                              </tr>
    </tbody>
                                        </table>
                                        </fieldset>
                                        <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid">
                                        <legend><strong>Personal Details</strong></legend>
                                        
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 66%;" valign="top">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                           <tr>
                                                  
                                                                               <td align="right" style="width: 190px; height: 13px; text-align: right">
                                                                                       <asp:Label ID="Label43" runat="server" meta:resourcekey="Label43Resource1" Text="First Name:" Width="72px">

                                                                                        </asp:Label>
                                                                                       </td>
                                                  
                                                                               <td style="padding-left: 5px; width: 230px; height: 13px; text-align: left">
                                                      <asp:TextBox ID="txt_FirstName" runat="server" CssClass="required_box" MaxLength="24" meta:resourcekey="txt_FirstNameResource1" TabIndex="1" Width="160px"></asp:TextBox>
                                                       
                                                      </td>
                                                  
                                                                               <td style="padding-left: 5px; width: 193px; height: 13px; text-align: right">
                                                      <asp:Label ID="Label37" runat="server" meta:resourcekey="Label37Resource1" Text="Middle Name:" Width="82px"></asp:Label>
                                                      </td>
                                                  
                                                                               <td style="padding-left: 5px; width: 199px; height: 13px; text-align: left">
                                                      <asp:TextBox ID="txt_MiddleName" runat="server" CssClass="input_box" MaxLength="24" TabIndex="2" Width="160px"></asp:TextBox>
                                                      </td>
                                                  
                                                                               <td align="right" style="width: 176px; height: 13px">
                                                      <asp:Label ID="Label38" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:" Width="100%"></asp:Label>
                                                     </td>
                                                  
                                                                               <td style="padding-left: 5px; width: 204px; height: 13px; text-align: left">
                                                      <asp:TextBox ID="txt_LastName" runat="server" CssClass="required_box" MaxLength="24" TabIndex="3" Width="160px"></asp:TextBox>
                                                        
                                                      </td>
                                              </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 190px; height: 13px; text-align: right">

                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 230px; height: 13px; text-align: left">
                                                                                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Required." ControlToValidate="txt_FirstName" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                <td  style="padding-left: 5px; width: 193px; height: 13px; text-align: right">

                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 199px; height: 13px; text-align: left">
                                                                                    </td>
                                                                                <td align="right" style="width: 176px; height: 13px">
                                                                                    </td>
                                                                                <td style="padding-left: 5px; width: 204px; height: 13px; text-align: left">
                                                                                     <asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ErrorMessage="Required." ControlToValidate="txt_LastName" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                                                                                    </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 19px" ><asp:Label id="Label40" runat="server" Width="86px" Text="Date Of Birth :" meta:resourcekey="Label40Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; height: 19px; text-align: left" >
                                                                                    <asp:TextBox ID="txt_DOB" runat="server" CssClass="required_box" Width="140px" TabIndex="4" AutoPostBack="True" ontextchanged="txt_DOB_TextChanged"></asp:TextBox>&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td align="right" style="width: 111px; height: 19px;">Age:</td>
                                                                                <td style="padding-left: 5px; height: 19px; text-align: left; width: 173px;" ><asp:TextBox ID="txt_Age" runat="server" CssClass="input_box" MaxLength="10" meta:resourcekey="txt_MemberIdResource1" ReadOnly="True" Width="155px"></asp:TextBox></td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 19px; text-align: right">Place of Birth:</td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 19px; text-align: left" ><asp:TextBox ID="txtplaceofbirth" runat="server" CssClass="required_box" Width="155px" meta:resourcekey="txtplaceofbirthResource1" MaxLength="25" TabIndex="5"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 1px" >
                                                                                </td>
                                                                                <td align="right" colspan="2" style="padding-left: 5px; color: #0e64a0; height: 1px;text-align: left">
                                                                                    
                                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_DOB" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" ControlToValidate="txt_DOB" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                                                                                    
                                                                                    &nbsp;</td>
                                                                                <td style="padding-left: 5px; color: #0e64a0; height: 1px; text-align: left; width: 173px;" ></td>
                                                                                <td style="padding-left: 5px; width: 102px; color: #0e64a0; height: 1px; text-align: left">&nbsp;</td>
                                                                                <td style="padding-left: 5px; width: 132px; color: #0e64a0; height: 1px; text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtplaceofbirth" ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr style="color: #0e64a0">
                                                                                <td align="right" style="width: 107px; height: 1px" ><asp:Label ID="Label16" runat="server" Text="Country of Birth:" Width="105px" meta:resourcekey="Label16Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left" >
                                                                                    <asp:DropDownList ID="ddcountryofbirth" runat="server" CssClass="required_box" Width="164px" TabIndex="6"></asp:DropDownList></td>
                                                                                <td align="right" style="width: 111px; height: 1px"><asp:Label ID="Label45" runat="server" Text="Nationality:" Width="72px" meta:resourcekey="Label45Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: left; width: 173px;" ><asp:DropDownList id="ddl_Nationality" runat="server" Width="160px" CssClass="required_box" meta:resourcekey="ddl_NationalityResource1" TabIndex="7"></asp:DropDownList></td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 1px; text-align: right">Gender:</td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 1px; text-align: left" ><asp:DropDownList id="ddl_Sex" runat="server" Width="160px" CssClass="required_box" meta:resourcekey="ddl_SexResource1" TabIndex="8"><asp:ListItem Text="&lt; Select &gt;"></asp:ListItem></asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 1px" ></td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left" >
                                                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddcountryofbirth"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                                <td align="right" style="width: 111px; height: 1px"></td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: left; width: 173px;" ><asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Nationality" meta:resourcekey="RequiredFieldValidator13Resource1"></asp:RequiredFieldValidator></td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 1px; text-align: left"></td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 1px; text-align: left" ><asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Sex" meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td  align="right" style="width: 107px; height: 1px"><asp:Label ID="Label1" runat="server" Text="Civil Status:" Width="84px" meta:resourcekey="Label1Resource1"></asp:Label></td>
                                                                                <td  style="padding-left: 5px; width: 151px; height: 1px; text-align: left">
                                                                                    <asp:DropDownList ID="ddmaritalstatus" runat="server" CssClass="required_box" 
                                                                                        Width="163px" meta:resourcekey="ddmaritalstatusResource1" TabIndex="9"><asp:ListItem Text="&lt; Select &gt;" Value="0" ></asp:ListItem></asp:DropDownList></td>
                                                                                <td align="right" style="width: 111px; height: 1px"> Acad. Qual.:</td>
                                                                                <td  style="padding-left: 5px; height: 1px; text-align: left; width: 173px;"><asp:DropDownList ID="ddl_Academy_Quali" runat="server" CssClass="input_box" Width="160px" meta:resourcekey="ddcountryofbirthResource1" TabIndex="10"></asp:DropDownList></td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: right; width: 102px;">
                                                                                    Blood Group:</td>
                                                                                <td  style="padding-left: 5px; width: 132px; height: 1px; text-align: left"><asp:DropDownList id="ddl_BloodGroup" runat="server" Width="160px" CssClass="input_box" meta:resourcekey="ddl_NationalityResource1" TabIndex="10">
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 1px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 1px; text-align: left">
                                                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ddmaritalstatus"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                                <td align="right" style="width: 111px; height: 1px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: left; width: 173px;">
                                                                                    </td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: left; width: 102px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 1px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 1px">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Height(cms):" meta:resourcekey="Label6Resource1"></asp:Label>
                                                                                    </td>
                                                                                <td style="padding-left: 5px; height: 1px; width: 151px; text-align: left;">
                                                                                    <asp:TextBox ID="txtheight" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtheightResource1" OnTextChanged="txtweight_TextChanged" AutoPostBack="true" TabIndex="11" CausesValidation="True"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtheight" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <td align="right" style="height: 1px; width: 111px;">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Weight(Kg.):" meta:resourcekey="Label9Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; height: 1px; text-align: left; width: 173px;">
                                                                                    <asp:TextBox ID="txtweight" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtweightResource1" AutoPostBack="True" OnTextChanged="txtweight_TextChanged" TabIndex="12" CausesValidation="True"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtweight" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>

                                                                                <td style="padding-left: 5px; height: 1px; text-align: right; width: 102px;">
                                                                                    BMI:</td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 1px; text-align: left">
                                                                                    <asp:TextBox ReadOnly="true" ID="txt_Bmi" runat="server" CssClass="input_box" MaxLength="10" meta:resourcekey="txtcollarResource1"
                                                                                        Width="155px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; text-align: left; height: 13px;">
                                                                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtheight"
                                                                                        ErrorMessage="Max. 250." MaximumValue="250" MinimumValue="0" Type="Double"></asp:RangeValidator></td>
                                                                                <td align="right" style="width: 111px; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left; height: 13px; width: 173px;">
                                                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtweight"
                                                                                        ErrorMessage="Max. 125." MaximumValue="125" MinimumValue="0" Type="Double"></asp:RangeValidator></td>
                                                                                <td style="padding-left: 5px; text-align: left; width: 102px; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; text-align: left; height: 13px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 10px">
                                                                                    <asp:Label ID="Label11" runat="server" Text="Waist(cms):" meta:resourcekey="Label11Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left">
                                                                                    <asp:TextBox ID="txtwaist" runat="server" CssClass="input_box" Width="155px" MaxLength="9" meta:resourcekey="txtwaistResource1" TabIndex="14"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtwaist" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <td align="right" style="width: 111px; height: 10px">
                                                                                    <asp:Label ID="Label10" runat="server" Style="text-align: right" Text="Shoes:" meta:resourcekey="Label10Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 173px; height: 10px; text-align: left"><asp:DropDownList ID="ddl_Shoes" runat="server" CssClass="input_box" Width="160px" meta:resourcekey="ddmaritalstatusResource1" TabIndex="15">
                                                                                    <asp:ListItem meta:resourceKey="ListItemResource2" Value="0" Text="&lt; Select &gt;"></asp:ListItem>
                                                                                </asp:DropDownList></td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 10px; text-align: right">
                                                                                    Shirt:</td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 10px; text-align: left">
                                                                                    <asp:DropDownList ID="ddl_Shirt" runat="server" CssClass="input_box" Width="160px" meta:resourcekey="ddmaritalstatusResource1" TabIndex="16">
                                                                                    <asp:ListItem meta:resourceKey="ListItemResource2" Value="0" Text="&lt; Select &gt;"></asp:ListItem>
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 10px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left">
                                                                                    </td>
                                                                                <td align="right" style="width: 111px; height: 10px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 173px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 10px; text-align: right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px;">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Date Joined Company:" Width="98px" meta:resourcekey="Label2Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:TextBox ID="txtdatefirstjoin" runat="server" CssClass="required_box" Width="140px" TabIndex="17"></asp:TextBox>&nbsp;<asp:ImageButton
                                                                                        ID="ImageButton4" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td align="right" style="width: 111px;">
                                                                                    <asp:Label ID="Label14" runat="server" Text="Rank Joined Company:" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 173px; text-align: left">
                                                                                    <asp:DropDownList ID="ddrankapp" runat="server" CssClass="required_box" Width="160px" meta:resourcekey="ddrankappResource1" TabIndex="18">
                                                                                    </asp:DropDownList></td>
                                                                                <td style="padding-left: 5px; width: 102px; text-align: right">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Recruit Off:" meta:resourcekey="Label13Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 132px; text-align: left">
                                                                                    <asp:DropDownList ID="ddrecruitingoff" runat="server" CssClass="required_box" Width="160px" meta:resourcekey="ddrecruitingoffResource1" TabIndex="19">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 10px">
                                                                                </td>
                                                                                <td align="right" colspan="2" style="padding-left: 5px; height: 10px; text-align: left">
                                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtdatefirstjoin" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtdatefirstjoin" ErrorMessage="Required."></asp:RequiredFieldValidator>  
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 173px; height: 10px; text-align: left">
                                                                                    <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="ddrankapp"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 10px; text-align: left">
                                                                                    <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="ddrecruitingoff"
                                                                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td align="right" style="width: 107px; height: 10px">
                                                                                    Passport No:
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left"><asp:TextBox ID="txt_Passport" runat="server" CssClass="required_box" MaxLength="49" TabIndex="3" Width="160px"></asp:TextBox>
                                                      
                                                                                    </td>
                                                                                <td align="right" style="width: 111px; height: 10px">
                                                                                    INDOS Certificate: 
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 173px; height: 10px; text-align: left">
                                                                                    <asp:TextBox ID="txt_INDOS" runat="server"  MaxLength="49" TabIndex="3" Width="160px"></asp:TextBox>
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 10px; text-align: right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 10px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 151px; height: 10px; text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Passport"
                                                          ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                <td align="right" style="width: 111px; height: 10px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 173px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 102px; height: 10px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 10px; text-align: right">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 107px; height: 3px">
                                                                                    </td>
                                                                                <td align="right" colspan="2" style="height: 3px">
                                                                                    <ajaxToolkit:CalendarExtender  ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" TargetControlID="txt_DOB" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" TargetControlID="txtdatefirstjoin" PopupPosition="TopRight"></ajaxToolkit:CalendarExtender>
                                                                                    <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txt_DOB" InvalidValueMessage="" Display="None" IsValidEmpty="False"></ajaxToolKit:MaskedEditValidator> --%>
                                                                                </td>
                                                                                <td colspan="3" style="text-align: right; padding-right: 10px;">
                                                            
</td>

                                                                            </tr>
                                                                            
                                                                        </table>
                                                            </td>
                                                    </tr>
                                                </table>
                                                </fieldset>
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="text-align: left; ">
                                                    <%--<fieldset style="border:solid 1px #8fafdb; height:70px;text-align:center; vertical-align:middle; width:850px; float:left; padding-top:5px;"> 
                                                        <legend><b>Latest Assessment</b></legend>
                                                        <table width="100%" border="0" cellpadding="4" cellspacing="2" style="font-size:12px" >
                                                        <tr style=" background-color:#D6EBFF">
                                                        <td style="text-align:left">Vessel</td>
                                                        <td style="width:100px">Owner Rep.</td>
                                                        <td style="width:100px">Charterer</td>
                                                        <td style="width:100px">Fleet Manager</td>
                                                        <td style="width:100px">Tech. Suptd.</td>
                                                        <td style="width:100px">Marine Suptd.</td>
                                                        </tr>
                                                        <tr>
                                                        <td style="text-align:left"><asp:Label runat="server" ID="lblVesselName"></asp:Label></td>
                                                        <td style="text-align:center"><center><div runat="server" id="Div1"></div></center></td>
                                                        <td style="text-align:center"><center><div runat="server" id="Div2"></div></center></td>
                                                        <td style="text-align:center"><center><div runat="server" id="Div4"></div></center></td>
                                                        <td style="text-align:center"><center><div runat="server" id="Div3"></div></center></td>
                                                        <td style="text-align:center"><center><div runat="server" id="Div5"></div></center></td>
                                                        </tr>
                                                        </table>
                                                        <br />
                                                    </fieldset>--%>
                                                        <div style="border:solid 1px #8fafdb;width:50%; float:left; text-align:left; vertical-align:middle; "> 
                                                        <asp:Button ID="btn_AddNew" runat="server" OnClick="btn_AddNew_Click" Text="Add" Width="59px" CssClass="btn" CausesValidation="False" TabIndex="20"/>
                                                        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" Width="59px" CssClass="btn" TabIndex="21" />&nbsp;
                                                      
                                                        <asp:Button ID="cmdsendmail" runat="server" OnClick="cmdsendmail_Click" Text="Send Mail" Width="90px" CssClass="btn" CausesValidation="False" TabIndex="23"/>
                                                        <%--<div style="padding:5px;">--%>
                                                            <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_Click" Text="Verify" Width="70px" CssClass="btn" CausesValidation="False" TabIndex="24" OnClientClick="return confirm('Please check travel documents, family , NOK , contact details are correct and updated before verification.')"/>

                                                            <table cellpadding="5" cellspacing="2" border="0" width="100%" style="text-align:left;">
                                                                <colgroup>
                                                                    <col width="110px;" />
                                                                    <tr>
                                                                        <td style="color: #0e64a0">Verified By/On : </td>
                                                                        <td style="text-align:left;">
                                                                            <asp:Label ID="lblVerifiedBy" runat="server"></asp:Label>
                                                                           <%-- -<asp:Label ID="lblVerifiedOn" runat="server"></asp:Label>--%>
                                                                        </td>
                                                                    </tr>
                                                                </colgroup>
                                                            </table>
                                                        <%--</div>--%>
                                                    </div>
                                                    </td>
                                                </tr>
                                                </table>
                                        </asp:View>
                                        <asp:View ID="Tab2" runat="server">
                                            <table cellpadding="0" cellspacing="0" width="100%"  >
                                                <tr valign="top">
                                                    <td class="TabArea">
                                                   
                                                        <table border="0" style="background-color: #f9f9f9; padding-left: 10px;width:100%;" cellspacing="0">
                                                            <tr style="display:none;">
                                                                <td colspan="2" style="height: 123px">
                                                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-bottom: 5px;">
                                                                    <legend ><strong>[<asp:Label ID="txt_MemberId1" runat="server"></asp:Label>]</strong></legend>
                                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 38px">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right" style="width: 233px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 140px; text-align: left;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 156px; text-align: left;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; text-align: left;">
                                                                                </td>
                                                                                <td align="right" style="width: 252px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 179px; text-align: left;">
                                                                                </td>
                                                                                <td rowspan="7" style="text-align: center; padding-right: 15px;" valign="middle">
                                                                                    <asp:Image ID="img_Crew1" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Width="60px" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="WIDTH: 233px; height: 19px; text-align:right" >
                                                                                    <asp:Label ID="Label49" runat="server" Text="First Name:" Width="72px" meta:resourcekey="Label49Resource1"></asp:Label></td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 140px; TEXT-ALIGN: left; height: 19px;">
                                                                                    <asp:TextBox ID="txt_FirstName1" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_FirstName1Resource1"></asp:TextBox></td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 156px; TEXT-ALIGN: right; height: 19px;">
                                                                                    <asp:Label ID="Label51" runat="server" Text="Middle Name:" Width="82px" meta:resourcekey="Label51Resource1"></asp:Label></td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 132px; TEXT-ALIGN: left; height: 19px;">
                                                                                    <asp:TextBox ID="txt_MiddleName1" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_MiddleName1Resource1"></asp:TextBox></td>
                                                                                <td style="WIDTH: 252px; height: 19px; text-align: right;" 
>
                                                                                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                                                        Width="100%"></asp:Label></td>
                                                                                <td 
style="PADDING-LEFT: 5px; TEXT-ALIGN: left; width: 179px; height: 19px; padding-right: 5px;">
                                                                                    <asp:TextBox ID="txt_LastName1" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_LastName1Resource1"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="WIDTH: 233px; HEIGHT: 4px" 
>
                                                                                </td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 140px; HEIGHT: 4px; TEXT-ALIGN: left"></td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 156px; HEIGHT: 4px; TEXT-ALIGN: right">
                                                                                </td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 132px; HEIGHT: 4px; TEXT-ALIGN: left">
                                                                                </td>
                                                                                <td style="WIDTH: 252px; HEIGHT: 4px" 
>
                                                                                </td>
                                                                                <td 
style="PADDING-LEFT: 5px; WIDTH: 179px; HEIGHT: 4px; TEXT-ALIGN: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 233px; height: 16px">
                                                                                    <asp:Label ID="Label48" runat="server" Text="Current Rank:" meta:resourcekey="Label48Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 140px; height: 16px; text-align: left">
                                                                                    <asp:TextBox ReadOnly="True"  CssClass="input_box" ID="ddcurrentrank1" runat="server" meta:resourcekey="ddcurrentrank1Resource1" Width="160px"></asp:TextBox></td>
                                                                                <td style="padding-left: 5px; height: 16px; text-align: right; width: 156px;">
                                                                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label44Resource1" Text="Last Vessel:"
                                                                                        Width="72px"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 16px; text-align: left">
                                                                                    <asp:TextBox ID="txt_LastVessel1" runat="server" CssClass="input_box" MaxLength="24" ReadOnly="True"
                                                                                        Width="160px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 252px; height: 16px">
                                                                                    Passport No:</td>
                                                                                <td style="padding-left: 5px; width: 179px; height: 16px; text-align: left; padding-right: 5px;">
                                                                                    <asp:TextBox ID="txt_passport1" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        meta:resourcekey="txt_LastName1Resource1" ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 233px; height: 16px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 140px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 156px; height: 16px; text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="width: 252px; height: 16px">
                                                                                </td>
                                                                                <td style="padding-right: 5px; padding-left: 5px; width: 179px; height: 16px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 233px; height: 16px">
                                                                                    Status:</td>
                                                                                <td colspan="4" style="padding-left: 5px; width: 140px; height: 16px; text-align: left">
                                                                                    <asp:TextBox ID="txt_Status1" runat="server" CssClass="input_box" MaxLength="10"
                                                                                        meta:resourcekey="txt_MemberIdResource1" ReadOnly="True" Width="493px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 233px; height: 16px">
                                                                                    </td>
                                                                                <td style="padding-left: 5px; width: 140px; height: 16px; text-align: left">
                                                                                    
                                                                                    
                                                                                    </td>
                                                                                <td style="padding-left: 5px; width: 156px; height: 16px; text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 132px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="width: 252px; height: 16px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 179px; height: 16px; text-align: left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        
                                                                        </tbody>
                                                                    </table>
                                                                    </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 50%;">&nbsp;
                                                                    </td>
                                                                <td style="width: 50%; text-align: left;">
                                                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                                        Text="Same as Permanent Address" meta:resourcekey="CheckBox1Resource1" TabIndex="49" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top" style="width: 50%;">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Permanent Address</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-left: 10px;">
                                                                            <tr>
                                                                                <td style="WIDTH: 151px"></td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td style="width: 260px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">Address:</td> 
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_Address" runat="server" CssClass="required_box" Width="215px" MaxLength="49" meta:resourcekey="txt_P_AddressResource1" TabIndex="31"></asp:TextBox> &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_P_Address"
                                                                                        ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 1px;">&nbsp;
                                                                                    </td>
                                                                                <td style="width: 20px; height: 1px">
                                                                                </td>
                                                                                <td style="height: 1px; text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_Address1" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_P_Address1Resource1" TabIndex="32"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 2px;">&nbsp;
                                                                                    </td>
                                                                                <td style="width: 20px; height: 2px">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px; height: 2px;">
                                                                                    <asp:TextBox ID="txt_P_Address2" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_P_Address2Resource1" TabIndex="33"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 19px; text-align: right;">
                                                                                    Country:</td>
                                                                                <td style="width: 20px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="height: 19px; text-align: left; width: 260px;">
                                                                                    <asp:DropDownList ID="ddl_P_Country" OnSelectedIndexChanged="ddl_P_Country_SelectedIndexChanged"  runat="server" AutoPostBack="True" CssClass="required_box"
                                                                                        Width="210px" TabIndex="33">
                                                                                    </asp:DropDownList> &nbsp; <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_P_Country"
                                                                                        ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    State/Province:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_State" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_P_StateResource1" TabIndex="35"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    City:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_City" runat="server" CssClass="required_box" Width="215px" MaxLength="29" meta:resourcekey="txt_P_CityResource1" TabIndex="36"></asp:TextBox> &nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_P_City"
                                                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 9px; text-align: right;">
                                                                                    Zip Code:</td>
                                                                                <td style="width: 20px; height: 9px; text-align: right">
                                                                                </td>
                                                                                <td style="height: 9px; text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_Pin" runat="server" CssClass="input_box" Width="215px" MaxLength="9" meta:resourcekey="txt_P_PinResource1" TabIndex="37"></asp:TextBox>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right; height: 19px;">
                                                                                    International Airport:</td>
                                                                                <td style="width: 20px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px; height: 19px;">
                                                                                    <asp:DropDownList ID="ddl_P_Airport" runat="server" CssClass="input_box" Width="210px" meta:resourcekey="ddl_P_AirportResource1" TabIndex="38">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 19px; text-align: right">
                                                                                    Local Airport:</td>
                                                                                <td style="width: 20px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 260px; height: 19px; text-align: left">
                                                                                    <asp:TextBox ID="ddl_LocalAirportPermanent" runat="server" CssClass="input_box" Width="215px" meta:resourcekey="ddl_P_AirportResource1" TabIndex="38"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                </td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 260px; text-align: left;" class="text-1">
                                                                                    CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;Area Code &nbsp; &nbsp; &nbsp; Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    Telephone:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                                <asp:DropDownList ID="ddl_P_CountryCode_Tel" runat="server" CssClass="input_box" Width="76px" meta:resourcekey="ddl_P_CountryCode_TelResource1" TabIndex="39">
                                                                                                </asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_P_Area_Code_Tel" runat="server" CssClass="input_box" Width="57px" MaxLength="5" meta:resourcekey="txt_P_Area_Code_TelResource1" TabIndex="40"></asp:TextBox>&nbsp;
                                                                                    <asp:TextBox ID="txt_P_Number_Tel" runat="server" CssClass="input_box" Width="80px" MaxLength="14" meta:resourcekey="txt_P_Number_TelResource1" TabIndex="41"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt_P_Number_Tel" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender> 

                                                                                        &nbsp;
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_P_Number_Tel" Enabled="false" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    </td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;" class="text-1">
                                                                                    CountryCode &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; height: 7px; text-align: right">
                                                                                    Mobile:</td>
                                                                                <td style="width: 20px; height: 7px; text-align: right">
                                                                                </td>
                                                                                <td class="text-1" style="width: 260px; height: 7px; text-align: left">
                                                                                                <asp:DropDownList ID="ddl_P_CountryCode_Mobile" runat="server" CssClass="input_box"
                                                                                                    Width="76px" meta:resourcekey="ddl_P_CountryCode_MobileResource1" TabIndex="42">
                                                                                                </asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_P_Number_Mobile" runat="server" CssClass="input_box" MaxLength="14" Width="144px" meta:resourcekey="txt_P_Number_MobileResource1" TabIndex="43"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txt_P_Number_Mobile" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right; height: 7px;">
                                                                                </td>
                                                                                <td style="width: 20px; height: 7px; text-align: right">
                                                                                </td>
                                                                                <td class="text-1" style="width: 260px; text-align: left; height: 7px;">
                                                                                    CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;Area Code &nbsp; &nbsp; &nbsp; Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right">
                                                                                    Fax:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 260px; text-align: left">
                                                                                                <asp:DropDownList ID="ddl_P_CountryCode_Fax" runat="server" CssClass="input_box"
                                                                                                    Width="76px" meta:resourcekey="ddl_P_CountryCode_FaxResource1" TabIndex="44">
                                                                                                </asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_P_Area_Code_Fax" runat="server" CssClass="input_box" Width="57px" MaxLength="5" meta:resourcekey="txt_P_Area_Code_FaxResource1" TabIndex="45"></asp:TextBox>&nbsp;
                                                                                    <asp:TextBox ID="txt_P_Number_Fax" runat="server" CssClass="input_box" Width="80px" MaxLength="14" meta:resourcekey="txt_P_Number_FaxResource1" TabIndex="46"></asp:TextBox></td>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txt_P_Number_Fax" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    Email 1:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_EMail1" runat="server" CssClass="input_box" Width="215px" MaxLength="99" meta:resourcekey="txt_P_EMail1Resource1" TabIndex="47"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right">&nbsp;
                                                                                    </td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 260px">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_P_EMail1"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator3Resource1" Display="Dynamic" Visible="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_P_EMail1"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right;">
                                                                                    Email 2:</td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:TextBox ID="txt_P_EMail2" runat="server" CssClass="input_box" Width="215px" MaxLength="99" meta:resourcekey="txt_P_EMail2Resource1" TabIndex="48"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 151px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 20px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; width: 260px;">
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_P_EMail2"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset><asp:HiddenField ID="HiddenField2" runat="server" />
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txt_C_Area_Code_Tel" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txt_C_Number_Tel" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txt_P_Area_Code_Tel" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txt_C_Number_Mobile" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="width: 50%;" valign="top">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Correspondence Address </strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-left: 10px;">
                                                                            <tr>
                                                                                <td style="WIDTH: 151px">&nbsp;</td>
                                                                                <td style="width: 20px">
                                                                                </td>
                                                                                <td style="width: 260px"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 1px; text-align: right; width: 152px;">Address:</td>
                                                                                <td style="width: 21px; height: 1px; text-align: right">
                                                                                </td>
                                                                                <td style="height: 1px; text-align: left"><asp:TextBox ID="txt_C_Address" runat="server" CssClass="required_box" Width="215px" MaxLength="49" meta:resourcekey="txt_C_AddressResource1" TabIndex="49"></asp:TextBox>&nbsp;
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_C_Address" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 1px; width: 152px;">&nbsp;</td>
                                                                                <td style="width: 21px; height: 1px">
                                                                                </td>
                                                                                <td style="height: 1px; text-align: left"><asp:TextBox ID="txt_C_Address1" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_C_Address1Resource1" TabIndex="50"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 1px; width: 152px;">&nbsp;</td>
                                                                                <td style="width: 21px; height: 1px">
                                                                                </td>
                                                                                <td style="height: 1px; text-align: left"><asp:TextBox ID="txt_C_Address2" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_C_Address2Resource1" TabIndex="51"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 13px; text-align: right; width: 152px;">Country:</td>
                                                                                <td style="width: 21px; height: 13px; text-align: right">
                                                                                </td>
                                                                                <td style="height: 13px; text-align: left"><asp:DropDownList ID="ddl_C_Country" runat="server" CssClass="required_box" Width="210px" meta:resourcekey="ddl_C_CountryResource1" AutoPostBack="True" OnSelectedIndexChanged="ddl_C_Country_SelectedIndexChanged" TabIndex="52"></asp:DropDownList> &nbsp; <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_C_Country" ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; height: 1px; width: 152px;">
                                                                                    State/Province:</td>
                                                                                <td style="width: 21px; height: 1px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; height: 1px;"><asp:TextBox ID="txt_C_State" runat="server" CssClass="input_box" Width="215px" MaxLength="29" meta:resourcekey="txt_C_StateResource1" TabIndex="53"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; height: 19px; width: 152px;">City:</td>
                                                                                <td style="width: 21px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; height: 19px;"><asp:TextBox ID="txt_C_City" runat="server" CssClass="required_box" Width="215px" MaxLength="29" meta:resourcekey="txt_C_CityResource1" TabIndex="54"></asp:TextBox> &nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_C_City" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">
                                                                                    Zip Code:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left"><asp:TextBox ID="txt_C_Pin" runat="server" CssClass="input_box" Width="215px" MaxLength="9" meta:resourcekey="txt_C_PinResource1" TabIndex="55"></asp:TextBox></td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px; height: 19px;">
                                                                                    International Airport:</td>
                                                                                <td style="width: 21px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left; height: 19px;"><asp:DropDownList ID="ddl_C_Airport" runat="server" CssClass="input_box" Width="210px" meta:resourcekey="ddl_C_AirportResource1" TabIndex="56"></asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="width: 152px; height: 19px; text-align: right">
                                                                                    Local Airport:</td>
                                                                                <td style="width: 21px; height: 19px; text-align: right">
                                                                                </td>
                                                                                <td style="height: 19px; text-align: left">
                                                                                    <asp:TextBox ID="ddl_LocalAirportCorrespondance" runat="server" CssClass="input_box" Width="215px" meta:resourcekey="ddl_P_AirportResource1" TabIndex="38"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 152px; text-align: right"></td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td class="text-1">CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Area Code &nbsp; &nbsp; &nbsp;&nbsp;Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">Telephone:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td><asp:DropDownList ID="ddl_C_CountryCode_Tel" runat="server" CssClass="input_box" Width="82px" meta:resourcekey="ddl_C_CountryCode_TelResource1" TabIndex="57"></asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_C_Area_Code_Tel" runat="server" CssClass="input_box" Width="57px" MaxLength="5" meta:resourcekey="txt_C_Area_Code_TelResource1" TabIndex="58"></asp:TextBox>&nbsp;
                                                                                    <asp:TextBox ID="txt_C_Number_Tel" runat="server" CssClass="input_box" Width="78px" MaxLength="14" meta:resourcekey="txt_C_Number_TelResource1" TabIndex="59"></asp:TextBox>&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_C_Number_Tel" Enabled="false" ErrorMessage="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="width: 152px; height: 13px; text-align: right"></td>
                                                                                <td style="width: 21px; height: 13px; text-align: right">
                                                                                </td>
                                                                                <td class="text-1" style="height: 13px">CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">Mobile:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td><asp:DropDownList ID="ddl_C_CountryCode_Mobile" runat="server" CssClass="input_box" Width="82px" meta:resourcekey="ddl_C_CountryCode_MobileResource1" TabIndex="60"></asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_C_Number_Mobile" runat="server" CssClass="input_box" Width="144px" MaxLength="14" meta:resourcekey="txt_C_Number_MobileResource1" TabIndex="61"></asp:TextBox>&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 152px; text-align: right"></td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td class="text-1">CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Area Code &nbsp; &nbsp; &nbsp;&nbsp;Number</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">Fax:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td><asp:DropDownList ID="ddl_C_CountryCode_Fax" runat="server" CssClass="input_box" Width="82px" meta:resourcekey="ddl_C_CountryCode_FaxResource1" TabIndex="62"></asp:DropDownList>
                                                                                    <asp:TextBox ID="txt_C_Area_Code_Fax" runat="server" CssClass="input_box" Width="57px" MaxLength="5" meta:resourcekey="txt_C_Area_Code_FaxResource1" TabIndex="63"></asp:TextBox>&nbsp;
                                                                                    <asp:TextBox ID="txt_C_Number_Fax" runat="server" CssClass="input_box" Width="78px" MaxLength="14" meta:resourcekey="txt_C_Number_FaxResource1" TabIndex="64"></asp:TextBox>&nbsp;
                                                                                </td>                                                                                                
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">Email 1:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left"><asp:TextBox ID="txt_C_EMail1" runat="server" CssClass="input_box" Width="215px" MaxLength="99" meta:resourcekey="txt_C_EMail1Resource1" TabIndex="65"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">&nbsp;</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_C_EMail1" ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator3Resource1" Display="Dynamic" Visible="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_C_EMail1"
                                                                                        ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;">Email 2:</td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left"><asp:TextBox ID="txt_C_EMail2" runat="server" CssClass="input_box" Width="215px" MaxLength="99" meta:resourcekey="txt_C_EMail2Resource1" TabIndex="66"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 152px;"></td>
                                                                                <td style="width: 21px; text-align: right">
                                                                                </td>
                                                                                <td style="text-align: left"><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                                                                                        ControlToValidate="txt_C_EMail2" ErrorMessage="Invalid Email Id." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                            </tr>
                                                                            
                                                                        </table>
                                                                    </fieldset>
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_C_Pin" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_P_Pin" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txt_C_Area_Code_Fax" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_C_Number_Fax" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txt_P_Area_Code_Fax" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 462px"></td>
                                                                <td style="width: 339px; text-align: right">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 462px">
                                                                </td>
                                                                <td style="text-align: right;">
                                                                                    <asp:Button ID="btn_Save1" runat="server" CssClass="btn" OnClick="btn_Save_Click1" Text="Save" meta:resourcekey="btn_Save1Resource1" TabIndex="67" Width="56px" />
                                                                    <asp:Button
                                                                ID="btn_Print1" runat="server" CausesValidation="False" CssClass="btn"
                                                                TabIndex="68" Text="Print" Width="56px"  OnClientClick="return printcv();" Visible="false"  /></td>
                                                            </tr>
                                                        </table>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="Tab3" runat="server">
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr style="display:none;">
                                                    <td >
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-bottom: 5px;">
                                                            <legend><strong>[<asp:Label ID="txt_MemberId_family" runat="server"></asp:Label>]</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 55px"
                                                                width="71%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; font-weight: bold; width: 178px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 144px; height: 13px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td align="right" style="width: 187px; height: 13px">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 189px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td colspan="1" rowspan="7" style="padding-right: 15px; text-align: center" valign="middle">
                                                                            <asp:Image ID="img_Crew2" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" ImageUrl="" Height="90px" Width="60px" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <!-- ****************************************************  -->
                                                                        <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                                            <asp:Label ID="vzsdfs" runat="server" meta:resourcekey="Label43Resource1" Text="First Name:"
                                                                                Width="72px"></asp:Label></td>
                                                                        <td style="padding-left: 5px;width: 178px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_FirstName_Family" runat="server" CssClass="input_box" MaxLength="24"
                                                                                meta:resourcekey="txt_firstname_family" ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                        <td style="padding-left: 5px; width: 144px; height: 13px; text-align: right">
                                                                            <asp:Label ID="Label18" runat="server" meta:resourcekey="Label37Resource1" Text="Middle Name:"
                                                                                Width="82px"></asp:Label></td>
                                                                        <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_MiddleName_Family" runat="server" CssClass="input_box" MaxLength="24"
                                                                                ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                        <td align="right" style="width: 187px; height: 13px">
                                                                            <asp:Label ID="Label19" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                                                Width="100%"></asp:Label></td>
                                                                        <td style="padding-left: 5px; width: 189px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_LastNameFamily" runat="server" CssClass="input_box" MaxLength="24"
                                                                                ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 151px; height: 3px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 178px; height: 3px; text-align: left">
                                                                            &nbsp;</td>
                                                                        <td style="padding-left: 5px; width: 144px; color: #0e64a0; height: 3px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 115px; color: #0e64a0; height: 3px; text-align: left">
                                                                        </td>
                                                                        <td align="right" style="width: 187px; color: #0e64a0; height: 3px">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 189px; color: #0e64a0; height: 3px; text-align: left">
                                                                            </td>
                                                                    </tr>
                                                                    <tr style="color: #0e64a0">
                                                                        <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                                            <asp:Label ID="Label20" runat="server" meta:resourcekey="Label15Resource1" Text="Current Rank:"></asp:Label></td>
                                                                        <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_CurrentRankFamily" runat="server" CssClass="input_box" meta:resourcekey="ddcurrentrankResource1"
                                                                                ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                        <td style="padding-left: 5px; height: 13px; text-align: right; width: 144px;">
                                                                            <asp:Label ID="Label21" runat="server" meta:resourcekey="Label44Resource1" Text="Last Vessel:"
                                                                                Width="72px"></asp:Label></td>
                                                                        <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_LastVesselFamily" runat="server" CssClass="input_box" MaxLength="24"
                                                                                ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                        <td align="right" style="width: 187px; height: 13px">
                                                                            Passport No:</td>
                                                                        <td style="padding-left: 5px; width: 189px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_PassportFamily" runat="server" CssClass="input_box" MaxLength="24"
                                                                                ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr style="color: #0e64a0">
                                                                        <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                                            &nbsp;</td>
                                                                        <td style="padding-left: 5px; width: 144px; height: 13px; text-align: right">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td align="right" style="width: 187px; height: 13px">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 189px; height: 13px; text-align: left">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="color: #0e64a0">
                                                                        <td align="right" style="width: 151px; height: 13px; text-align: right">
                                                                            Status:</td>
                                                                        <td colspan="4" style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                                            <asp:TextBox ID="txt_Status_Family" runat="server" CssClass="input_box" MaxLength="10"
                                                                                meta:resourcekey="txt_MemberIdResource1" ReadOnly="True" Width="493px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 151px; height: 13px">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 178px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 144px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 115px; height: 13px; text-align: left">
                                                                        </td>
                                                                        <td align="right" style="width: 187px; height: 13px">
                                                                        </td>
                                                                        <td style="padding-left: 5px; width: 189px; height: 13px; text-align: left">
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 143px;">
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>Family/NOK</strong></legend>
                                                            <%--<div style="overflow: auto; width: 100%; padding-top: 5px; height: 98px; text-align: center">
                                                                <asp:Label ID="lbl_Gvfamily" runat="server" Text=""></asp:Label>
                                                                <asp:GridView ID="Gvfamily" runat="server" AutoGenerateColumns="False" OnRowDataBound="Gvfamily_DataBound"
                                                                  OnPreRender="Gvfamily_PreRender" OnRowDeleting="family_Row_Deleting" OnRowEditing="family_Row_Editing"                                                                    
                        OnSelectedIndexChanged="Gvfamily_SelectIndexChanged" Style="text-align: center" Width="99%" GridLines="horizontal" >
                                                                    
                                                                    <HeaderStyle CssClass="headerstyle" />
                                                                    <PagerStyle CssClass="pagerstyle" />
                                                                    <RowStyle CssClass="rowstyle" />
                                                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                            ShowSelectButton="true">
                                                                            <ItemStyle Width="50px" />
                                                                        </asp:CommandField>
                                                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                            ShowEditButton="true">
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:CommandField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                            <ItemStyle Width="30px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                    ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                    Text="Delete" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                                                                        <asp:HiddenField ID="Hiddenfd11" runat ="server" 
                                                                            Value='<%#Eval("ImagePath") %>' />
                                                                    </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="FamilyEmployeeNumber" HeaderText="Emp #" SortExpression="FamilyEmployeeNumber">
                                                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="First Name" ItemStyle-HorizontalAlign="Left" 
                                                                            SortExpression="FirstName">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblfirstname" runat="server" Text='<%#Eval("FirstName")%>'></asp:Label>
                                                                                <asp:HiddenField ID="HiddenId3" runat="server" 
                                                                                    Value='<%#Eval("CrewFamilyId")%>' />
                                                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                        
                                                                    <asp:BoundField DataField="LastName" HeaderText="Family Name" 
                                                                            SortExpression="LastName">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Relation" HeaderText="Relation" 
                                                                            SortExpression="Relation">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Nationality" HeaderText="Nationality" 
                                                                            SortExpression="Nationality">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Sex" HeaderText="Gender" SortExpression="Sex">
                                                                            <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="IsNOK" HeaderText="NOK" SortExpression="IsNOK">
                                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>

                                                                
                                                            </div>--%>
                                                            <asp:Label ID="lbl_Gvfamily" runat="server" Text=""></asp:Label>
                                                            <div style="overflow-y:scroll;overflow-x:hidden;height:25px; border:solid 0px gray;" >
                                                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; height:25px;'>
                                                                <colgroup>
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='80px' />
                                                                    <col width='150px' />
                                                                    <col width='120px' />
                                                                    <col width='120px' />
                                                                    <col width='100px' />
                                                                    <col width='80px' />   
                                                                    <col width='80px' />
                                                                    <col width='20px' />
                                                                </colgroup>
                                                                <tr class="headerstylegrid" >
                                                                    <td align="center">View</td>
                                                                    <td align="center">Edit</td>
                                                                    <td align="center">Delete</td>
                                                                    <td></td>
                                                                    <td align="center">Emp #</td>
                                                                    <td>Name</td>
                                                                    <td>Relation</td>
                                                                    <td>Nationality</td>
                                                                    <td>Gender</td>
                                                                    <td>NOK</td>  
                                                                    <td>Ins. Covered</td> 
                                                                    <td></td>
                                                                </tr>
                                                                </table>
                                                                </div>
                                                                <div style="overflow-y:scroll;overflow-x:hidden;height:90px; border:solid 0px gray;" >
                                                                <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse:collapse; font-size:12px;  height:30px;'>
                                                                <colgroup>
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='50px' />
                                                                    <col width='80px' />
                                                                    <col width='150px' />
                                                                    <col width='120px' />
                                                                    <col width='120px' />
                                                                    <col width='100px' />
                                                                    <col width='80px' />    
                                                                    <col width='80px' />  
                                                                    <col width='20px' />
                                                                </colgroup>
                                                                <asp:Repeater runat="server" ID="rptfamily">
                                                                <ItemTemplate>
                                                                <tr style="font-family: Arial, sans-serif; font-size:11px;" class='<%# Common.CastAsInt32(Eval("CrewFamilyId")) == CrewFamilyId ? "selectedtowstyle" : "rowstyle" %>'>
                                                                    <td style="text-align:center;"><asp:ImageButton ID="btnView" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" runat="server" ToolTip='View Details' OnClick="btnView_Click" CommandArgument='<%#Eval("CrewFamilyId")%>' /> </td>
                                                                    <td style="text-align:center;"><asp:ImageButton ID="btnEdit" Visible='<%# ((Auth.isEdit) && ((Mode == "New") || (Mode == "Edit"))) %>'  ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit"  OnClick="btnEdit_Click" CommandArgument='<%#Eval("CrewFamilyId")%>'  /> </td>
                                                                    <td style="text-align:center;"><asp:ImageButton ID="btnDelete" Visible='<%# ((Auth.isDelete) && ((Mode == "New") || (Mode == "Edit"))) %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" runat="server" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" OnClick="btnDelete_Click" CommandArgument='<%#Eval("CrewFamilyId")%>' /> </td>
                                                                    <td style="text-align:center"><asp:ImageButton ID="imgattach" Visible='<%#Eval("ImagePath").ToString() != "" %>' runat="server" CommandArgument='<%#Eval("ImagePath") %>' OnClick="imgattach_ShowPreview" ImageUrl="~/Modules/HRD/Images/paperclip.gif" ToolTip="Click to Preview" style="cursor:hand;" />
                                                                                                  
                                                                    </td>
                                                                    <td style="text-align:center"><%#Eval("FamilyEmployeeNumber")%></td>
                                                                    <td style="text-align:left"><%#Eval("FirstName")%>   <%#Eval("LastName")%></td>
                                                                    <td style="text-align:left"><%#Eval("Relation")%></td>    
                                                                    <td style="text-align:left"><%#Eval("Nationality")%></td>
                                                                    <td style="text-align:left"><%#Eval("Sex")%></td>
                                                                    <td style="text-align:left"><%#Eval("IsNOK")%></td>
                                                                    <td style="text-align:left"><%#Eval("IsInsurance")%></td>
                                                                    <td></td>
                                                                </tr>
                                                                </ItemTemplate>
                                                                </asp:Repeater>
                                                                </table>
                                                                </div>


                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Panel ID="familypanel" runat="server" Width="100%">
                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                <tr>
                                                                    <td valign="top">
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>Personal Details</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:HiddenField ID="HiddenFamilyPK" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td rowspan="8" style="text-align: center">
                                                                        <asp:Image ID="img_FamilyMember" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="108px" Width="100px" />
                                                                        <br />
                                                                        <br />
                                                                        <div style="border:0px solid; overflow:hidden; width:75px">
                                                                        <asp:FileUpload size="1" ID="FileUpload2" style="left:-9px; position:relative; border:0px solid; background-color:#f9f9f9" runat="server" />
                                                                        </div>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                    &nbsp;
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="height: 1px">
                                                                        First Name:</td>
                                                                    <td align="left" style="height: 1px">
                                                                        <asp:TextBox ID="txtfirstname" runat="server" CssClass="required_box" MaxLength="24"
                                                                            TabIndex="1" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="height: 1px">
                                                                        Middle Name:</td>
                                                                    <td align="left" style="height: 1px">
                                                                        <asp:TextBox ID="txtmiddlename" runat="server" CssClass="input_box" MaxLength="24"
                                                                            TabIndex="2" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="height: 1px">
                                                                        Family Name:</td>
                                                                    <td align="left" style="height: 1px">
                                                                        <asp:TextBox ID="txtlastname" runat="server" CssClass="input_box" MaxLength="24"
                                                                            TabIndex="3" Width="160px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 13px">
                                                                    </td>
                                                                    <td align="left" style="height: 13px">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtfirstname"
                                                                            ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator></td>
                                                                    <td style="height: 13px">
                                                                    </td>
                                                                    <td style="height: 13px">
                                                                    </td>
                                                                    <td style="height: 13px">
                                                                    </td>
                                                                    <td align="left" style="height: 13px">
                                                                        </td>
                                                                </tr>
                                                                <tr style="color: #0e64a0">
                                                                    <td align="right" style="height: 19px">
                                                                        DOB:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:TextBox ID="txt_DOB_Family" runat="server" CssClass="input_box" TabIndex="4"
                                                                            Width="144px"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                    <td align="right" style="height: 19px">
                                                                        POB:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:TextBox ID="txtpob" runat="server" CssClass="input_box" MaxLength="49" TabIndex="6"
                                                                            Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="height: 19px">
                                                                        Gender:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:DropDownList ID="ddsex" runat="server" CssClass="input_box" TabIndex="7" Width="165px">
                                                                            <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txt_DOB_Family" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                        <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_DOB_Family" ErrorMessage="Required." Enabled="false"  ></asp:RequiredFieldValidator>  
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="height: 19px">
                                                                        Relation:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:DropDownList ID="ddrelation" runat="server" CssClass="required_box" TabIndex="8"
                                                                            Width="165px">
                                                                            <asp:ListItem Value=" ">&lt; Select &gt;</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" style="height: 19px">
                                                                        Nationality:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:DropDownList ID="ddnationality" runat="server" CssClass="input_box" TabIndex="9"
                                                                            Width="165px">
                                                                        </asp:DropDownList></td>
                                                                    <td style="height: 19px; text-align: right;">
                                                                        NOK:</td>
                                                                    <td style="height: 19px">
                                                                        <asp:CheckBox ID="chbox_isnok" runat="server" TabIndex="10" Width="128px" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddrelation" ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator></td>
                                                                    <td align="right" colspan="2">
                                                                        &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton11" TargetControlID="txt_DOB_Family" PopupPosition="TopRight">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td><asp:HiddenField ID="hfd_FamilyImage" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td align="right" style="height: 19px">
                                                                        Insurance Covered :</td>
                                                                    <td align="left" style="height: 19px">
                                                                         <asp:CheckBox ID="chkInsuCovered" runat="server" TabIndex="11" Width="128px" AutoPostBack="True" OnCheckedChanged="chkInsuCovered_CheckedChanged" /></td>
                                                                    <td align="right" style="height: 19px">
                                                                        Insurance ID:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:TextBox ID="txtInsuranceId" runat="server" MaxLength="30" Width="160px" Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td style="height: 19px; text-align: right;">
                                                                        Insurance Company:</td>
                                                                    <td style="height: 19px">
                                                                        <asp:TextBox ID="txtInsuCompany" runat="server" MaxLength="100" Width="160px" Enabled="false"></asp:TextBox>
                                                                       </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;</fieldset>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>Address</strong></legend>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 405px; height: 13px">
                                                                    </td>
                                                                    <td style="height: 13px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 50%;" valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px">
                                                                            Address 1:</td>
                                                                        <td align="left" style="width: 129px">
                                                                            <asp:TextBox ID="txtaddress1" runat="server" CssClass="input_box" MaxLength="49"
                                                                                TabIndex="11" Width="262px"></asp:TextBox></td>
                                                                        <td align="left" style="width: 418px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px; height: 9px;">
                                                                        </td>
                                                                        <td align="left" style="height: 9px; width: 129px;">
                                                                            &nbsp;</td>
                                                                        <td align="left" style="width: 418px; height: 9px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px">
                                                                            Address2:</td>
                                                                        <td align="left" style="width: 129px">
                                                                            <asp:TextBox ID="txtaddress2" runat="server" CssClass="input_box" MaxLength="29"
                                                                                TabIndex="12" Width="262px"></asp:TextBox></td>
                                                                        <td align="left" style="width: 418px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px">
                                                                        </td>
                                                                        <td align="left" style="width: 129px">
                                                                            &nbsp;</td>
                                                                        <td align="left" style="width: 418px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px">
                                                                            Address3:</td>
                                                                        <td align="left" style="width: 129px">
                                                                            <asp:TextBox ID="txtaddress3" runat="server" CssClass="input_box" MaxLength="29"
                                                                                TabIndex="13" Width="262px"></asp:TextBox></td>
                                                                        <td align="left" style="width: 418px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 626px; height: 13px">
                                                                        </td>
                                                                        <td align="left" style="height: 13px; width: 129px;">
                                                                            &nbsp;</td>
                                                                        <td style="width: 418px; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px; height: 19px;">
                                                                            Country:</td>
                                                                        <td align="left" style="height: 19px; width: 129px;">
                                                                            <asp:DropDownList ID="ddcountryname" runat="server" AutoPostBack="True" CssClass="input_box"
                                                                                OnSelectedIndexChanged="ddcountryname_SelectedIndexChanged" TabIndex="14" Width="267px">
                                                                                <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                        <td align="left" style="width: 418px; height: 19px;">
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 626px; height: 13px">
                                                                        </td>
                                                                        <td align="left" style="height: 13px; width: 129px;">
                                                                            &nbsp;</td>
                                                                        <td align="left" style="width: 418px; color: #0e64a0; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="color: #0e64a0">
                                                                        <td style="width: 626px; height: 13px; text-align: right">
                                                                            City:</td>
                                                                        <td align="left" style="height: 13px; width: 129px;">
                                                                            <asp:TextBox ID="txtcity" runat="server" CssClass="input_box" MaxLength="29" TabIndex="15"
                                                                                Width="262px"></asp:TextBox></td>
                                                                        <td align="left" style="width: 418px; color: #0e64a0; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 626px; height: 13px; text-align: right">
                                                                        </td>
                                                                        <td align="left" style="height: 13px; width: 129px;">
                                                                            &nbsp;</td>
                                                                        <td align="left" style="width: 418px; color: #0e64a0; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 626px; height: 13px; text-align: right">
                                                                            State &amp; PIN:</td>
                                                                        <td align="left" style="height: 13px; width: 129px;">
                                                                            <asp:TextBox ID="txtstate" runat="server" CssClass="input_box" MaxLength="29" TabIndex="16"
                                                                                Width="160px"></asp:TextBox>
                                                                            -
                                                                            <asp:TextBox ID="txtpin" runat="server" CssClass="input_box" MaxLength="9" TabIndex="17" Width="82px"></asp:TextBox></td>
                                                                        <td align="left" style="width: 418px; color: #0e64a0; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 626px; height: 3px">
                                                                        </td>
                                                                        <td align="left" style="height: 3px; width: 129px;">
                                                                            &nbsp;</td>
                                                                        <td align="left" style="width: 418px; color: #0e64a0; height: 3px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 626px">
                                                                            </td>
                                                                        <td align="left" style="text-align: left; width: 129px;">
                                                                            </td>
                                                                        <td align="left" style="width: 418px; height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td style="width: 95px; text-align: right">
                                                                            Nearest Airport:</td>
                                                                                <td class="text-1" style="width: 315px">
                                                                            <asp:DropDownList ID="dd_nearest_airport" runat="server" CssClass="input_box" TabIndex="18"
                                                                                Width="236px">
                                                                                <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                                <td style="height: 3px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 95px">
                                                                                </td>
                                                                                <td class="text-1" style="width: 315px">
                                                                                    CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Area Code &nbsp; &nbsp; &nbsp; Number</td>
                                                                                <td style="height: 3px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 12px; width: 95px; text-align: right;">
                                                                                    &nbsp;Telephone:</td>
                                                                                <td style="height: 12px; width: 315px;">
                                                                                        <asp:DropDownList ID="dd_tel_countrycode" runat="server" CssClass="input_box" meta:resourcekey="ddl_P_CountryCode_TelResource1"
                                                                                            TabIndex="19" Width="80px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txt_tel_Area_Code" runat="server" CssClass="input_box" MaxLength="4" meta:resourcekey="txt_P_Area_Code_TelResource1" TabIndex="20" Width="60px"></asp:TextBox>
                                                                                    <asp:TextBox ID="txt_tel_Number" runat="server" CssClass="input_box" MaxLength="14" meta:resourcekey="txt_P_Number_TelResource1" TabIndex="21" Width="80px"></asp:TextBox></td>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt_tel_Number" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <td style="height: 12px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; height: 7px; width: 95px;">
                                                                                    </td>
                                                                                <td class="text-1" style="height: 7px; width: 315px;">
                                                                                    Country Code &nbsp; &nbsp; &nbsp; &nbsp;Number</td>
                                                                                <td style="height: 7px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 8px; text-align: right; width: 95px;">
                                                                                    Mobile:</td>
                                                                                <td style="height: 8px; width: 315px;">
                                                                                        <asp:DropDownList ID="dd_mobile_countrycode" runat="server" CssClass="input_box"
                                                                                            meta:resourcekey="ddl_P_CountryCode_TelResource1" TabIndex="22" Width="80px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txt_mobile_Number" runat="server" CssClass="input_box" MaxLength="14" meta:resourcekey="txt_P_Number_MobileResource1" TabIndex="23" Width="148px"></asp:TextBox></td>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txt_mobile_Number" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <td style="height: 8px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; height: 20px; width: 95px;">
                                                                                    </td>
                                                                                <td class="text-1" style="height: 20px; width: 315px;">
                                                                                    CountryCode &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Area Code &nbsp; &nbsp; &nbsp; Number</td>
                                                                                <td style="height: 20px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="height: 20px; text-align: right; width: 95px;">
                                                                                    Fax:</td>
                                                                                <td style="height: 20px; width: 315px;">
                                                                                        <asp:DropDownList ID="dd_fax_country_code" runat="server" CssClass="input_box" meta:resourcekey="ddl_P_CountryCode_TelResource1"
                                                                                            TabIndex="24" Width="80px">
                                                                                        </asp:DropDownList>
                                                                                        <asp:TextBox ID="txt_fax_areacode" runat="server" CssClass="input_box" MaxLength="4" meta:resourcekey="txt_P_Area_Code_TelResource1" TabIndex="25" Width="60px"></asp:TextBox>
                                                                                    <asp:TextBox ID="txt_fax_number" runat="server" CssClass="input_box" MaxLength="14" meta:resourcekey="txt_P_Number_TelResource1" TabIndex="26" Width="80px"></asp:TextBox></td>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" TargetControlID="txt_fax_number" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <td style="height: 20px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 95px">
                                                                                </td>
                                                                                <td style="width: 315px">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 95px;">
                                                                                    Email 1:</td>
                                                                                <td style="width: 315px">
                                                                            <asp:TextBox ID="txt_EMail1" runat="server" CssClass="input_box" MaxLength="99" meta:resourcekey="txt_EMail1Resource1"
                                                                                TabIndex="27" Width="232px"></asp:TextBox></td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 95px">
                                                                                </td>
                                                                                <td style="width: 315px">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_EMail1"
                                                                                ErrorMessage="Invalid Email Format." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: right; width: 95px;">
                                                                                    Email 2:</td>
                                                                                <td style="width: 315px">
                                                                            <asp:TextBox ID="txt_email2" runat="server" CssClass="input_box" MaxLength="99" meta:resourcekey="txt_EMail1Resource1"
                                                                                TabIndex="28" Width="232px"></asp:TextBox></td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 95px">
                                                                                </td>
                                                                                <td style="width: 315px">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_email2"
                                                                                ErrorMessage="Invalid Email Format." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 95px">
                                                                                </td>
                                                                                <td style="width: 315px">
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtpin" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt_tel_Area_Code" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txt_fax_areacode" FilterType="Custom" FilterMode="validChars" ValidChars="0123456789+-" ></ajaxToolkit:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 405px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-top:4px">
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>Passport</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="height: 12px; width: 125px;">
                                                                    </td>
                                                                    <td style="height: 12px">
                                                                        &nbsp;</td>
                                                                    <td style="height: 12px; width: 91px;">
                                                                    </td>
                                                                    <td style="height: 12px; width: 192px;">
                                                                    </td>
                                                                    <td style="height: 12px; width: 87px;">
                                                                    </td>
                                                                    <td style="height: 12px; width: 287px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="height: 19px; width: 125px;">
                                                                        &nbsp;Passport #:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:TextBox ID="txtpassportno" runat="server" CssClass="input_box" MaxLength="19"
                                                                            TabIndex="29" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="height: 19px; width: 91px;">
                                                                        &nbsp;Issue Date:</td>
                                                                    <td align="left" style="height: 19px; width: 192px;">
                                                                        <asp:TextBox ID="txtissuedate" runat="server" CssClass="input_box" MaxLength="15"
                                                                            TabIndex="30" Width="144px"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                    <td style="height: 19px; text-align: right; width: 87px;">
                                                                        Expiry Date:</td>
                                                                    <td style="height: 19px; width: 287px;">
                                                                        <asp:TextBox ID="txtexpirydate" runat="server" CssClass="input_box" MaxLength="15"
                                                                            TabIndex="32" Width="144px"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 13px; width: 125px;">
                                                                    </td>
                                                                    <td align="left" style="height: 13px">
                                                                        &nbsp;</td>
                                                                    <td style="color: #0e64a0; height: 13px; width: 91px;" align="right">
                                                                        &nbsp;
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtissuedate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td style="height: 13px" colspan="2">
                                                                        
                                                                        </td>
                                                                    <td style="color: #0e64a0; height: 13px; width: 287px;">
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtexpirydate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="height: 19px; width: 125px;">
                                                                        Place of Issue:</td>
                                                                    <td align="left" style="height: 19px">
                                                                        <asp:TextBox ID="txtplaceofissue" runat="server" CssClass="input_box" MaxLength="49"
                                                                            TabIndex="34" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="height: 19px; width: 91px;">
                                                                        ECNR:</td>
                                                                    <td align="left" style="height: 19px; text-align: left; width: 192px;">
                                                                        <asp:CheckBox ID="chk_ECNR" runat="server" TabIndex="35" /></td>
                                                                    <td align="right" style="height: 19px; width: 87px;">
                                                                    </td>
                                                                    <td align="left" style="height: 19px; width: 287px;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 13px; width: 125px;">
                                                                    </td>
                                                                    <td align="left" style="height: 13px">
                                                                        &nbsp;</td>
                                                                    <td style="height: 13px; width: 91px;">
                                                                        &nbsp;</td>
                                                                    <td align="left" style="height: 13px; width: 192px;">
                                                                    </td>
                                                                    <td style="height: 13px; width: 87px;">
                                                                    </td>
                                                                    <td align="left" style="height: 13px; width: 287px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 125px">
                                                                    </td>
                                                                    <td align="left" colspan="3" style="height: 13px">
                                                                        &nbsp;
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" TargetControlID="txtissuedate" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                                            Format="dd-MMM-yyyy" PopupButtonID="ImageButton6" PopupPosition="TopLeft" 
                                                                            TargetControlID="txtexpirydate">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                    <td style="height: 13px; width: 87px;">
                                                                        &nbsp;</td>
                                                                    <td align="left" style="height: 13px; width: 287px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; display:none;">
                                                            <legend><strong>Bank</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 127px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 206px; height: 13px">
                                                                        &nbsp;</td>
                                                                    <td style="width: 129px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 112px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 100px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 200px; height: 13px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 19px">
                                                                        Bank Name:</td>
                                                                    <td align="left" style="width: 206px; height: 19px">
                                                                        <asp:TextBox ID="txtbankname" runat="server" CssClass="input_box" MaxLength="49"
                                                                            TabIndex="36" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="width: 129px; height: 19px">
                                                                        Branch Name:</td>
                                                                    <td align="left" style="width: 112px; height: 19px">
                                                                        <asp:TextBox ID="txtbranch" runat="server" CssClass="input_box" MaxLength="49" TabIndex="37"
                                                                            Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="width: 100px; height: 19px">
                                                                        &nbsp;Account #:</td>
                                                                    <td align="left" style="width: 200px; height: 19px">
                                                                        <asp:TextBox ID="txtaccountno" runat="server" CssClass="input_box" MaxLength="19"
                                                                            TabIndex="38" Width="160px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 129px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px">
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                        &nbsp;Personal Code:</td>
                                                                    <td align="left" style="width: 206px">
                                                                        <asp:TextBox ID="txtpersonalcode" runat="server" CssClass="input_box" MaxLength="9"
                                                                            TabIndex="39" Width="160px"></asp:TextBox>&nbsp;</td>
                                                                    <td align="right" style="width: 129px">
                                                                        Swift Code:</td>
                                                                    <td align="left" style="width: 112px">
                                                                        <asp:TextBox ID="txtswiftcode" runat="server" CssClass="input_box" MaxLength="9"
                                                                            TabIndex="40" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="width: 100px">
                                                                        IBAN #:</td>
                                                                    <td align="left" style="width: 200px">
                                                                        <asp:TextBox ID="txtibanno" runat="server" CssClass="input_box" MaxLength="9" TabIndex="41"
                                                                            Width="160px"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 129px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px">
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 4px;">
                                                                        Type of Remittance:</td>
                                                                    <td align="left" style="height: 4px; width: 206px;">
                                                                        <asp:DropDownList ID="ddtypeofremittance" runat="server" CssClass="input_box" TabIndex="42"
                                                                            Width="165px">
                                                                            <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" style="width: 129px; height: 4px;">
                                                                        Beneficiary:</td>
                                                                    <td align="left" style="width: 112px; height: 4px;">
                                                                        <asp:TextBox ID="txt_Beneficiary" runat="server" CssClass="input_box" MaxLength="49"
                                                                            TabIndex="43" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="width: 100px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px; height: 4px;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px; height: 4px">
                                                                    </td>
                                                                    <td align="right" style="width: 129px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px; height: 4px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 100px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px; height: 4px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 11px;">
                                                                        &nbsp;Address:</td>
                                                                    <td align="left" colspan="3" rowspan="4">
                                                                        <asp:TextBox ID="txtbankaddress" runat="server" CssClass="input_box" Height="55px"
                                                                            MaxLength="99" TabIndex="44" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                        &nbsp;</td>
                                                                    <td align="right" style="height: 11px; width: 100px;">
                                                                    </td>
                                                                    <td align="left" style="width: 200px; height: 11px;">
                                                                    </td>
                                                                    <td style="height: 11px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 127px">
                                                                        &nbsp;</td>
                                                                    <td style="width: 100px">
                                                                    </td>
                                                                    <td style="width: 200px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 127px">
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                    </td>
                                                                    <td style="width: 200px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 127px">
                                                                        &nbsp;</td>
                                                                    <td style="width: 100px">
                                                                    </td>
                                                                    <td style="width: 200px">
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; padding-bottom:5px">
                                                        <asp:Button ID="btn_Family_Reset" runat="server" CausesValidation="False" CssClass="btn"
                                                            OnClick="btn_Family_Reset_Click" TabIndex="45" Text="Add" Visible="False" Width="59px" />
                                                        <asp:Button
                                                                ID="btn_family_save" runat="server" CssClass="btn" OnClick="btn_Family_save_Click"
                                                                TabIndex="46" Text="Save" Width="59px" />
                                                        <asp:Button ID="btnfamilyCancel" runat="server"
                                                                    CausesValidation="false" CssClass="btn" OnClick="btnfamilyCancel_Click" TabIndex="47"
                                                                    Text="Cancel" Width="59px" />
                                                        <asp:Button
                                                                ID="btnfamilyPrint" runat="server" CausesValidation="False" CssClass="btn"
                                                                TabIndex="22" Text="Print" Width="56px"  OnClientClick="return printcv();" Visible="false"  /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="panelvisa1" runat="server" Style="background-color: #f9f9f9" Width="100%">
                                                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                <legend><strong>Visa</strong></legend>
                                                                <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; padding-top: 5px; height: 98px; text-align: center">
                                                                    <asp:Label ID="lbl_Gvvisa" runat="server" Text=""></asp:Label>
                                                                    <asp:GridView ID="Gvvisa" runat="server" AutoGenerateColumns="False" 
                                                                            GridLines="horizontal" OnDataBound="Gvvisa_DataBound" 
                                                                            OnPreRender="Gvvisa_PreRender" OnRowDeleting="visa_Row_Deleting" 
                                                                           
                                                                            OnSelectedIndexChanged="Gvvisa_SelectIndexChanged" Style="text-align: center" 
                                                                            Width="98%" OnRowCancelingEdit="Gvvisa_RowCancelingEdit" OnRowCommand="Gvvisa_RowCommand">
                                                                            <RowStyle CssClass="rowstyle" />
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                            <PagerStyle CssClass="pagerstyle" />
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" HeaderText="View" 
                                                                                    SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="true">
                                                                                    <ItemStyle Width="50px" />
                                                                                </asp:CommandField>
                                                                             
                                                                                 <asp:TemplateField HeaderText="Edit">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditVisa" CausesValidation="false"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" OnClick="btnEditVisa_Click"
                                                                                CommandArgument='<%#Eval("CrewFamilyDocumentId")%>' />
                                                                            <asp:HiddenField ID="HiddenIdvisa" runat="server" Value='<%#Eval("CrewFamilyDocumentId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                    <ItemStyle Width="30px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                                            CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                                                            OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" 
                                                                                            Text="Delete" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Visa Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVisaName" runat="server" Text='<%#Eval("VisaName")%>'></asp:Label>
                                                                                        <asp:HiddenField ID="HiddenIdvisa" runat="server" 
                                                                                            Value='<%#Eval("CrewFamilyDocumentId")%>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="VisaType" HeaderText="Visa Type">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VisaNumber" HeaderText="Visa #">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PlaceofIssue" HeaderText="Place of Issue">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Issue Date">
                                                                                    <ItemTemplate>
                                                                                        <%# Alerts.FormatDate(Eval("IssueDate"))%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Expiry Date">
                                                                                    <ItemTemplate>
                                                                                        <%# Alerts.FormatDate(Eval("ExpiryDate"))%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                </div>
                                                            </fieldset></asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top:10px">
                                                        <asp:Panel ID="panelvisa2" runat="server" Style="background-color: #f9f9f9" Width="100%">
                                                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                <legend><strong>Visa Details</strong></legend>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;</td>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td style="width: 97px">
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            Visa Name:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtvisaname" runat="server" CssClass="input_box" MaxLength="49"
                                                                                TabIndex="48" Width="160px"></asp:TextBox></td>
                                                                        <td align="right">
                                                                            Visa Type:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtvisatype" runat="server" CssClass="input_box" MaxLength="14"
                                                                                TabIndex="49" Width="160px"></asp:TextBox></td>
                                                                        <td align="right" style="width: 97px">
                                                                            Visa #:</td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtvisano" runat="server" CssClass="input_box" MaxLength="14" TabIndex="50"
                                                                                Width="160px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;</td>
                                                                        <td align="right">
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                        <td align="right" style="width: 97px">
                                                                        </td>
                                                                        <td align="left">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="height: 19px">
                                                                            Issue Date:</td>
                                                                        <td align="left" style="height: 19px">
                                                                            <asp:TextBox ID="txtvisaissue" runat="server" CssClass="input_box" MaxLength="10"
                                                                                TabIndex="51" Width="145px"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                                OnClientClick="return false;" />&nbsp;
                                                                        </td>
                                                                        <td align="right" style="height: 19px">
                                                                            Expiry Date:</td>
                                                                        <td align="left" style="height: 19px">
                                                                            <asp:TextBox ID="txtvisaexpiry" runat="server" CssClass="input_box" MaxLength="10"
                                                                                TabIndex="52" Width="144px"></asp:TextBox>
                                                                            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                                                OnClientClick="return false;" />&nbsp;
                                                                        </td>
                                                                        <td align="right" style="height: 19px; width: 97px;">
                                                                            Place Of Issue:</td>
                                                                        <td align="left" style="height: 19px">
                                                                            <asp:TextBox ID="txtvisapoi" runat="server" CssClass="input_box" MaxLength="49" TabIndex="53"
                                                                                Width="160px"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="height: 13px">
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtvisaissue" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                </td>
                                                                        <td align="left" style="height: 13px" colspan="2">
                                                                            </td>
                                                                        <td align="left" style="height: 13px">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtvisaexpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator></td>
                                                                        <td align="right" style="height: 13px; width: 97px;">
                                                                        </td>
                                                                        <td align="left" style="height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="height: 13px">
                                                                        </td>
                                                                        <td align="right" colspan="2" style="height: 13px">
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton7" TargetControlID="txtvisaissue" PopupPosition="TopLeft"></ajaxToolkit:CalendarExtender>
                                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton8" PopupPosition="TopLeft" TargetControlID="txtvisaexpiry"></ajaxToolkit:CalendarExtender>
                                                                            &nbsp; &nbsp;
                                                                        </td>
                                                                        <td align="left" style="height: 13px">
                                                                            &nbsp;<asp:HiddenField ID="Hiddenvisapk" runat="server" />
                                                                        </td>
                                                                        <td align="right" style="height: 13px; width: 97px;">
                                                                            &nbsp;&nbsp;</td>
                                                                        <td align="left" style="height: 13px">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset></asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; padding-top:10px; padding-bottom:5px">
                                                        <asp:Button ID="btnvisaadd" runat="server" CausesValidation="False" CssClass="btn"
                                                            OnClick="btnvisaadd_Click" TabIndex="54" Text="Add" Visible="False" Width="59px" />
                                                        <asp:Button
                                                                ID="btnvisasave" runat="server" CssClass="btn" OnClick="btnvisasave_Click" TabIndex="55"
                                                                Text="Save" Width="59px" />
                                                        <asp:Button ID="btnvisacancel" runat="server" CausesValidation="False"
                                                                    CssClass="btn" OnClick="btnvisacancel_Click" TabIndex="56" Text="Cancel" Width="59px" /></td>
                                                </tr>
                                                
                                                </table>      
                                                </asp:View>
                                        <asp:View ID="Tab4" runat="server">
                                            <table cellpadding="0" cellspacing="0"  width="100%">
                                                <tr valign="top">
                                                    <td class="TabArea">
                                                        <table cellpadding="0" cellspacing="0"  width="100%">
                                                            <tr valign="top">
                                                                <td class="TabArea">
                                                                
                                                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-bottom: 5px;display:none;" >
                                                                    <legend ><strong>[<asp:Label ID="txt_MemberId2" runat="server"></asp:Label>]</strong></legend>
                                                                    <table width="71%" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 52px">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td align="right" style="width: 166px; height: 13px; text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 129px; height: 13px; text-align: left">
                                                                                    &nbsp;</td>
                                                                                <td style="padding-left: 5px; width: 175px; height: 13px; text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 118px; height: 13px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="width: 229px; height: 13px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 177px; height: 13px; text-align: left">
                                                                                </td>
                                                                                <td rowspan="5" style="text-align: center; padding-right: 15px;" valign="middle">
                                                                                    <asp:Image ID="img_Crew3" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" ImageUrl="" Height="90px" Width="60px" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 166px; height: 13px; text-align: right">
                                                                                    <asp:Label ID="Label57" runat="server" Text="First Name:" Width="72px" meta:resourcekey="Label57Resource1"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 129px; height: 13px; text-align: left">
                                                                                    <asp:TextBox ID="txt_FirstName2" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_FirstName2Resource1">67</asp:TextBox></td>
                                                                                <td style="padding-left: 5px; width: 175px; height: 13px; text-align: right">
                                                                                    <asp:Label ID="Label58" runat="server" meta:resourcekey="Label58Resource1" Text="Middle Name:"
                                                                                        Width="100%"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 118px; height: 13px; text-align: left">
                                                                                    <asp:TextBox ID="txt_MiddleName2" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_MiddleName2Resource1" TabIndex="68"></asp:TextBox></td>
                                                                                <td align="right" style="width: 229px; height: 13px">
                                                                                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                                                        Width="100%"></asp:Label></td>
                                                                                <td style="padding-left: 5px; width: 177px; height: 13px; text-align: left">
                                                                                    <asp:TextBox ID="txt_LastName2" runat="server" CssClass="input_box" MaxLength="24"
                                                                                        ReadOnly="True" Width="160px" meta:resourcekey="txt_LastName2Resource1" TabIndex="69"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 166px; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 129px; text-align: left; height: 13px;">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 175px; text-align: left; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 118px; text-align: left; height: 13px;">
                                                                                </td>
                                                                                <td align="right" style="width: 229px; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 177px; text-align: left; height: 13px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="WIDTH: 166px; height: 19px; text-align: right;" >
                                                                                    <asp:Label ID="Label55" runat="server" Text="Current Rank:" meta:resourcekey="Label55Resource1"></asp:Label></td>
                                                                                    <td style="PADDING-LEFT: 5px; WIDTH: 129px; TEXT-ALIGN: left; height: 19px;">
                                                                                    <asp:TextBox ReadOnly="True" CssClass="input_box" ID="ddcurrentrank2" runat="server" meta:resourcekey="ddcurrentrank2Resource1" Width="160px" TabIndex="70"></asp:TextBox></td>
                                                                                    <td style="PADDING-LEFT: 5px; WIDTH: 175px; TEXT-ALIGN: right; height: 19px;">
                                                                                    <asp:Label ID="Label4" runat="server" Text="Last Vessel:" Width="100%" meta:resourcekey="Label58Resource1"></asp:Label></td>
                                                                                    <td style="PADDING-LEFT: 5px; WIDTH: 118px; TEXT-ALIGN: left; height: 19px;">
                                                                                    <asp:TextBox ID="txt_LastVessel2" runat="server" CssClass="input_box" meta:resourcekey="ddl_Nationality2Resource1" ReadOnly="True" Width="160px" TabIndex="71"></asp:TextBox></td>
                                                                                    <td style="WIDTH: 229px; height: 19px; text-align: right;" > Passport No:</td>
                                                                                    <td style="PADDING-LEFT: 5px; TEXT-ALIGN: left; width: 177px; height: 19px;">
                                                                                    <asp:TextBox ID="txtPassport2" runat="server" CssClass="input_box" MaxLength="24" meta:resourcekey="txt_LastName2Resource1" ReadOnly="True" TabIndex="69" Width="160px"></asp:TextBox></td>
                                                                                </tr>
                                                                                <tr>
                                                                                <td style="WIDTH: 166px; HEIGHT: 16px" ></td>
                                                                                <td style="PADDING-LEFT: 5px; WIDTH: 129px; HEIGHT: 16px; TEXT-ALIGN: left"></td>
                                                                                <td style="PADDING-LEFT: 5px; WIDTH: 175px; HEIGHT: 16px; TEXT-ALIGN: right"></td>
                                                                                <td style="PADDING-LEFT: 5px; WIDTH: 118px; HEIGHT: 16px; TEXT-ALIGN: left">
                                                                                </td><td style="WIDTH: 229px; HEIGHT: 16px" >
                                                                                </td><td style="PADDING-LEFT: 5px; WIDTH: 177px; HEIGHT: 16px; TEXT-ALIGN: left"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 166px; height: 19px; text-align: right">
                                                                                    <asp:Label ID="Label5" runat="server" meta:resourcekey="Label58Resource1" Text="Status:"
                                                                                        Width="70%"></asp:Label></td>
                                                                                <td colspan="4" style="padding-left: 5px; width: 129px; height: 19px; text-align: left">
                                                                                    <asp:TextBox ID="txt_Status2" runat="server" CssClass="input_box" meta:resourcekey="ddl_Nationality2Resource1"
                                                                                        ReadOnly="True" Width="493px" TabIndex="72"></asp:TextBox></td>
                                                                                <td rowspan="1" style="padding-right: 15px; height: 19px; text-align: center" valign="middle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 166px; height: 16px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 129px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 175px; height: 16px; text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 118px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td style="width: 229px; height: 16px">
                                                                                </td>
                                                                                <td style="padding-left: 5px; width: 177px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td rowspan="1" style="padding-right: 15px; height: 16px; text-align: center" valign="middle">
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </fieldset>
                                                                <%--<br />--%>
                                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" ><legend><strong >Current Company </strong> </legend>
                                                                        <table cellpadding="0" cellspacing="0" style=" width:100%"><tr>
                                                                        <td style="text-align:center; padding-bottom: 5px; padding-top: 5px;">
                                                                        <div style=" width:100%; height:110px; overflow-x:hidden; overflow-y:scroll" > 
                                                                         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting"  DataKeyNames="CrewExperienceId" OnSelectedIndexChanged="GridView3_SelectIndexChanged" PageSize="3" Style="text-align: center" Width="98%" meta:resourcekey="GridView3Resource1" OnPreRender="GridView3_PreRender" GridLines="horizontal"> 
                                                                               <AlternatingRowStyle CssClass="alternatingrowstyle" />
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="False" HeaderText="Company Name" meta:resourcekey="TemplateFieldResource1">
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%# Eval("CrewExperienceId") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Owner" HeaderText="Owner"   SortExpression="Owner" meta:resourcekey="BoundFieldResource1"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                                                                                <asp:BoundField DataField="RankName" HeaderText="Rank" SortExpression="RankName"  meta:resourcekey="BoundFieldResource1"><ItemStyle HorizontalAlign="Left" Width="100px"/></asp:BoundField>
                                                                                <asp:BoundField DataField="VesselName" SortExpression="VesselName" HeaderText="VSL" ><ItemStyle HorizontalAlign="Left" Width="50px"/></asp:BoundField>
                                                                                <asp:BoundField DataField="VesselType" SortExpression="VesselType"  HeaderText="VesselType" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Sign On Dt.">
                                                                            <ItemTemplate>
                                                                            <%# Alerts.FormatDate(Eval("FromDate"))%>
                                                                            </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sign Off Dt.">
                                                                            <ItemTemplate>
                                                                            <%# Alerts.FormatDate(Eval("ToDate"))%>
                                                                            </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                                <asp:BoundField DataField="Duration" SortExpression="Duration" HeaderText="Duration (M)" ><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:BoundField>
                                                                                <%--<asp:BoundField DataField="BHP" SortExpression="BHP"  HeaderText="BHP(Kw)" ><ItemStyle HorizontalAlign="Center" Width="60px" /></asp:BoundField>--%>
                                                                                <asp:BoundField DataField="GRT" SortExpression="GRT"  HeaderText="GRT" ><ItemStyle HorizontalAlign="Left" Width="60px" /></asp:BoundField>
                                                                                <asp:BoundField DataField="SOFReason" SortExpression="SOFReason"  HeaderText="Sign Off Reason" ><ItemStyle HorizontalAlign="Left" Width="130px" /></asp:BoundField>
                                                                                
                                                                                
                                                                            </Columns>
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                        </asp:GridView>
                                                                         <asp:Label ID="lbl_GridView3" runat="server" Text="Label" meta:resourcekey="lbl_GridView3Resource1"></asp:Label>
                                                                        </div>
                                                                         </td></tr></table>
                                                                        </fieldset>
                                                                        <br />
                                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" ><legend><strong >Other Company </strong></legend>
                                                                        <table cellpadding="0" cellspacing="0" style=" width:100%"><tr><td style="text-align:center; padding-bottom: 5px; padding-top: 5px;">
                                                                        <div style=" width:100%; height:200px; overflow-x:hidden; overflow-y:scroll" > 
                                                                        <asp:GridView ID="GridView3_1" runat="server" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" AutoGenerateColumns="False" OnDataBound="DataBound" DataKeyNames="CrewExperienceId"  OnSelectedIndexChanged="GridView3_SelectIndexChanged" PageSize="3" Style="text-align: center" Width="98%" meta:resourcekey="GridView3_1Resource1"  OnRowDeleting="Row_Deleting" OnPreRender="GridView3_1_PreRender" GridLines="horizontal" OnRowCancelingEdit="GridView3_1_RowCancelingEdit" OnRowCommand="GridView3_1_RowCommand" >
                                                                            <Columns>
                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" meta:resourcekey="CommandFieldResource1" />
                                                                               <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:CommandField>--%>
                                                                                 <asp:TemplateField HeaderText="Edit">
                                                                            <ItemStyle Width="25px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgbtnOtherCompanyEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                    <ItemStyle Width="30px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName" meta:resourcekey="TemplateFieldResource2">
                                                                                    <ItemStyle HorizontalAlign="Left" width="250px"/>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' meta:resourcekey="lblCompanyNameResource2"></asp:Label>
                                                                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%# Eval("CrewExperienceId") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="RankName" HeaderText="Rank" SortExpression="RankName" meta:resourcekey="BoundFieldResource7">
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VesselName" HeaderText="Vessel" SortExpression="VesselName" meta:resourcekey="BoundFieldResource11">
                                                                                    <ItemStyle HorizontalAlign="Left" width="200px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="VesselType" HeaderText="Vessel Type" SortExpression="VesselType" meta:resourcekey="BoundFieldResource12">
                                                                                    <ItemStyle HorizontalAlign="Left"  />
                                                                                </asp:BoundField>
                                                                                <%--<asp:TemplateField HeaderText="Sign On Dt.">
                                                                            <ItemTemplate>
                                                                            <%# Alerts.FormatDate(Eval("FromDate"))%>
                                                                            </ItemTemplate> 
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="S/Off Dt.">
                                                                            <ItemTemplate>
                                                                            <%# Alerts.FormatDate(Eval("ToDate"))%>
                                                                            </ItemTemplate> 
                                                                            </asp:TemplateField>
                                                                                <asp:BoundField DataField="Duration" HeaderText="Duration (M)" SortExpression="Duration" meta:resourcekey="BoundFieldResource10">
                                                                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="BHP" SortExpression="BHP"  HeaderText="GRT"  >
                                                                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="SOFReason" SortExpression="SOFReason"  HeaderText="Sign Off Reason" >
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                            <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                                                                            <RowStyle CssClass="rowstyle" />
                                                                        </asp:GridView>
                                                                        <asp:Label ID="lbl_GridView3_1" runat="server" Text="Label" meta:resourcekey="lbl_GridView3_1Resource1"></asp:Label>
                                                                        </div>                  
                                                                        </td></tr></table>
                                                                     </fieldset>
                                                                     <br />
                                                                     <asp:Panel ID="pnl_Experience" runat="server" Visible="false" Width="100%">
                                                                     <table cellpadding="0" cellspacing="0" width="100%">
                                                                     <tr><td style="padding-bottom:15px">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Experience Details</strong></legend>
                                                                        <table cellpadding="0" cellspacing="0" style="text-align: right;" border="0" width="100%">
                                                                            <tr>
                                                                                <td align="right" style="height: 7px" colspan="2">
                                                                                &nbsp;
                                                                                    <asp:HiddenField ID="HiddenPK2" runat="server" />
                                                                                </td>
                                                                                <td style="width: 121px; text-align: left">
                                                                                </td>
                                                                                <td style="width: 199px;">
                                                                                </td>
                                                                                <td align="right" style="width: 5px">
                                                                                </td>
                                                                                <td align="right" style="width: 92px;">
                                                                                </td>
                                                                                <td align="right" style="width: 181px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px; height: 2px;">
                                                                                    <asp:Label ID="Label24" runat="server" Text="Company Name:" Width="108px" meta:resourcekey="Label24Resource1"></asp:Label></td>
                                                                                <td style="width: 182px; height: 2px; text-align: left;">
                                                                                    <asp:TextBox ID="txtcompname" runat="server" CssClass="required_box" Width="143px" MaxLength="49" meta:resourcekey="txtcompnameResource1" TabIndex="73"></asp:TextBox></td>
                                                                                <td style="width: 121px; height: 2px; text-align: right">
                                                                                    <asp:Label ID="Label29" runat="server" Text="Vessel Name:" Width="100px" meta:resourcekey="Label29Resource1"></asp:Label></td>
                                                                                <td style="width: 199px; height: 2px; text-align: left">
                                                                                    <asp:TextBox ID="txtvesselname" runat="server" CssClass="required_box" Width="182px" MaxLength="49" meta:resourcekey="txtvesselnameResource1" TabIndex="74"></asp:TextBox></td>
                                                                                <td align="right" style="width: 5px; height: 2px">
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label28" runat="server" Text="Vessel Type:" Width="82px" meta:resourcekey="Label28Resource1"></asp:Label></td>
                                                                                <td align="left" style="width: 181px">
                                                                                    <asp:DropDownList ID="ddl_VesselType" runat="server" CssClass="required_box" Width="155px" meta:resourcekey="ddrankappResource1" TabIndex="75">
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px; height: 16px">
                                                                                </td>
                                                                                <td style="width: 182px; height: 16px; text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcompname"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator></td>
                                                                                <td style="width: 121px; height: 16px; text-align: left">
                                                                                </td>
                                                                                <td style="width: 199px; height: 16px; text-align: left;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtvesselname"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator></td>
                                                                                <td align="right" style="width: 5px; height: 16px">
                                                                                </td>
                                                                                <td align="right" style="width: 92px; height: 16px">
                                                                                </td>
                                                                                <td align="left" style="width: 181px">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddl_VesselType"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px; height: 4px">
                                                                                    <asp:Label ID="Label26" runat="server" Text="Sign On Dt:" Width="108px" meta:resourcekey="Label26Resource1"></asp:Label></td>
                                                                                <td style="width: 182px; height: 4px; text-align: left;">
                                                                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass="required_box" Width="94px" MaxLength="15" TabIndex="76"></asp:TextBox>
                                                                                    <asp:ImageButton
                                                                                        ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td style="height: 4px; text-align: right; width: 121px;">
                                                                                    <asp:Label ID="Label27" runat="server" Text="Sign Off Dt:" Width="91%" meta:resourcekey="Label27Resource1"></asp:Label></td>
                                                                                <td style="width: 199px; height: 4px; text-align: left">
                                                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="required_box" Width="94px" MaxLength="15" TabIndex="78"></asp:TextBox>
                                                                                    <asp:ImageButton
                                                                                        ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td align="right" style="height: 4px; text-align: right; width: 5px;">
                                                                                </td>
                                                                                <td align="right" style="height: 4px; text-align: right;">
                                                                                    <asp:Label ID="Label25" runat="server" Text="Rank:" meta:resourcekey="Label25Resource1"></asp:Label></td>
                                                                                <td align="left" style="width: 181px">
                                                                                    <asp:DropDownList ID="ddl_Rank1" runat="server" CssClass="input_box" Width="115px" meta:resourcekey="ddrankappResource1" TabIndex="80">
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px; height: 5px;">
                                                                                </td>
                                                                                <td style="width: 182px; text-align: left; height: 5px;">
                                                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtfromdate" ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator8Resource1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                <td style="width: 121px; height: 5px; text-align: left">
                                                                                </td>
                                                                                <td style="width: 199px; height: 5px; text-align: left;">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txttodate" ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator8Resource1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                    </td>
                                                                                <td align="right" style="width: 5px; height: 5px">
                                                                                </td>
                                                                                <td align="right" style="width: 92px; height: 5px;">
                                                                                </td>
                                                                                <td align="right" style="width: 181px; height: 5px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px">
                                                                                    <asp:Label ID="Label33" runat="server" Text="GRT:" meta:resourcekey="Label33Resource1"></asp:Label></td>
                                                                                <td style="width: 182px; text-align: left;">
                                                                                    <asp:TextBox ID="txtbhp" runat="server" CssClass="input_box" Width="110px" MaxLength="20" meta:resourcekey="txtbhpResource1" TabIndex="84"></asp:TextBox></td>
                                                                                <td style="width: 121px; text-align: right">
                                                                                    Sign Off Reason:</td>
                                                                                <td align="right" colspan="2" style="text-align: left">
                                                                                    <asp:DropDownList ID="ddl_Sign_Off_Reason" runat="server" CssClass="required_box" Width="203px" meta:resourcekey="ddrankappResource1" TabIndex="85">
                                                                                </asp:DropDownList></td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblBHP1" runat="server" Text="BHP(kw):"></asp:Label></td>
                                                                                <td align="left" style="width: 181px">
                                                                                    <asp:TextBox ID="txtbhp1" runat="server" CssClass="input_box" MaxLength="20" meta:resourcekey="txtbhpResource1"
                                                                                        TabIndex="86" Width="110px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px">
                                                                                </td>
                                                                                <td style="width: 182px; text-align: left">
                                                                                    &nbsp;</td>
                                                                                <td style="width: 121px; text-align: right">
                                                                                </td>
                                                                                <td style="width: 199px; text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddl_Sign_Off_Reason"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator></td>
                                                                                <td align="right" style="width: 5px">
                                                                                </td>
                                                                                <td align="right" style="width: 92px">
                                                                                </td>
                                                                                <td align="right" style="width: 181px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 172px; height: 6px">

                                                                                </td>
                                                                                <td colspan="3" style="height: 6px">
                                                                                
                                                                                </td>
                                                                                <td align="right" colspan="1" style="height: 6px; text-align: center; width: 5px;">
                                                                                </td>
                                                                                <td align="right" style="height: 6px; text-align: center;" colspan="2">
                                                                                    &nbsp;&nbsp;
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4" style="height: 6px">
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"
                                                                                        PopupButtonID="ImageButton2" TargetControlID="txtfromdate" PopupPosition="TopRight">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                </td>
                                                                                <td align="right" colspan="1" style="height: 6px; text-align: center; width: 5px;">
                                                                                </td>
                                                                                <td align="right" colspan="2" style="height: 6px; text-align: center">
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                                                                                        PopupButtonID="ImageButton3" TargetControlID="txttodate" PopupPosition="TopRight">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                   
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset>
                                                                    </td></tr>
                                                                     </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td class="TabArea" style="text-align: right;">
                                                                <asp:Button ID="btn_Add3" runat="server" CssClass="btn" OnClick="btnadd_Click3" Text="Add" Width="59px" CausesValidation="False" meta:resourcekey="btn_Add3Resource1" TabIndex="87" />
                                                                <asp:Button ID="btn_Save3" runat="server" CssClass="btn" OnClick="btn_Save_Click3" Text="Save" Width="59px" meta:resourcekey="btn_Save3Resource1" TabIndex="88" />
                                                                <asp:Button ID="btn_Experience_Cancel" runat="server" CssClass="btn" OnClick="btn_Experience_Cancel_Click" Text="Cancel" Width="59px" CausesValidation="False" meta:resourcekey="btn_ClearResource1" TabIndex="89" />
                                                                    <asp:Button
                                                                ID="btn_Experience_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                TabIndex="90" Text="Print" Width="56px"  OnClientClick="return printcv();" Visible="false" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="Tab5" runat="server">
                                            <table cellpadding="0" cellspacing="0"  width="100%">
                                                <tr valign="top">
                                                    <td class="TabArea">
                                                        <table cellpadding="0" cellspacing="0"  width="100%">
                                                            <tr valign="top">
                                                                <td class="TabArea">
                                                                
                                                                
                                                               
                                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" ><legend><strong >Bank </strong> </legend>
                                                                        <table cellpadding="0" cellspacing="0" style=" width:100%"><tr>
                                                                        <td style="text-align:center; padding-bottom: 5px; padding-top: 5px;">
                                                                        <div style=" width:100%; height:110px; overflow-x:hidden; overflow-y:scroll" > 
                                                                         <asp:GridView ID="GvBankDetails" runat="server" AutoGenerateColumns="False" AllowSorting="false"   DataKeyNames="CrewBankId" OnSelectedIndexChanged="GvBankDetails_SelectIndexChanged" PageSize="3" Style="text-align: center" Width="98%" meta:resourcekey="GvBankDetailsResource1" OnPreRender="GvBankDetails_PreRender" GridLines="horizontal" OnRowCancelingEdit="GvBankDetails_RowCancelingEdit" OnRowCommand="GvBankDetails_RowCommand" OnRowDeleting="GvBankDetails_RowDeleting" OnRowDataBound="GvBankDetails_RowDataBound"> 
                                                                               <AlternatingRowStyle CssClass="alternatingrowstyle" />
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid"  />
                                                                            <Columns>
                                                                                   <asp:CommandField ButtonType="Image" HeaderText="View" 
                                                                                    SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="true">
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:CommandField>
                                                                              
                                                                                   <asp:TemplateField HeaderText="Edit"> <ItemStyle Width="30px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnBankInfoEdit" CausesValidation="false"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" OnClick="btnBankInfoEdit_Click"
                                                                                CommandArgument='<%#Eval("CrewBankId")%>' />
                                                                            <asp:HiddenField ID="hdnCrewBankId" runat="server" Value='<%#Eval("CrewBankId")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                                 <%-- <asp:TemplateField HeaderText="Edit">
                                                                            <ItemStyle Width="25px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgbtnBankInfoEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                    <ItemStyle Width="30px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgDeleteBankInfo" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="AccountType" HeaderText="Account"   meta:resourcekey="BoundFieldResource1"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                                                                                <asp:BoundField DataField="Beneficiary" HeaderText="Payee Name"   meta:resourcekey="BoundFieldResource1"><ItemStyle HorizontalAlign="Left" Width="120px"/></asp:BoundField>
                                                                                <asp:BoundField DataField="BankName"  HeaderText="Bank Name" ><ItemStyle HorizontalAlign="Left" Width="120px"/></asp:BoundField>
                                                                                <asp:BoundField DataField="BankAccountNumber"   HeaderText="A/C #" ><ItemStyle HorizontalAlign="Left" Width="100px"/></asp:BoundField>
                                                                             <asp:BoundField DataField="BankBranchName"   HeaderText="Branch Name" ><ItemStyle HorizontalAlign="Left" Width="130px" /></asp:BoundField> 
                                                                                  <asp:BoundField DataField="BankCity"   HeaderText="City" ><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:BoundField>
                                                                                <asp:BoundField DataField="CountryName"   HeaderText="Country" ><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:BoundField>  
                                                                               
                                                                               
                                                                                
                                                                                
                                                                            </Columns>
                                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                        </asp:GridView>
                                                                         <asp:Label ID="lbl_GVBankDetails" runat="server" Text="Label" meta:resourcekey="lbl_GridView3Resource1"></asp:Label>
                                                                        </div>
                                                                         </td></tr></table>
                                                                        </fieldset>
                                                                        <br />
                                                                        
                                                                     <asp:Panel ID="PnlBankDetails" runat="server" Visible="false" Width="100%">
                                                                     <table cellpadding="0" cellspacing="0" width="100%">
                                                                     <tr><td style="padding-bottom:15px">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Bank Details</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td style="width: 127px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 206px; height: 13px">
                                                                       <asp:HiddenField ID="hdnCrewBankId" runat="server" /> &nbsp;</td>
                                                                    <td style="width: 129px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 112px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 100px; height: 13px">
                                                                    </td>
                                                                    <td style="width: 200px; height: 13px">
                                                                    </td>
                                                                </tr>
                                                                            <tr>
                                                                    <td align="right" style="width: 127px; height: 19px">
                                                                       Account Type :</td>
                                                                    <td align="left" style="width: 206px; height: 19px">
                                                                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="input_box" MaxLength="49"
                                                                            TabIndex="1" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged">
                                                                            <asp:ListItem Selected="True" Text="Primary" Value="P"></asp:ListItem> 
                                                                             <asp:ListItem  Text="Secondary" Value="S"></asp:ListItem> 
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" style="width: 129px; height: 19px">
                                                                         Beneficiary:</td>
                                                                    <td align="left" style="width: 112px; height: 19px">
                                                                        <asp:TextBox ID="txtBBeneficiary" runat="server" CssClass="input_box" MaxLength="100"
                                                                            TabIndex="14" Width="160px"></asp:TextBox></td>
                                                                    <td align="right" style="width: 100px; height: 19px">Bank Name:
                                                                        </td>
                                                                    <td align="left" style="width: 200px; height: 19px"><asp:TextBox ID="txtBName" runat="server" CssClass="input_box" MaxLength="49"
                                                                            TabIndex="4" Width="160px"></asp:TextBox>
                                                                      </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 129px">
                                                                        
                                                                    </td>
                                                                    <td align="left" style="width: 112px">
                                                                        <asp:RequiredFieldValidator ID="rfvBeneficiary" runat="server" ControlToValidate="txtBBeneficiary"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                         <asp:RequiredFieldValidator ID="rfvBBankName" runat="server" ControlToValidate="txtBName"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 19px">Address:
                                                                        </td>
                                                                    <td align="left" style="width: 206px; height: 40px"> <asp:TextBox ID="txtBAddress" runat="server" CssClass="input_box" Height="35px"
                                                                            MaxLength="99" TabIndex="15" TextMode="MultiLine" Width="98%"></asp:TextBox>
                                                                        </td>
                                                                    <td align="right" style="width: 129px; height: 19px">
                                                                        Branch Name:</td>
                                                                    <td align="left" style="width: 112px; height: 19px"> <asp:TextBox ID="txtBBName" runat="server" CssClass="input_box" MaxLength="50" TabIndex="7"
                                                                            Width="160px"></asp:TextBox>
                                                                       </td>
                                                                    <td align="right" style="width: 100px; height: 19px">
                                                                        City:</td>
                                                                    <td align="left" style="width: 200px; height: 19px">  <asp:TextBox ID="txtBBankCity" runat="server"  CssClass="input_box" MaxLength="50" TabIndex="7"
                                                                            Width="160px"></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                        <asp:RequiredFieldValidator ID="rfvBAddress" runat="server" ControlToValidate="txtBAddress"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator></td>
                                                                    <td align="right" style="width: 129px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px">
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtBBName"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                       <asp:RequiredFieldValidator ID="rfvBankCity" runat="server" ControlToValidate="txtBBankCity"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                             
                                                                
                                                                              <tr>
                                                                    <td align="right" style="width: 127px">
                                                                        Country:</td>
                                                                    <td align="left" style="width: 206px">
                                                                         <asp:DropDownList ID="ddlBankCountry" runat="server" CssClass="required_box" Width="210px" meta:resourcekey="ddlBankCountryResource1" AutoPostBack="True"  TabIndex="8"></asp:DropDownList></td>
                                                                    <td align="right" style="width: 129px">
                                                                    Account #:   </td>
                                                                    <td align="left" style="width: 112px">
                                                                     <asp:TextBox ID="txtACNo" runat="server" CssClass="input_box" MaxLength="19" TabIndex="5" Width="160px" TextMode="Password"></asp:TextBox>
                                                                       <%-- <asp:DropDownList ID="ddlBankCountry" runat="server" CssClass="input_box" 
                                                                            TabIndex="8" Width="160px"></asp:DropDownList>--%>
                                                                       
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                     Re-enter A/C #:  </td>
                                                                    <td align="left" style="width: 200px">
                                                                      <asp:TextBox ID="txtReAcNo" runat="server" CssClass="input_box" MaxLength="19"
                                                                            TabIndex="6" Width="160px"></asp:TextBox>  </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                          <asp:RangeValidator ID="rvBankCountry" runat="server" ControlToValidate="ddlBankCountry" ErrorMessage="Required" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator> </td>
                                                                    <td align="right" style="width: 129px">
                                                                    </td>
                                                                    <td>   <asp:RequiredFieldValidator ID="rfvBankAccount" runat="server" ControlToValidate="txtACNo"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>

                                                                         <asp:RegularExpressionValidator ID="revAcNo" runat="server"  
ControlToValidate="txtACNo" ErrorMessage="Enter numbers only"  
ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                        <asp:RequiredFieldValidator ID="rfvReAcNo" runat="server" ControlToValidate="txtReAcNo"
                                                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                                                                           <asp:RegularExpressionValidator ID="revReAcNo" runat="server"  
ControlToValidate="txtReAcNo" ErrorMessage="Enter numbers only"  
ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                                                         <asp:CompareValidator ErrorMessage="Account # do not match." ForeColor="Red" ControlToCompare="txtACNo"
                ControlToValidate="txtReAcNo" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                       IFSC Code:  </td>
                                                                    <td align="left" style="width: 206px">
                                                                         <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="input_box" MaxLength="25"
                                                                            TabIndex="9" Width="160px"></asp:TextBox>
                                                                     </td>
                                                                    <td align="right" style="width: 129px">
                                                                     Swift Code:   </td>
                                                                    <td align="left" style="width: 112px">
                                                                     <asp:TextBox ID="txtBSwiftCode" runat="server" CssClass="input_box" MaxLength="25"
                                                                            TabIndex="10" Width="160px"></asp:TextBox>&nbsp;   </td>
                                                                    <td align="right" style="width: 100px">
                                                                        IBAN #:
                                                                      </td>
                                                                    <td align="left" style="width: 200px">
                                                                          <asp:TextBox ID="txtBIBAN" runat="server" CssClass="input_box" MaxLength="25" TabIndex="11"
                                                                            Width="160px" ></asp:TextBox>
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 129px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px">
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 4px;">
                                                                        Type of Remittance:</td>
                                                                    <td align="left" style="height: 4px; width: 206px;">
                                                                        <asp:DropDownList ID="ddlBTypeOfRemitteance" runat="server" CssClass="input_box" TabIndex="13"
                                                                            Width="165px">
                                                                            <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                                        </asp:DropDownList></td>
                                                                    <td align="right" style="width: 129px; height: 4px;">
                                                                       </td>
                                                                    <td align="left" style="width: 112px; height: 4px;">
                                                                       </td>
                                                                    <td align="right" style="width: 100px; height: 4px">Active:
                                                                    </td>
                                                                    <td align="left" style="width: 200px; height: 4px;"><asp:CheckBox ID="chkActive" runat="server" TabIndex="12"/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 127px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 206px; height: 4px">
                                                                    </td>
                                                                    <td align="right" style="width: 129px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 112px; height: 4px">
                                                                        &nbsp;</td>
                                                                    <td align="right" style="width: 100px; height: 4px">
                                                                    </td>
                                                                    <td align="left" style="width: 200px; height: 4px">
                                                                    </td>
                                                                </tr>
                                                                
                                                               
                                                            </table>
                                                                        

                                                                    </fieldset>
                                                                    </td></tr>
                                                                     </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr valign="top">
                                                                <td class="TabArea" style="text-align: right;">
                                                                <asp:Button ID="btnAddBankInfo" runat="server" CssClass="btn" OnClick="btnAddBankInfo_Click" Text="Add" Width="59px" CausesValidation="False" meta:resourcekey="btnAddBankInfoResource1" TabIndex="16" />
                                                                <asp:Button ID="btnSaveBankInfo" runat="server" CssClass="btn" OnClick="btnSaveBankInfo_Click" Text="Save" Width="59px" meta:resourcekey="btnSaveBankInfoResource1" TabIndex="17" />
                                                                <asp:Button ID="btnCancelBankInfo" runat="server" CssClass="btn" OnClick="btnCancelBankInfo_Click" Text="Cancel" Width="59px" CausesValidation="False" meta:resourcekey="btnCancelBankInfoResource1" TabIndex="18" />
                                                                   </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="Tab6" runat="server">
                                            <%--<asp:UpdatePanel runat="server" id="UPTab5">
                                            <ContentTemplate>--%>
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr valign="top">
                                                    <td>
                                                        <asp:Button ID="btnRefresh" Text="" OnClick="btnRefresh_Click" style="display:none;" runat="server" />
                                                        <div style="overflow-y: scroll; overflow-x: hidden; height: 20px; border: solid 1px gray;">
                                                            <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse: collapse;
                                                                font-weight: bold; font-size: 11px; height: 20px; background-color: #e5e5e5;
                                                                color: #0e64a0'>
                                                                <colgroup>
                                                                    <col width='25px' />
                                                                    <col width='15px' />
                                                                    <col />
                                                                    <col width='60px' />
                                                                    <col width='90px' />
                                                                    <col width='90px' />
                                                                    <col width='90px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='100px' />
                                                                    <col width='80px' />
                                                                    <col width='20px' />
                                                                </colgroup>
                                                                <tr class= "headerstylegrid">
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <img src="../Images/green_circle.gif" alt="" />
                                                                    </td>
                                                                    <td>
                                                                        Vessel
                                                                    </td>
                                                                    <td>
                                                                        Rank
                                                                    </td>
                                                                    <td>
                                                                        Notify Dt.
                                                                    </td>
                                                                    <td>
                                                                        SignOn Dt
                                                                    </td>
                                                                    <td>
                                                                        SignOff Dt
                                                                    </td>
                                                                    <td>
                                                                        Owner Rep.
                                                                    </td>
                                                                    <td>
                                                                        Charterer
                                                                    </td>
                                                                    <td>
                                                                        Tech Supdt
                                                                    </td>
                                                                    <td>
                                                                        Fleet Mgr
                                                                    </td>
                                                                    <td>
                                                                        Marine Suptd
                                                                    </td>
                                                                    <td>
                                                                        Bonus Amt
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div style="overflow-y: scroll; overflow-x: hidden; height: 415px; border: solid 1px gray;">
                                                            <table width='100%' cellpadding='2' cellspacing='0' border="1" style='border-collapse: collapse;
                                                                font-size: 11px; height: 30px;'>
                                                                <colgroup>
                                                                    <col width='25px' />
                                                                    <col width='15px' />
                                                                    <col />
                                                                    <col width='60px' />
                                                                    <col width='90px' />
                                                                    <col width='90px' />
                                                                    <col width='90px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='80px' />
                                                                    <col width='100px' />
                                                                    <col width='80px' />
                                                                    <col width='20px' />
                                                                </colgroup>
                                                                <asp:Repeater runat="server" ID="rprCrewAssessments">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td style="text-align: center;">
                                                                                <a href='../CrewApproval/CrewAssessmentPrint.aspx?CCBId=<%#Eval("CREWBONUSID")%>'
                                                                                    style='<%#Convert.IsDBNull(Eval("BonusApproved"))?"display:none;": ""%>' target="_blank">
                                                                                    <img src="../Images/print_16.png" title="Print" style="border: none;" alt="" />
                                                                                </a>
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                <img id="Img1" src="../Images/green_circle.gif" title="Email sent" alt="" runat="server"
                                                                                    visible='<%#Eval("IsMailSent").ToString() == "Y"%>' />
                                                                                <img id="Img2" alt="" src="~/Modules/HRD/Images/red_circle.png" title="Email not sent" runat="server"
                                                                                    visible='<%#Eval("IsMailSent").ToString() != "Y"%>' />
                                                                            </td>
                                                                            <td style="text-align: left">
                                                                                <%#Eval("VESSELNAME")%>
                                                                            </td>
                                                                            <td style="text-align: left">
                                                                                <%#Eval("RANKCODE")%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%#Common.ToDateString(Eval("NotifyDt"))%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Common.ToDateString(Eval("SignOndate"))%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Common.ToDateString(Eval("SignOffdate"))%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <div class='Grade_<%#Eval("OwnerRep")%>'>
                                                                                    <a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 1);" href="#" title="View Remarks"
                                                                                        style="text-decoration: none;">
                                                                                        <%#Eval("OwnerRep")%></a></div>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <div class='Grade_<%#Eval("Charterer")%>'>
                                                                                    <a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 2);" href="#" title="View Remarks"
                                                                                        style="text-decoration: none;">
                                                                                        <%#Eval("Charterer")%></a></div>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <div class='Grade_<%#Eval("TechSupdt")%>'>
                                                                                    <a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 3);" href="#" title="View Remarks"
                                                                                        style="text-decoration: none;">
                                                                                        <%#Eval("TechSupdt")%></a></div>
                                                                                        <asp:ImageButton ID="imgTechSupdtEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif"  CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgTechSupdtEdit_Click" runat="server" Visible='<%# (Eval("TechSupdt").ToString() == ""  && Session["loginid"].ToString() == Eval("TechSupdtId").ToString() && Eval("IsMailSent").ToString() == "Y")  %>' />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <div class='Grade_<%#Eval("FleetMgr")%>'>
                                                                                    <a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 4);" href="#" title="View Remarks"
                                                                                        style="text-decoration: none;">
                                                                                        <%#Eval("FleetMgr")%></a></div>
                                                                                        <asp:ImageButton ID="imgFleetMgrEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgFleetMgrEdit_Click" runat="server" Visible='<%# (Eval("FleetMgr").ToString() == ""  && Session["loginid"].ToString() == Eval("FleetManagerId").ToString() && Eval("IsMailSent").ToString() == "Y") %>' />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <div class='Grade_<%#Eval("MarineSupdt")%>'>
                                                                                    <a onclick="ShowRemarks(<%#Eval("CREWBONUSID")%>, 5);" href="#" title="View Remarks"
                                                                                        style="text-decoration: none;">
                                                                                        <%#Eval("MarineSupdt")%></a></div>
                                                                                    <asp:ImageButton ID="imgMarineSupdtEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" CommandArgument='<%#Eval("CREWBONUSID")%>' OnClick="imgMarineSupdtEdit_Click" runat="server" Visible='<%# (Eval("MarineSupdt").ToString() == ""  && Session["loginid"].ToString() == Eval("MarineSupdtId").ToString() && Eval("IsMailSent").ToString() == "Y") %>' />
                                                                            </td>
                                                                            <td style="text-align: right">
                                                                                <%# String.Format("{0:C}", Eval("BonusAmount"))%>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </div>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                                                <%--                     </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                        </asp:View>
                                        <asp:View ID="Tab7" runat="server">
                                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height:28px; background-color:white; ">
                    <table border="1" cellpadding="7" cellspacing="0" width="100%" style="border-collapse:collapse" class="bordered">
                        <tr class= "headerstylegrid">
                            <td  style="width:30px">View</td>
                            <td style="width:50px">Crew#</td>
                            <td>Crew Name</td>
                            <td style="width:80px">Current Rank</td>
                            <td style="width:80px">Planned Rank</td>
                            <td style="width:90px">Planned Vessel</td>
                            <td style="width:90px">Approved On</td>
                            <td style="width:50px">Crew</td>
                            <td style="width:50px">Technical</td>
                            <td style="width:50px">Marine</td>
                            <td style="width:100px">Fleet Manager</td>
                            <td style="width:100px">Mgmt Approval</td>
                        </tr>
                        </table>
                    </div>
                <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height:350px; background-color:white; ">
                        <table border="1" cellpadding="7" cellspacing="0" width="100%"  style="border-collapse:collapse"  class="bordered"> 
                           
                    <asp:Repeater runat="server" id="gv_CrewApproval">
                        <ItemTemplate>
                            <tr>
                                <td style="width:30px">
                                    <a target="_blank" href="../CrewOperation/CrewPlanningApproval.aspx?_P=<%#Eval("PlanningId") %>" ><img style='border:none;' src="../Images/magnifier.png" title="View" /></a> 
                                </td>
                                <td style="width:50px">
                                     <%# Eval("EmpNo")%>                                    
                                    <asp:HiddenField ID="hfd_PlanningId" runat="server" Value='<% #Eval("PlanningId") %>' />
                                    <asp:HiddenField ID="hfd_AppStatus" runat="server" Value='<% #Eval("AppStatus") %>' />
                                </td>            
                                <td><%#Eval("CrewName") %>
                                    <asp:HiddenField ID="hfd_CrewId" runat="server" Value='<% #Eval("CrewId") %>' /></td>            
                                <td style="width:80px"><%#Eval("RankCode") %></td>            
                                <td style="width:80px"><%#Eval("PlannedRankCode") %></td>  
                                <td style="width:90px"><%#Eval("VesselCode") %></td>      
                              <%--  <td>
                                    <img src="../Images/cv.png" onclick="javascript:printrelivercv('<%# Eval("CrewId") %>');" style="cursor:pointer;" title="Open Crew CV" />
                                    <img src="../Images/report.gif" onclick="javascript:printvesselmatrix('<%# Eval("CrewId") %>','<%# Eval("RelieveId")%>','<%# Eval("VesselId") %>');" style="cursor:pointer;" title="Open Vessel Matrix Report" />
                                    <asp:ImageButton ID="btnCL" CommandName="img_dc" runat="server" ImageUrl="~/Modules/HRD/Images/icon_note.png" title="Open Document CheckList" OnClick='btnCL_Click' style="cursor:pointer;" CommandArgument='<%#Eval("PlanningId")%>'/>
                                </td>--%>
                               <%-- <td style="width:90px"><%#Eval("LastVessel") %></td>            --%>
				 <td style="width:90px"><%#Common.ToDateString(Eval("ApprovedOn")) %></td>      
                                <td style="width:50px"><%#getCssStatus(1,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:50px"><%#getCssStatus(2,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:50px"><%#getCssStatus(3,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:100px"><%#getCssStatus(4,Common.CastAsInt32(Eval("PlanningId"))) %></td>
                                <td style="width:100px"><%#getCssStatus(99,Common.CastAsInt32(Eval("PlanningId"))) %></td>                               
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                        <%--<asp:GridView ID="gv_CrewApproval" runat="server" AutoGenerateColumns="False" Width="100%" OnPreRender="gv_CrewApproval_PreRender" AllowSorting="false"  CssClass="bordered" BorderWidth="0" >
                         <Columns> 
                            

                            <asp:TemplateField HeaderText="Planning & Approval Details"  ItemStyle-HorizontalAlign="Left" Visible="false">
                                
                                <ItemTemplate>                                    
                                    
                                </ItemTemplate>
                            </asp:TemplateField>                           
                          </Columns>
                             <HeaderStyle CssClass="heading" HorizontalAlign="Left"  />                            
                            
                     </asp:GridView>--%>
                    </div>
                                        </asp:View>
                                    
        </asp:MultiView>  
        <asp:Button ID="btnShowRemarks" OnClick="btnShowRemarks_Click" runat="server" Style="display: none;" /><asp:HiddenField ID="hfCBId" runat="server" /><asp:HiddenField ID="hfMUM" runat="server" />
                                <%--<asp:BoundField DataField="BHP" SortExpression="BHP"  HeaderText="BHP(Kw)" ><ItemStyle HorizontalAlign="Center" Width="60px" /></asp:BoundField>--%>
                                            <div style="position:absolute;top:40px;left:0px; height :470px; width:100%; " id="dv_ViewRemarks" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:230px;padding :5px; text-align :center;background : white; z-index:150;top:180px; border:solid 0px black;">
            <center >
             <div style="padding:6px; background-color:#FFE6B2; font-size:14px; "><strong>View Remarks</strong></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:200px;">
               <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 150px; text-align: center; border-collapse:collapse; width:100%;">
                     <tr>                         
                          <td style="text-align: left;">
                             <b>Email :</b>&nbsp;<asp:Label ID="lblEmail" runat="server" ></asp:Label>   
                          </td>
                      </tr>
                     <tr>                         
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Height="130px" Width="98%" runat="server" ></asp:TextBox>   
                          </td>
                      </tr>
                      </table>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style=" text-align:center;">                              
                              <asp:Button ID="btn_Close" runat="server" Text="Close" Width="80px" OnClick="btn_Close_Click" CausesValidation="false" style=" background-color:red; color:White; border:none; padding:4px;"/>
                          </td>
                        </tr>
                      </table>
             </div>
             </center>
        </div>
    </center>
    </div>
                                <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:CommandField>--%>
                                       
                                </td>
                           </tr>
                         </table>
                </td>
                <td style="width: 25%;">
                     <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                 <asp:Image ID="img_Crew" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" ImageUrl="" Height="108px" Width="100px" />
                                <asp:HiddenField ID="HiddenPK" runat="server" /><asp:HiddenField id="HiddenField1" runat="server"></asp:HiddenField>
             <asp:HiddenField ID="hfd_fileName" runat="server" />
                            </td>  
                         </tr>
                         <tr>
                             <td style="text-align: center;" colspan="2">
                                  <div style="border:0px solid; width:100px">
                                                            <asp:FileUpload ID="FileUpload1" size="1" runat="server" style="position:relative; border:0px solid; background-color:#f9f9f9" />                                            
                                                        </div>
                             </td>
                         </tr>
                         </table>
                     <table style="background-color:#f9f9f9;border:1px dashed;" cellpadding="0" cellspacing="0" width="100%" class="table table-bordered">
                         
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">    
                                <strong>Name :</strong> </td>
                             <td style="text-align:left;padding:5px;width:125px;">
                                 <asp:Label ID="lblName" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong>Rank :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblCurrRank" runat="server"></asp:Label>
                                 <%--<asp:TemplateField HeaderText="Sign On Dt.">
                                                                            <ItemTemplate>
                                                                            <%# Alerts.FormatDate(Eval("FromDate"))%>
                                                                            </ItemTemplate> 
                                                                            </asp:TemplateField>--%>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                 <strong> Age : </strong></td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                  <strong>Current Vessel :</strong>
                                   </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblCurrentVessel" runat="server"></asp:Label>
                                 <%# Alerts.FormatDate(Eval("ToDate"))%>
                                  </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                                <strong> Rank Exp. : </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                <asp:Label ID="lblRankExp" runat="server"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Rating :</strong>
                             </td>
                             <td style="text-align:left;padding:5px;">

                                 <asp:Label ID="lblRating" runat="server"></asp:Label>

                             </td>
                         </tr>
                         <tr>
                             <td style="text-align:Left;padding:5px;width:100px;">
                               <strong> Status :  </strong>
                             </td>
                             <td style="text-align:left;padding:5px;">
                                 <asp:Label ID="lblStatus" runat="server"></asp:Label>
                             </td>
                         </tr>
                     </table> 
                     <br />
                     <table width="100%" style="background-color: #f9f9f9; vertical-align:top; overflow:visible;">
                        <tr>
                           <td ><asp:Button runat="server"  CommandArgument="0" Text="Personal" OnClick="Menu1_MenuItemClick" ID="b1" CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /></td>
                            </tr>
                             <tr>
                                <td><asp:Button runat="server"  CommandArgument="1" Text="Contact" OnClick="Menu1_MenuItemClick"  ID="b2"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False"/></td>
                                  </tr>
                        <tr>
                                <td><asp:Button runat="server"  CommandArgument="2" Text="Family" OnClick="Menu1_MenuItemClick" ID="b3"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False"/></td> </tr>
                         <tr>
                                <td><asp:Button runat="server"  CommandArgument="3" Text="Experience" OnClick="Menu1_MenuItemClick" ID="b4"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /></td> 
                         </tr>
                          <tr>
                                <td><asp:Button runat="server"  CommandArgument="4" Text="Bank" OnClick="Menu1_MenuItemClick" ID="btnBank"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /></td> 
                         </tr>  
                          <tr> 
                              <td>
                                    <asp:Button ID="btn_Print" runat="server" CommandArgument="5" CausesValidation="False" CssClass="btn1" Text="Print CV" Width="100px" OnClientClick="return printcv();" Font-Bold="True" OnClick="Menu1_MenuItemClick" />
                                
                                 <%--<asp:UpdatePanel runat="server" id="UPTab5">
                                            <ContentTemplate>--%>
                               </td>
                          </tr>
                          <tr> 
                              <td>
                                  <%#Eval("VESSELNAME")%>
                               </td>
                          </tr>
                         <%#Eval("RANKCODE")%>                        <%#Common.ToDateString(Eval("NotifyDt"))%>
                    </table>
                </td>
            </tr>
        </table>
         </td>
        </tr>
      </table>
        </div>
    </asp:Content>
                                        

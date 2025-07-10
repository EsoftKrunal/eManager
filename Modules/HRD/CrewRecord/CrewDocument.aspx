<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDocument.aspx.cs" MasterPageFile="~/MasterPage.master" Title="Documents" Inherits="CrewRecord_CrewDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
   <%-- <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script  type="text/javascript">
        function CallPrint(strid) {

            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            // prtContent.innerHTML=strOldOne;
        }
        function Show_Image_Large(obj) {
            window.open(obj.src, "a", "resizable=1,toolbar=0,scrollbars=1,status=0");

        }
        function Show_Image_Large1(path) {
            window.open(path, "", "resizable=1,toolbar=0,scrollbars=1,status=0");
        }

        function openpage() {
            var url = "../Registers/PopUp_Port.aspx"
            window.open(url, null, 'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
            return false;
        }
    </script>
    <style type="text/css">
.selbtn
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
    
}
        .auto-style1 {
            left: -3px;
            position: relative;
            top: 0px;
        }
        </style>
    </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: left">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" >
    <tr>
    <td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center; width :100%" width="100%">
            <tr>
                <td style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband">
                <img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> 
                Crew Documents</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9" width="100%">
                        <tr>
                            <td style="text-align:left;padding-left:10px;background-color:#fff">
                                 <asp:LinkButton ID="b6" runat="server"  Text="Personal" Font-Bold="True" OnClick="b6_Click" ForeColor="#206020"></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="b7" runat="server"  Text="Notes & History" Font-Bold="True" OnClick="b7_Click" ForeColor="#206020"></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                                 <asp:HyperLink runat="Server" ID="doc_Alert" Target="_blank" NavigateUrl="DocumentAlerts.aspx" ForeColor="Red" ></asp:HyperLink> 
                                 <asp:Label ID="crm_Alert" runat="server" ForeColor="Red"></asp:Label> &nbsp;&nbsp;&nbsp;
                                 <asp:LinkButton runat="server" ID="lnkAppraisal" Visible ="false" ForeColor="Red" style="text-decoration :none" OnClientClick ="window.open('../AlertSignOffAppraisal.aspx');return false;" Text ="EOC appraisal not recived." ></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton runat="server" ID="lnkPopUp" Visible ="false" ForeColor="Red" Font-Size="14pt" OnClientClick ="window.open('../CriticalRemarkPopUp.aspx');return false;" Text ="Remarks" Font-Bold="True" ></asp:LinkButton> &nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="text-align:center;background-color:#fff;font-size:14px;">
                               <strong>[<asp:Label ID="txt_MemberId" runat="server"></asp:Label>]</strong>

                                <%-- <asp:Button runat="server"  CommandArgument="5" Text="Personal" OnClick="Menu1_MenuItemClick" ID="b6"  CssClass="btn1"  Font-Bold="True" Width="100px" />
                                    
                                     <asp:Button runat="server"  CommandArgument="6" Text="Remarks" OnClick="Menu1_MenuItemClick" ID="b7"  CssClass="btn1"  Font-Bold="True" Width="100px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td   style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left;width:75%;">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="4" style="text-align:center;">
                                            
                                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" Font-Bold="true"  ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                         
                                     </tr>
                                    <%--<tr>
                                        <td>
                                   <asp:Menu ID="Menu1" runat="server" meta:resourcekey="Menu1Resource1" OnMenuItemClick="Menu1_MenuItemClick"
                                    Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" Width="357px">
                                    <Items>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/travel_a.gif" meta:resourcekey="MenuItemResource1"
                                            Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/professional_d.gif" meta:resourcekey="MenuItemResource2"
                                            Text=" " Value="1"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/academic_d.gif" meta:resourcekey="MenuItemResource3"
                                            Text=" " Value="2"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/medical_d.gif" meta:resourcekey="MenuItemResource4"
                                            Text=" " Value="3"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/archivedocs_d.gif" meta:resourcekey="MenuItemResource4"
                                            Text=" " Value="4"></asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                                        </td>
                                        <td style="width: 50px;">
                                            <asp:ImageButton ID="imgbtn_Personal" runat="server" ImageUrl="~/Modules/HRD/Images/btnPersonal.gif"
                                                OnClick="imgbtn_Personal_Click" ToolTip="Personal Details/Exp." CausesValidation="False" /></td>
                                        <td style="width: 50px;">
                                            <asp:ImageButton ID="imgbtn_CRM" runat="server" ImageUrl="~/Modules/HRD/Images/btnCRM.gif" OnClick="imgbtn_CRM_Click"
                                                ToolTip="Remarks" CausesValidation="False" /></td>
                                        <td style="width: 50px;">
                                            <asp:ImageButton ID="imgbtn_Search" runat="server" ImageUrl="~/Modules/HRD/Images/Activity.gif"
                                                OnClick="imgbtn_Search_Click" ToolTip="Tools" CausesValidation="False" /></td>
                                    </tr>--%>
                                </table>
                                 <table cellpadding="0" cellspacing="0" width="100%" style="display:none;">
                                    <%--<tr>
                                        <td>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;display:none;">              
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr>
                                                <td align="right" style="text-align: right; ">
                                                    <asp:Label ID="Label43" runat="server" meta:resourcekey="Label43Resource1" Text="First Name:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; text-align: left; ">
                                                    <asp:TextBox ID="txt_FirstName" runat="server" CssClass="required_box" MaxLength="24"
                                                        meta:resourcekey="txt_FirstNameResource1" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                                <td style="padding-left: 5px; text-align: right; ">
                                                    <asp:Label ID="Label37" runat="server" meta:resourcekey="Label37Resource1" Text="Middle Name:"
                                                        Width="82px"></asp:Label></td>
                                                <td style="padding-left: 5px; text-align: left; ">
                                                    <asp:TextBox ID="txt_MiddleName" runat="server" CssClass="input_box" MaxLength="24" Width="160px"></asp:TextBox></td>
                                                <td align="right" style="height: 15px">
                                                    <asp:Label ID="Label38" runat="server" meta:resourcekey="Label38Resource1" Text="Family Name:"
                                                        Width="100%"></asp:Label></td>
                                                <td style="padding-left: 5px; text-align: left; ">
                                                    <asp:TextBox ID="txt_LastName" runat="server" CssClass="required_box" MaxLength="24" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right"></td>
                                                <td style="padding-left: 5px; text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_FirstName"
                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator></td>
                                                <td style="padding-left: 5px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_LastName"
                                                        ErrorMessage="Required." meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                    <asp:Label ID="Label15" runat="server" meta:resourcekey="Label15Resource1" Text="Current Rank:"></asp:Label></td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    <asp:TextBox ID="ddcurrentrank" runat="server" CssClass="input_box" meta:resourcekey="ddcurrentrankResource1"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td style="padding-left: 5px; text-align: right;">
                                                    <asp:Label ID="Label44" runat="server" meta:resourcekey="Label44Resource1" Text="Last Vessel:"
                                                        Width="72px"></asp:Label></td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    <asp:TextBox ID="txt_LastVessel" runat="server" CssClass="input_box" MaxLength="24"
                                                        ReadOnly="True" Width="160px"></asp:TextBox></td>
                                                <td align="right">
                                                    Passport:</td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    <asp:TextBox ID="txt_Passport" runat="server" CssClass="input_box" MaxLength="24" Width="160px" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                    &nbsp;</td>
                                                <td style="padding-left: 5px; text-align: right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                    Status:</td>
                                                <td colspan="3" style="padding-left: 5px; text-align: left">
                                                    <asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" meta:resourcekey="ddcurrentrankResource1"
                                                        ReadOnly="True" Width="520px"></asp:TextBox></td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="text-align: right">
                                                </td>
                                                <td colspan="3" style="padding-left: 5px; text-align: left">
                                                    &nbsp;</td>
                                                <td align="right">
                                                </td>
                                                <td style="padding-left: 5px; text-align: left">
                                                </td>
                                                <td colspan="1" rowspan="1" style="padding-right: 3px; text-align: center" valign="middle">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </fieldset>
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td>
                                        </td>
                                    </tr>--%>
                                </table>
                                <div id="divPrint">
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="Tab1" runat="server">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style=" text-align:center ">
                                                    <table width="100%">
                                                   <%-- <tr><td>&nbsp;</td></tr>--%>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="True" Font-Bold="True"
                                                                    OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal" Width="650px">
                                                                    <asp:ListItem Value="3">Passport</asp:ListItem>
                                                                    <asp:ListItem Value="4">Visa</asp:ListItem>
                                                                    <asp:ListItem Value="5">Seaman Book</asp:ListItem>
                                                                    <asp:ListItem Value="6">INDOS Certificate</asp:ListItem>
                                                                    <asp:ListItem Value="7">SID</asp:ListItem>
                                                                    <asp:ListItem Value="8">Registration No</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                    <asp:Label ID="lbl_message" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                                                    <asp:HiddenField ID="HiddenPK" runat="server" />
                                                    <asp:HiddenField ID="HiddenTravel" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style=" text-align:center">
                                                  <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                                                  <legend><strong>Travel Document</strong></legend>
                                                  <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                   <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll"> 
                                                  <asp:GridView ID="gvDocument" OnRowDataBound="DataBound" runat="server" OnSelectedIndexChanged="gvDocument_SelectedIndexChanged" meta:resourcekey="gvDocumentResource1" AutoGenerateColumns="False" DataKeyNames="TravelDocumentId"  OnRowDeleting="Row_Deleting" OnPreRender="gvDocument_PreRender" Width="98%" GridLines="Horizontal" OnRowCancelingEdit="gvDocument_RowCancelingEdit" OnRowCommand="gvDocument_RowCommand" OnRowUpdating="gvDocument_RowUpdating">
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" ><ItemStyle Width="25px" /></asp:CommandField>
                                                                     <asp:TemplateField HeaderText="Edit">
                                                                            <ItemStyle Width="25px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImgbtnDocumentEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" CancelImageUrl="~/Modules/HRD/Images/edit.jpg" UpdateImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ><ItemStyle Width="25px" /></asp:CommandField>--%>
                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="25px" />
                                                                        <ItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                                                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%#Eval("TravelDocumentId")%>' />
                                                                        <asp:HiddenField ID="Hiddenfd11" runat ="server" Value='<%#Eval("ImagePath") %>' />
                                                                        <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                    </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Country" >
                                                                        <ItemTemplate >
                                                                            <asp:HiddenField ID="HiddenFlag" runat="server" Value='<%#Eval("FlagStateid")%>' />
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FlagStateName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="VisaName" HeaderText="VisaType">
                                                                        <ItemStyle HorizontalAlign="Left"  />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DocumentNumber" HeaderText="Document #">
                                                                        <ItemStyle HorizontalAlign="Left" />
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
                                                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                     <asp:BoundField DataField="ECNR" HeaderText="ECNR">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Archive">
                                                                        <ItemTemplate>

                                                                          <asp:HiddenField ID="arc_DocType" runat="server" Value='<%# Eval("ImagePath")%>' />
                                                                          

                                                                          <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("DocumentNumber")%>' />
                                                                          <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("IssueDate"))%>' />
                                                                          <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                          <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                          <asp:ImageButton runat="server" ID="ibArchiveTravel" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveTravel" />                                                                        
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle CssClass="rowstyle" />
                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                 
                                                            </asp:GridView>
                                                  <asp:Label ID="lbl_gvDocument" runat="server" Text=""></asp:Label>
                                                  </div> 
                                                  </td></tr></table>        
                                                  </fieldset>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                        <asp:Panel ID="pnl_Travel" runat="server" Visible="False" Width="100%">
                                                            <br />
                                                    
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                                                        <legend><strong>Travel Document</strong></legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    &nbsp;</td>
                                                                <td align="right" style="text-align: right">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                </td>
                                                                <td colspan="1" rowspan="8" style="padding-left: 5px; vertical-align: middle; width: 140px;
                                                                    text-align: center; padding-right: 10px;" valign="middle">
                                                                    <asp:Image ID="img_Travel" runat="server" style="cursor:hand" ToolTip="Click to Preview" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Visible="False" /><br />
                                                                    
                                                                    <div style="border:0px solid; overflow:hidden; width:125px">
                                <asp:FileUpload ID="FileUpload_Travel" size="1" runat="server" style="left:-10px; position:relative; border:0px solid; background-color:#f9f9f9"  />                                            
                                                        </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px; height: 19px;">
                                                                    &nbsp;<asp:Label ID="lbl_FlagStateId" runat="server">Country:</asp:Label></td>
                                                                <td style="padding-left: 5px; text-align: left; height: 19px;">
                                                                    <asp:DropDownList ID="ddl_Flag" runat="server" CssClass="input_box" Width="165px" TabIndex="1">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right" style="text-align: right; height: 19px;">
                                                                    <asp:Label ID="lbl_VisaName" runat="server">Visa Type:</asp:Label></td>
                                                                <td style="padding-left: 5px; text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txt_VisaName" runat="server" CssClass="input_box" MaxLength="49"
                                                                        TabIndex="2" Width="160px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    &nbsp;</td>
                                                                <td align="right" style="text-align: right">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                    <asp:Label ID="lbl_DocNo" runat="server">Passport #:</asp:Label></td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="input_box" Width="160px" MaxLength="49" TabIndex="2"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right;">
                                                                    Place Of Issue:</td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:TextBox ID="txtPlaceofIssue" runat="server" CssClass="input_box" Width="160px" MaxLength="49" TabIndex="3"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    &nbsp;</td>
                                                                <td align="right" style="text-align: right">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                    Issue Date:</td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:TextBox ID="txtIssueDate" runat="server" CssClass="required_box" Width="148px" TabIndex="4"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  />
                                                                </td>
                                                                <td align="right" style="text-align: right;">
                                                                    Expiry Date:</td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="input_box" Width="144px" TabIndex="5"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="txtIssueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator100" ControlToValidate="txtIssueDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                </td>
                                                                <td align="right" style="text-align: right">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtExpiryDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; width: 121px;">
                                                                    <asp:Label ID="lbl_ECNR" runat="server">ECNR:</asp:Label></td>
                                                                <td style="padding-left: 5px; text-align: left;">
                                                                    <asp:CheckBox ID="chk_ECNR" runat="server" TabIndex="7" /></td>
                                                                <td align="right" style="text-align: right;">
                                                                    </td>
                                                                <td style="padding-left: 5px; text-align: left;">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="5" style="text-align: center; height: 13px;"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="5" style="padding-right: 8px; text-align: right;">
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton1" TargetControlID="txtIssueDate" PopupPosition="TopLeft">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton2" TargetControlID="txtExpiryDate" PopupPosition="TopLeft">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    
                                                                    <asp:HiddenField ID="hfd_Image1" runat="server" /></td>
                                                            </tr>
                                                        </table>
                                                    </fieldset></asp:Panel>
                                                    <br />
                                                    <asp:Button ID="btn_Add" OnClick="btn_Add_Click" runat="server" CssClass="btn" Text="Add" Width="59px" TabIndex="7" CausesValidation="False" />
                                                    <asp:Button ID="btn_Save" OnClick="btn_Save_Click1" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="8" />
                                                    <asp:Button ID="btn_Cancel" OnClick="btn_Cancel_Click" runat="server" CssClass="btn" Text="Cancel" Width="59px" TabIndex="9" CausesValidation="False" />
                                                    <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn" OnClientClick="javascript:CallPrint('divPrint');"
                                                        TabIndex="10" Text="Print" Width="59px" /></td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab2" runat="server">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                    <asp:RadioButtonList AutoPostBack="true" ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Font-Bold="True">
                                                        <asp:ListItem>Licence</asp:ListItem>
                                                        <asp:ListItem>Course &amp; Certificate</asp:ListItem>
                                                        <asp:ListItem>Dangerous Cargo Endorsement</asp:ListItem>
                                                        <asp:ListItem>Other Documents</asp:ListItem>
                                                    </asp:RadioButtonList>&nbsp;
                                                    <asp:Panel ID="pnl_Professional_1" runat="server" Height="100%" Width="100%">
                                                       <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>License</strong></legend>
                                                                        <asp:Label ID="lbl_License_Message" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                                            <asp:GridView ID="gvLicense" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewLicenseId"
                                                                                OnPreRender="gvLicense_PreRender" OnRowDataBound="gvLicense_RowDataBound" OnRowDeleting="gvLicense_RowDeleting"
                                                                                 OnSelectedIndexChanged="gvLicense_SelectedIndexChanged"
                                                                                Width="98%" GridLines="Horizontal" OnRowCancelingEdit="gvLicense_RowCancelingEdit" OnRowCommand="gvLicense_RowCommand">
                                                                                <PagerStyle CssClass="pagerstyle" />
                                                                                <RowStyle CssClass="rowstyle" />
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                                <Columns>
                                                                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                                                                                    <HeaderStyle Width="30px" />
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:CommandField>
                                                                                     <asp:TemplateField HeaderText="Edit">
                                                                                                                    <ItemStyle Width="25px" />
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="ImagebtnLicenceEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                                                            ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                              <%--  <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ShowEditButton="True">
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:CommandField>--%>
                                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                    <ItemStyle Width="40px" />
                                                                                    <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                    <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" /></ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="License Name">
                                                                                        <ItemTemplate>
                                                                                        <asp:HiddenField ID="HcountryId" runat="server" Value='<%#Eval("CountryId")%>' />
                                                                                        <asp:HiddenField ID="HcourseId" runat="server" Value='<%#Eval("LicenseId")%>' />
                                                                                        <asp:Label ID="lblcourse" runat="server" Text='<%#Eval("LicenseName") %>'></asp:Label>
                                                                                        <asp:HiddenField ID="himag" runat="server" Value='<%#Eval("ImagePath") %>' />
                                                                                        <asp:HiddenField ID="verified" runat="server" Value='<%#Eval("IsVerified") %>' />
                                                                                        <asp:HiddenField ID="hiddenLvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Grade" HeaderText="Grade"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                                <asp:BoundField DataField="Number" HeaderText="License #"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
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
                                                                                
                                                                                  
                                                                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Country Name" Visible="false"  >
                                                                                        <ItemTemplate>
                                                                                        
                                                                                        <asp:Label ID="lblcountry" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                                                                         </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                    <asp:BoundField DataField="IsVerified" HeaderText="Is Verified">
                                                                                        <ItemStyle Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="VerifiedBy" HeaderText="Verified By">
                                                                                        <ItemStyle Width="70px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="VerifiedOn" HeaderText="Verified On" Visible="false"  >
                                                                                        <ItemStyle Width="70px" />
                                                                                    </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='0' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("CrewLicenseId")%>' />
                                                                          
                                                                          
                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("LicenseName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("Number")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("IssueDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>
                                                                                </Columns>
                                                                               
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                                    <fieldset id="trLicensefields" runat="server" style="border-right: #8fafdb 1px solid;
                                                                        border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong> License</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                                            padding-top: 5px; height: auto; text-align: center" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 118px; height: 13px;">
                                                                                            </td>
                                                                                            <td colspan="3" style="padding-left: 5px; text-align: left; height: 13px;">
                                                                                                &nbsp;</td>
                                                                                            <td colspan="1" rowspan="11" style="padding-right: 10px; padding-left: 5px; vertical-align: middle;
                                                                                                width: 38px; text-align: center" valign="middle">
                                                                                                <asp:Image ID="img_License" style="cursor:hand" ToolTip="Click to Preview" runat="server" Height="90px" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Visible="False" /><br />
                                                                                                
                                                                                                <div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="License_FileUpload" size="1" runat="server" style="left:-6px; position:relative; border:0px solid; background-color:#f9f9f9" Width="82px" />                                            
                                                                                                </div>  
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 118px; text-align: right">
                                                                                                License Name:</td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left; width: 284px;">
                                                                                                <asp:DropDownList ID="dd_License_licensename" runat="server" AutoPostBack="True"
                                                                                                    CssClass="required_box" OnSelectedIndexChanged="dd_License_licensename_SelectedIndexChanged"
                                                                                                    Width="321px" TabIndex="1">
                                                                                                </asp:DropDownList></td>
                                                                                            <td align="right" style="width: 113px">
                                                                                                Country Name:</td>
                                                                                            <td style="padding-left: 5px; width: 188px; text-align: left">
                                                                                                <asp:DropDownList ID="ddl_License_country" runat="server" CssClass="input_box" Width="165px" TabIndex="1">
                                                                                                </asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 118px; text-align: right">
                                                                                            </td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left; width: 284px;">
                                                                                                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dd_License_licensename"
                                                                                                    ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                                            <td align="right" style="width: 113px">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 188px; text-align: left">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 118px;">
                                                                                                Grade:</td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left; width: 284px;">
                                                                                                <asp:TextBox ID="txt_License_grade" runat="server" CssClass="input_box" MaxLength="20" Width="160px" TabIndex="2"></asp:TextBox>
                                                                                            </td>
                                                                                            <td align="right" style="width: 113px">
                                                                                                License #:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 188px;">
                                                                                                <asp:TextBox ID="txt_License_LicenseNo" runat="server" CssClass="input_box" MaxLength="20" Width="160px" TabIndex="3"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 118px; height: 22px;">
                                                                                            </td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left; width: 284px; height: 22px;">
                                                                                                </td>
                                                                                            <td align="right" style="width: 113px; height: 22px;">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 188px; height: 22px;">
                                                                                                <asp:DropDownList ID="dd_license_licenseex" runat="server" Style="display: none">
                                                                                                </asp:DropDownList></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 118px;">
                                                                                                Issue Date:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 284px;">
                                                                                                <asp:TextBox ID="txt_License_Issuedate" runat="server" CssClass="required_box" MaxLength="20"
                                                                                                    Width="143px" TabIndex="4"></asp:TextBox>
                                                                                                <asp:ImageButton ID="btn_License_Issuedate" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td align="right" style="width: 113px">
                                                                                                Expiry Date:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 188px;">
                                                                                                <asp:TextBox ID="txt_license_Expirydate" runat="server" CssClass="input_box" MaxLength="20"
                                                                                                    Width="143px" TabIndex="5"></asp:TextBox>
                                                                                                <asp:ImageButton ID="img_liccense_expirydate" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                                                &nbsp;&nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 118px; text-align: right">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 284px; text-align: left">
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_License_Issuedate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txt_License_Issuedate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                           </td>
                                                                                            <td align="right" style="width: 113px">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 188px; text-align: left">
                                                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_license_Expirydate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             
                                                                                           </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 118px">
                                                                                                Place Of Issue:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 284px;">
                                                                                                <asp:TextBox ID="txt_License_PlaceofIssue" runat="server" CssClass="input_box" MaxLength="50"
                                                                                                    Width="160px" TabIndex="6"></asp:TextBox></td>
                                                                                            <td align="right" style="text-align: right; width: 113px;">
                                                                                                Verified:</td>
                                                                                            <td colspan="1" style="padding-left: 5px; text-align: left; width: 188px;">
                                                                                                <asp:CheckBox ID="chk_Verified" runat="server" TabIndex="6" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="btn_License_Issuedate" TargetControlID="txt_License_Issuedate" PopupPosition="TopLeft">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="img_liccense_expirydate" TargetControlID="txt_license_Expirydate" PopupPosition="TopLeft">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                            <asp:HiddenField ID="h_License_File"
                                                                                                        runat="server" />
                                                                                <asp:HiddenField ID="h_Licenceid" runat="server" />
                                                                                                &nbsp;&nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset></td></tr></table>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="text-align: right;">
                                                                                <asp:Button ID="btn_License_Add" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    OnClick="btn_License_Add_Click" Text="Add" Width="59px" TabIndex="8" />
                                                                                <asp:Button ID="btn_License_Save" runat="server" CssClass="btn" OnClick="btn_License_Save_Click"
                                                                                    Text="Save" Width="59px" TabIndex="9" />
                                                                                <asp:Button ID="btn_License_cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    OnClick="btn_License_Cancel_Click" Text="Cancel" Width="59px" TabIndex="10" />
                                                                                <asp:Button ID="btn_License_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_Professional_2" runat="server" Height="100%" Width="100%">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;
                                                            text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; pdding-bottom: 10px;">
                                                                      <legend><strong>Course &amp; Certificate</strong></legend>
                                                                      <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                                        <asp:Label ID="lbl_certificate_message" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                                            <asp:GridView ID="gvCourseCertificate" runat="server" AutoGenerateColumns="False" DataKeyNames="CourseCerficiateId" OnPreRender="gvCourseCertificate_PreRender" OnRowDataBound="gvCourseCertificate_RowDataBound" OnRowDeleting="gvCourseCertificate_RowDeleting"
                                                                               OnSelectedIndexChanged="gvCourseCertificate_SelectedIndexChanged"
                                                                                Width="98%" GridLines="Horizontal" OnRowCancelingEdit="gvCourseCertificate_RowCancelingEdit" OnRowCommand="gvCourseCertificate_RowCommand" >
                                                                                <PagerStyle CssClass="pagerstyle" />
                                                                                <RowStyle CssClass="rowstyle" />
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                                <Columns>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                                        ShowSelectButton="True">
                                                                                        <HeaderStyle Width="30px" />
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>
                                                                                    <asp:TemplateField HeaderText="Edit">
                                                                                                        <ItemStyle Width="25px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="ImgbtnCourseEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                  <%--  <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                                        ShowEditButton="True">
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>--%>
                                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                        <ItemStyle Width="40px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                                Text="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" /></ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Course Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="HcourseId" runat="server" Value='<%#Eval("CourseCertificateId")%>' />
                                                                                            <asp:Label ID="lblcourse" runat="server" Text='<%#Eval("CourseName") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="himag" runat="server" Value='<%#Eval("ImagePath") %>' />
                                                                                            <asp:HiddenField ID="hiddenCvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                                        
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="DocumentNumber" HeaderText="Document #">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Issue Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("DateofIssue"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Expiry Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("ExpiryDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="IssuedBy" HeaderText="IssuedBy">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='1' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("CourseCerficiateId")%>' />

                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("CourseName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("DocumentNumber")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("DateofIssue"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Left" CssClass="headerstylefixedheadergrid" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                        </td></tr></table>
                                                                    </fieldset>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                                    <fieldset id="trcertificatefields" runat="server" style="border-right: #8fafdb 1px solid;
                                                                        border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Course &amp; Certificate</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                                            padding-top: 5px; height: auto; text-align: center" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 165px; text-align: right">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 239px; text-align: left">
                                                                                                &nbsp;</td>
                                                                                            <td style="width: 157px; text-align: right">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 186px; text-align: left">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 188px; text-align: left">
                                                                                            </td>
                                                                                            <td rowspan="9" style="padding-right: 10px; padding-left: 5px; vertical-align: middle;
                                                                                                width: 110px; text-align: center">
                                                                                                <asp:Image ID="img_cerificate" style="cursor:hand" ToolTip="Click to Preview" runat="server" Height="90px" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Visible="False" /><br />
                                                                                                
                                                                                                 
                                                                                                <div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="CrewCertifiacateFileUpload" size="1" runat="server" style="border-style: solid; border-color: inherit; border-width: 0px; background-color:#f9f9f9" Width="82px" CssClass="auto-style1" />                                            
                                                                                                </div>  
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: right; width: 165px; height: 20px;">
                                                                                                Course Name:</td>
                                                                                            <td colspan="3" style="padding-left: 5px; height: 20px; text-align: left">
                                                                                                <asp:DropDownList ID="ddl_certificate_course" runat="server" AutoPostBack="True"
                                                                                                    CssClass="required_box" OnSelectedIndexChanged="ddl_certificate_course_SelectedIndexChanged"
                                                                                                    Width="591px" TabIndex="1">
                                                                                                </asp:DropDownList>&nbsp;</td>
                                                                                            <td colspan="1" style="padding-left: 5px; height: 20px; text-align: left">
                                                                                            </td>
                                                                                                
                                                                                                
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 165px; height: 13px;">
                                                                                            </td>
                                                                                            <td style="text-align: left; width: 239px; height: 13px;">
                                                                                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddl_certificate_course"
                                                                                                    ErrorMessage="Required" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                                            <td style="width: 157px; height: 13px;">
                                                                                            </td>
                                                                                            <td style="width: 186px; height: 13px;">
                                                                                            </td>
                                                                                            <td style="width: 188px; height: 13px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: right; width: 165px;">
                                                                                                Issue Date:</td>
                                                                                            <td style="text-align: left; padding-left: 5px; width: 239px;">
                                                                                                <asp:TextBox ID="txt_certificate_issuedate" runat="server" CssClass="required_box"
                                                                                                    MaxLength="20" Width="106px" TabIndex="3"></asp:TextBox>
                                                                                                <asp:ImageButton ID="img_certificate_issue" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                            <td style="text-align: right; width: 157px;">
                                                                                                Document #:</td>
                                                                                            <td style="text-align: left; padding-left: 5px; width: 186px;">
                                                                                                <asp:TextBox ID="txt_certificate_docno" runat="server" CssClass="input_box" MaxLength="40"
                                                                                                    Width="204px" TabIndex="2"></asp:TextBox></td>
                                                                                            <td style="padding-left: 5px; width: 188px; text-align: left">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="height: 13px; width: 165px;">
                                                                                            </td>
                                                                                            <td style="text-align: left; height: 13px; width: 239px;">
                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_certificate_issuedate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txt_certificate_issuedate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                           </td>
                                                                                            <td style="height: 13px; width: 157px;">
                                                                                            </td>
                                                                                            <td style="text-align: left; height: 13px; width: 186px;">
                                                                                                </td>
                                                                                            <td style="width: 188px; height: 13px; text-align: left">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="text-align: right; width: 165px;">
                                                                                                Issue By:</td>
                                                                                            <td style="text-align: left; padding-left: 5px; width: 239px;">
                                                                                                <asp:TextBox ID="txtcertificate_issueBy" runat="server" CssClass="input_box" MaxLength="50"
                                                                                                    Width="195px" TabIndex="5"></asp:TextBox></td>
                                                                                            <td style="text-align: right; width: 157px;">
                                                                                                Expiry Date:</td>
                                                                                            <td style="width: 186px; text-align: left;">
                                                                                                &nbsp;<asp:TextBox ID="txt_certificate_expirydate" runat="server" CssClass="input_box"
                                                                                                    MaxLength="20" Width="106px" TabIndex="4"></asp:TextBox>
                                                                                                <asp:ImageButton ID="img_certificate_expiry" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                            <td style="width: 188px; text-align: left">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 165px; text-align: right">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; width: 239px; text-align: left">
                                                                                            </td>
                                                                                            <td style="width: 157px; text-align: right">
                                                                                            </td>
                                                                                            <td style="width: 186px; text-align: left">
                                                                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_certificate_expirydate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                           </td>
                                                                                            <td style="width: 188px; text-align: left">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 165px">
                                                                                            </td>
                                                                                            <td style="width: 239px">
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="img_certificate_issue" TargetControlID="txt_certificate_issuedate" PopupPosition="TopLeft">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="img_certificate_expiry" TargetControlID="txt_certificate_expirydate" PopupPosition="TopLeft">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 157px">
                                                                                                <asp:DropDownList ID="ddcouseex" runat="server" Style="display: none">
                                                                                                </asp:DropDownList>&nbsp;</td>
                                                                                            <td style="width: 186px">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td style="width: 188px">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                                &nbsp;</td>
                                                                                            <td colspan="1">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset></td></tr></table>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td id="Td2" runat="server" align="right" style="text-align: right;">
                                                                                                <asp:HiddenField ID="hfile" runat="server" />
                                                                                <asp:HiddenField ID="hcertificateid" runat="server" />
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                                &nbsp; &nbsp;<asp:Button ID="btn_Certificate_Add" runat="server" CausesValidation="False"
                                                                                    CssClass="btn" OnClick="btn_Certificate_Add_Click" Text="Add" Width="59px" TabIndex="7" />
                                                                                <asp:Button ID="btn_Certifiacte_Save" runat="server" CssClass="btn" OnClick="btn_Certificate_Save_Click"
                                                                                    Text="Save" Width="59px" TabIndex="8" />
                                                                                <asp:Button ID="btn_Certificate_Cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    OnClick="btn_Certificate_Cancel_Click" Text="Cancel" Width="59px" TabIndex="9" />
                                                                                <asp:Button ID="btn_Certificate_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                                     TabIndex="10" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_Professional_3" runat="server" Height="100%" Width="100%">
                                                   <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td style=" text-align:center">
                                                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Dangerous Cargo Endorsement</strong></legend>
                                                                        <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                                         <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                                            <asp:GridView ID="GvDCE" runat="server" AutoGenerateColumns="False" OnRowDataBound="GvDCE_DataBound"
                                                                                OnPreRender="GvDCE_PreRender" OnRowDeleting="DCE_Row_Deleting" 
                                                                                OnSelectedIndexChanged="GvDCE_SelectIndexChanged" Style="text-align: center"
                                                                                Width="98%" GridLines="Horizontal" OnRowCancelingEdit="GvDCE_RowCancelingEdit" OnRowCommand="GvDCE_RowCommand">
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <RowStyle CssClass="rowstyle" />
                                                                                <PagerStyle CssClass="pagerstyle" />
                                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                                <Columns>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                                        ShowSelectButton="true">
                                                                                        <ItemStyle Width="50px" />
                                                                                    </asp:CommandField>
                                                                                   <asp:TemplateField HeaderText="Edit">
                                                                                                        <ItemStyle Width="25px" />
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="ImgbtnCargoEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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
                                                                                    <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" /></ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="center" Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Cargo Name" ItemStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblcargoname" runat="server" Text='<%#Eval("CargoName")%>'></asp:Label>
                                                                                            <asp:HiddenField ID="HiddenIdcargo" runat="server" Value='<%#Eval("DangerousCargoId")%>' />
                                                                                            <asp:HiddenField ID="Hiddenimage" runat="server" Value='<%#Eval("ImagePath")%>' />
                                                                                            <asp:HiddenField ID="hiddenDvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Number" HeaderText="Number">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Nationality" HeaderText="Nationality">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="GradeLevel" HeaderText="Grade Level">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                    </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Issue Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("DateofIssue"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Expiry Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("ExpiryDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='2' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("DangerousCargoId")%>' />

                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("CargoName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("Number")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("DateofIssue"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <asp:Label ID="lbl_GridView_cargo" runat="server" Text=""></asp:Label></div>
                                                                        </td></tr></table>
                                                                    </fieldset>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:Panel ID="panelDCE" runat="server" Visible="false" Width="100%">
                                                                 <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Dangerous Cargo Endorsement</strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    &nbsp; &nbsp;</td>
                                                                                <td colspan="1" style="width: 33px">
                                                                                </td>
                                                                                <td runat="server" rowspan="11" style=" text-align: center; width: 71px; padding-right: 10px; padding-left: 5px; vertical-align: middle;" id="Td1">
                                                                                    <asp:Image ID="Imagecargo" style="cursor:hand" ToolTip="Click to Preview" runat="server" Height="90px" ImageUrl="~/EMANAGERBLOB/HRD/CrewPhotos/noimage.jpg" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Visible="False" /><br />
                                                                                    
                                                                                       
                                                                                                <div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="FileUploadcargo" size="1" runat="server" style="left:0px; position:relative; border:0px solid; background-color:#f9f9f9; top: 1px;" Width="78px" />                                            
                                                                                                </div>             
                                                                                    
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px; text-align: right;">
                                                                                    Dangerous Cargo Endorsements:</td>
                                                                                <td align="left" style="padding-left: 5px; width: 150px;">
                                                                <asp:DropDownList ID="ddcargoname" runat="server" CssClass="required_box" TabIndex="1" Width="248px"> <asp:ListItem Value=" ">&lt; Select &gt;</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                                                <td align="right" style="width: 143px">
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px">
                                                                                </td>
                                                                                <td align="left" style="width: 150px">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddcargoname"
                                                                                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                                                <td align="right" style="width: 143px">
                                                                                </td>
                                                                                <td align="left">
                                                                                </td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px; height: 19px; text-align: right;">
                                                                                    Number:</td>
                                                                                <td align="left" style="padding-left: 5px; width: 150px; height: 19px;">
                                                                                    <asp:TextBox ID="txtnumber" runat="server" CssClass="input_box" MaxLength="19" TabIndex="2"
                                                                                        Width="160px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 143px; height: 19px; text-align: right;">
                                                                                    Nationality:</td>
                                                                                <td align="left" style="padding-left: 5px; height: 19px;">
                                                                                    <asp:DropDownList ID="ddDCEnationality" runat="server" CssClass="required_box" TabIndex="3"
                                                                                        Width="165px">
                                                                                        <asp:ListItem Value=" ">&lt; Select &gt;</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td align="left" style="width: 33px; height: 19px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px">
                                                                                </td>
                                                                                <td align="left" style="width: 150px">
                                                                                </td>
                                                                                <td align="right" style="width: 143px">
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddDCEnationality"
                                                                                        ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px; text-align: right;">
                                                                                    Grade / Level I / II:</td>
                                                                                <td align="left" style="padding-left: 5px; width: 150px;">
                                                                                    <asp:TextBox ID="txtgradelevel" runat="server" CssClass="input_box" MaxLength="9"
                                                                                        TabIndex="4" Width="160px"></asp:TextBox></td>
                                                                                <td align="right" style="width: 143px">
                                                                                    Place Of Issue:</td>
                                                                                <td align="left" style="padding-left: 5px">
                                                                                    <asp:TextBox ID="txtcargopoi" runat="server" CssClass="input_box" MaxLength="49"
                                                                                        TabIndex="5" Width="160px"></asp:TextBox></td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px">
                                                                                </td>
                                                                                <td align="left" style="width: 150px">
                                                                                </td>
                                                                                <td align="right" style="width: 143px">
                                                                                    &nbsp;</td>
                                                                                <td align="left" style="width: 127px">
                                                                                </td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px; text-align: right;">
                                                                                    Date Of Issue:
                                                                                </td>
                                                                                <td align="left" style="padding-left: 5px;">
                                                                                    <asp:TextBox ID="txtcargoDOI" runat="server" CssClass="input_box" MaxLength="15"
                                                                                        TabIndex="6" Width="143px"></asp:TextBox>&nbsp;<asp:ImageButton ID="Imagedoi" runat="server"
                                                                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td align="right" style="width: 143px; text-align: right;">
                                                                                    Expiry Date:</td>
                                                                                <td align="left" style="padding-left: 5px;">
                                                                                    <asp:TextBox ID="txtcargoExpiry" runat="server" CssClass="input_box" MaxLength="15"
                                                                                        TabIndex="7" Width="143px"></asp:TextBox>&nbsp;<asp:ImageButton ID="Imageexpiry"
                                                                                            runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 234px">
                                                                                </td>
                                                                                <td align="left" style="width: 150px">
                                                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtcargoDOI" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             </td>
                                                                                <td align="right" style="width: 143px">
                                                                                </td>
                                                                                <td align="left" style="width: 127px">
                                                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtcargoExpiry" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                           </td>
                                                                                <td align="left" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="5" style="text-align: center">
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderdoi1" runat="server" PopupButtonID="Imagedoi"
                                                                                        TargetControlID="txtcargoDOI" Format="dd-MMM-yyyy" PopupPosition="TopLeft">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                 
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderexp2" runat="server" PopupButtonID="Imageexpiry"
                                                                                        TargetControlID="txtcargoExpiry" Format="dd-MMM-yyyy" PopupPosition="TopLeft">
                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                
                                                                                </td>
                                                                                <td align="left" colspan="2">
                                                                                    <asp:HiddenField ID="hiddencargoimage" runat="server" />
                                                                                    <asp:HiddenField ID="Hidden_cargo" runat="server" Value="" />
                                                                                </td>
                                                                                <td align="left" colspan="1" style="width: 33px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset></td></tr></table>
                                                                </asp:Panel>
                                                                <asp:Button ID="btn_cargo_Add" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="btn_cargo_Add_Click" Text="Add" Visible="False" Width="59px" TabIndex="9" />
                                                                <asp:Button ID="btn_cargo_save" runat="server" CssClass="btn" OnClick="btn_cargo_save_Click"
                                                                    Text="Save" Visible="False" Width="59px" TabIndex="10" />
                                                                <asp:Button ID="btn_cargo_cancel" runat="server" CausesValidation="false" CssClass="btn"
                                                                    OnClick="btn_cargo_cancel_Click" Text="Cancel" Visible="False" Width="59px" TabIndex="11" />
                                                                <asp:Button ID="btn_cargo_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                    TabIndex="12" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td>
                                                        </tr>
                                                    </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnl_Professional_4" runat="server" Width="100%">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;
                                                            text-align: center" width="100%"  >
                                                            <tr>
                                                                <td valign="top">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Other Document </strong></legend>
                                                                        <asp:Label ID="lbl_otherdoc_message" runat="server"></asp:Label>
                                                                         <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                                            <asp:GridView ID="gvotherdocument" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewOtherDocumentId"
                                                                                OnPreRender="gvotherdocument_PreRender" OnRowDataBound="gvotherdocument_RowDataBound"
                                                                                OnRowDeleting="gvotherdocument_RowDeleting" 
                                                                                OnSelectedIndexChanged="gvotherdocument_SelectedIndexChanged" Width="98%" GridLines="Horizontal" OnRowCancelingEdit="gvotherdocument_RowCancelingEdit" OnRowCommand="gvotherdocument_RowCommand">
                                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                                <PagerStyle CssClass="pagerstyle" />
                                                                                 <RowStyle CssClass="rowstyle" />
                                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                                <Columns>
                                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                                        ShowSelectButton="True">
                                                                                        <HeaderStyle Width="30px" />
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>
                                                                                   <%-- <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                                        ShowEditButton="True">
                                                                                        <ItemStyle Width="30px" />
                                                                                    </asp:CommandField>--%>
                                                                                     <asp:TemplateField HeaderText="Edit">
                                                                                <ItemStyle Width="25px" />
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="ImgbtnOthDocEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                                        <ItemStyle Width="40px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                                Text="Delete" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="">
                                                                                        <ItemTemplate>
                                                                                            <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="center" Width="30px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Document Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:HiddenField ID="HdocId" runat="server" Value='<%#Eval("OtherDocumentId")%>' />
                                                                                            <asp:Label ID="lbldocument" runat="server" Text='<%#Eval("DocumentName") %>'></asp:Label>
                                                                                            <asp:HiddenField ID="himag" runat="server" Value='<%#Eval("ImagePath") %>' />
                                                                                            <asp:HiddenField ID="hiddenOvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left"/>
                                                                                        <HeaderStyle Width="200px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="DocumentNumber" HeaderText="Document #">
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                     <asp:TemplateField HeaderText="Issue Date">
                                                                                    <ItemTemplate>
                                                                                    <%# Alerts.FormatDate(Eval("DateofIssue"))%>
                                                                                    </ItemTemplate> 
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Expiry Date">
                                                                                    <ItemTemplate>
                                                                                    <%# Alerts.FormatDate(Eval("ExpiryDate"))%>
                                                                                    </ItemTemplate> 
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="IssuedBy" HeaderText="IssuedBy">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="IsActive" HeaderText="IsActive">
                                                                                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                                                    </asp:BoundField>
                                                                                
                                                                                <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='3' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("CrewOtherDocumentId")%>' />

                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("DocumentName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("DocumentNumber")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("DateofIssue"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </fieldset>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                                    <fieldset id="trotherdoc" runat="server" style="border-right: #8fafdb 1px solid;
                                                                        border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                                        <legend><strong>Other Document </strong></legend>
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                                            padding-top: 5px; height: auto; text-align: center" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 243px;">
                                                                                                Document Name:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 193px;">
                                                                                                <asp:DropDownList ID="ddl_otherdoc_coursename" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddl_otherdoc_coursename_SelectedIndexChanged" Width="211px" TabIndex="1">
                                                                                                </asp:DropDownList></td>
                                                                                            <td align="right" style="text-align: right; width: 178px;">&nbsp;&nbsp; Document #:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 115px;">
                                                                                                <asp:TextBox ID="txt_otherdoc_docno" runat="server" CssClass="input_box" MaxLength="20" Width="160px" TabIndex="2"></asp:TextBox></td>
                                                                                            <td align="left" rowspan="6" style="width: 109px; text-align: center; padding-right: 10px; padding-left: 5px;" valign="middle">
                                                                                                <asp:Image ID="img_otherdoc"  BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" style="cursor:hand" ToolTip="Click to Preview" runat="server" Height="90px" Width="60px" Visible="False" /><div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="otherdoc_fileupload" size="1" runat="server" style="left:-6px; position:relative; border:0px solid; background-color:#f9f9f9" Width="85px" />
                                                                                                </div>  
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 243px;"></td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 193px;"><asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddl_otherdoc_coursename"
                                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td align="right" style="text-align: right; width: 178px;">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 115px;">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 243px;">
                                                                                                Issue Date:</td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left;">
                                                                                                <asp:TextBox ID="txt_otherdoc_issuedate" runat="server" CssClass="required_box" MaxLength="20"
                                                                                                    Width="139px" TabIndex="3"></asp:TextBox>
                                                                                                <asp:ImageButton ID="img_otherdoc_issue" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                            <td align="right" style="width: 178px">
                                                                                                Expiry Date:</td>
                                                                                            <td style="padding-left: 5px; text-align: left;">
                                                                                                <asp:TextBox ID="txt_otherdoc_expirtdate" runat="server" CssClass="input_box" MaxLength="20"
                                                                                                    Width="139px" TabIndex="4"></asp:TextBox>
                                                                                                <asp:ImageButton ID="img_otherdoc_expiry" runat="server" CausesValidation="false"
                                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="text-align: right; width: 243px;">
                                                                                            </td>
                                                                                            <td rowspan="1" style="padding-left: 5px; text-align: left; width: 193px;">
                                                                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_otherdoc_issuedate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txt_otherdoc_issuedate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                         
                                                                                                </td>
                                                                                            <td align="right" style="width: 178px">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 115px;">
                                                                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txt_otherdoc_expirtdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                         </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 243px">
                                                                                                Issue By:&nbsp;</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 193px;">
                                                                                                <asp:TextBox ID="txt_otherdoc_issueby" runat="server" CssClass="input_box" MaxLength="50"
                                                                                                    Width="206px" TabIndex="5"></asp:TextBox></td>
                                                                                            <td align="right" style="text-align: right; width: 178px;">
                                                                                                Is Active:</td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 115px;">
                                                                                                <asp:CheckBox ID="chk_OtherDoc_IsActive" runat="server" Width="180px" TabIndex="6" /></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right" style="width: 243px">
                                                                                            </td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 193px;">
                                                                                                &nbsp;</td>
                                                                                            <td align="right" style="text-align: right; width: 178px;">
                                                                                                <asp:DropDownList ID="dd_otherdoc_courseex" runat="server" Style="display: none">
                                                                                                </asp:DropDownList></td>
                                                                                            <td style="padding-left: 5px; text-align: left; width: 115px;">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="4">
                                                                                                </td>
                                                                                            <td align="right" colspan="1" rowspan="1" style="text-align: left; width: 109px;">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </fieldset></td></tr></table>
                                                                    <table style="background-color: #f9f9f9" width="100%">
                                                                        <tr>
                                                                            <td id="Td3" runat="server" align="right" style="text-align: right">
                                                                                <asp:HiddenField ID="h_otherdoc_hfile" runat="server" />
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="img_otherdoc_issue" PopupPosition="TopLeft" TargetControlID="txt_otherdoc_issuedate">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                               
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" Format="dd-MMM-yyyy"
                                                                                                    PopupButtonID="img_otherdoc_expiry" TargetControlID="txt_otherdoc_expirtdate">
                                                                                                </ajaxToolkit:CalendarExtender>
                                                                                               
                                                                                <asp:HiddenField ID="h_otherdoc_id" runat="server" />
                                                                                &nbsp; &nbsp;&nbsp;<asp:Button ID="btn_otherdoc_Add" runat="server" CausesValidation="False"
                                                                                    CssClass="btn" OnClick="btn_otherdoc_Add_Click" Text="Add" Width="59px" TabIndex="8" />
                                                                                <asp:Button ID="btn_Otherdoc_Save" runat="server" CssClass="btn" OnClick="btn_Otherdoc_Save_Click"
                                                                                    Text="Save" Width="59px" TabIndex="9" />
                                                                                <asp:Button ID="btn_Otherdoc_Cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    OnClick="btn_Otherdoc_Cancel_Click" Text="Cancel" Width="59px" TabIndex="10" />
                                                                                <asp:Button ID="btn_Otherdoc_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                                                    TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab3" runat="server">
                                    <br />
                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                                    <legend><strong>Academic Documents</strong></legend>
                                    <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                     <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                    <asp:Label ID="lbl_GridView_Academic" runat="server" Text=""></asp:Label>
                                    <asp:GridView ID="GridView_Academic" runat="server" AutoGenerateColumns="False" DataKeyNames="AcademicDetailsId"
                                                            OnRowDataBound="GridView_Academic_DataBound" OnPreRender="GridView_Academic_PreRender"
                                                            OnRowDeleting="GridView_Academic_Row_Deleting" 
                                                            OnSelectedIndexChanged="GridView_Academic_SelectIndexChanged" Style="text-align: center" Width="98%" GridLines="horizontal" OnRowCancelingEdit="GridView_Academic_RowCancelingEdit" OnRowCommand="GridView_Academic_RowCommand">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" HeaderText="View" meta:resourcekey="CommandFieldResource1" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" >
                                                                    <ItemStyle Width="25px" />
                                                                </asp:CommandField>
                                                         <%--       <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ShowEditButton="True"> <ItemStyle Width="25px" /></asp:CommandField>--%>
                                                                 <asp:TemplateField HeaderText="Edit">
                                                                        <ItemStyle Width="25px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImgbtnAcedemicEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" >
                                                                    <ItemStyle Width="25px" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Certificate Name" meta:resourcekey="TemplateFieldResource2">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("TypeOfCertificate") %>'></asp:Label>
                                                                        <asp:HiddenField ID="HiddenId" runat="server" Value='<%# Eval("AcademicDetailsId") %>' />
                                                                        <asp:HiddenField ID="Hiddenfd22" runat ="server" Value='<%#Eval("ImagePath") %>' />
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Institute" HeaderText="Institute" >
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                 <asp:TemplateField HeaderText="Duration From">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("DurationFrom"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Duration To">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("DurationTo"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                 
                                                                <asp:BoundField DataField="Grade" HeaderText="Grade" >
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                               <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                 <RowStyle CssClass="rowstyle" />
                                                               
                                                        </asp:GridView>
                                    </div>
                                    </td></tr></table>        
                                    </fieldset>
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td style="padding-top:13px">
                                                    <!-- By: Pankaj K. Verma : 30-May-2008 -->
                                                    <asp:Panel ID="pnl_Academic_Details" runat="server" Visible="false" Width="100%">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center">
                                                        <legend><strong>Academic Documents</strong></legend>&nbsp;&nbsp;
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="text-align: right; width: 175px; height: 19px;">
                                                                        Certificate Name:</td>
                                                                    <td style="text-align: left; padding-left: 5px; height: 19px;">
                                                                        <asp:TextBox ID="txt_CertificateType_Academic" runat="server" MaxLength="49" CssClass="input_box" TabIndex="1" Width="160px"></asp:TextBox></td>
                                                                    <td style="text-align: right; width: 141px; height: 19px;">
                                                                        Institute:</td>
                                                                    <td style="text-align: left; padding-left: 5px; width: 188px; height: 19px;">
                                                                        <asp:TextBox ID="txt_Institute_Academic" runat="server" MaxLength="49" CssClass="input_box" TabIndex="2" Width="160px"></asp:TextBox></td>
                                                                    <td rowspan="8" style="text-align: center; vertical-align: middle; width: 142px;" valign="middle">
                                                                        <asp:Image ID="img_Academic" style="cursor:hand" ToolTip="Click to Preview" runat="server"
                                                                        Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Visible="False" /><br />
                                                                        
                                                                        
                                                                           <div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="upld_Image_Academic" size="1" runat="server" style="left:-2px; position:relative; border:0px solid; background-color:#f9f9f9" Width="83px" />                                            
                                                                                                </div> 
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 175px; text-align: right">
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 141px; text-align: right">
                                                                    </td>
                                                                    <td style="width: 188px; text-align: left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right; width: 175px;">
                                                                        Duration From:</td>
                                                                    <td style="text-align: left; padding-left: 5px;">
                                                                        <asp:TextBox ID="txt_DurationFrom_Academic" runat="server" MaxLength="15" Width="143px" CssClass="input_box" TabIndex="3"></asp:TextBox>&nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                    <td style="text-align: right; width: 141px;">
                                                                        Duration To:</td>
                                                                    <td style="text-align: left; width: 188px; padding-left: 5px;">
                                                                        <asp:TextBox ID="txt_DurationTo_Academic" runat="server" MaxLength="15" Width="144px" CssClass="input_box" TabIndex="4"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 175px; text-align: right">
                                                                    </td>
                                                                    <td style="width: 180px; text-align: left">
                                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txt_DurationFrom_Academic" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </td>
                                                                    <td style="width: 141px; text-align: right">
                                                                    </td>
                                                                    <td style="width: 188px; text-align: left">
                                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txt_DurationTo_Academic" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right; width: 175px;">
                                                                        Grade:</td>
                                                                    <td style="text-align: left; padding-left: 5px;">
                                                                        <asp:TextBox ID="txt_Grade_Academic" runat="server" MaxLength="9" CssClass="input_box" TabIndex="5" Width="160px"></asp:TextBox></td>
                                                                    <td style="text-align: right; width: 141px;">
                                                                        </td>
                                                                    <td style="text-align: left; width: 188px;">
                                                                        </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 175px; text-align: right">
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 141px; text-align: right">
                                                                    </td>
                                                                    <td style="width: 188px; text-align: left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="text-align: center"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2"><ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton4"
                                                                                        TargetControlID="txt_DurationFrom_Academic" Format="dd-MMM-yyyy" PopupPosition="TopLeft">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    </td>
                                                                    <td colspan="2"><ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton5"
                                                                                        TargetControlID="txt_DurationTo_Academic" Format="dd-MMM-yyyy" PopupPosition="TopLeft">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                      
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        
                                                    </fieldset></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding-top:15px">
                                                        
                                                        
                                                            <asp:HiddenField ID="Hidden_Academic" runat="server" Value="" />
                                                                        
                                                                        <asp:HiddenField ID="hfd_Image3" runat="server" />
                                                        <asp:Button ID="btn_Add_Academic" runat="server" CssClass="btn" OnClick="btn_Add_Academic_Click"
                                                            Text="Add" Width="59px" TabIndex="6" />
                                                    <asp:Button ID="btn_Save_Academic" runat="server" CssClass="btn" OnClick="btn_Save_Academic_Click"
                                                            Text="Save" Width="59px" TabIndex="7" />
                                                    <asp:Button ID="btn_Cancel_Academic" runat="server" CssClass="btn" OnClick="btn_Cancel_Academic_Click"
                                                            Text="Cancel" Width="59px" TabIndex="8" />
                                                    <asp:Button ID="btn_Print_Academic" runat="server" CausesValidation="False" CssClass="btn"
                                                         TabIndex="22" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab4" runat="server">
                                                        <table cellpadding="0" cellspacing="0" style=" width :100%"> 
                                                     <tr><td style="text-align:center;">                                        <asp:Label ID="lbl_medical_message" runat="server" ForeColor="Red" Text="Label" Visible="False" Font-Bold="true"  ></asp:Label>
                                                        <asp:HiddenField
                                            ID="Hidden_Medical" runat="server" /></td></tr>
                                                         <%--      <tr>
                                                    <td style=" text-align: left">
                                                        <asp:RadioButtonList ID="RadioButtonList3" Font-Bold="true" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="2">Medical Certificate</asp:ListItem>
                                                            <asp:ListItem Value="6">P&amp;I Medical Case History</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                            </tr>--%>
                                                        </table>
                                                    <%--</div>--%>
                                                    <br />
                                        <asp:Panel ID="pnl_Medical_1" runat="server" Height="100%" Width="100%">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center">
                                            <tr>
                                                <td style="text-align: left; ">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                                                        <legend><strong>Medical Document</strong></legend>
                                                   <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                     <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                    <asp:GridView ID="GridView_Medical" Width="98%" OnRowDataBound="GridView_Medical_DataBound" runat="server" OnSelectedIndexChanged="GridView_Medical_SelectedIndexChanged" meta:resourcekey="GridView_MedicalResource1" AutoGenerateColumns="False" DataKeyNames="MedicalDetailsId" OnRowDeleting="GridView_Medical_Row_Deleting" OnPreRender="GridView_Medical_PreRender"  GridLines="horizontal" OnRowCancelingEdit="GridView_Medical_RowCancelingEdit" OnRowCommand="GridView_Medical_RowCommand">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" ><ItemStyle Width="25px" /></asp:CommandField>
                                                           <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ><ItemStyle Width="25px" /></asp:CommandField>--%>
                                                             <asp:TemplateField HeaderText="Edit">
                                                                                        <ItemStyle Width="25px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImgbtnMedicalEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="25px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Document Name">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldocumentname" runat="server" Text='<%#Eval("DocumentName")%>'></asp:Label>
                                                                    <asp:HiddenField ID="HiddenId" runat="server" Value='<%#Eval("MedicalDetailsId")%>' />
                                                                    <asp:HiddenField ID="Hiddenfd1" runat ="server" Value='<%#Eval("ImagePath") %>' />
                                                                    <asp:HiddenField ID="hiddenMvalue" runat="server" Value='<%# Eval("AValue") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="DocumentName" HeaderText="Document Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>--%>
                                                           
                                                            <asp:BoundField DataField="DocumentNumber" HeaderText="Document #">
                                                                <ItemStyle HorizontalAlign="Left" />
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
                                                            <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" Visible="False">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='4' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("MedicalDetailsId")%>' />

                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("DocumentName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("DocumentNumber")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("IssueDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" CausesValidation="false" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>
                                                        </Columns>
                                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                        <PagerStyle CssClass="pagerstyle" />
                                                        <RowStyle CssClass="rowstyle" />
                                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                        
                                                    </asp:GridView>
                                                    <asp:Label ID="lbl_GridView_Medical" runat="server" Text=""></asp:Label>
                                                    </div>
                                                     </td></tr></table>        
                                                  </fieldset>
                                                  <br />
                                                    <asp:Panel ID="pnl_Medical_Details" runat="server"  Width="100%" Visible="false">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right">
                                                        <legend><strong>Medical Document</strong></legend>
                                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="text-align: right; width: 120px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td style="visibility: hidden;">
                                                                </td>
                                                                <td style="visibility: hidden; text-align: left; width: 161px;">
                                                                </td>
                                                                <td rowspan="9" style="width: 167px; text-align: center; vertical-align: middle;">
                                                                    &nbsp;<asp:Image ID="img_Medical" style="cursor:hand" ToolTip="Click to Preview" runat="server" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Visible="False" />
                                                                    
                                                                      <div style="border:0px solid; overflow:hidden; width:75px">
                                                                                                <asp:FileUpload ID="FileUpload_Medical" size="1" runat="server" style="left:-1px; position:relative; border:0px solid; background-color:#f9f9f9" Width="82px" />                                            
                                                                                                </div> 
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 120px;">
                                                                    Document Name:</td>
                                                                <td colspan="2" style="text-align: left">
                                                                    &nbsp;<asp:DropDownList ID="ddl_MedicalDocuments" runat="server" CssClass="required_box"
                                                                        Width="350px">
                                                                    </asp:DropDownList></td>
                                                                <td style="text-align: left; padding-left: 5px; width: 161px; visibility: hidden;">
                                                                    <asp:TextBox ID="txt_DocumentName" runat="server" CssClass="input_box" MaxLength="29"
                                                                        TabIndex="2" Width="35px"></asp:TextBox>
                                                                    <asp:TextBox ID="txt_BloodGroup" runat="server" CssClass="input_box" MaxLength="14"
                                                                        TabIndex="2" Width="18px" Visible="False"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 109px; text-align: right">
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:CompareValidator ID="CompareValidator21" runat="server" ControlToValidate="ddl_MedicalDocuments"
                                                                        ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                <td>
                                                                </td>
                                                                <td style="padding-left: 5px; width: 161px; text-align: left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=" text-align: right; width: 109px;">
                                                                    Document #:</td>
                                                                <td style=" text-align: left; padding-left: 5px;">
                                                                    <asp:TextBox ID="txt_Medical_DocumentNumber" runat="server" CssClass="input_box"
                                                                        MaxLength="19" Width="160px" TabIndex="2"></asp:TextBox></td>
                                                                <td>
                                                                    Place Of Issue:</td>
                                                                <td style=" text-align: left; padding-left: 5px; width: 161px;">
                                                                    <asp:TextBox ID="txt_Medical_PlaceOfIssue" runat="server" CssClass="input_box" MaxLength="29"
                                                                        Width="160px" TabIndex="3"></asp:TextBox></td>
                                                                    
                                                                    
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 109px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td>
                                                                </td>
                                                                <td style="text-align: left; width: 161px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=" text-align: right; width: 109px;">
                                                                    Issue Date:</td>
                                                                <td style=" text-align: left; padding-left: 5px;">
                                                                    <asp:TextBox ID="txt_Medical_IssueDate" runat="server" CssClass="required_box" Width="140px" TabIndex="4"></asp:TextBox>
                                                                    <asp:ImageButton ID="img_Medical_IssueDate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /></td>
                                                                <td>
                                                                    Expiry Date:</td>
                                                                <td style=" text-align: left; padding-left: 5px; width: 161px;">
                                                                    <asp:TextBox ID="txt_Medical_ExpiryDate" runat="server" CssClass="input_box" Width="140px" TabIndex="5"></asp:TextBox>
                                                                    <asp:ImageButton ID="img_expirydate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 109px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txt_Medical_IssueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txt_Medical_IssueDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="text-align: left; width: 161px;">
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txt_Medical_ExpiryDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                               </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="text-align: center">
                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="img_Medical_IssueDate" TargetControlID="txt_Medical_IssueDate" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="img_expirydate" TargetControlID="txt_Medical_ExpiryDate" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                        </fieldset></td></tr></table>
                                                    </asp:Panel>
                                                   
                                                    </td>
                                            </tr>
                                            <tr><td style=" text-align:right; width: 839px;"> 
                                                                    <asp:HiddenField ID="hfd_Image4" runat="server" />
                                            <asp:Button ID="btn_Medical_Add" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btn_Medical_Add_Click" Visible="False" TabIndex="7" />
                                            <asp:Button ID="btn_Medical_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Medical_Save_Click1" Visible="False" TabIndex="8" />
                                            <asp:Button ID="btn_Medical_Cancel" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Medical_Cancel_Click" TabIndex="9" Text="Cancel" Width="59px" Visible="False" />
                                                <asp:Button ID="btn_Medical_Print" runat="server" CausesValidation="False" CssClass="btn"
                                                     TabIndex="10" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td></tr>
                                        </table>
                                        </asp:Panel>
                                        <%--<asp:Panel ID="pnl_Medical_2" runat="server" Height="100%" Width="100%" >
                                            <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;
                                                text-align: center" width="100%">
                                                <tr>
                                                    <td>
                                                        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>P&amp;I History</strong></legend>
                                                            <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                             <div style=" width:100%; height:180px; overflow-x:hidden; overflow-y:scroll" > 
                                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                <asp:GridView ID="gvMedical" runat="server" AutoGenerateColumns="False" DataKeyNames="MedicalCaseId"
                                                                    OnPreRender="gvMedical_PreRender" OnRowDataBound="gvMedical_RowDataBound" OnRowDeleting="gvMedical_RowDeleting"
                                                                    OnRowEditing="gvMedical_RowEditing" OnSelectedIndexChanged="gvMedical_SelectedIndexChanged"
                                                                    Width="98%"  GridLines="horizontal">
                                                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                                     <RowStyle CssClass="rowstyle" />
                                                                    <PagerStyle CssClass="pagerstyle" />
                                                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                    <Columns>
                                                                        <asp:CommandField ButtonType="Image" HeaderText="View"
                                                                            SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" >
                                                                            <HeaderStyle Width="30px" />
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:CommandField>
                                                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                            ShowEditButton="True">
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:CommandField>
                                                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                                            <ItemStyle Width="30px" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                    ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                    Text="Delete" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Case Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("CaseDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    
                                                                        <asp:TemplateField HeaderText="Vessel Name">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="HvesselId" runat="server" Value='<%#Eval("VesselId")%>' />
                                                                                <asp:Label ID="lblvessel" runat="server" Text='<%#Eval("VesselName") %>'></asp:Label>
                                                                                 
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Port Name">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField ID="Hportid" runat="server" Value='<%#Eval("PortId")%>' />
                                                                                <asp:Label ID="lblport" runat="server" Text='<%#Eval("portName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="CaseNumber" HeaderText="Case #">
                                                                            <ItemStyle HorizontalAlign="Left"/>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="CaseStatus" HeaderText="Case Status">
                                                                            <ItemStyle HorizontalAlign="Left" Width="80px"/>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount">
                                                                            <ItemStyle HorizontalAlign="Right" Width="70px"/>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Description" HeaderText="Description">
                                                                            <ItemStyle HorizontalAlign="Left"/>
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            </td></tr></table>
                                                        </fieldset>
                                                        <br />
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                        <fieldset id="trfields" runat="server" style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                            <legend><strong>P&amp;I History</strong></legend>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                                padding-top: 5px; height: auto; text-align: center" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0" style="height: 37px; width: 100%;">
                                                                            <tr>
                                                                                <td align="right" style="text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    &nbsp;</td>
                                                                                <td align="right" style="text-align: right; width: 110px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="text-align: right">
                                                                                    Case Date:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:TextBox ID="txtmedicalcasedate" runat="server" CssClass="required_box" MaxLength="15" Width="143px" TabIndex="1"></asp:TextBox>
                                                                                    <asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                                    &nbsp;&nbsp;
                                                                                </td>
                                                                                <td align="right" style="text-align: right; width: 110px;">
                                                                                    Vessel Name:</td>
                                                                                <td colspan="3" style="padding-left: 5px; text-align: left">
                                                                                    <asp:DropDownList ID="ddmedicalvessel" runat="server" CssClass="input_box" Width="249px" TabIndex="2">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtmedicalcasedate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtmedicalcasedate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                </td>  
                                                                                <td align="right" style="color: #0e64a0; text-align: right; width: 110px;">
                                                                                    &nbsp;</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="text-align: right">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 16%">
                                                                                    Country:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box"
                                                                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="165px">
                                                                                    </asp:DropDownList></td>
                                                                                <td align="right" style="text-align: right; width: 110px;">
                                                                                    Port Name:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:DropDownList ID="ddmedicalport" runat="server" CssClass="input_box" Width="165px" TabIndex="3">
                                                                                    </asp:DropDownList></td>
                                                                                <td align="left" style="width: 16%">
                                                                                    <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                                                                                        OnClientClick="return openpage();" /></td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 16%; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left; height: 13px;">
                                                                                    <asp:CompareValidator ID="CompareValidator20" runat="server" ControlToValidate="ddlCountry"
                                                                                        ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                                <td align="right" style="text-align: right; height: 13px; width: 110px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left; height: 13px;">
                                                                                </td>
                                                                                <td align="right" style="width: 16%; height: 13px;">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left; height: 13px;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 16%">
                                                                                    Case #:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:TextBox ID="txtmedicalcasenumber" runat="server" CssClass="required_box" MaxLength="255"
                                                                                        Width="160px" TabIndex="4"></asp:TextBox>
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td align="right" style=" text-align: right; width: 110px;">
                                                                                    Case Status:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:DropDownList ID="ddmedicalstatus" runat="server" CssClass="input_box" Width="165px" TabIndex="5">
                                                                                        <asp:ListItem Value="O">Open</asp:ListItem>
                                                                                        <asp:ListItem Value="C">Close</asp:ListItem>
                                                                                    </asp:DropDownList></td>
                                                                                <td align="right" style="width: 16%">
                                                                                    Amount:</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:TextBox ID="txtmedicalamount" runat="server" CssClass="required_box" MaxLength="8"
                                                                                        Width="160px" TabIndex="6"></asp:TextBox>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="width: 16%">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmedicalcasenumber"
                                                                                        ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                                                                <td align="right" style="text-align: right; width: 110px;">
                                                                                    &nbsp;</td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                </td>
                                                                                <td align="right" style="width: 16%">
                                                                                </td>
                                                                                <td style="padding-left: 5px; text-align: left">
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtmedicalamount"
                                                                                        ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="text-align: right">
                                                                                    Description:</td>
                                                                                <td colspan="5" style="padding-left: 5px; text-align: left">
                                                                                    <asp:TextBox ID="txtmedicaldescription" runat="server" CssClass="input_box" Height="40px"
                                                                                        TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" style="text-align: right">
                                                                                </td>
                                                                                <td colspan="5" style="padding-left: 5px; text-align: left">
                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton6" TargetControlID="txtmedicalcasedate" PopupPosition="TopLeft">
                                                                                        </ajaxToolkit:CalendarExtender>
                                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars" FilterType="Custom" ValidChars="0123456789." 
                                                                                         TargetControlID="txtmedicalamount">
                                                                                    </ajaxToolkit:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset></td></tr></table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right" style="text-align: right">
                                                                    <asp:HiddenField ID="hmedicalid" runat="server" />
                                                                    <asp:Button ID="btn_PCI_Add" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_PCI_Add_Click" Text="Add" Width="59px" TabIndex="7" />
                                                                    <asp:Button ID="btn_PCI_Save" runat="server" CssClass="btn" OnClick="btn_PCI_Save_Click" Text="Save" Width="59px" TabIndex="8" />
                                                                    <asp:Button ID="btn_PCI_Cancel" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_PCI_Cancel_Click" Text="Cancel" Width="59px" TabIndex="9" />
                                                                    <asp:Button ID="btn_PCI_Print" runat="server" CausesValidation="False" CssClass="btn" TabIndex="10" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>--%>
                                    </asp:View>
                                    <asp:View id="Tab5" runat="server">
                                                                          <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center">
                                            <tr>
                                                <td style="text-align: left; ">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                                                        <legend><strong>Archive Documents</strong></legend>
                                                   <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">    
                                    <br />
                                    <div style=" height:23px; overflow-x: hidden; overflow-y:scroll; border:solid 1px gray; background-color:#c2c2c2; font-weight:bold;" >
                                    <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="white" >
                                    <colgroup >
                                        <col width="100px" />
                                        <col width="150px" />
                                        <col width="100px"/>
                                        <col width="150px" />
                                        <col width="80px" />
                                        <col width="80px" />
                                        <col width="150px" />
                                        <col width="40px" />
                                    </colgroup>
                                    <tr class="headerstylegrid">
                                        <td style="height:18px">&nbsp;Doc. Category</td>
                                        <td>&nbsp;Document Type</td>
                                        <td>&nbsp;Document Name</td>
                                        <td>&nbsp;Document #</td>
                                        <td style="text-align:center">Issue Dt.</td>
                                        <td style="text-align:center">Exp Dt.</td>
                                        <td style="text-align:center">File Name</td>
                                        <td style="text-align:left">&nbsp;<asp:Image runat="server" ID="btnimg" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" /></td>
                                    </tr>
                                    </table>
                                    </div>
                                    <div style=" height:400px; overflow-x: hidden; overflow-y:scroll; border:solid 1px gray" >
                                    <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="white" >
                                    <colgroup >
                                        <col width="100px" />
                                        <col width="150px" />
                                        <col width="100px"/>
                                        <col width="150px" />
                                        <col width="80px" />
                                        <col width="80px" />
                                        <col width="150px" />
                                        <col width="40px" />
                                    </colgroup>
                                    <asp:Repeater runat="server" ID="grdDocs">
                                    <ItemTemplate>
                                    <tr>
                                        <td style="height:18px;text-align:left;width=100px;">&nbsp;<%# Eval("MainCategory") %></td>
                                        <td style="text-align:left;width=150px;">&nbsp;<%# Eval("SubCategory") %></td>
                                        <td style="text-align:left;width=100px;">&nbsp;<%# Eval("DocumentName") %></td>
                                        <td style="text-align:left;width=150px;">&nbsp;<%# Eval("DocumentNumber") %></td>
                                        <td style="text-align:left;width=80px;"><%# Common.ToDateString(Eval("IssueDate")) %></td>
                                        <td style="text-align:left;width=80px;"><%# Common.ToDateString(Eval("ExpiryDate")) %></td>
                                        <td style="text-align:left;width=150px;"><%# Eval("FileName") %></td>
                                        <td style="text-align:left">&nbsp;<asp:ImageButton runat="server" ID="btnDownload" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" CommandArgument='<%# Eval("TableId") %>' OnClick="btnDownload_Click" /> </td>
                                    </tr>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                    </div>
                                                       </td></tr></table></fieldset></td>
                                                </tr>
                                                                              </table>
                                    </asp:View>
                                    <asp:View ID="Tab6" runat="server">
                                       <table border="0" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9;
                                            text-align: center; width: 100%;" >
                                            <tr>
                                                <td style="height:auto" valign="top">
                                                    
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                        <legend><strong>Appraisal&nbsp; Details </strong></legend>
                                                        <asp:Label ID="lbl_apprasial_message" runat="server"></asp:Label>
                                                        <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" > 
                                                            <asp:GridView ID="gvApprasial" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewAppraisalId"
                                                                OnPreRender="gvApprasial_PreRender" OnRowDataBound="gvApprasial_RowDataBound"
                                                                OnRowDeleting="gvApprasial_RowDeleting" 
                                                                OnSelectedIndexChanged="gvApprasial_SelectedIndexChanged" Width="98%" GridLines="Horizontal" OnRowCancelingEdit="gvApprasial_RowCancelingEdit" OnRowCommand="gvApprasial_RowCommand">
                                                                <RowStyle CssClass="rowstyle" />
                                                                <PagerStyle CssClass="pagerstyle" />
                                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                                                        ShowSelectButton="True"><HeaderStyle Width="30px" /><ItemStyle Width="30px" /></asp:CommandField>
                                                                        
                                                                   <%-- <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                                                        ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>--%>
                                                                     <asp:TemplateField HeaderText="Edit">
                                                            <ItemStyle Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgbtngvApprasialEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="30px" /><ItemTemplate><asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                                                                Text="Delete" /></ItemTemplate></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=""><ItemTemplate><asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="30px" /></asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Appraisal Occasion"><ItemTemplate><asp:HiddenField ID="HdocId" runat="server" Value='<%#Eval("AppraisalOccasionId")%>' /><asp:Label ID="lbldocument" runat="server" Text='<%#Eval("ApprasialOccasion") %>' Width="250px" ></asp:Label><asp:HiddenField ID="himag" runat="server" Value='<%#Eval("ImagePath") %>' /></ItemTemplate><ItemStyle HorizontalAlign="Left" Width="70px" /><HeaderStyle HorizontalAlign="Left" Width="200px" /></asp:TemplateField>
                                                                    <asp:BoundField Visible="False" DataField="AvgMarks" HeaderText="Avg. Marks"><ItemStyle HorizontalAlign="Right" Width="110px" /></asp:BoundField>
                                                                    <%----------------------%>
                                                                    <asp:BoundField DataField="FromDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="Appraisal From" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                    <asp:BoundField DataField="ToDate" DataFormatString="{0:dd/MMM/yyyy}" HtmlEncode="false" HeaderText="Appraisal To" ><ItemStyle HorizontalAlign="Center" Width="100px" /></asp:BoundField>
                                                                    
                                                                    
                                                                    <asp:BoundField DataField="VesselName" HeaderText="Vessel"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                                                                    <%--<asp:BoundField DataField="N_RecommendedNew" HeaderText="Recommended"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>--%>
                                                                    <asp:BoundField Visible="False" DataField="AppraiserRemarks" HeaderText="Appraiser Remarks"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                     <asp:BoundField Visible="False" DataField="AppraiseeRemarks" HeaderText="Appraisee Remarks"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                                                                    <%--<asp:BoundField DataField="OfficeRemarks" HeaderText="Office Remark"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>--%>
                                                                </Columns>
                                                                <HeaderStyle CssClass="headerstylefixedheadergrid" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                    <asp:Panel ID="trapprasial" runat="server" Width="100%">
                                                    <br />
                                                    <fieldset style="border-right: #8fafdb 1px solid;border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                                        <legend><strong>Appraisal Details </strong></legend>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                            padding-top: 5px; height: auto; text-align: center" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 120px; height: 1px; text-align: right;padding-left: 5px;">
                                                                                Occasion:</td>
                                                                            <td style="padding-left: 5px; width: 186px; height: 1px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_apprasial_occasion" runat="server" CssClass="required_box"
                                                                                    Width="153px" TabIndex="1">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td  style="width: 157px; height: 1px; text-align: right">
                                                                                Period From:&nbsp;
                                                                                <%--Average Score:--%></td>
                                                                            <td style="padding-left: 5px; width: 186px; height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_apprasial_from" runat="server" CssClass="required_box" MaxLength="20"
                                                                                    Width="90px" TabIndex="3"></asp:TextBox>
                                                                                <asp:ImageButton ID="img_apprasial_issue" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                            </td>
                                                                            <td style="padding-left: 5px; width: 186px; height: 1px; text-align: right">
                                                                                Period &nbsp;To:</td>
                                                                            <td style="padding-left: 5px;width: 157px;  height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_To" runat="server" CssClass="required_box" MaxLength="20"
                                                                                    Width="90px" TabIndex="4"></asp:TextBox>
                                                                                <asp:ImageButton ID="img_apprasial_expiry" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                                &nbsp;&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_apprasial_occasion"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                            <td align="right" style="width: 17%; height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txt_apprasial_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txt_apprasial_from" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                         </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txt_Apprasial_To" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_Apprasial_To" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                                                                                         </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="height: 1px; text-align: right">
                                                                                Perf. Score:</td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_PerfScore" runat="server" CssClass="input_box" MaxLength="49"
                                                                                    Width="102px" TabIndex="2"></asp:TextBox><asp:TextBox ID="txt_Apprasial_Score" runat="server" CssClass="required_box" MaxLength="5"
                                                                                    Width="102px" TabIndex="2" style="display: none"></asp:TextBox></td>
                                                                            <td align="right" style=" height: 1px; text-align: right">
                                                                                Comp. Score :</td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                <asp:TextBox ID="txt_CompScore" runat="server" CssClass="input_box" MaxLength="49" TabIndex="5"
                                                                                    Width="102px"></asp:TextBox></td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: right">
                                                                                Vessel :</td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box"
                                                                                    Width="153px" TabIndex="5">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                                <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Apprasial_Score"
                                                                                    ErrorMessage="Required." Width="55px"></asp:RequiredFieldValidator></td>
                                                                            <td align="right" style=" height: 1px; text-align: right">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 1px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 19px; text-align: right">
                                                                                Promo. Recommended :</td>
                                                                            <td rowspan="1" style="padding-left: 5px;  height: 19px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_PromoRecomm" runat="server" CssClass="input_box" TabIndex="5" Width="107px">
                                                                                    <asp:ListItem Value="">< Select ></asp:ListItem>
                                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                    <asp:ListItem Value="L">Latter</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:CheckBox ID="chk_Recommended" runat="server" style="display: none" TabIndex="6"/></td>
                                                                            <td align="right" style=" height: 19px">
                                                                                Report# :</td>
                                                                            <td style="padding-left: 5px;  height: 19px; text-align: left">
                                                                                <asp:TextBox ID="txt_ReportNo" runat="server" CssClass="input_box" MaxLength="49" TabIndex="5"
                                                                                    Width="102px"></asp:TextBox></td>
                                                                            <td style="padding-left: 5px;  height: 19px; text-align: left">
                                                                            </td>
                                                                            <td rowspan="5" style="padding-left: 5px;  text-align: center;">
                                                                                <asp:Image ID="img_Apprasial" style="cursor:hand" ToolTip="Click to Preview" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" CausesValidation="false" runat="server" Height="108px" Width="100px" Visible="False" /><div style="border:0px solid; overflow:hidden; width:75px">
                                                                                    <br />
                                                                                                <asp:FileUpload ID="Apprasial_fileupload" size="1" runat="server" style="left:-2px; position:relative; border:0px solid;" BackColor="#F9F9F9" Width="81px" />                                            
                                                                                                </div> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                            </td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                &nbsp;</td>
                                                                            <td align="right" style=" height: 3px">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="vertical-align: top;  height: 3px; text-align: right">
                                                                                Office Remarks:</td>
                                                                            <td colspan="4" rowspan="1" style="padding-left: 5px; height: 3px; text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_OfficeRemarks" runat="server" CssClass="input_box"
                                                                                    MaxLength="50" Rows="2" TextMode="MultiLine" Width="450px" TabIndex="9" Height="84px"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                            </td>
                                                                            <td colspan="4" rowspan="1" style="padding-left: 5px; height: 3px; text-align: left">
                                                                </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                                Training Required :</td>
                                                                            <td rowspan="1" style="padding-left: 5px; width: 17%; height: 3px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_TrainingReq" runat="server" TabIndex="5" CssClass="input_box" Width="107px">
                                                                                <asp:ListItem Value="">< Select ></asp:ListItem>
                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                            <td align="right" style=" height: 3px">
                                                                                Date Join Company :</td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_DJC" runat="server"></asp:Label></td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                                </td>
                                                                            <td rowspan="1" style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                &nbsp;</td>
                                                                            <td align="right" style=" height: 3px">
                                                                                </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                                Updated By :</td>
                                                                            <td rowspan="1" style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_UpdatedBy" runat="server"></asp:Label></td>
                                                                            <td align="right" style="width: 17%; height: 3px">
                                                                                Updated On :</td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                <asp:Label ID="lbl_UpdatedOn" runat="server"></asp:Label></td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                <asp:Button ID="btn_ShowTrainings" Visible="false" runat="server" OnClick="btn_ShowTrainings_Click" Text="View Trainings" Width="100px" CssClass="btn" CausesValidation="False" /></td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td align="right" style=" height: 3px; text-align: right">
                                                                            </td>
                                                                            <td rowspan="1" style="padding-left: 5px;  height: 3px; text-align: left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="right" style=" height: 3px">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                            <td style="padding-left: 5px;  height: 3px; text-align: left">
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td align="right" style="">
                                                                                <%--Appraisier Remarks:&nbsp;--%></td>
                                                                            <td colspan="3" style="padding-left: 5px;  text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasial_remarks" runat="server" CssClass="input_box" MaxLength="50"
                                                                                    Rows="2" TextMode="MultiLine" Width="450px" TabIndex="7" style="display: none"></asp:TextBox></td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                       <%-- <tr>
                                                                            <td align="right" style="">
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td align="right" style="">
                                                                                <%--Appraisee's Remark:--%></td>
                                                                            <td colspan="3" style="padding-left: 5px;  text-align: left">
                                                                                <asp:TextBox ID="txt_Apprasiee_remarks" runat="server" CssClass="input_box" MaxLength="50"
                                                                                    Rows="2" TextMode="MultiLine" Width="450px" TabIndex="8" style="display: none"></asp:TextBox></td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                       <%-- <tr>
                                                                            <td align="right" style="">
                                                                            </td>
                                                                            <td colspan="3" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                            <td colspan="1" style="padding-left: 5px;  text-align: left">
                                                                            </td>
                                                                        </tr>--%>
                                                                    </table>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>      
                                                      <div style=" width:100%; height:250px; overflow-y: scroll; overflow-x: hidden;" >
                                                       <asp:GridView ID="gvTrainingsWithAppraisal" runat="server" AutoGenerateColumns="False" Width="98%" GridLines="horizontal">
                                                        <RowStyle CssClass="rowstyle" />
                                                        <PagerStyle CssClass="pagerstyle" />
                                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                        <Columns>
                                                        <asp:BoundField DataField="N_TrainingTypeName" HeaderText="Training Type"><ItemStyle HorizontalAlign="Left" Width="300px" /></asp:BoundField>
                                                        <asp:BoundField DataField="TrainingName" HeaderText="Training Name"><ItemStyle HorizontalAlign="Left" Width="300px" /></asp:BoundField>
                                                        <asp:BoundField Visible="False" DataField="TrainingCost" HeaderText="Investment ($)"><ItemStyle HorizontalAlign="Right" Width="110px" /></asp:BoundField>
                                                         <asp:TemplateField HeaderText="Due Date"><ItemTemplate></ItemTemplate></asp:TemplateField>
                                                        <asp:BoundField DataField="Location"  HeaderText="Rec. Office" ><ItemStyle Width="100px" /></asp:BoundField>
                                                        <asp:BoundField DataField="Attended" HeaderText="Attended"><ItemStyle HorizontalAlign="Left" Width="80px" /></asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Left" CssClass="headerstylefixedheadergrid" />
                                                    </asp:GridView>
                                                    </div>
                                                    </asp:Panel>
                                                    <table style="background-color: #f9f9f9" width="100%">
                                                        <tr>
                                                            <td id="Td4" runat="server" align="right" style="height: auto;padding-top:14px;
                                                                text-align: right" valign="top">
                                                                                
                                                                               
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy"
                                                                                    PopupButtonID="img_apprasial_issue" PopupPosition="TopLeft" TargetControlID="txt_apprasial_from"></ajaxToolkit:CalendarExtender>
                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                                    FilterType="Numbers,Custom" TargetControlID="txt_Apprasial_Score" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" Format="dd-MMM-yyyy"
                                                                                    PopupButtonID="img_apprasial_expiry" PopupPosition="TopLeft" TargetControlID="txt_Apprasial_To"></ajaxToolkit:CalendarExtender>
                                                                <asp:HiddenField ID="h_apprasial_hfile" runat="server" />
                                                                <asp:HiddenField ID="h_apprasial_id" runat="server" />
                                                                &nbsp; &nbsp;
                                                                &nbsp; &nbsp;<asp:Button ID="btn_Apprasial_Add" runat="server" CausesValidation="False"
                                                                    CssClass="btn" OnClick="btn_Apprasial_Add_Click" Text="Add" Width="59px" TabIndex="10" />
                                                                <asp:Button ID="btn_Apprasial_Save" runat="server" CssClass="btn" OnClick="btn_Apprasial_Save_Click"
                                                                    Text="Save" Width="59px" TabIndex="11" />
                                                                <asp:Button ID="btn_Apprasial_Cancel" runat="server" CausesValidation="False" CssClass="btn"
                                                                    OnClick="btn_Apprasial_Cancel_Click" Text="Cancel" Width="59px" TabIndex="12" />
                                                                <asp:Button ID="btn_Apprasial_Print" runat="server" CausesValidation="False" CssClass="btn" 
                                                                    OnClientClick="javascript:CallPrint('divPrint');" TabIndex="13" Text="Print" Width="59px" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="Tab7" runat="server">
                                                        <table cellpadding="0" cellspacing="0" style=" width :100%"> 
                                                     <tr><td style="text-align:center;">                                        
                                                         <asp:Label ID="lblTrainingMsg" runat="server" ForeColor="Red" Text="Label" Visible="False" Font-Bold="true"  ></asp:Label>
                                                        <asp:HiddenField ID="Hidden_Training" runat="server" /></td></tr>
                                                         <%--      <tr>
                                                    <td style=" text-align: left">
                                                        <asp:RadioButtonList ID="RadioButtonList3" Font-Bold="true" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="2">Medical Certificate</asp:ListItem>
                                                            <asp:ListItem Value="6">P&amp;I Medical Case History</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                            </tr>--%>
                                                        </table>
                                                    <%--</div>--%>
                                                    <br />
                                        <asp:Panel ID="pnl_Training_1" runat="server" Height="100%" Width="100%">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center">
                                            <tr>
                                                <td style="text-align: left; ">
                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                                                        <legend><strong>Training Records</strong></legend>
                                                   <table cellpadding="0" cellspacing="0" width="100%"><tr><td style=" padding-top:5px;">
                                                     <div style=" width:100%; height:250px; overflow-x:hidden; overflow-y:scroll" > 
                                                    <asp:GridView ID="Gv_Training" Width="98%" OnRowDataBound="Gv_Training_DataBound" runat="server" OnSelectedIndexChanged="Gv_Training_SelectedIndexChanged" meta:resourcekey="Gv_TrainingResource1" AutoGenerateColumns="False" DataKeyNames="TrainingDocumentId" OnRowDeleting="Gv_Training_Row_Deleting" OnPreRender="Gv_Training_PreRender"  GridLines="horizontal" OnRowCancelingEdit="Gv_Training_RowCancelingEdit" OnRowCommand="Gv_Training_RowCommand">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" ><ItemStyle Width="25px" /></asp:CommandField>
                                                           <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ><ItemStyle Width="25px" /></asp:CommandField>--%>
                                                             <asp:TemplateField HeaderText="Edit">
                                                                                        <ItemStyle Width="25px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImgbtnTrainginEdit" runat="server" CausesValidation="False" CommandName="Modify"
                                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" Text="Edit" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="25px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgbtnTrainingDelete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="Trainng Name">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltrainingname" runat="server" Text='<%#Eval("TrainingName")%>'></asp:Label>
                                                                    <asp:HiddenField ID="hdnTrainingDocid" runat="server" Value='<%#Eval("TrainingDocumentId")%>' />
                                                                    <asp:HiddenField ID="hdnDocPath" runat ="server" Value='<%#Eval("ImagePath") %>' />
                                                                    <asp:HiddenField ID="hdnTrainginExpired" runat="server" Value='<%# Eval("AValue") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="DocumentName" HeaderText="Document Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>--%>
                                                           
                                                            <asp:BoundField DataField="InstituteName" HeaderText="Institute Name">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                             <asp:TemplateField HeaderText="From Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("FromDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="To Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("ToDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Expiry Date">
                                                                    <ItemTemplate>
                                                                    <%# Alerts.FormatDate(Eval("ExpiryDate"))%>
                                                                    </ItemTemplate> 
                                                                    </asp:TemplateField>
                                                           <%-- <asp:BoundField DataField="PlaceOfIssue" HeaderText="Place Of Issue">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BloodGroup" HeaderText="Blood Group" Visible="False">
                                                                <ItemStyle HorizontalAlign="Left"/>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Archive">
                                                                                    <ItemTemplate>

                                                                                      <asp:HiddenField ID="arc_DocType" runat="server" Value='4' />
                                                                                      <asp:HiddenField ID="arc_DocumentID" runat="server" Value='<%# Eval("MedicalDetailsId")%>' />

                                                                                      <asp:HiddenField ID="arc_DocumentName" runat="server" Value='<%#Eval("DocumentName")%>' />
                                                                                      <asp:HiddenField ID="arc_DocumentNumber" runat="server" Value='<%#Eval("DocumentNumber")%>' />
                                                                                      <asp:HiddenField ID="arc_IssueDate" runat ="server" Value='<%# Alerts.FormatDate(Eval("IssueDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_ExpDate" runat="server" Value='<%# Alerts.FormatDate(Eval("ExpiryDate"))%>' />
                                                                                      <asp:HiddenField ID="arc_FilePath" runat="server" Value='<%# Eval("ImagePath")%>' />

                                                                                      <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/archive.png" OnClick="ArchiveOthers" CausesValidation="false" />                                                                        
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="45px" />
                                                                        
                                                                                </asp:TemplateField>--%>
                                                        </Columns>
                                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                                        <PagerStyle CssClass="pagerstyle" />
                                                        <RowStyle CssClass="rowstyle" />
                                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                        
                                                    </asp:GridView>
                                                    <asp:Label ID="lbl_Grid_Training" runat="server" Text=""></asp:Label>
                                                    </div>
                                                     </td></tr></table>        
                                                  </fieldset>
                                                  <br />
                                                    <asp:Panel ID="pnl_Training_Details" runat="server"  Width="100%" Visible="false">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                         <tr><td style="padding-bottom:10px">
                                                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right">
                                                        <legend><strong>Training Details</strong></legend>
                                                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                           <%-- <tr>
                                                                <td style="text-align: right; width: 120px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    &nbsp;</td>
                                                                <td >
                                                                </td>
                                                                <td style=" text-align: left; width: 161px;">
                                                                </td>
                                                                <td rowspan="9" style="width: 167px; text-align: center; vertical-align: middle;">
                                                                    &nbsp;
                                                                    
                                                                    
                                                                </td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td style="text-align: right; width: 120px;">
                                                                   Training Name:</td>
                                                                <td  style="text-align: left">
                                                                    <asp:DropDownList ID="ddlTraining" runat="server" CssClass="required_box"
                                                                        Width="300px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td style="text-align: right; padding-right: 5px; width: 161px; ">
                                                                  
                                                                    Institute Name:
                                                                </td>
                                                                 <td  style="text-align: left">
                                                                   <asp:DropDownList ID="ddlInstitute" runat="server" CssClass="required_box"
                                                                        Width="300px" TabIndex="2"> 
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 109px; text-align: right">
                                                                    
                                                                </td>
                                                                <td style="padding-left: 5px; text-align: left">
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlTraining"
                                                                        ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                <td >
                                                                      <asp:TextBox ID="txtTrainingName" runat="server" CssClass="input_box" MaxLength="29"
                                                                        TabIndex="2" Width="35px" Visible="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtInstiteName" runat="server" CssClass="input_box" MaxLength="14"
                                                                        TabIndex="2" Width="18px" Visible="False"></asp:TextBox>
                                                                </td>
                                                                <td style="padding-left: 5px; width: 161px; text-align: left">
                                                                     <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlInstitute"
                                                                        ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td style=" text-align: right; width: 109px;">
                                                                    From Date:</td>
                                                                <td style=" text-align: left; padding-left: 5px;">
                                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="required_box"
                                                                        MaxLength="19" Width="140px" TabIndex="3"></asp:TextBox>   <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /></td>
                                                                <td style="text-align: right; padding-right: 5px; width: 161px; ">
                                                                    To Date:</td>
                                                                <td style=" text-align: left; padding-left: 5px; width: 161px;">
                                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="required_box" MaxLength="29"
                                                                        Width="140px" TabIndex="4"></asp:TextBox><asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /> </td>
                                                                    
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 109px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtFromDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtFromDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  </td>
                                                                <td>
                                                                </td>
                                                                <td style="text-align: left; width: 161px;">
                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtToDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtToDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style=" text-align: right; width: 109px;">
                                                                    Expiry Date:</td>
                                                                <td style=" text-align: left; padding-left: 5px;">
                                                                    <asp:TextBox ID="txtExpiryDt" runat="server" CssClass="required_box" Width="140px" TabIndex="5"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgExpiryDt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  /></td>
                                                                <td style="text-align: right; padding-right: 5px; width: 161px; ">
                                                                    File Upload:
                                                                   </td>
                                                                <td style=" text-align: left; padding-left: 5px; width: 161px;">
                                                                         <div style="border:0px solid; overflow:hidden; width:150px;">
                                                                                                <asp:FileUpload ID="FU_Training" size="1" runat="server" style=" border-style: solid; border-color: inherit; border-width: 0px; background-color:#f9f9f9" />                                            
                                                                                                </div> 
                                                                    <asp:Image ID="Img_Training" style="cursor:hand" ToolTip="Click to Preview" runat="server" Width="60px" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="90px" Visible="False" />
                                                                   </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; width: 109px;">
                                                                </td>
                                                                <td style="text-align: left">
                                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtExpiryDt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                           <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtExpiryDt" ErrorMessage="Required." ></asp:RequiredFieldValidator> --%>       
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="text-align: left; width: 161px;">
                                                                               
                                                               </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="text-align: center">

                                                                    </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="imgFromDate" TargetControlID="txtFromDate" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="imgToDate" TargetControlID="txtToDate" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    &nbsp;</td>
                                                                <td colspan="2" >
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="imgExpiryDt" TargetControlID="txtExpiryDt" PopupPosition="TopLeft">
                                                                        </ajaxToolkit:CalendarExtender>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                        </fieldset></td></tr></table>
                                                    </asp:Panel>
                                                   
                                                    </td>
                                            </tr>
                                            <tr><td style=" text-align:right; width: 839px;"> 
                                                                    <asp:HiddenField ID="hdn_TrainingImgPath" runat="server" />
                                            <asp:Button ID="btnTrainingAdd" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btnTrainingAdd_Click" Visible="False" TabIndex="7" />
                                            <asp:Button ID="btnTrainingSave" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btnTrainingSave_Click" Visible="False" TabIndex="8" />
                                            <asp:Button ID="btnTrainingCancel" runat="server" CausesValidation="False" CssClass="btn" OnClick="btnTrainingCancel_Click" TabIndex="9" Text="Cancel" Width="59px" Visible="False" />
                                                <asp:Button ID="btnTrainingPrint" runat="server" CausesValidation="False" CssClass="btn"
                                                     TabIndex="10" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('divPrint');" /></td></tr>
                                        </table>
                                        </asp:Panel>
                                       
                                    </asp:View>
                                </asp:MultiView></div>
                            </td>
                            <td style="width:25%;">
                                <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                  <asp:Image ID="img_Crew" style="cursor:hand" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="108px" Width="100px" ImageUrl="" CausesValidation="False"  />
                                      
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
                                 <%--<asp:TextBox ID="ddcurrentrank" runat="server"   ReadOnly="True" Visible="false"></asp:TextBox>--%>
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
                                <%-- <asp:TextBox ID="txt_LastVessel" runat="server" MaxLength="24" ReadOnly="True" Visible="false"></asp:TextBox>--%>
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
                           <td ><asp:Button runat="server"  CommandArgument="0" Text="Travel" OnClick="Menu1_MenuItemClick" ID="b1" CssClass="btn1"  Font-Bold="True" Width="100px" /></td>
                            </tr>
                             <tr>
                                <td><asp:Button runat="server"  CommandArgument="1" Text="Professional" OnClick="Menu1_MenuItemClick"  ID="b2"  CssClass="btn1"  Font-Bold="True" Width="100px"/></td>
                                  </tr>
                        <tr>
                                <td><asp:Button runat="server"  CommandArgument="2" Text="Academic" OnClick="Menu1_MenuItemClick" ID="b3"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False"/></td> </tr>
                         <tr>
                                <td><asp:Button runat="server"  CommandArgument="3" Text="Medical" OnClick="Menu1_MenuItemClick" ID="b4"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /></td> 
                         </tr>
                         <tr>
                                <td><asp:Button runat="server"  CommandArgument="6" Text="Training" OnClick="Menu1_MenuItemClick" ID="b9"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" /></td> 
                         </tr>s
                          <tr> 
                              <td>
                                 <asp:Button runat="server"  CommandArgument="4" Text="Archive Docs." OnClick="Menu1_MenuItemClick" ID="b5"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" />
                               </td>
                          </tr>
                          <tr> 
                              <td>
                                  <asp:Button runat="server"  CommandArgument="5" Text="Appraisal" OnClick="Menu1_MenuItemClick" ID="b8"  CssClass="btn1"  Font-Bold="True" Width="100px" CausesValidation="False" />
                               </td>
                          </tr>
                         <tr> 
                              <td>
                                
                               </td>
                          </tr>
                         <tr> 
                              <td>
                                
                               </td>
                          </tr>
                        <%--<tr>
                            <td style="text-align:center;padding-left:20px;">
                                 <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Vertical"
                                                StaticEnableDefaultPopOutImage="False" Width="100px" meta:resourcekey="Menu1Resource1" BorderStyle="Solid"   >
                                                <Items>
                                                    <asp:MenuItem  Text="Personal" Value="0" meta:resourcekey="MenuItemResource1" ></asp:MenuItem>
                                                    <asp:MenuItem  Text="Contact" Value="1" meta:resourcekey="MenuItemResource2"></asp:MenuItem>
                                                    <asp:MenuItem  Text="Family" Value="2" meta:resourcekey="MenuItemResource3"></asp:MenuItem>
                                                    <asp:MenuItem  Text="Experience" Value="3" meta:resourcekey="MenuItemResource4"></asp:MenuItem>
                                                   <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/apphistory_d.gif" Text=" " Value="4" meta:resourcekey="MenuItemResource5"></asp:MenuItem>
                                                    <asp:MenuItem ImageUrl="~/Modules/HRD/Images/Tab/assessment_d.gif" Text=" " Value="5" meta:resourcekey="MenuItemResource5"></asp:MenuItem>
                                                </Items>
                                            </asp:Menu>
                            </td>
                        </tr>--%>
                        
                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
      </td>
        </tr>
      </table>
        </div>
    </asp:Content>
    
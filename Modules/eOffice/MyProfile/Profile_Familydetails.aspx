<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_Familydetails.aspx.cs" Inherits="Emtm_Profile_Familydetails" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master"%>
<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server" >
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
       
        <%--<asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
         <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Personal Details : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                        </div>
                        <div class="dottedscrollbox">
                            <uc3:Profile_PersonalHeaderMenu ID="Emtm_Profile_PersonalHeaderMenu1" runat="server" />
                        </div> 
                        <div style="padding:5px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col />
                                    <col style="width:80px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="text headerband">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Name</td>
                                        <td>DOB</td>
                                        <td>Gender</td>
                                        <td>Relation</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                       </table>           
                       </div>
                        <div id="divfamily" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 403px ; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col />
                                <col style="width:80px;" />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:25px;" />
                            </colgroup>
                            <asp:Repeater ID="rptRunningHour" runat="server" 
                                onitemcreated="rptRunningHour_ItemCreated">
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("FamilyId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("FamilyId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                OnClick="btnView_Click" ToolTip="View" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnedit" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("FamilyId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                OnClick="btnedit_Click" ToolTip="View" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("FamilyId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                OnClick="btnDelete_Click" ToolTip="View"  OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                        </td>
                                        <td align="left">
                                            <%#Eval("Name")%></td>
                                        <td align="left">
                                            <%#Eval("DateofBirth")%></td>
                                        <td align="center">
                                            <%#Eval("Gender")%></td>
                                        <td align="center">
                                            <%#Eval("RelationName")%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <table>
                            </table>
                        </div> 
                            <br /> 
                             <table id="tblview" runat="server" runat="server" width="100%" cellpadding="0" cellspacing ="0" border="0">
                            <tr>
                            <td>
                            <table width="95%" cellpadding="2" cellspacing ="2" border="0">
                                <colgroup>
                                    <col align="left" style="text-align :left" width="120px" />
                                    <col />
                                    <col align="left" style="text-align :left" width="120px" />
                                    <col />
                                    <tr>
                                        <td style="text-align :right">
                                            First Name :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtFirstName" required='yes' runat="server" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvdob0" runat="server" 
                                                ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align :right">
                                            Last Name :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtLastname" runat="server" MaxLength="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            Relation : </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRelation" required='yes' runat="server" Width="130px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvrelation0" runat="server" 
                                                ControlToValidate="ddlRelation" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align :right">
                                            DOB :</td>
                                        <td>
                                            <asp:TextBox ID="txtdob" required='yes' runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdob" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:ImageButton ID="imgdob" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            Gender : </td>
                                        <td style="text-align :left">
                                          
                                            <asp:DropDownList ID="ddlgender" required='yes' runat="server" Width="130px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="ddlgender" ErrorMessage="*"></asp:RequiredFieldValidator>
                                          
                                        </td>
                                        <td style="text-align :right">
                                            &nbsp;</td>
                                        <td style="text-align :left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">&nbsp; Address :</td>
                                        <td style="text-align :left"><asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            &nbsp;</td>
                                        <td style="text-align :left"> 
                                             <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
                                        </td>    
                                        <td style="text-align :right">
                                            Telephone :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtTelephone" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                         <td style="text-align :left"> 
                                             <asp:TextBox ID="txtAddress3" runat="server"></asp:TextBox>
                                        </td>   
                                        <td style="text-align :right">
                                            Mobile :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td style="text-align :left"> 
                                             <asp:TextBox ID="txtAddress4" runat="server"></asp:TextBox>
                                        </td>    
                                        <td style="text-align :right">
                                            Email :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <b>Passport Details<span lang="en-us"> :</span></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            Passport # :
                                        </td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtpassport" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align :right">
                                            Issue Date :
                                        </td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtpassportissuedate" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgpstissuedate" runat="server" 
                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            Expiry Date :
                                        </td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtpassportexpdate" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgexpdate" runat="server" 
                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                        </td>
                                        <td style="text-align :right">
                                            Place of Issue :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtplaceofissue" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            &nbsp;</td>
                                        <td style="text-align :left">
                                            &nbsp;</td>
                                        <td style="text-align :right">
                                            Attach Document :</td>
                                        <td style="text-align :left">
                                            <asp:FileUpload ID="flPassportdocument" runat="server" Width="215px" />&nbsp;<asp:ImageButton 
                                                runat="server" ID="imgbtnPassport" ImageUrl="~/Modules/HRD/Images/paperclip.gif" 
                                                Visible="false" onclick="imgbtnPassport_Click"/>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <b>FIN/NRIC Detail<span lang="en-us">s :</span></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            FIN/NRIC # :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtfinnric" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="text-align :right">
                                            FIN/NRIC Type :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtfinnrictype" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            Issue Date :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtfinissuedate" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgfinissuedate" runat="server" 
                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                        </td>
                                        <td style="text-align :right">
                                            Expiry Date :</td>
                                        <td style="text-align :left">
                                            <asp:TextBox ID="txtfinexpirydate" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgfinexpirydate" runat="server" 
                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align :right">
                                            &nbsp;</td>
                                        <td style="text-align :left">
                                            &nbsp;</td>
                                        <td style="text-align :right">
                                            Attach Document :</td>
                                        <td style="text-align :left">
                                            <asp:FileUpload ID="flFinNricdocument" runat="server" Width="215px" />&nbsp;<asp:ImageButton 
                                                runat="server" ID="imgbtnFin" ImageUrl="~/Modules/HRD/Images/paperclip.gif" Visible="false" 
                                                onclick="imgbtnFin_Click"/>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgdob" PopupPosition="TopLeft" TargetControlID="txtdob"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgexpdate" PopupPosition="TopLeft" TargetControlID="txtpassportexpdate"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfinissuedate" PopupPosition="TopLeft" TargetControlID="txtfinissuedate"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfinexpirydate" PopupPosition="TopLeft" TargetControlID="txtfinexpirydate"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgpstissuedate" PopupPosition="TopLeft" TargetControlID="txtpassportissuedate"></ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            </td>
                            <td style=" vertical-align :top; padding-right :40px;" >
                             <fieldset style=" height :200px"  > 
                        <center>
                        <table cellspacing="0" cellpadding="0" width="100px" border="0">
                        <tr>
                            <td style="text-align: center; padding-top :5px;">
                              <asp:Image ID="img_Family" style="cursor:hand" ImageUrl="~/Modules/HRD/Images/emtm/noimage.jpg" ToolTip="Click to Preview" runat="server" BorderColor="#8FAFDB" BorderStyle="Solid" BorderWidth="1px" Height="180px" Width="150px"/>
                             </td>
                        </tr>
                        <tr>
                        <td style="text-align: center;">
                            <div style="border:0px solid; overflow:hidden; width:100px;">
                            <asp:FileUpload ID="FileUpload1" size="1" runat="server" style="left:-9px; position:relative; border:0px solid; background-color:#f9f9f9" Width="100px"/>                                            
                            </div>
                            
                        </td>
                        </tr>
                        </table>
                        </center> 
                        </fieldset> 
                        <center>
                        <br />
                        <table id="Table1" runat="server" width="100%" cellpadding="2" cellspacing ="0" border="0">
                            <tr>
                                <td align="right" style="padding-right:50px">
                                    
                                    <asp:Button ID="btnsave" CssClass="btn" runat="server" Width="80px" Text="Save" onclick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn" runat="server"  Width="80px" 
                                        Text="Cancel" CausesValidation="false" onclick="btncancel_Click" ></asp:Button>
                                </td>
                            </tr>
                            </table>
                        </center>
                            </td>
                            </tr>
                            </table> 
                            <asp:Button ID="btnaddnew" CssClass="btn" style="float :right" runat="server" Width="80px" Text="Add New" onclick="btnaddnew_Click" CausesValidation="false"></asp:Button>
                        </div> 
                    </td>
                </tr>
         </table> 
         <%--</ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID ="btnsave" />
        </Triggers> 
          </asp:UpdatePanel>    --%>
    </div>
   </asp:Content>

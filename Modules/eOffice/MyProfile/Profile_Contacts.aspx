<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_Contacts.aspx.cs" Inherits="Emtm_Profile_Contacts" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>
<%@ Register src="Profile_PersonalHeaderMenu.ascx" tagname="Profile_PersonalHeaderMenu" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


        <link href="../style.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    
   
    <div style="font-family:Arial;font-size:12px;">
   
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
          <table width="100%">
                    <tr>
                       
                       <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                            <div class="dottedscrollbox" style="padding-left:5px;"></div> 
                            <div class="text headerband" style=" text-align :left; font-size :14px; padding :3px; font-weight: bold;">
                            Personal Details : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                            <div class="dottedscrollbox">
                                <uc3:Profile_PersonalHeaderMenu ID="Emtm_Profile_PersonalHeaderMenu1" 
                                    runat="server" />
                            </div> 
                            <div style="padding:0px 10px 0px 10px;" >
                              <table  width="100%" cellpadding="2" cellspacing ="0" border="0">
                                 <tr>
                                     <td style="padding-top:20px;">
                                         <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                             <colgroup>
                                                 <col width="120px">
                                                 <col />
                                                 <tr>
                                                     <td colspan="2">
                                                         <b>Local Address :</b><hr />
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Address :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtaddress1" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <asp:TextBox ID="txtaddress2" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <asp:TextBox ID="txtaddress3" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Country :</td>
                                                     <td>
                                                         <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" 
                                                             onselectedindexchanged="ddlcountry_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         State/Province :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtstate" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         City :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtcity" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Zip Code :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtzip" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                  </col>
                                             </colgroup>
                                         </table>
                                        <fieldset>
                                        <legend><strong>Personal Contact</strong></legend>
                                          <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                             <colgroup>
                                                 <col width="120px">
                                                 <col />
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; AreaCode&nbsp;&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Telephone :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtnumcntycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtareacode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtnumber" runat="server" Width="135px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Mobile :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtmobcntrycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtmobno" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; AreaCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Fax :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtfaxcntrycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtfaxareacode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtfax" runat="server" Width="135px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Email :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtPersonalEmail" runat="server" Width="258px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                </col>
                                             </colgroup>
                                         </table> 
                                        </fieldset>  
                                         <fieldset>
                                        <legend><strong>Office Contact</strong></legend>
                                         <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                             <colgroup>
                                                 <col width="120px">
                                                 <col />
                                                  <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; AreaCode&nbsp;&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Telephone :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtOffCntryCode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtOffAreaCode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txtOffNumber" runat="server" Width="135px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                  <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Mobile :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtOffMobCntryCode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;&nbsp;<asp:TextBox ID="txtOffMobNumber" runat="server" Width="135px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Email :</td>
                                                     <td>
                                                         <asp:TextBox ID="txtOffEmail" runat="server" Width="258px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 </col>
                                             </colgroup>
                                         </table>
                                        </fieldset> 
                                     </td>
                                     <td valign="top" >
                                         <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                             <colgroup>
                                                 <col width="120px">
                                                 <col />
                                                 <tr>
                                                     <td colspan="2">
                                                         <asp:CheckBox ID="chksameaddress" runat="server" AutoPostBack="true" 
                                                             oncheckedchanged="chksameaddress_CheckedChanged" 
                                                             Text="Same as Local Address" />
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td colspan="2">
                                                         <b>Permanent Address  :</b><hr />
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Address :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_address1" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_address2" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_address3" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Country :</td>
                                                     <td>
                                                         <asp:DropDownList ID="ddl_country" runat="server" AutoPostBack="true" 
                                                             onselectedindexchanged="ddl_country_SelectedIndexChanged">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         State/Province :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_state" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         City :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_city" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Zip Code :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_zip" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                            </colgroup>
                                            </table>   
                                         <fieldset>   
                                         <legend><strong>Personal Contact</strong></legend>
                                         <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                             <colgroup>
                                                 <col width="120px">
                                                 <col />
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; AreaCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 
                                                 <tr>
                                                     <td>
                                                         Telephone :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_numcntycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txt_areacode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txt_number" runat="server" Width="134px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Mobile :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_mobcntrycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txt_mob_number" runat="server"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         &nbsp;</td>
                                                     <td>
                                                         <span class="brief">CountryCode&nbsp; AreaCode&nbsp; Number</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Fax :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_faxcntrycode" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txt_fax_areacode" runat="server" Width="50px"></asp:TextBox>
                                                         &nbsp;<asp:TextBox ID="txt_fax_number" runat="server" Width="134px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         Email :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_email1" runat="server" Width="258px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 <tr style="display:none">
                                                     <td>
                                                         Email 2 :</td>
                                                     <td>
                                                         <asp:TextBox ID="txt_email2" runat="server" Width="258px"></asp:TextBox>
                                                     </td>
                                                 </tr>
                                                 </col>
                                             </colgroup>
                                         </table>
                                         </fieldset>
                                         <div style="width :100%; text-align :right; padding-top :110px; ">
                                         
                                         <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save"  CausesValidation="False" onclick="btnsave_Click"></asp:Button>
                                         <asp:Button ID="brncancel" CssClass="btn"  runat="server" Text="Cancel" PostBackUrl="~/emtm/MyProfile/Emtm_Profile_Contacts.aspx" CausesValidation="false"></asp:Button>
                                         </div>
                                     </td>
                                 </tr>
                             </table>
                            </div>
                        </td>
                        </tr>
          </table>
          </ContentTemplate>
        </asp:UpdatePanel> 
    </div>
    </asp:Content>
    

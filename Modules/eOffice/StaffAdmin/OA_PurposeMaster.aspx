<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OA_PurposeMaster.aspx.cs" Inherits="emtm_OfficeAbsence_Emtm_OA_PurposeMaster" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>

<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">     
   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css" >
    .SelectedPage
    {
    	background-color:#B38481;
    }
    </style>

    <div>
    
        
        <%--<asp:UpdateProgress runat="server" ID="UpdateProgress1">
            <ProgressTemplate>
            <div style="top :100px; width:100%; position :absolute; padding-top :100px; padding-left :90px;">
            <center >
            <div style ="width : 120px; height :36px;">
            <img src="../../Images/Emtm/loading.gif" alt="loading ..." style ="float:left"/><span style ="font-size :11px;"><br />Loading ... </span>
            </div>
            </center>
            </div> 
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        
        <table width="100%">
                    <tr>
                      
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Absence Purpose Register
                        </div>
                        <div class="dottedscrollbox">
                            <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                     <asp:UpdatePanel runat="server" ID="upd1">
                       <ContentTemplate>
                         <table cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td>
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse; height:26px;">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="50px;" />                                        
                                        <col />                                        
                                        <col width="25px" />                                       
                                    </colgroup>
                                    <tr align="left" class= "headerstylegrid">
                                        <th scope="col" style="font-size: 11px;">
                                            Edit
                                        </th>
                                        <th scope="col" style="font-size: 11px;">
                                            Delete
                                        </th>
                                        <th scope="col" style="font-size: 11px;">
                                            Purpose
                                        </th>
                                        <th scope="col" style="font-size: 11px;">
                                        &nbsp;
                                        </th>
                                    </tr>
                                </table>
                            </div>
                            <div id="divPurpose" runat="server" style="overflow-x: hidden; overflow-y: scroll;
                                width: 100%; height: 380px; border: solid 1px Gray;" onscroll="SetScrollPos(this)">
                                <table width="100%" cellspacing="0" cellpadding="1" border="1" rules="all" style="border-collapse: collapse;">
                                    <colgroup>
                                        <col width="50px;" />
                                        <col width="50px;" />                                        
                                        <col />                                        
                                        <col width="25px" /> 
                                    </colgroup>
                                    <asp:Repeater runat="server" ID="rptPurpose">
                                        <ItemTemplate>
                                            <tr class='<%# (Common.CastAsInt32(Eval("PurposeId"))==PID)?"selectedrow":"row"%>'>
                                                <td style="text-align: center; font-size: 11px;">
                                                    <asp:ImageButton ID="imgEdit" runat="server" OnClick="imgEdit_OnClick"
                                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" CausesValidation="false" />
                                                    <asp:HiddenField ID="hfPurposeId" runat="server" Value='<%#Eval("PurposeId")%>' />                                                    
                                                </td>
                                                <td style="text-align: center; font-size: 11px;">
                                                    <asp:ImageButton ID="imgDelete" runat="server" OnClick="imgDelete_OnClick"
                                                        ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return confirm('Are you sure to delete ?');"
                                                        CausesValidation="false" />
                                                </td>
                                                <td style="text-align: left; font-size: 11px;">                                                    
                                                    <asp:Label ID="lblPurpose" runat="server" Text='<%#Eval("Purpose")%>'></asp:Label>
                                                </td>
                                               <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <div id="tblPurpose" runat="server" visible="false">
                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                    <colgroup>
                                        <col width="110px" />
                                        <col />
                                        <tr>
                                            <td style="text-align: right; font-weight: bold;">
                                                Purpose :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtPurpose" runat="server" MaxLength="200" required="yes" 
                                                    Width="95%"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txtPurpose" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </colgroup>
                                </table>
                            </div>
                            <div style="float: right; padding: 2px;">
                                <asp:Label ID="lblMsg" runat="server" Style="color: Red;"></asp:Label>
                                <asp:Button ID="btnAdd" runat="server" Text=" Add New" CausesValidation="false"
                                    OnClick="btnAdd_OnClick" CssClass="btn" Width="100px" />
                                <asp:Button ID="btnSave" runat="server" Text="  Save  " OnClick="btnSave_OnClick"
                                    CssClass="btn" Visible="false" Width="100px" />
                                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " CausesValidation="false"
                                    OnClick="btnCancel_OnClick" CssClass="btn" Visible="false" Width="100px" />
                            </div>
                        </td>
                    </tr>
                </table>
                       </ContentTemplate>
                      </asp:UpdatePanel>
                    </td>
                </tr>
                </table> 

       
             
            
    </div>
</asp:Content>

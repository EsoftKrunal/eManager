<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeriodClosure.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_LeaveSearch" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>



<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #det li
    {
        padding-top :2px;
        padding-bottom :2px; 
    }
    </style>  

     
    <div style="font-family:Arial;font-size:12px;">
    
                <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Period Closure
                        </div>
                        <div class="dottedscrollbox">
                            <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                        <asp:UpdatePanel id="UpdatePanel" runat="server" >
                        <ContentTemplate>
                       <div style=" float:left; width :100%; text-align :left; margin:20px;" > 
                       <div style=" border:dotted 1px blue; text-align :center; width:90%;">
                            <table cellpadding="5" cellspacing="2" width="100%">
                                <colgroup>
                                    <tr>
                                        <td >
                                            <b><span style=" font-size :14px;" >Year for Closure</span></b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label Font-Size="13px" Font-Bold="true" ID="lblClosureYear" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style =" text-align :center ">
                                            <asp:Button ID="btnClosure" runat="server" CssClass="btn" Text="Close Year" 
                                                Width="100px" 
                                                OnClientClick="return confirm('Are you sure to close the last year?');" 
                                                onclick="btnClosure_Click"  />
                                        </td>
                                    </tr>
                                </colgroup>
                                </table>
                             <br />
                       </div>
                       </div>
                       
                       <div style="width :100%; text-align :left; margin:20px;" > 
                      <span style =" color:Blue"><b> What will change? : </b></span>
                      <br /><br />
                       <div style=" width:90%; border:dotted 1px blue; text-align :left; overflow-x :hidden ; overflow-y:scroll; min-height:240px;">
                       <br />
                           <ol id="det" start="1" type="1" >
                            <li>Closing year leave balance will be carry forwarded to current year.</li>
                            <li>Previous year annual entitlement for various leave will be copied to current year.</li>
                           </ol>
                       </div>
                       </div>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                        </td>
                    </tr>
            </table>      
    </div>
   </asp:Content>

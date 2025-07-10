<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="PMS_UserControls_Footer" %>
<script type="text/javascript">
function OpenPopup()
{
    window.open('PostComment.aspx','',"width=270px,height=450px");
}
</script> 
  <div id="footer" runat="server" style=" color : #4172ac; float :left ; clear : both ; width :100%;background-image :url(images/footer_bg.jpg); height:20px;width:100%; text-align :center; padding-top :3px; font-size :11px; display:none;">
        Developed by M.T.M. Shipmanagement Pte. Ltd. All rights reserved. 
        <span runat="server" id="aComm"> | <a style=" font-size :11px;" onclick='OpenPopup();' href="#">Feedback</a></span> | 
        <span style="padding-right:25px; font-size:12px; font-weight:bold; font-family:Calibri; font-style:italic;">Version: 1.01</span>  
  </div> 

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControls_Footer" %>
<script type="text/javascript">
function OpenPopup()
{
    window.open('PostComment.aspx','',"width=270px,height=450px");
}
</script> 
<div id="footer">
    Developed by e.Soft Technologies Pvt Ltd.  All rights reserved. 
    <span runat="server" id="aComm"> | <a style=" font-size :11px;" onclick='OpenPopup();' href="#">Feedback</a></span>
</div>
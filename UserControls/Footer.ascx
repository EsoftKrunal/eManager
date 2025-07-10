<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControls_Footer" %>
<script type="text/javascript">
function OpenPopup()
{
    window.open('PostComment.aspx','',"width=270px,height=450px");
}
</script> 
<style type="text/css">
    #footer {
	float: left;
	color: #4172ac;
	height: 20px;
	width: 100%;
	position: relative;
	margin-top: -20px;
	clear: both;
	background-image: url(images/footer_bg.jpg);
	text-align: center;
	padding-top: 3px;
	font-size: 11px;
}
    </style>
<div id="footer" class="main-footer" style="text-align:center;">
     <strong>Copyright &copy; &nbsp;2023. Power By : e.Soft Technologies Pvt Ltd. 
                    <asp:HyperLink Visible="false" ID="ComanyLinkFooter" NavigateUrl="https://www.esoftech.com/" runat="server"></asp:HyperLink></strong> All rights reserved.
    <span runat="server" id="aComm"> | <a style=" font-size :11px;" onclick='OpenPopup();' href="#">Feedback</a></span>
</div>
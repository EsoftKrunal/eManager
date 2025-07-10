<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Image_Zoom.ascx.cs" Inherits="Image_Zoom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<style type="text/css">
.modalBackground {
	background-color: Gray;
	filter: alpha(opacity=50);
	opacity: 0.5;
}
.modalBox {
	background-color : #f5f5f5;
	border-width: 0px;
	border-style: solid;
	border-color: Blue;
	padding: 0px;
}
.modalBox caption {
	background-image: url(images/window_titlebg.gif);
	background-repeat:repeat-x;
}
</style>
 <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowImage" PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnCancel" />
               <asp:Panel ID="pnlPopup" runat="server" style="display:none" CssClass="modalPanel">
                <table cellpadding="0" width="800" cellspacing="0" border="1" style=" border-collapse:collapse; border-color:White; font-family:Verdana; font-size:small">
                <tr>
                <td style=" display:none;" ><asp:Button ID="btnShowImage" runat="server" Text="Button" /></td>
                </tr>
                    <tr>
                        <td align="center" style=" width:775px; background-color:#5078AB; color:White;"><strong>Scanned Document Viewer</strong></td>
                        <td style=" width:39px; text-align: right;"><asp:ImageButton ID="btnCancel" ImageUrl="~/Modules/HRD/Images/delete_button.jpg" runat="server" Text="Close" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div runat="server" id="divShow" style="overflow:scroll; width:<% Response.Write(w); %>px;height:<% Response.Write(h); %>px">
                                <img id="imgPopup" alt="Image Preview" src="" runat="server" enableviewstate="false" visible="true" class="modalBox" />
                            </div>
                        </td>
                    </tr>
                    
                </table>
            </asp:Panel> 
            
</div>
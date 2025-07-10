<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewAppraisal.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="CrewAppraisal_Home" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
    .btn
    {
        background-color:#bbb;color:#333;border:none;padding:7px; padding-left:20px;padding-right:20px;
    }
    .btnsel
    {
        background-color:#333;color:#fff;border:none;padding:7px; padding-left:20px;padding-right:20px;
    }
</style>
    <div style="text-align: center;padding:2px">
     
        <div style="border-bottom:solid 5px #333">
              <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border-collapse:collapse;" >
           <tr>
               <td style="width:100px;padding-right:4px;">
                   <asp:Button runat="server" ID="btnVessel" CssClass="btnsel" OnClick="btnVessel_Click"  Text="Vessel Peap"/>
               </td>
               <td style="width:100px">
                   <asp:Button runat="server" ID="btnOffice" CssClass="btn" OnClick="btnOffice_Click"  Text="Office Peap"/>
               </td>
               <td></td>
           </tr>
       </table>
        </div>
        <div>
            <iframe runat="server" id="frame1" width="100%" height="518px" src="CrewAppraisal.aspx" frameborder="0">

            </iframe>
        </div>
    </div>
  
</asp:Content>
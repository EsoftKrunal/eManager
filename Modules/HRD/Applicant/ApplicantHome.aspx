<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ApplicantHome.aspx.cs" Inherits="CrewApproval_ApplicantHome" MasterPageFile="~/MasterPage.master" Title="Crew Approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        .btn1selected
        {
            border:none;
            background-color:#0094ff;
            color:white;
            padding:5px;
            width:100px;
        }
        .btn1
        {
            border:none;
            background-color:#c4c4c4;
            color:#000000;
            padding:5px;
            width:100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
        <div style="text-align: left">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table style="width :100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style=" text-align:left; vertical-align:top; border-left:solid 1px white;">
                        <div style="padding-bottom:0px;padding-left:0px; border-bottom:solid 5px #0094ff; text-align: left" class="headerband">
                            <asp:Button runat="server" id="btnHome" Text="Home" OnClick="btn_Home_Click" CssClass="btn1selected" />
                            <asp:Button runat="server" id="btnApplicant" Text="New Hires" OnClick="btn_Applicant_Click" CssClass="btn1"  />
                        </div>
                        <div>
                            <iframe src="ApplicantHomeNew.aspx" width="100%" height="520px" scrolling="no" frameborder="0" runat="server" id="frm1"></iframe>    
                        </div>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>

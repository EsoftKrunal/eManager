<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselSelection.aspx.cs" Inherits="VesselSelection" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
      <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <div class="text headerband">
            Vessel Setup
        </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td  >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                            <td style="padding-right: 10px;padding-left:2px">
                            <div style="width:100%; height:452px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
               <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>
                            <asp:UpdatePanel runat="server" id="up1" UpdateMode="Always">
                            <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                            <tr>
                               <td style=" height:50px; vertical-align:bottom;text-align:right;padding-right:10px; ">
                                   <b>Please Select Vessel :</b>
                               </td>
                                <td style=" height:50px; vertical-align:bottom;text-align:left;padding-left:10px; ">
                                    <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="true" Width="272px" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList>
                                </td>
                            </tr>
                            </table>
     </ContentTemplate>
     </asp:UpdatePanel>                            
      </div> 
      </td>
      </tr>
      </table>
      </td>
      </tr>
    </table>
    </td> 
    </tr>
    </table>
     </div>
    
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_DockingCategoryMaster.aspx.cs" Inherits="DryDock_DD_DockingCategoryMaster" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link href="~/CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="~/CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="~/JS/Common.js" type="text/javascript"></script>
             <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
      <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
        Registers - Dock Category
        </div>
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">                        
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 2px; padding-top:2px">
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
            <asp:UpdatePanel runat="server" id="up11" UpdateMode="Conditional">
             <ContentTemplate>
             <div style="border:none; background-color : #FFB870; font-size :14px; padding:3px; text-align:center;">
             <asp:ImageButton runat="server" ID="btnAddCat" OnClick="btnAddCat_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left" ToolTip="Add New Docking Category" />
             <asp:ImageButton runat="server" ID="btnEditCat" OnClick="btnEditCat_Click" Visible="false" ImageUrl="~/Modules/PMS/Images/editx16.png" style="float:left; padding-left:5px;" ToolTip="Edit Docking Category" />
             Docking Category <a href="../Registers.aspx" style="float:right;">Back</a>
             </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 390px ; text-align:center;" class="scrollbox">
             
                        <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:15px;" />
                                    <col style="width:40px;" />
                                    <col />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobCats" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("CatId"))== JobCatId)? "background-color:#1589FF;color:white;" : "" %> '>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnSelectCat" OnClick="btnSelectCat_Click" CommandArgument='<%#Eval("CatId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" style="float:right" ToolTip="Select Docking Category" /></td>
                                            <td style="text-align:center"><%#Eval("CatCode")%></td>
                                            <td align="left"><div style="height:14px"><%#Eval("CatName")%></div></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("CatId"))== JobCatId)? "background-color:#1589FF;color:white;" : "background-color:#FFF5E6" %>'>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnSelectCat" OnClick="btnSelectCat_Click" CommandArgument='<%#Eval("CatId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" style="float:right" ToolTip="Select Docking Category" /></td>
                                            <td style="text-align:center"><%#Eval("CatCode")%></td>
                                            <td align="left"><div style="height:14px"><%#Eval("CatName")%></div></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                       
                        <%-- Docking Category Add/ Edit Section --%>
                        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dv_JobCategory" runat="server" visible="false">
                        <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 500px; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 500px; height: 150px; padding: 3px; text-align: center;background: white; z-index: 150; top: 130px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="up1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td  colspan="2" style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;" >
                                            Add/ Edit Docking Category
                                        </td>
                                    </tr>

                                       <tr >
                                            <td style="text-align:right; width:25%;">Category Code :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtCatCode"  Width="50px"></asp:TextBox></td>
                            
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Category Name :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtCatName"  Width="300px" MaxLength="100"></asp:TextBox></td>                  
                                         </tr>
                                        </table>
                                        <br />
                                        <div style="text-align:center"><asp:Label ID="lblCalMag" ForeColor="Red" runat="server"></asp:Label></div>
                                        <br />
                                        <div style="text-align:center">
                                            <asp:Button runat="server" ID="btnSaveCat" Text="Save" OnClick="btnSaveCat_Click" style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;"  />
                                            <asp:Button runat="server" ID="btnCancelCat" Text="Close" OnClick="btnCancelCat_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnCancelCat" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
         </div>
            
            </div>
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

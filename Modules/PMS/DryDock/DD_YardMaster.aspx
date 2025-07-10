<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_YardMaster.aspx.cs" Inherits="DD_YardMaster" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
    <link href="~/CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="~/JS/Common.js" type="text/javascript"></script>
             <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
          <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
        Registers 
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
             <asp:ImageButton runat="server" ID="btnAddYard" OnClick="btnAddYard_Click" ImageUrl="~/Modules/PMS/Images/add.png" style="float:left" ToolTip="Add New Yard" />
             <asp:ImageButton runat="server" ID="btnEditYard" OnClick="btnEditYard_Click" Visible="false" ImageUrl="~/Modules/PMS/Images/editx16.png" style="float:left; padding-left:5px;" ToolTip="Edit Yard" />
             <a href="../Registers.aspx" style="float:right;padding-right:25px;">Back</a>
             </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 25px ; text-align:center; border-bottom:none; background-color:#ADD6FF; font-weight:bold;" class="scrollbox">
                <table cellspacing="0" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px">
                 <tr class= "headerstylegrid">
                    <td style="text-align:center;width:40px;">Select</td>
                    <td align="left" style="width:300px;">Yard Name</td>
                    <td align="left">Yard Address</td>
                    <td align="left" style="width:200px;">Contact #</td>
                    <td align="left" style="width:300px;">Email Address </td>
                    </tr>
                </table>
             </div>
             <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 390px ; text-align:center;" class="scrollbox">
             
                        <table cellspacing="0"  border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:40px;" />
                                    <col style="width:300px;" />
                                    <col />
                                    <col style="width:200px;" />
                                    <col style="width:300px;" />
                                </colgroup>
                                <asp:Repeater ID="rptJobYards" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("YardId"))== YardId)? "background-color:#1589FF;color:white;" : "" %> '>
                                            <td style="text-align:center;width:40px;"><asp:ImageButton runat="server" ID="btnSelectYard" OnClick="btnSelectYard_Click" CommandArgument='<%#Eval("YardId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" ToolTip="Select Yard" /></td>
                                            <td  style="width:300px;" align="left"><%#Eval("YardName")%></td>
                                            <td align="left"><%#Eval("Address")%></td>
                                            <td  style="width:200px;" align="left"><%#Eval("ContactNo")%></td>
                                            <td  style="width:300px;" align="left"><%#Eval("Email")%></td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("YardId"))== YardId)? "background-color:#1589FF;color:white;" : "background-color:#FFF5E6" %>'>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btnSelectYard" OnClick="btnSelectYard_Click" CommandArgument='<%#Eval("YardId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" ToolTip="Select Yard" /></td>
                                            <td  style="width:300px;" align="left"><%#Eval("YardName")%></td>
                                            <td align="left"><%#Eval("Address")%></td>
                                            <td  style="width:200px;" align="left"><%#Eval("ContactNo")%></td>
                                            <td style="width:300px;"  align="left"><%#Eval("Email")%></td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                       
                        <%-- Docking Yardegory Add/ Edit Section --%>
                        <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%;" id="dv_JobYardegory" runat="server" visible="false">
                        <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 500px; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 700px; height: 400px; padding: 3px; text-align: center;background: white; z-index: 150; top: 30px; border: solid 10px gray;">
                               <asp:UpdatePanel runat="server" id="up1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                       <tr>
                                        <td  colspan="2" style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;" >
                                            Add/ Edit Yard 
                                        </td>
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Yard Name :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtYardName"  Width="95%" MaxLength="100"></asp:TextBox></td>                  
                                         </tr>
                                         <tr>
                                            <td style="text-align:right; width:25%;">Yard Address :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtYardAddress" TextMode="MultiLine" Height="150px" Width="95%"></asp:TextBox></td>
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Contact# :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtYardContact"  Width="95%" MaxLength="100"></asp:TextBox></td>                  
                                         </tr>
                                         <tr >
                                            <td style="text-align:right; width:25%;">Email :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtYardEmail"  Width="95%" MaxLength="500"></asp:TextBox></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Contact Person :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtContactPerson"  Width="95%" MaxLength="50"></asp:TextBox></td>                  
                                         </tr>
                                         <tr >
                                            <td style="text-align:right; width:25%;">Port Name :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtPortName"  Width="50%" MaxLength="50"></asp:TextBox></td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Country :</td>
                                            <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlCountry"  Width="50%" ></asp:DropDownList></td>                  
                                         </tr>

                                        </table>
                                        
                                        <div style="text-align:left; padding-top:10px;">
                                            <div style="text-align:left; float:left;">
                                                <asp:Label ID="lblCalMag" ForeColor="Red" runat="server"></asp:Label>
                                            </div>
                                            <div style="text-align:right; float:right;">
                                                <asp:Button runat="server" ID="btnSaveYard" Text="Save" OnClick="btnSaveYard_Click" style=" padding:3px; border:none;  width:80px;" CssClass="btn"  />
                                                <asp:Button runat="server" ID="btnCancelYard" Text="Close" OnClick="btnCancelYard_Click" style=" padding:3px; border:none;  width:80px;" CssClass="btn"  />
                                            </div>
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnCancelYard" />
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

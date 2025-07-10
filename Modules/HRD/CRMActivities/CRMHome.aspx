<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRMHome.aspx.cs" Inherits="CRMActivities_CRMHome" MasterPageFile="~/MasterPage.master" Title="EMANAGER" %>
<%--<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/> 
  <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" type="text/css" href="../styles/stylesheet.css" />--%>
    <script type="text/javascript" src="../JS/jquery.min.js"></script>
    <script type="text/javascript" src="../JS/KPIScript.js" ></script>
    <script type="text/javascript" src="../JS/jquery.datetimepicker.js" ></script>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
.fh1
{ 
    font-weight:bold;
    font-size:14px;
}
.fh2
{ 
    font-size:14px;
}
.fh3
{ 
    font-size:14px;
    cursor:pointer;
    text-decoration:underline;
}
.fh3:hover
{ 
    font-size:14px;
    color:Red;
    cursor:pointer;
    text-decoration:underline;
}
.btn_Close
{
    background-color:Red;
    border:solid 1px grey;
    color:White;
    width:100px;
}
.cls_I
{
    background-color:#80E680;
}
.cls_O
{
    background-color:#FFAD99;
}

</style>
    <div style="background-color:Black; opacity:0.4;filter:alpha(opacity=40); width:100%; z-index:50; min-height:100%; position:absolute; top:0px; left:0px; display:none;" class='dv_ModalBox' onclick="HideAll();"></div>
  </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server"> 
    <div style="text-align: left">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style=" text-align :left; vertical-align : top;" >
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                        <tr>
                            <%--<td style="background-color:#4371a5; height: 23px; text-align :center" class="text">CRM Activity Home Page</td>--%>
                            <td class="text headerband">
                                 <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
                                Welfare</td>
                        </tr>
                        <tr>
                            <td>
                                 <table width="95%" align="center" cellspacing="20">
                                    <tr>
                                        <td style="text-align:left;">
                                            <div style="width :98%;">
                                                <div style="border:solid 1px gray; background-color : #80E680; font-size :14px; font-weight:bold; padding:3px; text-align:center;">Birth Day Greetings</div>
                                                <div style="border:solid 1px gray; text-align:left;font-size :12px;padding:3px">               
                                                    <table width="95%" cellpadding="4" border="0">
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >1. </td>
                                                            <td >
                                                                Birth Day in Next&nbsp;
                                                            </td>
                                                            <td style="text-align:left; width:120px">
                                                                <asp:TextBox ID="txtBDays" AutoPostBack="true" OnTextChanged="txtBDays_TextChanged" Text="30" runat="server" Width="20px" MaxLength="2" ></asp:TextBox>
                                                                &nbsp;Days</td>
                                                            <td style="text-align:right; width:70px">
                                                                <asp:LinkButton ID="lnkBirthDays" OnClick="lnkBirthDays_Click" runat="server"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >2. </td>
                                                            <td >
                                                                <asp:LinkButton ID="btnBDInventory" Text="Birth Day Card Inventory" OnClick="lnkInventory_Click" CommandArgument="1" runat="server"></asp:LinkButton>
                                                            </td>
                                                            <td style="text-align:left; width:80px">
                                                                </td>
                                                            <td style="text-align:right; width:70px">
                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <br /> <br />
                                                <div style="border:solid 1px gray; background-color : #FFE6E6; font-size :14px; font-weight:bold; padding:3px; text-align:center;">Seasons Greeting</div>
                                                <div style="border:solid 1px gray; text-align:left;font-size :13px;padding:3px">
                                                    <table width="95%" cellpadding="4" border="0">
                                                        <tr>
                                                           <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >1. </td>
                                                            <td >Seasons Greeting </td>
                                                            <td style="text-align:left; width:80px"></td>
                                                            <td style="text-align:right; width:70px">
                                                                <asp:LinkButton ID="lnkSeasonsGreeting" OnClick="lnkSeasonsGreeting_Click" Text="Open" runat="server"></asp:LinkButton>                              
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >2. </td>
                                                            <td >
                                                                <asp:LinkButton ID="btnSGInventory" Text="Seasons Greeting Card Inventory" OnClick="lnkInventory_Click" CommandArgument="2" runat="server"></asp:LinkButton>
                                                            </td>
                                                            <td style="text-align:left; width:80px"></td>
                                                            <td style="text-align:right; width:70px"></td>
                                                        </tr>                  
                                                    </table>
                                                </div>
                                                <br /> <br />
                                                <div style="border:solid 1px gray; background-color : #C2E0FF; font-size :14px; font-weight:bold; padding:3px; text-align:center;">Welcome Home</div>
                                                <div style="border:solid 1px gray; text-align:left;font-size :12px;padding:3px">
                                                    <table width="95%" cellpadding="4" border="0">
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >1. </td>
                                                            <td>
                                                                Welcome Home&nbsp;
                                                            </td>
                                                            <td style="text-align:left; width:80px"></td>
                                                            <td style="text-align:right; width:70px">
                                                               <asp:LinkButton ID="lnkWelcome" OnClick="lnkWelcome_Click" runat="server"></asp:LinkButton> 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >2. </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnWHInventory" Text="Welcome Home Card Inventory" OnClick="lnkInventory_Click" CommandArgument="3" runat="server"></asp:LinkButton>
                                                            </td>
                                                            <td style="text-align:left; width:80px"></td>
                                                            <td style="text-align:right; width:70px"></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                    
                                            <div style="width :98%">
                                                <br /> <br />
                                                <div style="border:solid 1px gray; background-color : #FFEB99; font-size :14px; font-weight:bold; padding:3px; text-align:center;">Crew Communication</div>
                                                <div style="border:solid 1px gray; text-align:left;font-size :13px;padding:3px">
                                                    <table width="95%" cellpadding="4" border="0">
                                                        <tr>
                                                            <td style="width:50px">&nbsp;</td>
                                                            <td style="width:50px" >1. </td>
                                                            <td style="text-align:left" >Crew On Leave</td>
                                                            <td style="text-align:left; width:80px"></td>
                                                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnlOnleaveCrew" Text="Onleave Crew Members" OnClick="lnlOnleaveCrew_Click" CommandArgument="4" runat="server"></asp:LinkButton></td>
                                                        </tr>                  
                                                    </table>
                                                </div>
                                            </div>  
                                        </td>
                                    </tr>
                                </table>
                   
                                <%-- <div style="text-align: center">
                                   <table cellpadding="20" cellspacing="15" width="100%" border="0">
                
                                    <tr>
                                    <td style="text-align:left; background-color:#80E680;" class="fh2">
                                        Birth Days in Next&nbsp;<asp:TextBox ID="txtBDays" AutoPostBack="true" OnTextChanged="txtBDays_TextChanged" Text="30" runat="server" Width="20px" MaxLength="2" ></asp:TextBox>&nbsp;Days
                    
                                    </td>
                                    <td style="text-align:center; background-color:#80E680;" class="fh2">
                                       <asp:LinkButton ID="lnkBirthDays" OnClick="lnkBirthDays_Click" runat="server"></asp:LinkButton> 
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align:left; background-color:#FFE6E6;" class="fh2">
                                        Seasons Greeting&nbsp;
                                    </td>
                                    <td style="text-align:center; background-color:#FFE6E6;" class="fh2">
                                       <asp:LinkButton ID="lnkSeasonsGreeting" OnClick="lnkSeasonsGreeting_Click" Text="10" runat="server"></asp:LinkButton>
                                    </td>
                                    </tr>               
                
                                    <tr>
                                    <td style="text-align:left; background-color:#C2E0FF;" class="fh2">
                                        Welcome Home </td>
                                    <td style="text-align:center; background-color:#C2E0FF;" class="fh2">
                                        <asp:LinkButton ID="lnkWelcome" OnClick="lnkWelcome_Click" runat="server"></asp:LinkButton>
                                      </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align:left; background-color:#FFEB99;" class="fh2">
                                        Card Inventory </td>
                                    <td style="text-align:center; background-color:#FFEB99;" class="fh2">
                                       <asp:LinkButton ID="lnkInventory" OnClick="lnkInventory_Click" Text="20" runat="server"></asp:LinkButton>
                                       </td>
                                    </tr>
                                    </table>
                                </div>--%>
                            </td>
                        </tr>
                    </table> 
                </td>
            </tr>
        </table>
    </div>   
</asp:Content>

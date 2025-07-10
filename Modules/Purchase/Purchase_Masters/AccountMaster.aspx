<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMaster.aspx.cs" Inherits="AccountMaster" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/Common.js"></script>
     <style type="text/css">
        .PageError
        {
        	color:Red;
        	font-family:Verdana;
        	font-weight:bold;
        	font-size:11px;
        }
    </style>
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    //                    if(trSel.getAttribute(CSSName)=="selectedrow")
                    //                    {
                    //                        trSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    //                        document.getElementById('hfPRID').value = "";
                    //                    }
                    //                    else
                    //                    {
                        trSel.setAttribute(CSSName, "selectedrow");
                        lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                    //}
                }
                else // clicking on ohter row
                {
                    lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
                }
            }
        }
        function fncInputNumericValuesOnly(evnt) {
                
                if (!(event.keyCode == 13 || event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {

                    event.returnValue = false;

                }

            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="font-family:Arial;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    
            <div style="border:2px solid #4371a5;" >
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
    <tr>
    <td>
       <uc2:Registers runat="server" ID="Registers1" />  
        <div class="text headerband">
            Account Codes
        </div>
        <table width="100%">
            <tr>
                <td width="200px" style="text-align:right;padding-right:5px;"> 
                    Mid Category :
                </td>
                <td width="200px" style="text-align:left;padding-left:5px;">
                    <asp:DropDownList ID="ddlMidCate" runat="server" Width="150px" ></asp:DropDownList>
                </td>
                <td style="text-align:left;padding-left:5px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    <asp:UpdatePanel ID="UP" runat="server" >
        <ContentTemplate>   
       <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                <col style="width:3%;" />
                <col style="width:3%;" />
                <col style="width:6%;" />
                <col style="width:20%;"/>
                <col style="width:6%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:10%; text-align:left;" />
                <col style="width:3%; " />
                <tr align="left" class= "headerstylegrid">
                    <td>Edit</td>
                    <td>Delete</td>
                    <td>Acct #</td>
                    <td>Acct Name</td>
                    <td>Active</td>
                    <td>Maj Category</td>    
                    <td>Mid Category</td>    
                    <td>Min Category</td>    
                    <td>Cls_cat</td>    
                    <td></td>
                </tr>
        </table>
       
       <div id="dvscroll_RFQ"  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px ; text-align:center;" onscroll="SetScrollPos(this)">
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                 <col style="width:3%;" />
                <col style="width:3%;" />
                <col style="width:6%;" />
                <col style="width:20%;"/>
                <col style="width:6%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:16%; text-align:left;" />
                <col style="width:10%; text-align:left;" />
                <col style="width:2%; " />
            </colgroup>
            <asp:Repeater ID="RptAccount" runat="server">
                <ItemTemplate>
                        <tr id='tr<%#Eval("AccountID")%>' class='<%#(Convert.ToInt32(Eval("AccountID"))!=SelectedMidId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("AccountID")%>);' lastclass='alternaterow'  >
                        <td>
                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/Edit.jpg" OnClick="imgEdit_OnClick" Visible='<%#authPR.IsUpdate %>' />                            
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%#authPR.IsDelete %>' />
                        </td>
                         
                        <td style="text-align:left;">
                            <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                            <asp:HiddenField ID="hfAccountID" runat="server" Value='<%# Eval("AccountID") %>' />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                            <asp:HiddenField ID="hfActive" runat="server" Value='<%# Eval("Active") %>' />
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblMajCatID" runat="server" Text='<%# Eval("MajCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMajCatID" runat="server" Value='<%# Eval("MajCatID") %>' />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblMidCatID" runat="server" Text='<%# Eval("MidCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMidCatID" runat="server" Value='<%# Eval("MidCatID") %>' />
                        </td>
                        
                        <td align="left">
                            <asp:Label ID="lblMinCatID" runat="server" Text='<%# Eval("MinCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMinCatID" runat="server" Value='<%# Eval("MinCatID") %>' />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblcls_cat" runat="server" Text='<%# Eval("cls_cat ") %>' Visible="false"></asp:Label>
                            <%# Eval("cls_catName")%>
                        </td>
                        <td></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr id='tr<%#Eval("AccountID")%>' class='<%#(Convert.ToInt32(Eval("AccountID"))!=SelectedMidId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("AccountID")%>);' lastclass='alternaterow'>
                       <td>
                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/Edit.jpg" OnClick="imgEdit_OnClick" Visible='<%#authPR.IsUpdate %>' />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%#authPR.IsDelete %>' />
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                            <asp:HiddenField ID="hfAccountID" runat="server" Value='<%# Eval("AccountID") %>' />
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                            <asp:HiddenField ID="hfActive" runat="server" Value='<%# Eval("Active") %>' />
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblMajCatID" runat="server" Text='<%# Eval("MajCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMajCatID" runat="server" Value='<%# Eval("MajCatID") %>' />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblMidCatID" runat="server" Text='<%# Eval("MidCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMidCatID" runat="server" Value='<%# Eval("MidCatID") %>' />
                        </td>
                        
                        <td align="left">
                            <asp:Label ID="lblMinCatID" runat="server" Text='<%# Eval("MinCat") %>'></asp:Label>
                            <asp:HiddenField ID="hfMinCatID" runat="server" Value='<%# Eval("MinCatID") %>' />
                        </td>
                        <td align="left">
                            <asp:Label ID="lblcls_cat" runat="server" Text='<%# Eval("cls_cat ") %>' Visible="false"></asp:Label>
                            <%# Eval("cls_catName")%>
                        </td>
                        <td></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
       </div>
       
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <tr>
                <td align="right">
                    <asp:Button ID="imgAddNew" runat="server" Text="ADD" OnClick="imgAddNew_OnClick" CssClass="btn" />
                </td>
            </tr>
       </table>
       
       
        <div style="position:absolute;top:50px;left:50px; height :300px; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
    <div style="position:absolute;top:50px;left:50px; height :300px; width:100%;background-color:Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:1150px; height:240px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px;  ;opacity:1;filter:alpha(opacity=100)">
                    
            <%---------------------------------------------------%>
            
                <div id="DivUpdate"  runat="server"  style="width:100%;">
        <fieldset>
             <legend style="font-weight:bold;color:#4371a5; font-family:Arial;" >Add Account</legend>
             <table cellspacing="0" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;margin:15px;">
                <tr align="center" >
                    <td align="right">Account Number</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtAccNo" runat="server" MaxLength="100" ></asp:TextBox>
                     </td>
                     <td align="right">Account Name</td>
                    <td align="left">
                        <asp:TextBox ID="txtAccName" runat="server"  Width="150" ></asp:TextBox>
                    </td>
                    <td align="right">Active</td>
                    <td align="left">
                        <asp:DropDownList ID="ddlActive" runat="server" Width="220px" >
                            <asp:ListItem Text="Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td align="right">Major category</td>
                    <td align="left">
                        <asp:DropDownList id="ddlMajCat" runat="server" ></asp:DropDownList>
                    </td>
                    <td align="right">Mid category</td>
                    <td align="left">
                        <asp:DropDownList id="ddlMidCat" runat="server" Width="220px" ></asp:DropDownList>
                    </td>
                    <td>Min Category</td>
                    <td align="left">
                        <asp:DropDownList id="ddlMinCat" runat="server" Width="220px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Cls Cat</td>
                    <td align="left">
                        <%--<asp:TextBox ID="txtCls_cat" runat="server" MaxLength="5" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlClsCat" runat="server" ></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6" style="padding-right:20px;text-align:right;" >
                        
                          <asp:button ID="imgUpdate" runat="server" TExt="Save" OnClick="imgUpdate_OnClick" CssClass="btn" />
                          <asp:button ID="imgCalcel" runat="server" TExt="Cancel" OnClick="imgCalcel_OnClick" CssClass="btn" />
                          <asp:HiddenField ID="hfAccID" runat="server" Value="" />
                    
                    </td>
                </tr>
        </table>
        </fieldset>
       </div> 
             <asp:Label ID="lblmsg" runat="server" CssClass="PageError" style="float:left;" ></asp:Label>     
            
            <%-----------------------------------------------------------%>
            
            
        
        <br /><br />
        <br /><br />
    </div> 
    </center>
    </div>
      
      </ContentTemplate>
    </asp:UpdatePanel> 
       
    </td>
    </tr>
    </table>
    </div>
    
</div> 
    <script type="text/javascript" >
var Id='tr' + document.getElementById('hfPRID').value;
lastSel=document.getElementById(Id);
</script>
</asp:Content>
 




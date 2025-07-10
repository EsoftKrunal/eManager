<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMinorMaster.aspx.cs" Inherits="AccountMinorMaster" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/Common.js"></script>
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
    
    <style type="text/css">
        .PageError
        {
        	color:Red;
        	font-family:Verdana;
        	font-weight:bold;
        	font-size:11px;
        }
    </style>
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
            Minor Accounts
        </div>
       <asp:UpdatePanel ID="UP" runat="server" >
            <ContentTemplate>
            
       <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:120px;" />
                <col  />
                <col style="width:180px; text-align:left;" />
                <col style="width:17px; " />
                <tr align="left" class= "headerstylegrid">
                    <td>
                        Edit
                    </td>
                    <td>
                        Delete
                    </td>
                    <td>
                        Minor Cat ID</td>
                    <td>
                        Minor Cat</td>
                    <td>
                        Minor Seq No</td>
                        <td></td>
                </tr>
        </table>
       
       <div id="dvscroll_RFQ"  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px ; text-align:center;" onscroll="SetScrollPos(this)">
       <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <colgroup>
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:120px;" />
                <col  />
                <col style="width:180px; text-align:left;" />
                <col style="width:17px; text-align:left;" />
            </colgroup>
            <asp:Repeater ID="RptMinCat" runat="server">
                <ItemTemplate>
                        <tr id='tr<%#Eval("MinCatID")%>' class='<%#(Convert.ToInt32(Eval("MinCatID"))!=SelectedMidId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("MinCatID")%>);' lastclass='alternaterow'>
                        <td>
                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/Edit.jpg" OnClick="imgEdit_OnClick" Visible='<%# authPR.IsUpdate %>' />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%# authPR.IsDelete %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblMinCatID" runat="server" Text='<%# Eval("MinCatID") %>'></asp:Label>
                            
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblMinorCat" runat="server" Text='<%# Eval("MinorCat") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMinSeqNo" runat="server" Text='<%# Eval("MinSeqNo") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr id='tr<%#Eval("MinCatID")%>' class='<%#(Convert.ToInt32(Eval("MinCatID"))!=SelectedMidId)?"alternaterow":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("MinCatID")%>);' lastclass='alternaterow'>
                       <td>
                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/Edit.jpg" OnClick="imgEdit_OnClick"  Visible='<%# authPR.IsUpdate %>'/>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="imgDel_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%# authPR.IsDelete %>' />
                        </td>
                       <td>
                            <asp:Label ID="lblMinCatID" runat="server" Text='<%# Eval("MinCatID") %>'></asp:Label>
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblMinorCat" runat="server" Text='<%# Eval("MinorCat") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMinSeqNo" runat="server" Text='<%# Eval("MinSeqNo") %>'></asp:Label>
                        </td>
                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
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
       
       
       
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvConfirmCancel" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:920px; height:100px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                    
            <%---------------------------------------------------%>
            
                <div id="DivUpdate"  runat="server"  style="width:100%;"  >
        <fieldset>
            <legend style="font-weight:bold;color:#4371a5; font-family:Arial;" >Add Minor Account</legend>
             
             <table cellspacing="2" border="0" cellpadding="4" style="width:100%;border-collapse:collapse; margin:15px;">
                <tr align="center" >
                    <td align="right"  >Minor Cat</td>
                    <td style="text-align:left;">
                        <asp:TextBox ID="txtMinCat" runat="server" Width="300px" MaxLength="100" ></asp:TextBox>
                     </td>
                     <td align="right">Minor Seq No</td>
                    <td align="left">
                        <asp:TextBox ID="txtMinSeqNo" runat="server" MaxLength="5" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox>
                    </td>
                    
                    <td >
                          <asp:button ID="imgUpdate" runat="server" Text="Save" OnClick="imgUpdate_OnClick" CssClass="btn" />
                          <asp:button ID="imgCalcel" runat="server" Text="Cancel" OnClick="imgCalcel_OnClick" CssClass="btn" />
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
 




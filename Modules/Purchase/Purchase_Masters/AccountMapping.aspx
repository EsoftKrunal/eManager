<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountMapping.aspx.cs" Inherits="AccountMapping" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <title>EMANAGER</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../JS/Common.js"></script>
     <script type="text/javascript">
        function SelectCheckBox(Obj)
        {
            var x= Obj.childNodes[0].childNodes[0];
            if(x.checked)
            {
                x.checked=false;
                Obj.setAttribute(CSSName, "alternaterow");
            }
            else
            {
                x.checked=true;
                Obj.setAttribute(CSSName, "selectedrow");
             }
            
        }
        function ShowAccountReport()
        {
            
        }
     </script>
     
    <script type="text/javascript" >
        var lastSel=null;
        function Selectrow(trSel, prid) 
        {
            if(lastSel==null)
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel=trSel;
                //document.getElementById('hfPRID').value = prid;
            }
            else
            {
                if(lastSel.getAttribute("Id")==trSel.getAttribute("Id")) // clicking on same row
                {   
                    trSel.setAttribute(CSSName, "selectedrow");
                    lastSel=trSel;
                    document.getElementById('ctl00_ContentMainMaster_hfPRID').value = prid;
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
    <uc2:Registers runat="server" ID="Registers1" />  
      <div class="text headerband">
           Account Mapping
        </div>
    <asp:UpdatePanel ID="UP1" runat="server" >
        <ContentTemplate>
            <div style="border:2px solid #4371a5;" >
    <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
    <tr>
    <td>
       <asp:HiddenField ID="hfPRID" runat="server" Value="0" />  
       
       <table cellpadding="4" cellspacing="4" border="1" width="100%" rules="all" >
        <col width="300px" />
        <col width="200px" />
        <col width="50px" />
        <col width="200px" />
        <col />
               <tr>
                <td colspan="5">
                    <table cellpadding="2" cellspacing="2" border="1" width="100%" rules="rows" >
                        <col width="310px" />
                        <col width="310px" />
                        <col />
                        <tr class= "headerstylegrid">
                            <td align="left" >Type</td>
                            <td align="left" >Department</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td >
                                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDep" runat="server" OnSelectedIndexChanged="ddlDep_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="imgPring" runat="server" Text="Print" OnClick="imgPring_OnClick" style="float:right;" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <b> Available Account Codes   
                    <asp:Label ID="lblAvlAccCode" runat="server" ></asp:Label>
                    </b>
                </td>
                <td></td>
                <td colspan="2">
                    <b> Selected Account Codes
                    <asp:Label ID="lblSelAccCode" runat="server" ></asp:Label>
                    </b>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 328px; width:500px;">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%" rules="rows">
                            <colgroup>
                        <%--<col width="30px" />--%>
                        <col width="50px" />
                        <col />
                            </colgroup>
                        <asp:Repeater ID="rptAvailableAccCode" runat="server" >
                            <ItemTemplate>
                                <tr onclick="SelectCheckBox(this);" class="alternaterow" >
                                    <td >
                                        <asp:CheckBox ID="chkSelect" runat="server"  />
                                    </td>
                                    <td> 
                                        <%#Eval("AccountNumber") %> 
                                        <asp:HiddenField ID="hfAccID" runat="server" Value='<%#Eval("AccountID") %>' />
                                    </td>
                                    <td> <%#Eval("AccountName") %> </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                </td>
                
                <td style="vertical-align:middle; text-align:center; width:30px;">
                
                    <asp:ImageButton ID="imgRight" runat="server" OnClick="imgRight_OnClick" ImageUrl="~/Modules/HRD/Images/right_24.png" />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="imgLeft" runat="server" OnClick="imgLeft_OnClick" ImageUrl="~/Modules/HRD/Images/left_24.png" />
                </td>
                
                
                <td colspan="2">    
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 328px;">
                        <table cellpadding="2" cellspacing="2" border="0" width="100%" rules="rows">
                            <colgroup>
                        <col width="30px" />
                        <col width="100px" />
                        <col width="100px" />
                        <col width="50px" />
                        <col />
                        </colgroup>
                        <asp:Repeater ID="rptSelectedAcc" runat="server" >
                            <ItemTemplate>
                                <tr onclick="SelectCheckBox(this)" class="alternaterow" > 
                                    <td>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </td>
                                    <td>
                                         <%#Eval("PrtypeDesc") %> 
                                         <asp:HiddenField ID="hfAccNum" runat="server" Value='<%#Eval("AccountNumber") %>' />
                                         <asp:HiddenField ID="hfDeptAcctID" runat="server" Value='<%#Eval("DeptAcctID") %>' />
                                     </td>
                                    <td> <%#Eval("DeptName") %> </td>
                                    <td> <%#Eval("AccountNumber") %> </td>
                                    <td> <%#Eval("AccountName") %> </td>
                                    
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    </div>
                
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblmsg" runat="server" CssClass="error" ></asp:Label>
                </td>
            </tr>
       </table>
       
    </td>
    </tr>
    </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</div> 
</asp:Content>
 




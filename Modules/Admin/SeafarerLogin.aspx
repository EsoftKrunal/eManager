<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeafarerLogin.aspx.cs" Inherits="SeafarerLogin"  MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <script type="text/javascript" src="JS/Common.js" ></script>
        <title>User Management</title>
    <link rel="stylesheet" href="../../css/app_style.css" />
     <link rel="stylesheet" href="../HRD/Styles/StyleSheet.css" />
     <link rel="stylesheet" href="../../css/style.css" />
    <style type="text/css" >
/* AutoComplete item */
.AutoCompleteExtender_CompletionList 
{
background-color : lightgray;
color : windowtext;
padding : 1px;
font-size: small;
background-color:Gray;
list-style-type: none ; 
border :solid 1px gray;
height :100px;
overflow-y:scroll;  
overflow-x:hidden; 
/*creates border with
autocomplete_completionListElement
background-color*/
}

/*AutoComplete flyout */
.AutoCompleteExtender_CompletionListItem 
{ 
text-align : left;
background-color:White;
list-style-type: none ; 
color:black;
}

/* AutoComplete highlighted item */
.AutoCompleteExtender_HighlightedItem
{
background-color: Silver;
text-align :left; 
color: blue;
list-style-type: none ; 
}
</style> 
<script type="text/javascript" >
    function move() {
        var cd = event.keyCode;
        if (cd == 13) {
            //document.getElementById('btnShow').focus();
            //document.getElementById('btnShow').click();
        }
    }

    function back() {
        history.back();
    }
</script>

<script type="text/javascript" >
    var lastSel = null;
    function Selectrow(trSel, prid) {
        if (lastSel == null) {
            trSel.setAttribute(CSSName, "selectedrow");
            lastSel = trSel;
            document.getElementById('hfPRID').value = prid;
        }
        else {
            if (lastSel.getAttribute("Id") == trSel.getAttribute("Id")) // clicking on same row
            {
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfPRID').value = prid;
            }
            else // clicking on ohter row
            {
                lastSel.setAttribute(CSSName, lastSel.getAttribute("lastclass"));
                trSel.setAttribute(CSSName, "selectedrow");
                lastSel = trSel;
                document.getElementById('hfPRID').value = prid;
            }
        }
    }
</script>
<style type="text/css">
.selectedrow
{
	background-color : #de7f04;
	color :White; 
	cursor:pointer;
}

</style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align:center">
   <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="up1" runat="server" >
        <ContentTemplate>--%>
         <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" >&nbsp;Seafarer Login (User Management)</td>
            </tr>
             
        </table> 
    
    <table cellspacing="0" border="0" cellpadding="0" style="width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <asp:HiddenField ID="hfPRID" runat="server" />
            <%--<strong class="pagename" >User Management</strong>--%>
                <mtm:Message runat="server" ID="Msgbox" /> 
            </td>
         </tr>
        </table>
       </td>
      </tr>
      <tr>
        <td style="padding-top:5px;">
            
            <table cellpadding="2" cellspacing="0" width="100%"  rules="all" style="border-collapse:collapse;">   
                
                <tr style="font-weight:bold;" class= "headerstylegrid">
                    <td style="width:180px;">Crew #</td>
                    <td style="width:180px;">Name</td>
                    <td style="width:120px;">Rank</td>
                    <td style="width:250px;">Vessel</td>
                    <td style="width:120px;" >Crew Status</td>
                    <td style="width:120px;">Login Status</td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCrewNumber" runat="server" CssClass="input_box" MaxLength="6" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input_box" MaxLength="20" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList id="ddlRank" runat="server" Width="100px" ></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList id="ddlVessel" runat="server" Width="200px" ></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList id="ddlCrewStatus" runat="server" Width="100px"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList id="ddlLoginStatus" runat="server" Width="100px" >
                            <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Active" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="In Active" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_OnClick" />
                        <%--<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_OnClick"  />--%>
                        <asp:Button runat="server" id="Button1" UseSubmitBehavior="false" CssClass="btn" Text="<< Go Back" OnClientClick="return parent.DoPost(5);" ></asp:Button> 
                        
                    </td>
                </tr>
            </table>
            
        </td>
      </tr>
      <tr>
        <td >
            <br />
            <div   >
            <table cellpadding="0" cellspacing ="0" width="100%" border="1"  style="border-collapse:collapse;">
            <tr class= "headerstylegrid" >
            <td style=" width :3%"></td>
            <td style=" width :8%">Crew #</td>
            <td style=" width :25%">Name</td>
            <td style=" width :8%">Rank</td>
            <td style=" width :32%">Vessel</td>
            <td style=" width :10%" >Crew Status</td>
            <td style=" width :10%" >Login Status</td>
            <td style=" width :4%" ></td>
           
            </tr>
            </table>
            </div>
            <asp:Panel runat="server" ID="pnlList" Width="100%" style="overflow-x:hidden; overflow-y:scroll; height:230px;" >
            <table  cellpadding ="0" cellspacing ="0" width="100%" border="1" style="border-collapse:collapse;" >
            <asp:Repeater runat="server" ID="rpt_Users">
                <ItemTemplate >
                    <tr id='tr<%#Eval("CrewID")%>' class='<%#(Common.CastAsInt32(Eval("CrewID"))!=SelectedCrewId)?"":"selectedrow"%>' onclick='Selectrow(this,<%#Eval("CrewID")%>);' lastclass='row' a='<%=SelectedCrewId %>' >
                    
                    <td style=" width :3%"><asp:ImageButton runat="server" ImageUrl="~/images/edit.jpg" ID="imgResetPass" CommandArgument='<%# Eval("CrewNumber")%>' OnClick="imgResetPass_OnClick" /></td>
                    <td style=" width :8%; text-align : center ">&nbsp;<%# Eval("CrewNumber")%><asp:HiddenField ID="hfLoginStatus" runat="server" Value='<%# Eval("LoginStatus1")%>' />
                        <asp:HiddenField ID="hfCrewID" runat="server" Value='<%# Eval("CrewID")%>' />
                        <asp:HiddenField ID="hfCrewNumber" runat="server" Value='<%# Eval("CrewNumber")%>' />
                    </td>
                    <td style=" width :25%; text-align : left">&nbsp;<%# Eval("UName")%></td>
                    <td style=" width :8%; text-align : left">&nbsp;<%# Eval("Rank")%></td>
                    <td style=" text-align : left;width :32%;">&nbsp;<%# Eval("Vessel")%></td>
                    <td style=" width :10%">&nbsp;<%# Eval("CrewStatus")%></td>
                    <td style=" width :10%">&nbsp;<%# Eval("LoginStatus")%></td>
                    <td style=" width :3%">
                        <asp:CheckBox ID="chkActive" runat="server"  AutoPostBack="true" OnCheckedChanged="chkActive_OnCheckedChanged" Checked='<%# (Eval("LoginStatus1").ToString()=="True")%>'     />
                        
                    </td>
                </tr>
                </ItemTemplate>
            </asp:Repeater> 
            </table>
            </asp:Panel>
        </td>
      </tr>
     </table>
      
    </div>
    
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <div id="divReseetPass" runat="server"  visible="false" style="margin:auto; margin-top:15px;width:900px;" >
        <table>
            <tr>
                <td width="20%">
 New Password &nbsp;&nbsp;
            </td>
                <td width="80%">
 <asp:TextBox ID="txtNewPass" runat="server" MaxLength="15" ></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Confirm Password &nbsp;&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPass" runat="server" MaxLength="15"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                        <asp:Button ID="btnResetPass" runat="server" Text="Reset Pass" CssClass="btn"  OnClick="btnResetPass_OnClick" Width="100px"/>
                <asp:Button ID="btnCancelPass" runat="server" Text="Cancel" CssClass="btn"  OnClick="btnCancelPass_OnClick" Width="80px"/>
                </td>
            </tr>
            
        </table>
              
                
            
            </div>
    
    
</asp:Content>
<%--OnRowDeleting="GridView_UserLogin_Row_Deleting"--%>
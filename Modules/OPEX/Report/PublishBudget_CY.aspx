<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishBudget_CY.aspx.cs" Inherits="PublishBudget_CY" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title>EMANAGER</title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div class="text headerband">
        Publish Budget
    </div>
    <div style=" border:solid 1px gray; height:405px;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"  AsyncPostBackTimeout="600"></asp:ToolkitScriptManager>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress2">
            <ProgressTemplate>
                <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                    <center>
                    <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                    <img src="../HRD/Images/loading.gif" alt="loading"> Loading ...
                    </div>
                    </center>
                </div>
            </ProgressTemplate> 
    </asp:UpdateProgress>  
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
        <div>
            <div style="padding:5px"> 
                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true"  width="250px"></asp:DropDownList> &nbsp;
                <asp:RequiredFieldValidator runat="server" ID="fd" ControlToValidate="ddlCompany" ErrorMessage="*"></asp:RequiredFieldValidator>  &nbsp;
                <asp:Button runat="server" ID="btnPublish" Text="Publish Budget Forecast" OnClick="btnPublish_Click" CssClass="btn"  />
                <div style="display:none">
                <asp:DropDownList runat="server" ID="ddlShip"></asp:DropDownList>
                </div>
            </div>
            <div>
            <table cellpadding="3" cellspacing="0" border="1" width="100%">
            <tr class= "headerstylegrid">
                <td>File Name</td>
                <td>Published By</td>
                <td style="width:100px; text-align:center">Published On</td>
                <td style="width:100px; text-align:center">Download</td>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
            <ItemTemplate>
            <tr style="">
                <td><%#Eval("FileName")%></td>
                <td><%#Eval("PublishedBy")%></td>
                <td style="text-align:center"><%#Eval("PublishedOnF")%></td>
                <td style="text-align:center">
                <a href='http://app.mtmshipmanagement.com/pos/publish_ny/<%#Eval("FileName")%>.zip' target="_blank">
                    <img style="border:none" src="../HRD/Images/icon_zip.gif" title="Download File"/>
                </a>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>

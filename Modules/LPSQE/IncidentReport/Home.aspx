<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="eReports_Home" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>eMANAGER</title>
    <script src="JS/jquery.min.js" type="text/javascript"></script>
    <script src="JS/KPIScript.js" type="text/javascript"></script>
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="CSS/KPIStyle.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="text headerband">
        Incident Reports
    </div>
    <div style="font-family:Arial;font-size:12px;">   
     <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width:30%; border-right:solid 1px #f2f2f2">
                <table border="0" cellpadding="0" cellspacing="0" style="" width="100%">
                    <%--<tr>
                         <td style="text-align:center; font-weight:bold; vertical-align:middle; background-color:#00ABE1; color:White; height:25px;">Inspections List</td>
                    </tr>--%>
                    <tr>
                     <td>
                         <div>
                         <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Conditional">
                         <ContentTemplate>
                         <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse; " >
                         <tr >
                         <td style="text-align:center; padding:5px;" >
                         Search Form : <asp:TextBox runat="server" ID="txtFormNoName" Width="70%" MaxLength="50" OnTextChanged="txtFormNoName_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                         </td>
                         </tr>
                         <%--<tr >
                         <td style="text-align:center; padding:8px;" >
                                    <asp:LinkButton runat="server" ID="LinkButtonA" Text="A" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonB" Text="B" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonC" Text="C" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonD" Text="D" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonE" Text="E" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonF" Text="F" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonG" Text="G" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonH" Text="H" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonI" Text="I" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonJ" Text="J" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonK" Text="K" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonL" Text="L" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonM" Text="M" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonN" Text="N" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonO" Text="O" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonP" Text="P" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonQ" Text="Q" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonR" Text="R" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonS" Text="S" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonT" Text="T" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonU" Text="U" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonV" Text="V" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonW" Text="W" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonX" Text="X" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonY" Text="Y" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="LinkButtonZ" Text="Z" OnClick="lnkForm_Click" CssClass='alpha' ></asp:LinkButton>
                                    </td>
                         </tr>--%>
                         </table>
                         
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col />
                                    <col style="width:60px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr class= "headerstylegrid">
                                    <td style="color:White;vertical-align:middle;">&nbsp;Reports List</td>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Analysis</td>
                                    <td></td>
                                </tr>
                                
                                </table>
                           </div>
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 395px ; text-align:center;" class="ScrollAutoReset" id='dv_FRMS'>
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" class="newformat">
                                <colgroup>
                                    <col />
                                    <col style="width:60px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tbody>
                                <asp:Repeater ID="rptFormList" runat="server">
                                    <ItemTemplate>
                                           <tr runat="server" visible='<%#(lastGroupName != Eval("ReportGroupName").ToString().Trim())%>' >
                                           <td colspan="3" style='background-color:#CCE6FF; text-align:left; padding:4px; font-size:13px;'>&bull; &nbsp;<%#Eval("ReportGroupName")%></td>
                                           </tr>
                                           <tr>
                                            <td style="text-align:left"><asp:LinkButton ID="lnkFormNo" runat="server" OnClick="lnkFormNo_OnClick" CommandArgument='<%#Eval("FormNo")%>' Text='<%#Eval("FormName").ToString()%>' Font-Underline="false"></asp:LinkButton></td>
                                            <td><span style="display:none"><%# lastGroupName = Eval("ReportGroupName").ToString()%></span><asp:ImageButton ID="btnReport" ImageUrl="~/Modules/HRD/Images/log.gif" OnClick="btnReport_Click" CommandArgument='<%#Eval("FormNo")%>' ToolTip="Analysis Report" runat="server" /> </td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                           <tr runat="server" visible='<%#(lastGroupName != Eval("ReportGroupName").ToString().Trim())%>' >
                                           <td colspan="3" style='background-color:#CCE6FF; text-align:left; padding:4px; font-size:13px;'>&bull; &nbsp;<%#Eval("ReportGroupName")%></td>
                                           </tr>
                                           <tr style="background-color:#FFF5E6">
                                            <td style="text-align:left"><asp:LinkButton ID="lnkFormNo" runat="server" OnClick="lnkFormNo_OnClick" CommandArgument='<%#Eval("FormNo")%>' Text='<%#Eval("FormName").ToString()%>' Font-Underline="false" ></asp:LinkButton></td>
                                            <td><span style="display:none"><%# lastGroupName = Eval("ReportGroupName").ToString()%></span><asp:ImageButton ID="btnReport" ImageUrl="~/Modules/HRD/Images/log.gif" OnClick="btnReport_Click" CommandArgument='<%#Eval("FormNo")%>' ToolTip="Analysis Report" runat="server" /></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table> 
                           </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
            <iframe src="" runat="server" id="frm_Details" width="100%" frameborder="0" scrolling="no" height="485px">
            
            </iframe>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

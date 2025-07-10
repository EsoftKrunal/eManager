<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCrewCheckList1.aspx.cs" Inherits="Applicant_ViewPromotionChecklist1" %>

<!DO<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .rem
    {
        border:solid 1px #c2c2c2;
        font-size:11px;
    }
    .rem:focus
    {
        background-color:#FFFFCC;
    }
    body
    {
        font-family:Calibri; 
        font-size:15px;
    }
    h1
    {
        font-size:18px;
        background-color:#d2d2d2;
        padding:5px;
        margin:0px;
    }
     h2
    {
        font-size:17px;
        background-color:#d2d2d2;
        margin:0px;
    }
    .data
    {
        font-size:12px;
    }
    .dataheader
    {
        font-size:14px;
        background-color:#FFE0C2;
    }
    a img
    {
        border:none;
    }
    .newbtn
    {
        border:solid 1px #c2c2c2;
        background-color:Orange;
        padding:5px;
        width:100px;
        margin-top:2px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="text-align: center; width:98%; border:solid 1px black;">
        <div class="text headerband"> <asp:Literal ID="litPageHeading" runat="server"></asp:Literal> </div>  
        <table width='100%' style="text-align:left" >
        <colgroup>
        <col width='120px' />
        <col />
        <col width='120px' />
        <col />
        </colgroup>
        <tr>
            <td>Crew Number&nbsp; : </td>
            <td><asp:Label runat="server" ID="lblID"></asp:Label>  </td>
            <td>Crew Name : </td>
            <td>  <asp:Label runat="server" ID="lblName"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Nationality : </td>
            <td>  <asp:Label runat="server" ID="lblNationality"></asp:Label>  </td>
            <td>DOB :</td>
            <td>  <asp:Label runat="server" ID="lblDOB"></asp:Label>  </td>
        </tr>
         <tr>
            <td>Planned Rank : </td>
            <td>  <asp:Label runat="server" ID="lblRank"></asp:Label>  </td>
            <td>Planned Vessel :</td>
            <td>  <asp:Label runat="server" ID="lblVName"></asp:Label>  </td>
        </tr>
         </table>
        
        <table width='100%' style="text-align:left; " cellspacing='0' cellpadding="3"  >
                <colgroup>
                    <col width='40px' />                 
                    <col width='350px' />     
                    <col />        
			<col width='50px' />  
                    
                    <col width='170px' />     
                    <col width='60px' />                 
                    <col width='120px' />
                </colgroup>
                <tr class= "headerstylegrid">
                    <td>Sr#</td>
                    <td style="text-align:left">Checklist</td>
                    
                    <td>Comments</td>
<td>Verify</td>
                    <td>Verified By/On</td>
                    <td style="text-align:center;">Download</td>
                    <td style="text-align:center;">Upload</td>
                </tr>
            <asp:UpdatePanel ID="UP" runat="server">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="rptPromotionChecklist">
                <ItemTemplate>
                <tr class='dataheader'>
                    <td style="text-align:left;"><%#Eval("Sr")%></td>
                    <td style="text-align:left">&nbsp;<%#Eval("CheckListItemName") %></td>
                    
                    <td>
                        <asp:TextBox ID="txtComments" runat="server" Width="99%" Text='<%#Eval("Comments")%>'></asp:TextBox>
                    </td>
<td>
                        <asp:CheckBox ID="chKVerify" runat="server" Checked='<%# (Eval("Verified").ToString()=="1")%>'/>
                        <asp:HiddenField ID="hfdTableID" runat="server" Value='<%#Eval("TableID")%>' />
                        <asp:HiddenField ID="hfdChecklistItemID" runat="server" Value='<%#Eval("ID")%>' />
                    </td>
                    <td>
                        <%#Eval("VerifiedBy")%>/<%#  Common.ToDateString(Eval("VerifiedOn"))%></td>
                    <td style="text-align:center;">
                        <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" OnClick="btnDownload_OnClick" CommandArgument='<%#Eval("TableID") %>' Visible='<%#(Eval("FileName").ToString()!="" ) %>' style="cursor:pointer;" />
                    </td>
                    <td style="text-align:center;">
                        <asp:Button ID="btnUploadPopup" runat="server" Text="Upload" OnClick="btnUploadPopup_OnClick" CssClass="btn" />
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                </ContentTemplate>  
              <%--  <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="click"/>
</Triggers>--%>
            </asp:UpdatePanel>

           
                
            </table>
    </div>
    <div style="text-align: center; width:90%; padding:5px; text-align:right">
    <asp:Label runat="server" ID="lblMess" style="float:left" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Button runat="server" CssClass="btn" Text="Save" ID="btnSave" onclick="btnSave_Click" /> &nbsp;&nbsp;
        <asp:Button runat="server" CssClass="btn" Text="Close" ID="btnClose" OnClientClick='window.close();' />
    </div>
    </center>


        <%------------------------------------------------------------------------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="divFileUpload" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:800px; padding :0px; text-align :center;background : white; z-index:150;top:20px; border:solid 3px #d8e9f5;">
                <center >
                    <div style="padding:5px; " class="text headerband"><b>Upload File</b></div>
                    <table cellpadding="5" cellspacing="5" border="0" width="0">
                        <tr>
                            <td>
                                <asp:FileUpload ID="UplaodFile" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnUploadFile" runat="server" Text="Upload" OnClick="btnUploadFile_OnClick"  CssClass="btn"/>
                            </td>
                        </tr>

                    </table>
                    <div style="text-align:center;padding:5px;margin-top:30px;">
                            <asp:Label ID="lblUPloadFile" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnCloseFileUPloadPopup" runat="server" Text="Close" OnClick="btnCloseFileUPloadPopup_OnClick"  CssClass="btn" />
                    </div>
                </center>
            </div>
            </center>
        </div>
    </form>
</body>
</html>

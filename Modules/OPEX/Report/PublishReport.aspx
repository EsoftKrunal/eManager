<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishReport.aspx.cs" Inherits="PublishReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="JS/Common.js"></script>
     <script type="text/javascript" src="JS/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="JS/BudgetScript.js"></script>
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/Budgetstyle.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" >
    function OpenReport(Mnth, yr)
    {
        var ob=document.getElementById('ddlCompany');
        var Cocode=ob.options[ob.selectedIndex].value;
        ob=document.getElementById('ddlyear');
        var curfyyr=ob.options[ob.selectedIndex].value;
        window.open('previewreport.aspx?cocode=' + Cocode + '&yr=' + yr + '&mnth=' + Mnth + '&CurFinYear=' + curfyyr);
    }
    function ConfirmPBox()
    {
        var list = document.getElementById('ddlPublishPeriod');
        var listText = list.options[list.selectedIndex].text;
        return window.confirm('Are you sure to publish the report for ' + listText + ' ?');  
    }
    function ConfirmCBox()
    {
        return window.confirm('Are you sure to close the report for ' + document.getElementById('lblMonth').innerHTML + '?');  
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager> 
    <div style="font-family:Arial, Helvetica, sans-serif;font-size:12px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        
        <tr>
        
            <td>
               <%-- <table cellpadding="0" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="Text headerband"  colspan="5" >Publish Reports
                       <asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" Visible="false"  ImageUrl="~/Modules/HRD/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " CausesValidation="false"/></td>
                </tr>
                </table>--%>
                <table cellpadding="0" cellspacing="1" width="100%" border="1" style="border-collapse:collapse;">
                <tr>
                <td style="text-align:left;">
           
            <table width="100%" cellpadding="3" cellspacing="0"  >
            
            <tr>
            <td width="300px" style="text-align:center;display:none;vertical-align:top;" >
                <fieldset>
            <legend style="color:Purple">Publish Report</legend> 
            <br />
            <br />
            <asp:DropDownList runat="server" ID="ddlPublishPeriod" Font-Size="Larger" Width="145px"></asp:DropDownList>
            <br /><br /> 
            <asp:Button runat="server" Text="Publish Report" Font-Bold="true" ID="btnPubLish" Height="50px" Width="150px" OnClientClick="return ConfirmPBox();" OnClick="btnPublishReport_Click" CssClass="btn" />
             <br /><br /> 
             <center >
             <div style="height :30px; width :50px; padding :5px; text-align :center" >
        
             </div>
             </center>
            </fieldset>
            <br/>
            
                <fieldset>
                <legend style="color:Purple"><span lang="en-us">Close</span> Report<span 
                        lang="en-us"> Period</span></legend> 
                <br />
                    <asp:Label ID="lblMonth" runat="server" Font-Bold="true" Font-Names="MS" 
                        Font-Size="17px" ForeColor="Purple"></asp:Label>
                    <br />
                <br />
                    <asp:Button ID="btnClose" runat="server" Font-Bold="true" Height="50px" 
                        OnClick="btnCloseReport_Click" OnClientClick="return ConfirmCBox();" 
                        Text="Close Report" Width="150px" CssClass="btn" />
                    <br />
                 <br /><br /> 
                </fieldset>
                <br/>
                <asp:Label runat="server" ID="Label2" ForeColor="Red"></asp:Label>
            </td>
            <td style ="text-align :left">
                 <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up2" ID="UpdateProgress2">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="../../HRD/Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>  
             <asp:UpdatePanel runat="server" ID="up2">
             <ContentTemplate>
             
             <fieldset>
            <legend style="color:Purple">Publish Reports </legend> 
            <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
            <div style =" padding:5px; font-weight:bold;">
                <table width="100%">
                    <tr>
                         <td style="width:125px;text-align:right;padding-right:10px;">
                             Select Company :  
                        </td>
                        <td style="text-align:left;padding-left:10px;">
                      <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Width="358px"  ></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCompany" ErrorMessage="Required." ></asp:RequiredFieldValidator>
                            <div style="float:right;margin-right:5px;">
                                <asp:Button ID="btnUpdateTransaction" runat="server"  Text="Update Transaction" CssClass="btn" CausesValidation="false" OnClick="btnUpdateTransaction_Click"/>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:125px;text-align:right;padding-right:10px;">
                           Financial  Year :
                        </td>
                        <td style="text-align:left;padding-left:10px;">
                       <asp:DropDownList ID="ddlyear" runat="server" Width="120px"  AutoPostBack="true" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"   ></asp:DropDownList>
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="height:425px;" class='ScrollAutoReset' id='tbl_Spares'>
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                    <thead>
            <tr class= "headerstylegrid" style="height:25px;" >
                <td style="width:7%;">Period</td>
                <td style="width:7%;">Year</td>
                <td style="width:4%">Closed</td>
                <td style="width:4%">Review</td>
                <td style="width:11%;">Publish Accounts</td>
		       <%-- <td style="width:17%;">Publish PO Commitments</td>
                <td style="width:11%;">Infrom Suptd.</td>
                <td style="width:17%;">Publish Comments</td>  --%>              
                <td style="width:11%;">Close Report</td>
                <td style="width:15%;">Published By/On</td>
                <td style="width:2%;">&nbsp;</td>
            </tr>
            </thead>
                    <tbody>
                         <div style="overflow-y:scroll;overflow-x:hidden;vertical-align:top;">
            <asp:Repeater ID="rptItems" runat="server">
                   <ItemTemplate>
                    <tr onmouseover="">
                        <td style="width:7%;"><%#Eval("rptPeriod")%>
                            <asp:HiddenField ID="hdnRptYear" runat="server" Value='<%#Eval("rptYear")%>' />
                        </td> 
                        <td style="width:7%;"><%#Eval("rptYear")%>
                        </td> 
                        <td style="width:4%;">
                        <img src="../../HRD/Images/Lock.png" style='display:<%#(Eval("perClosed").ToString().Trim()=="True") ?"":"none"%>'/> 
                        </td>
                        <td style="width:4%;">
                                <img src="../../HRD/Images/poanalysis.png" style='border:none; cursor:pointer;display:<%#(Eval("UpdatedBy").ToString().Trim()=="")?"none":""%>' onclick='OpenReport(<%#Eval("rptPeriod")%>, <%#Eval("rptYear")%>);'/>
                           <%-- display:<%#(Eval("rptLink").ToString().Trim()=="")?"none":""%>--%>
                        </td>
                        <td style="width:11%;"><asp:Button runat ="server" id="btnPublish" Width="100px" OnClick="btnGridPublish_Click" CommandArgument='<%#Eval("rptPeriod")%>' Text="Publish" rptyear='<%#Eval("rptYear")%>' CssClass="btn"/></td>
                       <%-- <td style="width:17%;"><asp:Button runat ="server" id="btnPublishPOComm" Width="170px" link='<%#Eval("rptLink")%>' OnClick="btnGridPublishPOComm_Click" CommandArgument='<%#Eval("rptPeriod")%>' rptyear='<%#Eval("rptYear")%>' Text="Publish PO Commitments" CssClass="btn"/></td>
                        <td style="width:11%;"><asp:Button runat ="server" id="btnInformSuptd" Width="120px" link='<%#Eval("rptLink")%>' OnClick="btnInformSuptd_Click" CommandArgument='<%#Eval("rptPeriod")%>' rptyear='<%#Eval("rptYear")%>' Text="Inform Suptd." CssClass="btn"/></td>
                        <td style="width:17%;"><asp:Button runat ="server" id="btnPublishComm" Width="150px" link='<%#Eval("rptLink")%>' OnClick="btnGridPublishComm_Click" CommandArgument='<%#Eval("rptPeriod")%>' rptyear='<%#Eval("rptYear")%>' Text="Publish Comments" CssClass="btn"/></td>--%>
                        <td style="width:11%;"><asp:Button runat ="server" id="btnClosure" Width="100px" OnClick="btnGridClosure_Click" CommandArgument='<%#Eval("rptPeriod")%>' Text="Close Now" CssClass="btn" rptyear='<%#Eval("rptYear")%>'
                            />
                            <asp:HiddenField ID="hdnRptClosed" runat="server" Value='<%#Eval("perClosed")%>' />
                            <asp:HiddenField ID="hdnRptPeriod" runat="server" Value='<%#Eval("rptPeriod")%>' />
                        </td>
                        <td style="text-align :center">
                            <%#Eval("UpdatedBy")%> <%#Eval("UpdateOn")%>
                        </td>
                    </tr>
                </ItemTemplate> 
            </asp:Repeater>
                             </div>
            </tbody>
            </table>
            </div>
            <div style="height:25px; overflow-y:scroll;overflow-x:hidden;">
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="white">
                    <tfoot>
                        <tr>
                            <td style="width:80px">&nbsp;</td>
                            <td style="width:80px">&nbsp;</td>
                            <td style="width:80px">&nbsp;</td>
                            <td style="width:120px">&nbsp;</td>
                            <td style="width:120px">&nbsp;</td>
                            <td style="width:150px">&nbsp;</td>
                            <td style="width:120px">&nbsp;</td>
                            <td >&nbsp;</td>
                        </tr>
                    </tfoot>
                    </table>
                    </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </fieldset> 
             </ContentTemplate>
             </asp:UpdatePanel>
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
</form>
</body>
</html>

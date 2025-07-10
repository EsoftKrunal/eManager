<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewReport.aspx.cs" Inherits="PreviewReport" EnableEventValidation="false" EnableViewStateMac="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview Report</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <style type="text/css">
          
     img
     {
     	border :none;
     }
     .clsFalse
     {
     	background-image :url('<%= ConfigurationManager.AppSettings["PDFIconPath"] %>');
     	height :20px;
     	width:25px; 
     	border :none;
     	background-repeat :no-repeat;
     }
     .clsTrue
     {
     	background-image :url('<%= ConfigurationManager.AppSettings["ExcelIconPath"] %>');
     	height :20px;
     	width:25px;
     	border :none;
     	background-repeat :no-repeat;
     }
     </style>    
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager id="ScriptManager1" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager> 
    <div style="font-family:Arial, Helvetica, sans-serif;font-size:11px;">
    <center>
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
        <div class="text headerband" style="font-family:Arial, Helvetica, sans-serif;">
            <b> Account Reports</b>
        </div>
        <%--<h2><asp:Label runat ="server" ID="lblCompany"></asp:Label>
            
        </h2>--%>
        <div style="height:25px;">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:LinkButton ID="ibDownloadMultipleFile" runat="server" Text="Download All Reports" style="float:right;margin-right:5px;" OnClick="ibDownloadMultipleFile_Click" ></asp:LinkButton>
        </div>
        <div class="text headerband" style="font-family:Arial, Helvetica, sans-serif;">
            Monthly Report
        </div>
         <table style="width:100%; border-collapse : collapse; height :150px;font-family:Arial, Helvetica, sans-serif;font-size:12px;" border="1" cellpadding="5" >
                        <tr style=" font-size :12px; font-weight : bold ; background-color: Black;color:White; height :20px;" >
                            <td width="45%">
                                <span lang="en-us">Report Title</span></td>
                             <td width="15%">
                                <span lang="en-us">Report Group</span></td>
                            <td width="15%"> 
                                <span lang="en-us">Sub Group</span></td>
                            <td width="15%">
                                <span lang="en-us">Period</span></td>
                            <td width="10%">
                                <span lang="en-us">View</span></td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptdata">
                        <ItemTemplate>
                        <tr>
                            <td><span lang="en-us"><%#Eval("FLDREPORTTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTSHIPNAME")%></td>
                            <td><span lang="en-us"><%#Eval("FLDREPORTSUBTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTTIMEPERIOD")%></td>
                            <td style =" text-align :center"> 
                               <asp:Button ID="btnDownload" runat="server" CssClass='<%#"cls" + Eval("excelfile").ToString().Trim()%>' Text="" CommandArgument='<%#Eval("REPORTID")%>' OnClick="Report_Click" />
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>  
                        <%--<tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>--%>
                    </table>
         <div class="text headerband" style="font-family:Arial, Helvetica, sans-serif;">
            YTD Report
        </div>
        <table style="width:100%; border-collapse : collapse; height :150px;font-family:Arial, Helvetica, sans-serif;font-size:12px;" border="1" cellpadding="5" >
                        <tr style=" font-size :12px; font-weight : bold ; background-color: Black;color:White; height :20px;" >
                             <td width="45%">
                                <span lang="en-us">Report Title</span></td>
                             <td width="15%">
                                <span lang="en-us">Report Group</span></td>
                            <td width="15%"> 
                                <span lang="en-us">Sub Group</span></td>
                            <td width="15%">
                                <span lang="en-us">Period</span></td>
                            <td width="10%">
                                <span lang="en-us">View</span></td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptYTDReport">
                        <ItemTemplate>
                        <tr>
                            <td><span lang="en-us"><%#Eval("FLDREPORTTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTSHIPNAME")%></td>
                            <td><span lang="en-us"><%#Eval("FLDREPORTSUBTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTTIMEPERIOD")%></td>
                            <td style =" text-align :center"> 
                               <asp:Button ID="btnDownload" runat="server" CssClass='<%#"cls" + Eval("excelfile").ToString().Trim()%>' Text="" CommandArgument='<%#Eval("REPORTID")%>' OnClick="Report_Click" />
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>  
                        <%--<tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>--%>
                    </table>
        <div class="text headerband" style="font-family:Arial, Helvetica, sans-serif;">
            Variance Report
        </div>
        <table style="width:100%; border-collapse : collapse; height :150px;font-family:Arial, Helvetica, sans-serif;font-size:12px;" border="1" cellpadding="5" >
                        <tr style=" font-size :12px; font-weight : bold ; background-color: Black;color:White; height :20px;" >
                             <td width="45%">
                                <span lang="en-us">Report Title</span></td>
                             <td width="15%">
                                <span lang="en-us">Report Group</span></td>
                            <td width="15%"> 
                                <span lang="en-us">Sub Group</span></td>
                            <td width="15%">
                                <span lang="en-us">Period</span></td>
                            <td width="10%">
                                <span lang="en-us">View</span></td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptVarianceReport">
                        <ItemTemplate>
                        <tr>
                            <td><span lang="en-us"><%#Eval("FLDREPORTTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTSHIPNAME")%></td>
                            <td><span lang="en-us"><%#Eval("FLDREPORTSUBTITLE")%></span></td>
                            <td><%#Eval("FLDREPORTTIMEPERIOD")%></td>
                            <td style =" text-align :center"> 
                               <asp:Button ID="btnDownload" runat="server" CssClass='<%#"cls" + Eval("excelfile").ToString().Trim()%>' Text="" CommandArgument='<%#Eval("REPORTID")%>' OnClick="Report_Click" />
                            </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>  
                       <%-- <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>--%>
                    </table>
                   </ContentTemplate>
              <Triggers>
        <asp:PostBackTrigger ControlID="rptdata" />
                  <asp:PostBackTrigger ControlID="rptYTDReport" />
                  <asp:PostBackTrigger ControlID="rptVarianceReport" />
                  <asp:PostBackTrigger ControlID="ibDownloadMultipleFile" />
    </Triggers>
            </asp:UpdatePanel>
    </center> 
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ApplicantHomeNew.aspx.cs" Inherits="CrewApproval_ApplicantHomeNew" Title="Crew Approval" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <style type="text/css">
        .borderd tr td
        {
            border:solid 1px #f5ecec;
        }
        .officename
        {
               text-align: left;    font-size: x-large;    font-weight: bold;    margin-left: 10px; padding: 5px;

        }
    </style>
</head>
<body style="margin:0; font-family:Calibri;font-size:14px;" >
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="padding:5px; text-align:left">
          Year -  <asp:DropDownList runat="server" Id="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged"></asp:DropDownList>
        </div>
      <table style="width :100%" cellpadding="5" cellspacing="0" class="borderd" border="0" style="border-collapse:collapse">
            <col />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
        <tr style="background-color:#6c6c6c;color:#ffffff;" >
            <td style="background-color:#6c6c6c;color:#ffffff; text-align:left">Office</td>
            <td>Applicant</td>
            <td>Ready for Approval</td>
            <td>Approved</td>
            <td>Rejected</td>
            <td>Archived</td>
            <td>Employed</td>
            <td>Total</td>
        </tr>
            <asp:Repeater runat="server" ID="rptData">
                <ItemTemplate>
                     <tr>
                             <td style="text-align:left;"><asp:LinkButton runat="server" ID="btnPost" OnClick="btn_PostClick" CommandArgument='<%#Eval("OfficeId")%>' Text=' <%#Eval("Officename")%>' /></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=1&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Applicant")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=2&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Ready for Approval")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=3&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Approved")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=4&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Rejected")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=5&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Archived")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=6&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Employed")%></a></td>
                            <td><a href='CandidatedetailInformation.aspx?Status=6&OfficeId=<%#Eval("OfficeId")%>'><%#Eval("Total")%></a></td>
                        </tr>
                </ItemTemplate>
            </asp:Repeater>
       
      </table>
     <%-- <div style="text-align: left; height:345px;overflow-x:hidden; overflow-y:scroll; padding:5px">--%>
        <table width="100%" class="borderd" cellspacing="0" cellpadding="3" style="border-collapse:collapse">
            <col />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
            <col style="width:150px" />
             <tr style="background-color:#6c6c6c;color:#ffffff;" >
                <td>User Name</td>
                <td>Applicant</td>
                <td>Ready for Approval</td>
                <td>Approved</td>
                <td>Rejected</td>
                <td>Archived</td>
                <td>Employed</td>
                <td>Total</td>
                 </tr>
            <asp:Repeater runat="server" ID="rptdata1">
                <ItemTemplate>

            <tr>
                <td style="text-align:left;">
                    <%#Eval("UserName")%>
                </td>
                <td><%#Eval("Applicant")%></a></td>
                <td><%#Eval("Ready for Approval")%></td>
                <td><%#Eval("Approved")%></td>
                <td><%#Eval("Rejected")%></td>
                <td><%#Eval("Archived")%></td>
                <td><%#Eval("Employed")%></td>
                <td><%#Eval("Total")%></td>

            </tr>

                </ItemTemplate>
            </asp:Repeater>
        </table>
        
      <%--</div>--%>
    </form>
</body>
</html>

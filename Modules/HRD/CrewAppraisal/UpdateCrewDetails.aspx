<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCrewDetails.aspx.cs" Inherits="CrewAppraisal_UpdateCrewDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Crew Details</title>
    <style type="text/css">
         .Desc
        {
        	font-family:Arial
        	text-align:left; 
        	font-size:10px;
        	font-weight:bold; 
        	font-style:italic;  
        	color:Gray;
        }
       input[type=text]
        {
             font-family: Arial;
             font-size: 11px;
             color: #003399;
             background-color:#F7F8E0;  
             border:1px solid #9abcd7;
             text-transform:uppercase;
             width:160px;
        }
                
        select
        {
             font-family: Arial ;
             font-size: 11px;
             color: #003399;
             border:1px solid #9abcd7;
             width: 50px;
             background-color:#F7F8E0;
             text-transform:uppercase;  
        }
        
        textarea
        {
	         font-family:  Arial;
	         font-size: 11px;
	         color: #003399;
	         background-color:#F7F8E0;  
	         border:1px solid #9abcd7;
	         text-transform:uppercase;
        }
        .section    
        {
        	font-size:20px;
        	font-weight:bold; 
        	color:Black;
        }
        .secHeading
        {
        	font-size:18px;
        	font-weight:bold; 
        	color:White;
        	background-color:#C3862E;
        }
        .TblHeading
        {
        	font-size:13px;
        	font-weight:bold; 
        	background:#A9A9A9	;
        	color:White;
        }
        .hint
        {
        	text-align:left; 
        	font-size:10px;
        	font-weight:bold; 
        	font-style:italic;  
        	color:Gray;
        	text-transform :uppercase; 
        }    
        .SelBtn
        {
        	font-size:11px;
        	font-weight:bold; 
        	color:White;
        	background-color:#C3862E;
        }
        .lblData
        {
        	font-family:Arial;
        	text-align:left;
        	font-size:11px;
        	color:Gray;
        }
        .error
        {
        	font-family:Arial;
        	text-align:left;
        	font-size:11px;
        	color:#F62817;
        }
    </style>
    <script type="text/javascript">
        function BindParentGrid()
        {
            alert('Updated successfully.');
            window.opener.ReloadPage();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
            <td>
                <fieldset id="fMissMatch" runat="server"  style="width:98%; padding:5px;">
                <legend>Crew Update</legend>
                     <table cellpadding="2" cellspacing="0" width="100%" style="text-align:center;">
                        <col width="100px" />
                        <col width="170px" />
                        <col width="170px" />
                        <col />
                        <tr >
                            <td>Crew#</td>
                            <td>First Name</td>
                            <td>Last Name</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCrewNoforSearch" runat="server"  Width="80px" AutoPostBack="true" OnTextChanged="txtCrewNoforSearch_OnTextChanged" ></asp:TextBox>
                                <asp:HiddenField ID="hfShipSoftRank" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtFnameMisMatch" runat="server" ReadOnly="true"  ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLnameMisMatch" runat="server" ReadOnly="true" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnUpdMissMatch" runat="server" OnClick="btnUpdMissMatch_OnClick" Text="Update" CssClass="btn" style="float:right;" OnClientClick="return confirm('Are u sure to update ?')" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblmsgMissMatch" runat="server" CssClass="error" style="float:left;" ></asp:Label>
                            </td>
                        </tr>
                    </table>
            </fieldset>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

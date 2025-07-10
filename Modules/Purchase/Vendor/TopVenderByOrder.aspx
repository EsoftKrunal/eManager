<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopVenderByOrder.aspx.cs" Inherits="TopVenderByOrder" EnableEventValidation="false" %>
<%--<%@ Register Src="../UserControls/Registers.ascx" TagName="Registers" TagPrefix="uc2" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>POS : Top 10 Vendors By No of Orders </title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="./JS/Jquery.js"></script>
    <script type="text/javascript" src="./JS/Common.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <style  type="text/css">
        #fountainG{
	position:relative;
	width:234px;
	height:28px;
	margin:auto;
}

.fountainG{
	position:absolute;
	top:0;
	background-color:rgb(0,0,0);
	width:28px;
	height:28px;
	animation-name:bounce_fountainG;
		-o-animation-name:bounce_fountainG;
		-ms-animation-name:bounce_fountainG;
		-webkit-animation-name:bounce_fountainG;
		-moz-animation-name:bounce_fountainG;
	animation-duration:1.5s;
		-o-animation-duration:1.5s;
		-ms-animation-duration:1.5s;
		-webkit-animation-duration:1.5s;
		-moz-animation-duration:1.5s;
	animation-iteration-count:infinite;
		-o-animation-iteration-count:infinite;
		-ms-animation-iteration-count:infinite;
		-webkit-animation-iteration-count:infinite;
		-moz-animation-iteration-count:infinite;
	animation-direction:normal;
		-o-animation-direction:normal;
		-ms-animation-direction:normal;
		-webkit-animation-direction:normal;
		-moz-animation-direction:normal;
	transform:scale(.3);
		-o-transform:scale(.3);
		-ms-transform:scale(.3);
		-webkit-transform:scale(.3);
		-moz-transform:scale(.3);
	border-radius:19px;
		-o-border-radius:19px;
		-ms-border-radius:19px;
		-webkit-border-radius:19px;
		-moz-border-radius:19px;
}

#fountainG_1{
	left:0;
	animation-delay:0.6s;
		-o-animation-delay:0.6s;
		-ms-animation-delay:0.6s;
		-webkit-animation-delay:0.6s;
		-moz-animation-delay:0.6s;
}

#fountainG_2{
	left:29px;
	animation-delay:0.75s;
		-o-animation-delay:0.75s;
		-ms-animation-delay:0.75s;
		-webkit-animation-delay:0.75s;
		-moz-animation-delay:0.75s;
}

#fountainG_3{
	left:58px;
	animation-delay:0.9s;
		-o-animation-delay:0.9s;
		-ms-animation-delay:0.9s;
		-webkit-animation-delay:0.9s;
		-moz-animation-delay:0.9s;
}

#fountainG_4{
	left:88px;
	animation-delay:1.05s;
		-o-animation-delay:1.05s;
		-ms-animation-delay:1.05s;
		-webkit-animation-delay:1.05s;
		-moz-animation-delay:1.05s;
}

#fountainG_5{
	left:117px;
	animation-delay:1.2s;
		-o-animation-delay:1.2s;
		-ms-animation-delay:1.2s;
		-webkit-animation-delay:1.2s;
		-moz-animation-delay:1.2s;
}

#fountainG_6{
	left:146px;
	animation-delay:1.35s;
		-o-animation-delay:1.35s;
		-ms-animation-delay:1.35s;
		-webkit-animation-delay:1.35s;
		-moz-animation-delay:1.35s;
}

#fountainG_7{
	left:175px;
	animation-delay:1.5s;
		-o-animation-delay:1.5s;
		-ms-animation-delay:1.5s;
		-webkit-animation-delay:1.5s;
		-moz-animation-delay:1.5s;
}

#fountainG_8{
	left:205px;
	animation-delay:1.64s;
		-o-animation-delay:1.64s;
		-ms-animation-delay:1.64s;
		-webkit-animation-delay:1.64s;
		-moz-animation-delay:1.64s;
}



@keyframes bounce_fountainG{
	0%{
	transform:scale(1);
		background-color:rgb(0,0,0);
	}

	100%{
	transform:scale(.3);
		background-color:rgb(255,255,255);
	}
}

@-o-keyframes bounce_fountainG{
	0%{
	-o-transform:scale(1);
		background-color:rgb(0,0,0);
	}

	100%{
	-o-transform:scale(.3);
		background-color:rgb(255,255,255);
	}
}

@-ms-keyframes bounce_fountainG{
	0%{
	-ms-transform:scale(1);
		background-color:rgb(0,0,0);
	}

	100%{
	-ms-transform:scale(.3);
		background-color:rgb(255,255,255);
	}
}

@-webkit-keyframes bounce_fountainG{
	0%{
	-webkit-transform:scale(1);
		background-color:rgb(0,0,0);
	}

	100%{
	-webkit-transform:scale(.3);
		background-color:rgb(255,255,255);
	}
}

@-moz-keyframes bounce_fountainG{
	0%{
	-moz-transform:scale(1);
		background-color:rgb(0,0,0);
	}

	100%{
	-moz-transform:scale(.3);
		background-color:rgb(255,255,255);
	}
}
    </style>
    <style type="text/css">
        td
        {  
            overflow: hidden;
            text-overflow: ellipsis;
        }
        .gridheader tr td{
            background-color: #b9b7b7;
            color: #333333;
        }
        .rightpanescrollfixed{
            position:fixed;
            margin-top:73px;
            margin-left:400px;
            left:0px;
            top:0px;
        }
        .header
        {
            position:fixed;
            top:0px;
            left:0px;
            width:100%;
            height:72px;
            background-color:rebeccapurple;
            z-index:15;
        }
        .content
        {
            margin-top:103px;
            position:relative;
        }
        .leftpane
        {
            width:400px;
            position:fixed;
            left:0px;
            top:73px;
        }
         .rightpane
        {
            margin-left:400px;
        }
        .bordered
        {
            border-collapse:collapse;
        }
        .bordered tr td
        {
            border:solid 1px #e9e9e9;
        }
        .active
        {
            background-color:#ffad33;
        }
    </style>
</head>
<body style="font-family: 'Roboto', sans-serif; margin:0px; font-size:13px;">
<form id="form1" runat="server">
<div>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div class="header">
        <div style="text-align: center; padding: 8px; background-color: #06203b; color: White; font-size:16px;">
            <b> Top 10 Vendors By No of Orders </b>
        </div>
        <div style="text-align: center; padding:5px; background-color:#036387;color:white; font-size:15px; ">
            <table cellpadding="5" cellspacing="0" border="0" width="100%" >
                <tr>
                    <td>
                        Fleet : <asp:Label ID="lblFleet" runat="server"></asp:Label>
                    </td>
                    <td>
                        Vessel : <asp:Label ID="lblVessle" runat="server"></asp:Label>
                    </td>
                    <td>
                        Period : <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
</div>
    <div class="content">
        <div class="leftpane">
            <table width="100%" cellspacing="0" cellpadding="7" border="0" class="bordered gridheader">
                    <col width="35px" />
                    <col />
                    <col width="60px" />                                
                    <tr>
                    <td>Sr#</td>
                    <td>Supplier Name</td>
                    <td style="text-align:right">Orders</td>
                </tr>
            </table>
                    
            <table width="100%" cellspacing="0" cellpadding="7" border="0" class="bordered">
                            <col width="35px" />
                            <col />
                            <col width="60px" />   
                            <asp:Repeater ID="rptTop5VendorByOrder" runat="server">
                            <ItemTemplate>  
                            <tr style="cursor:pointer" class="trvessel" supplierid='<%#Eval("SupplierID")%>'>
                                <td style="text-align:left;"> <%#Eval("Sr") %> </td>
                                <td style="text-align:left;"> <%#Eval("suppliername") %> </td>
                                <td style="text-align:right;padding-right:5px;" class="SpanTotal" > <%#Eval("NoOfOrders") %> </td>                                                                            
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </table>
        </div>
        <div class="rightpane">
            <asp:UpdatePanel runat="server" id="UpdatePanel1">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hfdsupplierid" />
                    <asp:Button runat="server" ID="btnpost" OnClick="btnPost_Click" style="display:none" />
                    <table width="100%" cellspacing="0" cellpadding="7" border="0" class="bordered gridheader rightpanescrollfixed">
                           <col width="35px" />
                        <col width="50px" />
                        <col width="150px" />
                        <col width="120px" />
                        <col width="90px" />
                        <col width="150px" />
                        <col width="90px" />                                
                        <col />
                            <tr>
                                <td>Sr#</td>
                                <td>VSL</td>
                                <td>PO#</td>
                                <td>Amount (US$)</td>
                                <td>Created Dt.</td>
                                <td>Invoice#</td>
                                <td>Invoice Dt.</td>
                                <td>Remarks</td>
                            </tr>
                        </tr>
                    </table>
                    <table width="100%" cellspacing="0" cellpadding="7" border="0" class="bordered">
            <col width="35px" />
            <col width="50px" />
            <col width="150px" />
            <col width="120px" />
            <col width="90px" />
            <col width="150px" />
            <col width="90px" />                                
            <col />
                <asp:Repeater ID="rptdetails" runat="server" >
                <ItemTemplate>  
                <tr style="cursor:pointer">
                    <td style="text-align:left;"> <%#Eval("SNO") %> </td>
                    <td style="text-align:left;"> <%#Eval("SHIPID") %> </td>
                    <td style="text-align:left;"> <a target="_blank" href='<%#"VeiwRFQDetailsForApproval.aspx?BidId=" + Eval("BIDID").ToString() %>'> <%#Eval("BIDPONUM") %> </a></td>
                    <td style="text-align:right;padding-right:5px;" class="SpanTotal" > <%#Eval("TRANSUSD") %> </td>                                                                            
                    <td style="text-align:left;"> <%#Common.ToDateString(Eval("BidSMDLevel1ApprovalDate")) %> </td>
                    <td style="text-align:left;"> <%#Eval("InvoiceNO") %> </td>
                    <td style="text-align:left;"> <%#Common.ToDateString(Eval("bidInvoiceDate"))%> </td>
                    <td style="text-align:left;"> <%#Eval("ApproveComments") %> </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
   </div>
</div> 
    <asp:UpdateProgress ID="prog1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" >
            <ProgressTemplate>
                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvSalaryRevisionUpdate" visible="true" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :white;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:750px;text-align :center; border :solid 0px #000000;padding-bottom:5px;z-index:150;top:300px;opacity:1;filter:alpha(opacity=100)">
                 <div id="fountainG">
	                <div id="fountainG_1" class="fountainG"></div>
	                <div id="fountainG_2" class="fountainG"></div>
	                <div id="fountainG_3" class="fountainG"></div>
	                <div id="fountainG_4" class="fountainG"></div>
	                <div id="fountainG_5" class="fountainG"></div>
	                <div id="fountainG_6" class="fountainG"></div>
	                <div id="fountainG_7" class="fountainG"></div>
	                <div id="fountainG_8" class="fountainG"></div>
                </div>
            </div>
        </center>
        </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
<script type="text/javascript">
        $(document).ready(function () {
            $(".trvessel").click(function () {
                $(".trvessel").removeClass("active");
                $("#hfdsupplierid").val($(this).attr('supplierid'));
                $("#btnpost").focus();
                $("#btnpost").click();
                $(this).addClass("active");
            });
        });
    </script>
</form>
</body>
</html> 
 




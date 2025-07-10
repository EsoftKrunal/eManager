<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OwnerAccount.aspx.cs" Inherits="Invoice_OwnerAccount"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <script type="text/javascript" src="../Purchase/JS/jquery.min.js"></script>
     <link href="../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../Purchase/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <!-- Auto Complete -->
    <link rel="stylesheet" href="../Purchase/JS/AutoComplete/jquery-ui.css" />
    <script src="../Purchase/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            font-family: Verdana;
            font-size: 12px;
            margin: 0px;
        }

        #nav {
            margin: 0px;
            padding: 0px;
        }

            #nav li {
                margin: 0px;
                margin-left: -3px;
                padding: 10px;
                display: inline-block;
                width: 150px;
                background-color: #dbd6d6;
                text-align: center;
                cursor: pointer;
            }

                #nav li:hover {
                    color: white;
                    background-color: #888888;
                }

                #nav li.active {
                    color: white;
                    background-color: #000000;
                }

        .bordered tr td {
            border: solid 1px #e5edf3;
        }

        input, select, textarea {
            line-height: 15px;
            padding: 3px;
            border: solid 1px #dbd6d6;
            background: #fff6ed;
        }

       /* .btn {
            background-color: #006EB8;
            color: White;
            border: none;
            padding: 6px;
            min-width: 100px;
        }*/

        .headerrow td {
            background-color: #666;
            color: white;
            padding: 5px;
            height: 25px;
        }

        .bordered tr:nth-child(even) {
            background: #ffffff;
        }

        .bordered tr:nth-child(odd) {
            background: #f1faff;
        }
    </style>
    <script type="text/javascript" src="../Purchase/JS/KPIScript.js"></script>
    <script>
        $(document).ready(function () {
            $(".accmenu").click(function () {
                $(".accmenu").removeClass("active");
                $(this).addClass("active");

                $(".AccTab").hide();
                var tab = $(this).attr("id");
                if (tab == "Tab1")
                    $("#divAccountSummary").show();
                if (tab == "Tab2") {
                    $("#divAccountStatement").show();
                    $("#divAccountSummary").show();
                }
                if (tab == "Tab3") {
                    $("#divFundTransfet").show();
                    $("#divAccountSummary").show();
                }

            });
        })
    </script>
  

</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div>
            <div style="padding: 8px; text-align: center;" class="text headerband">Ownres's Account balance </div>
            <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                <tr>
                    <td style="width:500px;vertical-align:top;">                        
                        <div style="height:650px; overflow-x:hidden; overflow-y:scroll">
                            <asp:UpdatePanel ID="UPOwnerList" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                
                        <table cellpadding="5" cellspacing="0" border="0" width="100%" class="bordered" style="border-collapse:collapse;">
                            <tr class= "headerstylegrid">
                                <td></td>
                                <td>Company</td>
                                <td style="text-align:center">SGD</td>
                                <td style="text-align:center">USD</td>  
                                <td style="width:20px">&nbsp;</td>                              
                            </tr>
                            
                        <asp:Repeater ID="rptOwnerList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <input type="radio" name="Owner" company='<%#Eval("Company") %>' class="classOwner" />
                                    </td>
                                    <td style="text-align:left;"><%#Eval("Company Name") %> </td>
                                    <td style="text-align:right;"> <%#FormatCurrency(Eval("Balance_SGD")) %> </td>
                                    <td style="text-align:right;"> <%#FormatCurrency(Eval("Balance_USD")) %> </td>
                                    <td style="width:20px">&nbsp;</td>                              
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                            </table>
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        
                    </td>
                    <td style="vertical-align:top;">

                        <asp:UpdatePanel  runat="server">
                            <ContentTemplate>

                        <asp:Button ID="tempBtn" runat="server" OnClick="tempBtn_OnClick" style="display:none;" />
                        <asp:HiddenField ID="hfCompany" runat="server" />
                        
                        <div id="divContent" runat="server" visible="false">

          
               
               <div id="divAccountSummary" class="AccTab">
                <table width="100%" cellpadding="5" cellspacing="0" border="0" style="border-collapse: collapse; text-align: center;">
                    <tr>
                        <td style="text-align:center">
                             <asp:RadioButtonList ID="rdoAccount" AutoPostBack="true" OnSelectedIndexChanged="rdoAccount_OnSelectedIndexChanged" runat="server" RepeatDirection="Horizontal" style="font-size: 20px; color: #0574d4;">
                                    <asp:ListItem Selected="True" Text="SGD Account" Value="SGD"></asp:ListItem>
                                    <asp:ListItem Text="USD Account" Value="USD"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        </tr>
                    <tr>
                       <td style="text-align:center" >
                            <asp:UpdatePanel ID="upAcSummary" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                            
                            <span style="font-size: 25px; font-weight: bold;">
                               Account Balance <span style="font-size: 30px; font-weight: bold; color: orange" id="spanCurr" runat="server">SG$</span><asp:Label ID="lblAccountBalance" runat="server"></asp:Label> as on <asp:Label ID="lblToday" runat="server"></asp:Label>
                            </span>
                         </ContentTemplate>
                </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
                   
            <%-----------------------------------------------------------------------------%>
            <div id="divAccountStatement"  class="AccTab">
                <asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>

                    <table width="100%" cellpadding="4" cellspacing="0" border="0" style="border-collapse: collapse; text-align: center;text-align:left; background-color:#eceeef" >
                        <colgroup>
                            <col width="80px" />
                            <col width="200px" />
                            <col width="140px" />
                            <col width="180px" />
                            <col width="130px" />
                            <col  />
                            <tr>
                                <td><b>Period :</b> </td>
                                <td>
                                    <asp:TextBox ID="txtPeriodFrom" runat="server" Width="80px"></asp:TextBox>
                                    :
                                    <asp:TextBox ID="txtPeriodTo" runat="server" Width="80px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txtPeriodFrom">
                                    </asp:CalendarExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txtPeriodTo">
                                    </asp:CalendarExtender>
                                </td>
                                <td><b>Transaction Type :</b> </td>
                                
                                <td>
                                    <asp:RadioButtonList ID="rdoTransactionType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Debit" Value="D"></asp:ListItem>
                                        <asp:ListItem Text="Credit" Value="C"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td><b>Payment User :</b></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlpaymentuser"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align:right">
                                     <asp:Button ID="btnShow" runat="server" CausesValidation="false" CssClass="btn" OnClick="btnShow_OnClick" Text="Show" />
                                    <asp:Button ID="btnOpenAddStatement" runat="server" Width="150px" Visible="false" CausesValidation="false" CssClass="btn" OnClick="btnOpenAddStatement_OnClick" Text="+ New Transaction" />
                                    <asp:Button ID="btnPrint" runat="server" Visible="false" CausesValidation="false" CssClass="btn" OnClick="btnPrint_OnClick" Text="Print" />
                           
                                </td>
                            </tr>
                        </colgroup>
                </table>
            <div style="height:525px; overflow-x:hidden; overflow-y:scroll">
                <table width="100%" cellpadding="4" cellspacing="0" class="bordered" style="border-collapse: collapse; text-align: center;text-align:left;" >
                    <colgroup>
                        <col style="width:100px" />
                        <col style="width:150px" />
                        <col style="width:150px" />
                        <col style="width:100px" />
                        <col style="width:100px" />
                        <col style="width:100px" />
                        <col />
                        <col style="width:120px" />
                        <col style="width:20px" />
                        
                    </colgroup>
                    <tr class= "headerstylegrid">
                        <td>Date</td>
                        <td>PVNo</td>
                        <td>Transaction Type</td>
                        <td>Debit</td>
                        <td>Credit</td>
                        <td>By/On</td>
                        <td>Supplier Name</td>
                        <td>TravId</td>
                        <td style="width:20px">&nbsp;</td> 
                    </tr>
                    <asp:Repeater ID="rptAccountStatement" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td> <%#Common.ToDateString(Eval("TransDate")) %> </td>
                                <td> 
                                    <a id="A1" runat="server" onclick='<%#"PrintVoucherN(" +Eval("TableID").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="N")%>'><%#Eval("PVNo")%></a>
                                    <a id="A2" runat="server" onclick='<%#"PrintVoucherO(" +Eval("TableID").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%></a>
                                </td>
                                <td> <%#Eval("TransactionTypeName") %> </td>                                
                                <td style="text-align:right"> <%#Eval("DebitAmount") %> </td>
                                <td style="text-align:right"> <%#Eval("CreditAmount") %> </td>
                                <td style="text-align:left"> <%#Eval("ModifiedBy") %> / <%#Common.ToDateString(Eval("ModifiedOn")) %>  </td>
                                <td style="text-align:left"> <%#Eval("Suppliername") %> </td>
                                <td style="text-align:left"> <%#Eval("TravId") %> </td>
                                <td style="width:20px">&nbsp;</td>   
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
                <!-----------------Add Popup------------------------>
                <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="div_AddTransaction" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:800px;padding :0px; text-align :center;background : white; z-index:150;top:50px; border:solid 2px black;">
                <center >
                 <div style="padding:6px;  font-size:14px; " class="text headerband"><b>New Transaction</b></div>
                    <div style="text-align:center; padding:5px; font-size:20px; color:#006EB8">
                        <asp:RadioButton ID="rad_debit_A" runat="server" Text=" Debit" ForeColor="Red" GroupName="g1" OnCheckedChanged="TransactionType_OnCheckedChanged" AutoPostBack="true" />
                        <asp:RadioButton ID="rad_credit_A" runat="server" Text=" Credit" ForeColor="Green" GroupName="g1"  OnCheckedChanged="TransactionType_OnCheckedChanged" AutoPostBack="true" />
                        <asp:RadioButton ID="rad_transfer" runat="server" Text=" Transfer" ForeColor="#333" GroupName="g1"  OnCheckedChanged="TransactionType_OnCheckedChanged" AutoPostBack="true" />
                    </div>
                    <div style="padding:8px; text-align:left; background-color:#f1f0f0;"><asp:Label runat="server" ID="lblParty1" Font-Size="Larger" Font-Bold="true"></asp:Label></div>
                    <table width="100%" cellpadding="4" cellspacing="0" border="0" style="border-collapse: collapse; text-align: left;" class="bordered">
                        <tr >
                            <td style="width:130px;">Owner Name :</td>
                            <td>
                                 <asp:Label ID="lblOwnerCode" runat="server" Font-Bold="true" Font-Size="15px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Account Type :</td>
                            <td>
                                 <asp:Label ID="lblAccount_A" runat="server" Font-Bold="true" Font-Size="15px"></asp:Label>
                            </td>
                        </tr>
                      
                       
                        <tr>
                            <td>Transaction Date :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtTransactionDate_A" Width="120px"  ValidationGroup="v1"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txtTransactionDate_A"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="v1" ControlToValidate="txtTransactionDate_A" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Amount :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAmount_A" ValidationGroup="v1" Width="120px" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="v1" ControlToValidate="txtAmount_A" ErrorMessage="*"></asp:RequiredFieldValidator>
                                &nbsp;
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="v1" ControlToValidate="txtAmount_A" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\d*\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Remarks :</td>
                            <td>
                                <asp:TextBox ID="txtRemarks_A" runat="server" Width="400px" ValidationGroup="v1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="v1" ControlToValidate="txtRemarks_A" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        

                        
                        
                    </table>
                    <div  id="trRecivingOwner" runat="server" visible="false">
                        <div style="padding:8px; text-align:left; background-color:#f1f0f0;"><asp:Label runat="server" ID="lblParty2" Font-Size="Larger" Font-Bold="true"></asp:Label></div>
                        <table width="100%" cellpadding="4" cellspacing="0" border="0" style="border-collapse: collapse; text-align: left;" class="bordered">
                        <tr>
                            <td style="width:130px;">Reciving Owner :</td>
                            <td>
                                <asp:DropDownList ID="ddlRecivingOwner" runat="server" ValidationGroup="v2" Width="255px"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="v1" ControlToValidate="ddlRecivingOwner" ErrorMessage="*"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>Reciving Account :</td>
                            <td>
                                 <asp:RadioButtonList ID="radRecCurr" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Text="SGD Account" Value="SGD"></asp:ListItem>
                                    <asp:ListItem Text="USD Account" Value="USD"></asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>Received Amount :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtAmount_B" ValidationGroup="v1" Width="120px" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="v1" ControlToValidate="txtAmount_B" ErrorMessage="*"></asp:RequiredFieldValidator>
                                &nbsp;
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="v1" ControlToValidate="txtAmount_B" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\d*\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <div style="padding:6px;">
                        <asp:Button ID="btnAddStatement" runat="server" Text="Save" OnClick="btnAddStatement_OnClick" CssClass="btn" ValidationGroup="v1" />
                        <asp:Button ID="btnClosePopup" runat="server" Text="Close" OnClick="btnClosePopup_OnClick"  CssClass="btn" CausesValidation="false" />
                    </div>
                    <div style="padding:7px; background-color:#fcf08f; text-align:left;">
                            &nbsp;<asp:Label ID="lblMsgPopup_A" runat="server" ForeColor="Red"></asp:Label>&nbsp;
                    </div>
                </center>
            </div>
        </center>
        </div>

                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%-----------------------------------------------------------------------------%>
            

                </div>
                        
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <div style="border-top:solid 2px #666"></div>
            
            
        </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".classOwner").click(function () {
                var company = $(this).attr("company");
                $("#hfCompany").val(company);
                $("#tempBtn").click();
            });
        })
    </script>

       
        
    <script type="text/javascript">
    
    function PrintVoucherN(pid) {
        winref = window.open('../Purchase/Invoice/PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=N', '');
        return false;
    }
    function PrintVoucherO(pid) {
        winref = window.open('../Purchase/Invoice/PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=O', '');
        return false;
    }
    </script>
      

    </form>
</body>
</html>

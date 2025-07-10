<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditVoyageDetails.aspx.cs" Inherits="Modules_REVENUE_AddEditVoyageDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../HRD/Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../css/StyleSheet.css" />
    <style type="text/css">

        #tblContractDtls td, #tblContractDtls th {
            border: 1px solid black;
            border-collapse: collapse;
        }

        #tblContractDtlsVC td, #tblContractDtlsVC th {
            border: 1px solid black;
            border-collapse: collapse;
        }

        #tblContractDtlsTC td, #tblContractDtlsTC th {
            border: 1px solid black;
            border-collapse: collapse;
        }

        #tblContractStatus td, #tblContractStatus th {
            border: 1px solid black;
            border-collapse: collapse;
        }

        

        .auto-style1 {
            height: 30px;
        }

        
        #tblVoyageDetails td, #tblVoyageDetails th {
            border: 1px solid black;
            border-collapse: collapse;
        }

        #tblVoyageInfo td, #tblVoyageInfo th {
             border: 1px solid black;
            border-collapse: collapse;
        }
    </style>

   <script type="text/javascript">
           function RefreshContractList() {
               window.opener.document.getElementById('ctl00_ContentMainMaster_btnsearch').click();
       }
      
   </script>

</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div class="text headerband">
             Contract # - [ <asp:Label ID="lblContractNo" runat="server"></asp:Label> ]
        </div>
        <asp:HiddenField ID="hdnContractId" runat="server" />
        <table width="100%">
            <tr>
                <td width="30%">
                    <div class="text headerband"> 
                        <strong> Contract Details </strong>
                    </div>
                    <table width="100%" id="tblContractDtls" >
                        <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                               <b> Vessel : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3">
                                <asp:Label ID="lblVessel" runat="server" ></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%">
                                <b>Contract Type :</b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                                <asp:Label ID="lblContractType" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%">
                                <b>Charterer :</b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                                <asp:Label ID="lblCharterer" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        </table>
                    <table width="100%" id="tblContractDtlsTC" runat="server" visible="false"> 
                         <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                                <b>Contract Start Dt : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                                <asp:Label ID="lblConStartDt" runat="server" ></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                             <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%">
                               <b> Contract End Dt : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                                <asp:Label ID="lblConEndDt" runat="server" ></asp:Label>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%">
                              <b> Contract Duration (Days)  : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3"> 
                                <asp:Label ID="lblContractDuration" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                                <b> Per Day Hire Amount ($) : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3">
                                <asp:Label ID="lblPerDayHireAmount" runat="server" ></asp:Label>
                            </td>
                           
                        </tr>
                        <tr>
                            <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                               <b> Total Hire Amount (US $) : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3">
                                <asp:Label ID="lblTotalHireAmountUSD" runat="server" ></asp:Label>
                            </td>
                            
                        </tr>
                    </table>
                            <table width="100%" id="tblContractDtlsVC" runat="server" visible="false"> 
                        
                    <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>From Port : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblFromPortVC" runat="server" ></asp:Label>
                </td>
                    </tr>
                                <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>To Port : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblToPortVC" runat="server" ></asp:Label>
                </td>
                    </tr>

                                    <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>Volume : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblVolume" runat="server" ></asp:Label>
                </td>
                    </tr>
                                    <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>Rate : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblRate" runat="server" ></asp:Label>
                </td>
                    </tr>
                                    <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>Total Hire Amount : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblTotalHireAmtVC" runat="server" ></asp:Label>
                </td>
                    </tr>
                                <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>Expected Voyage Duration (Days) : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblExpVoyageDays" runat="server" ></asp:Label>
                </td>
                    </tr>
                                    <tr>
                <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%" >
                    <b>Cargo Details : </b>
                </td>
                <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3" >
                    <asp:Label ID="lblCargoDetails" runat="server" ></asp:Label>
                </td>
                    </tr>
                                </table>
                    <table width="100%" id="tblContractStatus">
                         <tr>
                             <td style="padding:2px 5px 2px 0px;text-align:right;" width="42%">
                                <b>Contract Status : </b>
                            </td>
                            <td style="padding:2px 0px 2px 5px;text-align:left;" colspan="3">
                                <asp:Label ID="lblContractStatus" runat="server" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr />
                     <div style="min-height:200px;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;padding-top: 5px;padding-right:5px;"  >
                          <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                                        <tr align="left" class="headerstylegrid">
                                            <td style="width: 4%;">Expense Head</td>
                                            <td style="width: 4%;">Amount (US $)</td>
                                        </tr>
                                    
                                </table>
                <table width="100%"  style="border: 1px solid #4C7A6F; " >
                    <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                    <tr>
                    <td>
                        <asp:Repeater runat="server" ID="rptExpExpenses" Visible="false" >
                   
                <ItemTemplate>
                <tr>
                    <td style="width:60%;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lblCategoryName" Text='<%#Eval("CategoryName")%>' CssClass="textlabel"  style='text-align:center'  Font-Size="12px"></asp:Label>
                    </td>
                    <td style="width:40%;text-align:center;border: 1px solid #4C7A6F;">
                        <asp:TextBox runat="server" ID="txtRVCEEAmount" Text='<%#(Eval("RVCEE_Amount"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="12px" OnTextChanged="txtRVCEEAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                        <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%#Eval("CategoryId")%>' />
                      
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                    </td>
                    </tr>
                 </table>
                         <table width="100%"  style="border: 1px solid #4C7A6F; ">
                        <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                <tr>
                <td style="width:60%;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> 
                    <asp:Label runat="server" ID="lblAddCom" Text="AddCOM" CssClass="textlabel"  style='text-align:center'  Font-Size="12px"></asp:Label> &nbsp;&nbsp;&nbsp;
                   <span> <asp:Label ID="lblAddComPer" runat="server" Width="75px"  MaxLength="5" ></asp:Label> %</span>
                    
                </td>
                <td style="width:40%;text-align:center;border: 1px solid #4C7A6F;">
                    <asp:Label runat="server" ID="txtAddComAmt" Text="0.00" CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="12px" ></asp:Label>
                  
                      
                </td>
                    </tr>
                    </table>
                    </div>
                
                 
                </td>
                <td width="70%" style="vertical-align:top;">
                    <div >
                       <table width="100%">
                           <tr>
                               <td style="padding:2px 0px 2px 5px;"> 
                                   <div style="float:left; padding-left:5px;"> 
                                        <asp:Button ID="btnAddVoyage" runat="server" Text="Add Voyage" CssClass="btn" OnClick="btnAddVoyage_Click" /> &nbsp;
                                   </div>
                                   <div style="float:right; padding-right:5px;">
                                        <asp:Button runat="server" ID="btnContractCloser" Text="Close Contract" CssClass="btn"  OnClick='btnContractCloser_Click' OnClientClick="return window.confirm('Are you sure to close this Contract?')"  Visible="false"/>
                                   </div>
                                  
                                  
                               </td>
                           </tr>
                           <tr>
                               <td style="padding-left:5px;">
                                  
                                  <strong> <asp:Label ID="lblVoyageInfo" runat="server" Text="Voyage Details" ></asp:Label> </strong>
                               </td>
                           </tr>
                           <tr>
                               <td>
                                    <div style="width: 100%; height: 275px; text-align: center;">
                              <%--      <asp:UpdatePanel ID="uplnk" runat="server">
                                        <ContentTemplate>--%>
                                           <%-- <asp:LinkButton id="lnk" onclick="lnk_Click" runat="server" Text=""></asp:LinkButton>
                                            <br />--%>
                                            <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                    
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width: 4%;" />
                                       <col style="width: 4%;" />
                                        <col style="width: 4%;" />
                                        <col style="width: 18%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 2%;" />

                                    </colgroup>
                                        <tr align="left" class="headerstylegrid">
                                            <td style="width: 4%;">View</td>
                                            <td style="width: 4%;">Edit</td>
                                            <td style="width: 4%;">Delete</td>
                                            <td style="width: 18%;">
                                                Voyage #
                                            </td>
                                            <td style="width: 10%;">
                                                From Port
                                            </td>
                                            <td style="width: 10%;">
                                                To Port
                                            </td>
                                            <td style="width: 10%;">
                                                From Date
                                            </td>
                                            <td style="width: 10%;">
                                                To Date
                                            </td>
                                            <td style="width: 10%;">
                                                Voyage Duration (Days)
                                            </td>
                                            <td style="width: 10%;">
                                                Tot. Off-hire Amt.
                                            </td>
                                            <td style="width: 10%;">
                                                Tot. Act. Expenses Amt.
                                            </td>
                                            <td style="width: 2%;"> 

                                            </td>
                                        </tr>
                                    
                                </table>
                                <div id="dvJP"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                   <col style="width: 4%;" />
                                         <col style="width: 4%;" />
                                        <col style="width: 4%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 10%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 9%;" />
                                        <col style="width: 1%;" />
                    </colgroup>
                   
               <asp:Repeater ID="rptVoyage" runat="server"  >
                  <ItemTemplate>
                      <tr class="alternaterow" onMouseOver="this.className='highlight'" onMouseOut="this.className='alternaterow'">
                     
                            <td align="center" style="width: 
    4%;"> 
 <asp:ImageButton ID="ImgVoyView" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" CommandArgument='<%#Eval("RCVD_ID") %>' OnClick="ImgVoyView_Click" CausesValidation="false"/>
                           </td>
                            </td> 
                           <td align="center" style="width: 
    4%;">
                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("RCVD_ID") %>' OnClick="imgEdit_Click" CausesValidation="false"/>
                           </td>
                           <td align="center" style="width: 
    4%;">
                            <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("RCVD_ID") %>' OnClick="imgDelete_Click"  OnClientClick="return window.confirm('Are you sure to remove this?');" CausesValidation="false"/>
                           </td>
                           <td align="left" style="width: 18%;"><%#Eval("RCVD_VoyageNo")%><asp:HiddenField ID="hfVoyageId" Value='<%#Eval("RCVD_ID") %>' runat="server" /></td>
                          <td align="left" style="width: 10%;"><%#Eval("FromPort")%></td>
                           <td align="left" style="width: 10%;"><%#Eval("ToPort")%></td>    
                             <td align="left" style="width: 10%;"><%#Eval("RCVD_FromDt")%></td>                  <td align="left" style="width: 10%;"><%#Eval("RCVD_ToDt")%></td> 
                          <td align="left" style="width: 10%;"><%#Eval("VoyageDuration")%></td>
                          <td align="left" style="width: 9%;">
                              <asp:Label ID="lblTotalOffHireAmt" runat="server" Text='<%#Eval("TotalOffHireAmt")%>' ></asp:Label>
                          </td>
                          <td align="left" style="width: 9%;">
                               <asp:Label ID="lblTotalActExpDetails" runat="server" Text='<%#Eval("TotalActExpDetails")%>' ></asp:Label>

                          </td>
                           <td ></td>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
                                </div>
                            </td>                            
                        </tr>
                        
                        </table>
                                           <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                               </td>
                           </tr>
                       </table>
                    </div>
                    
                </td>
            </tr> 
            <tr>
                <td colspan="2">
                    <div style="font-size:14px;font-family:Arial;" id="divTotalExpExpense" runat="server" visible="false">
                    <table width="100%">
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <b>Expected </b>
                            </td>
                            <td colspan="2" style="text-align:center;">
                                <b>Actual </b>
                            </td>
                        </tr>
                         <tr>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Contract Amount (US $) :
                            </td>
                            <td width="20%" style="padding:2px 5px 2px 0px;text-align:left;">
                                 <asp:Textbox ID="txtContractAmount" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Off-hire Amount (US $) :
                               <%-- (<span><asp:Label ID="lblTotalOffhireDays" runat="server"></asp:Label></span> Days) :--%>
                            </td>
                            <td style="padding:2px 5px 2px 0px;text-align:left;">
                                  <asp:Textbox ID="txtTotalOffhireAmount" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Total Expected Expenses (US $) :
                            </td>
                            <td width="20%" style="padding:2px 5px 2px 0px;text-align:left;">
                                 <asp:Textbox ID="txtTotalExpExpenses" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Actual Expenses Amount (US $) :
                            </td>
                            <td width="20%" style="padding:2px 5px 2px 0px;text-align:left;">
                                 <asp:Textbox ID="txtTotalActualExpectedAmount" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Total Expected Revenue (US $) :
                            </td>
                            <td width="20%" style="padding:2px 5px 2px 0px;text-align:left;">
                                 <asp:Textbox ID="txtTotalRevenue" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">
                                Actual Revenue (US $)
                            </td>
                            <td style="padding:2px 5px 2px 0px;text-align:left;">
                                <asp:Textbox ID="txtTotalActualRevenue" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                            </td>
                        </tr>
                         <tr>
                            <td width="15%" style="padding:2px 0px 2px 5px;text-align:right;">
                                
                            </td>
                            <td width="20%" style="padding:2px 5px 2px 0px;text-align:left;">
                               
                            </td>
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">

                            </td>
                            <td style="padding:2px 5px 2px 0px;text-align:left;">

                            </td>
                        </tr>
                        <tr>
                           
                            <td width="20%" style="padding:2px 0px 2px 5px;text-align:right;">

                            </td>
                            <td style="padding:2px 5px 2px 0px;text-align:left;">

                            </td>
                        </tr>
                    </table>
                    
                </div>
                </td>
            </tr>
        </table>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dvVoyage" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; height:350px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br />
        <div class="text headerband"> <b>Voyage Details</b> 
            <asp:ImageButton ID="imgClosebtn2" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="imgClosebtn2_OnClick" CssClass="btn" CausesValidation="false" />
        </div>
        <br />
            <table width="100%" id="tblVoyageDetails">
                
                     <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Voyage # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox runat="server" CssClass="input_box" ID="txtVoyageNo"  Width="250px" MaxLength="100"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="CompareValidator2" runat="server" ControlToValidate="txtVoyageNo" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">From Port : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtfromport" runat="server" AutoPostBack="True" 
                                                                    CssClass="input_box"  Width="164px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromport" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" MinimumPrefixLength="1" TargetControlID="txtfromport" ServicePath="~/Modules/REVENUE/WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                    </cc1:AutoCompleteExtender>
                                                
                    </td>
                </tr>
                 <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">To Port : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                         <asp:TextBox ID="txttoport" runat="server" CssClass="input_box" Width="164px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToPort" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" MinimumPrefixLength="1" TargetControlID="txttoport" ServicePath="~/Modules/REVENUE/WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                </cc1:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">From Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                         <asp:TextBox ID="txtFromDate" runat="server" CssClass="input_box" Width="92px" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                <asp:ImageButton ID="imgFromDt" runat="server" CausesValidation="False" 
                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
<asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ErrorMessage="Required." Text=" *" ControlToValidate="txtFromDate" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="imgFromDt" PopupPosition="TopRight" TargetControlID="txtFromDate">
                                                    </ajaxToolkit:CalendarExtender>
                         &nbsp;
                <asp:DropDownList runat="server" ID="ddlFromHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator88" ControlToValidate="ddlFromHrs" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlFromMin" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator89" ControlToValidate="ddlFromMin" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>
                                 
                    </td>
                </tr>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;" class="auto-style1">To Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;" class="auto-style1">
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="input_box" Width="92px" OnTextChanged="txtToDate_TextChanged" AutoPostBack="True" style="height: 21px"></asp:TextBox>
                                                                <asp:ImageButton ID="imgToDate" runat="server" CausesValidation="False" 
                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Required." Text="*" ControlToValidate="txtToDate" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="imgToDate" PopupPosition="TopRight" TargetControlID="txtToDate">
                                                    </ajaxToolkit:CalendarExtender>
                        &nbsp;
                <asp:DropDownList runat="server" ID="ddlToHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" ControlToValidate="ddlToHrs" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlToMins" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" ControlToValidate="ddlToMins" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;" class="auto-style1">Voyage Duration (Days)</td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;" class="auto-style1">
                        <asp:TextBox ID="txtVoyageDurationDays" runat="server" CssClass="input_box" Width="92px" ReadOnly="true" ></asp:TextBox>
                    </td>
                </tr>
                  <tr id="trVoyageCargo" runat="server" visible="false">
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;" class="auto-style1">Cargo Qty (MT) :</td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;" class="auto-style1">
                        <asp:TextBox ID="txtCargoQty" runat="server" CssClass="input_box" Width="92px"  ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCargoQty" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"> <asp:Button ID="btnSaveVoyage" runat="server" CssClass="btn" onclick="btnSaveVoyage_Click" Text="Save" ValidationGroup="mnth" Width="100px" /> &nbsp;
            <asp:Button ID="btnCancelVoyage" runat="server" CssClass="btn" onclick="btnCancelVoyage_Click" Text="Close" CausesValidation="false" Width="100px" /> </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                         <asp:Label runat="server" ForeColor ="Red" ID="lblPopError" ></asp:Label> 
                    </td>
                </tr>
            </table>
        
            <asp:HiddenField ID="hdnVoyageId" runat="server" Value="" />
        </div> 
    </center>
    </div>
        <div style="position:absolute;top:0px;left:0px; height :525px; width:100%;z-index:100;" runat="server" id="divViewVoyage" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :525px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:90%; height:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px;  opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br />
        <div class="text headerband" style="vertical-align:central;"> <b></b> 
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;" OnClick="btnClose_OnClick" CssClass="btn" />
        </div>
            <table width="100%" id="tblVoyageInfo">
                <tr>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">Voyage # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblVoyageNo" runat="server"></asp:Label></td>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">From Port : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblFromPort" runat="server"></asp:Label></td>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">To Port : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblToPort" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">From Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblVoyFromDt" runat="server"></asp:Label></td>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">To Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblVoyToDt" runat="server"></asp:Label></td>
                    <td width="20%" style="padding:2px 5px 2px 0px;text-align:right;">Voyage Duration (Days) : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblVoyDuration" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" style="padding:2px 0px 2px 5px;text-align:left;">
                         <asp:Button ID="btnAddActualExpenses" runat="server" Text="Add Actual Expenses" CssClass="btn"  OnClick="btnAddActualExpenses_Click"/> &nbsp;
                         <asp:Button ID="btnAddOffHireDetails" runat="server" Text="Add Off-hire" CssClass="btn"  OnClick="btnAddOffHireDetails_Click"  /> &nbsp;
                                  
                    </td>
                </tr>
                 
            </table>
        <br />
            <table width="100%">
                <tr>
                   
                     <td style="width:50%;text-align: left; padding-left: 5px; padding-right: 5px;">
                         <b> Actual Expenses Details</b>
                    </td>
                     <td style="width:50%;text-align: left; padding-left: 5px; padding-right: 5px;">
                        <div id="divOffHireheader" runat="server" visible="false">
                            <b> Off-hire Details </b>
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    
                   <td style="width:50%;text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                       
                                        <col style="width: 7%;" />
                                        <col style="width: 7%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 13%;" />
                                        
                                        <col style="width: 3%;" /> </colgroup>
                                        <tr align="left" class="headerstylegrid">
                                            
                                            <td style="width: 7%;">Edit</td>
                                            <td style="width: 7%;">Delete</td>
                                            <td style="width: 35%;">
                                                Voyage #
                                            </td>
                                            <td style="width: 35%;">
                                                Category
                                            </td>
                                            <td style="width: 13%;">
                                                Amount
                                            </td>
                                           
                                            <td style="width: 3%;"> 

                                            </td>
                                        </tr>
                                   
                                </table>
                                <div id="dvActExpdtls"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                  
                                        <col style="width: 7%;" />
                                        <col style="width: 7%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 13%;" />                                      
                                        <col  />
                    </colgroup>
                   
               <asp:Repeater ID="rptActExpDtls" runat="server"  >
                  <ItemTemplate>
                      <tr class="alternaterow" onMouseOver="this.className='highlight'" onMouseOut="this.className='alternaterow'">
                     
                           
                           <td align="center" style="width: 
    7%;">
                            <asp:ImageButton ID="imgEditActExp" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("ExpensesId") %>' OnClick="imgEditActExp_Click" CausesValidation="false"/>
                           </td>
                           <td align="center" style="width: 
    7%;">
                            <asp:ImageButton ID="imgDeleteActExp" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("ExpensesId") %>' OnClick="imgDeleteActExp_Click"  OnClientClick="return window.confirm('Are you sure to remove this?');" CausesValidation="false"/>
                           </td>
                           <td align="left" style="width: 35%;"><%#Eval("RCVD_VoyageNo")%><asp:HiddenField ID="hdnExpensesId" Value='<%#Eval("ExpensesId") %>' runat="server" /></td>
                          <td align="left" style="width: 35%;"><%#Eval("CategoryName")%></td>
                           <td align="left" style="width: 13%;"><%#Eval("Amount")%></td>    
                            
                           <td ></td>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
                                </div>
                            </td> 
                    <td style="width:50%;text-align: center; padding-left: 5px; padding-right: 5px;">
                        <div id="divOffHireDetails" runat="server" visible="false">
                             <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;" >
                                    <colgroup>
                                       
                                        <col style="width: 7%;" />
                                        <col style="width: 7%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 13%;" />
                                        
                                        <col style="width: 3%;" /></colgroup>
                                        <tr align="left" class="headerstylegrid">
                                            
                                            <td style="width: 7%;">Edit</td>
                                            <td style="width: 7%;">Delete</td>
                                            <td style="width: 35%;">
                                                Voyage #
                                            </td>
                                            <td style="width: 35%;">
                                                Category
                                            </td>
                                            <td style="width: 13%;">
                                                Amount
                                            </td>
                                           
                                            <td style="width: 3%;"> 

                                            </td>
                                        </tr>
                                    
                                </table>
                                <div id="dvOffhiredtls"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                  
                                        <col style="width: 7%;" />
                                        <col style="width: 7%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 35%;" />
                                        <col style="width: 13%;" />                                      
                                        <col  />
                    </colgroup>
                   
               <asp:Repeater ID="rptOffhireDtls" runat="server"  >
                  <ItemTemplate>
                      <tr class="alternaterow" onMouseOver="this.className='highlight'" onMouseOut="this.className='alternaterow'">
                     
                           
                           <td align="center" style="width: 
    7%;">
                            <asp:ImageButton ID="imgEditOffHire" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("OffHireId") %>' OnClick="imgEditOffhire_Click" CausesValidation="false"/>
                           </td>
                           <td align="center" style="width: 
    7%;">
                            <asp:ImageButton ID="imgDeleteOffhire" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" CommandArgument='<%#Eval("OffHireId") %>' OnClick="imgDeleteOffhire_Click"  OnClientClick="return window.confirm('Are you sure to remove this?');" CausesValidation="false"/>
                           </td>
                           <td align="left" style="width: 35%;"><%#Eval("RCVD_VoyageNo")%><asp:HiddenField ID="hdnOffhireId" Value='<%#Eval("OffHireId") %>' runat="server" /></td>
                          <td align="left" style="width: 35%;"><%#Eval("CategoryName")%></td>
                           <td align="left" style="width: 13%;"><%#Eval("Amount")%></td>    
                            
                           <td ></td>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
                                </div>
                        </div>

                    </td>
                </tr>
            </table>
            
        </div> 
    </center>
    </div>
       <div style="position:absolute;top:0px;left:0px; height :475px; width:100%;z-index:100;" runat="server" id="divContractActualExpenses" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :475px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; height:350px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br />
            <asp:HiddenField ID="hdnActualExpId" runat="server" Value="" />
        <div class="text headerband" style="vertical-align:central;"> <b>Contract Actual Expenses</b> 

             <asp:ImageButton ID="ibCloseActExpId" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" CausesValidation="false" OnClick="ibCloseActExpId_Click" />
        </div>
            <table width="100%">
               <%-- <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Contract # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblActExpContractId" runat="server"></asp:Label></td>
                </tr>--%>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Voyage # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:Label ID="lblActExpVoyages" runat="server" Width="200px"></asp:Label></td>
                </tr>
                 <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Expenses Type : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:DropDownList ID="ddlActExpType" runat="server" Width="200px"></asp:DropDownList>
                         <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" ErrorMessage="Required." ControlToValidate="ddlActExpType" InitialValue="0" ValidationGroup="ActExp"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Amount (US $) : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtActExpAmount" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtActExpAmount" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Required." ControlToValidate="txtActExpAmount" ValidationGroup="ActExp" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Remark : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtActExpRemark" runat="server" TextMode="MultiLine"  Width="198px" MaxLength="100" Height="48px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;"></td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                    <asp:Button ID="btnSaveActExp" runat="server" CssClass="btn" Text="Save" ValidationGroup="ActExp" Width="100px" OnClick="btnSaveActExp_Click"/> &nbsp;
                    <asp:Button ID="btnCancelActExp" runat="server" CssClass="btn" Text="Close" CausesValidation="false" Width="100px" OnClick="btnCancelActExp_Click"/> &nbsp; 
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblActExpMsg" runat="server" ForeColor ="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        <br /><br />
        
            
           
            
            
            
        </div> 
    </center>
    </div> 
        <div style="position:absolute;top:0px;left:0px; height :525px; width:100%;z-index:100;" runat="server" id="divContractOffHire" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :525px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; height:550px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
   
        <br />
            <asp:HiddenField ID="hdnOffHireId" runat="server" Value="" />
        <div class="text headerband" style="vertical-align:central;"> <b>Off-hire Details</b> 

             <asp:ImageButton ID="ibOffHire" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" CausesValidation="false" OnClick="ibOffHire_Click" />
        </div>
            <table width="100%">
               <%-- <tr>
                    <td width="25%" style="padding:2px 5px 2px 0px;text-align:right;">Contract # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;"><asp:Label ID="lblActExpContractId" runat="server"></asp:Label></td>
                </tr>--%>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Voyage # : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:Label ID="lblOffHireVoyageNo" runat="server" Width="200px"></asp:Label></td>
                </tr>
                 <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Off-hire Type : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:DropDownList ID="ddlOffHireCategory" runat="server" Width="200px"></asp:DropDownList>
                         <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Required." ControlToValidate="ddlOffHireCategory" InitialValue="0" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Location : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffhireLocation" runat="server" Width="200px"></asp:TextBox>
                         <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Required." ControlToValidate="txtOffhireLocation" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">From Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                         <asp:TextBox ID="txtOffHireFromDt" runat="server" CssClass="input_box" Width="92px" AutoPostBack="True" OnTextChanged="txtOffHireFromDt_TextChanged"></asp:TextBox>
                                                                <asp:ImageButton ID="ibOffHireFromDt" runat="server" CausesValidation="False" 
                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ErrorMessage="Required." Text="*" ControlToValidate="txtOffHireFromDt" ValidationGroup="OffHire" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="ibOffHireFromDt" PopupPosition="TopRight" TargetControlID="txtOffHireFromDt">
                                                    </ajaxToolkit:CalendarExtender>
                         &nbsp;
                <asp:DropDownList runat="server" ID="ddlOffHireFromHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" ControlToValidate="ddlOffHireFromHrs" ErrorMessage="Required." Text="*" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlOffHireFromMins" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" ControlToValidate="ddlOffHireFromMins" ErrorMessage="Required." Text="*" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                                 
                    </td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;" class="auto-style1">To Date : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;" class="auto-style1">
                        <asp:TextBox ID="txtOffHireToDt" runat="server" CssClass="input_box" Width="92px"  AutoPostBack="True" style="height: 21px" OnTextChanged="txtOffHireToDt_TextChanged"></asp:TextBox>
                                                                <asp:ImageButton ID="ibOffhireToDt" runat="server" CausesValidation="False" 
                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ErrorMessage="Required." Text="*" ControlToValidate="txtOffHireToDt"  ValidationGroup="OffHire" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="ibOffhireToDt" PopupPosition="TopRight" TargetControlID="txtOffHireToDt">
                                                    </ajaxToolkit:CalendarExtender>
                          &nbsp;
                <asp:DropDownList runat="server" ID="ddlOffhireToHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator17" ControlToValidate="ddlOffhireToHrs" ErrorMessage="Required." Text="*" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlOffhireToMins" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
                            <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text="54"></asp:ListItem>
                            <asp:ListItem Value="55" Text="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text="59"></asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator18" ControlToValidate="ddlOffhireToMins" ErrorMessage="Required." Text="*" ValidationGroup="OffHire"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                     <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Off-hire Duration (Days) : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffhireDuration" runat="server" Width="92px" MaxLength="10" ReadOnly="true"></asp:TextBox>
                        
                    </td>
                </tr>
                
                 
                
                <tr>
                     <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Per Day Hire Amount (US $): </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffhireAmountperday" runat="server" Width="92px" MaxLength="10" ReadOnly="true"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Total Hire Amount (US $) : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffHireAmount" runat="server" Width="200px" MaxLength="10" ReadOnly="true"  ></asp:TextBox> 
                        <%--OnTextChanged="txtOffHireAmount_TextChanged" AutoPostBack="true"--%>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtOffHireAmount" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ErrorMessage="Required." ControlToValidate="txtOffHireAmount" ValidationGroup="OffHire" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Reason of Off-hire  : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffHireReason" runat="server" TextMode="MultiLine"  Width="198px" MaxLength="100" Height="48px"></asp:TextBox>
                         <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ErrorMessage="Required." ControlToValidate="txtOffHireReason" ValidationGroup="OffHire" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;">Remark : </td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                        <asp:TextBox ID="txtOffHireRemarks" runat="server" TextMode="MultiLine"  Width="198px" MaxLength="100" Height="48px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="35%" style="padding:2px 5px 2px 0px;text-align:right;"></td>
                    <td style="padding:2px 0px 2px 5px;text-align:left;">
                    <asp:Button ID="btnSaveOffHire" runat="server" CssClass="btn" Text="Save" ValidationGroup="OffHire" Width="100px" OnClick="btnSaveOffHire_Click" /> &nbsp;
                    <asp:Button ID="btnCancelOffhire" runat="server" CssClass="btn" Text="Close" CausesValidation="false" Width="100px" OnClick="btnCancelOffhire_Click" /> &nbsp; 
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblOffhiremsg" runat="server" ForeColor ="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        <br /><br />
        
            
           
            
            
            
        </div> 
    </center>
    </div>
    </form>
</body>
</html>

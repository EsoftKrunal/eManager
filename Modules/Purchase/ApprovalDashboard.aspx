<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalDashboard.aspx.cs" Inherits="ApprovalDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
     <link rel="stylesheet" href="Modules/HRD/Styles/StyleSheet.css" />  
    <style type="text/css" >
        .tdApprovalDashboard {
            width:30%;
            padding: 10px 20px 15px 0px;
        }
        .tdDiv {
            border: 1px solid #4C7A6F; 
            border-radius: 10px;
            padding:5px;
            min-height:183px;
            text-align:center;
        }
        @media screen and (max-width:767px) {

            .trApprovalDashboard {
                display: flex;
                flex-direction: column;
            }

            .tdApprovalDashboard {
                width:100% !important;
               
                padding: 10px 20px 15px 0px;
            }

            .tdDiv {
            border: 1px solid #4C7A6F; 
            border-radius: 10px;
            padding:5px;
            min-height:auto;
            text-align:center;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
      <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="table-responsive">
       <table style="width: 100%; text-align: center; height:550px; ">  
            <tr class="text headerband">
                <td colspan="2" style="text-align:center;font-size: 14px;" class="text headerband" >
               Dashboard</td>
            </tr>
            <tr>  
                <td align="center" style="padding-top:25px;" >  
                    <asp:Label ID="lblMsg" runat ="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                    <br />
                    <asp:Panel ID = "Panel1" runat="server">
                        <table width="100%"> 
                            <tr class="trApprovalDashboard" >
                                <td class="tdApprovalDashboard" >
                                    <div class="tdDiv">
                                        <asp:Label ID="lblPoApproval" runat="server" Text="Purchase Approvals" style="text-align:center;Font-Family:Arial;font-size: 16px;font-weight:bold;" ForeColor="#206020" ></asp:Label>
                                        <br />
                                        <hr style="height:3px;background-color:#206020;padding:0px 5px 0px 5px;"/>
                                    <div style="text-align:center;Font-Family:Arial;font-size: 12px;font-weight:bold;">
                                        <table width="100%">
                                            <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> PO Approval Stage1 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                    <div style="display:flex;"> 
                                                         <asp:LinkButton ID="lbPendingPoStage1" runat="server" Text="0" OnClick="lbPendingPoStage1_Click" ></asp:LinkButton>  
                                                  <div id="divPoStg1" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                   
                                                </td>
                                            </tr>
                                             <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> PO Approval Stage2 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;"> 
                                                    <asp:LinkButton ID="lbPendingPoStage2" runat="server" Text="0" OnClick="lbPendingPoStage2_Click"></asp:LinkButton> 
                                                          <div id="divPoStg2" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                         </td>
                                            </tr>
                                            <tr>
                                                 <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> PO Approval Stage3 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;"> 
                                                    <asp:LinkButton ID="lbPendingPoStage3" runat="server" Text="0" OnClick="lbPendingPoStage3_Click"></asp:LinkButton>
                                                          <div id="divPoStg3" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div></td>
                                            </tr>
                                             <tr>
                                                 <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> PO Approval Stage4 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;">
                                                    <asp:LinkButton ID="lbPendingPoStage4" runat="server" Text="0"  OnClick="lbPendingPoStage4_Click"></asp:LinkButton> 
                                                          <div id="divPoStg4" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                         </td>
                                            </tr>
                                             <tr>
                                                 <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> PO Ready to Place Order : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;">
                                                    <asp:LinkButton ID="lbPendingPoStage5" runat="server" Text="0"  OnClick="lbPendingPoStage5_Click"></asp:LinkButton> 
                                                          <div id="divPoStg5" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                         </td>
                                            </tr>
                                        </table>
                                    </div>
                                    </div>
                                    
                                </td>
                                <td class="tdApprovalDashboard" >
                                     <div class="tdDiv">
                                        <asp:Label ID="lblInvoiceApproval" runat="server" Text="Invoice Approvals" style="text-align:center;Font-Family:Arial;font-size: 16px;font-weight:bold;" ForeColor="#206020" ></asp:Label>
                                         <br />
                                    <hr style="height:3px;background-color:#206020;padding:0px 5px 0px 5px;"/>
                                    <div style="text-align:center;Font-Family:Arial;font-size: 12px;font-weight:bold;">
                                        <table width="100%">
                                            <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> Approval Stage1 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                    <div style="display:flex;">
                                                    <asp:LinkButton ID="lbPendingInvoiceStage1" runat="server" Text="0" OnClick="lbPendingInvoiceStage1_Click" ></asp:LinkButton> 
                                                        <div id="divInvStg1" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div></td>
                                            </tr>
                                             <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> Approval Stage2 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                    <div style="display:flex;">
                                                    <asp:LinkButton ID="lbPendingInvoiceStage2" runat="server" Text="0" OnClick="lbPendingInvoiceStage2_Click" ></asp:LinkButton> 
                                                         <div id="divInvStg2" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                        </td>
                                            </tr>
                                            <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> RFP Approval : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                    <div style="display:flex;">
                                                    <asp:LinkButton ID="lblRFPApprovalCount" runat="server" Text="0" OnClick="lblRFPApprovalCount_Click"></asp:LinkButton> 
                                                         <div id="divRFP" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                        </td>
                                            </tr>
                                           
                                        </table>
                                    </div>
                                    </div>
                                    
                                </td>
                                <td class="tdApprovalDashboard">
                                     <div class="tdDiv">
                                        <asp:Label ID="lblVendorApprovl" runat="server" Text="Vendor Approvals" style="text-align:center;Font-Family:Arial;font-size: 16px;font-weight:bold;" ForeColor="#206020" ></asp:Label>
                                          <br />
                                    <hr style="height:3px;background-color:#206020;padding:0px 5px 0px 5px;"/>
                                    <div style="text-align:center;Font-Family:Arial;font-size: 12px;font-weight:bold;">
                                        <table width="100%">
                                            <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> Approval Stage1 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;">
                                                    <asp:LinkButton ID="lblPendingVendorStage1" runat="server" Text="0" OnClick="lblPendingVendorStage1_Click"></asp:LinkButton> 
                                                         <div id="divVenStg1" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                         </td>
                                            </tr>
                                             <tr>
                                                <td width="75%" style="padding:2px 5px 2px 0px;text-align:right;"> Approval Stage2 : </td>
                                                <td width="25%" style="padding:2px 0px 2px 5px;text-align:left;"> 
                                                     <div style="display:flex;">
                                                    <asp:LinkButton ID="lblPendingVendorStage2" runat="server" Text="0" OnClick="lblPendingVendorStage2_Click"></asp:LinkButton> 
                                                         <div id="divVenStg2" runat="server" visible="false" style="margin-left:7px;" > 
                                                      <img src="../HRD/Images/Bell.png" />
                                                  </div>
                                                    </div>
                                                         </td>
                                            </tr>
                                           
                                        </table>
                                        
                                        
                                        
                                        
                                        
                                    </div>
                                    </div>
                                   

                                </td>
                                
                            </tr>
                            
                        </table>
                     </asp:Panel>
                </td>  
            </tr>  
        </table>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselSetup.aspx.cs" Inherits="MRV_VesselSetup" %>
<%@ Register src="mrvmenu.ascx" tagname="mrvmenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    
 <%-- <link rel="stylesheet" href="../../HRD/Styles/style.css" /> 
	<link rel="stylesheet" type="text/css" href="../../../css/app_style.css" />--%>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
            <style type="text/css">
.selbtn
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;

	
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
            </style>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    
    <div style="font-family:Arial;font-size:12px;" >
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
           <%-- <div class="text headerband">
                Vessel SetUp
            </div>--%>
           <%-- <div style="border-bottom:solid 5px #4371a5;"></div>
            <h1>Vessel SetUp</h1>--%>
           <div >
            <table>
                <tr style="height:25px;">
                        <td width="width:120px;" style="padding-right:10px;text-align:right;">
                          <b> Select Vessel : </b> 
                        </td>
                        <td width="width:200px;" style="padding-left:10px;text-align:left;">
                            <asp:DropDownList ID="ddlVessel" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                              <div style="padding:10px; text-align:center;" >
                                   <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click" Text="Export to Ship" CssClass="btn" Visible="false" />
                               </div>
                        </td>

                    </tr>
                <tr>
                    <td>
                        <div style="height:400px;overflow-x:hidden;overflow-y:scroll; position:absolute;top:40px;left:0px;width:100%">
                <table cellpadding="0" cellspacing="0" border="0"  width="100%">               
                    <tr>
                        <td colspan="3">
                            <div>
                   <%--<asp:UpdatePanel runat="server">
                       <ContentTemplate>  --%>
                           <div class="modal" runat="server" id="dvModal" visible="false"></div> 
                           <div style="display:none">
                               <asp:Button runat="server" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_Click" />
                               <asp:HiddenField runat="server" ID="hfdVesselCode"/>
                           </div>
                           <div runat="server" id="dvVesselDetails" visible="false">
                                                        
                                    <asp:Linkbutton runat="server" Text="Home"  CommandArgument="0"  ID="li1" OnClick="btnTab_Click" ForeColor="#206020" Font-Bold="True" Font-Size="Medium" ></asp:Linkbutton> &nbsp; &nbsp;&nbsp;
                                    <asp:Linkbutton runat="server" Text="Emission Sources"  CommandArgument="1" OnClick="btnTab_Click" ID="li2" ForeColor="#206020" Font-Bold="True" Font-Size="Medium" ></asp:Linkbutton> &nbsp; &nbsp;&nbsp;
                                    <asp:Linkbutton runat="server" Text="Fuel Tanks"  CommandArgument="2" OnClick="btnTab_Click" ID="li3" ForeColor="#206020" Font-Bold="True" Font-Size="Medium" ></asp:Linkbutton> &nbsp; &nbsp;&nbsp;
                                    <asp:Linkbutton runat="server" Text="Flow Meters"  CommandArgument="3" OnClick="btnTab_Click" ID="li4" ForeColor="#206020" Font-Bold="True"  Font-Size="Medium"></asp:Linkbutton> &nbsp; &nbsp;&nbsp;
                                     <asp:Linkbutton runat="server" Text="Vessel Particulars"  CommandArgument="4" OnClick="btnTab_Click" ID="li5" ForeColor="#206020" Font-Bold="True" Font-Size="Medium" ></asp:Linkbutton> &nbsp; &nbsp;&nbsp;
                                   
                     
                               <asp:MultiView runat="server" ID="mv1" ActiveViewIndex="0" >
                                    <asp:View runat="server">
                                        <div>
                                            <div style="padding:10px;font-size:13px;color:#4371a5; font-weight:bold;">Emission Sources Fuel Consumption Method</div>
                                            <asp:Repeater runat="server" ID="rptFuelConsMethod">
                                                <ItemTemplate>
                                                    <div style="text-align:left;position:relative;height:55px;">
                                                        <div style="position:absolute;width:100px; height:50px;background-color:#4a867e;line-height:50px;color:white;text-align:center;top:0px;left:10px;font-size: 13px;font-weight: bold;">
                                                            <%#Eval("EmissionSourceName")%>
                                                        </div> 
                                                        <div style="margin-left:130px;line-height: 50px;font-size: 14px;">
                                                           <b> Fuel Consumption = </b> <%#GetFuelConsumptionString(Eval("EmissionSourceId")) %>
                                                        </div>
                                                    </div>
                                                  </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        </asp:View>
                                   <asp:View runat="server">

                      
                                   <h3>                           
                                       <asp:ImageButton runat="server" ID="btn_Add" ImageUrl="~/Modules/HRD/Images/add_16.gif" style="float:left;margin-left:0px;" OnClick="btn_AddSource_Click" />                           
                                       <span style="margin-left:10px;">Emission Sources</span>
                                       </h3>
                                  <div>
                               <table cellpadding="0" cellspacing="0" border="0"  width="100%">
                                    <tr class= "headerstylegrid">
                                        <th style="width:30px;">Edit</th>
                                        <th style="text-align:left">Emission Source Name</th>
                                        <th style="width:200px;">Linked Flow Meters</th>
                                    </tr>
                                   <asp:Repeater runat="server" ID="rptEmissionSource">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:center"><asp:ImageButton runat="server" ID="btn_Edit" ImageUrl="~/Modules/HRD/Images/editX12.jpg" OnClick="btn_EditSource_Click" CommandArgument='<%#Eval("EmissionSourceId")%>' /></td>
                                                <td style="text-align:left"><%#Eval("EmissionSourceName")%>
                                                    
                                                </td>
                                                <td style="text-align:left">
                                                   <asp:LinkButton runat="server" ID="lnk_ShowLinkedFlowMeters" Text='<%#Eval("FlowMetersCount").ToString() + " flowmeter linked."%>' OnClick="lnk_ShowLinkedFlowMeters_Click" CommandArgument='<%#Eval("EmissionSourceId")%>' esname='<%#Eval("EmissionSourceName")%>' ></asp:LinkButton>
                                            </tr>
                                          </ItemTemplate>
                                  </asp:Repeater>

                                   </table>
                           </div>
                                       
                                   </asp:View>
                                <asp:View runat="server">
                               <h3>
                                   <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Modules/HRD/Images/add_16.gif" style="float:left;margin-left:0px;" OnClick="btn_AddFuelTank_Click" />
                                   <span style="margin-left:10px;">Fuel Tanks</span>
                           
                               </h3>
                                <div>
                           <table cellpadding="0" cellspacing="0" border="0"  width="100%">
                                 <tr class= "headerstylegrid">
                                    <th style="width:30px;">Edit</th>
                                    <th style="text-align:left;width:300px;">Fuel Tank Name</th>
                                    <th style="text-align:left;width:200px;">Fuel Type</th>
                                    <th style="text-align:right;width:200px;">100% Capacity (M<sup>3</sup>)</th>
                                    <th style="text-align:left">Monitoring Device</th>
                                </tr>
                               <asp:Repeater runat="server" ID="rptFuelTanks">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align:center"><asp:ImageButton runat="server" ID="btn_Edit" ImageUrl="~/Modules/HRD/Images/editx12.jpg" OnClick="btn_EditFuelTank_Click" CommandArgument='<%#Eval("FuelTankId")%>' /></td>
                                            <td style="text-align:left"><%#Eval("FuelTankName")%></td>
                                            <td style="text-align:left"><%#Eval("ShortName")%></td>
                                            <td style="text-align:right"><%#Eval("Capacity")%></td>
                                            <td style="text-align:left"><%#Eval("MeasuringDevice")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                       </div>   
                                 </asp:View>
                                 <asp:View runat="server">
                                       <h3>
                                           <asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/Modules/HRD/Images/add_16.gif" style="float:left;margin-left:0px;" OnClick="btn_AddFlowMeter_Click" />
                                           <span style="margin-left:10px;">Flow Meters</span>
                           
                                       </h3>
                                      <table cellpadding="0" cellspacing="0" border="0"  width="100%">
                                         <tr class= "headerstylegrid">
                                            <th style="width:30px;">Edit</th>
                                            <th style="text-align:left;">Flow Meter Name</th>
                                            <th style="text-align:left;width:200px;">Type</th>
                                            <th style="text-align:left;width:200px;">PMS Comp. Code</th>
                                            <th style="text-align:left;width:200px;">Flow Unit</th>
                                        </tr>
                                       <asp:Repeater runat="server" ID="rptFlowMeters">
                                            <ItemTemplate>
                                                <tr >
                                                    <td style="text-align:center"><asp:ImageButton runat="server" ID="btn_Edit" ImageUrl="~/Images/editx12.jpg" OnClick="btn_EditFlowMeter_Click" CommandArgument='<%#Eval("FlowMeterId")%>' /></td>
                                                    <td style="text-align:left"><%#Eval("FlowMeterName")%></td>
                                                    <td style="text-align:left"><%#(Eval("FlowMeterType").ToString()=="I")?"Inlet":"Outlet"%></td>
                                                    <td style="text-align:left"><%#Eval("PMSCompCode")%></td>
                                                    <td style="text-align:left"><%#(Eval("ReadingMode").ToString()=="C")?"Cubic Mtr.":"Ltr."%></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                       </table>
                                 </asp:View>

                                   <asp:View runat="server">
                                       
                                       <div style="width:100%;height:350px;overflow-x:hidden;overflow-y:scroll;"> 
                                          <h3><span style="margin-left:10px;">Part A</span></h3>
                                          <table cellpadding="0" cellspacing="0" border="0"  width="100%" style="text-align:left;">                                                                                
                                          <col width="170px" />
                                          <col width="350px" />
                                            <col width="170px" />
                                              <col />
                                            <tr class= "headerstylegrid">                                                    
                                                <td>Vessel Code</td>
                                                <td><asp:Label ID="lblVP_VesselCode" runat="server"  ></asp:Label></td>
                                                <td>Vessel Name</td>
                                                <td><asp:Label ID="lblVP_VesselName" runat="server"></asp:Label></td>
                                            </tr>
                                          
                                          <tr>                                                    
                                                <td>IMO ID</td>
                                                <td><asp:TextBox ID="lblVP_IMOID" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              <td> Port Of Registry </td>
                                                <td><asp:TextBox ID="lblVP_PortOfRegistry" runat="server" CssClass="input medium"></asp:TextBox></td>
                                            </tr>
                                          
                                          <tr>                                                    
                                                <td>Home Port</td>
                                                <td><asp:TextBox ID="lblVP_HomePort" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              <td>Ice Class</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVP_IceClass" runat="server" CssClass="input medium">
                                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                                        <asp:ListItem Value="Polar Class PC1 — PC7" Text="Polar Class PC1 — PC7"></asp:ListItem>
                                                        <asp:ListItem Value="Finnish-Swedish Ice Class IC" Text="Finnish-Swedish Ice Class IC"></asp:ListItem>
                                                        <asp:ListItem Value="IB" Text="IB"></asp:ListItem>
                                                        <asp:ListItem Value="IA" Text="IA"></asp:ListItem>
                                                        <asp:ListItem Value="IA Super" Text="IA Super"></asp:ListItem>
                                                    </asp:DropDownList>

                                                    







                                                </td>
                                            </tr>
                                          
                                          <tr>                                                    
                                                <td>EEDI</td>
                                                <td><asp:TextBox ID="lblVP_EEDI" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              <td>EIV</td>
                                                <td><asp:TextBox ID="lblVP_EIV" runat="server" CssClass="input medium"></asp:TextBox></td>
                                            </tr>
                                          
                                          <tr>                                                    
                                                <td>Ship Owner</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="lblVP_ShipOwner" runat="server" CssClass="input medium"></asp:TextBox>
                                                </td>
                                              
                                            </tr>
                                          <tr>
                                              <td>Address Of Shipowner</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="lblVP_AddressOfShipowner" runat="server" CssClass="input medium" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox>
                                                </td>
                                          </tr>
                                          <tr>                                                    
                                                <td>Name Of Company</td>
                                                <td colspan="3"><asp:TextBox ID="lblVP_NameOfCompany" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              
                                            </tr>
                                          <tr>
                                              <td>Address Of Company</td>
                                                <td colspan="3"><asp:TextBox ID="lblVP_AddressOfCompany" runat="server" CssClass="input medium" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td>
                                          </tr>
                                          <tr>                                                    
                                                <td>Contact Person</td>
                                                <td colspan="3"><asp:TextBox ID="lblVP_ContactPerson" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              
                                            </tr>
                                              <tr>
                                                  <td>Address Of Company</td>
                                                <td colspan="3" ><asp:TextBox ID="lblCP_VP_AddressOfCompany" runat="server" CssClass="input medium" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td>
                                              </tr>
                                          <tr>                                                    
                                                <td>Telephone</td>
                                                <td><asp:TextBox ID="lblCP_VP_Telephone" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              <td>Email</td>
                                                <td><asp:TextBox ID="lblCP_VP_Email" runat="server" CssClass="input medium"></asp:TextBox></td>
                                            </tr>
                                          
                                       </table>

                                           <h3><span style="margin-left:10px;">Part B</span></h3>
                                           <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">                                                                                
                                          <col width="170px" />
                                          <col width="350px" />
                                            <col width="170px" />
                                              <col />
                                               <tr >                                                    
                                                <td>Name of Verifier</td>
                                                <td colspan="3"><asp:TextBox ID="lblVP_NameofVerifier" runat="server" CssClass="input medium"></asp:TextBox></td>
                                                
                                            </tr>                                          
                                               <tr>
                                                   <td >Address of Verifier</td>
                                                    <td colspan="3"><asp:TextBox ID="lblVP_AddressofVerifier" runat="server" CssClass="input medium" TextMode="MultiLine" Width="400px" Rows="4"></asp:TextBox></td>
                                               </tr>
                                                <tr>                                                    
                                                <td>Accreditation No</td>
                                                <td><asp:TextBox ID="lblVP_AccreditationNo" runat="server" CssClass="input medium"></asp:TextBox></td>
                                              <td> Verifiers Statement </td>
                                                <td><asp:TextBox ID="lblVP_VerifiersStatement" runat="server" CssClass="input medium"></asp:TextBox></td>
                                            </tr>
                                       </table>
                                        </div>
                                       <asp:Button ID="btnSaveVesselParticulars" runat="server" Text="Save" OnClick="btnSaveVesselParticulars_OnClick" CssClass="btn"/>

                                 </asp:View>

                               </asp:MultiView>                                                 

                           <div class="modal_frame" runat="server" id="dvAddEditSource" visible="false">
                                <div class="text headerband">Add / Edit Emission Source</div>
                                <div class="modal_content">
                                    <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                                        <tr>
                                            <td style="text-align:left;width:130px;">Emission Source Name</td>
                                            <td style="text-align:left"><asp:TextBox CssClass="input input_text large" runat="server" ID="txtEmissionSourceName"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="txtEmissionSourceName"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                     
                                    </table>
                                </div>
                                <div class="modal_footer"">
                                    <asp:Button runat="server" id="btnSave" CssClass="btn" Text="Save" OnClick="btnSaveEmissionSource_Click" />
                                    <asp:Button runat="server" id="btnCancel" CssClass="btn" Text="Cancel" OnClick="btnCancelEmissionSource_Click" CausesValidation="false" />
                                </div>
                            </div>
                           <div class="modal_frame" runat="server" id="dvAddEditFuelTank" visible="false">
                                <div class="text headerband">Add / Edit Fuel Tank</div>
                                <div class="modal_content">
                                    <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                                        <tr>
                                            <td style="text-align:left;width:180px;">Fuel Tank Name</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtFuelTankName"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ErrorMessage="*" ControlToValidate="txtFuelTankName"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="text-align:left;">Fuel Type</td>
                                            <td style="text-align:left"><asp:DropDownList runat="server" CssClass="input input_select medium" ID="ddlFuelType"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;">100% Capacity (M<sup>3</sup>)</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" ID="txtCapacity" CssClass="input input_number small" ></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ErrorMessage="*" ControlToValidate="txtCapacity"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;">Measuring Device Name</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" ID="txtMeasuringDevice" CssClass="input input_text large" ></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ErrorMessage="*" ControlToValidate="txtMeasuringDevice"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal_footer"">
                                    <asp:Button runat="server" id="btnSave1" CssClass="btn" Text="Save" OnClick="btnSaveFuelTank_Click" />
                                    <asp:Button runat="server" id="btnCancel1" CssClass="btn" Text="Cancel" OnClick="btnCancelFuelTank_Click" CausesValidation="false" />
                                </div>
                            </div>
                           <div class="modal_frame" runat="server" id="dvAddEditFlowMetres" visible="false">
                                <div class="text headerband">Add / Edit Flow Meter</div>
                                <div class="modal_content">
                                    <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                                        <tr>
                                            <td style="text-align:left;width:130px;">Flow Meter Name</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtFlowMeterName"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ErrorMessage="*" ControlToValidate="txtFlowMeterName"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="text-align:left;">Flow Meter Type</td>
                                            <td style="text-align:left">
                                                <asp:DropDownList runat="server" CssClass="input input_select medium" ID="ddlFlowMeterType">
                                                    <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Inlet" Value="I"></asp:ListItem>
                                                    <asp:ListItem Text="Outlet" Value="O"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ErrorMessage="*" ControlToValidate="ddlFlowMeterType">

                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align:left;">Flow Unit</td>
                                            <td style="text-align:left">
                                                <asp:DropDownList runat="server" CssClass="input input_select medium" ID="ddlFlowUnit">
                                                    <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Cubic Mtr." Value="C"></asp:ListItem>
                                                    <asp:ListItem Text="Ltr." Value="L"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ErrorMessage="*" ControlToValidate="ddlFlowUnit">

                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align:left;width:130px;">PMS Component Code :</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtPMSCompCode"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;width:130px;">Maker :</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtMaker"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;width:130px;">Model :</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtModel"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;width:130px;">Part # :</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtPartNo"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;width:130px; vertical-align:top;">Other Details ( if any ) :</td>
                                            <td style="text-align:left"><asp:TextBox runat="server" CssClass="input input_text large" ID="txtOtherDetails" TextMode="MultiLine" Rows="5"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal_footer"">
                                    <asp:Button runat="server" id="btnSaveFlowMeter" CssClass="btn" Text="Save" OnClick="btnSaveFlowMeter_Click" />
                                    <asp:Button runat="server" id="btnCancelFlowMeter" CssClass="btn" Text="Cancel" OnClick="btnCancelFlowMeter_Click" CausesValidation="false" />
                                </div>
                            </div>     
                           <div class="modal_frame" runat="server" id="dvLinkFlowMeters" visible="false">
                                <div class="text headerband">Link Emission Source to Flow Meters</div>
                                <div class="modal_content">
                                    <div style="text-align:left;padding:8px;">
                                        <b>Emission Source Name : </b> <asp:Label runat="server" ID="lblEmissionsourceName"></asp:Label>
                                    </div>
                                    <table cellpadding="0" cellspacing="5" border="0" class="nobordered" width="100%">
                                        <tr>
                                            <td style="text-align:center;width:50%;font-size:12px; background-color:#4a867e;color:white;"><b>Inlet Flow Meters</b></td>
                                            <td style="text-align:center;font-size:12px; background-color:#4a867e;color:white;"><b>Outlet Flow Meters</b></td>                                            
                                        </tr>
                                         <tr>
                                            <td style="text-align:left">
                                                <asp:CheckBoxList runat="server" ID="chhklst_InletFM"></asp:CheckBoxList>
                                            </td>
                                            <td style="text-align:left">
                                                <asp:CheckBoxList runat="server" ID="chhklst_OutletFM"></asp:CheckBoxList>
                                            </td>                                            
                                        </tr>
                                     
                                    </table>
                                </div>
                                <div class="modal_footer"">
                                    <asp:Button runat="server" id="btnLink" CssClass="btn" Text="Save" OnClick="btnSaveLink_Click" />
                                    <asp:Button runat="server" id="btnCancelLink" CssClass="btn" Text="Cancel" OnClick="btnCancelLink_Click" CausesValidation="false" />
                                </div>
                            </div>
                              <%-- <div style="border-top:solid 1px #efeeee; margin-top:5px;">
                                                        <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%">
                                                                <tr>
                                                                    <td style="width:30px;"><b>Edit</b></td>
                                                                    <td style="text-align:left"><b>FlowMeter Name</b></td>
                                                                    <td style="width:200px;"><b>Type</b></td>
                                                                </tr>
                                                                  <asp:Repeater runat="server" DataSource='<%#GetLinkedFlowMeters(Eval("EmissionSourceId"))%>'>
                                                                    <ItemTemplate>
                                                                    <tr>
                                                                        <td style=""><asp:ImageButton runat="server" ID="btn_Edit" ImageUrl="~/Images/editx12.jpg" OnClick="btn_EditSource_Click" CommandArgument='<%#Eval("FlowMeterId")%>' /></td>
                                                                        <td style="text-align:left"><%#Eval("FlowMeterName")%></td>
                                                                        <td style=""><%#(Eval("FlowMeterType").ToString()=="I")?"Inlet":"Outlet"%></td>
                                                                    </tr>
                                                                    </ItemTemplate>
                                                                 </asp:Repeater>
                                                        </table>
                                                    </div>
                               --%>  
                       </div>                  
                        
                       <div >
                                <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                                        
                       </div>   
                                  
                     <%--  </ContentTemplate>
                   </asp:UpdatePanel>--%>
               </div>
                        </td>
                        
                    </tr>
                    <%--<asp:Repeater runat="server" ID="rptVessels">
                        <ItemTemplate>
                            <tr>
                                <td style="width:30px;"><input type="radio" vslcode='<%#Eval("VESSELCODE")%>' onclick='SelectVessel(this)' name="radvsl" id='rad-<%#Eval("Vesselid")%>' /></td>
                                <td style="text-align:left"><label for='rad-<%#Eval("Vesselid")%>'><%#Eval("Vesselname")%></label></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>--%>
                    </table>
            </div>
                    </td>
                </tr>
            </table>
            
            
            </div>
    </div>
    <script type="text/javascript">
        function SelectVessel(ctl)
        {
            document.getElementById("hfdVesselCode").value = ctl.getAttribute("vslcode");
            document.getElementById("btnRefresh").focus();
            document.getElementById("btnRefresh").click();
        }
         
    </script>
   
    </form>
</body>
</html>
                                        

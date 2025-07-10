<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoyageHistory1.aspx.cs" Inherits="MRV_VoyageHistory1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="mrvmenu.ascx" tagname="mrvmenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
     <!-- CSS -->
     <link href="style.css?14" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/JS/jquery.datetimepicker.js"/>
     
     <script type="text/javascript" src="../../HRD/JS/jquery.min.js"></script>
     <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="Stylesheet" href="../js/AutoComplete/jquery-ui.css" />
     <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <!-- KPI -->
     <script type="text/javascript" src="../js/KPIScript.js"></script>    
    <style type="text/css">
        .OpenVoyage 
        {
            font-size:11px;font-weight:bold;text-align:left;
        }
        .OpenVoyage span
        {
            font-size:12px;font-weight:normal;
        }
    </style> 
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript">
        function closeWindow() {
            try {
                window.close();
            }
            catch (err) {

            }
        }
    </script>
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div style="border-bottom:solid 5px #4371a5;"></div>
            <h1 style="text-align:center" class="text headerband" >
                <span style="margin-left:10px;"> Voyage Details </span> 
                            
            </h1>
            <div class="OpenVoyage" >
                <table cellpadding="5" cellspacing="5" border="0" style="margin:0px auto;width:100%" class="padded bordered">
                    <col width="150px" />
                    <col width="10px" />
                    <col width="300px" />
                    <col width="180px" />
                    <col width="10px" />
                    <col width="500px" />
                    <col width="150px" />
                    <col width="10px" />
                    <col/>
                    <tr>
                        <td style="text-align:left;">Voyage No</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblVoyageNo" runat="server"></asp:Label>
                            <asp:Label ID="lblEUVoyage" runat="server" style="font-weight:bold;"></asp:Label>
                        </td>
                        <td style="text-align:left;">Voyage Duration</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            <b><i> ( UTC Time )</i></b>
                            To
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            <b><i> ( UTC Time )</i></b>
                        </td>
                         <td style="text-align:left;">Voyage Condition</td>
                         <td> : </td>
                        <td><asp:Label ID="lblCondition" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">From - To Port</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblFromPort" runat="server"></asp:Label>
                            To
                            <asp:Label ID="lblToPort" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:left;"> Distance Travelled ( SBE to FWE)</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblDistanceToGo" runat="server"></asp:Label> <b><i> ( NM )</i></b>
                        </td>
                        <td style="text-align:left;">Total CO<sub>2</sub> Emission</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblTotalCO2" runat="server"></asp:Label><b><i> ( Tonne )</i></b>
                            
                        </td>
                    </tr>
                          <tr>
                        <td style="text-align:left;">Master Name </td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblMasterName" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:left;"> Chief Engineer Name</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblCEName" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:left;">Calculation Method</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblcalcMethod" runat="server"></asp:Label><b></b>                            
                        </td>
                    </tr>
                          <tr>
                        <td style="text-align:left;">Cargo Carried</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblCargoCarried" runat="server"></asp:Label>  <b><i> ( MT )</i></b>
                        </td>
                        <td style="text-align:left;"> Transport Work</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblTotalTransportWork" runat="server"></asp:Label> <b><i> ( NM * MT)</i></b>
                        </td>
                        <td style="text-align:left;"></td>
                        <td>  </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
                
                </div>    
            <br /> 
        <div>
            <div style="padding:5px;">
                <asp:Label runat="server" ID="lblErrorMessage" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
               <table cellpadding="0" cellspacing="1" border="0" width="100%">
                  <tr>
                      <td style="width:50%;"><h1 style="background-color:#24619b;color:white;">Consumption At Sea</h1></td>
                      <td> <h1 style="background-color:#e89632;color:white;">Consumption At Port</h1></td>
                  </tr>
                  <tr>
                      <td>
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;background-color:rgb(215, 234, 253)">
                              <col />
                                <col width="150px;" />
                              <tr>
                                  <td>Time At Sea </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblTimeAtSea" runat="server"></asp:Label></b>
                                  </td>
                              </tr>
                              <tr>
                                  <td>Time At Anchorage </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblTimeAtAnchorage" runat="server"></asp:Label></b>
                                  </td>
                              </tr>
                              <tr>
                                  <td>CO<sub>2</sub> Emission ( TONNE )</td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblCo2EmissionAtSea" runat="server"></asp:Label></b> 
                                  </td>
                              </tr>                                                  
                              </table>                                                 
                      </td>
                      <td valign="top">
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;background-color:#f3e2ce;">
                              <col />
                                <col width="150px;" />
                              <tr>
                                  <td>CO<sub>2</sub> Emission ( TONNE )</td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblCo2EmissionInPort" runat="server"></asp:Label></b> 
                                  </td>
                              </tr>                              
                            </table>
                      </td>
                  </tr>
                  </table>
            <asp:Panel runat="server" ID="pnl_FM" Visible="false">
           
                   <table cellpadding="0" cellspacing="1" border="" class="bordered" width="100%">
                          <tr>
                              <td style="width:50%">
                              <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                                <col />
                                <col width="180px;" />
                                <col width="180px;" />
                                <col width="180px;" />    
                                <tr style="background-color:#9bd0f7">
                                     <td><b>Emission Source</b></td>
                                      <td style="text-align:center;"><b>Heavy Fuel Oil (ISO 8217 Grades RME through RMK) HFO<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Light Fuel Oil (ISO 8217 Grades RMA through RMD) LFO<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Diesel/Gas Oil (ISO 8217 Grades DMX through DMC) DO<br/>( MT )</b></td>                                        
                                  </tr>
                                 <asp:Repeater runat="server" ID="rptFuelConsPort">
                                 <ItemTemplate>
                                   <tr>
                                    <td><%#Eval("EmissionSourceName")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed1")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed2")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed3")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                  <tr style="background-color:#9bd0f7">
                                      <td><b>Total ( MT ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC1_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC2_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC3_P"></asp:Label></b></td>                                      
                                 </tr>
                                <tr style="background-color:#9bd0f7">
                                      <td><b>CO2 Emission ( TONNE ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO21_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO22_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO23_P"></asp:Label></b></td>                                      
                                 </tr>
                                </table>
                        </td>
                              <td>
                          
                                  <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                                <col />
                                <col width="180px;" />
                                <col width="180px;" />
                                <col width="180px;" />                                
                                <tr style="background-color:#f8b666">
                                      <td><b>Emission Source</b></td>
                                      <td style="text-align:center;"><b>Heavy Fuel Oil (ISO 8217 Grades RME through RMK) HFO<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Light Fuel Oil (ISO 8217 Grades RMA through RMD) LFO<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Diesel/Gas Oil (ISO 8217 Grades DMX through DMC) DO<br/>( MT )</b></td>                                      
                                 </tr>
                                <asp:Repeater runat="server" ID="rptFuelConsVoyage">
                                 <ItemTemplate>
                                 <tr>
                                    <td><%#Eval("EmissionSourceName")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed1")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed2")%></td>
                                    <td style="text-align:right;"><%#Eval("FuelConsumed3")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                <tr style="background-color:#f8b666">
                                      <td><b>Total ( MT ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC1"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC2"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblFC3"></asp:Label></b></td>                                      
                                 </tr>
                                <tr style="background-color:#f8b666">
                                      <td><b>CO2 Emission ( TONNE ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO21"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO22"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCO23"></asp:Label></b></td>                                      
                                 </tr>
                                </table>
                      </td> 
                            
                  </tr>
              </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnl_TS" Visible="false">
                 <table cellpadding="0" cellspacing="1" border="" class="bordered" width="100%">
                  <tr>
                      <td style="width:50%">
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                                <col />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />    
                                <col width="120px;" />    
                                <tr style="background-color:#9bd0f7">
                                     <td><b>Fuel Type</b></td>
                                      <td style="text-align:center;"><b>ROB at SBE Departure Port<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Bunker Qty <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>ROB at FWE Arrival Port <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Consumed Qty <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>CO2 Emission <br/>( Tonne )</b></td>
                                  </tr>
                                 <asp:Repeater runat="server" ID="rptFuelConsTank_Sea">
                                 <ItemTemplate>
                                   <tr>
                                    <td><%#Eval("FuelTypeName")%></td>
                                    <td style="text-align:right;"><%#Eval("Opening")%></td>
                                    <td style="text-align:right;"><%#Eval("Bunker")%></td>
                                    <td style="text-align:right;"><%#Eval("Closing")%></td>
                                    <td style="text-align:right;"><%#Eval("Consumed")%></td>
                                    <td style="text-align:right;"><%#Eval("Co2Emission")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                  <tr style="background-color:#9bd0f7">
                                      <td><b>Total ( MT ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblOP"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCl"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblClo"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCons"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblConsco2"></asp:Label></b></td>                                      
                                 </tr>                             
                                </table>
                      </td>
                      <td valign="top">
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                                <col />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                              
                               <tr style="background-color:#f8b666">
                                     <td><b>Fuel Type</b></td>
                                      <td style="text-align:center;"><b>ROB at SBE Departure Port<br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Bunker Qty <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>ROB at FWE Arrival Port <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>Consumed Qty <br/>( MT )</b></td>
                                      <td style="text-align:center;"><b>CO2 Emission <br/>( Tonne )</b></td>
                                  </tr>
                                 <asp:Repeater runat="server" ID="Repeater2">
                                 <ItemTemplate>
                                   <tr>
                                    <td><%#Eval("FuelTypeName")%></td>
                                    <td style="text-align:right;"><%#Eval("Opening")%></td>
                                    <td style="text-align:right;"><%#Eval("Bunker")%></td>
                                    <td style="text-align:right;"><%#Eval("Closing")%></td>
                                    <td style="text-align:right;"><%#Eval("Consumed")%></td>
                                    <td style="text-align:right;"><%#Eval("Co2Emission")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                <tr style="background-color:#f8b666">
                                      <td><b>Total ( MT ) :</b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblOP_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCl_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblClo_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblCons_P"></asp:Label></b></td>
                                      <td style="text-align:right;"><b><asp:Label runat="server" ID="lblConsco2_P"></asp:Label></b></td>                                      
                                 </tr>     
                                </table>
                      </td>
                  </tr>
              </table>
            </asp:Panel>
        </div>
         <div style="margin:5px;">                
                <asp:Button runat="server" id="btnClose" CssClass="btn" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
         </div> 
<h1 style="background-color:#9bd0f7">Supporting Documents </h1>
           <div>
               <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered">
                            <tr>
                                <th style="width:50px; text-align:center;">SR#</th>

                                <th style="width:70px; text-align:center;">Download</th>
                                <th style="text-align:left">Description</th>
                                <th style="width:300px; text-align:left;">Filename</th>
                                <th class="fixedWidth">&nbsp;</th>
                            </tr>
                            <asp:Repeater ID="rptFiles" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:center"><%#Eval("Sr") %>.</td>
                                        
                                        <td style="text-align:center">
                                            <asp:ImageButton runat="server" ImageUrl="~/Modules/HRD/Images/paperclip12.gif"  CommandArgument='<%#Eval("FileId")%>' OnClick="imgbtn_Clip_Click" />
                                        </td>
                                        <td><%#Eval("Description") %></td>
                                        <td><%#Eval("FileName") %></td>
                                        <td class="fixedWidth">&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
           </div> 
           <br />            
          <%--  <div>
                <ul class="menu">
                    <li><a href="#"> Voyage Activity </a></li>                    
                </ul>
                <div style="border-bottom:solid 5px #4371a5;"></div>
            </div>  --%>
            <%--<div>
                <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight padded" width="100%">
                                 <tr>
                                        <th style="width:50px;">View</th>
                                        <th style="text-align:left">&nbsp;Activity Name</th>
                                        
                                        <th style="text-align:left;width:130px;text-align:center;">Start Time</th>
                                        <th style="text-align:left;width:130px;text-align:center;">End Time</th>
                                        <th style="text-align:left;width:110px;">Steaming Time </th>
                                        <th style="text-align:center;width:140px;">Distance Made Good</th>
                                        <th style="text-align:center;width:140px;">Cargo Weight</th>
                                        <th style="text-align:center;width:140px;">Transport Work</th>
                                        <th style="text-align:center;width:140px;">CO<sub>2</sub> Emission</th>
                                 </tr>                 
                            <asp:Repeater runat="server" ID="rptActivity">
                   
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:center">
                                            <a target="_blank" href="NewActivity.aspx?TableId=<%#Eval("TableID") +"&VoyageId="+ Eval("VoyageId")+"&VesselCode="+ Eval("VesselCode") %>"> <img src="../Images/search_magnifier_12.png" alt="" style='margin:0px auto;<%#((Eval("Verified").ToString()=="-")?"display:none":"display:block")%>'/></a>
                                            
                                        </td>
                                        <td style="text-align:left">
                                            &nbsp;<span class='<%#((Eval("Verified").ToString()=="-")?"pending":"")%>' > <%#Eval("ActivityName")%> </span> 
                                            <img src="~/Images/warning.gif" runat="server" style="height:16px;float:right;" Visible='<%#(Eval("Altered").ToString()=="Y")%>' />                                         
                                        </td>                                        
                                        <td style="text-align:center"><%#FormatDate(Eval("StartTime"))%></td>
                                        <td style="text-align:center"><%#FormatDate(Eval("EndTime"))%></td>
                                        <td style="text-align:right">
                                            <%#Eval("SteamingHrs").ToString().PadLeft(2,'0')%>:<%#Eval("SteamingMin").ToString().PadLeft(2,'0')%> ( HRS )                                        
                                        </td>
                                        <td style="text-align:right"><%#Eval("DistanceMadeGoods")%> NM</td>
                                        <td style="text-align:right"><%#Eval("CurrentCargoWeight")%> MT</td>
                                        <td style="text-align:right"><%#Eval("TransportWork")%> NM.MT</td>
                                        <td style="text-align:right"><%#Eval("CO2")%> Tonne</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>            
            </div>--%>
        </div>   
     
    <div class="message">
        <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                
    </div>
    </form>
    
</body>
</html>
                                        

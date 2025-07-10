<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddGLEntry.aspx.cs" Inherits="Modules_OPEX_AddGLEntry" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <title> EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
     <link href="../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Purchase/JS/jquery.min.js"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
     <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
   <script type="text/javascript">
       function OpenDocument(DocId, GLId, VesselCode) {
           //  alert('Hi');
           window.open("../Purchase/Requisition/ShowDocuments.aspx?DocId=" + DocId + "&GLId=" + GLId + "&VesselCode=" + VesselCode +  "&PRType=GLEntry");
       }

       function fncInputNumericValuesOnly(evnt) {
           if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
               event.returnValue = false;
           }
       }
   </script>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: center">
    <div id="log" style="display:none"></div>
    <div>
    <center>
        <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td style="  padding:10px;" class="text headerband">
             <strong> GL Entry</strong>
         </td>
         </tr>
         </table>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style=" vertical-align:top; padding-top:3px;padding-left:5px;padding-right:5px;">
    <div style="border:solid 1px #008AE6;font-family:Arial, Helvetica, sans-serif;font-size:11px; " >
        <div>
  <%--  <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
         <table cellpadding="6" cellspacing="0" width="100%">
           <tr>
               <td style=" background-color:#FFFFCC">
                 <asp:Label ID="lblMessage" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
           <td>
           <table border="1" cellpadding="0" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
           <tr>
           <td>
            
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                     
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                              Vessel :</td>
                          <td style="text-align: left;padding-left:10px;" >
                            <asp:DropDownList runat="server"  ID="ddlVessel" CssClass="input_box" Width="90%"></asp:DropDownList>
                          </td>
                           <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                             Year :</td>
                          <td style="text-align: left; padding-left:10px; width: 175px; ">
                             <asp:DropDownList ID="ddlYear" runat="server" Width="100px" >
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px; width: 110px; ">
                             Posted :
                          </td>
                          <td style="text-align: left;width: 100px;" >
                             <asp:DropDownList ID="ddlPosted" runat="server" Width="100px"> 
                                   <asp:ListItem Text="< ALL >" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:10px;  ">
                              <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" /> &nbsp;
                              <asp:Button ID="btnAddGLENtry" runat="server" Text="Add GL Entry" CssClass="btn" OnClick="btnAddGLENtry_Click"  />
                          </td>
                          

                      </tr>
                    
                      <tr style="background-color:#E6F3FC">
                             <td colspan="7">
                                 <%--<div style="text-align:left;padding-left:20px;height:25px;vertical-align:central;">
                                     <b> Item wise Purchase Order Details : </b>
                                 </div>--%>
                                 <div class="table-responsive">
                                   <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                 
                                               <col style="width:3%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:6%;text-align:center;" />
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:14%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td>View</td>
                                                    <td>GL Ref #</td>
                                                    <td>GL Entry Date</td>
                                                    <td>Month & Year</td>
                                                    <td style="text-align:center;">Amount</td>
                                                    <td style="text-align:center;">Currency</td>
                                                    <td>Amount ($)</td>
                                                     <td style="text-align:center;">Account Name</td>
                                                    <td style="text-align:center;">Remark</td>
                                                    <td style="text-align:center;">Posted</td>
                                                    <td style="text-align:center;">Posted Date</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                               <div id="divPayment" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 400px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:3%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;"/>
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:6%;text-align:center;" />
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:14%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:9%;text-align:center;" />
                                                <col style="width:2%;"/>
                                                    </colgroup>
                                            <asp:Repeater ID="RptGlEntry" runat="server">
                                                <ItemTemplate>
                                                    <tr style="background-color:<%#(Eval("ReverseEntry").ToString()=="1"?"#ffff00":"")%> ">
                                                          <td align="center">  
                                                            <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("GLId")%>'  OnClick="imgbtnView_Click" CausesValidation="false" /></td>
                                                        <td align="Left">
                                                            <%#Eval("GLRefNo")%>
                                                        </td>
                                                        <td align="center">
                                                            <%#Common.ToDateString(Eval("GLEntryDate"))%>
                                                        </td>
                                                        <td align="left"><%#Eval("GLMonthYear")%></td>
                                                        <td align="right" style="padding-right:10px;"><%#Common.CastAsDecimal(Eval("Amount"))%>
                                                        </td>
                                                         <td align="center"><%#Eval("Currency")%></td>
                                                        <td align="right" style="padding-right:10px;"><%#Common.CastAsDecimal(Eval("AmountUSD"))%></td>
                                                        <td align="left"><%#Eval("AccountName")%></td>
                                                        <td align="left"><%#Eval("Remark")%></td>
                                                        <td align="center"><%#Eval("IsPosted")%></td>
                                                        <td align="center"><%#Common.ToDateString(Eval("TransDate"))%></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color:<%#(Eval("ReverseEntry").ToString()=="1"?"#ffff00":"")%> ">
                                                         <td align="center">  
                                                            <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" CommandArgument='<%#Eval("GLId")%>'  OnClick="imgbtnView_Click" CausesValidation="false" /></td>
                                                        <td align="Left">
                                                            <%#Eval("GLRefNo")%>
                                                        </td>
                                                        <td align="center">
                                                            <%#Common.ToDateString(Eval("GLEntryDate"))%>
                                                        </td>
                                                        <td align="left"><%#Eval("GLMonthYear")%></td>
                                                        <td align="right" style="padding-right:10px;"><%#Common.CastAsDecimal(Eval("Amount"))%>
                                                        </td>
                                                         <td align="center"><%#Eval("Currency")%></td>
                                                        <td align="right" style="padding-right:10px;"><%#Common.CastAsDecimal(Eval("AmountUSD"))%></td>
                                                        <td align="left"><%#Eval("AccountName")%></td>
                                                        <td align="left"><%#Eval("Remark")%></td>
                                                        <td align="center"><%#Eval("IsPosted")%></td>
                                                        <td align="center"><%#Common.ToDateString(Eval("TransDate"))%></td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                             </table>
                                        </div>
                                     </div>
                             </td>
                          </tr>
                     </table>
           </td>
           </tr>
           </table>
           </td>
         </tr>
       </table>
     <%--  </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btn_Save"  />
       </Triggers>
     </asp:UpdatePanel>--%>
            </div>
        <div style="position:absolute;top:50px;left:0px; height :100%; width:100%;" id="dv_GLDetails" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:2;top:25px; border:solid 10px black;">
            <center>
                <div class="text headerband">
                    <strong> GL Entry Details </strong>
                    <span style="float:right;padding-right:5px;color:red;">
                        <asp:LinkButton ID="lbAddUpdateDoc" runat="server" Visible="false" Text="Add Documents" ForeColor="Red"  OnClick="lbAddUpdateDoc_Click" ></asp:LinkButton> &nbsp;
                         (&nbsp;<asp:Label ID="lblAttchmentCount" runat="server" Text="0" ForeColor="Red" ></asp:Label>&nbsp;)
                     
                    </span>
                    
                </div>
                <table width="100%">
                    <tr style="background-color:#E6F3FC;vertical-align:middle;height:30px;">
                        <td align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                            Vessel : </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;" >
                            <asp:DropDownList ID="ddlVesselGL" runat="server" Width="70%" CssClass="input_box"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlVesselGL" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                            GL Ref # :
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                          <asp:Label ID="lblGLRefNo" runat="server"></asp:Label>
                        </td>
                      </tr>
                      <%--<tr style="padding : 3px 0px 3px 0px;vertical-align:middle;height:30px;">
                        <td align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Month : </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;" >
                             <asp:DropDownList ID="ddlMonthGL" runat="server" Width="90px" CssClass="input_box" ToolTip="Select GL Month">
                                        <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                              </asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonthGL" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Year : 
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                          <asp:DropDownList ID="ddlYearGL" runat="server" Width="100px" ToolTip="Select GL Year" >
                           </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYearGL" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                      </tr>--%>
                   <tr style="background-color:#E6F3FC;padding : 3px 0px 3px 0px;vertical-align:middle;height:30px;" > 
                          <td align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                             Amount : </td>
                          <td style="text-align: left;padding: 3px 0px 3px 10px;">
                             <asp:TextBox ID="txtAmount" runat="server"  onkeypress="fncInputNumericValuesOnly(event)" MaxLength="12" ToolTip="Enter Amount"> </asp:TextBox> 
                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtAmount" ErrorMessage="*" ></asp:RequiredFieldValidator>
                          </td>
                          <td align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                             Currency : </td>
                          <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;" >
                             <asp:DropDownList ID="ddlCurrency"  runat="server" CssClass="input_box" Width="100px"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCurrency" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                          </td>
                     </tr>
                    <tr style="padding : 3px 0px 3px 0px;vertical-align:middle;height:30px;">
                        <td align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                             Amount (In USD) : </td>
                          <td style="text-align: left;padding: 3px 0px 3px 10px;">
                             <asp:Label ID="lblAmtUSD" runat="server"  > </asp:Label> 
                          </td>
                         <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                            Account  : 
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;" >
                           <asp:DropDownList Id="ddlAccount" runat="server"  Width="200px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAccount" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                          
                      </tr>
                     <tr style="background-color:#E6F3FC;height:30px;vertical-align:middle;padding : 3px 0px 3px 0px;">
                          
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Transaction Date :  
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                              <asp:TextBox ID="txtTransactionDate" runat="server" onfocus="showCalendar('',this,this,'','holder1',-205,-150,1)" CssClass="required_box" Width="126px" ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtTransactionDate" ErrorMessage="*" ></asp:RequiredFieldValidator>
                           <%--  <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txtTransactionDate"></ajaxToolkit:CalendarExtender>--%>
                        </td>
                         <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                           
                        </td>
                          
                      </tr>
                     <tr style="padding : 5px 0px 5px 0px;vertical-align:middle;height:30px;">
                          
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           GL Entry Date :  
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                             <asp:Label ID="lblGLEntryDt" runat="server" > </asp:Label>
                        </td>
                         <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Updated By/On : 
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                            <asp:Label ID="lblAddedBy" runat="server" > </asp:Label> /  <asp:Label ID="lblAddedOn" runat="server" > </asp:Label>
                        </td>
                          
                      </tr>
                    <tr style="background-color:#E6F3FC;padding : 3px 0px 3px 0px;vertical-align:middle;height:50px;">
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Remark :  
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;" colspan="3">
                           <asp:TextBox ID="txtRemark" runat="server"  MaxLength="250" TextMode="MultiLine" Height="40px" Width="70%"> </asp:TextBox>
                        </td>
                      </tr>
                   
                     <tr style="padding : 3px 0px 3px 0px;vertical-align:middle;height:30px;" runat="server" id="trPosted" visible="false">
                        <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                           Posted :  
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                             <asp:Label ID="lblPostedStatus" runat="server" > </asp:Label>
                        </td>
                         <td  align="right" style="text-align: right; padding: 3px 10px 3px 0px; width: 100px; ">
                          Posted By/On : 
                        </td>
                        <td style="text-align: left;padding: 3px 0px 3px 10px;width:200px;">
                            <asp:Label ID="lblPostedBy" runat="server" > </asp:Label> /  <asp:Label ID="lblPostedOn" runat="server" > </asp:Label>
                        </td>
                          
                      </tr>
                </table>
            
                       
                 <div class="searchsection-btn">
                      <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px"  style="  border:none; padding:4px;" CssClass="btn"  OnClick="btnSave_Click" /> &nbsp;
                     <asp:Button ID="btnPosted" runat="server" Text="Post" Width="80px"  style="  border:none; padding:4px;" CssClass="btn"  CausesValidation="false"  Visible="false" OnClick="btnPosted_Click" /> &nbsp;
                      <asp:Button ID="btnReverseGLEntry" runat="server"  Text="Reverse Entry" ToolTip="Reverse GL Entry" CssClass="btn" Visible="false" OnClick="btnReverseGLEntry_Click"/> &nbsp;
                      <asp:Button ID="btnClose1" runat="server" Text="Close" Width="80px"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnClose1_Click" CausesValidation="false" /> &nbsp;
                 </div>
              

               
               
           
            </center>
        </div>
    </center>
    </div>
     <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
            <center>
                <div class="text headerband">
                    <strong> GL Entry Documents </strong>
                     <div style=" float :right " >
                          <asp:ImageButton ID="ibClose" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="ibClose_Click" CausesValidation="false" />
                               </div>
                </div>
                <div style="padding:3px 3px 3px 3px;">
                    <table width="100%">
                        <tr>
                            <td style="width:120px;text-align:right;padding-right:5px;">
                                Add Document :
                            </td>
                            <td style="width:250px;text-align:left;padding-left:5px;">
                                <asp:FileUpload ID="FU" runat="server" CssClass="input_box" />
                            </td>
                            <td style="width:120px;text-align:left;padding-left:5px;">
                                 <asp:Button ID="btnAddDoc" runat="server" CssClass="btn" Text="Upload" OnClick="btnAddDoc_Click" />
                            </td>
                        </tr>
                    </table>
                     
                </div>
                 <br />
        <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                       <col width="50px" />
                                       <col />
                                       <col width="90px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                           <td ></td>
                                           <td >File Name</td>
                                           <td >Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  <td style="text-align:center;">
                                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>'  />
                                                  </td>
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                                   </td>
                                                   <td style="text-align:center;"> 
                                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                                        <a onclick='OpenDocument(<%#Eval("DocId")%>,<%#Eval("GLId")%>,"<%#Eval("VesselCode")%>")' style="cursor:pointer;">
                                                       <img src="../HRD/Images/paperclip12.gif" />
                                                       </a>
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div>
        <asp:Button ID="btnPopupAttachment" runat="server" CssClass="btn" onclick="btnPopupAttachment_Click" Text="Close" CausesValidation="false" Width="100px" />
                </center>
            </div>
        </center>
             </div>
    </div>

    </td>
    
    </tr>
    </table>
    </center>
    </div>
            
    </div>
</asp:Content>



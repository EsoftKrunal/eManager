<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MocRequestNew.aspx.cs" Inherits="HSSQE_MOC_MocRequestNew" MasterPageFile="~/MasterPage.master" %>
<%--<%@ Register src="~/HSSQE/HSSQEMenu.ascx" tagname="HSSQEMenu" tagprefix="uc1" %>
<%@ Register src="MocMenu.ascx" tagname="MocMenu" tagprefix="uc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

      <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />

    <script src="../js/Common.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="Stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto" rel="stylesheet">
    <script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>

    <title></title>
    
    <style type="text/css">
        /**
        {
            font-family: 'Roboto', sans-serif;
            font-size:12px;            
        }*/
        .disableMe {
           /*background-color:#5F9EA0;
           background:url("../../Images/progress.gif") no-repeat center 50%;
           background-size:25px;
           background-repeat:no-repeat;
           content:"";*/
           opacity: 0.65; 
           cursor: not-allowed;
           /*color:black !important;*/
        }
        ul,li
        {
            margin:0px;
            padding:0px;
        }
        /*.btn
        {
            padding:0px;
            height:30px;
            background-color:#333;
            color:White;
            border:solid 0px white;
            padding-left:10px;
            padding-right:10px;
        }*/
        .counterlist li
        {
            color: white;
            border: solid 0px red;
            display:inline;
            margin:0px;
            padding:0px;
        }
        .counterlist li a
        {
            display: inline-block;
            text-decoration:none;                
            color:White;  
            margin:0px;                                        
            padding:5px;
            
        }
        .counterlist .cnt
        {
            font-size:20px;
            display:block;
            text-align:center;
        }
        .counterlist .name
        {
            font-size:14px;                
            color:#fff;
            font-style:italic;
        }
        .color1 {background-color:#5F9EA0;}        
        .color2 {background-color:#FF7F50;}
        .color3 {background-color:#6495ED;}
        .color4 {background-color:#8A2BE2;}
        .color5 {background-color:#f32285;}
        .color6 {background-color:#B8860B;}
        .color7 {background-color:#006400;}
        .color8 {background-color:#FF8C00;}
        .color9 {background-color:#8FBC8F;}
        .color10 {background-color:#2F4F4F;}
        .color11 {background-color:#1E90FF;}
        .color12 {background-color:#CD5C5C;}
        .color13 {background-color:#FF4500;}
        .color14 {background-color:#8B4513;}
        
        .bordered
        {
            border-collapse:collapse;
            width:100%;
        }
        td
        {
            vertical-align:middle;
        }
       /* .bordered tr td
        {
            padding:5px;
            border:solid 1px #e2e2e2;
        }
        .bordered tr:hover td
        {
            padding:5px;
            background-color:#fffedf;
            
        }
        .bordered thead tr th
        {
            padding: 5px;
            background-color: #64a1a2;
            border:solid 1px #4e9192;
            color:White;
            font-weight:normal;
        }*/
        .control
        {
            line-height:21px;
            height:21px; 
            padding-left:5px;           
        }
    </style>
</asp:Content>

    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
     <div class="text headerband">
         MOC 
     </div>
    
    <div>
    <%--<uc1:HSSQEMenu ID="HSSQEMenu1" runat="server" />
    <uc1:MocMenu ID="MocMenu" runat="server" />--%>
    <div style="margin:3px 0px 3px 0px" >
        <ul class="counterlist">
            <asp:Repeater ID="rptGroupedItems" runat="server">
                <ItemTemplate>
                        <li >
                            <a href="MocList.aspx?StageId=<%#Eval("STAGEID") %>&StageName=<%#Eval("STAGENAME") %>" target="_blank" class='<%#Eval("cssname") %>'>
                                <span class="name"> <%#Eval("STAGENAME") %> </span>
                                <span class="cnt"> <%#Eval("NOR") %> </span>
                            </a>
                        </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <table width='100%' cellpadding="0" cellspacing="0" >
    <tr>
        <td>
             <div style="padding:5px;">  
                     <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                         <colgroup>
                         <col width="260px" />
                         <col width="300px" />
                         <col  />
                             </colgroup>
                     <tr>
                         <td style="text-align:left;">
                             <asp:DropDownList ID="ddlStage" CssClass="control" runat="server" AutoPostBack="true" OnTextChanged="ddlStage_OnTextChanged"></asp:DropDownList>
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlFYear" CssClass="control" AutoPostBack="true" onSelectedIndexChanged="ddlFYear_onSelectIndexChanged" runat="server" Width="80px">                     
                            </asp:DropDownList>
                         </td>
                         <%--<td>
                             <asp:DropDownList ID="ddlOffice" runat="server"></asp:DropDownList>
                             <asp:DropDownList ID="ddlVessel" runat="server"></asp:DropDownList>
                         </td>--%>
                         <td style="text-align:right;">                        
                            <asp:Button ID="btnCreateNewMOC" runat="server" OnClick="btnCreateNewMOC_Click" Text=" + New MOC Request" CssClass="btn" />
                         </td>
                     </tr>
                     </table>
                </div>
           
        </td>
    </tr>
    
    <tr>
        <td style="vertical-align:top">
            <div style="height:25px;overflow-x:hidden;overflow-y:Scroll;">
                <table class="bordered">
                         <colgroup>
                            <col style="text-align: left" width="25px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="200px" /> 
                            <col style="text-align: left" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="100px" />
			    <col style="text-align: left" width="150px" />
                            <col style="text-align: left" width="150px" />                            
                            <col width="25px" />
                        </colgroup>
                        <thead>
                        <tr class= "headerstylegrid">
                            <th>&nbsp;</th>
                            <th style="text-align: left">&nbsp;Source</th>
                            <th style="text-align: left">&nbsp;Location</th>                            
                            <th>&nbsp;MOC#</th>
                            <th style="text-align: left">&nbsp;Topic</th>
                            <th>&nbsp;Request Date</th>
                            <th>&nbsp;Target Date</th>
			    <th style="text-align: left">&nbsp;User </th>  
			    <th style="text-align: left">&nbsp;Stage </th>                         
                            <th>&nbsp;</th>
                        </tr>
                        </thead>
                    </table>
            </div>
<div style="height:330px;overflow-x:hidden;overflow-y:Scroll;">
                    <table cellspacing="0" border="0" cellpadding="0"  class="bordered">
                        <colgroup>
                            <col style="text-align: left" width="25px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="200px" /> 
                            <col style="text-align: left" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="150px" />                                                     
			    <col style="text-align: left" width="150px" />
                            <col width="25px" />
                        </colgroup>
                        <asp:Repeater ID="rptMOC" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="btnView" OnClick="btnView_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View" CommandArgument='<%#Eval("MocID")%>' runat="server" />   
                                    </td>
                                    <td style="text-align: left">&nbsp;<%#Eval("Source")%></td>
                                    <td style="text-align: left">&nbsp;<%#Eval("Location")%></td>
                                    <td>&nbsp;<%#Eval("MOCNumber")%></td>
                                    <td style="text-align: left">&nbsp;<%#Eval("Topic")%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("RequestDate"))%></td>
                                    <td>&nbsp;<%# Common.ToDateString(Eval("TargetDate"))%></td>
                                    <td style="text-align: left">&nbsp;<%#Eval("WaitingUser")%></td>
				    <td style="text-align: left">&nbsp;<%#Eval("STAGENAME")%></td>
				   
                                    <td>&nbsp;</td>                                                                
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
</div>
       
    </td>
    </tr>
    </table>
    </div>
        <div ID="dv_AddNewMOC" runat="server" style="position: absolute; top: 50px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:50px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:55px; border:solid 4px #2a839e; border-top:none;">
          <center >
                
                <div>
                  <div style="padding:10px;font-size:17px; " class="text headerband"><b>Request Details</b></div>
                  <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
                    <tr>
                    <td style="text-align:left; width:48%;">Source :</td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left; width:48%;">VSL/ Office : 
                    </td>
                    <td style="text-align:left;">&nbsp;</td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlSource" AutoPostBack="true" CssClass="control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" runat="server" Width="100px" >
                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Vessel" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="ddlSource" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;</td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlVessel_Office" runat="server" Width="60%" CssClass="control">
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVessel_Office" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;</td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">Impact : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">Topic : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    </tr>
                    <tr>
                    <td>
                        <asp:CheckBoxList ID="cbImpact" RepeatDirection="Horizontal" runat="server" >
                            <asp:ListItem Text="People" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Process" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Equipment" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Safety" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Environment" Value="5"></asp:ListItem>
                       </asp:CheckBoxList>
                    </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" ID="txtTopic"  TextMode="MultiLine" Width="98%" Rows="2" ></asp:TextBox>
                        </td>
                    <td style="text-align:left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTopic" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">Reason for change : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">Brief Description of change : 
                         </td>
                    <td style="text-align:left;">&nbsp;</td>
                     </tr>
                    <tr>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" ID="txtReasonforChange"  TextMode="MultiLine" Width="98%" Rows="7"></asp:TextBox>
                     </td>
                    <td style="text-align:left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReasonforChange" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                        </td>
                    <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtDescr" TextMode="MultiLine"  Width="98%" Rows="7"></asp:TextBox>
                     </td>
                    <td style="text-align:left;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDescr" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                      </table>
                 <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#fff"><b>Forwared for Approval</b></div>
                       <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
                      <tr>
                         <td style="text-align:left; width:48%;">
                             Proposed TimeLine for completion of change : 
                        </td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                          <td style="text-align:left; width:48%;">
                             Comments ( if any )</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; width:48%;">
                            <asp:TextBox runat="server" ID="txtPropTL" CssClass="control" MaxLength="15" Width="90px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPropTL" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                          </td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                          <td style="text-align:left; width:48%;" rowspan="3">
                             <asp:TextBox runat="server" ID="txtForwardedComments" TextMode="MultiLine"  Width="99%" Rows="7"></asp:TextBox></td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; width:48%;">
                             Forwarded To</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; vertical-align:top;">
                            <asp:DropDownList ID="ddlForwardedTo" runat="server" CssClass="control" ></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlForwardedTo" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                             </td>
                          <td></td>
                          <td></td>
                      </tr>
                      
                      </table>
                </div>
          </center>
          <div style="padding:3px; text-align:right; ">
              <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtPropTL" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
              <asp:Label ID="lblMsg" runat="server" ForeColor="Red" ></asp:Label>
              <asp:Label ID="lblMsgPopup" runat="server" style="color:red;"></asp:Label>
              <asp:Button runat="server" ID="btnSaveNew" Text="Save" ValidationGroup="V1" OnClick="btnSaveNew_Click" CssClass="btn"  OnClientClick="DisableButton()" CausesValidation="false"/>
              <asp:Button runat="server" ID="btnNext" Text="Next >>" CausesValidation="false" OnClick="btnNext_Click" Visible="false" CssClass="btn"/>
              <asp:Button runat="server" ID="btnCloseNew" Text="Close" OnClick="btnCloseNew_Click" CausesValidation="false" CssClass="btn" />
              
          </div>
          </div>
        </center>
        </div>
        <script type="text/javascript">
            function DisableButton()
            {
                //$("#btnSaveNew").val(" ");
                $("#btnSaveNew").addClass("disableMe");
                $("#btnSaveNew").prop('disabled', 'disabled');
                //document.getElementById("btnSaveNew").disabled = true;
                //alert(document.getElementById("btnSaveNew").disabled);
                return null;
            }
            //window.onbeforeunload = DisableButton;            
        </script>
    </div>
    </asp:Content>

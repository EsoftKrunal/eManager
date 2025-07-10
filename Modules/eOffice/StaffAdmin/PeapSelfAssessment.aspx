<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeapSelfAssessment.aspx.cs" Inherits="Emtm_PeapSelfAssessment" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Page</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">

     <script language="javascript" type="text/javascript">
         function focusthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "70px";
         }
        function blurthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "30px";
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server" > 
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <table width="100%">
                <tr>
               
                <td valign="top" style="border:solid 1px #4371a5; height:500px;background-color:#f9f9f9;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" CssClass="text">
                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                    <col width="310px" />
                                    <col />
                                    <col width="200px" />
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                         <td style="font-size:16px; font-weight:bold; color:White; text-align:center;"> 
                                            Performance Evaluation & Assessment Of Potential (PEAP)</td>
                                        <td style="font-size:10px; color:White;vertical-align:top;"> 
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                             <td style="padding:10px">
                                <span style="font-size:Large; font-weight:bold; color:#336699;">
                                [ 
                                    <asp:Label ID="txtFirstName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                    <asp:Label ID="txtLastName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label> &nbsp;/ <asp:Label ID="lblPeapLevel" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                &nbsp;]
                                </span>
                                    <span style="font-size:Large; font-weight:bold; color:#6600CC;">
                                ( 
                                    <asp:Label ID="txtOccasion" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                &nbsp;)
                                </span>
                                <%--<span style="font-size:Large; font-weight:bold; color:#000;">
                                     [
                                      <asp:Label ID="lblPeapPeriod" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ] 
                                </span>--%>

                                <span style="padding-left:50px;" > 
                                     <asp:Label ID="lblUpdatedOn" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                </span>
                                 
                                   <span style="float:right"> 
                                    <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Modules/HRD/Images/back_button.gif" AlternateText="Back" OnClick="btnBack_Click" />
                                </span>
                                <br /><br />
                                <table style="width:100%; font-size:14px; border-collapse:collapse;" cellpadding="5" cellspacing="0" border="1" >
                                <tr>
                                    <td id="tdQuestions" runat="server" style="background-color: #f2f2f2;width: 300px;">
                                        <div style="padding: 5px; background-color: #fffff; width: 300px; float: left; height: 370px; overflow-x: hidden; overflow-y:scroll;">
                                            <table style="width: 100%; font-size: 14px;" cellpadding="0" cellspacing="5">
                                                <colgroup>
                                                    <col width="20px" />
                                                    <col />
                                                </colgroup>
                                                <asp:Repeater runat="server" ID="rptSAQuestions">
                                                    <ItemTemplate>
                                                     <tr>
                                                        <td>
                                                            <img src ='<%#Eval("Answer").ToString() == "" ? "../../Images/icon_pencil.gif" : "../../Images/favicon.png" %>' />
                                                        </td>
                                                        <td>
                                                            <div style="height:16px; overflow:hidden;">
                                                                &nbsp;<span><%#Eval("Srno").ToString() + ". "%></span>
                                                                <asp:LinkButton ID="lbQuestion" Text='<%#Eval("Qtext") %>' CommandArgument='<%#Eval("QId") %>' OnClick="lbQuestion_Click" runat="server" style="font-size:13px"></asp:LinkButton>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                    <td >
                                        <div style="height: 370px; width:100%; overflow-x:hidden;overflow-y:scroll;">
                                            <table style="width: 100%; font-size: 14px;" cellpadding="2" cellspacing="5">
                                                <colgroup>
                                                    <col width="20px" />
                                                    <col />
                                                </colgroup>
                                                    <asp:Repeater runat="server" ID="rptQuesAnswer">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="vertical-align:top">
                                                                    <span style="font-weight:bold;"><%#Eval("Srno").ToString() + ". "%></span>
                                                                </td>
                                                                <td style="font-weight:bold;">
                                                                    <asp:Label ID="lbQuestion" Text='<%#Eval("Qtext") %>' runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight:bold;">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbAnswer" Text='<%#Eval("Answer1") %>' runat="server"></asp:Label>
                                                                       <a runat="server" ID="ancFile"  href='<%# "~/EMANAGERBLOB/Peap/" + Eval("FileName").ToString() %>' target="_blank" visible='<%#Eval("FileName").ToString()!= "" %>'  title="Show Attachment" >
                                                                        <img src="../../Images/Emtm/paperclip.png" style="border:none"  />
                                                                       </a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                </table>   
                                <div style="text-align:left; padding:5px;">
                                    <asp:Label ID="lblMsg_Notify" style="color:Red;" runat="server"></asp:Label>&nbsp;
                                    <asp:Button ID="btnNotify" style="float:right" CssClass="btn" Width="100px" Text="Notify" runat="server" Visible="false" onclick="btnNotify_Click" />                             
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
    </div> 
    
    <div style="position:absolute;top:0px;left:0px; height :560px; width:100%;z-index:100;" runat="server" id="dvSubmitAnswer" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :540px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:800px;  text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" >
                <ContentTemplate>
                <table cellpadding="3" cellspacing="0" border="0" width="100%" style="border-collapse:collapse ">
                    <tr>
                        <td colspan="2" style="background-color:#d2d2d2; padding:4px;"><b>Submit Answer</b></td>
                    </tr>
                    <tr style="font-weight:bold">
                        <td style="text-align:right; width:100px;">
                          Question :&nbsp;
                        </td>
                        <td style="text-align:left; ">
                          <asp:Label ID="lblSA_Question" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td style="text-align:right; width:100px; vertical-align:top; font-weight:bold;">
                          Guidance :&nbsp;
                        </td>
                        <td style="text-align:left; ">
                            <div style="width:95%; ">
                          <asp:Label ID="lblGuidance" runat="server" Font-Italic="true"></asp:Label>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; vertical-align:top;">
                           Answer :&nbsp;
                        </td>
                        <td style="text-align:left;">
                           <asp:TextBox ID="txtSA_Answer" runat="server" TextMode="MultiLine" Height="190px" Width="95%" ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSA_Answer" ErrorMessage="Please enter 'NONE' if no details available." SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align:right;">
                          Upload File :&nbsp;
                         </td>
                       <td style="text-align:left;">
                          <asp:FileUpload ID="FileUpload1" runat="server"/>
                       </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:left;">
                            <asp:Button ID="btnSaveSA" Text="Save" OnClick="btnSaveSA_Click" runat="server" Width="100px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" onclick="btnCancel_Click" Text="Cancel" CausesValidation="false" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <asp:Label ID="lblMsg" runat="server" style="color:Red;"></asp:Label>                            
                        </td>
                    </tr>
                </table>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveSA" />
                <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                </asp:UpdatePanel>
                
            </div> 
            </center>
         </div>
     
    </form>
</body>
</html>

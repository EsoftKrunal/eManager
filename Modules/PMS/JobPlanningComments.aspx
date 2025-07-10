<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobPlanningComments.aspx.cs" Inherits="JobPlanningComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />   
    <script src="JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
    
    <style type="text/css">
    .Comment_Box
    {
        border: solid 1px #ffe0b2;    
        margin:5px;
        text-align:left;
        font-family:MS Sans Serif;
        -webkit-border-radius: 10;
        -moz-border-radius: 10;
        border-radius: 10px;
    }
        .Comment_header {
            background-color: #FFE0B2;
            padding: 9px;
            height: 15px;
            -webkit-border-top-left-radius: 10px;
            border-top-left-radius: 10px;
            -moz-border-radius-topleft: 10px;

            -webkit-border-top-right-radius: 10px;
            border-top-right-radius: 10px;
            -moz-border-radius-topright: 10px;
        }
    #dvClosure
    {
        margin:5px;
        border:solid 1px red;
        -webkit-border-radius: 10;
        -moz-border-radius: 10;
        border-radius: 10px;
        background-color:#95ff8f;
    }
    .Comment_content
    {
        padding:8px;
    }
    .commenton
    {
        color:Maroon; 
        float:left;
        margin:0px 10px 0px 10px;
    }
    .reply
    {
        float:right;
        color:black; 
        margin:0px 10px 0px 10px;
        cursor:pointer;
        text-decoration:underline;
    }
    .commentby
    {
        float:left; 
        margin-right:5px;
        color:Red; 
        margin:0px 10px 0px 10px;
    }
    .addmember
    {
        color:Green;
        margin-right:5px; 
        margin:0px 10px 0px 5px;
        text-decoration:underline;
        cursor:pointer;
    }
    .commenttext
    {
        font-size:13px;
        cursor:pointer;
        padding-left:5px;
    }
    .attachment
    {
        cursor:pointer;
    }
    .spncat
    {
        cursor:pointer;
        float:left;
    }
    .striped
    {
        border-collapse:collapse;
    }
    .striped tr td
    {
        border:solid 1px #e9e7e7;
    }
    /*.striped tr:nth-child(odd) td
    {

    }
    .striped tr:nth-child(even) td
    {
        background-color:#eaecea;
    }*/
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".attachment").click(function () {
                
                $("#hfdReplyCommentID").val($(this).attr('commentid'));                
                $("#btnClip").click();
            });

            $(".reply").click(function () {
                //$("#txtCommId").val($(this).attr('commentid'));
                $("#hfdReplyCommentID").val($(this).attr('commentid'));
                $("#btnReply").click();
            });

            $(".spncat").mouseover(function () {
                $(this).addClass("editable")
            });
            $(".spncat").mouseout(function () {
                $(this).removeClass("editable")

            });
            $(".spncat").click(function () {
                var myval = $(this).attr('catname');
                $("#txtThreadId").val($(this).attr('threadid'));
                $("[name=rad_fd]").each(function (i, o) {
                    if (i == 0) {
                        $(o).attr('checked', myval == "People");
                    }
                    else if (i == 1) {
                        $(o).attr('checked', myval == "Process");
                    }
                    else if (i == 2) {
                        $(o).attr('checked', myval == "Equipment");
                    }
                });
                $("#dvCategory").slideDown();
            });

        });
        function HideCatBox() {
            $("#dvCategory").slideUp();
        }
        function SelectInverseAll(me) {
            var myval=$(me).attr('checked');
            $("#dvall").find(":checkbox").attr('checked', myval); 
        }
    </script>
    <style type="text/css">
        *{
            font-size:12px;
        }
        body{
            margin:0px;
           
        }
    .bordered tr td
    {
        border:solid 1px #efeded;
        padding:5px;
    }
    </style>
    <style type="text/css">
    .office,.ship
    {
        margin-top:5px;
    }
    .content
    {
        border-radius:10px;
        
        overflow:auto;   
        padding:10px;
        position:relative;
        min-height:64px;        
    }
    .content .icon
    {
        width:64px;
        height:64px;
        display:block;
        border-radius:64px;   
        background-position:center;
    }
    .office .icon
    {
        position:absolute;
        top:10px;
        left:10px;
        background-color:#3be0ef;
        background-image:url('./Images/worker.png')
    }
    .content .received {

        width:15px;
        height:15px;
        display:block;        
        background-position:center;

        position:absolute;
        top:10px;
        right:10px;        
        background-image:url('./Images/checked-mark-green.png')
    }
    .message
    {
        color:#9e9e9e;
        font-size:12px;  
        margin-bottom:20px;  
        text-align:left;
    }
    .office .message
    {
        margin-left:80px;        
    }
    .ship .message
    {
        margin-right:80px;        
    }
    
    .office .content
    {
        margin:10px;
        margin-right:100px;
        text-align:left;
        border:solid 1px #3be0ef;     
        background-color:#edfeff;
    }
    .ship .content
    {
        margin:10px;
        margin-left:100px;     
        text-align:left;
        border:solid 1px #ffeb3b;     
        background-color:#fffde1;
    }
    
    .ship .icon
    {
        position:absolute;
        top:10px;
        right:10px;        
        background-color:#ffeb3b;
        background-image:url('./Images/cruise.png')
    }
    .byon
    {
        color:Red;
        position:absolute;
        bottom:4px; 
        font-size:12px;
        font-weight:bold;
        font-style:italic;
    }
    .office .byon
    {
        margin-left:80px;                  
    }
    .ship .byon
    {
        margin-right:80px;                          
    }
    .dataheading
    {
        padding:12px;
        color:white;
        background-color:#676767;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
        <div style="display:none;">
            <asp:Button ID="btnClip" runat="server" Text=" Download Attachment " OnClick="btnClip_OnClick"/>
            <asp:Button ID="btnReply" runat="server" Text=" Reply " OnClick="btnReply_OnClick"/>
            <asp:HiddenField runat="server" ID="hfdReplyCommentID"></asp:HiddenField>
        </div>
        <div style="position:fixed;top:0px;left:0px; height:100px;width:100%; z-index:2;background-color:white;">
            <div style="padding:8px; font-weight:bold;text-align:center;font-size:14px;" class="text headerband" >PMS-Discussion ( Vessel : <asp:Label ID="lblVesselCode" runat="server"/> )</div>
                    <table cellpadding="2" cellspacing="2" width="100%" border="0" style="text-align:left;">
                        <col width="170px;" />
                        <col width="25%" />
                        <col width="100px" />
                        <col width="25%" />
                        <col width="80px" />
                        <col/>
                        <tr>
                            <td style="text-align:left"><b>Comp.Code </b></td>
                            <td>:&nbsp;<asp:Label ID="lblCompCode" runat="server"></asp:Label></td>
                            <td style="text-align:left"><b>Comp. Name </b></td>
                            <td>:&nbsp;<asp:Label ID="lblCompName" runat="server"></asp:Label></td>
                              <td style="text-align:left"><b>Next Due </b></td>
                            <td>:&nbsp;<asp:Label ID="lblDueInDays" runat="server"></asp:Label> ( <asp:Label ID="lblNextHour" runat="server"></asp:Label> )</td>
                        </tr>
                        <tr>
                            <td style="text-align:left"><b>Last Done Date </b></td>
                            <td>:&nbsp;<asp:Label ID="lblLastDoneDate" runat="server"></asp:Label></td>
                             <td style="text-align:left"><b>Job Name </b></td>
                            <td>:&nbsp;<asp:Label ID="lblJobName" runat="server"></asp:Label></td>
                         
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align:left"><b>Job Description (Short) </b></td>
                            <td colspan="5">:&nbsp;<asp:Label ID="lblShortDesc" runat="server"></asp:Label></td>
                        </tr>
                    </table>
            <div style="text-align:center">
            <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                </div>
              </div>
        <div style="margin-top:120px; z-index:1;">
                <table width="100%" cellpadding="0" style="border:solid 1px #c6d9ef;border-collapse:collapse" border="1">
                     <tr style="color:white;font-weight:bold">
                        <td style="width:20%"><div class="dataheading">Job Description</div></td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Text=" Export " OnClick="btnExport_OnClick" style="border:none;padding:6px; width:120px; float:right;margin:5px;" CssClass="btn" />
                            <asp:Button ID="btnPost" runat="server" Text="+ New Comments " OnClick="btnPost_OnClick" style="border:none;padding:6px; width:120px; float:right;margin:5px;" CssClass="btn" />
                            <div class="dataheading">PMS-Discussion</div>                            
                        </td>
                        <td style="width:40%"><div class="dataheading">Spare Status</div></td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow-x:hidden;overflow-y:scroll; height:550px;padding:5px;">
                                <asp:Label ID="lblLongDesc" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td><div style="overflow-x:hidden;overflow-y:scroll; height:550px;padding:5px;">
                              <asp:Literal runat="server" ID="litComments"></asp:Literal>
                            </div>
                        </td>
                        <td>
                            <div style="overflow-x:hidden;overflow-y:scroll; height:550px;padding:5px;">
                             <table width="100%" cellpadding="5" cellspacing="0" class="striped" >

                                <asp:Repeater runat="server" ID="rptSpares" >
                                    <HeaderTemplate>
                                    <col />
                                    <col style="width:80px;" />
                                    <col style="width:100px;" />
                                    <col style="width:20px;" />
                                    <tr class= "headerstylegrid">
                                        <td>Spare Name</td>
                                        <td style="text-align:right">Req. Qty</td>
                                        <td style="text-align:right">ROB </td>
					<td style="text-align:right">&nbsp;</td>
                                    </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <tr style='<%#(Common.CastAsInt32(Eval("Qty"))>Common.CastAsInt32(Eval("ROB")))?"background-color:#fb9b9b":""%>'>
                                        <td><a title="View spare details." href="Ship_AddEditSpares.aspx?CompCode= <%#lblCompCode.Text%>&&VC=<%#Eval("VesselCode")%>&&SPID=<%#Eval("SpareId")%>&&OffShip=<%#Eval("Office_Ship")%>" target="_blank"><%#Eval("SpareName")%></a></td>
                                        <td style="text-align:right"><%#Eval("Qty")%></td>
                                        <td style="text-align:right"><%#Eval("ROB")%></td>
					<td style="text-align:right">&nbsp;</td>
                                    </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                                  </div>
                        </td>
                    </tr>
                </table>
                
              
         </div>         
              
    </div>

        <!-- Reply Comment -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;font-family:Arial;font-size:12px;" id="dvReply" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; padding:5px;" class="text headerband">
                <asp:Label ID="Label1" runat="server" style="font-weight:bold;color:Black;text-align:left; " font-size="15px" ></asp:Label>
            </div>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td>
                        <div style='display:<%=((IsReply)?"block":"none") %>'>
                            <div style="text-align:center; padding:5px;"><b>Previous User Comment</b></div>
                            <div runat="server" id="dvOrgComment" style="height:100px;overflow:scroll; overflow-x:hidden; border:solid 1px #c2c2c2;text-align:left;"> 
                    
                            </div>
                         </div>
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Please Enter Your Comment Below ...</b>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtReply" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td>
                    <asp:TextBox TextMode="MultiLine" Width="100%" Height="210px" runat="server" ID="txtReply" style=" background-color:#FFFFCC"></asp:TextBox>
                   </td>
                </tr>
                   <tr>
                    <td style="text-align:center">
                        <asp:FileUpload runat="server" ID="flpReply" Width="250px" /> (Only zip file is allowed. Max file size 100 KB.)
                   </td>
                </tr>
                   <tr>
                    <td>
                        <hr />
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnSave_Reply" runat="server" Text=" Save " OnClick="btnSave_Reply_OnClick" CssClass="btn" style="border:none;padding:6px; width:80px;" OnClientClick="DisableMe(this);"/>
                        <asp:Button ID="btnClose_Reply" runat="server" CssClass="btn" Text=" Close " CausesValidation="false" style="border:none;padding:6px; width:80px;" OnClick="btnClose_Reply_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
        </center>
        </div>
         <div style="background:#4371a5;padding:8px;color:white; font-weight:bold;text-align:center;font-size:14px;" >&nbsp;</div>
    </form>
</body>
</html>

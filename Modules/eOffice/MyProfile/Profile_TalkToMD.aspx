<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_TalkToMD.aspx.cs" Inherits="Emtm_Profile_TalkToMD" Async="true" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script src="../../HRD/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JQScript.js" type="text/javascript"></script>
    <link href="../JQstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .Comment_Box
    {
        border:dotted 1px orange;     
        margin:5px;
        padding:5px;
        text-align:left;
        font-family:MS Sans Serif;
        -webkit-border-radius: 10;
        -moz-border-radius: 10;
        border-radius: 10px;
    }
    .Comment_header
    {
        background-color:#FFE0B2;     
        padding:5px;
        height:15px;
        -webkit-border-radius: 10;
        -moz-border-radius: 10;
        border-radius: 10px;
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
        padding:5px;
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
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".attachment").click(function () {
                $("#txtCommId").val($(this).attr('commentid'));
                $("#btnClip").click();
            });

            $(".reply").click(function () {
                $("#txtCommId").val($(this).attr('commentid'));
                $("#btnReply").click();
            });

            $(".addmember").click(function () {
                $("#txtCommId").val($(this).attr('commentid'));
                $("#btnInvite").click();
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

<div style="display:none">
    <asp:Button ID="btnClip" runat="server" Text=" Download Attachment " OnClick="btnClip_OnClick"/>
    <asp:Button ID="btnReply" runat="server" Text=" Reply " OnClick="btnReply_OnClick"/>
    <asp:Button ID="btnInvite" runat="server" Text=" Invite " OnClick="btnInvite_OnClick"/>
    <asp:TextBox runat="server" ID="txtCommId"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtThreadId"></asp:TextBox>
</div>
    
    
    <div style="border:solid 5px #CCE6FF">
        <div style="background-color:#8AB8E6; text-align:center; padding:5px;">
    <span style="font-size:15px; color:White;"><b>Hello MD</b></span>
    </div>
        <table style="padding:0px;width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding:2px;">
                    <asp:TextBox ID="txtSearchField" runat="server" Width="300px" CssClass="input_box" ></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text=" Topic Search " OnClick="btnSearch_OnClick" CssClass="Btn1" />
                    <asp:Button ID="btnAdd" runat="server" Text=" Add New" OnClick="btnAdd_OnClick" style="width:70px;" CssClass="Btn1" />
                    <asp:Button ID="btnPrint" runat="server" Text=" Print " OnClick="btnPrint_OnClick" CssClass="Btn1"  />

                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <div style="overflow-x:hidden;overflow-y:scroll; width:100%;">
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="grey">
                    <thead>
                        <tr style="text-align:center; font-weight:bold;">
                            <td style="text-align:center; width:40px;">View</td>
                            <td style="text-align:center;width:60px;">Sr No.</td>
                            <td style="width:150px;">Origin</td>
                            <td style="text-align:left;">Topic</td>
                            <td style="width:100px; text-align:left;">Category</td>
                            <td style="width:100px">Start Date</td>
                            <td style="width:100px">Closure Date</td>
                            <td style="width:80px;text-align:center;">Status</td>
                            <td style="width:40px; text-align:center;">Print</td>
                            <td style="width:20px">&nbsp;</td>
                        </tr>
                    </thead>
                    </table>
                    </div>
                    <div style="height:200px;" class='ScrollAutoReset' id='d1'>
                    <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="grey">
                    <tbody>
                        <asp:Repeater ID="rptCRM" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="text-align:center; width:40px;"> 
                                        <asp:ImageButton ID="btnViewDiscussion" runat="server" ImageUrl="~/Modules/HRD/Images/discussion-bubble.png" OnClick="btnViewDiscussion_OnClick" CommandArgument='<%#Eval("ThreadId")%>' /> 
                                    </td>
                                    <td style="text-align:center; width:60px"> <%#Eval("RowNumber")%>.</td>
                                    <td style="width:150px; text-align:left;"> <%#Eval("Origin")%> </td>
                                    <td style=" text-align:left;"><%#Eval("Topic")%></td>
                                    <td style="width:100px; text-align:left;">
                                    <span runat="server" visible='<%# UserId.ToString()=="19"%>'>
                                        <div class="spncat" threadid='<%#Eval("ThreadId")%>' catname='<%#Eval("Category")%>' > <img src="../../Images/AddPencil.gif" /></div>
                                        <%#Eval("Category")%>
                                    </span>
                                    </td>
                                    <td style="width:100px"> <%#Common.ToDateString(Eval("StartDate"))%> </td>
                                    <td style="width:100px"> 
                                        <asp:ImageButton ID="imgbtnClosure" CommandArgument='<%#Eval("ThreadId")%>' runat="server" Visible='<%#(Eval("Status").ToString()=="Open") && (Eval("StartByUserId").ToString()==UserId.ToString())%>' ImageUrl="~/Modules/HRD/Images/closure_flag.png" OnClick="imgbtnClosure_OnClick"  />
                                        <%#Common.ToDateString(Eval("ClosedOn"))%> 
                                    </td>
                                    <td style="width:80px;text-align:center;">
                                        <%#Eval("Status")%> 
                                    </td>
                                    <td style="width:40px; text-align:center;"> 
                                        <asp:HiddenField ID="hfThreadID" runat="server" Value='<%#Eval("ThreadId")%>' />
                                        <asp:ImageButton ID="imgPrintDiscussion" runat="server" ImageUrl="~/Modules/HRD/Images/printer16x16.png" OnClick="imgPrintDiscussion_OnClick"  />
                                     </td>
                                     <td style="width:20px">&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    </table>
                    <asp:Label ID="lblNoRecords" runat="server" style="font-weight:bold;color:Red;"></asp:Label>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td style="background-color:#8AB8E6; padding:5px; text-align:center; ">
                    <span style="font-size:15px; color:White;"><b>Discussion</b></span>
                </td>
            </tr>
            <tr>
                <td style="background-color:#FFCC80; padding:5px; text-align:center; ">
                    <span style="font-size:15px;"><b><asp:Label runat="server" ID="lblTopic"></asp:Label></b></span>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                   <div id="content" class='ScrollAutoReset' style="height:400px">
                   <asp:Literal runat="server" ID="litComments"></asp:Literal>
                   <div id="dvClosure" runat="server" style="font-size:13px; color:#F8F8FA;padding:0px;" visible="false">
                            <div style='font-size:13px; color:Teal; font-style:italic; padding:5px; vertical-align:middle; text-align:left;'>
                                <div style="padding-left:0px;text-align:left;">
                                <b>Closed On :</b> 
                                <asp:Label runat="server" ID="lblClosedByOn" Font-Bold="true"></asp:Label> 
                                <b style="color:Red">[ Closure ]</b>
                                <hr />
                                <b>Closure Remarks : </b>
                                <asp:Label runat="server" ID="lblClosureRemarks" Width='100%'></asp:Label> 
                                </div>
                            </div>
                        </div>
                  </div>
                 </td>
            </tr>
        </table>
    </div>


    <!-- Add / Edit Thread  -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvThread" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; background-color:#c2c2c2; padding:5px;">
                <asp:Label ID="lblRRHeading" runat="server" style="font-weight:bold;color:Black;" font-size="15px" ></asp:Label>
            </div>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Topic Name</b>
                     <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtTopic" ErrorMessage="*"></asp:RequiredFieldValidator>
                     </td>
                </tr>
                <tr>
                    <td>
                    <asp:TextBox TextMode="MultiLine" Width="100%" Height="40px" runat="server" ID="txtTopic" style=" background-color:#FFFFCC"></asp:TextBox>
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Please Enter Comments Below ...</b>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtComment" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td>
                    <asp:TextBox TextMode="MultiLine" Width="100%" Height="210px" runat="server" ID="txtComment" style=" background-color:#FFFFCC"></asp:TextBox>
                   </td>
                </tr>
                   <tr>
                    <td style="text-align:center">
                        <asp:FileUpload runat="server" ID="flpUpload" Width="90%" />
                   </td>
                </tr>
                   <tr>
                    <td>
                        <hr />
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnCreate" runat="server" Text=" Save " OnClick="btnCreate_OnClick" CssClass="popupclosebtn" style="padding:5px 0px 5px 0px; border:none; background-color :Green;" OnClientClick="DisableMe(this);"/>
                        <asp:Button ID="btnClose" runat="server" CssClass="popupclosebtn" Text=" Close " CausesValidation="false" style="padding:5px 0px 5px 0px; border:none;" OnClick="btnClose_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
        </center>
        </div> 
    
    <!-- Reply Comment -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvReply" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; background-color:#c2c2c2; padding:5px;">
                <asp:Label ID="Label1" runat="server" style="font-weight:bold;color:Black;" font-size="15px" Text="Reply Comment" ></asp:Label>
            </div>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Previous User Comment</b></td>
                </tr>
                <tr>
                    <td>
                        <div runat="server" id="dvOrgComment" style="height:100px;overflow:scroll; overflow-x:hidden; border:solid 1px #c2c2c2;"> 
                    
                        </div>
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Please Enter Reply Comment Below ...</b>
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
                        <asp:FileUpload runat="server" ID="flpReply" Width="90%" />
                   </td>
                </tr>
                   <tr>
                    <td>
                        <hr />
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnSave_Reply" runat="server" Text=" Save " OnClick="btnSave_Reply_OnClick" CssClass="popupclosebtn" style="padding:5px 0px 5px 0px; border:none; background-color :Green;" OnClientClick="DisableMe(this);"/>
                        <asp:Button ID="btnClose_Reply" runat="server" CssClass="popupclosebtn" Text=" Close " CausesValidation="false" style="padding:5px 0px 5px 0px; border:none;" OnClick="btnClose_Reply_OnClick" />
                    </td>
                </tr>
            </table>
        </div>
        </center>
        </div>

    <!-- Invite Members -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvMembers" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; background-color:#c2c2c2; padding:5px;">
                <asp:Label ID="lbl" runat="server" style="font-weight:bold;color:Black;" font-size="15px" Text="Invite Member(s)" ></asp:Label>
            </div>
            <asp:UpdatePanel runat="server" ID="UP1">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center; padding:5px; background-color:#EEEEEE">
                        <asp:CheckBoxList runat="server" ID="chkOffice" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkOffice_OnSelectedIndexChanged" AutoPostBack="true" />
                        <span style="float:left; margin-left:-3px;">
                        <input type="checkbox" onclick='SelectInverseAll(this);' id="call" /><label for="call">All</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="height:400px; overflow-x:hidden;overflow-y:scroll;" id='dvall'>
                            <asp:Repeater runat="server" ID="rptMembers">
                            <ItemTemplate>
                            <div>
                                <asp:CheckBox runat="server" ID="chkMember" Text='<%#Eval("OFFICENAME")%>' CssClass='<%#Eval("LOGINID")%>'  />
                                <span>[ <span style='color:red'><%#Eval("USERNAME")%></span>-<span style='color:green'><%#Eval("POSITIONNAME")%></span>]</span>
                            </div>    
                            </ItemTemplate>
                            </asp:Repeater>
                        </div>
                   </td>
                </tr>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnSaveInvite" runat="server" Text=" Save " OnClick="btnSaveInvite_OnClick" CssClass="popupclosebtn" style="padding:5px 0px 5px 0px; border:none; background-color :Green;" OnClientClick="DisableMe(this);"/>
                        <asp:Button ID="btnCancelInvite" runat="server" CssClass="popupclosebtn" Text=" Close " CausesValidation="false" style="padding:5px 0px 5px 0px; border:none;" OnClick="btnCancelInvite_OnClick" />
                    </td>
                </tr>
            </table>
            </div>
        </center>
        </div>

    <!-- Close Thread -->
        <div style="position:absolute;top:0px;left:0px; height :370px; width:100%;" id="dvClosureBox" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; background-color:#c2c2c2; padding:5px;">
                <asp:Label ID="Label2" runat="server" style="font-weight:bold;color:Black;" font-size="15px" Text="Close Topic" ></asp:Label>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                 <tr>
                    <td style="text-align:center; padding:5px;"><b>Topic</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div runat="server" id="dv_Topic" style="height:100px;overflow:scroll; overflow-x:hidden; border:solid 1px #c2c2c2;"> 
                    
                        </div>
                   </td>
                </tr>
                <tr>
                    <td style="text-align:center; padding:5px;"><b>Enter your Comments</b>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtClosureComments" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox TextMode="MultiLine" Width="100%" Height="210px" runat="server" ID="txtClosureComments" style=" background-color:#FFFFCC"></asp:TextBox>
                   </td>
                </tr>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnCloseTopic" runat="server" Text=" Save " OnClick="btnCloseTopic_OnClick" CssClass="popupclosebtn" style="padding:5px 0px 5px 0px; border:none; background-color :Green;" OnClientClick="DisableMe(this);"/>
                        <asp:Button ID="btnCancelClosure" runat="server" CssClass="popupclosebtn" Text=" Close " CausesValidation="false" style="padding:5px 0px 5px 0px; border:none;" OnClick="btnCancelClosure_OnClick" />
                    </td>
                </tr>
            </table>
            </div>
        </center>
        </div>

    <!-- Update Category -->
        <div style="position:absolute;top:0px;left:0px; height :370px; width:100%; display:none;" id="dvCategory">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; padding :3px; text-align :center; border :solid 10px #85C2FF; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
            <div style="text-align:center; background-color:#c2c2c2; padding:5px;">
                <asp:Label ID="Label3" runat="server" style="font-weight:bold;color:Black;" font-size="15px" Text="Update Category" ></asp:Label>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <table cellpadding="5" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td>
                        <center>
                        <asp:RadioButton Text="People" runat="server" ID="rad_P1" GroupName="rad_fd"/>
                        <asp:RadioButton Text="Process" runat="server" ID="rad_P2" GroupName="rad_fd"/>
                        <asp:RadioButton Text="Equipment" runat="server" ID="rad_P3" GroupName="rad_fd"/>
                        </center>
                   </td>
                </tr>
                </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table cellpadding="1" cellspacing="0" border="0" width="100%" rules="all">
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnUpdateCat" runat="server" Text=" Save " OnClick="btnUpdateCat_OnClick" CssClass="popupclosebtn" style="padding:5px 0px 5px 0px; border:none; background-color :Green;" OnClientClick="DisableMe(this);"/>
                        <input type="button" class="popupclosebtn" value=" Close " style="padding:5px 0px 5px 0px; border:none;" onclick="HideCatBox();" />
                    </td> 
                </tr>
            </table>
            </div>
        </center>
        </div>
   </asp:Content>

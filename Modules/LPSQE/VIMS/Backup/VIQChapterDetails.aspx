<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIQChapterDetails.aspx.cs" Inherits="VIMS_VIQChapterDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eMANAGER</title>
    <script src="jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VIMSScript.js" type="text/javascript"></script>
    <link href="VIMSStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chapname").click(function () {
                $(this).parentsUntil('tr').parent().find(".qlist").slideToggle();
            });

            //----------------------
            $("#dv_Guidance").keyup(function () {
                if (event.keyCode == 27) {
                    HideGuidanceModel();
                }
            });


            $("#dv_Comments").keyup(function () {
                if (event.keyCode == 27) {
                    HideComments();
                }
            });

            //----------------------

            $("body").scroll(function () {
                var bodystp = $("body").scrollTop();
                if (bodystp == 0) {
                    bodystp = $(document).scrollTop();
                }
                $("#dv_Guidance").css('top', bodystp + 150 + 'px');
                $("#dv_Comments").css('top', bodystp + 150 + 'px');
            });
            //----------------------
            $(document).scroll(function () {
                var bodystp = $("body").scrollTop();
                if (bodystp == 0) {
                    bodystp = $(document).scrollTop();
                }
                $("#dv_Guidance").css('top', bodystp + 150 + 'px');
                $("#dv_Comments").css('top', bodystp + 150 + 'px');
            });
            //----------------------
            $(".tab_1 > a").click(function () {
                $(".c1").hide();
                $(".tab_1").removeClass('tabsel');
                $(this).parent().addClass('tabsel');
                var cc = $(this).attr('ctr');
                $("#" + cc).show();
            });
        });


        function ShowGuidanceModel(ctl) {
            $("#dv_Header").html("VIQ Guidance");
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Guidance").slideDown();
            $("#dv_Guidance").css('top', bodystp + 100 + 'px');
            $("#spn_Guidance").html($(ctl).siblings(".spn_G").html());

            $("#btn_ClosebtnPopUP").focus();
        }
        function HideGuidanceModel() {
            $(".dv_ModalBox").hide();
            $("#dv_Guidance").hide();
        }

        function ShowOfficeRemarkModel(ctl) {
            $("#dv_Header").html("Office Guidance");
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Guidance").slideDown();
            $("#dv_Guidance").css('top', bodystp + 100 + 'px');
            $("#spn_Guidance").html($(ctl).siblings(".spn_OC").html());

            $("#btn_ClosebtnPopUP").focus();
        }


        function ShowCommentsModel(ctl,viqid,qid,rankid) {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dv_Comments").slideDown();
            $("#lblQno").html($(ctl).parentsUntil("tr").parent().find(".clsqno").html());

            $("#dvQuestion").html($(ctl).parentsUntil("tr").parent().find(".spn_Question").html());
            $("#spnGq1").html($(ctl).parentsUntil("tr").parent().find(".spn_G").html());
            $("#spnOFC1").html($(ctl).parentsUntil("tr").parent().find(".spn_OC").html());
            $("#spnORanks").html($(ctl).parentsUntil("tr").parent().find(".spn_ORanks").html());

            $("#dv_Comments").css('top', bodystp + 100 + 'px');


            $("#frmUpload").attr("src", "UploadFile.aspx?Mode=E&VIQId=" + viqid + "&QId=" + qid + "&RankId=" + rankid);

            $("#<%=hfd_RankId.ClientID%>").val(rankid);
            $("#<%=hfd_QId.ClientID%>").val(qid);
            $("#<%=txtUserComments.ClientID%>").val($(ctl).parentsUntil("tr").parent().find(".spnShipComm").html());
            $("#<%=txtUserComments.ClientID%>").focus();
        }
        function HideComments() {
            $(".dv_ModalBox").hide();
            $("#dv_Comments").hide();
        }


        function ShowViewCommentsModel(ctl, viqid, qid, rankid)
        {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dvViewComments").slideDown();
            $("#lblQno_v").html($(ctl).parentsUntil("li").parent().find(".clsqno").html());
            $("#dvQuestion_v").html($(ctl).parentsUntil("li").parent().find(".spn_Question").html());
            $("#dvViewComments").css('top', bodystp + 50 + 'px');

            $("#spnCommby").html($(ctl).parentsUntil("li").parent().find(".spnCommby").html());
            $("#spnCommon").html($(ctl).parentsUntil("li").parent().find(".spnCommon").html());

            $("#spnCommby_S").html($(ctl).parentsUntil("li").parent().find(".spnCommby_S").html());
            $("#spnCommon_S").html($(ctl).parentsUntil("li").parent().find(".spnCommon_S").html());
            
            $("#frmUpload_v").attr("src", "UploadFile.aspx?Mode=V&VIQId=" + viqid + "&QId=" + qid + "&RankId=" + rankid);
            $("#<%=txtShipComments.ClientID%>").val($(ctl).parentsUntil("li").parent().find(".spnShipComm").html());
            $("#<%=txtOFCComments.ClientID%>").val($(ctl).parentsUntil("li").parent().find(".spnOFCComm").html());
            
        }

        function HideViewComments() {
            $(".dv_ModalBox").hide();
            $("#dvViewComments").hide();
        }
        
    </script>
    <style type="text/css">
    .bar
    {
        background-color:Green; 
        height:11px;
    }
    .tab_1
    {
        float:left;
        padding:5px 0px 5px 0px; 
        margin-right:4px;
        border:none;
        color:White;
    }
    
    .tab_1 a
    {
        background-color:grey;
        padding:10px 5px 10px 5px; 
        border:none;
        color:White;
        cursor:pointer;
    }
    tabsel
    {
        float:left;
        padding:5px 0px 5px 0px; 
        margin-right:4px;
        border:none;
        color:White;
    }
     .tabsel a
    {
        background-color:#008AE6;
        padding:10px 5px 10px 5px; 
        border:none;
        color:White;
        cursor:pointer;
    }
    .c1
    {
        padding:5px;
    }
    </style>

    </head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="background-color:Black; opacity:0.4;filter:alpha(opacity=40); width:100%; min-height:100%; position:absolute; top:0px; left:0px; display:none;" class='dv_ModalBox' onclick="HideGuidanceModel();HideComments();"></div>
    <div>
    <table align="center" width="100%" border="1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">    
    <tr>
     <td style=" background-color:#007A99; color:White; padding:4px; font-size:14px; text-align:center;">
        <b>Vetting Preparation</b>
     </td>
     <tr>
       <td style=" background-color:#FFC266; color:White; padding:4px; font-size:14px; text-align:center;">
       <table style="width:100%; font-weight:bold;">
       <tr>
       <td style="width:80px">VIQ #</td>
       <td>:</td>
       <td><asp:Label runat="server" ID="lblviq"></asp:Label></td>
       <td style="width:150px">VIQ Closure Date #</td>
       <td>:</td>
       <td><asp:Label runat="server" ID="lblcdate"></asp:Label></td>
       </tr>
       <tr>
       <td>Chapter #</td>
       <td>:</td>
       <td><asp:Label runat="server" ID="lblchapno"></asp:Label></td>
       <td>Chapter Name</td>
       <td>:</td>
       <td><asp:Label runat="server" ID="lblchapname"></asp:Label></td>
       </tr>
       </table>
       </td>
    </tr>
    </tr>
    <tr>
    <td style=" padding:5px">
    <table>
    <tr>
    <td>Select Rank : </td>
    <td><asp:DropDownList runat="server" ID="ddlRank" AutoPostBack="true" OnSelectedIndexChanged="ddlRankStatus_OnSelectedIndexChanged"></asp:DropDownList></td>
    <td> Select Status : </td>
    <td><asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlRankStatus_OnSelectedIndexChanged">
    <asp:ListItem Text=" < All > " Value=""></asp:ListItem>
    <asp:ListItem Text=" Open " Value="0"></asp:ListItem>
    <asp:ListItem Text=" Closed " Value="1"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    </table>
    </td>
    </tr>
    <tr>
       <td>
       <div style="height:30px; overflow-y:scroll; overflow-x:hidden;">
       <table cellpadding="5" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse; height:30px;' bordercolor="white">
       <thead>
       <tr>
       <td style="width:70px">Question#</td>
       <td>Question</td>
       <td style="width:80px">Resp. Rank</td>
       <td style="width:80px">Risk Score</td>
       <td style="width:40px"><img src="../Images/cv.png" title="VIQ Guidance" style="cursor:pointer; height:12px;" /></td>
       <td style="width:40px"><img src="../Images/icon_comment.gif" title="Office Guidance" style="cursor:pointer"/></td>
       <td style="width:40px">Status</td>
       <td style="width:120px">Office Comments</td>
       <td style="width:30px">&nbsp;</td>
       </tr>
       </table>

       </div>
       <div style="height:505px; overflow-y:scroll; overflow-x:hidden;">
       <table cellpadding="5" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
       <tbody>
            <asp:Repeater runat="server" ID="rpt_ChaptersQuestions">
            <ItemTemplate>
                <tr>
                    <div>
                    <td style="width:70px; text-align:left"><span class='clsqno'><%#Eval("QuestionNo")%></span></td>
                    <td style=" text-align:left; text-justify:inter-word;">
                            <div style="cursor:pointer;" onclick="ShowCommentsModel(this,<%#Eval("VIQId")%>,<%#Eval("QuestionId")%>,<%#Eval("RankId")%>);">
                            <span style='<%#(Common.CastAsInt32(Eval("PreVetting"))>0)?"font-weight:bold":""%>' class="spn_Question">
                            <%#Eval("Question")%>
                            <span class='spn_ORanks' style='display:none'><%#Eval("RanksInvolved")%></span>
                            <span class='spnShipComm' style='display:none'><%#Eval("ShipComments")%></span>
                            <span class='spnOFCComm' style='display:none'><%#Eval("OfficeComment")%></span>
                            <span class='spnCommby' style='display:none'><%#Eval("OfficeCommentby")%></span>
                            <span class='spnCommon' style='display:none'><%#Common.ToDateString(Eval("OfficeCommentdate"))%></span>
                            <span class='spnCommby_S' style='display:none'><%#Eval("NameDoneby")%></span>
                            <span class='spnCommon_S' style='display:none'><%#Common.ToDateString(Eval("DoneDate"))%></span>
                            </span>
                            </div>
                    </td>
                    <td style="width:80px; text-align:left;"><%#Eval("RankCode")%></td>
                    <td style="width:80px"><%#Eval("RiskScore")%></td>
                    <td style="width:40px">
                        <img src="../Images/cv.png" title="VIQ Guidance" onclick="ShowGuidanceModel(this);" style="cursor:pointer; height:12px;" />
                        <span class='spn_G' style='display:none'><%#Eval("Guidance")%></span>
                    </td>
                    <td style="width:40px">
                        <img src="../Images/icon_comment.gif" title="Office Guidance" onclick="ShowOfficeRemarkModel(this);" style="cursor:pointer"/>
                        <span class='spn_OC' style='display:none'><%#Eval("OfficeRemarks")%></span>
                    </td>
                    
                    <td style="width:40px">
                    <img src='../Images/<%#(Convert.IsDBNull(Eval("DONEDATE"))?"red_circle.png":"green_circle.GIF")%>' />
                    </td>
                    <td style="width:120px">
                    <span runat="server"  visible='<%#(Eval("Office_Closed").ToString()=="N" && VIQStatus <=0 )%>' >
                        <a href="#" style='' onclick="ShowCommentsModel(this,<%#Eval("VIQId")%>,<%#Eval("QuestionId")%>,<%#Eval("RankId")%>);" >Ship Comments</a>
                    </span>
                    <span id="Span1" runat="server"  visible='<%#(Eval("Office_Closed").ToString()=="Y" || VIQStatus >=1 )%>' >
                        <a href="#" onclick="ShowViewCommentsModel(this,<%#Eval("VIQId")%>,<%#Eval("QuestionId")%>,<%#Eval("RankId")%>);">View</a>
                    </span>
                    </td>
                    <td style="width:30px">&nbsp;</td>
                    </div>
                </tr>
            </ItemTemplate>
            </asp:Repeater>
            </tbody>
            </table>
       </div>
       </td>
    </tr>
    </table>
    </div>

    <div style="position:absolute; z-index:100; top:100px;left :0px; width:100% ; display:none;" id="dv_Guidance">
        <center>
            <div style="border:solid 10px orange; width:800px; background-color:White; text-align:left; padding:5px; padding-top:0px; ">
                <div id="dv_Header" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;"></div>
                <span id="spn_Guidance"></span>
                <br />
                <center>
                    <input type="button" id="btn_ClosebtnPopUP" class="popupclosebtn" value="Close" onclick="HideGuidanceModel();" style="margin-top:10px;"/>
                </center>
            </div>
        </center>
    </div>

    <div style="position:absolute; z-index:100; top:100px;left :0px; width:100% ; display:none;" id="dv_Comments">
        <center>
            <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; ">
                <center>
                <div id="Div1" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Question : 
                <span id="lblQno"></span>
                </div>
                <div style="width:95%;text-align:left;">
                    <div class="tab_1 tabsel" ><a ctr='dvQuestion'>Question</a></div>
                    <div class="tab_1" ><a ctr='spnGq1'>VIQ Guidance</a></div>
                    <div class="tab_1" ><a ctr='spnOFC1'>Office Guidance</a></div>
                </div>
                <div style="width:95%; height:100px;overflow-y:scroll; border:solid 1px #008AE6; text-align:left;" >
                    <span id="dvQuestion" class="c1" style="" ></span>
                    <span id="spnGq1" class="c1" style="display:none;"></span>
                    <span id="spnOFC1" class="c1" style="display:none;"></span>
                </div>
                <div id="Div2" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Ship Comments</div>
                <asp:TextBox runat="server" style="width:95%;border:solid 1px #e2e2e2;" Height="125px" ID="txtUserComments" TextMode="MultiLine" ValidationGroup="v1"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ValidationGroup="v1" ID="r1" ControlToValidate="txtUserComments" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:HiddenField runat="server" ID="hfd_RankId" />
                <asp:HiddenField runat="server" ID="hfd_QId" />
                <div style='color:Red; padding:5px'>
                Responsible person/s must verify that the ranks below are able to satisfy the guidelines.<br />
                [<span id='spnORanks'></span>]
                </div>
                <div id="Div3" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Upload Attachments</div>
                <iframe width="95%" height="150px" frameborder="yes" src="" scrolling="yes" id="frmUpload"></iframe>
                <br />
                <asp:Button runat="server" ID="btnSaveComments" Text="Save" CausesValidation="true" CssClass="Btn1" ValidationGroup="v1" OnClick="btnSaveComments_Click" />
                <input type="button" id="btn_ClosebtnPopUP1" class="Btn1" value="Close" onclick="HideComments();" style="margin-top:10px; background:red; color:White;"/>
                </center>
            </div>
        </center>
    </div>

    <div style="position:absolute; z-index:100; top:100px;left :0px; width:100% ; display:none;" id="dvViewComments">
        <center>
            <div style="border:solid 10px orange; width:1000px; background-color:White; text-align:left; padding:5px; padding-top:0px; ">
                <center>
                <div id="Div5" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Question : <span id="lblQno_v"></span></div>
                <div style="width:95%; height:70px;overflow-y:scroll; border:solid 1px #008AE6; text-align:left;" >
                    <span id="dvQuestion_v" class="c1" style="" ></span>
                </div>
                <div id="Div6" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Ship Comments</div>
                <asp:TextBox runat="server" style="width:95%;border:solid 1px #e2e2e2;" Height="125px" ID="txtShipComments" TextMode="MultiLine" ValidationGroup="v1" ReadOnly="true"></asp:TextBox>
                 <div id="Div9" style="width:95%;padding:5px; font-size:11px; text-align:center;font-weight:bold;">
                    <div style="width:100%;">
                    <span style="float:left">Comments By : <span id="spnCommby_S"></span></span>
                    <span style="float:right">Comments On : <span id="spnCommon_S"></span></span>
                    </div>
                </div>
                <div id="Div4" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Office Comments</div>
                <asp:TextBox runat="server" style="width:95%;border:solid 1px #e2e2e2;" Height="125px" ID="txtOFCComments" TextMode="MultiLine" ValidationGroup="v1" ReadOnly="true"></asp:TextBox>
                <div id="Div8" style="width:95%;padding:5px; font-size:11px; text-align:center;font-weight:bold;">
                    <div style="width:100%;">
                    <span style="float:left">Comments By : <span id="spnCommby"></span></span>
                    <span style="float:right">Comments On : <span id="spnCommon"></span></span>
                    </div>
                </div>
                
                <div id="Div7" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Attachments</div>
                <iframe width="95%" height="100px" frameborder="yes" src="" scrolling="yes" id="frmUpload_v"></iframe>
                <br />
                <input type="button" id="Button2" class="Btn1" value="Close" onclick="HideViewComments();" style="margin-top:10px; background:red; color:White;"/>
                </center>
            </div>
        </center>
    </div>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VIQChapterDetails.aspx.cs" Inherits="VIMS_VIQChapterDetails" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
    <script src="../js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="VettingScript.js" type="text/javascript"></script>
    <link href="Vettingstyle.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
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

        function ShowViewCommentsModel(ctl,vsl, viqid, qid, rankid)
        {
            $(".dv_ModalBox").show();
            $(".dv_ModalBox").height($("body").height());
            var bodystp = $("body").scrollTop();
            if (bodystp == 0) {
                bodystp = $(document).scrollTop();
            }
            $("#dvViewComments").slideDown();
            $("#lblQno_v").html($(ctl).parentsUntil("tr").parent().find(".clsqno").html());
            $("#dvQuestion_v").html($(ctl).parentsUntil("tr").parent().find(".spn_Question").html());
            $("#dvViewComments").css('top', bodystp + 50 + 'px');
            $("#<%=chkClosure.ClientID%>").attr('checked',false);

            $("#<%=hfd_RankId.ClientID%>").val(rankid);
            $("#<%=hfd_QId.ClientID%>").val(qid);

            $("#spnCommby").html($(ctl).parentsUntil("tr").parent().find(".spnCommby").html());
            $("#spnCommon").html($(ctl).parentsUntil("tr").parent().find(".spnCommon").html());

            $("#spnCommby_S").html($(ctl).parentsUntil("tr").parent().find(".spnCommby_S").html());
            $("#spnCommon_S").html($(ctl).parentsUntil("tr").parent().find(".spnCommon_S").html());

            $("#<%=txtShipComments.ClientID%>").val($(ctl).parentsUntil("tr").parent().find(".spnShipComm").html());
            $("#<%=txtOFCComments.ClientID%>").val($(ctl).parentsUntil("tr").parent().find(".spnOFCComm").html());

            $("#<%=btnSaveComments.ClientID%>").show();
            if ($(ctl).parentsUntil("tr").parent().find(".spnOFCClosure").html() == "1") {
                $("#<%=chkClosure.ClientID%>").attr('checked', true);
                $("#<%=btnSaveComments.ClientID%>").hide();
            }
            
            $("#<%=txtOFCComments.ClientID%>").focus();
            $("#frmUpload_v").attr("src", "ViewUploadFile.aspx?VSL=" + vsl + "&VIQId=" + viqid + "&QId=" + qid + "&RankId=" + rankid);
            
        }

        function HideViewComments() {
            $(".dv_ModalBox").hide();
            $("#dvViewComments").hide();
        }
        
    </script>
    <style type="text/css">
    .bar
    {
        background-color:LightGreen; 
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
    .tr_high
    {
        background-color:#FFEBEB;
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
       <td style="width:40px"><img src="../Images/cv.png" title="Show Guidance" style="cursor:pointer; height:12px;" /></td>
       <td style="width:40px"><img src="../Images/icon_comment.gif" title="Show Office Comments" style="cursor:pointer"/></td>
       <td style="width:40px">Status</td>
       <td style="width:120px">Office Comments</td>
       <td style="width:30px">&nbsp;</td>
       </tr>
       </table>
       </div>
       <div style="height:580px; overflow-y:scroll; overflow-x:hidden;" >
       <table cellpadding="5" rules="rows" cellspacing="0" width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor="#E6E6E6">
       <tbody>
          <asp:Repeater runat="server" ID="rpt_ChaptersQuestions">
          <ItemTemplate>
            <tr class='<%#(Eval("ShipComments").ToString().Trim()!="" &&  Eval("OfficeComment").ToString().Trim()=="")?"tr_high":""%>'>
                <td style="width:70px; text-align:left"><span class='clsqno'><%#Eval("QuestionNo")%></span></td>
                <td style=" text-align:left; text-justify:inter-word;">
                    <span style='<%#(Common.CastAsInt32(Eval("PreVetting"))>0)?"font-weight:bold":""%>' class="spn_Question">
                        <%#Eval("Question")%>
                        <span class='spn_ORanks' style='display:none'><%#Eval("RanksInvolved")%></span>
                        <span class='spnShipComm' style='display:none'><%#Eval("ShipComments")%></span>
                        <span class='spnOFCComm' style='display:none'><%#Eval("OfficeComment")%></span>
                        <span class='spnCommby' style='display:none'><%#Eval("OfficeCommentby")%></span>
                        <span class='spnCommon' style='display:none'><%#Common.ToDateString(Eval("OfficeCommentdate"))%></span>
                        <span class='spnCommby_S' style='display:none'><%#Eval("NameDoneby")%></span>
                        <span class='spnCommon_S' style='display:none'><%#Common.ToDateString(Eval("DoneDate"))%></span>
                        <span class='spnOFCClosure' style='display:none'><%#Eval("OfficeClosureStatus")%></span>
                    </span>
                </td>
                <td style="width:80px; "><%#Eval("RankCode")%></td>
                <td style="width:80px"><%#Eval("RiskScore")%></td>
                <td style="width:40px"><img src="../Images/cv.png" title="VIQ Guidance" onclick="ShowGuidanceModel(this);" style="cursor:pointer; height:12px;" /><span class='spn_G' style='display:none'><%#Eval("Guidance")%></span></td>
                <td style="width:40px"><img src="../Images/icon_comment.gif" title="Office Guidance" onclick="ShowOfficeRemarkModel(this);" style="cursor:pointer"/><span class='spn_OC' style='display:none'><%#Eval("OfficeRemarks")%></span></td>
                <td style="width:40px"><img src='../Images/<%#((Common.CastAsInt32(Eval("OfficeClosureStatus")) > 0)?"green_circle.gif":"red_circle.png")%>' /></td>
                <td style="width:120px">
                    <span runat="server" visible='<%#Auth.IsUpdate%>'>
                        <a href="#" onclick="ShowViewCommentsModel(this,<%#"'" + Eval("VesselCode").ToString()+"'"%>,<%#Eval("VIQId")%>,<%#Eval("QuestionId")%>,<%#Eval("RankId")%>);">Office Comments</a>
                    </span>
                </td>
                <td style="width:30px">&nbsp;</td>
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

    <div style="position:absolute; z-index:100; top:100px;left :0px; width:100% ; display:none;" id="dvViewComments">
        <center>
            <div style="border:solid 10px orange; width:900px; background-color:White; text-align:left; padding:5px; padding-top:0px; ">
                <center>
                <div id="Div5" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Question : <span id="lblQno_v"></span></div>
                <div style="width:95%; height:50px;overflow-y:scroll; border:solid 1px #008AE6; text-align:left;" >
                    <span id="dvQuestion_v" class="c1" style="" ></span>
                        <asp:HiddenField runat="server" ID="hfd_RankId" />
                        <asp:HiddenField runat="server" ID="hfd_QId" />
                </div>
                <div id="Div6" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Ship Comments</div>
                <asp:TextBox runat="server" style="width:95%;border:solid 1px #e2e2e2;" Height="60px" ID="txtShipComments" TextMode="MultiLine" ValidationGroup="v1" ReadOnly="true"></asp:TextBox>
                <div id="Div9" style="width:95%;padding:5px; font-size:11px; text-align:center;font-weight:bold;">
                    <div style="width:100%;">
                    <span style="float:left">Comments By : <span id="spnCommby_S"></span></span>
                    <span style="float:right">Comments On : <span id="spnCommon_S"></span></span>
                    </div>
                </div>
                <div id="Div4" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Office Comments</div>
                <asp:TextBox runat="server" style="width:95%;border:solid 1px #e2e2e2; background-color:#FFFFEB;" Height="100px" ID="txtOFCComments" TextMode="MultiLine" ValidationGroup="gg" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtOFCComments" ValidationGroup="gg"></asp:RequiredFieldValidator>
                <div id="Div8" style="width:95%;padding:5px; font-size:11px; text-align:left;font-weight:bold;">
                    <asp:CheckBox runat="server" ID="chkClosure" Text="Tick the Checkbox if no further information required from the ship." ForeColor="Red"/> 
                    <div style="width:100%;">
                    <span style="float:left">Comments By : <span id="spnCommby"></span></span>
                    <span style="float:right">Comments On : <span id="spnCommon"></span></span>
                    </div>
                </div>
                
                <div id="Div7" style="width:100%;padding:5px; font-size:15px; text-align:center;font-weight:bold;">Attachments</div>
                <iframe width="95%" height="100px" frameborder="yes" src="" scrolling="yes" id="frmUpload_v"></iframe>
                <br />
                <asp:Button runat="server" ID="btnSaveComments" Text="Save" CausesValidation="true" CssClass="Btn1" ValidationGroup="gg" OnClick="btnSaveofcComments_Click" />
                <input type="button" id="Button2" class="Btn1" value="Close" onclick="HideViewComments();" style="margin-top:10px; background:red; color:White;"/>
                </center>
            </div>
        </center>
    </div>
    </form>
</body>
</html>

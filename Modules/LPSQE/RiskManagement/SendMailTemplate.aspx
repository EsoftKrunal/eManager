<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMailTemplate.aspx.cs" Inherits="HSSQE_RiskManagement_SendMailTemplate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
    *{box-sizing:border-box;}
    </style>
</head>
<body style="font-family:Calibri; font-size:15px;margin:0px;">
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
        <div style=" padding:7px; " class="text headerband">Sending Risk Assesment Templates To Vessel</div>
        <asp:Repeater runat="server" ID="rptVsls">
            <ItemTemplate>
                <div style="border-bottom:solid 1px #2E9AFE; padding:3px; color:#2E9AFE; overflow:auto;  ">
                <span style="display:inline-block; float:left;"><%#Eval("Vesselname")%></span>
                <span style="display:inline-block; width:50%; float:right; text-align:left; vertical-align:middle;">

                    <span class="progress" style="display:none; font-size:12px;" vesselid='<%#Eval("VesselId")%>' id='progress_<%#Eval("VesselId")%>'><img src="../../HRD/Images/progress.gif" title="Processing... Please wait." style="float:left"/>&nbsp;Processing..</span>
                    <span><img src="../../HRD/Images/select.png" style="width:16px; float:left;display:none;" title="Mail sent successfully." class="sent" id='done_<%#Eval("VesselId")%>' /></span>
                    <span><img src="../../HRD/Images/delete.gif" style="width:16px; float:left;display:none;" title="Unable to sent mail." class="notsent" id='notdone_<%#Eval("VesselId")%>' /></span>
                    <span style="font-size:12px; margin-left:5px;" id='msg_<%#Eval("VesselId")%>'></span>

                </span>    
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div style="padding:20px; text-align:center;" >
            <input type="button" id="btnclose" value="Close" disabled="disabled" style=" background-color:Red; color:White;  border:none;padding:7px; width:120px;" />
        </div>
    </div>
    <script type="text/javascript">
        function GoProgressNext() {
            $(".progress").first().css("display", "");
        }
        var timerid = null;
        $(document).ready(function () {
            GoProgressNext();
            timerid = window.setInterval(getStatus, 500);
        });
        
        function getStatus() {
            //-------------------------
            var progress = $(".progress").first();
            if (progress != null) 
            {
                var vslid = $(progress).attr('vesselid');
                if (parseInt(vslid) > 0) {
                    $.ajax({
                        url: 'SendMailTemplate.aspx?Mode=RiskAssesmentTemplateMail&VesselId=' + vslid,
                        method: "GET",
                        cache: false,
                        success: function (data) {
                            if (data != null) {
                                var v = null;
                                v = $.parseJSON(data);
                                if (v != undefined) {
                                    if (v[0].Result == "Y") {
                                        $("#done_" + vslid).css("display", "");
                                        $("#progress_" + vslid).css("display", "none");
                                        $("#progress_" + vslid).removeClass("progress");
                                        $("#msg_" + vslid).html(v[0].Message);
                                        GoProgressNext();
                                    }
                                    else if (v[0].Result == "E") {
                                        $("#notdone_" + vslid).css("display", "");
                                        $("#progress_" + vslid).css("display", "none");
                                        $("#progress_" + vslid).removeClass("progress");
                                        $("#msg_" + vslid).html(v[0].Message);
                                        GoProgressNext();
                                    }
                                }
                            }
                        }
                    });
                }
            }
            else {
                clearInterval(timerid);
            }
            //-------------------------
        }
        function StopStatus() {
            clearInterval(timerid);
        }
    </script>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<script type="text/javascript">
    var reloadtime = 3590000;
    var timeout = '<%= Session.Timeout * 60 * 1000 %>';
    var tot = timeout;
    var timer = setInterval(function () {
        tot = tot - 1000;
        spnSession.innerHTML = time(tot);
        if (tot <= 0) {
            clearInterval(timer); alert('Times up...!')
        }
        if (tot == reloadtime) { ShowReload(); }
        if (tot <= reloadtime) {
            spnReload.innerHTML = spnSession.innerHTML;
        }
    }, 1000);

    function two(x) {
        return ((x > 9) ? "" : "0") + x;
    }

    function time(ms) {
        var t = '';
        var sec = Math.floor(ms / 1000);
        ms = ms % 1000

        var min = Math.floor(sec / 60);
        sec = sec % 60;
        t = two(sec);

        var hr = Math.floor(min / 60);
        min = min % 60;
        t = two(min) + ":" + t;
        return "[ " + t + " ]";
    }
</script>
<style type="text/css">
    ul.obtabs {
        list-style: none;
        margin: 1px 0 -1px 0;
        padding: 0;
        position: absolute;
    }

        ul.obtabs li {
            float: left;
            display: block;
            height: 24px;
            padding-right: 20px;
            margin-left: -5px;
            position: relative;
            background: url(images/tabright-back.gif) 100% 0 no-repeat;
            border-bottom: 1px solid #bbb8a9;
            white-space: nowrap;
            width: 80px;
            cursor: pointer;
        }

        ul.obtabs span {
            height: 24px;
            line-height: 24px;
            padding-left: 7px;
            background: url(images/tableft-back.gif) no-repeat;
        }

        html > body ul.obtabs span {
            display: block;
        }

        ul.obtabs li.current {
            z-index: 1;
            font-weight: bolder;
            border-bottom: 1px solid #fff;
            height: 25px;
            background-image: url(images/tabright.gif);
        }

            ul.obtabs li.current span {
                background-image: url(images/tableft.gif);
            }

        ul.obtabs a.new {
            color: Red;
        }

    A {
        text-decoration: none;
        font-size: 12px;
        cursor: pointer;
    }
</style>
<div id="header" style="width: 100%; vertical-align: top; text-align: right; background-image: url(images/header_bg.jpg);">
    <script type="text/javascript">
        function ShowReload() {
            //    document.getElementById("dvRePost").style.visibility="hidden";
            //    document.getElementById("dvRePost").filters[0].Apply();
            //    document.getElementById("dvRePost").style.visibility="visible";
            //    document.getElementById("dvRePost").filters[0].Play();
            return false;
        }
    </script>
    <div style="position: absolute; top: 0px; text-align: right; z-index: 100; left: 60px; background-image: url(Images/Alert/impmsg.png); display: block; height: 93px; width: 404px; visibility: hidden; filter: progid:DXImageTransform.Microsoft.Fade(duration=1.0,overlap=1.0)" id="dvRePost">
        <div style="position: relative; left: 245px; top: 16px; text-align: center; width: 140px; color: Red; font-size: 12px; font-family: Verdana; font-weight: normal;">Please refresh your browser window.<br />
            Your session will be closed in  <span id="spnReload"></span>Minuts.</div>
    </div>
    <div style="width: 100%; height: 124px; overflow: hidden">
        <div style="float: left; position: relative">
            <img src="images/logo.png" width="165" height="70" alt="">
            <span id="spnSession" style="position: absolute; top: 5px; left: 5px; color: #b6e3d4;"></span>
        </div>
        <div style="float: right">
            <!-- <img src="images/map.jpg" width="259" height="118" alt=""> -->
        </div>
        <div style="padding: 47px 259px 0px 235px;">
            <div style="float: left; padding-top: 20px; height: 25px; text-align: right; width: 200px; color: #808080">
                <asp:Label runat="server" ID="txtUserName" Font-Size="11px"></asp:Label>
            </div>
        </div>
        <br />
        <br />
        <br />
        <center>
            <div style="height: 27px;">

                <ul class="obtabs">
                    <!-- <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'  class="current" style='width:110px;'><span><a id="btn_DB" runat="server" title="My Dashboard" >DASHBOARD</a></span></li> -->
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_CMS" class="new" runat="server" title="Crew Management System">HRD</a></span></li>
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_VIMS" runat="server" title="Vessel Inspection & Audits">INSPECTION</a></span></li>
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_POS" runat="server" title="Purchase System">PURCHASE</a></span></li>
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_PMS" runat="server" title="Planned Maintenance System">PMS</a></span></li>
                    <li runat="server" visible="false" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_eMTM" runat="server" title="Office">OFFICE</a></span></li>
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_Analytics" title="Budget" runat="server">BUDGET</a></span></li>
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_Hssqe" title="HSSQE" runat="server">LPSQE</a></span></li>
                    <!--<li runat="server" onclick='this.childNodes[0].childNodes[0].click();'  style='width:110px;'><span><a id="btn_Accounts" title="Accounts" runat="server">ACCOUNTS</a></span></li> -->
                    <li runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_ADMIN" title="Administrator" runat="server">ADMIN</a></span></li>
                </ul>
                <div style="float: right; text-align: right; width: 350px; color: #4172ac; padding-top: 0px;">
                    <%--<asp:ImageButton runat="server" ImageUrl="~/images/chpwd.png"  OnClientClick="GoChange(); return false;"/>--%>
                    <span runat="server" id="btn_ChPwd">
                        <img src="./images/chpwd.png" onclick="GoChange(); return false;" />
                    </span>
                    <asp:ImageButton runat="server" CssClass="lgo" ImageUrl="~/images/logout.png" ID="LinkButton1" ForeColor="Red" OnClick="btn_LogOut_Click" Font-Underline="true" />
                </div>
            </div>
        </center>
        <div style="background-color: red; height: 3px; width: 100%"></div>
        <div style="background-color: #4371a5; height: 3px; width: 100%"></div>
    </div>
</div>

<script type="text/javascript">
    function GoChange() {
        //alert(document.getElementById("frmApp"));
        document.getElementById("frmApp").attributes("src", "changepassword.aspx");
    }

    var timeout = 400;
    var closetimer = 0;
    var ddmenuitem = 0
    var lastpost = 0;

    function DoPost(arg) {
        document.getElementById('<%=hfd_CB.ClientID%>').setAttribute("value", arg);
        document.getElementById('<%=btnPost.ClientID%>').click();
        lastpost = arg;
    }
    function DoPostLast() {
        ctl = '<%=CurrentModule%>';
        if (ctl != "0") {
            var arg = ctl;
            document.getElementById('<%=hfd_CB.ClientID%>').setAttribute("value", arg);
            document.getElementById('<%=btnPost.ClientID%>').click();
        }
    }
    function mclosetime() {
        closetimer = window.setTimeout(HideMenu, timeout);
    }

    // cancel close timer
    function mcancelclosetime() {
        if (closetimer) {
            window.clearTimeout(closetimer);
            closetimer = null;
        }
    }
    function InvertImage(ctl, arg) {
        ctl.style.backgroundImage = 'url(images/' + arg + '_s.png)';
    }
    function RestoreImage(ctl, arg) {
        ctl.style.backgroundImage = 'url(images/' + arg + '.png)';
    }
    function ChangePwd() {
        document.getElementById("frmApp").src = 'changepassword.aspx';
    }
</script>
<asp:LinkButton runat="server" ID="btnPost" Text="" OnClick="btn_POST_Click" arg="0" TabIndex="0" />
<asp:HiddenField runat="server" ID="hfd_CB" />


<!-- Retention Rate Verification Per Month  -->
<div style="position: absolute; top: 0px; left: 0px; height: 470px; width: 100%;" id="divChPwd" runat="server" visible="false">
    <center>
        <div style="position: absolute; top: 0px; left: 0px; height: 750px; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
        <div style="position: relative; width: 400px; height: 230px; padding: 3px; text-align: center; border: solid 1px #4371a5; background: green; z-index: 150; top: 30px; opacity: 1; filter: alpha(opacity=100)">

            <div style="text-align: center;">
                Change Pwd
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </div>
    </center>
</div>

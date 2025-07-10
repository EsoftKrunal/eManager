<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Src="~/UserControls/FullHeader.ascx" TagName="header" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<%@ Register namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <title>eMANAGER</title>
        
        <script type ="text/javascript" language ="javascript"  >
            var login='yes';
            var loc="";
            loc=window.parent.location;
            var pos=("" + loc + "").indexOf('Home.aspx');
            if (pos!=-1)
            {
                top.location='login.aspx';
            }
        </script>
        <script type="text/javascript" language ="javascript" src="JScript.js"></script>   
        <meta name="Keywords" content="ship management, vessel management, Oil Tankers, Oil Tankers Specialist,  tanker management, reefer management, shipboard maintenance, ship manager, crew administration,vessel inspections, project superintendence, ship operation services" />
        <meta name="" content="Our Inspiration is drawn from the ant, which goes about its business quietly, carrying twice its body weight and planning for the season ahead. We imbibe this quality into the very nature of our company as it requires foresight to plan and execute a job in the field of ship management." />
        <style type="text/css">
            .login-wrapper {
	display: flex;
	height: 585px;
}

.login-bg {
	width: 50%;
	background-color: #6c9e7b;
	text-align: center;
	display: flex;
	justify-content: center;
	align-items: center;
}

.login-wrap {
	width: 50%;
	display: flex;
	justify-content: center;
	align-items: center;
}

.inner-field {
	width: 305px;
	margin: 0 auto;
	padding: 20px;
}

#footer {
	float: left;
	color: #4172ac;
	height: 20px;
	width: 100%;
	position: relative;
	margin-top: -20px;
	clear: both;
	background-image: url(images/footer_bg.jpg);
	text-align: center;
	padding-top: 3px;
	font-size: 11px;
}


@media screen and (max-width:767px) {
	.login-wrapper {
		display: flex;
		flex-direction: column;
		height: 585px;
		background-color: #6c9e7b;
	}

	.login-bg {
		width: 100%;
		background-color: #6c9e7b;
		text-align: center;
		display: flex;
		justify-content: center;
		align-items: center;
		padding-top: 20px;
	}

	.login-wrap {
		width: 100%;
		display: flex;
		justify-content: center;
		align-items: center;
		padding-top: 20px;
	}

		.login-wrap fieldset {
			border-color: white;
		}

	.inner-field {
		width: 305px;
		margin: 0 auto;
		padding: 10px 0px;
	}

		.inner-field span {
			color: #fff;
		}
}
            </style>
    </head>
    <body onload="MM_preloadImages('images/bt_company_r.jpg','images/bt_services_r.jpg','images/bt_careers_r.jpg','images/bt_contacus_r.jpg','images/bt_login_r.jpg')">
        <form runat="server" id="frmMain" defaultbutton="btn_Login">
            <%--<div id="container">
                <div id="main">
                    <table width="100%" style="height:585px;" >
                        <tr>
                            <td width="50%" style="background-color:#6c9e7b;text-align:center;vertical-align:middle;height:450px;">
                                <table style="margin: 0 auto;">
                                    <tr >
                                        <td>
                                             <img alt="" src="~/images/Energios_logo_Latest_22112022.png" runat="server" style="width:300px;height:100px;" />
                                        </td>
                                    </tr>
                                    <tr style="height:40px;text-align:center;">
                                        <td style="color:#fff;font-size:20px;text-align:center;vertical-align:middle;font-weight:500;font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Energising Maritime Businesses
                                        </td>
                                    </tr>
                                </table>
                               
                                
                                
                            </td>
                            <td width="50%" >
                                <div style="width :100%; text-align :center; padding-top :30px;">
                        <mtm:Message ID="MessageBox1" runat="server" /> 
                        <center>
                            <fieldset style="width:300px;">
                                <legend style=" color : red; font-size : 12px;" >Sign In </legend>
                                <div style="width :305px; height:78px; text-align :center; padding-top :20px; ">
                                    <div style=" height :25px;">
                                        <div style="width : 120px;float :left; text-align :right;">User Name : </div>
                                        <div style="float :left; padding-left :5px;">
                                            <asp:TextBox runat="server" ID="txt_Login" MaxLength ="30"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="height :25px;">
                                        <div style="width : 120px;float :left;text-align :right">Password : </div>
                                        <div style="float :left;padding-left :5px;">
                                            <asp:TextBox runat="server" ID="txt_Pwd" TextMode="Password" MaxLength ="30"></asp:TextBox>
                                        </div>
                                    </div> 
                                    <div>
                                        <asp:Button runat="server" ID="btn_Login" Text="Sign In" onclick="btn_Login_Click" />
                                    </div>
                                </div> 
                            </fieldset>
                        </center>
                    </div>
                            </td>
                        </tr>

                    </table>
                   
                    
                </div>
            </div>--%>
            <div id="container">
            <div id="main" class="login-wrapper" >
                <div class="login-bg" >
                    <div style="text-align: center;">
                        <img src="images/Energios_logo_Latest_22112022.png" style="width: 300px; height: 100px;" alt="Energios Logo" />
                        <div style="color: #fff; font-size: 20px; font-weight: 500; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin-top: 40px;">
                            Energising Maritime Businesses
                        </div>
                    </div>
                </div>
                <div class="login-wrap">
                    <div>
                        <fieldset style="width: 300px; ">
                            <mtm:Message ID="MessageBox1" runat="server" />
                            <legend style="color: red; font-size: 12px;font-family:Arial;">Sign In</legend>
                            <div class="inner-field">

                                <div style="margin-bottom: 10px;">
                                    <asp:Label ID="lblLogin" runat="server" style="width: 120px; display: inline-block; text-align: right;font-family:Arial;" Text="User Name : "></asp:Label>
                                  <%--  <label for="txt_Login" style="width: 120px; display: inline-block; text-align: right;">User Name:</label>--%>
                                    <asp:TextBox runat="server" ID="txt_Login" MaxLength ="30"></asp:TextBox>
                                </div>
                                <div style="margin-bottom: 10px;">
                                    <asp:Label ID="lblPwd" runat="server" style="width: 120px; display: inline-block; text-align: right;font-family:Arial;" Text="Password : "></asp:Label>
                                   <%-- <label for="txt_Pwd" style="width: 120px; display: inline-block; text-align: right;">Password:</label>--%>
                                     <asp:TextBox runat="server" ID="txt_Pwd" TextMode="Password" MaxLength ="30"></asp:TextBox>
                                </div>
                                <div style="text-align: center;">
                                    <asp:Button runat="server" ID="btn_Login" Text="Sign In" onclick="btn_Login_Click" />
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
            <mtm:footer ID="footer1" runat ="server" />
        </form>
    </body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppletUploader.aspx.cs" Inherits="AppletUploader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <applet id="jumpLoaderApplet" name="jumpLoaderApplet" code="jmaster.jumploader.app.JumpLoaderApplet.class" archive="../jar/jumploader_z.jar" width="1060" height="400" mayscript>
    <param name="uc_imageEditorEnabled" value="true"/>
	<param name="uc_uploadUrl" value='AppletReceiver.aspx?Key=<%=Request.QueryString["Key"].ToString()%>&ReportId=<%=Request.QueryString["ReportId"].ToString()%>'/>
	<param name="uc_partitionLength" value="1000000"/>
    </applet>
    <%--<param name="uc_uploadUrl" value="AppletReceiver.aspx"/>--%>
    
    </div>
    </form>
</body>
</html>

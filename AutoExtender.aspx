<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoExtender.aspx.cs" Inherits="AutoExtender" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
       <Services>
       <asp:ServiceReference Path="~/WebService.asmx" />
       </Services>
   </ajaxToolkit:ToolkitScriptManager>
    <div>
          <asp:TextBox runat="server" ID="myTextBox" Width="300"/>
            <ajaxToolkit:AutoCompleteExtender runat="server" 
              ID="autoComplete1" 
              TargetControlID="myTextBox"
              ServiceMethod="GetCompletionList"
              MinimumPrefixLength="1"
              UseContextKey="true"
              ServicePath="~/WebService.asmx"
              ContextKey="Country|CountryId"
              />
    </div>
    </form>
</body>
</html>

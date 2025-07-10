<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadDocuments.aspx.cs" Inherits="eReports_UploadDocuments" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShipSoft - eReports</title>
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <style type="text/css">
    .fileBox
    {
        background-color:#f2f2f2;
        margin:4px;
        padding:4px;
        border:solid 1px gray;
    }
    .descr
    {
        width:400px;
        border:solid 1px gray;
        margin:5px;
        background-color:#EBF5FF;
    }
    
    .btn_upload {
       background: #fa9128;
      background-image: -webkit-linear-gradient(top, #fa9128, #b87f2b);
      background-image: -moz-linear-gradient(top, #fa9128, #b87f2b);
      background-image: -ms-linear-gradient(top, #fa9128, #b87f2b);
      background-image: -o-linear-gradient(top, #fa9128, #b87f2b);
      background-image: linear-gradient(to bottom, #fa9128, #b87f2b);
      -webkit-border-radius: 5;
      -moz-border-radius: 5;
      border-radius: 5px;
      font-family: Arial;
      color: #ffffff;
      font-size: 13px;
      padding: 7px 19px 7px 20px;
      text-decoration: none;
    }

    .btn_upload:hover {
      background: #f5bf62;
  background-image: -webkit-linear-gradient(top, #f5bf62, #deaa56);
  background-image: -moz-linear-gradient(top, #f5bf62, #deaa56);
  background-image: -ms-linear-gradient(top, #f5bf62, #deaa56);
  background-image: -o-linear-gradient(top, #f5bf62, #deaa56);
  background-image: linear-gradient(to bottom, #f5bf62, #deaa56);
  text-decoration: none;
    }
    
    
    
    .btn {
    background: #3491d9;
      background-image: -webkit-linear-gradient(top, #3491d9, #2980b9);
      background-image: -moz-linear-gradient(top, #3491d9, #2980b9);
      background-image: -ms-linear-gradient(top, #3491d9, #2980b9);
      background-image: -o-linear-gradient(top, #3491d9, #2980b9);
      background-image: linear-gradient(to bottom, #3491d9, #2980b9);
      -webkit-border-radius: 8;
      -moz-border-radius: 8;
      border-radius: 8px;
      font-family: Arial;
      color: #ffffff;
      font-size: 12px;
      padding: 6px 21px 7px 21px;
      text-decoration: none;
    }

    .btn:hover {
      background: #3cb0fd;
      background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);
      background-image: -o-linear-gradient(top, #3cb0fd, #3498db);
      background-image: linear-gradient(to bottom, #3cb0fd, #3498db);
      text-decoration: none;
    }
    </style>
      <script type="text/javascript">
          $(document).ready(function () {
              $("#btnupl").click(function () {
                  $("#d1").click();
              });

              $("#d1").change(function () {
                  var FilesHTML = "";
                  var Files = $(this).val();
                  var arr = Files.split(',');
                  $("#fls").html('');
                  $.each(arr, function (i, o) {
                      var filename = o.split("\\")[o.split("\\").length - 1];
                      var filenamewithoutextension = filename.split(".")[0];
                      FilesHTML = FilesHTML + "<div>";

                      $('<div class="fileBox"><b>File : </b> ' + filename + '<br><b>Description :</b> <input class="descr" type="text" title="' + o + '" id="txt' + i + '" value="' + filenamewithoutextension + '" name="txt' + i + '"></input><span style="color:red">*</span></div>').appendTo("#fls");
                      $("#txtNOF").val(i + 1);
                  });
              });

          });

          function Validate() {
              var result=true;
              $(".descr").each(function (i, o) {
                  if ($(o).val() == "") {
                      $("#Label1").html('Please enter description with files.');
                      result = false;
                  }
              });
              return result;
          }
    </script>
</head>
<body style="font-family:Arial ; font-size:13px">
     <form id="form1" runat="server" enctype='multipart/form-data'>
     <div>
        <asp:TextBox runat="server" ID="txtNOF" style='display:none'></asp:TextBox>
        <asp:FileUpload runat="server" ID="d1" multiple style="display:none"/>
        <input type="button" id="btnupl" value=" ( + ) Select Files" class="btn_upload"/>
        &nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Upload Selected Files" CssClass="btn" Width="160px" OnClientClick="return Validate();"  />
        <br /><br />
        &nbsp;<asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <div id="fls" style="width:550px"></div>
        <br />
    </div>
    </form>
</body>
</html>


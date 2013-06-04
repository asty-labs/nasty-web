<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register TagPrefix="t" Assembly="Nasty.Mvc" Namespace="Nasty.Mvc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
  <head>
    <title>Guess a number</title>
	<script src="Scripts/jquery-1.7.min.js" type="text/javascript"></script>
	<script src="Scripts/jquery.form.js" type="text/javascript"></script>
	<script src="Scripts/jasty-core.js" type="text/javascript"></script>
    <script src="Scripts/jasty-components.js" type="text/javascript"></script>
  </head>
  <body>
	<script>
	    jasty.settings.formEngineUrl = "formEngine";
	</script>
    <t:FormViewer runat="server" ID="myform" EntryPointClass="Nasty.Samples.Forms.MainForm, Nasty.Samples"/>
  </body>
</html>

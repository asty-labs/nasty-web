<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
  <%
      var currentForm = (Nasty.Samples.Forms.MainForm)ViewData["currentForm"];
  %>
<div class="status"><%=ViewData["model"]%></div>
<div><%=currentForm.Counter%></div>
<div>Guess a number between <%=currentForm.LowerBound%> and <%=currentForm.UpperBound%>:</div>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Nasty.Mvc" %>

<h1>Congratulations!</h1>
<div>You got the correct answer in <%=ViewData["model"]%> tries</div>
<%=Html.Component(new Nasty.Components.Button { OnClick = "StartNewGame", Text = "Play Again" })%>

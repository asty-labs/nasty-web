<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Nasty.Mvc" %>
<%
    var currentForm = (Nasty.Samples.Forms.MainForm)ViewData["currentForm"];
%>
<h1>Guess a number</h1>
<div id="<%=currentForm.Id%>_stats">
</div>
<input type="hidden" name="myform.t1" value="1"/>
<input type="hidden" name="myform.t1" value="2"/>
<%=Html.Component(new Nasty.Components.TextBox { Id = "guessEntryField" })%>
<%=Html.Component(new Nasty.Components.Button { OnClick = "ProcessGuess", Text = "Submit Your Guess" })%>
<%=Html.Component(new Nasty.Components.Button { OnClick = "StartNewGame", Text = "Start a New Game" })%>

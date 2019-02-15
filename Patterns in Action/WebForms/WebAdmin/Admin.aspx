<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebForms.WebAdmin.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administration</h1>

    <p>
       Administration pages are password protected and access is restricted to 
       authorized administrators. The new <i>SimpleMembership</i> functionality in 
       .NET 4.5 is great for this purpose. 
       This administration module supports Member and Order management. 
    </p>
    <p>
       For additional administration and reporting functionality please check the 
    <b>Spark 4.5</b> web reference application called <i>Art Shop</i>. 
    </p>


    <ul>
        <li><a href="/admin/members">Click here</a> to maintain members</li>
        <li><a href="/admin/members/orders">Click here</a> to view orders</li>
    </ul>
</asp:Content>

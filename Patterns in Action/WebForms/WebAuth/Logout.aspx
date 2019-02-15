<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="WebForms.WebAuth.Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Logout</h1>

    <p>
    Thank you for using the <i>ASP.NET Web Forms</i> application.<br />
    You are now logged out.  
    </p>

    <br />

    <ul>
      <li><asp:HyperLink ID="HyperLinkLogin" runat="server" NavigateUrl="~/login" Text="Click here"></asp:HyperLink> to log back in</li>
      <li><asp:HyperLink ID="HyperLinkHome" runat="server" NavigateUrl="~/" Text="Click here"></asp:HyperLink> to return to home page</li>
      <li><asp:HyperLink ID="HyperLinkShopping" runat="server" NavigateUrl="~/shop" Text="Click here"></asp:HyperLink> to start shopping</li>
    </ul>

</asp:Content>

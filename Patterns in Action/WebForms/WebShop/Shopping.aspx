<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="WebForms.WebShop.Shopping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Shopping</h1>

    <p style="width: 600px;">
      This is where users start shopping. They can browse and/or search a catalog 
      of products.  The app demonstrates navigation patterns you find in most apps: searching, filtering, 
      sorting, and master-detail. The master-detail paradigm is demonstrated by product and order pages. 
      The list of products represents the master and the product details page represents the detail. 
      Similarly with orders and order details.
</p>
<p>
    For additional functionality, including pagination and a shopping cart, please check 
    the <i>Spark 4.5</i> web reference application called <i>Art Shop</i>. 
</p>
 
    <ul>
        <li><a href="/shop/products">Click here</a> to start shopping</li>
        <li><a href="/shop/search">Click here</a> to search for products</li>
    </ul>
</asp:Content>

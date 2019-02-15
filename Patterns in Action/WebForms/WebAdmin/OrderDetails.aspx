<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="WebForms.WebAdmin.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Content header and back button -->

    <ul class="zone">
        <li class="zoneleft">
            <h1>
                <asp:Label ID="LabelHeader" runat="server" /></h1>
        </li>
        <li class="zoneright">
            <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back"></asp:HyperLink></li>
    </ul>
    <div style="clear: both"></div>

    <!-- Member name and order date -->
    <h3>
        <asp:Label ID="LabelOrderDate" runat="server" /></h3>
    <br />

    <!-- Order details GridView -->

    <asp:GridView ID="GridViewOrderDetails" runat="server"
        AutoGenerateColumns="False" Width="600">
        <Columns>
            <asp:BoundField HeaderText="Product" DataField="ProductName" />
            <asp:BoundField HeaderText="Quantity" DataField="Quantity" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Discount" DataField="Discount" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" />
        </Columns>
    </asp:GridView>

    <br />
    <br />

</asp:Content>

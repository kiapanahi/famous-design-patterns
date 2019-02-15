<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WebForms.WebShop.Product" %>

<%@ Import Namespace="BusinessObjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content header and back button -->

    <ul class="zone">
        <li class="zoneleft">
            <h1>Product Details</h1>
        </li>
        <li class="zoneright">
            <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back to products page"></asp:HyperLink></li>
    </ul>

    <div id="main-body">

        <!-- Product image -->

        <asp:Image ID="ImageProduct" runat="server"
            CssClass="floatright" Width="90" Height="90" BorderWidth="0" />

        <!-- Details View -->
        <asp:DetailsView ID="DetailViewProduct" runat="server"
            DataSourceID="ObjectDataSourceProduct">
            <Fields>
                <asp:TemplateField HeaderText="Category&nbsp;" ItemStyle-BackColor="#eeeeee" ItemStyle-Font-Bold="True">
                    <ItemTemplate>
                        <asp:Label ID="LabelCategory" runat="server" Text='<%# (Eval("Category") as Category).CategoryName %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="ProductName" HeaderText="Product Name&nbsp;" />
                <asp:BoundField DataField="UnitPrice" DataFormatString="{0:c}" HtmlEncode="False" HeaderText="Price&nbsp;" />
                <asp:BoundField DataField="Weight" HeaderText="Weight&nbsp;" />
                <asp:BoundField DataField="UnitsInStock" HeaderText="# In Stock&nbsp;" />
            </Fields>
        </asp:DetailsView>

        <!-- Object Data Source -->

        <asp:ObjectDataSource ID="ObjectDataSourceProduct" runat="server"
            TypeName="ActionService.Service" SelectMethod="GetProduct">
            <SelectParameters>
                <asp:RouteParameter Name="ProductId" RouteKey="productid" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <br />
        <br />

        <input name="quantity" id="quantity" type="text" value="1" maxlength="2" style="width: 30px; margin: 0 5px 0 150px;" />
        <input type="submit" id="button" value=" Add to Cart " />

        <br />

    </div>

    <br />
    <br />
    <script type="text/javascript">
        $(function () {
            $("#button").on("click", function () {
                alert("Shopping cart is not implemented in Patterns in Action 4.5. \nFor a full implementation see the Spark 4.5 app.");
                return false;
            });
        });

    </script>

</asp:Content>

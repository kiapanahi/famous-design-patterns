<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WebForms.WebShop.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Products</h1>

    <%-- Ajax Update Panel --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%-- Product Category --%>
    
    Select a Category:&nbsp;
            <asp:DropDownList ID="DropDownListCategories" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceCategories"
                DataTextField="CategoryName" DataValueField="CategoryId" Width="130" Font-Size="11pt" OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:ObjectDataSource ID="ObjectDataSourceCategories" runat="server"
                SelectMethod="GetCategories" TypeName="ActionService.Service"></asp:ObjectDataSource>
            <br />
            <br />


            <%-- Product List --%>

            <span class="sortmessage">Click on headers to sort</span>

            <asp:GridView ID="GridViewProducts" runat="server"
                AutoGenerateColumns="False" Width="600"
                AllowSorting="True"
                OnSorting="GridViewProducts_Sorting"
                OnRowCreated="GridViewProducts_RowCreated">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="ProductId" SortExpression="ProductId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />
                    <asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" HeaderStyle-Width="300" />
                    <asp:BoundField HeaderText="Weight" DataField="Weight" SortExpression="Weight" HeaderStyle-Width="80" />
                    <asp:BoundField HeaderText="Price" DataField="UnitPrice" SortExpression="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" />
                    <asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="~/shop/products/{0}"
                        Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" />
                </Columns>
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <br />

</asp:Content>

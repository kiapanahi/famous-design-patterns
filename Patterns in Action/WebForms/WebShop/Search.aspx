<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebForms.WebShop.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Search Products</h1>

    <%-- Search criteria --%>

    Product Name: 
     <asp:TextBox ID="TextBoxProductName" runat="server" Width="100" Font-Size="11pt"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    Price range: 
    <asp:DropDownList ID="DropDownListRange" runat="server" Width="130" DataSourceID="ObjectDataSourcePriceRange" Font-Size="11pt" DataTextField="RangeText" DataValueField="RangeId">
        <asp:ListItem Selected="True" Value="0"></asp:ListItem>
    </asp:DropDownList>

    &nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server" Text=" Find " UseSubmitBehavior="true" OnClick="ButtonSearch_Click" />
    &nbsp;&nbsp;<asp:HyperLink ID="HyperLinkReset" runat="server" Text="reset" NavigateUrl="~/shop/search" />

    <hr />
    <br />

    <%--  Price range ObjectDataSource --%>

    <asp:ObjectDataSource runat="Server" ID="ObjectDataSourcePriceRange"
        SelectMethod="GetList" TypeName="WebForms.Code.PriceRange"></asp:ObjectDataSource>

    <%-- Ajax Update Panel --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--  Search results --%>

            <asp:Panel ID="PanelSearchResults" runat="server" Visible="false">

                <span class="sortmessage">Click on headers to sort</span>

                <asp:GridView ID="GridViewProducts" runat="server"
                    AutoGenerateColumns="False" Width="600"
                    AllowSorting="True"
                    OnSorting="GridViewProducts_Sorting"
                    OnRowCreated="GridViewProducts_RowCreated"
                    EmptyDataText="No products found. Please try again">

                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="ProductId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="ProductId" HeaderStyle-Width="50" />
                        <asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" HeaderStyle-Width="350" />
                        <asp:BoundField HeaderText="Price" DataField="UnitPrice" DataFormatString="{0:c}" SortExpression="UnitPrice" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                        <asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="~/shop/products/{0}"
                            Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" />
                    </Columns>
                    <EmptyDataRowStyle Font-Bold="True" BackColor="FloralWhite" />
                </asp:GridView>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <br />
    <br />

</asp:Content>

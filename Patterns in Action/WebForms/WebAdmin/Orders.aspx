<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="WebForms.WebAdmin.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Orders by Member</h1>

    <%-- Ajax Update Panel --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <!-- Members wit Order Summary GridView -->

            <span class="sortmessage">Click on headers to sort</span>

            <asp:GridView ID="GridViewOrders" runat="server"
                AutoGenerateColumns="False" Width="640"
                AllowSorting="True"
                OnSorting="GridViewOrders_Sorting"
                OnRowCreated="GridViewOrders_RowCreated">

                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="MemberId" SortExpression="MemberId" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Name" DataField="CompanyName" SortExpression="CompanyName" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="# Orders" DataField="NumOrders" SortExpression="NumOrders" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Last Order" DataField="LastOrderDate" SortExpression="LastOrderDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:HyperLinkField HeaderText="Orders" DataNavigateUrlFields="MemberId" DataNavigateUrlFormatString="~/admin/members/{0}/orders"
                        Text="View" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <br />
</asp:Content>

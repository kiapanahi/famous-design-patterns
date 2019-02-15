<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="WebForms.WebAdmin.Members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Members</h1>

    <%-- Ajax Update Panel --%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <%-- Error message and Add new Member button --%>

            <ul id="customers">
                <li id="customerserror">
                    <asp:Label ID="LabelError" runat="server" Text="" EnableViewState="false"></asp:Label></li>
                <li id="addnewcustomer">
                    <asp:HyperLink ID="HyperLinkNewMember" runat="Server"
                        Text="Add new Member" NavigateUrl="~/admin/members/0" /></li>
            </ul>



            <%-- Member GridView --%>

            <span class="sortmessage">Click on headers to sort</span>

            <asp:GridView ID="GridViewMembers" runat="server"
                DataKeyNames="MemberId"
                AutoGenerateColumns="False" Width="600"
                AllowSorting="True"
                OnRowDataBound="GridView_RowDataBound"
                OnSorting="GridViewMembers_Sorting"
                OnRowCreated="GridViewMembers_RowCreated"
                OnRowDeleting="GridViewMembers_RowDeleting">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="MemberId" SortExpression="MemberId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />
                    <asp:BoundField HeaderText="Member Name" DataField="CompanyName" SortExpression="CompanyName" HeaderStyle-Width="220" />
                    <asp:BoundField HeaderText="Email" DataField="Email" SortExpression="Email" HeaderStyle-Width="120" />
                    <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="MemberId" DataNavigateUrlFormatString="~/admin/members/{0}"
                        Text="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" />
                    <asp:CommandField HeaderText="Delete" ButtonType="Link" ItemStyle-ForeColor="#006666"
                        ShowDeleteButton="True" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70" />
                </Columns>
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <br />
    <br />
</asp:Content>

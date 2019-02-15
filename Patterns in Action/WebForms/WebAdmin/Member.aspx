<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="WebForms.WebAdmin.Member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content header and back button -->

    <ul class="zone">
        <li class="zoneleft">
            <h1>Member Details</h1>
        </li>
        <li class="zoneright">
            <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back to member list"></asp:HyperLink></li>
    </ul>

    <div id="main-body">

        <!-- Member image -->

        <asp:Image ID="ImageMember" runat="server" CssClass="floatright" Width="100" Height="100" BorderStyle="None" />

        <!-- Form View -->

        <asp:DetailsView ID="DetailsViewMember" runat="server"
            DataKeyNames="MemberId"
            DataSourceID="ObjectDataSourceMember"
            OnDataBound="DetailsView_OnDataBound">
            <Fields>
                <asp:BoundField DataField="MemberId" HeaderText="Id&nbsp;" InsertVisible="False" ReadOnly="True" ItemStyle-BackColor="#ffeecc" ItemStyle-Font-Bold="True" />
                <asp:BoundField DataField="Email" HeaderText="Email&nbsp;" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company&nbsp;" />
                <asp:BoundField DataField="City" HeaderText="City&nbsp;" />
                <asp:BoundField DataField="Country" HeaderText="Country&nbsp;" />
            </Fields>
            <FooterTemplate>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="ButtonSave" runat="server" Text=" Save " OnClick="ButtonSave_Click"></asp:Button>
                &nbsp;&nbsp;<asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click"></asp:Button>
            </FooterTemplate>
            <FooterStyle Height="40" BackColor="White" HorizontalAlign="Center" />
        </asp:DetailsView>

        <!-- Object Data Source -->

        <asp:ObjectDataSource ID="ObjectDataSourceMember" runat="server"
            TypeName="ActionService.Service"
            SelectMethod="GetMember">
            <SelectParameters>
                <asp:RouteParameter Name="MemberId" RouteKey="memberid" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>

    <!-- Error message label -->
    <br />

    <asp:Panel ID="PanelError" runat="server" ForeColor="#000000" BackColor="#ffffff" Visible="false">
        <asp:Label ID="LabelError" runat="server" />
    </asp:Panel>

    <br />
    <br />
</asp:Content>

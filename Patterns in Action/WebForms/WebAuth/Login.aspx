<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms.WebAuth.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Login</h1>

    <p>
        To access the administration area you need to login as an Admin.
    <br />
        Admin credentials are: &nbsp;email: <span style="color: #333; font-style: italic;">debbie@company.com</span>,
    password: <span style="color: #333; font-style: italic;">secret123</span>.
    </p>

    <br />

    <%-- panel allows default button to be set --%>


    <asp:Panel ID="Panel1" DefaultButton="ButtonSubmit" runat="server" Style="padding-left: 40px;">

        <table style="width: 340px;">
            <tr>
                <td style="height: 28px; background-color: #466864; color: #fff;" colspan="2">&nbsp;&nbsp;&nbsp;please login
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 32px;">email:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextboxEmail" runat="server" TextMode="SingleLine" TabIndex="1" Font-Size="11pt"
                        Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">password:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="TextboxPassword" runat="server" TextMode="Password" TabIndex="2" Font-Size="11pt"
                        Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="height: 40px; vertical-align: middle;">
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click"></asp:Button>
                </td>
            </tr>
        </table>

    </asp:Panel>

    <br />

    <span style="color: #f45;">
        <asp:Literal runat="server" ID="LiteralError"></asp:Literal>
    </span>

    <br />
    <br />
    <br />
</asp:Content>

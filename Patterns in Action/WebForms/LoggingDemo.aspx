<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="LoggingDemo.aspx.cs" Inherits="ASPNETWebApplication.LoggingDemo" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

   <h1>Logging Demonstration</h1>

    <p>
     This page demonstrates error logging. &nbsp;Select severity of error you wish to generate:<br /> 
    </p>

    <div style="margin:30px 0 0 30px;">
      
       <asp:RadioButton ID="RadioButtonDebug" runat="server" Text="Debug" GroupName="LogSeverity" /><br />
       <asp:RadioButton ID="RadioButtonInfo" runat="server" Text="Info" GroupName="LogSeverity" /><br />
       <asp:RadioButton ID="RadioButtonWarning" runat="server" Text="Warning" GroupName="LogSeverity" /><br />
       <asp:RadioButton ID="RadioButtonError" runat="server" Text="Error" GroupName="LogSeverity" Checked="true" /><br />
       <asp:RadioButton ID="RadioButtonFatal" runat="server" Text="Fatal" GroupName="LogSeverity" /><br />
       <br />
	   <asp:Button ID="ButtonGenerateError" runat="server" Text="Click here" OnClick="Button_Click" ></asp:Button>
       &nbsp;&nbsp; to generate an exception of the above severity error.
    </div>


</asp:Content>

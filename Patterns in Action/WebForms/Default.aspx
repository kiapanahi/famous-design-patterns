<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <h1>Welcome</h1>

    <%--image--%>

    <div class="floatright" style="text-align: right;">
        <asp:Image ID="ImageDefault" runat="server" ImageUrl="~/images/image45.jpg" Width="181" Height="212" BorderWidth="0" />
    </div>

    <p>
	You are running <i>Patterns in Action 4.5</i> with <b>ASP.NET Web Forms</b>. This app
    demonstrates when, where, and how design patterns are 
	used in a modern multi-layered web application.
	</p>

    <p>
    <i>Patterns in Action 4.5</i> has been built around the most frequently used 
	design patterns and associated best practices. These include GoF patterns 
    and numerous Enterprise Patterns as documented in Martin Fowler's book titled: 
    "Patterns of Enterprise Application Architecture". 
    An MVC version of the same app is also available.
    </p>
    <br />
    <b>Getting Started</b>

    <br />
	<p>
    To familiarize yourself with the functionality of this app begin by selecting the menu items on the left and 
    then explore the different pages. Once you've explored all pages, we suggest you analyze the .NET Solution 
    and Project structure. This will provide a general overview of the architecture, the layers, 
    and some of the major patterns used to build this reference application. 
    </p>
    <p>
    Once you are comfortable with the overall functionality and project structure you'll 
    want to explore the details of the code and patterns used throughout the application.  
    The Visual Studio debugger will be of great help. Start by setting a breakpoint in the appropriate 
    action method in a controller. Then step through all the down to the data access layer and 
    back up just before the View is rendered. 
    </p>
    <p>
    Be sure to also explore the other UI platforms, that is, ASP.NET MVC, 
    Windows Forms and WPF. All of these reside in the folder named 'Presentation Layer'. 
    You select the appropriate UI platform by selecting the 'Set as Startup Project' menu item.
    Please realize that all UI platforms consume the exact same Service, Business, and Data Access layers.
    </p>
    <p><i>Patterns in Action 4.5</i> supports 3 different data access technologies: 
    ADO.NET, Linq2Sql, and Entity Framework. Choose your technology by changing the 
    <i>Provider</i> application setting in web.config. This flexibility is offered courtesy of 
    the Data Access Object pattern.
    </p>
    <p>
    <br />
    <b>Where To Find Documentation</b>
    <br />
    <br />
    Setup, functionality, design, architecture, and design patterns are discussed in
    the accompanying document named: <b>PatternsInAction4.5.pdf</b>. &nbsp;
    Also, the C# and VB source code follows rigorous naming and other conventions and is 
    commented where appropriate. 
    </p>
    <br />
    <b>Next Step: Spark 4.5</b>
    <br />
     <p>
    Upon completing Patterns in Action 4.5 you will be ready to explore <b>Spark 4.5</b>
    which is a pattern-based rapid application development (RAD) platform that allows you 
    to build .NET apps quickly and easily.  
    It uses a lightweight architecture and a limited set of practical, proven patterns and practices. 
    Spark 4.5 embraces <i>convention over configuration</i> which helps to create
    .NET apps that are simple, robust, and fast. Of course, 100% of the source code is included.
    </p>
    <p>
    <b>Art Shop</b> is a reference application built with Spark 4.5. 
    It is a full-stack, real-world e-commerce Web application that allows users to purchase art reproductions by 
    famous artists, such as Van Gogh, Monet, and Vermeer. 
    </p>
    <p>        
    Art Shop is a comprehensive online store with product catalog, shopping cart, 
    administration and reporting areas. Common navigation patterns include: browsing, searching, 
    sorting, filtering, pagination, and master-detail. Security measures include authorization, authentication, 
    OAuth (Facebook), cross-site scripting, and SQL injection. Other features and tools 
    are REST, JavaScript, jQuery, Bootstrap and more. In short, <i>everything</i> you need to build 
    a modern web apps today. 
    </p>
    <br />

    <br />
    <br />
    <br />
    <br />
</asp:Content>

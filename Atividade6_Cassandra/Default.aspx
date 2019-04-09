<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Atividade6_Cassandra._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Atividade 6</h1>
        <p class="lead">Executando a migração de uma base SQL Server para uma base Cassandra e obtendo os valores para gerar um relatório em pdf.</p>
        <%--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>--%>
                    <h3>Digite o número da NF para gerar o relatório:</h3>
            <p> 
                <asp:TextBox class="btn btn-default"  runat="server" ID="nfNumber" TextMode="Number" /> 
                <asp:Button class="btn btn-default" runat="server" ID="executeButton" Text="Exportar PDF" OnClick="executeButton_Click"/>
            </p>

    </div>

    <div class="row">
        <div class="col-md-4">
        </div>
        <!-- div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </!-->
        <!-- div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </!-->
    </div>

</asp:Content>

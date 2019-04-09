<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Atividade6_Cassandra.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Atividade 6 - NoSQL - Big Data - UNIVALI.</h3>
    <p>Emanuel Espíndola - emanuelx@msn.com</p>
    <asp:Button  runat="server" class="btn btn-default" OnClick="Unnamed_Click" Text="Importar dados (Realiza toda a migração do SQLServer para o Cassandra)" />
</asp:Content>

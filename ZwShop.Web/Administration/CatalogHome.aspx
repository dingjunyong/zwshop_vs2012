<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Main.Master" AutoEventWireup="true" 
    CodeBehind="CatalogHome.aspx.cs" Inherits="ZwShop.Web.Administration.CatalogHome" %>
<%@ Register TagPrefix="shop" TagName="CatalogHome" Src="Modules/CatalogHome.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <shop:CatalogHome runat="server" ID="ctrlCatalogHome" />
</asp:Content>

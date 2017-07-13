<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Main.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="ZwShop.Web.Administration.Categories" %>
<%@ Register TagPrefix="shop" TagName="Categories" Src="Modules/Categories.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <shop:Categories runat="server" ID="ctrlCategories" />
</asp:Content>

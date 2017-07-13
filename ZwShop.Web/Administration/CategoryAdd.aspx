<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Main.Master" AutoEventWireup="true" CodeBehind="CategoryAdd.aspx.cs" Inherits="ZwShop.Web.Administration.CategoryAdd" %>
<%@ Register TagPrefix="shop" TagName="CategoryAdd" Src="Modules/CategoryAdd.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <shop:CategoryAdd runat="server" ID="ctrlCategoryAdd" />
</asp:Content>

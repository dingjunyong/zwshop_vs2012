<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Main.Master" AutoEventWireup="true" CodeBehind="CategoryDetails.aspx.cs" Inherits="ZwShop.Web.Administration.CategoryDetails" %>
<%@ Register TagPrefix="shop" TagName="CategoryDetails" Src="Modules/CategoryDetails.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <shop:CategoryDetails runat="server" ID="ctrlCategoryDetails" />
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Administration/Main.Master" AutoEventWireup="true"
     CodeBehind="CustomersHome.aspx.cs" Inherits="ZwShop.Web.Administration.CustomersHome" %>
<%@ Register Src="Modules/CustomersHome.ascx" TagName="CustomersHome" TagPrefix="shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <shop:CustomersHome ID="ctrlCustomersHome" runat="server" />
</asp:Content>
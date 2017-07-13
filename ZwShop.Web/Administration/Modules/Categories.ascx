<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="ZwShop.Web.Administration.Modules.Categories" %>

<div class="section-header">
    <div class="title">
        <img src="Common/ico-catalog.png" alt="" />
        管理分类
    </div>
    <div class="options">
        <input type="button" onclick="location.href = 'CategoryAdd.aspx'" value="添加新分类"
            id="btnAddNew" class="adminButtonBlue" title="添加新分类" />
        <asp:Button runat="server" Text="导出XML" CssClass="adminButtonBlue" ID="btnExportXML"
            OnClick="btnExportXML_Click" ValidationGroup="ExportXML" ToolTip="导出XML" />
    </div>
</div>
<table class="adminContent">
    <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="False" Width="100%"
    OnPageIndexChanging="gvCategories_PageIndexChanging" AllowPaging="true" PageSize="15">
        <Columns>
            <asp:TemplateField HeaderText="分类名称" ItemStyle-Width="55%">
                <ItemTemplate>
                    <%# Server.HtmlEncode(GetCategoryFullName((Category)Container.DataItem))%>
                    <asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Eval("CategoryId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已发布" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <nopCommerce:ImageCheckBox runat="server" ID="cbPublished" Checked='<%# Eval("Published") %>'>
                    </nopCommerce:ImageCheckBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="显示顺序"
                HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDisplayOrder" Text='<%# Eval("DisplayOrder") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="CategoryDetails.aspx?CategoryId=<%#Eval("CategoryId")%>" >编辑
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings PageButtonCount="50" Position="TopAndBottom" />
    </asp:GridView>
</table>

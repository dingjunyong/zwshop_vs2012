<%@ Application  Language="C#" %>

<%@ Import Namespace="ZwShop.WebApi.App_Code" %>

<script RunAt="server">

     

    void Application_BeginRequest(object sender, EventArgs e)
    {
        var app = (HttpApplication)sender;
        ZwShopConfig.Initialize(app.Context);
    }
        
    
</script>

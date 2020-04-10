using ProductManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProductManagement
{
    /// <summary>
    /// 定义权限==》PermissionDefinitionProvider
    /// </summary>
    public class ProductManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //权限组
            var productManagementGroup = context.AddGroup(ProductManagementPermissions.GroupName, L("Permission:ProductManagement"));

            var products = 
                productManagementGroup.AddPermission(ProductManagementPermissions.Products.Default, L("Permission:Products"));

            products.AddChild(ProductManagementPermissions.Products.Update, L("Permission:Edit"));

            products.AddChild(ProductManagementPermissions.Products.Delete, L("Permission:Delete"));

            products.AddChild(ProductManagementPermissions.Products.Create, L("Permission:Create"));

            products.AddChild(ProductManagementPermissions.Products.Audi, L("Permission:Audi"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProductManagementResource>(name);
        }
    }
}
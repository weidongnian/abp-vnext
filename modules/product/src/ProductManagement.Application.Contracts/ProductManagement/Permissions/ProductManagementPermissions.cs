using Volo.Abp.Reflection;

namespace ProductManagement
{
    public class ProductManagementPermissions
    {
        public const string GroupName = "ProductManagement";

        public static class Products
        {
            public const string Default = GroupName + ".Product";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";

            /// <summary>
            /// 审核权限
            /// </summary>
            public const string Audi= Default + ".Audi";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProductManagementPermissions));
        }
    }
}
身份授权认证应用服务
AuthServer.Host
http://10.211.55.11:64999

角色管理服务 微服务
IdentityService.Host
http://10.211.55.11:63568

网关后台服务
BackendAdminAppGateway.Host
http://10.211.55.11:65115


后台应用
BackendAdminApp.Host
http://10.211.55.11:51954


https://www.cnblogs.com/jlion/p/12447081.html

认证分为：1,身份资源(Identity resources)	2,API资源(API resources)

定义资源

/// <summary>
/// Api资源 静态方式定义
/// </summary>
/// <returns></returns>
public static IEnumerable<ApiResource> GetApiResources()
{
       return new List<ApiResource>
       {
            new ApiResource(OAuthConfig.UserApi.ApiName,OAuthConfig.UserApi.ApiName),
       };
}

定义客户端Client

public static IEnumerable<Client> GetClients()
{
       return new List<Client>
       {
           new Client()
           {
               ClientId =OAuthConfig.UserApi.ClientId,
               AllowedGrantTypes = new List<string>()
               {
                   GrantTypes.ResourceOwnerPassword.FirstOrDefault(),//Resource Owner Password模式
               },
               ClientSecrets = {new Secret(OAuthConfig.UserApi.Secret.Sha256()) },
               AllowedScopes= {OAuthConfig.UserApi.ApiName},
               AccessTokenLifetime = OAuthConfig.ExpireIn,
           },
      };
 }

AllowedGrantTypes:配置授权类型，可以配置多个授权类型
ClientSecrets:客户端加密方式
AllowedScopes:配置授权范围，这里指定哪些API 受此方式保护
AccessTokenLifetime:配置Token 失效时间
GrantTypes:授权类型，这里使用的是密码模式,ResourceOwnerPassword

需要把IdentityServer4 注册到DI中：
Startup 中的ConfigureServices

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    #region 内存方式
    services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(OAuthMemoryData.GetApiResources())
        .AddInMemoryClients(OAuthMemoryData.GetClients())
        .AddTestUsers(OAuthMemoryData.GetTestUsers());
    #endregion

}

AddDeveloperSigningCredential:
添加证书加密方式，执行该方法，会先判断tempkey.rsa证书文件是否存在，
如果不存在的话，就创建一个新的tempkey.rsa证书文件，如果存在的话，就使用此证书文件。

AddInMemoryApiResources:把受保护的Api资源添加到内存中

AddInMemoryClients:客户端配置添加到内存中

AddTestUsers:测试的用户添加进来

UseIdentityServer:需要把IdentityServer4 中间件添加到Http管道中

三、MicroserviceDemo微服务架构分析#
abp vNext微服务Demo提供多个公共微服务和一个产品服务，分别是身份授权服务(AuthServer)，后台管理网关服务(BackendAdminAppGateway)，内部网关服务(InternalGateway)，门户网站网关服务(PublicWebSiteGateway)，博客服务(BloggingService)，用户管理服务(IdentityService)，产品服务(ProductService)。在这些服务的基础上可以做各种业务开发，目前vNext的文档还不够完善，建议业务开发暂时集成在产品服务中。下面开始为以上服务分别介绍，MicroserviceDemo中还有其他几个服务，因为用不上所以就不介绍了。

服务分层#
MicroserviceDemo对微服务进行了分层，分为微服务层(microservices)、网关层(gateways)、应用层(applications)和模块层(modules)。

微服务层(microservices)：#
MicroserviceDemo微服务层中提供了三个微服务，分别是：IdentityService（用户）、BloggingService（博客）、ProductService（产品）。IdentityService提供了用户和角色信息管理，但是接口并不在该服务中，vNext将接口实现在底层module中，也就是volo包中，这种实现方式可能在以后的定制开发中有些不灵活。BloggingService服务提供了博客管理，管理接口也不再服务中实现。ProductService服务提供的是产品服务，同样，该服务也没有任何的接口实现，只是将模块层中product模块的接口发布出来，相当于ProductManagement的Host宿主。

网关层(gateways)：#
gateway服务都是网关服务，如果要开发后台管理系统(B端)，只需提供auth-server和backend-adminapp-gateway端口给前端即可。public-website-gateway是门户网站网关服务提供博客和前台(C端)网站统一网关；internal-gateway是内部网关服务提供微服务系统调用，该服务并没有提供api接口，仅仅在系统内部可以调用；backend-adminapp-gateway是后台管理系统网关提供B端系统统一网关。网关的工程结构非常简单，在了解ocelot配置后，对下游服务做好配置就可以使用。

应用层（applications）：#
MicroserviceDemo中还提供了简单的应用服务实现，分别是：AuthServer.Host（身份服务）、BackendAdminApp.Host（后端web应用）、ConsoleClientDemo（控制台应用）、PublicWebSite.Host（公共web应用）。其中AuthServer最重要，也是整个微服务中的核心所在，AuthServer集成了identity server4框架，提供微服务统一的身份授权认证。AuthServer提供了简单的登录页面和跳转服务，但这些都是基于mvc开发，如果想要改为spa开发就要对.net core身份机制和ids4框架十分了解。由于我打算直接改为spa项目，所以后面的应用就不再介绍了，如果对mvc比较有兴趣的可以深入了解。

模块层(modules)#
模块层中是业务代码的实现层，该层中可以根据需要实现各种业务系统，如CRM、OMS、ERP等等。模块层使用的是ddd架构风格，和aspnet zero风格一致，有了ddd的保驾护航，相信后面的业务开发也会非常的顺畅，后面我会介绍模块中的实际业务开发。




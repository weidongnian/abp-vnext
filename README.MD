abp vnext 项目整理

1，依赖 dotnet core 3.1，Volo.Cli

2，依赖 sql server 2012+

3，依赖vs 2019+

4，依赖redis 5.0+(非必选，不用时请注释相关代码)

5，依赖redis 5.0+(非必选，不用时请注释相关代码)

6，依赖RabbitMQ 3.7.11+(可选，不用时请注释相关代码)

7，依赖ElasticSearch 6.6+(可选，不用时请注释相关代码)

8，依赖MongoDB 4.0+(可选，不用时请注释相关代码)

9，依赖Kibana 6.6+ (可选,建议显示日志)

10，进入applications/AuthServer.Host，修改数据库配置，完成数据迁移

	Add-Migration init
	Update-Database 


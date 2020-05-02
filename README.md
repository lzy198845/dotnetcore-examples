# .NET Core  学习示例
> 学习之路漫漫无期

看到java 的spring boot 框架如何火热，examples - [https://github.com/ityouknow/spring-boot-examples](https://github.com/ityouknow/spring-boot-examples)多达16628 star（2019-6-27），回头看dotnetcore，则不温不火。我想写点示例，降低入门门槛。

## 关于此项目的文档 [https://luoyunchong.github.io/vuepress-docs/dotnetcore/examples/](https://luoyunchong.github.io/vuepress-docs/dotnetcore/examples/)

假设你已经有了C#基础、ASP .NET MVC或其他语言的MVC基础。 

本项目以C#语言为示例，结合 ASP .NET Core，集成第三方类库的示例，运用基础组件，写好Demo。如果你是一个 .NET Framework开发者，转去学习 .NET Core,你会发现新的世界，我给自己的定位是软件开发工程师，而不只是 .NET 开发工程师，结合其他牛比的技术才能共赢。


## 我正在学习和使用的技术、关注的技术
- Linux：Ubuntu
- CLI：PowerShell、Bash
- Docker:Docker for windows、Hyper-v、WSL2
- DevOps:Jenkins、Travis CI、Aurze DevOps
- MySQL、Mariadb
- NoSQL：Redis、MongoDB
- Nginx、
- .NET Core、ASP.NET Core
- RabbitMQ
- SignlaR

## 关注的开源组织

- dotnetcore :.NET Core Community
    - 官网 [https://www.dotnetcore.xyz](https://www.dotnetcore.xyz)
    - 开源 [https://github.com/dotnetcore](https://github.com/dotnetcore)
    - 21个开源项目，都是基于dotnetcore开源的优秀项目。
- abpframework：Web Application Framework for ASP .NET Core 
    - 官网 https://abp.io/
    - 开源地址 https://github.com/abpframework
    - abp vnext 完善的基础设施与文档  https://github.com/abpframework/abp
- surging-cloud:
    - 开源地址 https://github.com/surging-cloud
    - 基于Surging框架实现的权限管理系统 https://github.com/surging-cloud/Surging.Hero
    - 微服务引擎：https://github.com/dotnetcore/surging

## 要集成的类库
| 基础类库集成方案                                                                                                                                                                      | 开源地址                                                                                            | 文档                                                                        | 说明                                                                                              |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| [FreeSql](https://github.com/luoyunchong/dotnetcore-examples/tree/master/aspnetcore-freesql)                                                                                          | [GitHub](https://github.com/2881099/FreeSql)                                                        | [wiki](https://github.com/2881099/FreeSql/wiki)                             | O/RM 支持code/db first,轻量级、高性能、数据访问技术                                               |
| [csredis](https://github.com/luoyunchong/dotnetcore-examples/tree/master/dotnet-core-redis)                                                                                           | [GitHub](https://github.com/2881099/csredis)                                                        | 看README                                                                    | redis、高性能、分区、集群、哨兵                                                                   |
| [StackExchange.Redis](https://github.com/luoyunchong/dotnetcore-examples/tree/master/dotnet-core-redis)                                                                               | [GitHub](https://github.com/StackExchange/StackExchange.Redis)                                      | [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/) | redis、良好的文档、stackoverflow出品                                                              |
| [WebApiClient](https://github.com/luoyunchong/dotnetcore-examples/tree/master/dotnet-core-webapiclient)                                                                               | [GitHub](https://github.com/dotnetcore/WebApiClient)                                                | [WIKI](https://github.com/dotnetcore/WebApiClient/wiki)                     | HTTPAPI、base on httpclient、使用简单                                                             |
| [EntityFrameworkCore](https://github.com/luoyunchong/dotnetcore-examples/tree/master/dotnet-core-efcore)                                                                              | [GitHub](https://github.com/aspnet/EntityFrameworkCore)                                             | [docs](https://docs.microsoft.com/ef/core)                                  | O/RM 支持code/db first、轻量化、可扩展、数据访问技术                                              |
| [Qiniu云对象存储](https://github.com/luoyunchong/dotnetcore-examples/tree/master/aspnetcore-qiniu)                                                                                    | [.net](https://github.com/qiniu/csharp-sdk)/[.net core](https://github.com/Hello-Mango/MQiniu.Core) | [c# sdk](https://developer.qiniu.com/kodo/sdk/1237/csharp)                  | 由于官网未支持. net core，所以 大家看[社区版解决方案](https://github.com/Hello-Mango/MQiniu.Core) |
| [ImCore通讯组件](https://github.com/luoyunchong/dotnetcore-examples/tree/master/dotnet-core-im)                                                                                       | [GitHub](https://github.com/2881099/im)                                                             | 看README                                                                    | 基于webSocket 协议实现简易、高性能、集群即时通讯组件                                              |
| [ToolGood.Words](https://github.com/luoyunchong/dotnetcore-examples/blob/7b01de64b8/aspnetcore-%E6%95%8F%E6%84%9F%E8%AF%8D%E5%A4%84%E7%90%86/StopWords/Controllers/WordController.cs) | [GitHub](https://github.com/toolgood/ToolGood.Words)                                                | 官网README                                                                  | 一款高性能非法词(敏感词)检测组件                                                                  |

## 与该项目相关
### Freesql
* [FreeSql在ASP.NTE Core WebApi中如何使用的教程](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-aspnetcore-how-to-use/)
* [使用RESTful、FreeSql构建简单的博客系统-集成AutoMapper](https://blog.igeekfan.cn/2019/06/30/re-start/FreeSql-sample-blog-RESTful/)
### csredis
* [csredis-in-asp.net core理论实战-主从配置、哨兵模式](https://blog.igeekfan.cn/2019/07/06/re-start/csredis-in-asp-net-core-master-slaver/)
* [csredis-in-asp.net-core理论实战-使用示例](https://blog.igeekfan.cn/2019/07/07/re-start/csredis-in-aspnetcore-how-to-use/)

### 配置项
* 配置项[aspnetcore-Get-Json-Array-using-IConfiguration](https://blog.igeekfan.cn/2019/07/07/dotnetcore/aspnetcore-Get-Json-Array-using-IConfiguration/)

### ASP.NET Core
* [ASP.NET Core 集成七牛云对象存储](https://blog.igeekfan.cn/2019/07/28/dotnetcore/Qiniu-Object-Storage/)

using System;

namespace AddressBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        /*public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddLog4Net("log4net.config");
                    builder.SetMinimumLevel(LogLevel.Trace);
                });*/
    }
}

/* 
   crit: Microsoft.AspNetCore.Hosting.Diagnostics[6]
      Application startup exception
      System.ArgumentException: There is an incomplete parameter in the route template. Check that each '{' character has a matching '}' character. (Parameter 'routeTemplate')
       ---> Microsoft.AspNetCore.Routing.Patterns.RoutePatternException: There is an incomplete parameter in the route template. Check that each '{' character has a matching '}' character.
         at Microsoft.AspNetCore.Routing.Patterns.RoutePatternParser.Parse(String pattern)
         at Microsoft.AspNetCore.Routing.Patterns.RoutePatternFactory.Parse(String pattern)
         at Microsoft.AspNetCore.Routing.Template.TemplateParser.Parse(String routeTemplate)
         --- End of inner exception stack trace ---
         at Microsoft.AspNetCore.Routing.Template.TemplateParser.Parse(String routeTemplate)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.ParameterExistsInAnyRoute(ActionModel action, String parameterName)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.InferBindingSourceForParameter(ParameterModel parameter)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.InferParameterBindingSources(ActionModel action)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.Apply(ActionModel action)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.ApiBehaviorApplicationModelProvider.OnProvidersExecuting(ApplicationModelProviderContext context)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModelFactory.CreateApplicationModel(IEnumerable`1 controllerTypes)
         at Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider.GetDescriptors()
         at Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider.OnProvidersExecuting(ActionDescriptorProviderContext context)
         at Microsoft.AspNetCore.Mvc.Infrastructure.DefaultActionDescriptorCollectionProvider.UpdateCollection()
         at Microsoft.AspNetCore.Mvc.Infrastructure.DefaultActionDescriptorCollectionProvider.Initialize()
         at Microsoft.AspNetCore.Mvc.Routing.ActionEndpointDataSourceBase.<>c__DisplayClass11_0.<Subscribe>b__0()
         at Microsoft.Extensions.Primitives.ChangeToken.OnChange(Func`1 changeTokenProducer, Action changeTokenConsumer)         at Microsoft.AspNetCore.Mvc.Routing.ActionEndpointDataSourceBase.Subscribe()
         at Microsoft.AspNetCore.Mvc.Routing.ControllerActionEndpointDataSource..ctor(ControllerActionEndpointDataSourceIdProvider dataSourceIdProvider, IActionDescriptorCollectionProvider actions, ActionEndpointFactory endpointFactory, OrderedEndpointsSequenceProvider orderSequence)
         at Microsoft.AspNetCore.Builder.ControllerEndpointRouteBuilderExtensions.GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
         at Microsoft.AspNetCore.Builder.ControllerEndpointRouteBuilderExtensions.MapControllers(IEndpointRouteBuilder endpoints)
         at AddressBook.Startup.<>c.<Configure>b__5_0(IEndpointRouteBuilder endpoint) in D:\VSasp.netcorecode\AddressBookAssignment\AddressBook\AddressBook\Startup.cs:line 99
         at Microsoft.AspNetCore.Builder.EndpointRoutingApplicationBuilderExtensions.UseEndpoints(IApplicationBuilder builder, Action`1 configure)
         at AddressBook.Startup.Configure(IApplicationBuilder app, IWebHostEnvironment env) in D:\VSasp.netcorecode\AddressBookAssignment\AddressBook\AddressBook\Startup.cs:line 97
         at System.RuntimeMethodHandle.InvokeMethod(Object target, Span`1& arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)
         at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
         at Microsoft.AspNetCore.Hosting.ConfigureBuilder.Invoke(Object instance, IApplicationBuilder builder)
         at Microsoft.AspNetCore.Hosting.ConfigureBuilder.<>c__DisplayClass4_0.<Build>b__0(IApplicationBuilder builder)
         at Microsoft.AspNetCore.Hosting.GenericWebHostBuilder.<>c__DisplayClass15_0.<UseStartup>b__1(IApplicationBuilder app)
         at Microsoft.AspNetCore.Mvc.Filters.MiddlewareFilterBuilderStartupFilter.<>c__DisplayClass0_0.<Configure>g__MiddlewareFilterBuilder|0(IApplicationBuilder builder)
         at Microsoft.AspNetCore.HostFilteringStartupFilter.<>c__DisplayClass0_0.<Configure>b__0(IApplicationBuilder app)
         at Microsoft.AspNetCore.Hosting.GenericWebHostService.StartAsync(CancellationToken cancellationToken)
Unhandled exception. System.ArgumentException: There is an incomplete parameter in the route template. Check that each '{' character has a matching '}' character. (Parameter 'routeTemplate')
 ---> Microsoft.AspNetCore.Routing.Patterns.RoutePatternException: There is an incomplete parameter in the route template. Check that each '{' character has a matching '}' character.
   at Microsoft.AspNetCore.Routing.Patterns.RoutePatternParser.Parse(String pattern)
   at Microsoft.AspNetCore.Routing.Patterns.RoutePatternFactory.Parse(String pattern)
   at Microsoft.AspNetCore.Routing.Template.TemplateParser.Parse(String routeTemplate)
   --- End of inner exception stack trace ---
   at Microsoft.AspNetCore.Routing.Template.TemplateParser.Parse(String routeTemplate)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.ParameterExistsInAnyRoute(ActionModel action, String parameterName)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.InferBindingSourceForParameter(ParameterModel parameter)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.InferParameterBindingSources(ActionModel action)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.InferParameterBindingInfoConvention.Apply(ActionModel action)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.ApiBehaviorApplicationModelProvider.OnProvidersExecuting(ApplicationModelProviderContext context)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.ApplicationModelFactory.CreateApplicationModel(IEnumerable`1 controllerTypes)
   at Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider.GetDescriptors()
   at Microsoft.AspNetCore.Mvc.ApplicationModels.ControllerActionDescriptorProvider.OnProvidersExecuting(ActionDescriptorProviderContext context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.DefaultActionDescriptorCollectionProvider.UpdateCollection()
   at Microsoft.AspNetCore.Mvc.Infrastructure.DefaultActionDescriptorCollectionProvider.Initialize()
   at Microsoft.AspNetCore.Mvc.Routing.ActionEndpointDataSourceBase.<>c__DisplayClass11_0.<Subscribe>b__0()
   at Microsoft.Extensions.Primitives.ChangeToken.OnChange(Func`1 changeTokenProducer, Action changeTokenConsumer)
   at Microsoft.AspNetCore.Mvc.Routing.ActionEndpointDataSourceBase.Subscribe()
   at Microsoft.AspNetCore.Mvc.Routing.ControllerActionEndpointDataSource..ctor(ControllerActionEndpointDataSourceIdProvider dataSourceIdProvider, IActionDescriptorCollectionProvider actions, ActionEndpointFactory endpointFactory, OrderedEndpointsSequenceProvider orderSequence)
   at Microsoft.AspNetCore.Builder.ControllerEndpointRouteBuilderExtensions.GetOrCreateDataSource(IEndpointRouteBuilder endpoints)
   at Microsoft.AspNetCore.Builder.ControllerEndpointRouteBuilderExtensions.MapControllers(IEndpointRouteBuilder endpoints)
   at AddressBook.Startup.<>c.<Configure>b__5_0(IEndpointRouteBuilder endpoint) in D:\VSasp.netcorecode\AddressBookAssignment\AddressBook\AddressBook\Startup.cs:line 99
   at Microsoft.AspNetCore.Builder.EndpointRoutingApplicationBuilderExtensions.UseEndpoints(IApplicationBuilder builder, Action`1 configure)
   at AddressBook.Startup.Configure(IApplicationBuilder app, IWebHostEnvironment env) in D:\VSasp.netcorecode\AddressBookAssignment\AddressBook\AddressBook\Startup.cs:line 97
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Span`1& arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)
   at System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at Microsoft.AspNetCore.Hosting.ConfigureBuilder.Invoke(Object instance, IApplicationBuilder builder)
   at Microsoft.AspNetCore.Hosting.ConfigureBuilder.<>c__DisplayClass4_0.<Build>b__0(IApplicationBuilder builder)
   at Microsoft.AspNetCore.Hosting.GenericWebHostBuilder.<>c__DisplayClass15_0.<UseStartup>b__1(IApplicationBuilder app)   at Microsoft.AspNetCore.Mvc.Filters.MiddlewareFilterBuilderStartupFilter.<>c__DisplayClass0_0.<Configure>g__MiddlewareFilterBuilder|0(IApplicationBuilder builder)
   at Microsoft.AspNetCore.HostFilteringStartupFilter.<>c__DisplayClass0_0.<Configure>b__0(IApplicationBuilder app)
   at Microsoft.AspNetCore.Hosting.GenericWebHostService.StartAsync(CancellationToken cancellationToken)
   at Microsoft.Extensions.Hosting.Internal.Host.StartAsync(CancellationToken cancellationToken)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.RunAsync(IHost host, CancellationToken token)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.RunAsync(IHost host, CancellationToken token)
   at Microsoft.Extensions.Hosting.HostingAbstractionsHostExtensions.Run(IHost host)
   at AddressBook.Program.Main(String[] args) in D:\VSasp.netcorecode\AddressBookAssignment\AddressBook\AddressBook\Program.cs:line 9
 
*/
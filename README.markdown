FuncCatalog Extension for MEF
======================================================================

This is a port from [Glen Block's old FuncCatalog for MEF](https://skydrive.live.com/self.aspx/blog/FuncCatalogExtensions.zip?cid=f8b2fd72406fb218&sc=documents) to .NET 4.0. It was originally written for a preview release of MEF and did not compile in .NET 4.0. I fixed it, because it is one building block to be able to use MEF more like a traditional IoC container. (Yeah wouldn't it be cool if .NET had a full IoC container built in?)  

# What does it do?

This is nothing more than a custom `ComposablePartCatalog` called the `FuncCatalog`. Instead of configuring parts with attributes you can use `Func<T>` approach known form many other IoC containers such as [Funq](http://funq.codeplex.com/). Your parts stay POCOs:

    var funcCatalog = new FuncCatalog();

    // whenever a new ILogger is requested a new ConsoleLogger will be returned
    funcCatalog.AddPart<ILogger>(ep => new ConsoleLogger());

The `ExportProvider` is also passed to the Func so you can do:

    // everything is composed together with constructor injection
    funcCatalog.AddPart<ILogAnalyzer>(new DefaultLogAnalyzer(ep => ep.GetExportedValue<ILogger>());
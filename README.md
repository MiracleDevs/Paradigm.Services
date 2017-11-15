[![Build Status](https://travis-ci.org/MiracleDevs/Paradigm.Services.svg?branch=master)](https://travis-ci.org/MiracleDevs/Paradigm.Services)


| Library    | Nuget | Install
|-|-|-|
| CLI | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.CLI/) | `Install-Package Paradigm.Services.CLI` |
| DependencyInjection.Extensions | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.DependencyInjection.Extensions/) | `Install-Package Paradigm.Services.DependencyInjection.Extensions` |
| DependencyInjection.Extensions.ORM | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.DependencyInjection.Extensions.ORM/) | `Install-Package Paradigm.Services.DependencyInjection.Extensions.ORM` |
| Mapping.Extensions | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Mapping.Extensions/) | `Install-Package Paradigm.Services.Mapping.Extensions` |
| Domain | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Domain/) | `Install-Package Paradigm.Services.Domain` |
| Exceptions | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Exceptions/) | `Install-Package Paradigm.Services.Exceptions` |
| Exceptions.SqlServer | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Exceptions.SqlServer/) | `Install-Package Paradigm.Services.Exceptions.SqlServer` |
| Interfaces | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Interfaces/) | `Install-Package Paradigm.Services.Interfaces` |
| Mvc | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Mvc/) | `Install-Package Paradigm.Services.Mvc` |
| Providers | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Providers/) | `Install-Package Paradigm.Services.Providers` |
| Repositories | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Repositories/) | `Install-Package Paradigm.Services.Repositories` |
| StateMachines | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.StateMachines/) | `Install-Package Paradigm.Services.StateMachines` |
| WorkingTasks | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.WorkingTasks/) | `Install-Package Paradigm.Services.WorkingTasks` |
| Repositories.EntityFramework | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Repositories.EntityFramework/) | `Install-Package Paradigm.Services.Repositories.EntityFramework` |
| WorkingTasks.EntityFramework | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.WorkingTasks.EntityFramework/) | `Install-Package Paradigm.Services.WorkingTasks.EntityFramework` |
| Mvc.ORM | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Mvc.ORM/) | `Install-Package Paradigm.Services.Mvc.ORM` |
| Repositories.ORM | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.Repositories.ORM/) | `Install-Package Paradigm.Services.Repositories.ORM` |
| WorkingTasks.ORM | [![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/Paradigm.Services.WorkingTasks.ORM/) | `Install-Package Paradigm.Services.WorkingTasks.ORM` |

# Paradigm.Services
Base libraries for service and webapi projects, containing support for different data sources, state machines and more.


Change log
---

Version `2.0.14`
- Updated nuget dependencies.
- Modified the `ArgumentParser` to allow not nullable types to receive default values if no value was provided in the command line.
- Added new tests for the new functionality.


Version `2.0.13`
- Updated nuget dependencies.
- Removed dependencies on a parameterless constructor for domain entities.


Version `2.0.12`
- Registers the Argument class in the service collection as a singleton if the `ParseArguments<T>` method
  was called. The method should be called before the `UseStartup` or the arguments won't be registered.
- Added more tests.


Version `2.0.11`
- Removed IStartup interface. Now startup classes don't require implemeting interfaces,
  the ConsoleHost will reflect the class to call the correct methods:
  - `Startup()`
  - `Startup(IConfiguration configuration)`
  - `Startup(IConfigurationRoot configuration)`
  - `void ConfigureServices(IServiceCollection services)`
  - `void Run(IServiceProvider provider)`
  - `Task Run(IServiceProvider provider)`
- Argument parsing is done on ParseArguments method, before was done on Run before executing run method.
- Added new `ConsoleHost` tests.
- Updated `ArgumentParser` tests.
- Configured test suite to travis file.
- Fixed tests not testing some cases and giving false positives.


Version `2.0.10`
- Fixed errors when parsing timespans, guids, etc.
- Fixed tests not testing some cases and giving false positives.


Version `2.0.9`
- Improved `Paradigm.Services.CLI` argument parsing. Separated the logic to a new class,
  and added tests. Now the parser supports parameters of type:
  - `byte`
  - `ushort`
  - `uint`
  - `ulong`
  - `sbyte`
  - `short`
  - `int`
  - `long`
  - `float`
  - `double`
  - `decimal`
  - `DateTime`
  - `TimeSpan`
  - `DateTimeOffset`
  - `Guid`
  - `Enums`

  For all the types, the parser also supports nullable versions, and list of type or nullable types.
  To support list types, remember to set the MultipleValue enumeration to the attribute.
- Added default values to nullable parameters.
- Changed working tasks when repeating indefinitely to not store exceptions. When using repeat=-1, the
  system will continue retrying the task until success, so no exception will be thrown. If we store them,
  we can virtually store exceptions consuming unnecessary memory.


Version `2.0.8`
- Added a validation when parsing command line parameters to check if the argument allows null or not, and throw a clear error if an argument is missing.
- Updated nuget dependencies.


Version `2.0.7`
- Changed the dependency registration to include the entry assembly in the lookup.
- Added a new method than can receive multiple assemblies as parameters.


Version `2.0.6`
- Added new method to register transactional working tasks.
- Changed the default behavior when registering types, to use the entry assembly instead of the calling assembly.
- Changed the default behavior when registering mappings, to use the entry assembly instead of the calling assembly.
- Changed how database access are registered, and added a new registration:
  - ICustomDatabaseAccess => CustomDatabaseAccess
  - IDatabaseAccess<Custom> => CustomDatabaseAccess
  - CustomDatabaseAccess => CustomDatabaseAccess


Version `2.0.5`
- Updated nuget dependencies.
- Changed the name of Use** methods by Add** when registering dependency injection container.
- Added new `serviceCollection.AddExceptionHandler()` method.
- Moved service collection extensions to separate methods than can be used in both mvc and console projects.
- Added a new extension method to automatically register mappings.
- Added new `Paradigm.Services.CLI` project with classes that help when working with console apps.
  This library imitates the program/startup approach of mvc applications, allowing to add configuration,
  command line arguments and more.
- Changed providers to use the service provider to instantiate new entities before trying to use the activator.
  This will allow for better DDD, allowing custom entity constructors with injection dependency.
  This will allow domain entities to receive repositories as injection parameters.


Version `2.0.4`
- Added new methods to DomainBase:
  - BeforeSave: Method called before the entity is added or edited.
  - AfterSave: Method called after the entity has been added or edited.

- Added new methods to EditProviderBase:
  - BeforeSave, BeforeSaveAsync: Methods called before the entity is added or edited.
  - AfterSave, AfterSaveAsync: Methods called after the entity has been added or edited.

  Is also worth mentioning that both async and sync methods are called in async and sync conditions.

- Added a new edit provider that works only with a domain entity instead of having both domain and view. This ideal for simple case scenarios, or when using cassandra connector without materialized views.

- Added new helper methods that populates the service collection with all the user classes
  that inherits from the framework types:
  - `serviceCollection.UseDomain()`
  - `serviceCollection.UseDatabaseAccess()`
  - `serviceCollection.UseDatabaseReaderMappers()`
  - `serviceCollection.UseStoredProcedures()`
  - `serviceCollection.UseRepositories()`
  - `serviceCollection.UseProviders()`
  - `serviceCollection.UseWorkingTasks()`
  - `serviceCollection.UseUnitOfWork()`

- Added a helper method that includes all the methods above:
  - `serviceCollection.UserParadigmFramework()`

Version `2.0.3`
- Changed the ORM middleware to use configuration instead of configuration root,
  and added specific exceptions.

Version `2.0.2`
- Changed middleware due to problems utilizing the core interface.
- Fixed an error with an static method calling the wrong middleware.

Version `2.0.1`
- Updated Paradigm.Core to version `2.0.1`.

Version `2.0.0`
- Updated .net core from version 1 to version 2.

Version `1.0.0`
- Uploaded first version of the Paradigm Service Libraries.


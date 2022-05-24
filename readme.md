# Funcky.EntityFrameworkCore

[![Build](https://github.com/polyadic/funcky-efcore/workflows/Build/badge.svg)](https://github.com/polyadic/funcky-efcore/actions?query=workflow%3ABuild)
[![Nuget](https://img.shields.io/nuget/v/Funcky.EntityFrameworkCore)](https://www.nuget.org/packages/Funcky.EntityFrameworkCore/)
[![Changelog: changelog.md](https://img.shields.io/badge/changelog-changelog.md-orange)](./changelog.md)
[![Licence: MIT](https://img.shields.io/badge/licence-MIT-green)](https://raw.githubusercontent.com/polyadic/funcky-efcore/master/LICENSE-MIT)
[![Licence: Apache](https://img.shields.io/badge/licence-Apache-green)](https://raw.githubusercontent.com/polyadic/funcky-efcore/master/LICENSE-Apache)

## Features
### Extension Methods for `IQueryable<T>`
This package provides the extension methods `FirstOrNoneAsync`, `LastOrNoneAsync` and `SingleOrNoneAsync` for EF Core's `IQueryable<T>` objects.
These are analogous to the ones provided by [`EntityFrameworkQueryableExtensions`].

Example:
```csharp
using System;
using Funcky.Extensions;
using Microsoft.EntityFrameworkCore;

DbContext context;
var firstPerson = await context.Set<Person>().FirstOrNoneAsync();
firstPerson.AndThen(p => Console.WriteLine($"Hello {p.FirstName}!"));
```

[`EntityFrameworkQueryableExtensions`]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions?view=efcore-5.0

# Kentico Cloud Personalization .NET SDK

[![Build status](https://ci.appveyor.com/api/projects/status/bbn6iy1yok766yux/branch/master?svg=true)](https://ci.appveyor.com/project/kentico/personalization-sdk-net/branch/master)
[![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization) KenticoCloud.Personalization
[![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.AspNetCore.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization.AspNetCore) KenticoCloud.Personalization.AspNetCore
[![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.MVC.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization.MVC) KenticoCloud.Personalization.MVC

## Summary

The Kentico Cloud Personalization .NET SDK is a library used for retrieving personalization information (such as visitor's first visit, activity on a website, etc.) from [Kentico Cloud](https://kenticocloud.com/). You can use the SDK in the form of a [NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization).

## Prerequisities

To retrieve data from Kentico Cloud via the Personalization API, you need to have a Kentico Cloud subscription at <https://app.kenticocloud.com>. For more information see our [documentation](http://help.kenticocloud.com/).

## Basic scenarios

### Create PersonalizationClient instance

The **PersonalizationClient** class in the `KenticoCloud.Personalization` assembly is the main class of the SDK that enables you to query the API. To create an instance of the class, you need to provide your **personalization API key**. You can find the key in the Development section of Kentico Cloud.

### Getting UserID and SessionID

_User ID_ and _Session ID_ of current visitor are saved in cookie. We are providing methods for retrieving these values.

If you have ASP.NET MVC application use the [KenticoCloud.Personalization.MVC NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization.MVC) which contains extension methods for `HttpRequestBase` object, which is generally available in ASP.NET MVC applications.

If you have ASP.NET Core application use the [KenticoCloud.Personalization.AspNetCore NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization.AspNetCore) which contains extension methods for `HttpRequest` object, which is generally available in ASP.NET Core applications.

### Basic querying examples

Once you create the `PersonalizationClient` instance, you can start querying the Personalization API by calling methods on the class instance.

```C#
var client = new PersonalizationClient("eyJh...5cCI");
var location = await client.GetVisitorUsualLocationAsync("0f2a1fa152b8e92d");
```

```C#
var client = new PersonalizationClient("eyJh...5cCI");
var visit = await client.GetFirstVisitAsync("0f2a1fa152b8e92d");
```

```C#
var client = new PersonalizationClient("eyJh...5cCI");
var session = await client.GetCurrentSessionAsync("0f2a1fa152b8e92d", "8d532785326b0258");
```

### Example of use in MVC application

```C#
using System.Threading.Tasks;
using System.Web.Mvc;
using KenticoCloud.Personalization;
using KenticoCloud.Personalization.MVC;

namespace SDKWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly PersonalizationClient PersonalizationClient = new PersonalizationClient("");

        public async Task<ActionResult> Index()
        {
            var uid = this.Request.GetCurrentPersonalizationUid();
            var sid = this.Request.GetCurrentPersonalizationSid();
            var session = await PersonalizationClient.GetCurrentSessionAsync(uid, sid);
            ViewBag.PersonalizationSessionOrigin = session.Origin;
            return View();
        }
    }
}
```

# Kentico Cloud Personalization .NET SDK

The Kentico Cloud Personalization .NET SDK is a library used for retrieving personalization information from Kentico Cloud. You can use SDK in the form of a [NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization).

## Prerequisities

To retrieve content from the Kentico Cloud via the Personalization API, you need to have a Kentico Cloud subscription at [https://app.kenticocloud.com](https://app.kenticocloud.com). For more information see our [documentation](https://kenticocloud.com/docs).

## Basic scenarios

### Create PersonalizationClient instance

**PersonalizationClient** in KenticoCloud.Personalization assembly is the main class of the SDK that enables you to query the API. To create the instance you need to provide your **personalization API key**. Key can be found on the Kentico Cloud application's Development page.

### Getting UserID and SessionID

There are extension methods in KenticoCloud.Personalization.MVC assembly which are adding functionality for getting UserID and SessionID from HttpRequestBase object. This object is available in ASP.NET applications. You can use this assembly in the form of a [NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization.MVC).

### Basic querying examples

Once you have the PersonalizationClient instance, you can start querying personalization API by calling methods on the instance.

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

### Example for MVC application.

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

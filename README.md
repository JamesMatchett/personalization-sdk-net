# Engage .NET SDK

Kentico Engage .NET SDK is a library used for retrieving personalization information from Kentico Engage API from your .NET applications.

## Prerequisities

In order to retrieve the content from the Kentico Engage API, you need to have a Engage subscription at [https://app.kenticocloud.com](https://app.kenticocloud.com). Please read [https://kenticocloud.com/docs](https://kenticocloud.com/docs) for more information.

## Installation

SDK at NuGet:

    Install-Package KenticoCloud.Engage

MVC helpers at NuGet:

    Install-Package KenticoCloud.Engage.MVC

## Usage

### Create EngageClient instance

**EngageClient** in KenticoCloud.Engage assembly is the main class of the SDK that enables you to query the API. To create the instance you need to provide your **personalization API key**. Key can be found on the [Engage settings page](https://engage.kenticocloud.com/Home/Settings). 

### Getting UserID and SessionID

There are extension methods in KenticoCloud.Engage.MVC assembly which are adding functionality for getting UserID and SessionID from HttpRequestBase object. This object is available in ASP.NET applications.

### Basic querying examples

Once you have the EngageClient instance, you can start querying personalization API by calling methods on the instance.

```C#
var client = new EngageClient("eyJh...5cCI");
var location = await client.GetVisitorUsualLocationAsync("0f2a1fa152b8e92d");
```

```C#
var client = new EngageClient("eyJh...5cCI");
var visit = await client.GetFirstVisitAsync("0f2a1fa152b8e92d");
```

```C#
var client = new EngageClient("eyJh...5cCI");
var session = await client.GetCurrentSessionAsync("0f2a1fa152b8e92d", "8d532785326b0258");
```

### Example for MVC application.

```C#
using System.Threading.Tasks;
using System.Web.Mvc;
using KenticoCloud.Engage;
using KenticoCloud.Engage.MVC;

namespace SDKWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly EngageClient engageClient = new EngageClient("");

        public async Task<ActionResult> Index()
        {
            var uid = this.Request.GetCurrentEngageUid();
            var sid = this.Request.GetCurrentEngageSid();
            var session = await engageClient.GetCurrentSessionAsync(uid, sid);
            ViewBag.EngageSessionOrigin = session.Origin;
            return View();
        }
    }
}
```

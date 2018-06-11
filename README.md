# Kentico Cloud Personalization .NET SDK

[![Build status](https://ci.appveyor.com/api/projects/status/bbn6iy1yok766yux/branch/master?svg=true)](https://ci.appveyor.com/project/kentico/personalization-sdk-net/branch/master) 
[![Forums](https://img.shields.io/badge/chat-on%20forums-orange.svg)](https://forums.kenticocloud.com)
[![Analytics](https://ga-beacon.appspot.com/UA-69014260-4/Kentico/personalization-sdk-net?pixel)](https://github.com/igrigorik/ga-beacon)


| Package | Version |
| :-- | :--: |
| KenticoCloud.Personalization | [![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization) |
| KenticoCloud.Personalization.AspNetCore | [![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.AspNetCore.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization.AspNetCore) |
| KenticoCloud.Personalization.MVC | [![NuGet](https://img.shields.io/nuget/v/KenticoCloud.Personalization.MVC.svg)](https://www.nuget.org/packages/KenticoCloud.Personalization.MVC) |


## Summary

The Kentico Cloud Personalization .NET SDK is a library used for retrieving personalization information (such as visitor's first visit, activity on a website, etc.) from [Kentico Cloud](https://kenticocloud.com/). You can use the SDK in the form of a [NuGet package](https://www.nuget.org/packages/KenticoCloud.Personalization).

## Prerequisites

To retrieve data from Kentico Cloud via the Personalization API, you need to have a Kentico Cloud subscription at <https://app.kenticocloud.com>. For more information see our [documentation](http://help.kenticocloud.com/).

## Basic personalization scenarios

### Create PersonalizationClient instance

The **PersonalizationClient** class in the `KenticoCloud.Personalization` assembly is the main class of the SDK that enables you to query the API. To create an instance of the class, you need to provide your **personalization API key**. You can find the key in the API Keys section of Kentico Cloud.

### Getting UserID and SessionID

_User ID_ and _Session ID_ identify a specific visitor and his current session. These values are saved in a cookie and we provide methods to retrieve them in code.

* If you have an ASP.NET MVC application, use the [KenticoCloud.Personalization.MVC](https://www.nuget.org/packages/KenticoCloud.Personalization.MVC) NuGet package. This package provides extension methods for the `HttpRequestBase` object, which is generally available in ASP.NET MVC applications.
* If you have an ASP.NET Core application, use the [KenticoCloud.Personalization.AspNetCore](https://www.nuget.org/packages/KenticoCloud.Personalization.AspNetCore) NuGet package. This package provides extension methods for the `HttpRequest` object, which is generally available in ASP.NET Core applications.

### Basic querying examples

Once you create the `PersonalizationClient` instance, you can start querying the Personalization API by calling methods on the class instance.

```C#
// Retrieves segments of a visitor
var client = new PersonalizationClient("<YOUR_API_KEY>", new Guid("<YOUR_PROJECT_ID>"));
var visitorSegments = await client.GetVisitorSegmentsAsync("<USER_ID>");
```

```C#
// Retrieves all visitors belonging to a segment
var client = new PersonalizationClient("<YOUR_API_KEY>", new Guid("<YOUR_PROJECT_ID>"));
var segmentVisitors = await client.GetVisitorsInSegmentAsync("<SEGMENT_CODENAME>");
```

### Example – use in ASP.NET MVC applications

The following example shows how you can use the Personalization API in an ASP.NET MVC application to find out which segments the current visitor belongs to. This information can then be used to personalize content shown to the visitor.

```C#
using System.Threading.Tasks;
using System.Web.Mvc;
using KenticoCloud.Personalization;
using KenticoCloud.Personalization.MVC;

namespace DancingGoat.Controllers
{
    public class HomeController : Controller
    {
        // Initializes an instance of the PersonalizationClient class
        var client = new PersonalizationClient("<YOUR_API_KEY>", new Guid("<YOUR_PROJECT_ID>"));

        public async Task<ActionResult> Index()
        {
            // Retrieves User ID of the current visitor
            var uid = this.Request.GetCurrentPersonalizationUid();

            // Retrieves segments of the visitor
            SegmentsResponse response = await client.GetVisitorSegmentsAsync(uid);
            ViewBag.VisitorSegments = response.segments;

            return View();
        }
    }
}
```

See [Personalizing content](https://developer.kenticocloud.com/docs/personalizing-content) in our DevHub for a more detailed explanation of delivering different content to different visitors.

## Actively tracking visitors

Our [Tracking API](https://developer.kenticocloud.com/reference#tracking-api-beta) is a write-only REST API that allows you to track your users or visitors directly, without the use of our JavaScript [tracking code](https://developer.kenticocloud.com/docs/enable-tracking). You can use it, for example, to track users in your mobile application.

### Creating TrackingClient instance

The **TrackingClient** class in the `KenticoCloud.Personalization` assembly enables you to send information about your visitors or users to Kentico Cloud. At this time, it doesn't require the use of your Personalization API Key. You only need to pass it your [Project Id](https://developer.kenticocloud.com/v1/docs/getting-content#section-getting-content-items).

#### Recording a session

```C#
// Records new session of a specified visitor, generates session ID automatically (and returns it)
var client = new TrackingClient("https://engage-ket.kenticocloud.com", Guid.Parse("38af179c-40ba-42e7-a5ca-33b8cdcc0d45"));
string uid = "7899852211af00000";
sid = client.RecordNewSession(uid);
```

#### Recording a custom activity

```C#
// Records custom activity of a specified visitor during the specified session
var client = new TrackingClient("https://engage-ket.kenticocloud.com", Guid.Parse("38af179c-40ba-42e7-a5ca-33b8cdcc0d45"));
string uid = "1111136b4af00000";
string sid = "7899852211af0000";
string activityCodename = "Clicked_facebook_icon";

client.RecordActivity(uid, sid, activityCodename);
```

#### Recording email and other information about a visitor

```C#
// Records information about the specified visitor
var client = new TrackingClient("https://engage-ket.kenticocloud.com", Guid.Parse("38af179c-40ba-42e7-a5ca-33b8cdcc0d45"));
string uid = "1111136b4af00000";
string sid = "7899852211af00000";
Contant contact = new Contact {
    Email = "johnsmith@gmail.com",
    Company = "Alphabet",
    Name = "John Smith",
    Phone = "555-888-777",
    Website = "johnsmith.blog.com"    
}

client.RecordVisitor(uid, sid, contact);
```

## Feedback & Contributing
Check out the [contributing](https://github.com/Kentico/personalization-sdk-net/blob/master/CONTRIBUTING.md) page to see the best places to file issues, start discussions and begin contributing.

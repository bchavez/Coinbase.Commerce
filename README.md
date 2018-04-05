[![Build status](https://ci.appveyor.com/api/projects/status/3nq1hvf67yp0nswg/branch/master?svg=true)](https://ci.appveyor.com/project/bchavez/coinbase-commerce/branch/master) [![Nuget](https://img.shields.io/nuget/v/Coinbase.Commerce.svg)](https://www.nuget.org/packages/Coinbase.Commerce/) [![Users](https://img.shields.io/nuget/dt/Coinbase.Commerce.svg)](https://www.nuget.org/packages/Coinbase.Commerce/) <img src="https://raw.githubusercontent.com/bchavez/Coinbase.Commerce/master/Docs/coinbase_commerce.png" align='right' />

Coinbase.Commerce for .NET and C#
=================

Project Description
-------------------
:moneybag: A C# API library and HTTP client implementation for the [**Coinbase Commerce** API](https://commerce.coinbase.com/docs/).

#### Supported Platforms
* **.NET Standard 1.3** or later
* **.NET Framework 4.5** or later

#### Crypto Tip Jar
* :dollar: **Bitcoin**: `1KgpR5rQmFpfvxQKdPsL9jU8FPf35xmjvn`
* :pound: **Litecoin**: `LXWELKEw124ryu3hbwzBJPUy81odeLthkv`
* :euro: **Ethereum**: `0xeb294D2BCb1Cf25cBEBd0bF55160aA655F82D8c0`
* :dog2: **Dogecoin**: `DGVC2drEMt41sEzEHSsiE3VTrgsQxGn5qe`


### Download & Install
Nuget Package **[Coinbase.Commerce](https://www.nuget.org/packages/Coinbase.Commerce/)**

```
Install-Package Coinbase.Commerce
```

Usage
-----
### A Simple Payment Page
Suppose you want to charge a customer **1.00 USD** for a candy bar and you want to receive payment in concurrency like **Bitcoin** or **Ethereum**. The following **C#** creates a hosted payment page using the **Coinbase Commerce API**:

```csharp
var commerceApi = new CommerceApi(apiKey);

// Something that identifies the customer on your system with
// the payment the customer is about to make.
var customerId = Guid.NewGuid();

var charge = new CreateCharge
   {
      Name = "Candy Bar",
      Description = "Sweet Tasting Chocolate",
      PricingType = PricingType.FixedPrice,
      LocalPrice = new Money { Amount = 1.00m, Currency = "USD"},
      Metadata =
         {
            {"customerId", customerId }
         },
   };

var response = await commerceApi.CreateChargeAsync(charge);

// Check for any errors
if( response.HasError() )
{
   // The server says something is wrong. Log the error 
   // and report back to the user an error has occurred.
   Console.WriteLine(response.Error.Message);
   Server.Render("Error creating checkout page.", 500);
   return;
}

// else, send the user to the hosted checkout page at Coinbase.
Server.Redirect(response.Data.HostedUrl);
```
When the customer's browser is redirected to the `HostedUrl` the customer will be allowed to choose a method of payment:

<img src="https://raw.githubusercontent.com/bchavez/Coinbase.Commerce/master/Docs/charge.png" />

It's important to keep in mind that the customer has **15 minutes** to complete the payment; otherwise the payment will fail.

### Look Ma! No Redirects!
It's possible to create your own custom **UI** with **QR codes** for payment.

In the previous example, if the charge creation was successful, you should get back a `Charge` object in `response.Data` that looks like:

```json
{
  "data": {
    "code": "SOMECODE",
    "name": "Candy Bar",
    "description": "Sweet Tasting Chocolate",
    "logo_url": null,
    "hosted_url": "https://commerce.coinbase.com/charges/SOMECODE",
    "created_at": "2018-04-04T19:45:34+00:00",
    "expires_at": "2018-04-04T20:00:34+00:00",
    "confirmed_at": "0001-01-01T00:00:00+00:00",
    "checkout": null,
    "timeline": [
      {
        "time": "2018-04-04T19:45:34+00:00",
        "status": "NEW",
        "context": null
      }
    ],
    "metadata": {
      "customerId": "30025397-adff-4d80-8e9f-c231b582be85"
    },
    "pricing_type": "fixed_price",
    "pricing": {...},
    "payments": [],
    "addresses": {
      "ethereum": "0xeb294D2BCb1Cf25cBEBd0bF55160aA655F82D8c0",
      "bitcoin": "1KgpR5rQmFpfvxQKdPsL9jU8FPf35xmjvn",
      "bitcoincash": "DGVC2drEMt41sEzEHSsiE3VTrgsQxGn5qe",
      "litecoin": "LXWELKEw124ryu3hbwzBJPUy81odeLthkv"
    }
  },
  "error": null,
  "warnings": null
}
```
Wonderful! Notice the `data.addresses` dictionary of `bitcoin`, `ethereum` and `litecoin` addresses above. You can use these addresses to generate **QR codes** in your custom **UI**. The same timelimit and rules apply, the customer has **15 minutes** to complete the payment. 

### Webhooks: Don't call us, we'll call you...
If you want to receive a notification on your server when a payment has been created, completed, or failed, you can use [webhooks](https://commerce.coinbase.com/docs/api/#webhooks). Once you've sub  

Building
--------
* Download the source code.
* Run `build.cmd`.

Upon successful build, the results will be in the `\__compile` directory. If you want to build NuGet packages, run `build.cmd pack` and the NuGet packages will be in `__package`.

---
*Note: This application/third-party library is not directly supported by Coinbase Inc. Coinbase Inc. makes no claims about this application/third-party library.  This application/third-party library is not endorsed or certified by Coinbase Inc.*
## v2.0.1
* Update `Flurl.Http` dependency to 3.0.1.
* Minimum .NET Framework requirement changed from 4.5 to 4.6.1.
* Dropped support for `netstandard1.3`; `netstandard2.0` still supported.

## v1.1.1
* Issue 12: Fixes error converting null to type 'System.Int64' in charge payments block height.

## v1.1.0
* PR 10: New API methods added: `CancelChargeAsync` and `ResolveChargeAsync`.
* Model update. `Id` and `Resource` properties added on models.

## v1.0.6
* PR 5: Added additional `Webhook` event helper properties, `Event.IsChargeDelayed`, `Event.IsChargePending`, `Event.IsChargeResolved`. 

## v1.0.5
* Added SourceLink compatibility.
* Added `CommerceApi.EnableFiddlerDebugProxy()` method to help debug HTTP calls. The method appears for applications using **.NET Standard 2.0** or **Full .NET Framework**. The `EnableFiddlerDebugProxy` will not appear for **.NET Standard 1.3** consumers.
* NuGet dependency `Flurl.Http` updated.
* NuGet dependency `Newtonsoft.Json` updated.

## v1.0.4
* Ensure multiple calls to same endpoint don't cause HTTP 404. 

## v1.0.3
* Model update to `UpdateCheckout.RequestedInfo` to clear "email" and "name" flags.
* Added more XMLDoc comments.
* Added missing `CreateCharge.RedirectUrl`. 

## v1.0.1
* Added `.HasError()` and `.HasWarnings()` helper methods on JSON response objects.
* Set `X-CC-Version` header to `2018-03-22` to remove server warnings.
* Set `User-Agent` string when making API calls.
* Fixed `Webhook.Id` type.

## v1.0.0
* Initial release.
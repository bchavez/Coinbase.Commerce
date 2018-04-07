# v1.0.3
* Model update to `UpdateCheckout.RequestedInfo` to clear "email" and "name" flags.
* Added more XMLDoc comments.
* Added missing `CreateCharge.RedirectUrl`. 

# v1.0.1
* Added `.HasError()` and `.HasWarnings()` helper methods on JSON response objects.
* Set `X-CC-Version` header to `2018-03-22` to remove server warnings.
* Set `User-Agent` string when making API calls.
* Fixed `Webhook.Id` type.

# v1.0.0
* Initial release.
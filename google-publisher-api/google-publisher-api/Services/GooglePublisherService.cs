using System;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace google_publisher_api.Services
{
	public class GooglePublisherService : IGooglePublisherService
	{
        private readonly AndroidPublisherService _androidPublisherService;
        public GooglePublisherService()
		{
            // Load credentials from JSON key file
            GoogleCredential credential;
            using (var stream = new FileStream("/Users/burakyildirim/Downloads/keystore/andrid-keystore/google-service-account.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(AndroidPublisherService.Scope.Androidpublisher);
            }

            // Initialize the AndroidPublisherService
            _androidPublisherService = new AndroidPublisherService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        }

        public async Task<Listing> ValidateAppPackageName(string packageName)
        {
            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();

            // Getting App Detail for default language
            var editDetails = await _androidPublisherService.Edits.Details.Get(packageName, edit.Id).ExecuteAsync();

            // Getting Full Description, Short Description and titile for default language
            var appDetailsRequest = _androidPublisherService.Edits.Listings.Get(packageName, edit.Id, editDetails.DefaultLanguage);
            
            var appDetailsResponse = await appDetailsRequest.ExecuteAsync();
            await _androidPublisherService.Edits.Delete(packageName, edit.Id).ExecuteAsync();

            return appDetailsResponse;
        }
    }
}


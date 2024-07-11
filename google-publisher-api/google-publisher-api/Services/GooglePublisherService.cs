﻿using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using static google_publisher_api.Models.GooglePublisherModel;

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

        // Get App details
        public async Task<AppDetails> GetAppDetails(string packageName)
        {
            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();
   
            // Getting App Detail for default language
            var editDetailsRequest = _androidPublisherService.Edits.Details.Get(packageName, edit.Id);
            var editDetails = await editDetailsRequest.ExecuteAsync();

            await _androidPublisherService.Edits.Delete(packageName, edit.Id).ExecuteAsync();
            return editDetails;
        }

        // Update App details
        public async Task<bool> UpdateAppDetails(string packageName,UpdateAppDetailRequest model, bool changesNotSentForReview)
        {
            AppDetails details = await GetAppDetails(packageName);

            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();

            details.ContactEmail = model.contactEmail;
            details.ContactPhone = model.contactPhone;
            details.ContactWebsite = model.contactWebsite;

            await _androidPublisherService.Edits.Details.Update(details, packageName, edit.Id).ExecuteAsync();

            EditsResource.CommitRequest commitRequest = _androidPublisherService.Edits.Commit(packageName, edit.Id);
            commitRequest.ChangesNotSentForReview = changesNotSentForReview;
            await commitRequest.ExecuteAsync();

            return true;
        }

        // Retrieve main store listings for all language
        public async Task<List<MainStoreListingResponse>> GetMainStoreListings(string packageName)
        {
            List<MainStoreListingResponse> response = new List<MainStoreListingResponse>();

            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();

            // Getting App Details for all languages
            var appDetailsRequest = _androidPublisherService.Edits.Listings.List(packageName, edit.Id);

            ListingsListResponse appDetailsResponse = await appDetailsRequest.ExecuteAsync();

            foreach (Listing localizedItem in appDetailsResponse.Listings)
            {
                List<GraphicsResponse> imageResponseList = new List<GraphicsResponse>
                {
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.FeatureGraphic).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.FeatureGraphic
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.Icon).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.Icon
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.PhoneScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.PhoneScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.SevenInchScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.SevenInchScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TenInchScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TenInchScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvBanner).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvBanner
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, localizedItem.Language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.WearScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.WearScreenshots
                     }
                };


                response.Add(new MainStoreListingResponse
                {
                    listingAssets = localizedItem,
                    graphics = imageResponseList
                });
            }

            await _androidPublisherService.Edits.Delete(packageName, edit.Id).ExecuteAsync();

            return response;
        }

        // Retrieve main store listings for selected language
        public async Task<MainStoreListingResponse> GetMainStoreListingByLanguage(string packageName, string language)
        {

            MainStoreListingResponse response = new MainStoreListingResponse();
            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();
            // Getting App Details for selected language
            var appDetailsResponse = await _androidPublisherService.Edits.Listings.Get(packageName, edit.Id, language).ExecuteAsync();
            response.listingAssets = appDetailsResponse;
            response.graphics = new List<GraphicsResponse>
                {
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.FeatureGraphic).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.FeatureGraphic
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.Icon).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.Icon
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.PhoneScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.PhoneScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.SevenInchScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.SevenInchScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TenInchScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TenInchScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvBanner).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvBanner
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.TvScreenshots
                     },
                     new GraphicsResponse {
                         imageInfo = await _androidPublisherService.Edits.Images.List(packageName, edit.Id, language, EditsResource.ImagesResource.ListRequest.ImageTypeEnum.WearScreenshots).ExecuteAsync(),
                         imageType= EditsResource.ImagesResource.ListRequest.ImageTypeEnum.WearScreenshots
                     }
                };
            await _androidPublisherService.Edits.Delete(packageName, edit.Id).ExecuteAsync();
            return response;
        }

        // Retrieve already configured language list for app
        public async Task<List<string>> GetAppCurrentTranslations(string packageName)
        {
            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();
            List<string> appTranslations = new List<string>();

            // Getting App Details for all languages
            ListingsListResponse appDetailsResponse = await _androidPublisherService.Edits.Listings.List(packageName, edit.Id).ExecuteAsync();

            foreach (Listing localizedItem in appDetailsResponse.Listings)
            {
                appTranslations.Add(localizedItem.Language);
            }

            await _androidPublisherService.Edits.Delete(packageName, edit.Id).ExecuteAsync();

            return appTranslations;
        }

        // Set app's default language
        public async Task<bool> SetDefaultLanguage(string packageName, string language, bool changesNotSentForReview)
        {
            AppDetails details = await GetAppDetails(packageName);

            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();

            details.DefaultLanguage = language;

            await _androidPublisherService.Edits.Details.Update(details,packageName, edit.Id).ExecuteAsync();

            EditsResource.CommitRequest commitRequest = _androidPublisherService.Edits.Commit(packageName, edit.Id);
            commitRequest.ChangesNotSentForReview = changesNotSentForReview;
            await commitRequest.ExecuteAsync();

            return true;
        }

        // Remove translation for selected language
        public async Task<bool> RemoveTranslation(string packageName, string language, bool changesNotSentForReview)
        {

            var edit = await _androidPublisherService.Edits.Insert(new AppEdit(), packageName).ExecuteAsync();

            await _androidPublisherService.Edits.Listings.Delete(packageName, edit.Id, language).ExecuteAsync();

            EditsResource.CommitRequest commitRequest = _androidPublisherService.Edits.Commit(packageName, edit.Id);
            commitRequest.ChangesNotSentForReview = changesNotSentForReview;
            await commitRequest.ExecuteAsync();

            return true;
        }

    }
}


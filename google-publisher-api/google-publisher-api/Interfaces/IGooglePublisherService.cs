using System;
using Google.Apis.AndroidPublisher.v3.Data;
using static google_publisher_api.Models.GooglePublisherModel;

namespace google_publisher_api.Services
{
	public interface IGooglePublisherService
    {
        Task<Listing> ValidateAppPackageName(string packageName);
        Task<List<MainStoreListingResponse>> GetMainStoreListings(string packageName);
        Task<MainStoreListingResponse> GetMainStoreListingByLanguage(string packageName,string language);
        Task<AppDetails> GetAppDetails(string packageName);
        Task<bool> UpdateAppDetails(string packageName,UpdateAppDetailRequest model, bool changesNotSentForReview);
        Task<List<string>> GetAppCurrentTranslations(string packageName);
        Task<bool> SetDefaultLanguage(string packageName, string language,bool changesNotSentForReview);
        Task<bool> RemoveTranslation(string packageName, string language, bool changesNotSentForReview);
        Task<Tracks> GetTrackList(string packageName);
        Task<bool> SubmitReleaseToTrack(string packageName, SubmitReleaseToTrackRequest model, string trackValue, bool changesNotSentForReview);
        Task<object> TestEmptyService();
    }
}


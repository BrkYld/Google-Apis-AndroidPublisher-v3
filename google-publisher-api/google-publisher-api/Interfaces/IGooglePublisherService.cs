using System;
using Google.Apis.AndroidPublisher.v3.Data;

namespace google_publisher_api.Services
{
	public interface IGooglePublisherService
    {
        Task<Listing> ValidateAppPackageName(string packageName);

    }
}


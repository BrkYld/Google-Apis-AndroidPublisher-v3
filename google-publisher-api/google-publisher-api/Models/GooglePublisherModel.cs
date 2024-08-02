using System;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Newtonsoft.Json.Linq;

namespace google_publisher_api.Models
{
	public class GooglePublisherModel
	{
		public class GraphicsResponse
		{
			public EditsResource.ImagesResource.ListRequest.ImageTypeEnum imageType { get; set; } = EditsResource.ImagesResource.ListRequest.ImageTypeEnum.AppImageTypeUnspecified;
            public ImagesListResponse? imageInfo { get; set; }
        }
		public class MainStoreListingResponse
		{
			public Listing? listingAssets { get; set; }
			public List<GraphicsResponse>? graphics { get; set; }
		}

		public class UpdateAppDetailRequest 
		{
			public string contactEmail { get; set; }
            public string? contactPhone { get; set; }
            public string? contactWebsite { get; set; }
        }

		public class SubmitReleaseToTrackRequest
		{

			public double? _userFraction;

            public string name { get; set; }
			public List<long?> versionCodes { get; set; }
			public List<LocalizedText> releaseNotes { get; set; } = new List<LocalizedText>();
			public double? userFraction {
                get
				{
					return _userFraction;
				}
				set
                {
                    if (value.HasValue && (value <= 0.0 || value > 1.0))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "UserFraction must be between 0 and 1.");
                    }
                    _userFraction = value;
                }
            }
		}
        public class Tracks
		{
            public List<Track> production { get; set; } = new List<Track>();
            public List<Track> openTesting { get; set; } = new List<Track>();
			public List<Track> closedTesting { get; set; } = new List<Track>();
			public List<Track> internalTesting { get; set; } = new List<Track>();
        }
	}
}


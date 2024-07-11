using System;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;

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
	}
}


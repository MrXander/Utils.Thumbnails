using System;
using System.IO;
using System.Threading.Tasks;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using Orchard.MediaLibrary.Models;
using Orchard.MediaLibrary.Services;
using Utils.Thumbnails.Models;
using Utils.Thumbnails.Services;

namespace Utils.Thumbnails.Handlers
{
    public class ThumbnailsSettingsHandler : ContentHandler
    {
        private readonly IMediaLibraryService _mediaLibraryService;
        private readonly IContentManager _contentManager;
        private readonly IThumbnailsSettingsService _thumbnailsService;

        public ThumbnailsSettingsHandler(
            IMediaLibraryService mediaLibraryService,
            IRepository<ThumbnailsSettingsRecord> thumbnailsRepository,            
            IContentManager contentManager,
            IThumbnailsSettingsService thumbnailsService)
        {
            _mediaLibraryService = mediaLibraryService;
            _contentManager = contentManager;
            _thumbnailsService = thumbnailsService;
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<ThumbnailsSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(thumbnailsRepository));

            OnCreated<MediaPart>((context, part) => Task.Factory.StartNew(ProcessAsync(part)));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
            {
                return;
            }
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Thumbnails")));
        }

        private Action ProcessAsync(MediaPart part)
        {
            if (!String.IsNullOrEmpty(part.FileName))
            {
                var settings = _thumbnailsService.GetSettings();

                if (settings.MaxWidth == 0 || settings.MaxHeight == 0)
                    return null;

                //if event triggered by already created thumbnail - return
                if (part.FolderPath.EndsWith("thumbnails") || (settings.Folder != null && part.FolderPath.EndsWith(settings.Folder)))
                    return null;

                var mediaPublicUrl = _mediaLibraryService.GetMediaPublicUrl(part.FolderPath, part.FileName).Replace("/", "\\");
                var imagePath = GetFullPathFromRelative(mediaPublicUrl);

                using (var resizedImageStream = ImageProcessor.Resize(imagePath, settings.MaxWidth, settings.MaxHeight))
                {                        
                    var folderPath = string.IsNullOrEmpty(settings.Folder)
                        ? Path.Combine(part.FolderPath, "thumbnails")
                        : Path.Combine(part.FolderPath, settings.Folder);

                    var mediaPart = _mediaLibraryService.ImportMedia(resizedImageStream, folderPath, part.FileName);
                    _contentManager.Create(mediaPart);
                }                
            }
            return null;
        }

        private string GetFullPathFromRelative(string mediaPublicUrl)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;            
            var relativePath = RemoveFirstSlash(mediaPublicUrl);            
            return Path.Combine(currentDirectory, relativePath);
        }

        private static string RemoveFirstSlash(string relativePath)
        {
            return relativePath.IndexOf("/", StringComparison.InvariantCultureIgnoreCase) == 0
                || relativePath.IndexOf("\\", StringComparison.InvariantCultureIgnoreCase) == 0
                ? relativePath.Substring(1, relativePath.Length - 1)
                : relativePath;            
        }
    }
}
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Utils.Thumbnails.Models;

namespace Utils.Thumbnails.Services
{
    public class ThumbnailsSettingsService : IThumbnailsSettingsService
    {
        private readonly IOrchardServices _orchardServices;

        public ThumbnailsSettingsService(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public ThumbnailsSettingsPart GetSettings()
        {
            return _orchardServices
                .ContentManager
                .Query<ThumbnailsSettingsPart, ThumbnailsSettingsRecord>()
                .List()
                .FirstOrDefault();
        }
    }
}
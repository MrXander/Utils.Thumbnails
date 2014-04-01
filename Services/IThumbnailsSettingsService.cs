using Orchard;
using Utils.Thumbnails.Models;

namespace Utils.Thumbnails.Services
{
    public interface IThumbnailsSettingsService : IDependency
    {
        ThumbnailsSettingsPart GetSettings();
    }
}

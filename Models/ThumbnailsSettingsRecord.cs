using Orchard.ContentManagement.Records;

namespace Utils.Thumbnails.Models
{
    public class ThumbnailsSettingsRecord : ContentPartRecord
    {        
        public virtual string Folder { get; set; }
        public virtual int MaxWidth { get; set; }
        public virtual int MaxHeight { get; set; }

        public ThumbnailsSettingsRecord()
        {
            Folder = "thumbnails";
            MaxHeight = 120;
            MaxWidth = 120;
        }
    }
}
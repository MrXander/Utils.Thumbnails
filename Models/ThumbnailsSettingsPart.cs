using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace Utils.Thumbnails.Models
{
    public class ThumbnailsSettingsPart : ContentPart<ThumbnailsSettingsRecord>
    {
        public string Folder
        {
            get { return Record.Folder; }
            set { Record.Folder = value; }
        }

        [Required]
        public int MaxWidth
        {
            get { return Record.MaxWidth; }
            set { Record.MaxWidth = value; }
        }

        [Required]
        public int MaxHeight
        {
            get { return Record.MaxHeight; }
            set { Record.MaxHeight = value; }
        }
    }
}
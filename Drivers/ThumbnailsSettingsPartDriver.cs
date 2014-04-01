using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Utils.Thumbnails.Models;

namespace Utils.Thumbnails.Drivers
{
    public class ThumbnailsSettingsPartDriver : ContentPartDriver<ThumbnailsSettingsPart>
    {
        private const string TemplateName = "Parts/ThumbnailsSettings";        

        public ThumbnailsSettingsPartDriver()
        {
            T = NullLocalizer.Instance;            
        }

        public Localizer T { get; set; }

        protected override string Prefix { get { return "ThumbnailsSettings"; } }

        protected override DriverResult Editor(ThumbnailsSettingsPart part, dynamic shapeHelper)
        {
            return Editor(part, null, shapeHelper);
        }

        protected override DriverResult Editor(ThumbnailsSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            return ContentShape("Parts_Thumbnails_SiteSettings", () =>
            {
                if (updater != null)
                {
                    updater.TryUpdateModel(part.Record, Prefix, null, null);
                }

                return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            })
            .OnGroup("Thumbnails");
        }
    }
}
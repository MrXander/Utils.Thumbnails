using Orchard.Data.Migration;

namespace Utils.Thumbnails
{
    public class ThumbnailsMigration : DataMigrationImpl
    {        
        public int Create()
        {
            SchemaBuilder.CreateTable("ThumbnailsSettingsRecord", t => t
                .Column<int>("Id")
                .Column<string>("Folder")
                .Column<int>("MaxWidth")
                .Column<int>("MaxHeight")
                );

            return 1;
        }        
    }
}
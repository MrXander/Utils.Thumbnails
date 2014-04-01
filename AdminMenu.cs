using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Utils.Thumbnails {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            //builder
            //    .Add(T("Settings"), menu => menu
            //        .Add(T("Thumbnails"), "10.0", subMenu => subMenu.Action("Index", "Admin", new { area = "Utils.Thumbnails" }).Permission(StandardPermissions.SiteOwner)));

            builder
                .Add(T("Thumbnails"), "11",
                    menu => menu.Action("Index", "Admin", new { area = "Utils.Thumbnails" }).Permission(StandardPermissions.SiteOwner)
                        .Add(T("Thumbnails"), "1.0", item => item.Action("Index", "Admin", new { area = "Utils.Thumbnails" })
                            .LocalNav().Permission(StandardPermissions.SiteOwner)));
        }
    }
}
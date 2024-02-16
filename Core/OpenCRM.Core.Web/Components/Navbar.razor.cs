using Microsoft.AspNetCore.Components;
using OpenCRM.Core.Web.Components.DropdownMenu;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Core.Web.Components
{
    public partial class Navbar : ComponentBase
    {
        //TODO: Module informations as MenuLinks can be stored on the CRM database on the next future and this data cal be
        // loaded from there to load the current module dropdownModel.

        private static List<DropdownMenuModel> mainModulesLinks = new List<DropdownMenuModel>()
        {
            new DropdownMenuModel("Register", "/Identity/Register"),

            new DropdownMenuModel("Login", "/Identity/Login"),

            new DropdownMenuModel()
            {
                Name = "Management",
                Url = "/Manage",
                Items = new List<DropdownMenuModel>()
                {
                    new DropdownMenuModel("Blocks", "/Manage/Block"),
                    new DropdownMenuModel("Medias", "/Manage/Media"),
                    new DropdownMenuModel("Languages", "/Manage/Languages")
                }
            },

            new DropdownMenuModel()
            {
                Name = "SwissLDP",
                Url = "/SwissLDP",
                ShowItemList = false,
                Items = new List<DropdownMenuModel>()
                {
                    new DropdownMenuModel("Event", "/SwissLDP/Event"),
                    new DropdownMenuModel("Supplier", "/SwissLDP/Supplier", new List<DropdownMenuModel>()
                        {
                            new DropdownMenuModel("Register", "/SwissLDP/Supplier/Register")
                        }, false)
                }
            },
        };

        [Inject]
        public required NavigationManager Navigation { get;set; }
        [Inject]
        public required IIdentityService IdentityService { get; set; }

        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [Parameter]
        public DropdownMenuModel DropdownMenuModules { get; set; } = new DropdownMenuModel("Modules", "", mainModulesLinks); //Main Modules

        [Parameter]
        public DropdownMenuModel CurrentModuleLinks { get; set; } = saveCurrentModelLinks; //Active Main Module

        static DropdownMenuModel saveCurrentModelLinks = new DropdownMenuModel();
        static string UserName = "";
        static string Name = "";

        protected override async void OnInitialized()
        {
            string currentModuleUrl = "/" + Navigation.ToBaseRelativePath(Navigation.Uri);
            var usermodel = await IdentityService.GetLoggedUser();

            if (usermodel != null)
            {
                UserName = usermodel.UserName;
                Name = $"{usermodel.Name} {usermodel.Lastname}";
            }

            if (currentModuleUrl == "/")
            {
                saveCurrentModelLinks = new DropdownMenuModel();
                CurrentModuleLinks = saveCurrentModelLinks;
            }
            else if (currentModuleUrl != "")
            {
                var result = DropdownMenuModules.FindItemByUrl(currentModuleUrl);

                if (result != null)
                {
                    saveCurrentModelLinks = new DropdownMenuModel(result.Name, result.Url, result.Items);
                    CurrentModuleLinks = saveCurrentModelLinks;
                }
            }
        }
    }
}

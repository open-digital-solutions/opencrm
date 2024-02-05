using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Components.DropdownMenu;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components
{
    public partial class Navbar : ComponentBase
    {
        //TODO: Module informations as MenuLinks can be stored on the CRM database on the next future and this data cal be
        // loaded from there to load the current module dropdownModel.

        private static List<DropdownMenuModel> dataModulesLinks = new List<DropdownMenuModel>()
        {
            new DropdownMenuModel("Management", "/Manage"),

            new DropdownMenuModel("Register", "/Identity/Register"),

            new DropdownMenuModel("Login", "/Identity/Auth"),

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

            new DropdownMenuModel()
            {
                Name = "Finance",
                Url = "Finance",
                ShowItemList = false,
                Items = new List<DropdownMenuModel>()
                {
                    new DropdownMenuModel("Accounting", "/Finance/Accounting"),
                }
            }
        };

        [Inject]
        public NavigationManager Navigation { get;set; }

        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [Parameter]
        public DropdownMenuModel DropdownMenuModules { get; set; } = new DropdownMenuModel("Modules", "", dataModulesLinks); //Modules DropdownMenu

        [Parameter]
        public DropdownMenuModel CurrentModuleLinks { get; set; } = saveCurrentModelLinks; //Active Module

        
        static DropdownMenuModel saveCurrentModelLinks = new DropdownMenuModel();

        protected override void OnInitialized()
        {
            string currentModuleUrl = "/" + Navigation.ToBaseRelativePath(Navigation.Uri);

            if(currentModuleUrl != "")
            {
                DropdownMenuModel result = DropdownMenuModules.FindItemByUrl(currentModuleUrl);

                if (result == null && CurrentModuleLinks.Items.Count() != 0)
                {
                    result = CurrentModuleLinks.FindItemByUrl(currentModuleUrl);
                }
                
                if(result != null)
                { 
                    saveCurrentModelLinks = new DropdownMenuModel(result.Name, result.Url, result.Items);
                    CurrentModuleLinks = saveCurrentModelLinks;
                }
            }
        }
    }
}

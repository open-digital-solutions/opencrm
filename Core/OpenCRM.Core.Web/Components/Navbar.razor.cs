using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Components.Block;
using OpenCRM.Core.Web.Components.Block.Description;
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
        public NavigationManager? Navigation { get;set; }

        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [Parameter]
        public DropdownMenuModel DropdownMenuModules { get; set; } = new DropdownMenuModel("Modules", "", mainModulesLinks); //Main Modules

        [Parameter]
        public DropdownMenuModel CurrentModuleLinks { get; set; } = saveCurrentModelLinks; //Active Main Module

        public DescriptionModel DescriptionItem = new DescriptionModel()
        {
            ListItems = new List<DescriptionItem>()
            {
                new DescriptionItem()
                {
                    Text = "Ricarica da 10ml con 600ml acqua superfici lavabili"
				},

                new DescriptionItem()
                {
                    Text = "Disifezioni mani e supperfici si consiglia:",
                    Items = new List<DescriptionItem>()
                    {
                        new DescriptionItem()
                        {
                            Text = "alcool etilico al 95%"
						},
                        new DescriptionItem()
                        {
                            Text = "ml 200 acqua"
						},
                        new DescriptionItem()
                        {
							Text = "ml 12 OmniumBio"
						}
                    }
				}
            }
        };

        static DropdownMenuModel saveCurrentModelLinks = new DropdownMenuModel();

        protected override void OnInitialized()
        {
            if (Navigation != null)
            {
                string currentModuleUrl = "/" + Navigation.ToBaseRelativePath(Navigation.Uri);

                if (currentModuleUrl != "")
                {
                    DropdownMenuModel result = DropdownMenuModules.FindItemByUrl(currentModuleUrl);

                    if (result != null)
                    {
                        saveCurrentModelLinks = new DropdownMenuModel(result.Name, result.Url, result.Items);
                        CurrentModuleLinks = saveCurrentModelLinks;
                    }
                }
            }
        }
    }
}

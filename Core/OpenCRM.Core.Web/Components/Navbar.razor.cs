﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
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
            new DropdownMenuModel("Management", "/Manage"),

            new DropdownMenuModel("Register", "/Identity/Register"),

            new DropdownMenuModel("Login", "/Identity/Login"),

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
        public NavigationManager Navigation { get;set; }
        [Inject]
        public IHttpContextAccessor ContextAccessor { get; set; }

        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [Parameter]
        public DropdownMenuModel DropdownMenuModules { get; set; } = new DropdownMenuModel("Modules", "", mainModulesLinks); //Main Modules

        [Parameter]
        public DropdownMenuModel CurrentModuleLinks { get; set; } = saveCurrentModelLinks; //Active Main Module

        
        static DropdownMenuModel saveCurrentModelLinks = new DropdownMenuModel();
        static string UserName = "";

        protected override void OnInitialized()
        {
            string currentModuleUrl = "/" + Navigation.ToBaseRelativePath(Navigation.Uri);
            var username = ContextAccessor?.HttpContext?.User.GetDisplayName();
            if(!string.IsNullOrEmpty(username) ) {
                UserName = username;
            }

            if(currentModuleUrl != "")
            {
                DropdownMenuModel result = DropdownMenuModules.FindItemByUrl(currentModuleUrl);

                if(result != null)
                { 
                    saveCurrentModelLinks = new DropdownMenuModel(result.Name, result.Url, result.Items);
                    CurrentModuleLinks = saveCurrentModelLinks;
                }
            }
        }
    }
}

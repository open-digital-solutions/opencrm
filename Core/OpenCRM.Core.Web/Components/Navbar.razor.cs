using Microsoft.AspNetCore.Components;
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
        public DropdownModel DropwdownMenuActive { get; set; } = new DropdownModel();

        [Parameter]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [Parameter]
        public DropdownModel DropdownMenuModules { get; set; } = new DropdownModel()
        {
            Name = "Modules",
            Items = new List<DropdownModel>()
            {
                new DropdownModel("Management", "/Manage"),

                new DropdownModel("Register", "/Identity/Register"),

                new DropdownModel("Login", "/Identity/Auth"),

                new DropdownModel("SwissLDP", "/SwissLDP", new List<DropdownModel>()
                {
                    new DropdownModel("Event", "/SwissLDP/Event"),
                    new DropdownModel("Supplier", "/SwissLDP/Supplier", new List<DropdownModel>()
                    {
                        new DropdownModel("Register", "/SwissLDP/Supplier/Register")
                    })
                }),

                new DropdownModel("Finance", "/Finance", new List<DropdownModel>()
                {
                    new DropdownModel("Accounting", "/Finance/Accounting")
                }),
            }
        };

        [Parameter]
        public DropdownModel SwissLDPModules { get; set; } = new DropdownModel()
        {
            Name = "SwissLDP",
            Items = new List<DropdownModel>()
            {
                new DropdownModel("Event", "/SwissLDP/Event"),
                new DropdownModel("Supplier", "/SwissLDP/Supplier", new List<DropdownModel>()
                {
                    new DropdownModel("Register", "/SwissLDP/Supplier/Register")
                })
            }
        };
        
    }
}

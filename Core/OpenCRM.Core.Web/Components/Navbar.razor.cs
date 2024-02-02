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
        public string? Module { get; set; } = "SwissLDP"; //TODO: This can be an enumerator

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

                new DropdownModel("SwissLDP", "/SwissLDP"),
            }
        };

        [Parameter]
        public DropdownModel CurrentModuleLinks { get; set; } = new DropdownModel()
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

        //TODO: Module informations as MenuLinks can be stored on the CRM database on the next future and this data cal be
        // loaded from there to load the current module dropdownModel.

        static int _selectedModule = 1;
        protected override void OnInitialized()
        {
            // Aqui puedes utilizar el NavigatorManager y otra cosa para saber el url en el que estas y a partir de ahi saber el modulo.
            Console.WriteLine("Me initialice, ahora puedo ver cual es el modulo seleccionado y mostrar sus links!!!");

            _selectedModule++;
        }
    }
}

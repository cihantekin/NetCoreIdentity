using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreIdentity.Models;

namespace NetCoreIdentity.Infrastructure
{
    [HtmlTargetElement("td",Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleUsersTagHelper(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("identity-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();

            IdentityRole role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null)
            {
                foreach (ApplicationUser applicationUser in _userManager.Users)
                {
                    if (applicationUser != null && await _userManager.IsInRoleAsync(applicationUser, role.Name))
                    {
                        names.Add(applicationUser.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "No User" : string.Join(", ", names));
        }
    }
}

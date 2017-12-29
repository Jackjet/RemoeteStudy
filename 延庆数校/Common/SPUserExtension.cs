using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class SPUserExtension
    {
        public static bool InGroup(this SPUser user, SPGroup group)
        {

            return user.Groups.Cast<SPGroup>()

              .Any(g => g.ID == group.ID);
        }
    }
}

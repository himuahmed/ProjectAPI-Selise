﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjectAPI_Selise.Models
{
    public class UserModel :IdentityUser
    {
        public string Name { get; set; }
    }
}

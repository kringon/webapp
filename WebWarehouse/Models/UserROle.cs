﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{
    public enum UserRole
    {
        //Has all privileges
        Admin,
        //Can edit content, but not users
        Editor,
        //Can only use website / order
        Customer
    }
}
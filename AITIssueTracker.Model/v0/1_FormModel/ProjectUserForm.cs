﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AITIssueTracker.Model.v0._1_FormModel
{
    public class ProjectUserForm
    {
        public Guid ProjectId { get; set; }

        public string Username { get; set; }
    }
}

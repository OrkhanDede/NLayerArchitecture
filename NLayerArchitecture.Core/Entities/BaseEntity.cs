﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerArchitecture.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}

using EHT.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EHT.BLL.DTOs
{
    public class NodePropertiesDto
    {
        public string Code { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public string OrganizationOwner { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EHT.BLL.DTOs
{
    public class NodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public NodePropertiesDto Properties { get; set; }
        public int ParentId { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace trip_tree.Models
{
    public partial class Line
    {
        public string Departure { get; set; }
        public string Busname { get; set; }
        public string Destination { get; set; }
        public int Id { get; set; }
    }
}

using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Listings
{
    public class OwnerImageVM
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public int CasteId { get; set; }
        public int StateId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
    }
}

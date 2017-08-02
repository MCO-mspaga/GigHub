using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class ArtistViewModel
    {
        public IEnumerable<ApplicationUser> Artist { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Models;
using System.Data.Entity;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController()
        {
            this._context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var artists = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();

            var viewModel = new ArtistViewModel
            {
                Artist = artists,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Who I'm Following"
            };

            return View(viewModel);
        }
    }
}
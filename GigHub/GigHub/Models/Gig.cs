﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set;  }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set;  }
        
        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = Notification.GigCreated(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

        }

        internal void Modify(DateTime dateTime, string venue, byte genre)
        {
            var notification = Notification.GigUpdated(this, dateTime, venue);

            DateTime = dateTime;
            Venue = venue;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);


        }
    }

}
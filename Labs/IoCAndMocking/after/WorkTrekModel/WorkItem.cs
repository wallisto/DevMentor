using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkTrekModel
{
    public class WorkItem
    {
        internal WorkItem()
        {

        }

        public WorkItem(string title, string description, DateTime createdOn)
        {
            Title = title;
            Description = description;
            CreatedOn = createdOn;
        }

        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Category { get; set; }
        public int? Status { get; set; }
        public int? Resolution { get; set; }
        public int? Priority { get; set; }

        DateTime _createdOn;
        public DateTime CreatedOn { get { return _createdOn; } 
            set 
            {
                if (value.Date > DateTime.Now.Date)
                {
                    throw new ArgumentOutOfRangeException("Creation date cannot be in the future");
                }
                _createdOn = value; 
            } 
        }

        DateTime? _finishedOn;
        public DateTime? FinishedOn 
        {
            get { return _finishedOn; }
            set
            {
                if (value.HasValue && (value.Value.Date > DateTime.Now.Date || value.Value.Date < CreatedOn.Date))
                {
                    throw new ArgumentOutOfRangeException("Creation date cannot be in the future");
                }
                //if (value.HasValue && value.Value.Date < CreatedOn)
                //{
                //    throw new ArgumentOutOfRangeException("Creation date cannot be in the future");
                //}
                _finishedOn = value;
            }
        }
    }
}

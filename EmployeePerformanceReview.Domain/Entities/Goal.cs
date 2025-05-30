﻿
using EmployeePerformanceReview.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // e.g. Technical, Communication
        public GoalStatus Status { get; set; } // Enum: NotStarted, InProgress, Completed
        public DateTime TargetDate { get; set; }
        public string Remarks { get; set; }
        public Employee Employee { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }            // User who created the follow-up
        public string? UpdatedBy { get; set; }
    }
}

﻿namespace Re.PL.Areas.Dashboard.ViewModels
{
    public class SerDelailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
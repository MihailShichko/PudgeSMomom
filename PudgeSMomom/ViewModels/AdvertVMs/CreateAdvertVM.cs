﻿namespace PudgeSMomom.ViewModels.AdvertVMs
{
    public class CreateAdvertVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.ViewModels;

public class SubscribeViewModel
{

    [Required]
    [EmailAddress]
    [Display(Name = "E-mail addres", Prompt = "Your Email")]
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;
}

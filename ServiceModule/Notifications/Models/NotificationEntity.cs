﻿namespace ServiceModule.Notifications.Models
{
    public class NotificationEntity
    {
        public string Controllerid { get; set; }
        public long Time { get; set; }
        public int Ruleid { get; set; }
        public string Message { get; set; }
    }
}

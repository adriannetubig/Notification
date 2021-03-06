﻿using System;
using System.Configuration;
namespace ConsumerMvc.Helper
{
    public class Config
    {
        public static int HubReconnectionAttempts => Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttempts"]);
        public static int HubReconnectionAttemptDelaySeconds => Convert.ToInt32(ConfigurationManager.AppSettings["HubReconnectionAttemptDelaySeconds"]) * 1000;
        public static string URLAuthentication => ConfigurationManager.AppSettings["URLAuthentication"];
        public static string URLSignalR => ConfigurationManager.AppSettings["URLSignalR"];
    }
}
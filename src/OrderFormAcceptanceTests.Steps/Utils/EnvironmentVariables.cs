﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OrderFormAcceptanceTests.Actions.Utils;

namespace OrderFormAcceptanceTests.Steps.Utils
{
    internal static class EnvironmentVariables
    {
        internal static (string, string, string, string, string, string, string) Get()
        {
            var url = Url();
            var hubUrl = HubUrl();
            var browser = Browser();

            var (serverUrl, databaseName, dbUser, dbPassword) = DbConnectionDetails();

            return (url, hubUrl, browser, serverUrl, databaseName, dbUser, dbPassword);
        }

        internal static string HubUrl()
        {
            return Environment.GetEnvironmentVariable("HUBURL") ?? "http://localhost:4444/wd/hub";
        }

        internal static string Url()
        {
            var uri = Environment.GetEnvironmentVariable("PBURL") ?? DefaultUri();
            return uri.TrimEnd('/');
        }

        internal static string Browser()
        {
            return Environment.GetEnvironmentVariable("BROWSER") ?? "ChromeLocal";
        }

        internal static (string serverUrl, string databaseName, string dbUser, string dbPassword) DbConnectionDetails()
        {
            var serverUrl = Environment.GetEnvironmentVariable("SERVERURL") ?? "tcp:gpitfutures-dev-sql-pri.database.windows.net,1433";
            var databaseName = Environment.GetEnvironmentVariable("DATABASENAME") ?? "bc-feature-8122-previewordersummary-ordapi";
            var dbUser = JsonConfigValues("user", "gpitfbcadmin");
            var dbPassword = JsonConfigValues("password", "mzwtabxensk5o7cGgf6ydirvj0luphq");

            return (serverUrl, databaseName, dbUser, dbPassword);
        }

        internal static string DbConnectionString()
        {
            var (serverUrl, databaseName, dbUser, dbPassword) = DbConnectionDetails();

            return string.Format(ConnectionString.GPitFutures, serverUrl, databaseName, dbUser, dbPassword);
        }

        internal static string BapiDbConnectionString()
        {
            var (serverUrl, databaseName, dbUser, dbPassword) = DbConnectionDetails();

            databaseName = Environment.GetEnvironmentVariable("BAPIDATABASENAME") ?? "bc-feature-8122-previewordersummary-bapi";

            return string.Format(ConnectionString.GPitFutures, serverUrl, databaseName, dbUser, dbPassword);
        }

        internal static string IsapiDbConnectionString()
        {
            var (serverUrl, databaseName, dbUser, dbPassword) = DbConnectionDetails();

            databaseName = Environment.GetEnvironmentVariable("ISAPIDATABASENAME") ?? "bc-feature-8122-previewordersummary-isapi";

            return string.Format(ConnectionString.GPitFutures, serverUrl, databaseName, dbUser, dbPassword);
        }

        private static string JsonConfigValues(string section, string defaultValue)
        {
            var path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "Utils",
                "tokens.json");
            var jsonSection = JObject.Parse(File.ReadAllText(path))[section];

            var dbValues = jsonSection.ToObject<Dictionary<string, string>>();

            var result = dbValues.Values
                .FirstOrDefault(s => !s.Contains("#{"));

            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        internal static User User(UserType userType)
        {
            string userTypeString = string.Empty;
            string defaultUserName = string.Empty;

            switch (userType)
            {
                case UserType.Authority:
                    userTypeString = "adminUser";
                    defaultUserName = "BobSmith@email.com";
                    break;
                case UserType.Buyer:
                    userTypeString = "buyerUser";
                    defaultUserName = "AliceSmith@email.com";
                    break;
            }

            var userJson = JsonConfigValues(userTypeString, $"{{\"username\": \"{defaultUserName}\", \"password\": \"Pass123$\" }}");

            var user = JsonConvert.DeserializeObject<User>(userJson);

            return user;
        }

        private static string DefaultUri()
        {
            var uri = "https://feature-8122-previewordersummary-dev.buyingcatalogue.digital.nhs.uk";

            return uri;
        }
    }

    public static class ConnectionString
    {
        internal const string GPitFutures =
            @"Server={0};Initial Catalog={1};Persist Security Info=false;User Id={2};Password={3}";
    }
}

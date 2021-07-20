using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// USING WEB.HOSTING TO USE HostingEnvironment IN ReadSiteSettings
using System.Web.Hosting;
using Newtonsoft.Json;


namespace TheatreCMS3.Models
{
    public class SiteSettings
    {
        public int Copyright { get; set; }
        // ADDED PHONE NUMBER AND ADDRESS AS STRINGS
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // MOVED METHOD FROM HOMECONTROLLER TO HERE
        public static SiteSettings ReadSiteSettings()
        {
            // CHANGED Server.MapPath TO HostingEnvironment.MapPath AS Server DOES NOT WORK WITH STATIC METHOD
            SiteSettings siteSettings = JsonConvert.DeserializeObject<SiteSettings>(System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/SiteSettings.json")));
            return siteSettings;
        }
    }
}
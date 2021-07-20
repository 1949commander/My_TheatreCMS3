using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class ProductionPhotographer: ApplicationUser
    {
        public string Camera { get; set; }
        public double CameraCost { get; set; }
        public string CameraSerialNumber { get; set; }
    }
}
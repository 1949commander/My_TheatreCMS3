using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Prod.Models
{
    //Data annotations: Data Annotations are attributes that can be applied to classes, properties, and/or methods and have syntax like [Key], [Display()], [Table()].
    [Table("ProductionPhotos")]
    public class ProductionPhoto
    {
        [Key]       //Database needs a key column
        public int ProPhotoId { get; set; }
        public Byte[] PhotoFile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
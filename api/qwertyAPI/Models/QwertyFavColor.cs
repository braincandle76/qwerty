using System;
using System.Collections.Generic;

namespace QwertyAPI.Models
{
    public class QwertyFavColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int QwertyProfileId { get; set; }
        public virtual QwertyProfile QwertyProfile { get; set; }

    }
}
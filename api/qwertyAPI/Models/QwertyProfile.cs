using System;
using System.Collections.Generic;

namespace QwertyAPI.Models
{
    public class QwertyProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual QwertyFavColor FavColor { get; set; }
        public virtual ICollection<QwertyMessage> Messages { get; set; }

        public QwertyProfile(string name)
        {
            Name = name;
        }
    }
}
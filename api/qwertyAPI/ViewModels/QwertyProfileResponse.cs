using System;
using System.Collections.Generic;
using QwertyAPI.Models;

namespace QwertyAPI.ViewModels
{
    public class QwertyProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public QwertyProfileResponse(QwertyProfile profile)
        {
            Id = profile.Id;
            Name = profile.Name;
        }
    }

}
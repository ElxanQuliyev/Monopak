using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonopakApp.Models
{
    public enum OrderStatus
    {
        Yeni = 1,
        ProsesGedir = 2,
        Catdırıldı = 3,
        UgursuzOldu = 4,
        LevgEdildi = 5,
        Gözlemede = 6,
        GeriQaytarıldı = 8
    }
}
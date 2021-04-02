using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Operation:IEntity
    {
        public int Id { get; set; }
        public string Foto { get; set; }
        public string IpAdresi { get; set; }
        public string Response { get; set; }
        public string IstekResponseSure { get; set; }
        public string YuklenenFormat { get; set; }
        public string DonusturulenFormat { get; set; }
    }
}

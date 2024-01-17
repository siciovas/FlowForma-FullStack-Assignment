using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Dto
{
    public class DeviceDto : ProductDto
    {
        public bool IsElectronical { get; set; }
    }
}

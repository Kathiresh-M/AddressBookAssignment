using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class AssetReturnDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string DownloadURL { get; set; }
        public string FileType { get; set; }
        public int Size { get; set; }
        public string FileContent { get; set; }
    }
}

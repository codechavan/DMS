using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model
{
    public class DocumentFolderTree
    {
        public long id { get; set; }
        public string text { get; set; }
        public long parentId { get; set; }
        public TreeState state { get; set; }
        public string icon { get { return "fa fa-folder-o"; } } //glyphicon glyphicon-stop
        public string selectedIcon { get { return "fa fa-folder-open-o"; } } //glyphicon glyphicon-stop
        public List<DocumentFolderTree> nodes { get; set; }
    }

    public class TreeState
    {
        public bool @checked { get; set; }
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
    }
}

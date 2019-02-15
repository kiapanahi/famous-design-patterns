using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Controls
{
    
    // represents a single menu item which is a node in a tree of menu items. 
    // menu items can have children themselves.
    
    // GoF Design Pattern: Composite.

    [Serializable()]
    public class MenuCompositeItem
    {
        public MenuCompositeItem(string item, string link)
        {
            Children = new List<MenuCompositeItem>();

            Item = item;
            Link = link;
        }
        
        public string Item { get; private set; }
        public string Link { get; private set; }
        public List<MenuCompositeItem> Children{ get; private set; }
    }
}

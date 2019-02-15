using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfApp.Controls
{
   
    // drop down button control
    
    public class DropDownButton : ToggleButton
    {
        // Dropdown dependency property
        
        public static readonly DependencyProperty DropDownProperty =
            DependencyProperty.Register("DropDown", typeof(ContextMenu),
            typeof(DropDownButton), new UIPropertyMetadata(null));

        public DropDownButton()
        {
            // binds the ToggleButton.IsChecked property to the drop-down's IsOpen property 

            Binding binding = new Binding("DropDown.IsOpen");
            binding.Source = this;
            this.SetBinding(IsCheckedProperty, binding);
        }

        // gets and sets the context menu (the dropdown).
        
        public ContextMenu DropDown
        {
            get { return (ContextMenu)GetValue(DropDownProperty); }
            set { SetValue(DropDownProperty, value); }
        }


        // oerridden OnClick. opens the dropdown.
        
        protected override void OnClick()
        {
            if (DropDown == null) return;

            // position and display dropdown

            DropDown.PlacementTarget = this;
            DropDown.Placement = PlacementMode.Bottom;

            DropDown.IsOpen = true;
        }
    }
}

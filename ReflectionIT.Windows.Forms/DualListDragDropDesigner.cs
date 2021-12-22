using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#if NET48

namespace ReflectionIT.Windows.Forms.Design {
    /// <summary>
    /// DualListDragDropDesigner is the Designer for the DualListComponent. 
    /// 
    /// It shadows the ListBoxFrom and ListBoxTo properties. This to
    /// prevent event handling from these controls in the Designer
    /// 
    /// Created by: Fons Sonnemans, Reflection IT
    /// 
    /// For questions and comments: Fons.Sonnemans@reflectionit.nl
    /// 
    /// </summary>
    public class DualListDragDropDesigner : System.ComponentModel.Design.ComponentDesigner {

        public virtual ListBox ListBoxFrom {
            get { return (ListBox)ShadowProperties["ListBoxFrom"]; }
            set {
                // note this value is not passed to the actual component
                this.ShadowProperties["ListBoxFrom"] = value;
            }
        }

        public virtual ListBox ListBoxTo {
            get { return (ListBox)ShadowProperties["ListBoxTo"]; }
            set {
                // note this value is not passed to the actual component
                this.ShadowProperties["ListBoxTo"] = value;
            }
        }

        protected override void PreFilterProperties(IDictionary properties) {
            base.PreFilterProperties(properties);

            // replace ListBoxFrom with our shadowed versions.
            properties["ListBoxFrom"] = TypeDescriptor.CreateProperty(
                typeof(DualListDragDropDesigner),
                (PropertyDescriptor)properties["ListBoxFrom"],
                new Attribute[0]);

            // replace ListBoxTo with our shadowed versions.
            properties["ListBoxTo"] = TypeDescriptor.CreateProperty(
                typeof(DualListDragDropDesigner),
                (PropertyDescriptor)properties["ListBoxTo"],
                new Attribute[0]);

        }

    }
}
#endif
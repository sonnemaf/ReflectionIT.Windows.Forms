//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Windows.Forms;

//#if !NET5_0_OR_GREATER 

//namespace ReflectionIT.Windows.Forms.Design {
//    /// <summary>
//    /// DualListDesigner is the Designer for the DualListComponent. 
//    /// 
//    /// It shadows the Button, ListBoxFrom and ListBoxTo properties. This to
//    /// prevent event handling from these controls in the Designer
//    /// 
//    /// Created by: Fons Sonnemans, Reflection IT
//    /// 
//    /// For questions and comments: Fons.Sonnemans@reflectionit.nl
//    /// 
//    /// </summary>
//    public class DualListDesigner : System.ComponentModel.Design.ComponentDesigner {

//        public virtual Button Button {
//            get { return (Button)ShadowProperties["Button"]; }
//            set {
//                // note this value is not passed to the actual component
//                this.ShadowProperties["Button"] = value;
//            }
//        }

//        public virtual ListBox ListBoxFrom {
//            get { return (ListBox)ShadowProperties["ListBoxFrom"]; }
//            set {
//                // note this value is not passed to the actual component
//                this.ShadowProperties["ListBoxFrom"] = value;
//            }
//        }

//        public virtual ListBox ListBoxTo {
//            get { return (ListBox)ShadowProperties["ListBoxTo"]; }
//            set {
//                // note this value is not passed to the actual component
//                this.ShadowProperties["ListBoxTo"] = value;
//            }
//        }

//        protected override void PreFilterProperties(IDictionary properties) {
//            base.PreFilterProperties(properties);

//            // replace Button with our shadowed versions.
//            properties["Button"] = TypeDescriptor.CreateProperty(
//                typeof(DualListDesigner),
//                (PropertyDescriptor)properties["Button"],
//                new Attribute[0]);

//            // replace ListBoxFrom with our shadowed versions.
//            properties["ListBoxFrom"] = TypeDescriptor.CreateProperty(
//                typeof(DualListDesigner),
//                (PropertyDescriptor)properties["ListBoxFrom"],
//                new Attribute[0]);

//            // replace ListBoxTo with our shadowed versions.
//            properties["ListBoxTo"] = TypeDescriptor.CreateProperty(
//                typeof(DualListDesigner),
//                (PropertyDescriptor)properties["ListBoxTo"],
//                new Attribute[0]);

//        }

//    }
//}
//#endif
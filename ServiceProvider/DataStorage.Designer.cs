﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceProvider {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.3.0.0")]
    internal sealed partial class DataStorage : global::System.Configuration.ApplicationSettingsBase {
        
        private static DataStorage defaultInstance = ((DataStorage)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new DataStorage())));
        
        public static DataStorage Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string xmlFileDirectory {
            get {
                return ((string)(this["xmlFileDirectory"]));
            }
            set {
                this["xmlFileDirectory"] = value;
            }
        }
    }
}

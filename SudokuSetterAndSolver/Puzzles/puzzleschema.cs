﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/PuzzleSchema.xsd")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/PuzzleSchema.xsd", IsNullable=false)]
public partial class puzzle {
    
    private object difficultyField;
    
    private object[] puzzlecellsField;
    
    /// <remarks/>
    public object difficulty {
        get {
            return this.difficultyField;
        }
        set {
            this.difficultyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("cell", IsNullable=false)]
    public object[] puzzlecells {
        get {
            return this.puzzlecellsField;
        }
        set {
            this.puzzlecellsField = value;
        }
    }
}
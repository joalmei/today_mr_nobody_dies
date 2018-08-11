using UnityEngine;
using System;
using System.Collections;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    // ------------------------------ PUBLIC ATTRIBUTES ----------------------------- //
    //The name of the bool field that will be in control (MUST BE IN PUBLIC)
    public string ConditionalSourceField    = "";

    //TRUE = Hide in inspector / FALSE = Disable in inspector 
    public bool HideInInspector             = false;


    // ============================================================================== //
    // PUBLIC MEMBERS
    // ============================================================================== //
    // ============================================================================== //
    /// <summary>
    /// Switches an attribute's display in inspector with a boolean
    /// </summary>
    /// <param name="conditionalSourceField"> name of the bool attribute used as switch (MUST BE PUBLIC) </param>    
    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector        = false;
    }

    // ============================================================================== //
    /// <summary>
    /// Switches an attribute's display in inspector with a boolean
    /// </summary>
    /// <param name="conditionalSourceField"> name of the bool attribute used as switch (MUST BE PUBLIC) </param>
    /// <param name="hideInInspector"> true : hides attribute / false : disables selection </param>
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector        = hideInInspector;
    }
}

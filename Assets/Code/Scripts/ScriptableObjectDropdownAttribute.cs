// Copyright (c) ATHellboy (Alireza Tarahomi) Limited. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root.

using System;
using UnityEngine;

namespace ScriptableObjectDropdown
{
    /// <summary>
    /// Indicates how selectable scriptableObjects should be collated in drop-down menu.
    /// </summary>
    public enum ScriptableObjectGrouping
    {
        /// <summary>
        /// No grouping, just show type names in a list; for instance, "MainFolder > NestedFolder > SpecialScriptableObject".
        /// </summary>
        None,
        /// <summary>
        /// Group classes by namespace and show foldout menus for nested namespaces; for
        /// instance, "MainFolder >> NestedFolder >> SpecialScriptableObject".
        /// </summary>
        ByFolder,
        /// <summary>
        /// Group scriptableObjects by folder; for instance, "MainFolder > NestedFolder >> SpecialScriptableObject".
        /// </summary>
        ByFolderFlat
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ScriptableObjectDropdownAttribute : PropertyAttribute
    {
        public ScriptableObjectGrouping grouping = ScriptableObjectGrouping.None;

        public ScriptableObjectDropdownAttribute() { }
    }
}
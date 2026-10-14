namespace ReadyM.SDK.Attributes;

/// <summary>
/// Builds this archetype on another one, taking all of its fields. Including an archetype is inheritance, so there is
/// one at most; combine data with <see cref="IncludeAttribute"/> and mixins instead.
/// </summary>
/// <param name="ArchetypeType">The archetype to build on.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class IncludeArchetypeAttribute(Type ArchetypeType) : Attribute;
using System;

public class EnumCategoryAttribute : Attribute
{
    public string Category { get; }

    public EnumCategoryAttribute(string category)
    {
        Category = category;
    }
}

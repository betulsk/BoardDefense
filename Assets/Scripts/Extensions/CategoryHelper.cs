using System;
using System.Linq;

public static class CategoryHelper 
{
    public static bool IsInCategory(EPoolObjectType gameElementType, string categoryName)
    {
        return HasCategory(gameElementType, categoryName);
    }

    private static bool HasCategory(Enum value, string category)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        var field = type.GetField(name);
        var attribute = field.GetCustomAttributes(false).OfType<EnumCategoryAttribute>().FirstOrDefault();
        return attribute != null && attribute.Category == category;
    }
}

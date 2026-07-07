namespace HelloMaui.Helpers
{

    public class MauiStyleHelpers
    {

        public static T? GetApplicationStyle<T>(string resourceKey)
        {
            ArgumentNullException.ThrowIfNull(resourceKey);

            if (Application.Current?.Resources?.TryGetValue(resourceKey, out object? style) == true)
            {
                if (style is T)
                {
                    return (T)style;
                }

                throw new InvalidCastException($"Resource with key '{resourceKey}' is not of type {typeof(T).FullName}");
            }

            return default;

        }


    }
}

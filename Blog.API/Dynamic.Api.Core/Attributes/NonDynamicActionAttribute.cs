namespace Blog.API.Dynamic.Api.Core.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class NonDynamicActionAttribute : Attribute
    {
    }
}

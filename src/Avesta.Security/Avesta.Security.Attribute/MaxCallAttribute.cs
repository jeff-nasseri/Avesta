namespace Avesta.Security.Attribute;
using System;

[AttributeUsage(AttributeTargets.Method)]
public class MaxCallAttribute : Attribute
{
    public int MAXTry { get; set; }
}

using Avesta.Security.AES.GCM;
using Avesta.Security.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Globalization;
using System.Text;

namespace Avesta.Security.Test;

public class ModifierUnitTest
{
    const string BODY = "HELLO! THIS IS JUST A SAMPLE PASSWORD OR IMPORTANT PHRASE FOR ANY SOFTWARE SYSTEM OR HARDWARE SYSTEM.";
    byte[] BodyInByte = UTF32Encoding.UTF8.GetBytes(BODY);
    readonly IModifier _modifier;
    public ModifierUnitTest()
    {
        _modifier = new Modifier(BodyInByte);
    }
}
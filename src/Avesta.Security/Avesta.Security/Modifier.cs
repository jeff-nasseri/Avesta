using Avesta.Security.AES.GCM;
using Avesta.Security.Attribute;
using Avesta.Security.Interface;
using Avesta.Security.Model;

namespace Avesta.Security;



public class Modifier : SecurityBaseModel, IModifier
{
    readonly AesGcmService _aesGcmService;
    public Modifier(string name, ModifierKey key, byte[] body) : this(body)
    {
        Name = name;
        Key = key;
        Body = body;
    }
    public Modifier(byte[] body)
    {
        Name = Guid.NewGuid().ToString();
        Key = new ModifierKey();
        Body = body;
        _aesGcmService = new AesGcmService();
    }

    public virtual string Name { get; set; }

    [SecurityValue]
    public virtual byte[] Body { get; private set; }
    [SecurityValue]
    public virtual ModifierKey Key { get; private set; }


    public int HorizontalLevel { get; private set; }
    public int VerticalLevel { get; private set; }
    public int ZLevel { get; private set; }


    public string GetStringFromBody() => Convert.ToBase64String(Body);
    public byte[] GetBodyFromString(string str) => Convert.FromBase64String(str);
    public bool Equals(IModifier? other) => Body == other?.Body;


    public IEnumerable<IModifier> MakePairs() => Split();
    public IEnumerable<IModifier> Split(int number = 2)
    {
        var body = _aesGcmService.Encrypt(Body,Key.ValueInByte);
        var result = TreeAESGCMService.GetChilds(body, Key.ValueInByte, number);

        foreach (var item in result)
        {
            yield return new Modifier(body: item.Value)
            {
                HorizontalLevel = item.HLevel,
                VerticalLevel = item.Vlevel,
                ZLevel = item.ZLevel,
                Key = new ModifierKey(item.Key),
                Name = this.Name,
            };
        }
    }


    public byte[] Match(params IModifier[] modifiers)
    {
        throw new NotImplementedException();
    }

    public bool IsMatch(params IModifier[] modifiers)
    {
        throw new NotImplementedException();
    }


}


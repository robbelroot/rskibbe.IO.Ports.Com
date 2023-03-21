namespace rskibbe.IO.Ports.Com.Virtual;

/// <summary>
/// Simple structure representing a COMA <> COMB pair
/// </summary>
public struct VirtualComPortPair
{

    string _nameA;

    public string NameA
    {
        get => _nameA;
        set
        {
            if (_nameA == value)
                return;
            _nameA = value;
            RefreshIdA();
        }
    }

    byte _idA;

    public byte IdA => _idA;

    string _nameB;

    public string NameB
    {
        get => _nameB;
        set
        {
            if (_nameB == value)
                return;
            _nameB = value;
            RefreshIdB();
        }
    }

    byte _idB;

    public byte IdB => _idB;

    public bool IsComplete => !string.IsNullOrWhiteSpace(NameA) && !string.IsNullOrWhiteSpace(NameB);

    public VirtualComPortPair()
    {
        _nameA = string.Empty;
        _idA = 0;
        _nameB = string.Empty;
        _idB = 0;
    }

    public VirtualComPortPair(string nameA, string nameB) : this()
    {
        _nameA = nameA;
        _nameB = nameB;
        RefreshIdA();
        RefreshIdB();
    }

    private void RefreshIdA()
    {
        if (_nameA.ExtractByte(out var id))
            _idA = id;
        else
            _idA = 0;
    }

    private void RefreshIdB()
    {
        if (_nameB.ExtractByte(out var id))
            _idB = id;
        else
            _idB = 0;
    }

    public byte[] ToIdArray()
        => new byte[] { IdA, IdB };

    public string[] ToNameArray()
        => new string[] { NameA, NameB };

    public override string ToString()
        => $"{NameA}<>{NameB}";

}

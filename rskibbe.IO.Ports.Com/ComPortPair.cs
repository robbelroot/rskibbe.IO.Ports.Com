namespace rskibbe.IO.Ports.Com
{
    /// <summary>
    /// Simple structure representing a COMA <> COMB pair
    /// </summary>
    public struct ComPortPair
    {

        public string NameA { get; set; }

        public string NameB { get; set; }

        public bool IsComplete => !string.IsNullOrWhiteSpace(NameA) && !string.IsNullOrWhiteSpace(NameB);

        public ComPortPair()
        {
            NameA = string.Empty;
            NameB = string.Empty;
        }

        public ComPortPair(string nameA, string nameB)
        {
            NameA = nameA;
            NameB = nameB;
        }

        public byte[] ToIdArray()
        {
            NameA.ExtractByte(out var idA);
            NameB.ExtractByte(out var idB);
            return new byte[] { idA, idB };
        }

        public string[] ToNameArray()
            => new string[] { NameA, NameB };

        public override string ToString()
            => $"{NameA}<>{NameB}";

    }
}

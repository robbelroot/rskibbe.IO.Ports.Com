namespace rskibbe.IO.Ports.Com.Virtual
{
    /// <summary>
    /// Simple structure representing a COMA <> COMB pair
    /// </summary>
    public struct VirtualComPortPair
    {

        public string NameA { get; set; }

        public byte IdA
        {
            get
            {
                NameA.ExtractByte(out var idA);
                return idA;
            }
        }

        public string NameB { get; set; }

        public byte IdB
        {
            get
            {
                NameB.ExtractByte(out var idB);
                return idB;
            }
        }

        public bool IsComplete => !string.IsNullOrWhiteSpace(NameA) && !string.IsNullOrWhiteSpace(NameB);

        public VirtualComPortPair()
        {
            NameA = string.Empty;
            NameB = string.Empty;
        }

        public VirtualComPortPair(string nameA, string nameB)
        {
            NameA = nameA;
            NameB = nameB;
        }

        public byte[] ToIdArray()
            => new byte[] { IdA, IdB };

        public string[] ToNameArray()
            => new string[] { NameA, NameB };

        public override string ToString()
            => $"{NameA}<>{NameB}";

    }
}

namespace Labrune
{
    public class LanguageStringRecord
    {
        public uint Hash;
        public string Label;
        public string Text;
        public string InitialText; // To store the original value
        public bool IsModified;

        // Constructor with both values

        public LanguageStringRecord() : this(0,"","")
        {

        }

        public LanguageStringRecord(uint _Hash) : this(_Hash, "", "")
        {
           
        }

        public LanguageStringRecord(string _Label) : this((uint)BinHash.Hash(_Label), _Label, "")
        {

        }

        public LanguageStringRecord(string _Label, string _Text) : this((uint)BinHash.Hash(_Label), _Label, _Text)
        {

        }

        public LanguageStringRecord(uint _Hash, string _Label, string _Text)
        {
            Hash = _Hash;
            Label = _Label;
            Text = _Text;
            InitialText = _Text; // Initialize Original Value
            IsModified = true;
        }

    }
}

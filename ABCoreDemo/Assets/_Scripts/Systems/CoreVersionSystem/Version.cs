using System;

namespace AlchemyBow.CoreDemos.Systems
{
    /// <summary>
    /// Represents a version.
    /// </summary>
    public class Version : IComparable<Version>
    {
        public readonly int major;
        public readonly int minor;
        public readonly int patch;

        public Version(int major, int minor, int patch)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
        }

        public int CompareTo(Version other)
        {
            int result = major.CompareTo(other.major);
            if (result == 0)
            {
                result = minor.CompareTo(other.minor);
                if (result == 0)
                {
                    result = patch.CompareTo(other.patch);
                }
            }
            return result;
        }

        public override string ToString()
        {
            return $"v{major}.{minor}.{patch}";
        }
    } 
}
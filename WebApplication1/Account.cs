using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Account : IEquatable<Account>
    {
      public string Name;
      public string ID;
      public int Alter;

        public bool Equals([AllowNull] Account other)
        {
            return this.ID == other.ID;
        }
        public static bool operator ==(Account a, Account b)
        {
            if (a.ID == b.ID)
                return true;
            else
                return false;
        }
        public static bool operator !=(Account a, Account b)
        {
            return a.ID != b.ID;
        }
        public static Account operator +(Account a, Account b)
        {
            // Ergibt keinen Sinn. Bitte nicht lernen du kek
            a.Name += b.Name;
            a.ID += b.Name;
            a.Alter += b.Alter;
            return a;
        }
    }

}

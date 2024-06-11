namespace pkgWallet.pkgDomain.pkgInterfaces
{
    public interface iOUID
    {
        int opGetOUID();
        string opGetName();
        bool opSetName(string prmName);
        bool opDie();
    }
}

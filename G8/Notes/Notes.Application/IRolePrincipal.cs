

namespace Notes.Application
{
    public interface IRolePrincipal
    {
        public string Name { get;  }

        public int Id { get; }

        public bool IsInRole(string roles);


    }
}


using XBio.Core.Interfaces;

using XBio.Data;

namespace XBio.Service
{
    public class ResumeService
    {
        private readonly ResumeRepository _repo = new ResumeRepository();

        public IResume Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
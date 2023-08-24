using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.DataAccess.Contracts.Repositories
{
    public interface IUnitOfWorkRepository:IDisposable
    {
        IUsercarInformationRepository UserCarInformationRepository { get; }
        public IReceivedSmsRepository ReceivedSmsRepository { get; }
        Task<int> SaveAsync(CancellationToken cancellationToken=default);
    }
}

using BICE.

namespace BICE.DAL.Repositories;

public interface IMaterialUsageHistory_Repository
{
    Task CreateMaterialUsageHistory(MaterialUsageHistoryDTO materialUsageHistory);

}
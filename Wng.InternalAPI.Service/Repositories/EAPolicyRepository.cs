using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

using Wng.InternalAPI.Service.Domain;

namespace Wng.InternalAPI.Service.Repositories
{
    public class EAPolicyRepository
    {
        public IEnumerable<EAPolicy> getPolicies(string policyNumber)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WngConfigEAPolicy"].ConnectionString;
            string query = @"
                            SELECT TOP 10
	                            ph.PolicyNumber,
	                            br.Code as Brand,
	                            CASE WHEN tr.FirstName IS NULL THEN dp.FirstName ELSE tr.FirstName END as FirstName,
	                            CASE WHEN tr.Surname IS NULL THEN dp.Surname ELSE tr.Surname END as Surname,
	                            CASE WHEN tr.DateOfBirth IS NULL THEN dp.DateOfBirth ELSE tr.DateOfBirth END as DateOfBirth,
								tr.EmailAddress,
	                            pd.PolicyDepartureDate,
	                            pd.PolicyReturnDate,
	                            uw.Code as UW
                            FROM [dbo].[PolicyHeader] as ph
                            LEFT JOIN [dbo].[PolicyDetail] as pd
	                            ON ph.PolicyHeaderId = pd.PolicyHeaderId
                            LEFT JOIN [dbo].[Traveller] as tr
	                            ON pd.PolicyDetailId = tr.PolicyDetailId
                            LEFT JOIN [dbo].[Dependent] as dp
	                            ON pd.PolicyDetailId = dp.PolicyDetailId
                            LEFT JOIN [dbo].[PolicyProduct] as pp
	                            ON ph.PolicyProductId = pp.PolicyProductId
                            LEFT JOIN [dbo].[InsuranceContract] as ic
	                            ON pp.InsuranceContractId = ic.InsuranceContractId
                            LEFT JOIN [dbo].[Underwriter] as uw
	                            ON ic.UnderwriterId = uw.UnderwriterId
                            LEFT JOIN [dbo].[Brand] AS br
	                            ON ic.BrandId = br.BrandId
                            WHERE ph.PolicyNumber = @value and pd.SequenceNumber = (
	                            SELECT MAX(SequenceNumber)
	                            FROM [dbo].[PolicyDetail] as ipd
	                            LEFT JOIN [dbo].[PolicyHeader] as iph
		                            ON ipd.PolicyHeaderId = iph.PolicyHeaderID
	                            WHERE iph.PolicyNumber = @value)";

            using (var sqlConnection
                = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                IEnumerable<EAPolicy> policies = sqlConnection
                    .Query<EAPolicy>(query, new { value = policyNumber });

                return policies;
            }
        }
    }
}
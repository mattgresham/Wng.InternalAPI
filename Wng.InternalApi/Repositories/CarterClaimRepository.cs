using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Wng.InternalApi.Domain;
using Wng.InternalApi.Helpers;
using Wng.InternalApi.Interfaces;

namespace Wng.InternalApi.Repositories
{
    public class CarterClaimRepository : ICarterClaimRepository
    {
        public IEnumerable<EaPolicySummary> GetPolicySummary(string policyNumber)
        {
            string policyQuery = @"
                            SELECT TOP 10
	                            ph.PolicyNumber,
	                            br.Code as Brand,
	                            tr.FirstName as FirstName,
	                            tr.Surname as Surname,
	                            tr.DateOfBirth as DateOfBirth,
	                            tr.EmailAddress,
	                            pd.PolicyDepartureDate,
	                            pd.PolicyReturnDate,
	                            uw.Code as Underwriter
                            FROM [dbo].[PolicyHeader] as ph
                            LEFT JOIN [dbo].[PolicyDetail] as pd
	                            ON ph.PolicyHeaderId = pd.PolicyHeaderId
                            LEFT JOIN [dbo].[Traveller] as tr
	                            ON pd.PolicyDetailId = tr.PolicyDetailId
                            LEFT JOIN [dbo].[PolicyProduct] as pp
	                            ON ph.PolicyProductId = pp.PolicyProductId
                            LEFT JOIN [dbo].[InsuranceContract] as ic
	                            ON pp.InsuranceContractId = ic.InsuranceContractId
                            LEFT JOIN [dbo].[Underwriter] as uw
	                            ON ic.UnderwriterId = uw.UnderwriterId
                            LEFT JOIN [dbo].[Brand] AS br
	                            ON ic.BrandId = br.BrandId
                            WHERE ph.PolicyNumber = @PolicyNumber and pd.SequenceNumber = (
	                            SELECT MAX(SequenceNumber)
	                            FROM [dbo].[PolicyDetail] as ipd
	                            LEFT JOIN [dbo].[PolicyHeader] as iph
		                            ON ipd.PolicyHeaderId = iph.PolicyHeaderID
	                            WHERE iph.PolicyNumber = @PolicyNumber)

                            UNION

                            SELECT TOP 10
	                            ph.PolicyNumber,
	                            br.Code as Brand,
	                            dp.FirstName as FirstName,
	                            dp.Surname as Surname,
	                            dp.DateOfBirth as DateOfBirth,
	                            null as EmailAddress,
	                            pd.PolicyDepartureDate,
	                            pd.PolicyReturnDate,
	                            uw.Code as Underwriter
                            FROM [dbo].[PolicyHeader] as ph
                            LEFT JOIN [dbo].[PolicyDetail] as pd
	                            ON ph.PolicyHeaderId = pd.PolicyHeaderId
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
                            WHERE ph.PolicyNumber = @PolicyNumber and dp.FirstName is not null and pd.SequenceNumber = (
	                            SELECT MAX(SequenceNumber)
	                            FROM [dbo].[PolicyDetail] as ipd
	                            LEFT JOIN [dbo].[PolicyHeader] as iph
		                            ON ipd.PolicyHeaderId = iph.PolicyHeaderID
	                            WHERE iph.PolicyNumber = @PolicyNumber)";

            using (var sqlConnection = new SqlConnection(ParameterHelper.Current.CarterClaimDatabaseConnectionString))
            {
                sqlConnection.Open();

                var policies = sqlConnection
                    .Query<EaPolicySummary>(policyQuery, new { PolicyNumber = policyNumber });

                return policies;
            }
        }
    }
}
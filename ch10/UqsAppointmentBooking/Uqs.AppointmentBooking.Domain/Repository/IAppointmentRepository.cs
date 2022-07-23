﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Uqs.AppointmentBooking.Domain.DomainObjects;

namespace Uqs.AppointmentBooking.Domain.Repository;

public interface IAppointmentRepository : ICosmosRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAppoitmentsByEmployeeIdAsync(string employeeId);
}

public class AppointmentRepository : CosmosRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(CosmosClient cosmosClient, IOptions<ApplicationSettings> settings) :
        base(nameof(Appointment), cosmosClient, settings)
    {
    }

    public async Task<IEnumerable<Appointment>> GetAppoitmentsByEmployeeIdAsync(string employeeId)
    {
        var queryDefinition = new QueryDefinition("SELECT * FROM c");
        var appointments = await GetItemsAsync(queryDefinition);
        return appointments.Where(x => x.EmployeeId == employeeId);
    }
}

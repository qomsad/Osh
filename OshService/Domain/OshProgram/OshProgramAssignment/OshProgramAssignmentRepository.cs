﻿using AspBoot.Data.Implementation;
using AspBoot.Data.Model;
using AspBoot.Repository;
using Microsoft.EntityFrameworkCore;
using OshService.Data;

namespace OshService.Domain.OshProgram.OshProgramAssignment;

[Repository]
public class OshProgramAssignmentRepository(DatabaseContext context) : Repository<OshProgramAssignmentModel>(context)
{
    public override IQueryable<OshProgramAssignmentModel> Projection(IQueryable<OshProgramAssignmentModel> queryable)
    {
        queryable = queryable.Include(entity => entity.OshProgram).ThenInclude(program => program.Specialty);
        queryable = queryable.Include(entity => entity.Employee).ThenInclude(employee => employee.Specialty);
        return queryable;
    }

    public OshProgramAssignmentModel? GetById(long id, long organizationId)
    {
        return Get().FirstOrDefault(entity => entity.Id == id
                                              && entity.OshProgram.OrganizationId == organizationId);
    }

    protected override IQueryable<OshProgramAssignmentModel> ApplyFiltering(IQueryable<OshProgramAssignmentModel> query,
        Filter.Predicate? filter)
    {
        if (filter != null
            && filter.Selector.ToLower().Equals("Speciality".ToLower())
            && filter.Operator == Filter.Operator.In)
        {
            var values = filter.Values.Split(";");
            var ids = new long[values.Length];
            if (values.Any())
            {
                for (var i = 0; i < values.Length; i++)
                {
                    Int64.TryParse(values[i], out var val);
                    ids[i] = val;
                }
            }
            query = query.Where(e => ids.Contains(e.OshProgram.SpecialityId ?? 0));
        }

        if (filter != null
            && filter.Selector.ToLower().Equals("Employee".ToLower())
            && filter.Operator == Filter.Operator.In)
        {
            var values = filter.Values.Split(";");
            var ids = new long[values.Length];
            if (values.Any())
            {
                for (var i = 0; i < values.Length; i++)
                {
                    Int64.TryParse(values[i], out var val);
                    ids[i] = val;
                }
            }
            query = query.Where(e => ids.Contains(e.Employee.Id));
        }

        return query;
    }

    public IQueryable<OshProgramAssignmentModel> OrganizationScope(long organizationId,
        IQueryable<OshProgramAssignmentModel> query)
    {
        return query.Where(entity => entity.OshProgram.OrganizationId == organizationId);
    }
}

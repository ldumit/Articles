// talk about global using :
// Benefits :
// - reduces boirleplate using statements across the project.
// - makes the files more readable 
// Drawbacks :
// - can lead to confusion about where the types are coming from.
// - can lead to conflicts if the same type name is defined in multiple namespaces (ex: ValidationMessages)

// Third-party libraries
global using MediatR;
global using Mapster;
global using FluentValidation;

// Internal libraries
global using Blocks.MediatR;
global using Blocks.EntityFrameworkCore;
global using Blocks.FluentValidation;
global using Articles.Abstractions;
global using Articles.Abstractions.Enums;

// Domain
global using Review.Domain.Entities;
global using Review.Domain.Enums;
global using Review.Domain.ValueObjects;
global using Review.Domain.StateMachines;

// Application
global using Review.Application.Features.Shared;

//Persistence
global using Review.Persistence.Repositories;



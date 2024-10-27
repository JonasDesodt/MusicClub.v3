using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Transfer;

namespace MusicClub.v3.DbServices.Extensions
{
    internal static class ServiceMessageExtensions
    {
        public static ServiceMessages AddNotFound(this ServiceMessages serviceMessages, string entity, int? id)
        {
            if (id is not null)
            {
                serviceMessages.Add(new ServiceMessage
                {
                    Code = ErrorCode.NotFound,
                    Description = $"The {entity} with id {id} could not be found."
                });
            }
            else
            {
                serviceMessages.Add(new ServiceMessage
                {
                    Code = ErrorCode.NotFound,
                    Description = $"There is no id provided. The {entity} cannot be found."
                });
            }

            return serviceMessages;
        }

        public static ServiceMessages AddNotCreated(this ServiceMessages serviceMessages, string entity)
        {
            serviceMessages.Add(new ServiceMessage
            {
                Code = ErrorCode.NotCreated,
                Description = $"The {entity} could not be created."
            });

            return serviceMessages;
        }

        public static ServiceMessages AddReferenceFound(this ServiceMessages serviceMessages, string entityA, int idA, string entityB)
        {
            serviceMessages.Add(new ServiceMessage
            {
                Code = ErrorCode.Referenced,
                Description = $"One or more records in the {entityB} table hold a reference to {entityA} with id {idA}."
            });

            return serviceMessages;
        }

        public static ServiceMessages AddNotDeleted(this ServiceMessages serviceMessages, string entity, int id)
        {
            serviceMessages.Add(new ServiceMessage
            {
                Code = ErrorCode.NotDeleted,
                Description = $"The {entity} with id {id} could not be deleted."
            });

            return serviceMessages;
        }

        public static ServiceMessages AddNotUpdated(this ServiceMessages serviceMessages, string entity, int id)
        {
            serviceMessages.Add(new ServiceMessage
            {

                Code = ErrorCode.NotUpdated,    
                Description = $"The {entity} with id {id} could not be updated."
            });

            return serviceMessages;
        }

        public static ServiceMessages AddDuplicate(this ServiceMessages serviceMessages, string entity)
        {
            serviceMessages.Add(new ServiceMessage
            {

                Code = ErrorCode.Duplicate,
                Description = $"You are trying to duplicate a {entity}. The GoogleIdentifier already exists in the database."
            });

            return serviceMessages;
        }
    }
}

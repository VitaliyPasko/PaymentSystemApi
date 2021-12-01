using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PaymentSystem.ApplicationLayer.Services.ErrorIdentifierService.Interfaces;

namespace PaymentSystem.ApplicationLayer.Services.ErrorIdentifierService
{
    public class ErrorIdentifierService<T> : IErrorIdentifierService<T>
    {
        private readonly Dictionary<string, string> _propName;

        public ErrorIdentifierService()
        {
            _propName = new Dictionary<string, string>
            {
                {"ExternalNumber", "External"},
                {"BarCode", "Bar"}
            };
        }

        public bool IdentifyErrorForExternalNumber(DbUpdateException e, T entity)
        {
            const string duplicateCodeException = "23505";
            if (e.InnerException is not PostgresException {SqlState: duplicateCodeException} innerException)
                return false;
            Type type = entity.GetType();
            var properties = type.GetProperties().ToList();
            string propName = SearchProperty(properties);
            if (string.IsNullOrEmpty(propName))
                return false;
            return innerException.Detail != null && innerException.MessageText.Contains(propName);
        }

        private string SearchProperty(List<PropertyInfo> propertyInfos)
        {
            var prop = propertyInfos.FirstOrDefault(a =>
                a.Name.Contains(_propName["ExternalNumber"]) ||
                a.Name.Contains(_propName["BarCode"]));
            return prop?.Name;
        }
    }
}